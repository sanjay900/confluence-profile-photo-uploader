using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.GPU;
using VanClinic.Libraries.LogWriter;

namespace VanClinic.Utilities.ConfluenceProfilePhotos
{
	class PhotoUpload
	{
		private String mSoapURL = String.Empty;
		private String mUsername = String.Empty;
		private String mPassword = String.Empty;
		private String mPhotoPath = String.Empty;
		private Boolean mDeleteOriginal = false;
		private Boolean mArchivePhoto = false;
		private String mArchivePath = String.Empty;
        private String mNoFacePath = String.Empty;
		private Boolean mOverwrite = false;
		private Boolean mAbort = false;
		private Boolean mNoCrop = false;
        private Boolean mPreCropped = false;
		private HubService.ConfluenceSoapServiceService mService = null;
		private String mToken = String.Empty;
        private Boolean mHeadless = false;
        private Int32 finalSize = 256;

		public String SoapURL
		{
			get { return mSoapURL; }
			set { mSoapURL = value; }
		}
		public String Username
		{
			get { return mUsername; }
			set { mUsername = value; }
		}
		public String Password
		{
			get { return mPassword; }
			set { mPassword = value; }
		}
		public String PhotoPath
		{
			get { return mPhotoPath; }
			set { mPhotoPath = value; }
		}
		public Boolean DeleteOriginal
		{
			get { return mDeleteOriginal; }
			set { mDeleteOriginal = value; }
		}
		public Boolean ArchivePhoto
		{
			get { return mArchivePhoto; }
			set { mArchivePhoto = value; }
		}
		public String ArchivePath
		{
			get { return mArchivePath; }
			set { mArchivePath = value; }
		}
        public String NoFacePathPath
        {
            get { return mNoFacePath; }
            set { mNoFacePath = value; }
        }
		public Boolean Overwrite
		{
			get { return mOverwrite; }
			set { mOverwrite = value; }
		}
		public Boolean NoCrop
		{
			get { return mNoCrop; }
			set { mNoCrop = value; }
		}
        public Boolean PreCropped
        {
            get { return mPreCropped; }
            set { mPreCropped = value; }
        }
		public Boolean Abort
		{
			get { return mAbort; }
			set { mAbort = value; }
		}
        //Setting headless to true will keep going even if there is an error on an image upload.
        public Boolean Headless
        {
            get { return mHeadless; }
            set { mHeadless = value; }
        }
        public Int32 FinalSize
        {
            get { return finalSize; }
        }


		public event UploadEventHandler PhotoUploading;
		public event UploadErrorEventHandler UploadError;
        public event FaceDetectionSuccessEventHandler FaceDetectionSuccess;
        public event FaceDetectionFailEventHandler FaceDetectionFail;

		public PhotoUpload()
		{
		}

		public void Upload()
		{
			List<String> files = new List<String>();
			Log log = new Log();
			log.LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log\Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

			try
			{
				LogEntry entry1 = new LogEntry();
				entry1.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
				entry1.Items.Add(new LogEntryItem("Status", "Started syncing profile photos."));
				log.Entries.Add(entry1);
                log.WriteFlatLog();
                log.Entries.Clear();

                mService = GetSoapService();

				mToken = mService.login(mUsername, mPassword);

				files.AddRange(SanitizeFiles(Directory.GetFiles(mPhotoPath, "*.jpg", SearchOption.TopDirectoryOnly), "jpg"));
				files.AddRange(Directory.GetFiles(mPhotoPath, "*.jpeg", SearchOption.TopDirectoryOnly)); //four letter extension ... sanitization not needed
				files.AddRange(SanitizeFiles(Directory.GetFiles(mPhotoPath, "*.tif", SearchOption.TopDirectoryOnly), "tif"));
				files.AddRange(Directory.GetFiles(mPhotoPath, "*.tiff", SearchOption.TopDirectoryOnly)); //four letter extension ... sanitization not needed
				files.AddRange(SanitizeFiles(Directory.GetFiles(mPhotoPath, "*.png", SearchOption.TopDirectoryOnly), "png"));
				files.AddRange(SanitizeFiles(Directory.GetFiles(mPhotoPath, "*.bmp", SearchOption.TopDirectoryOnly), "bmp"));

				LogEntry entry2 = new LogEntry();
				entry2.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
				entry2.Items.Add(new LogEntryItem("Status", "Found " + files.Count.ToString() + " photo(s) to upload."));
				log.Entries.Add(entry2);
                log.WriteFlatLog();
                log.Entries.Clear();

				foreach (String file in files)
				{
                    LogEntry entry3 = new LogEntry();
                    entry3.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
                    entry3.Items.Add(new LogEntryItem("Status", "Uploading " + file));
                    log.Entries.Add(entry3);
                    log.WriteFlatLog();
                    log.Entries.Clear();

                    if (mAbort)
					{
						break;
					}
					UploadPhoto(file);

                    LogEntry entry4 = new LogEntry();
                    entry4.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
                    entry4.Items.Add(new LogEntryItem("Status", "Successfully uploaded " + file));
                    log.Entries.Add(entry4);
                    log.WriteFlatLog();
                    log.Entries.Clear();
				}

				mService.Dispose();

				LogEntry entry5 = new LogEntry();
				entry5.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
				entry5.Items.Add(new LogEntryItem("Status", "Finished syncing profile photos."));
				log.Entries.Add(entry5);
                log.WriteFlatLog();
                log.Entries.Clear();
			}
			catch (Exception ex)
			{
				LogEntry entry = new LogEntry();
				entry.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
				entry.Items.Add(new LogEntryItem("Error", ex.ToString()));
				log.Entries.Add(entry);
                log.WriteFlatLog();
                log.Entries.Clear();

				OnUploadError(new UploadErrorStatusEventArgs(ex));
			}
		}

		private void UploadPhoto(String Photo)
		{
            Rectangle crop = new Rectangle();
			Rectangle face;
			String user = String.Empty;
            Int32 longestSide = 0;
            Double resizePercent = 0;
			Boolean isSquare = false;
            Boolean fileOriginal = false;
            Boolean filePreCropped = false;
            Byte[] bytImg;
            Image<Bgr, Byte> emguimg = null;
            Log log = new Log();
            log.LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log\Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            LogEntry entry = new LogEntry();

            try
            {
                //Isolate out username from filename
                user = Path.GetFileNameWithoutExtension(Photo).ToLower().Replace("_original", "").Replace("_precropped", "");

                fileOriginal = Path.GetFileNameWithoutExtension(Photo).ToLower().EndsWith("_original") ? true : false;
                filePreCropped = Path.GetFileNameWithoutExtension(Photo).ToLower().EndsWith("_precropped") ? true : false;

                emguimg = new Image<Bgr, Byte>(Photo);
                
                //Let's not do face detection on a really huge image so we will scale it down before doing the face detection
                longestSide = emguimg.Height > emguimg.Width ? emguimg.Height : emguimg.Width;

                if (longestSide > 640)
                {
                    //What percent do we need to scale down to get the longest side to 640
                    resizePercent = ((Double)640 / (Double)longestSide);
                    emguimg = emguimg.Resize(resizePercent, Emgu.CV.CvEnum.INTER.CV_INTER_AREA);
                }

                //Is the image square
                isSquare = emguimg.Height == emguimg.Width ? true : false;

                //If image isn't square or if it is square and we want to do face detection and crop on square images
                if (!isSquare || (isSquare && !mNoCrop))
                {
                    //Detect largest face in photo
                    face = GetFace(emguimg);

                    if (face.Width * face.Height != 0)
                    {
                        OnFaceDetectionSuccess(new FaceDetectionSuccessStatusEventArgs(user, Photo));
                        
                        entry = new LogEntry();
                        entry.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));

                        //Get the correct crop box
                        if (mPreCropped)
                        {
                            if (fileOriginal)
                            {
                                crop = GetCropRectangleForOriginal(emguimg, face);
                                entry.Items.Add(new LogEntryItem("Status", "Cropping photo for " + user + " as original."));
                            }
                            else
                            {
                                crop = GetCropRectangleForPreCropped(emguimg, face);
                                entry.Items.Add(new LogEntryItem("Status", "Cropping photo for " + user + " as pre-cropped."));
                            }
                        }
                        else
                        {
                            if (filePreCropped)
                            {
                                crop = GetCropRectangleForPreCropped(emguimg, face);
                                entry.Items.Add(new LogEntryItem("Status", "Cropping photo for " + user + " as pre-cropped."));
                            }
                            else
                            {
                                crop = GetCropRectangleForOriginal(emguimg, face);
                                entry.Items.Add(new LogEntryItem("Status", "Cropping photo for " + user + " as original."));
                            }
                        }
                        log.Entries.Add(entry);
                        log.WriteFlatLog();
                        log.Entries.Clear();
                    }
                    else
                    {
                        OnFaceDetectionFail(new FaceDetectionFailStatusEventArgs(user, Photo));
                        
                        entry = new LogEntry();
                        entry.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
                        entry.Items.Add(new LogEntryItem("Error", "Could not detect a face for \"" + Photo + "\"."));
                        log.Entries.Add(entry);
                        log.WriteFlatLog();
                        log.Entries.Clear();

                        entry = new LogEntry();
                        entry.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
                        entry.Items.Add(new LogEntryItem("Status", "Moving \"" + Photo + "\" to \"" + mNoFacePath + "\" folder."));
                        log.Entries.Add(entry);
                        log.WriteFlatLog();
                        log.Entries.Clear();

                        if (!Directory.Exists(mNoFacePath))
                        {
                            Directory.CreateDirectory(mNoFacePath);
                        }

                        File.Copy(Photo, Path.Combine(mNoFacePath, Path.GetFileName(Photo)), true);
                        File.Delete(Photo);

                        entry = new LogEntry();
                        entry.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
                        entry.Items.Add(new LogEntryItem("Status", "Moved \"" + Photo + "\" to \"" + mNoFacePath + "\" folder."));
                        log.Entries.Add(entry);
                        log.WriteFlatLog();
                        log.Entries.Clear();

                        return;
                    }
                }

                //if (crop.Width > finalSize)
                //{
                    bytImg = PhotoToByteArray(CropPhoto(emguimg, crop).Resize(finalSize, finalSize, Emgu.CV.CvEnum.INTER.CV_INTER_AREA).ToBitmap());
                //}
                //else
                //{
                    // If crop is smaller that finalSize x finalSize then revert to 48 x 48 so we don't upsample and make it super ugly
                //    bytImg = PhotoToByteArray(CropPhoto(emguimg, crop).Resize(48, 48, Emgu.CV.CvEnum.INTER.CV_INTER_AREA).ToBitmap());
               // }

                if (mService.hasUser(mToken, user))
                {
                    OnPhotoUploading(new UploadStatusEventArgs(user, Photo));
                    mService.addProfilePicture(mToken, user, user + "ProfilePhoto.png", "image/png", bytImg);
                }

                //Archive the original after upload
                if (mArchivePhoto)
                {
                    File.Copy(Photo, Path.Combine(mArchivePath, Path.GetFileName(Photo)), true);
                }

                //Delete original after upload
                if (mDeleteOriginal)
                {
                    File.Delete(Photo);
                }
            }
            catch (Exception ex)
            {
                if (!mHeadless)
                {
                    mAbort = true;
                }
                OnUploadError(new UploadErrorStatusEventArgs(ex));
            }
            finally
            {
                if (emguimg != null)
                {
                    emguimg.Dispose();
                }
            }
		}

        public Image<Bgr, Byte> ResizePhoto(Image<Bgr, Byte> Img, Int32 Percent)
        {
            Int32 sourceWidth = Img.Width;
            Int32 sourceHeight = Img.Height;

            Double percent = ((Double)Percent / 100);

            Int32 destWidth = (Int32)(sourceWidth * percent);
            Int32 destHeight = (Int32)(sourceHeight * percent);

            return Img.Resize(destWidth, destHeight, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
        }

        public Rectangle GetCropRectangleForOriginal(Image<Bgr, Byte> img, Rectangle face)
        {
            Rectangle crop = new Rectangle();
            Int32 top = 0;
            Int32 left = 0;
            Int32 cropSize = 0;

            //Initial crop values
            top = face.Top;
            left = face.Left;

            //Crop size will by based on face width ... times it by 2
            cropSize = face.Width * 2;

            //Give a little room above the face ... add 1/3 of face height
            top = top - Convert.ToInt32(Math.Ceiling((Double)face.Height / (Double)3));
            // make sure we don't go beyond top of image
            if (top < 0)
            {
                top = 0;
            }

            //Start the left sideof the crop by subtracting half the width of the face from the left side
            left = left - Convert.ToInt32(Math.Ceiling((Double)face.Width / (Double)2));

            // make sure we don't go beyond left side of image
            if (left < 0)
            {
                left = 0;
                //we went beyond left side of image so the cropsize needs to change
                //so we will take the distance between the left side of the face and zero and add the same amount to the right side of the face to keep the face centered
                cropSize = face.Width + (face.Left * 2);
            }

            //are we going to go past the right side now?
            if (left + cropSize > img.Width)
            {
                //we went past the right side of the image ... take the distance between the right side ofthe face and the right edge and subtract that from the left side of the face to get the left crop start
                left = face.Left - (img.Width - (face.Left + face.Width));

                //set crop size to face width + two times the distance between face right and right side of image
                cropSize = face.Width + ((img.Width - (face.Left + face.Width)) * 2);
            }

            //have we gone beyond the bottom for the crop?
            if (top + cropSize > img.Height)
            {
                //set crop size to the distance between the top of the crop and the bottom of the image.
                cropSize = img.Height - top;
                //set crop left to the face left minus half the crop size.
                left = face.Left - Convert.ToInt32(Math.Ceiling((Double)cropSize / (Double)2));
            }
            if (left < 0)
            {
                left = 0;
            }
            crop = new Rectangle(left, top, cropSize, cropSize);

            return crop;
        }

        public Rectangle GetCropRectangleForPreCropped(Image<Bgr, Byte> img, Rectangle face)
        {
            Rectangle crop = new Rectangle();
            Int32 top = 0;
            Int32 left = 0;
            Int32 cropSize = 0;

            //Initial crop values
            top = face.Top;
            left = face.Left;

            //Crop size will by based on smallest side
            cropSize = img.Height > img.Width ? img.Width : img.Height;

            //Give a little room above the face ... add 1/3 of face height
            top = top - Convert.ToInt32(Math.Ceiling((Double)face.Height / (Double)3));

            //Find center of face then center that inside cropbox size
            left = (face.Left + Convert.ToInt32(Math.Ceiling((Double)face.Width / (Double)2))) - Convert.ToInt32(Math.Ceiling((Double)cropSize / (Double)2));

            // make sure we don't go beyond top and bottom of image
            top = top < 0 ? 0 : top; //top
            top = img.Height < top + cropSize ? img.Height - cropSize : top; //bottom

            // make sure we don't go beyond left and right of image
            left = left < 0 ? 0 : left; //left
            left = img.Width < left + cropSize ? img.Width - cropSize : left; //right

            crop = new Rectangle(left, top, cropSize, cropSize);

            return crop;
        }
        
        public Image<Bgr, Byte> CropPhoto(Image<Bgr, Byte> img, Rectangle CropArea)
        {
            if (CropArea.Width == 0 || CropArea.Height == 0)
            {
                return img;
            }
            else
            {
                return new Image<Bgr, Byte>(img.ToBitmap().Clone(CropArea, img.ToBitmap().PixelFormat));
            }
        }

		public byte[] PhotoToByteArray(System.Drawing.Image imageIn)
		{
			MemoryStream ms = new MemoryStream();
			imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
			return ms.ToArray();
		}

        public HubService.ConfluenceSoapServiceService GetSoapService()
        {
            HubService.ConfluenceSoapServiceService serv = new HubService.ConfluenceSoapServiceService();
            serv = new HubService.ConfluenceSoapServiceService();
			serv.Url = mSoapURL;
            
            return serv;
        }

        public Rectangle GetFace(String Photo)
        {
            Rectangle rect;

            using (Image<Bgr, Byte> img = new Image<Bgr, Byte>(Photo))
            {
                rect = GetFace(img);
            }

            return rect;
        }

        public Rectangle GetFace(Image<Bgr, Byte> img)
        {
            Int64 detectionTime;

            List<Rectangle> faces = new List<Rectangle>();
            Rectangle largestRect = new Rectangle(0, 0, 0, 0);

            Detect(img, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_default.xml"), faces, out detectionTime);

            if (faces.Count > 0)
            {
                largestRect = faces.Aggregate((r1, r2) => (r1.Height * r1.Width) > (r2.Height * r2.Width) ? r1 : r2);
            }

            return largestRect;
        }

		private void Detect(Image<Bgr, Byte> image, String faceFileName, List<Rectangle> faces, out Int64 detectionTime)
		{
			Stopwatch watch;

			if (GpuInvoke.HasCuda)
			{
				using (GpuCascadeClassifier face = new GpuCascadeClassifier(faceFileName))
				{
					watch = Stopwatch.StartNew();
					using (GpuImage<Bgr, Byte> gpuImage = new GpuImage<Bgr, byte>(image))
					using (GpuImage<Gray, Byte> gpuGray = gpuImage.Convert<Gray, Byte>())
					{
						Rectangle[] faceRegion = face.DetectMultiScale(gpuGray, 1.1, 10, Size.Empty);
						faces.AddRange(faceRegion);
					}
					watch.Stop();
				}
			}
			else
			{
				//Read the HaarCascade objects
				using (CascadeClassifier face = new CascadeClassifier(faceFileName))
				{
					watch = Stopwatch.StartNew();
					using (Image<Gray, Byte> gray = image.Convert<Gray, Byte>()) //Convert it to Grayscale
					{
						//normalizes brightness and increases contrast of the image
						gray._EqualizeHist();

						//Detect the faces  from the gray scale image and store the locations as rectangle
						//The first dimensional is the channel
						//The second dimension is the index of the rectangle in the specific channel
						Rectangle[] facesDetected = face.DetectMultiScale(gray, 1.1, 10, new Size(20, 20), Size.Empty);
						faces.AddRange(facesDetected);
					}
					watch.Stop();
				}
			}
			detectionTime = watch.ElapsedMilliseconds;
		}

		/// <summary>
		/// Will take an array of file paths and make sure that only the specified extension in in the array. Wildcards searches with three letter extensions can return more than just that extension.
		/// </summary>
		/// <param name="Files">Array of file paths</param>
		/// <param name="Extension">Extension</param>
		/// <returns></returns>
		public String[] SanitizeFiles(String[] Files, String Extension)
		{
			List<String> newFiles = new List<String>();
			
			foreach (String file in Files)
			{
				if (Path.GetExtension(file).ToLower().Replace(".", "") == Extension.ToLower())
				{
					newFiles.Add(file);
				}
			}

			return newFiles.ToArray();
		}

		protected virtual void OnPhotoUploading(UploadStatusEventArgs e)
		{
			if (PhotoUploading != null)
			{
				PhotoUploading(this, e);
			}
		}
		protected virtual void OnUploadError(UploadErrorStatusEventArgs e)
		{
			if (UploadError != null)
			{
				UploadError(this, e);
			}
		}
        protected virtual void OnFaceDetectionSuccess(FaceDetectionSuccessStatusEventArgs e)
        {
            if (FaceDetectionSuccess != null)
            {
                FaceDetectionSuccess(this, e);
            }
        }
        protected virtual void OnFaceDetectionFail(FaceDetectionFailStatusEventArgs e)
        {
            if (FaceDetectionFail != null)
            {
                FaceDetectionFail(this, e);
            }
        }
	}

	public delegate void UploadEventHandler(object sender, UploadStatusEventArgs e);
	public delegate void UploadErrorEventHandler(object sender, UploadErrorStatusEventArgs e);
    public delegate void FaceDetectionSuccessEventHandler(object sender, FaceDetectionSuccessStatusEventArgs e);
    public delegate void FaceDetectionFailEventHandler(object sender, FaceDetectionFailStatusEventArgs e);

	public class UploadStatusEventArgs : System.EventArgs
	{
		private String mCurrentUploadUser = String.Empty;
		private String mCurrentUploadFile = String.Empty;

		public String CurrentUploadUser
		{
			get { return mCurrentUploadUser; }
		}
		public String CurrentUploadFile
		{
			get { return mCurrentUploadFile; }
		}

		private UploadStatusEventArgs()
		{
		}

		public UploadStatusEventArgs(String CurrentUploadUser, String CurrentUploadFile)
		{
			mCurrentUploadUser = CurrentUploadUser;
			mCurrentUploadFile = CurrentUploadFile;
		}

	}
    public class UploadErrorStatusEventArgs : System.EventArgs
	{
		private Exception mUploadError = new Exception();

		public Exception UploadError
		{
			get { return mUploadError; }
		}

		private UploadErrorStatusEventArgs()
		{
		}

		public UploadErrorStatusEventArgs(Exception UploadError)
		{
			mUploadError = UploadError;
		}

	}
    public class FaceDetectionSuccessStatusEventArgs : System.EventArgs
    {
        private String mCurrentUploadUser = String.Empty;
        private String mCurrentUploadFile = String.Empty;

        public String CurrentUploadUser
        {
            get { return mCurrentUploadUser; }
        }
        public String CurrentUploadFile
        {
            get { return mCurrentUploadFile; }
        }

        private FaceDetectionSuccessStatusEventArgs()
        {
        }

        public FaceDetectionSuccessStatusEventArgs(String CurrentUploadUser, String CurrentUploadFile)
        {
            mCurrentUploadUser = CurrentUploadUser;
            mCurrentUploadFile = CurrentUploadFile;
        }

    }
    public class FaceDetectionFailStatusEventArgs : System.EventArgs
    {
        private String mCurrentUploadUser = String.Empty;
        private String mCurrentUploadFile = String.Empty;

        public String CurrentUploadUser
        {
            get { return mCurrentUploadUser; }
        }
        public String CurrentUploadFile
        {
            get { return mCurrentUploadFile; }
        }

        private FaceDetectionFailStatusEventArgs()
        {
        }

        public FaceDetectionFailStatusEventArgs(String CurrentUploadUser, String CurrentUploadFile)
        {
            mCurrentUploadUser = CurrentUploadUser;
            mCurrentUploadFile = CurrentUploadFile;
        }

    }
}
