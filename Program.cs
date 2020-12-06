using System;
using System.IO;
using System.Windows.Forms;
using VanClinic.Libraries.LogWriter;
using VanClinic.Libraries.EmailBuilder;

namespace VanClinic.Utilities.ConfluenceProfilePhotos
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			String arg = args.Length > 0 ? args[0] : "";
            if (PreReqCheck.HasVCP11())
            {
                switch (arg)
                {
                    case "":
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Uploader());
                        break;
                    case "/h":
                    case "-h":
                    case "/help":
                    case "-help":
                    case "/?":
                    case "-?":
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Help());
                        break;
                    case "/headless":
                        Upload();
                        break;
                }
            }
            else
            {
                MessageBox.Show("You must have the Microsoft Visual C++ 2012 Redistributable x86 package installed. Please install that first.");
            }
		}

		static void Upload()
		{
			try
			{
				MyConfig config = new MyConfig();
				PhotoUpload upload = new PhotoUpload();
                UpdateCheck check = new UpdateCheck();
                Version CurrentVersion;
                Version ThisVersion;
                Version LastUpdateCheck;
                Boolean upToDate = false;
                EmailBuilder email = new EmailBuilder();

                if (config.UpdateEmail)
                {
                    LastUpdateCheck = new Version(config.LastUpdateEmailVersion);

                    try
                    {
                        upToDate = check.UpToDate(out ThisVersion, out CurrentVersion);
                        if (!upToDate && LastUpdateCheck.CompareTo(CurrentVersion) < 0)
                        {
                            config.LastUpdateEmailVersion = CurrentVersion.ToString();

                            email.SMTPServer = config.SMTPServer;
                            email.SMTPPort = config.SMTPPort;
                            email.SMTPUserName = config.SMTPUserName;
                            try
                            {
                                email.SMTPPassword = StringCipher.Decrypt(config.SMTPPassword);
                            }
                            catch
                            {
                                email.SMTPPassword = "";
                            }
                            email.MailFrom = config.MailFrom;
                            foreach (String to in config.MailTo.Split(';'))
                            {
                                email.MailTo.Add(to.Trim());
                            }
                            email.MailSubject = "Confluence Profile Photo Uploader Update Available";
                            email.MailBody = "There is a newer version of the Confluence Profile Photo Uploader.<br>This version: " + ThisVersion.ToString() + "<br>Available version: " + CurrentVersion.ToString() + "<br><a href='https:////bitbucket.org//fredclown//confluence-profile-photo-uploader//downloads'>Get the latest version</a>";
                            email.HTMLBody = true;
                            email.Send();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log log = new Log();
                        log.LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log\Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                        LogEntry entry = new LogEntry();
                        entry.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
                        entry.Items.Add(new LogEntryItem("Error", ex.ToString()));
                        log.Entries.Add(entry);
                        log.WriteFlatLog();
                        log.Entries.Clear();
                    }
                }

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
                upload.Headless = true;
				upload.UploadError += new UploadErrorEventHandler(upload_UploadError);
                upload.FaceDetectionFail += new FaceDetectionFailEventHandler(upload_FaceDetectionFail);
				upload.Upload();
			}
			catch (Exception ex)
			{
				Log log = new Log();
				log.LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log\Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
				LogEntry entry = new LogEntry();
				entry.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
				entry.Items.Add(new LogEntryItem("Error", ex.ToString()));
				log.Entries.Add(entry);
                log.WriteFlatLog();
                log.Entries.Clear();
			}
		}

        static void upload_FaceDetectionFail(object sender, FaceDetectionFailStatusEventArgs e)
        {
            MyConfig config = new MyConfig();
            EmailBuilder email = new EmailBuilder();

            try
            {
                if (config.FaceDetectFailedEmail)
                {
                    email.SMTPServer = config.SMTPServer;
                    email.SMTPPort = config.SMTPPort;
                    email.SMTPUserName = config.SMTPUserName;
                    try
                    {
                        email.SMTPPassword = StringCipher.Decrypt(config.SMTPPassword);
                    }
                    catch
                    {
                        email.SMTPPassword = "";
                    }
                    email.MailFrom = config.MailFrom;
                    foreach (String to in config.MailTo.Split(';'))
                    {
                        email.MailTo.Add(to.Trim());
                    }
                    email.MailSubject = config.MailSubject;
                    email.MailBody = String.Format(config.MailBody, e.CurrentUploadUser, e.CurrentUploadFile.Replace(config.PhotoPath.TrimEnd(new Char[] { '\\', '/' }), config.NoFacePath.TrimEnd(new Char[] { '\\', '/' })), DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"));
                    email.HTMLBody = true;
                    email.Send();
                }
            }
            catch (Exception ex)
            {
                Log log = new Log();
                log.LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log\Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                LogEntry entry = new LogEntry();
                entry.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
                entry.Items.Add(new LogEntryItem("Error", ex.ToString()));
                log.Entries.Add(entry);
                log.WriteFlatLog();
                log.Entries.Clear();
            }
        }

		static void upload_UploadError(object sender, UploadErrorStatusEventArgs e)
		{
			Log log = new Log();
			log.LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log\Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
			LogEntry entry = new LogEntry();
			entry.Items.Add(new LogEntryItem("Time", DateTime.Now.ToString()));
			entry.Items.Add(new LogEntryItem("Error", e.UploadError.ToString()));
			log.Entries.Add(entry);
            log.WriteFlatLog();
            log.Entries.Clear();
		}
	}
}
