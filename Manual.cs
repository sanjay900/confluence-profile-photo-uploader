using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using VanClinic.Libraries.LogWriter;

namespace VanClinic.Utilities.ConfluenceProfilePhotos
{
    public partial class Manual : Form
    {
        MyConfig config = new MyConfig();
        PhotoUpload upload = new PhotoUpload();
        Image picture;
        Point mouseDownLoc;
        Double min = 0;
        HubService.ConfluenceSoapServiceService mService = null;
        String mToken = String.Empty;

        public Manual()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
        }
        
        ~Manual()
        {
        }

        #region Form Event Handlers
        private void Manual_Load(object sender, EventArgs e)
        {
            try
            {
                SetColWidth();
                PopulateList();
                pbPerson.Width = upload.FinalSize;
                pbPerson.Height = upload.FinalSize;
                upload.SoapURL = config.WikiURL + config.SoapPath;
                upload.Username = config.Username;
                upload.Password = StringCipher.Decrypt(config.Password);

                mService = upload.GetSoapService();
                mToken = mService.login(upload.Username, upload.Password);
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                if(ex.Message.Contains("AuthenticationFailedException"))
                {
                    DisplayError("The username and password combination supplied are incorrect.");
                }
                else
                {
                    DisplayError(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex.ToString());
            }
        }

        private void Manual_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mService != null)
            {
                mService.Dispose();
            }
        }

        private void Manual_Resize(object sender, EventArgs e)
        {
            SetColWidth();
        }
        #endregion

        private void btnUpload_Click(object sender, EventArgs e)
        {
            Byte[] bytImg = null;
            String user = String.Empty;
            String path = String.Empty;
            Log log = new Log();
            log.LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log\Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

            try
            {
                foreach (ListViewItem item in lvNoFace.SelectedItems)
                {
                    path = item.Text;
                    user = Path.GetFileNameWithoutExtension(item.Text).ToLower().Replace("_original", "").Replace("_precropped", "");
                }

                LogEntry entry1 = new LogEntry();
                entry1.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
                entry1.Items.Add(new LogEntryItem("Status", "Manually uploading photo for " + user + " to Confluence."));
                log.Entries.Add(entry1);
                log.WriteFlatLog();
                log.Entries.Clear();

                using (Image<Bgr, Byte> img = new Image<Bgr, Byte>(new Bitmap(CroppedImage())))
                {
                    bytImg = upload.PhotoToByteArray(img.Resize(upload.FinalSize, upload.FinalSize, Emgu.CV.CvEnum.INTER.CV_INTER_AREA).ToBitmap());

                    if (bytImg != null)
                    {
                        if (mService.hasUser(mToken, user))
                        {
                            mService.addProfilePicture(mToken, user, user + "ProfilePhoto.png", "image/png", bytImg);
                        }
                    }
                }

                if (config.Archive)
                {
                    File.Copy(path, Path.Combine(config.ArchivePath, Path.GetFileName(path)), true);
                    File.Delete(path);

                    foreach (ListViewItem item in lvNoFace.SelectedItems)
                    {
                        lvNoFace.Items[item.Index].Remove();
                    }
                }

                ResetImage();

                LogEntry entry3 = new LogEntry();
                entry3.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
                entry3.Items.Add(new LogEntryItem("Status", "Manual upload complete for " + user + "."));
                log.Entries.Add(entry3);
                log.WriteFlatLog();
                log.Entries.Clear();

                tslblStatus.Text = "Successfully uploaded " + path;
            }
            catch(Exception ex)
            {
                LogEntry entry2 = new LogEntry();
                entry2.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
                entry2.Items.Add(new LogEntryItem("Error", ex.ToString()));
                log.Entries.Add(entry2);
                log.WriteFlatLog();
                log.Entries.Clear();

                DisplayError(ex.ToString());
            }
        }

        #region Image Manipulator Event Handlers
        private void lvNoFace_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            foreach (ListViewItem item in lvNoFace.SelectedItems)
            {
                LoadImage(item.Text);
            }
        }

        private void trkSize_Scroll(object sender, EventArgs e)
        {
            pbPerson.Image = ResizeImage(picture, trkSize.Value);

            if (pbPerson.Image.Width <= pnlPict.Width)
            {
                pbPerson.Location = new Point(0, pbPerson.Location.Y);
            }
            if (pbPerson.Image.Height <= pnlPict.Height)
            {
                pbPerson.Location = new Point(pbPerson.Location.X, 0);
            }
        }

        private void pbEmployee_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLoc = e.Location;
            }
        }

        private void pbEmployee_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point currentMousePos = e.Location;
                int distanceX = currentMousePos.X - mouseDownLoc.X;
                int distanceY = currentMousePos.Y - mouseDownLoc.Y;
                int newX = pbPerson.Location.X + distanceX;
                int newY = pbPerson.Location.Y + distanceY;

                if (newX + pbPerson.Image.Width < pbPerson.Image.Width && pbPerson.Image.Width + newX > pnlPict.Width)
                {
                    pbPerson.Location = new Point(newX, pbPerson.Location.Y);
                }
                if (newY + pbPerson.Image.Height < pbPerson.Image.Height && pbPerson.Image.Height + newY > pnlPict.Height)
                {
                    pbPerson.Location = new Point(pbPerson.Location.X, newY);
                }
            }
        }
        #endregion

        #region Methods
        private void SetColWidth()
        {
            colFileName.Width = lvNoFace.Width - 24;
        }

        private void PopulateList()
        {
            List<String> files = new List<String>();

            try
            {
                files.AddRange(upload.SanitizeFiles(Directory.GetFiles(config.NoFacePath, "*.jpg", SearchOption.TopDirectoryOnly), "jpg"));
                files.AddRange(Directory.GetFiles(config.NoFacePath, "*.jpeg", SearchOption.TopDirectoryOnly)); //four letter extension ... sanitization not needed
                files.AddRange(upload.SanitizeFiles(Directory.GetFiles(config.NoFacePath, "*.tif", SearchOption.TopDirectoryOnly), "tif"));
                files.AddRange(Directory.GetFiles(config.NoFacePath, "*.tiff", SearchOption.TopDirectoryOnly)); //four letter extension ... sanitization not needed
                files.AddRange(upload.SanitizeFiles(Directory.GetFiles(config.NoFacePath, "*.png", SearchOption.TopDirectoryOnly), "png"));
                files.AddRange(upload.SanitizeFiles(Directory.GetFiles(config.NoFacePath, "*.bmp", SearchOption.TopDirectoryOnly), "bmp"));

                foreach (String file in files)
                {
                    lvNoFace.Items.Add(file);
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Unable to find the \"No Detected Face Folder\".");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        
        private void LoadImage(String Path)
        {
            Int32 smallestSide = 0;
            Double percent = 0;
            FileStream fs;
            
            ResetImage();

            try
            {
                EnableForm(pnlObjects, false);

                fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
                picture = Image.FromStream(fs, true);
                fs.Dispose();

                smallestSide = picture.Width > picture.Height ? picture.Height : picture.Width;
                
                //Image smallest side should be at least as big as the picture box
                if (smallestSide < pbPerson.Width)
                {
                    percent = ((Double)pbPerson.Width / (Double)smallestSide);

                    picture = ResizeImage(picture, Convert.ToInt32(Math.Ceiling(percent * (Double)100)));

                    smallestSide = picture.Width > picture.Height ? picture.Height : picture.Width;
                }
                
                min = ((Double)pnlPict.Width / (Double)smallestSide) * 100;
                min = RoundUp(min);
                trkSize.Minimum = (Int32)min;

                pbPerson.Image = picture;
            }
            catch
            {
            }
            finally
            {
                EnableForm(pnlObjects, true);
            }
        }

        private void ResetImage()
        {
            trkSize.Minimum = 0;
            trkSize.Maximum = 100;
            trkSize.Value = 100;
            pbPerson.Image = null;
            pbPerson.Location = new Point(0, 0);
        }

        private Image ResizeImage(Image imgToResize, Int32 size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            double percent = ((Double)size / 100);

            int destWidth = (int)(sourceWidth * percent);
            int destHeight = (int)(sourceHeight * percent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.CompositingMode = CompositingMode.SourceCopy;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;


            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        private Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea,
            bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        private Image CroppedImage()
        {
            Rectangle rect;
            Int32 cropTop = 0;
            Int32 cropLeft = 0;
            Image croppedImage = null;

            if (pbPerson.Image != null)
            {
                cropTop = pbPerson.Top * -1;
                cropLeft = pbPerson.Left * -1;
                rect = new Rectangle(cropLeft, cropTop, pnlPict.Width, pnlPict.Height);
                croppedImage = CropImage(pbPerson.Image, rect);
            }

            return croppedImage;
        }

        private void EnableForm(Control parent, Boolean enable)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Enabled = enable;

                if (ctrl.HasChildren)
                {
                    EnableForm(ctrl, enable);
                }
            }
        }
        
        private double RoundUp(double valueToRound)
        {
            return (Math.Round(valueToRound + 0.5));
        }

        private void DisplayError(String Error)
        {
            EnableForm(pnlObjects, false);
            
            MessageBox.Show(Error);
        }
        #endregion
    }
}
