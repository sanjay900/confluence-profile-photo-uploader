using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace VanClinic.Utilities.ConfluenceProfilePhotos
{
	public partial class Help : Form
	{
        MyConfig config = new MyConfig();

        public Help()
		{
			InitializeComponent();
		}

        private void Help_Load(object sender, EventArgs e)
        {
            Assembly assem = Assembly.GetEntryAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;

            lblVersion.Text = ver.ToString();
        }       
	}
}
