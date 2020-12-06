using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;


namespace VanClinic.Utilities.ConfluenceProfilePhotos
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            PhotoUpload ul = new PhotoUpload();
            Rectangle face;
            Rectangle cropRectOriginal;
            Rectangle cropRectPreCrop;
            Int32 finalSize = 256;
            Image<Bgr, Byte> img = null;
            Image<Bgr, Byte> img2 = null;
            Int32 longestSide = 0;
            Double resizePercent = 0;

            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = openFileDialog.FileName;

                    img = new Image<Bgr, Byte>(txtFile.Text);

                    //Let's not do face detection on a really huge image so we will scale it down before doing the face detection
                    longestSide = img.Height > img.Width ? img.Height : img.Width;

                    if (longestSide > 640)
                    {
                        //What percent do we need to scale down to get the longest side to 640
                        resizePercent = ((Double)640 / (Double)longestSide);
                        img = img.Resize(resizePercent, Emgu.CV.CvEnum.INTER.CV_INTER_AREA);
                    }
                    
                    face = ul.GetFace(img);
                    cropRectOriginal = ul.GetCropRectangleForOriginal(img, face);
                    cropRectPreCrop = ul.GetCropRectangleForPreCropped(img, face);

                    img2 = img.Clone();
                    ibCropOriginal.Image = ul.CropPhoto(img2, cropRectOriginal).Resize(finalSize, finalSize, Emgu.CV.CvEnum.INTER.CV_INTER_AREA);
                    ibCropPreCrop.Image = ul.CropPhoto(img2, cropRectPreCrop).Resize(finalSize, finalSize, Emgu.CV.CvEnum.INTER.CV_INTER_AREA);
                    img2.Dispose();

                    img.Draw(face, new Bgr(Color.Red), 2);
                    img.Draw(cropRectOriginal, new Bgr(Color.Blue), 2);
                    img.Draw(cropRectPreCrop, new Bgr(Color.Green), 2);
                    ibPicture.Image = img;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (img != null)
                {
                    img.Dispose();
                }
                if(img2 != null)
                {
                    img2.Dispose();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ibCropOriginal.Image.Save(saveFileDialog.FileName);
            }
        }

        private void btnSavePreCropped_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ibCropPreCrop.Image.Save(saveFileDialog.FileName);
            }
        }
    }
}
