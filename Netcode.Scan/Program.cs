using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Netcode.Scan
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
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
            this.Size = new Size(300, 150);
            TopMost = true;
            Opacity = 100;
            StartPosition = FormStartPosition.CenterScreen;
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;

            PictureBox pb = new PictureBox();
            pb.Dock = DockStyle.Fill;
            pb.Image = global::Netcode.Scan.Properties.Resources.splash;
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Controls.Add(pb);
        }

        void splash_Shown(object sender, EventArgs e)
        {
            FormMain fm = new FormMain();
            fm.Show();
            this.Hide();
        }

    }
}