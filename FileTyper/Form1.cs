using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using MySql.Data.MySqlClient;
using Netcode.Common;
using System.IO;

namespace FileTyper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(@"C:\Server\Apache2.2\modules\mod_dir.so");
            //FileInfo fi = new FileInfo(@"C:\Server\Apache2.2\modules\mod_dir.so");
            
            ExFileInfo efi = new ExFileInfo();
            Hashtable ht_fi = efi.LoadFInfo("00000000000000000000000000000000", @"C:\Server\Apache2.2\modules\mod_dir.so", false, DateTime.Now);

            //string ff = efi.GetInsertSqlQuery(ht_fi);


            MySql.Data.MySqlClient.MyConnect my = new MyConnect("127.0.0.1", "root", "adminadmin");

            Stopwatch c = new Stopwatch();
            c.Start();
            //for (int i = 0; i < 1000; i++)
            {
                //int r = 4;
                //r += r + 1;
                my.Insert(ht_fi, "aver", "fileinfo");
            }
            
            c.Stop();
            
            MessageBox.Show(c.ElapsedMilliseconds.ToString());
        }
    }
}