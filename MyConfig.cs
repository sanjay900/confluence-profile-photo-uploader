using System;
using System.Configuration;
using System.Linq;

namespace VanClinic.Utilities.ConfluenceProfilePhotos
{
	class MyConfig
	{
		private String _defaultWikiURL = "http://yourwikiservername";
		private String _defaultSoapPath = "/rpc/soap-axis/confluenceservice-v2?wsdl";
		private String _defaultUsername = "admin";
        private String _defaultVersionURL = "https://bitbucket.org/fredclown/confluence-profile-photo-uploader/raw/tip/Version.txt";
        private String _defaultLastUpdateEmailVersion = "0.0.0.0";
        private String _defaultMailSubject = "Confluence Profile Photo Uploader Face Detection Failed";
        private String _defaultMailBody = @"The Confluence Profile Photo Uploader was unable to detect a face for <b>{0}</b> in the image {1}. This image will need to be manually cropped and uploaded.<br /><br />\r\nSent time: {2}";
		
		public String WikiURL
		{
			get
			{
				return GetKey("wikiURL");
			}
			set
			{
				SettingCheck("wikiURL");
				if (value == "")
				{
					value = _defaultWikiURL;
				}
				SetKey("wikiURL", value);
			}
		}
		public String SoapPath
		{
			get
			{
				return GetKey("soapPath");
			}
			set
			{
				SettingCheck("soapPath");
				if (value == "")
				{
					value = _defaultSoapPath;
				}
				SetKey("soapPath", value);
			}
		}
		public String Username
		{
			get
			{
				return GetKey("username");
			}
			set
			{
				SettingCheck("username");
				SetKey("username", value);
			}
		}
		public String Password
		{
			get
			{
				return GetKey("password");
			}
			set
			{
				SettingCheck("password");
				SetKey("password", value);
			}
		}
		public String PhotoPath
		{
			get
			{
				return GetKey("photoPath");
			}
			set
			{
				SettingCheck("photoPath");
				SetKey("photoPath", value);
			}
		}
		public Boolean Archive
		{
			get
			{
				Boolean val = false;

				val = GetKey("archive") == "true" ? true : false;
				
				return val;
			}
			set
			{
				String val = value ? "true" : "false";
				
				SettingCheck("archive");
				SetKey("archive", val);
			}
		}
		public String ArchivePath
		{
			get
			{
				return GetKey("archivePath");
			}
			set
			{
				SettingCheck("archivePath");
				SetKey("archivePath", value);
			}
		}
        public String NoFacePath
        {
            get
            {
                return GetKey("noFacePath");
            }
            set
            {
                SettingCheck("noFacePath");
                SetKey("noFacePath", value);
            }
        }
		public Boolean Delete
		{
			get
			{
				Boolean val = false;

				val = GetKey("delete") == "true" ? true : false;
				
				return val;
			}
			set
			{
				String val = value ? "true" : "false";
				
				SettingCheck("delete");
				SetKey("delete", val);
			}
		}
		public Boolean NoCrop
		{
			get
			{
				Boolean val = false;

				val = GetKey("noCrop") == "true" ? true : false;

				return val;
			}
			set
			{
				String val = value ? "true" : "false";

				SettingCheck("noCrop");
				SetKey("noCrop", val);
			}
		}
        public Boolean PreCropped
        {
            get
            {
                Boolean val = false;

                val = GetKey("preCropped") == "true" ? true : false;

                return val;
            }
            set
            {
                String val = value ? "true" : "false";

                SettingCheck("preCropped");
                SetKey("preCropped", val);
            }
        }
        public Boolean UseProxy
        {
            get
            {
                Boolean val = false;

                val = GetKey("useProxy") == "true" ? true : false;

                return val;
            }
            set
            {
                String val = value ? "true" : "false";

                SettingCheck("useProxy");
                SetKey("useProxy", val);
            }
        }
        public String ProxyAddress
        {
            get
            {
                return GetKey("proxyAddress");
            }
            set
            {
                SettingCheck("proxyAddress");
                SetKey("proxyAddress", value);
            }
        }
        public String ProxyPort
        {
            get
            {
                return GetKey("proxyPort");
            }
            set
            {
                SettingCheck("proxyPort");
                SetKey("proxyPort", value);
            }
        }
        public String ProxyUsername
        {
            get
            {
                return GetKey("proxyUsername");
            }
            set
            {
                SettingCheck("proxyUsername");
                SetKey("proxyUsername", value);
            }
        }
        public String ProxyPassword
        {
            get
            {
                return GetKey("proxyPassword");
            }
            set
            {
                SettingCheck("proxyPassword");
                SetKey("proxyPassword", value);
            }
        }
        public String VersionURL
        {
            get
            {
                return GetKey("versionURL");
            }
        }
        public Boolean UpdateEmail
        {
            get
            {
                Boolean val = false;

                val = GetKey("updateEmail") == "true" ? true : false;

                return val;
            }
            set
            {
                String val = value ? "true" : "false";

                SettingCheck("updateEmail");
                SetKey("updateEmail", val);
            }
        }
        public String LastUpdateEmailVersion
        {
            get
            {
                return GetKey("lastUpdateEmailVersion");
            }
            set
            {
                SettingCheck("lastUpdateEmailVersion");
                SetKey("lastUpdateEmailVersion", value);
            }
        }
        public Boolean FaceDetectFailedEmail
        {
            get
            {
                Boolean val = false;

                val = GetKey("faceDetectFailedEmail") == "true" ? true : false;

                return val;
            }
            set
            {
                String val = value ? "true" : "false";

                SettingCheck("faceDetectFailedEmail");
                SetKey("faceDetectFailedEmail", val);
            }
        }
        public String SMTPServer
        {
            get
            {
                return GetKey("smtpServer");
            }
            set
            {
                SettingCheck("smtpServer");
                SetKey("smtpServer", value);
            }
        }
        public Int32 SMTPPort
        {
            get
            {
                try
                {
                    return Convert.ToInt32(GetKey("smtpPort"));
                }
                catch
                {
                    return 25;
                }
            }
            set
            {
                SettingCheck("smtpPort");
                SetKey("smtpPort", value.ToString());
            }
        }
        public String SMTPUserName
        {
            get
            {
                return GetKey("smtpUserName");
            }
            set
            {
                SettingCheck("smtpUserName");
                SetKey("smtpUserName", value);
            }
        }
        public String SMTPPassword
        {
            get
            {
                return GetKey("smtpPassword");
            }
            set
            {
                SettingCheck("smtpPassword");
                SetKey("smtpPassword", value);
            }
        }
        public String MailFrom
        {
            get
            {
                return GetKey("mailFrom");
            }
            set
            {
                SettingCheck("mailFrom");
                SetKey("mailFrom", value);
            }
        }
        public String MailTo
        {
            get
            {
                return GetKey("mailTo");
            }
            set
            {
                SettingCheck("mailTo");
                SetKey("mailTo", value);
            }
        }
        public String MailSubject
        {
            get
            {
                return GetKey("mailSubject");
            }
            set
            {
                SettingCheck("mailSubject");
                SetKey("mailSubject", value);
            }
        }
        public String MailBody
        {
            get
            {
                return GetKey("mailBody");
            }
            set
            {
                SettingCheck("mailBody");
                SetKey("mailBody", value);
            }
        }

		public MyConfig()
		{
		}

		public void SettingsCheck()
		{
            String[] settings = { "wikiURL", "soapPath", "username", "password", "photoPath", "archive", "archivePath", "noFacePath", "delete", "noCrop", "preCropped", "proxyAddress", "proxyPort", "proxyUsername", "proxyPassword", "versionURL", "updateEmail", "lastUpdateEmailVersion", "faceDetectFailedEmail", "smtpServer", "smtpPort", "smtpUserName", "smtpPassword", "mailFrom", "mailTo", "mailSubject", "mailBody" };

			foreach (String setting in settings)
			{
				SettingCheck(setting);
			}
		}

		private void SettingCheck(String Key)
		{
			Configuration config;
			String _default = String.Empty;

			// Open App.Config of executable
			config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			//Just in case it was changed outside of the program
			ConfigurationManager.RefreshSection("appSettings");
			
			if (!ConfigurationManager.AppSettings.AllKeys.Contains(Key))
			{
				switch (Key)
				{
					case "wikiURL":
						_default = _defaultWikiURL;
						break;
					case "soapPath":
						_default = _defaultSoapPath;
						break;
					case "username":
						_default = _defaultUsername;
						break;
                    case "versionURL":
                        _default = _defaultVersionURL;
                        break;
                    case "lastUpdateEmailVersion":
                        _default = _defaultLastUpdateEmailVersion;
                        break;
                    case "mailSubject":
                        _default = _defaultMailSubject;
                        break;
                    case "mailBody":
                        _default = _defaultMailBody;
                        break;
					default:
						_default = "";
						break;
				}

				// Add an Application Setting.
				config.AppSettings.Settings.Add(Key, _default);
			}
			
			// Save the configuration file.
			config.Save(ConfigurationSaveMode.Modified);

			// Force a reload of a changed section.
			ConfigurationManager.RefreshSection("appSettings");
		}

		private String GetKey(String Key)
		{
			SettingCheck(Key);

			return ConfigurationManager.AppSettings[Key].ToString();
		}

		private void SetKey(String Key, String Value)
		{
			Configuration config;
			
			// Open App.Config of executable
			config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			
			// Add an Application Setting.
			config.AppSettings.Settings.Remove(Key);
			config.AppSettings.Settings.Add(Key, Value);
			
			// Save the configuration file.
			config.Save(ConfigurationSaveMode.Modified);
			
			// Force a reload of a changed section.
			ConfigurationManager.RefreshSection("appSettings");
		}
	}
}
