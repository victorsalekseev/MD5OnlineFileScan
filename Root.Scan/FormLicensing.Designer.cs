namespace Root.Scan
{
    partial class FormLicensing
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelProductName = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.panel = new System.Windows.Forms.Panel();
            this.button_close = new System.Windows.Forms.Button();
            this.Button_ok = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelProductName
            // 
            this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductName.Location = new System.Drawing.Point(9, 9);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(491, 17);
            this.labelProductName.TabIndex = 20;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.BackColor = System.Drawing.Color.White;
            this.textBoxDescription.Location = new System.Drawing.Point(8, 32);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDescription.Size = new System.Drawing.Size(491, 215);
            this.textBoxDescription.TabIndex = 26;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Text = "Description";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.button_close);
            this.panel.Controls.Add(this.Button_ok);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Location = new System.Drawing.Point(9, 252);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(491, 29);
            this.panel.TabIndex = 27;
            // 
            // button_close
            // 
            this.button_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.button_close.Location = new System.Drawing.Point(284, 0);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(207, 29);
            this.button_close.TabIndex = 27;
            this.button_close.Text = "Не согласен (закрыть программу)";
            // 
            // Button_ok
            // 
            this.Button_ok.Dock = System.Windows.Forms.DockStyle.Left;
            this.Button_ok.Location = new System.Drawing.Point(0, 0);
            this.Button_ok.Name = "Button_ok";
            this.Button_ok.Size = new System.Drawing.Size(75, 29);
            this.Button_ok.TabIndex = 26;
            this.Button_ok.Text = "Согласен";
            // 
            // FormLicensing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 290);
            this.ControlBox = false;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelProductName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLicensing";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Лицензионное соглашение";
            this.TopMost = true;
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Button Button_ok;
    }
}
