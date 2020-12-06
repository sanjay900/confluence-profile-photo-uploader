using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VanClinic.Utilities.ConfluenceProfilePhotos.HubService;

namespace VanClinic.Utilities.ConfluenceProfilePhotos
{
	public partial class Uploader : Form
	{
		Help help = null;
        Test test = null;
        Manual manual = null;
		MyConfig config = new MyConfig();
		BackgroundWorker BGW = null;
		PhotoUpload upload = null;
        Boolean faceDetectFail = false;
		
		public Uploader()
		{
			InitializeComponent();
		}

		private void Uploader_Load(object sender, EventArgs e)
		{
            UpdateCheck check = new UpdateCheck();
            Version CurrentVersion;
            Version ThisVersion;
            Boolean upToDate = false;

            try
            {
                upToDate = check.UpToDate(out ThisVersion, out CurrentVersion);
                if (!upToDate)
                {
                    MessageBox.Show("There is a newer version of the Confluence profile photo uploader.\r\nThis version: " + ThisVersion.ToString() + "\r\nAvailable version: " + CurrentVersion.ToString());
                }
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Message.Contains("Proxy Authentication Required"))
                {
                    MessageBox.Show("Could not connect to check for an update. If you are behind a proxy set the proxy settings in the Update Check Config tab.");
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            SizeHelpText(450);
            
            /////////////////////////
            // Uploader Config Tab //
            /////////////////////////
            txtWikiUrl.Text = config.WikiURL;
			txtUser.Text = config.Username;
			try
			{
				txtPassword.Text = StringCipher.Decrypt(config.Password);
			}
			catch
			{
				txtPassword.Text = "";
			}
			txtImageFolder.Text = config.PhotoPath;
			chkArchive.Checked = config.Archive;
			txtArchiveFolder.Text = config.Archive ? config.ArchivePath : "";
            txtNoFaceFolder.Text = config.NoFacePath;
			chkDelete.Checked = config.Delete;
			chkNoCrop.Checked = config.NoCrop;
            chkPreCropped.Checked = config.PreCropped;

            //////////////////////
            // Email Config Tab //
            //////////////////////
            chkUpdateEmail.Checked = config.UpdateEmail;
            chkFaceDetectionEmail.Checked = config.FaceDetectFailedEmail;
            txtSMTPServer.Text = config.SMTPServer;
            nudSMTPPort.Value = config.SMTPPort;
            txtSMTPUser.Text = config.SMTPUserName;
            try
            {
                txtSMTPPassword.Text = StringCipher.Decrypt(config.SMTPPassword);
            }
            catch
            {
                txtSMTPPassword.Text = "";
            }
            txtMailFrom.Text = config.MailFrom;
            txtMailTo.Text = config.MailTo;
            txtMailSubject.Text = config.MailSubject;
            txtMailBody.Text = config.MailBody;

            ///////////////////////////////
            // Update Checker Config Tab //
            ///////////////////////////////
            chkUseProxy.Checked = config.UseProxy;
            txtProxyAddress.Text = config.UseProxy ? config.ProxyAddress : "";
            txtProxyPort.Text = config.UseProxy ? config.ProxyPort : "";
            txtProxyUsername.Text = config.UseProxy ? config.ProxyUsername : "";
            if (config.UseProxy)
            {
                try
                {
                    txtProxyPassword.Text = StringCipher.Decrypt(config.ProxyPassword);
                }
                catch
                {
                    txtProxyPassword.Text = "";
                }
            }
		}
		
		private void tsbtnSave_Click(object sender, EventArgs e)
		{
			String error = String.Empty;

			if (txtWikiUrl.Text == "")
			{
				error += "You must enter the wiki URL.\r\n";
			}
			if (txtUser.Text == "")
			{
				error += "You must enter a username.\r\n";
			}
			if (txtPassword.Text == "")
			{
				error += "You must enter a password.\r\n";
			}
			if (txtImageFolder.Text == "")
			{
				error += "You must enter a profile photo folder.\r\n";
			}
			if (chkArchive.Checked && txtArchiveFolder.Text == "")
			{
				error += "You must enter an archive folder.\r\n";
			}
            if (txtNoFaceFolder.Text == "")
            {
                error += "You must enter a no detected face folder.\r\n";
            }

			if (error != "")
			{
				MessageBox.Show(error);
				tslblStatus.Text = "Config not saved.";
				return;
			}

            /////////////////////////
            // Uploader Config Tab //
            /////////////////////////
			config.WikiURL = txtWikiUrl.Text.TrimEnd('/');
			config.Username = txtUser.Text;
			config.Password = StringCipher.Encrypt(txtPassword.Text);
			config.PhotoPath = txtImageFolder.Text;
			config.Archive = chkArchive.Checked;
			config.ArchivePath = chkArchive.Checked ? txtArchiveFolder.Text : "";
            config.NoFacePath = txtNoFaceFolder.Text;
            config.Delete = chkDelete.Checked;
			config.NoCrop = chkNoCrop.Checked;
            config.PreCropped = chkPreCropped.Checked;

            //////////////////////
            // Email Config Tab //
            //////////////////////
            config.UpdateEmail = chkUpdateEmail.Checked;
            config.FaceDetectFailedEmail = chkFaceDetectionEmail.Checked;
            config.SMTPServer = txtSMTPServer.Text;
            config.SMTPPort = Convert.ToInt32(nudSMTPPort.Value);
            config.SMTPUserName = txtSMTPUser.Text;
            config.SMTPPassword = StringCipher.Encrypt(txtSMTPPassword.Text);
            config.MailFrom = txtMailFrom.Text;
            config.MailTo = txtMailTo.Text;
            config.MailSubject = txtMailSubject.Text;
            config.MailBody = txtMailBody.Text;

            ///////////////////////////////
            // Update Checker Config Tab //
            ///////////////////////////////
            config.UseProxy = chkUseProxy.Checked;
            config.ProxyAddress = txtProxyAddress.Text;
            config.ProxyPort = txtProxyPort.Text;
            config.ProxyUsername = txtProxyUsername.Text;
            config.ProxyPassword = StringCipher.Encrypt(txtProxyPassword.Text);
			
			tslblStatus.Text = "Config saved.";
		}

		private void tsbtnUpload_Click(object sender, EventArgs e)
		{
			//Save settings
			tsbtnSave_Click(null, null);

            //Reset this variable each time around
            faceDetectFail = false;
			
			//Upload worker
			BGW = new BackgroundWorker();
			BGW.WorkerSupportsCancellation = true;
			BGW.WorkerReportsProgress = true;
			BGW.DoWork += new DoWorkEventHandler(bgw_DoWork);
			BGW.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
			BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
			BGW.RunWorkerAsync(BGW);
		}

		private void tsbtnAbort_Click(object sender, EventArgs e)
		{
			upload.Abort = true;
		}

        private void tsbtnTest_Click(object sender, EventArgs e)
        {
            if (test == null || test.IsDisposed)
            {
                test = new Test();
            }
            test.Show();
        }

        private void tsbtnManual_Click(object sender, EventArgs e)
        {
            //Save settings
            tsbtnSave_Click(null, null);

            if (manual == null || manual.IsDisposed)
            {
                manual = new Manual();
            }
            manual.Show();
        }

		private void btnImageFolder_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				txtImageFolder.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void btnArchivePath_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				txtArchiveFolder.Text = folderBrowserDialog.SelectedPath;
			}
		}

        private void btnNoFaceFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtNoFaceFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

		private void Uploader_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
		{
            if (help == null || help.IsDisposed)
            {
                help = new Help();
            }
            help.Show();
		}

		private void chkArchive_CheckedChanged(object sender, EventArgs e)
		{
			if (chkArchive.Checked)
			{
				txtArchiveFolder.Enabled = true;
				btnArchivePath.Enabled = true;
			}
			else
			{
				txtArchiveFolder.Enabled = false;
				txtArchiveFolder.Text = "";
				btnArchivePath.Enabled = false;
			}
		}

        private void chkUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            lblProxyAddress.Enabled = chkUseProxy.Checked;
            lblProxyPassword.Enabled = chkUseProxy.Checked;
            lblProxyPort.Enabled = chkUseProxy.Checked;
            lblProxyUsername.Enabled = chkUseProxy.Checked;
            txtProxyAddress.Enabled = chkUseProxy.Checked;
            txtProxyPassword.Enabled = chkUseProxy.Checked;
            txtProxyPort.Enabled = chkUseProxy.Checked;
            txtProxyUsername.Enabled = chkUseProxy.Checked;
        }

        private void btnUpdateCheck_Click(object sender, EventArgs e)
        {
            UpdateCheck check = new UpdateCheck();
            Version CurrentVersion;
            Version ThisVersion;
            Boolean upToDate = false;

            try
            {
                //Save settings
                tsbtnSave_Click(null, null);

                upToDate = check.UpToDate(out ThisVersion, out CurrentVersion);
                MessageBox.Show("Up to date: " + upToDate.ToString() + "\r\nThis version: " + ThisVersion.ToString() + "\r\nAvailable version: " + CurrentVersion.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect to check for an update. If you are behind a proxy set the proxy settings.");
            }
        }
        
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				postBGWUpdate("Enable Form", "pnlControls,false");
				postBGWUpdate("Status Update", "Config saved.");

				upload = new PhotoUpload();
				upload.SoapURL = config.WikiURL + config.SoapPath;
				upload.Username = config.Username;
				upload.Password = StringCipher.Decrypt(config.Password);
				upload.PhotoPath = config.PhotoPath;
				upload.ArchivePhoto = config.Archive;
				upload.ArchivePath = config.ArchivePath;
                upload.NoFacePathPath = config.NoFacePath;
                upload.DeleteOriginal = config.Delete;
				upload.NoCrop = config.NoCrop;
                upload.PreCropped = config.PreCropped;
				upload.PhotoUploading += new UploadEventHandler(upload_PhotoUploading);
				upload.UploadError += new UploadErrorEventHandler(upload_UploadError);
                upload.FaceDetectionFail += new FaceDetectionFailEventHandler(upload_FaceDetectionFail);
				upload.Upload();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				postBGWUpdate("Status Update", "Finished.");
				postBGWUpdate("Enable Form", "pnlControls,true");
			}
		}

		private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			String message = String.Empty;
			Exception ex = null;
			switch (e.UserState.GetType().Name)
			{
				case "Dictionary`2":
					Dictionary<String, Object> progress = (Dictionary<String, Object>)e.UserState;

					if (progress.ContainsKey("Status Update"))
					{
						UpdateStatus(progress["Status Update"].ToString());
					}
					else if (progress.ContainsKey("Enable Form"))
					{
						String[] args = progress["Enable Form"].ToString().Split(',');

						if (args[1] == "true")
						{
							enableForm(this.Controls[args[0]], true);
						}
						else if (args[1] == "false")
						{
							enableForm(this.Controls[args[0]], false);
						}
					}
					break;
				case "Exception":
				case "WebException":
				case "SoapException":
					ex = (Exception)e.UserState;

					//Login failure
					if (ex.Message.Contains("com.atlassian.confluence.rpc.AuthenticationFailedException"))
					{
						message = ex.Message.Replace("com.atlassian.confluence.rpc.AuthenticationFailedException:", "");
						
					}
					//Bad URL
					else if (ex.Message.Contains("The remote name could not be resolved:"))
					{
						message = ex.Message;
					}
					//Everything else
					else
					{
						message = ex.ToString();
					}

					MessageBox.Show("Error:\r\n" + message);
					break;
				default:
					//Some kind of exception not accounted for
					if (e.UserState.GetType().Name.ToUpper().Contains("EXCEPTION"))
					{
						ex = (Exception)e.UserState;
						MessageBox.Show("Error:\r\n" + ex.ToString());
					}
					break;
			}
		}

		private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
            if (faceDetectFail)
            {
                MessageBox.Show("The uploader was unable to find a face in at least one of the photos. Please use the Manual Upload button to manually crop the image(s) and upload.");
            }
		}

		private void upload_PhotoUploading(object sender, UploadStatusEventArgs e)
		{
			postBGWUpdate("Status Update", "Uploading photo " + e.CurrentUploadFile + " for user " + e.CurrentUploadUser + ".");
		}

		private void upload_UploadError(object sender, UploadErrorStatusEventArgs e)
		{
			BGW.ReportProgress(0, e.UploadError);
		}

        private void upload_FaceDetectionFail(object sender, FaceDetectionFailStatusEventArgs e)
        {
            faceDetectFail = true;
        }

        private void postBGWUpdate(String key, Object value)
		{
			Dictionary<String, Object> progress = new Dictionary<String, Object>();
			progress.Add(key, value);
			BGW.ReportProgress(0, progress);
		}

		private void enableForm(Control parent, Boolean enable)
		{
			//Toggle save config button
			tsbtnSave.Enabled = enable;

			//Toggle the upload/cancel button
			tsbtnAbort.Enabled = !enable;
			tsbtnAbort.Visible = !enable;
			tsbtnUpload.Enabled = enable;
			tsbtnUpload.Visible = enable;

			//Toggle form fields
			foreach (Control ctrl in parent.Controls)
			{
				if (ctrl.Name == "txtArchiveFolder" || ctrl.Name == "btnArchivePath")
				{
					if (enable)
					{
						if (chkArchive.Checked)
						{
							ctrl.Enabled = enable;
						}
					}
					else
					{
						ctrl.Enabled = enable;
					}
				}
				else
				{
					ctrl.Enabled = enable;
				}

				if (ctrl.HasChildren)
				{
					enableForm(ctrl, enable);
				}
			}
		}

		private void UpdateStatus(String Status)
		{
			tslblStatus.Text = Status;
			this.Update();
        }

        private void SizeHelpText(Int32 Width)
        {
            for(Int32 i = 1; i <= 25; i++)
            {
                Control cntrl = this.Controls.Find("lblHelp" + i.ToString(), true)[0];
                toolTip.SetToolTip(cntrl, ConstrainTextWidth(toolTip.GetToolTip(cntrl), cntrl.Font, Width));
            }
        }
        
        private String ConstrainTextWidth(String Text, Font FontFace, Int32 Width)
        {
            StringBuilder sb = new StringBuilder();
            Graphics g = this.CreateGraphics();
            Int32 i = 0;
            String temp = String.Empty;

            Text = Text.Trim() + ' ';

            try
            {
                if (MeasureDisplayStringWidth(g, Text, FontFace) <= Width)
                {
                    return Text;
                }
                else
                {
                    while (Text.Length > 0)
                    {
                        i = Text.IndexOf(' ');
                        if (MeasureDisplayStringWidth(g, temp + Text.Substring(0, i + 1), FontFace) < Width)
                        {
                            temp += Text.Substring(0, i + 1);
                            Text = Text.Substring(i + 1, Text.Length - (i + 1));
                        }
                        else
                        {
                            sb.AppendLine(temp);
                            temp = String.Empty;
                        }
                    }
                    sb.AppendLine(temp);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                g.Dispose();
            }

            return sb.ToString().TrimEnd();
        }

        private Int32 MeasureDisplayStringWidth(Graphics graphics, string text, Font font)
        {
            System.Drawing.StringFormat format = new System.Drawing.StringFormat();
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(0, 0, 1000, 1000);
            System.Drawing.CharacterRange[] ranges = { new System.Drawing.CharacterRange(0, text.Length) };
            System.Drawing.Region[] regions = new System.Drawing.Region[1];

            format.SetMeasurableCharacterRanges(ranges);
            regions = graphics.MeasureCharacterRanges(text, font, rect, format);
            rect = regions[0].GetBounds(graphics);

            return (int)(rect.Right + 1.0f);
        }
	}
}
