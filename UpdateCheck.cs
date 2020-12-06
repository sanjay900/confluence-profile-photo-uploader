using System;
using System.Reflection;
using System.Net;

namespace VanClinic.Utilities.ConfluenceProfilePhotos
{
    class UpdateCheck
    {
        MyConfig config = new MyConfig();

        public Boolean UpToDate(out Version ThisVersion, out Version CurrentVersion)
        {
            Assembly assem = Assembly.GetEntryAssembly();
            AssemblyName assemName = assem.GetName();
            CurrentVersion = ThisVersion = assemName.Version;
            return true;
            //Boolean upToDate = false;
            //Assembly assem = Assembly.GetEntryAssembly();
            //AssemblyName assemName = assem.GetName();
            //WebClient client = new WebClient();
            //WebProxy proxy = null;
            //String currentVersionString = String.Empty;

            //ThisVersion = assemName.Version;

            //try
            //{
            //    if (config.UseProxy && config.ProxyAddress != "" && config.ProxyPort != "")
            //    {
            //        proxy = new WebProxy(config.ProxyAddress, Convert.ToInt32(config.ProxyPort));
            //        if (config.ProxyUsername != "" && config.ProxyPassword != "")
            //        {
            //            try
            //            {
            //                proxy.Credentials = new NetworkCredential(config.ProxyUsername, StringCipher.Decrypt(config.ProxyPassword));
            //            }
            //            catch
            //            {
            //                throw;
            //            }
            //        }
            //        client.Proxy = proxy;
            //    }

            //    currentVersionString = client.DownloadString(config.VersionURL);

            //    CurrentVersion = new Version(currentVersionString);

            //    upToDate = ThisVersion.CompareTo(CurrentVersion) >= 0 ? true : false;
            //}
            //catch
            //{
            //    throw;
            //}
            //finally
            //{
            //    client.Dispose();
            //}

            //return upToDate;
        }
    }
}
