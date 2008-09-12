using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Netcode.Scan
{
    public partial class FormDetails : Form
    {
        public FormDetails()
        {
            InitializeComponent();
            
        }

        public FormDetails(string[] tag)
        {
            
            InitializeComponent();
            toolStripButtonGo.Click += new EventHandler(toolStripButtonGo_Click);
            toolStripTextBoxSign.Text = tag[0];
            if (!File.Exists(tag[1]))
            {
                propertyGridFS.Enabled = false;
                propertyGridExe.Enabled = false;
                this.Text += " (файл не найден)";
            }
            else
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(tag[1]);
                FileInfo fi = new FileInfo(tag[1]);
                propertyGridFS.SelectedObject = fi;
                propertyGridExe.SelectedObject = fvi;
            }
        }

        void toolStripButtonGo_Click(object sender, EventArgs e)
        {
            webBrowserGo.Navigate("http://127.0.0.1/?" + toolStripTextBoxSign.Text);
        }
    }


}