using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace VanClinic.Utilities.ConfluenceProfilePhotos
{
    class PreReqCheck
    {
        public static Boolean HasVCP11()
        {
            Boolean is64Bit = Is64Bit();

            return is64Bit ? File.Exists(Environment.ExpandEnvironmentVariables(@"%windir%\SysWOW64\msvcp110.dll")) : File.Exists(Environment.ExpandEnvironmentVariables(@"%windir%\system32\msvcp110.dll"));

        }

        private static Boolean Is64Bit()
        {
            return (Environment.GetEnvironmentVariable("ProgramFiles(x86)") != null);
        }
    }
}
