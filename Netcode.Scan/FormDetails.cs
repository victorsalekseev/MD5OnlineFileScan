using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Netcode.Preferences;

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
            toolStripTextBoxSign.KeyDown += new KeyEventHandler(toolStripTextBoxSign_KeyDown);
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

        void toolStripTextBoxSign_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(toolStripTextBoxSign.Text))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    toolStripButtonGo_Click(this, null);
                }
            }
        }

        void toolStripButtonGo_Click(object sender, EventArgs e)
        {
            webBrowserGo.Navigate(OnlineServices.server_url_hash_details + toolStripTextBoxSign.Text + "&rand=" + new Random(123).Next(1, 10000).ToString());
        }
    }


}