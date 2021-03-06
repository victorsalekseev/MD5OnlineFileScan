﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Root.Preferences;
using System.IO;
using System.Text;

namespace Root.Scan
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 4)
            {
                if (args[0] == "online")
                {
                    DB.use_db = true;
                    if (!string.IsNullOrEmpty(args[1]))
                    {
                        DB.host = args[1];
                    }
                    
                    if (!string.IsNullOrEmpty(args[2]))
                    {
                        DB.db_name = args[2];
                    }

                    if (!string.IsNullOrEmpty(args[3]))
                    {
                        DB.user = args[3];
                    }

                    if (!string.IsNullOrEmpty(args[4]))
                    {
                        DB.password = args[4];
                    }

                    MessageBox.Show("Внимание! Программа запущена с параметром"+Environment.NewLine+
                        "автоматического добавления сигнатур"+Environment.NewLine+"и данных файла в базу данных." + Environment.NewLine +
                        Environment.NewLine +
                        "Host: " + DB.host + Environment.NewLine + 
                        "DB Name: " + DB.db_name + Environment.NewLine + 
                        "User: " + DB.user + Environment.NewLine + 
                        "Password: ***");
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new splash());
        }
    }

    public class splash : Form
    {
        public splash()
        {
            this.Shown += new EventHandler(splash_Shown);
            this.Size = new Size(218, 57);
            TopMost = true;
            Opacity = 100;
            StartPosition = FormStartPosition.CenterScreen;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;

            PictureBox pb = new PictureBox();
            pb.Dock = DockStyle.Fill;
            pb.Image = global::Root.Scan.Properties.Resources.splash;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Controls.Add(pb);
        }

        void splash_Shown(object sender, EventArgs e)
        {
            if (!File.Exists(Licensing.license_rus))
            {
                FormLicensing frm_l = new FormLicensing();
                DialogResult dr = frm_l.ShowDialog();

                switch (dr)
                {
                    case DialogResult.Abort:
                        {
                            Application.Exit();
                        }
                        break;
                    case DialogResult.Yes:
                        {
                            ShowMainForm();
                            using (StreamWriter sw = new StreamWriter(Licensing.license_rus, false, Encoding.UTF8))
                            {
                                sw.Write(frm_l.AssemblyDescription);
                                sw.Close();
                            }
                        }
                        break;
                    default:
                        break;
                }

                frm_l.Dispose();
            }
            else
            {
                ShowMainForm();
            }
        }

        private void ShowMainForm()
        {
            FormMain fm = new FormMain();
            fm.Show();
            this.Hide();
        }

    }
}