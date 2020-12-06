using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace VanClinic.Libraries.EmailBuilder
{
    public class EmailBuilder
    {
        private String _SMTPServer = String.Empty;
        private Int32 _SMTPPort = 0;
        private String _SMTPUserName = String.Empty;
        private String _SMTPPassword = String.Empty;
        private String _MailFrom = String.Empty;
        private List<String> _MailTo = new List<String>();
        private List<String> _MailCC = new List<String>();
        private List<String> _MailBCC = new List<String>();
        private String _MailSubject = String.Empty;
        private String _MailBody = String.Empty;
        private Boolean _MailEnableSSL = false;
        private Boolean _HTMLBody = false;

        public String SMTPServer
        {
            get { return _SMTPServer; }
            set { _SMTPServer = value; }
        }
        public Int32 SMTPPort
        {
            get { return _SMTPPort; }
            set { _SMTPPort = value; }
        }
        public String SMTPUserName
        {
            get { return _SMTPUserName; }
            set { _SMTPUserName = value; }
        }
        public String SMTPPassword
        {
            get { return _SMTPPassword; }
            set { _SMTPPassword = value; }
        }
        public String MailFrom
        {
            get { return _MailFrom; }
            set { _MailFrom = value; }
        }
        public List<String> MailTo
        {
            get { return _MailTo; }
            set { _MailTo = value; }
        }
        public List<String> MailCC
        {
            get { return _MailCC; }
            set { _MailCC = value; }
        }
        public List<String> MailBCC
        {
            get { return _MailBCC; }
            set { _MailBCC = value; }
        }
        public String MailSubject
        {
            get { return _MailSubject; }
            set { _MailSubject = value; }
        }
        public String MailBody
        {
            get { return _MailBody; }
            set { _MailBody = value; }
        }
        public Boolean MailEnableSSL
        {
            get { return _MailEnableSSL; }
            set { _MailEnableSSL = value; }
        }
        public Boolean HTMLBody
        {
            get { return _HTMLBody; }
            set { _HTMLBody = value; }
        }

        public void Send()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(_SMTPServer);

                mail.From = new MailAddress(_MailFrom);
                foreach (String To in _MailTo)
                {
                    mail.To.Add(To);
                }
                foreach (String CC in _MailCC)
                {
                    mail.CC.Add(CC);
                }
                foreach (String BCC in _MailBCC)
                {
                    mail.Bcc.Add(BCC);
                }
                mail.Subject = _MailSubject;
                mail.Body = _MailBody;
                mail.IsBodyHtml = _HTMLBody;

                if (_SMTPPort != 0)
                {
                    SmtpServer.Port = _SMTPPort;
                }
                if (_SMTPUserName != String.Empty && _SMTPPassword != String.Empty)
                {
                    SmtpServer.Credentials = new System.Net.NetworkCredential(_SMTPUserName, _SMTPPassword);
                }
                SmtpServer.EnableSsl = _MailEnableSSL;

                SmtpServer.Send(mail);
            }
            catch
            {
                throw;
            }
        }
    }
}