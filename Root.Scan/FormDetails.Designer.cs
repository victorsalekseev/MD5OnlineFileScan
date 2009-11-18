namespace Root.Scan
{
    partial class FormDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDetails));
            this.tabControlDt = new System.Windows.Forms.TabControl();
            this.tabPageFS = new System.Windows.Forms.TabPage();
            this.propertyGridFS = new System.Windows.Forms.PropertyGrid();
            this.tabPageExe = new System.Windows.Forms.TabPage();
            this.propertyGridExe = new System.Windows.Forms.PropertyGrid();
            this.tabPageOnline = new System.Windows.Forms.TabPage();
            this.webBrowserGo = new System.Windows.Forms.WebBrowser();
            this.toolStripSrc = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelMd = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxSign = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonGo = new System.Windows.Forms.ToolStripButton();
            this.tabControlDt.SuspendLayout();
            this.tabPageFS.SuspendLayout();
            this.tabPageExe.SuspendLayout();
            this.tabPageOnline.SuspendLayout();
            this.toolStripSrc.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlDt
            // 
            this.tabControlDt.Controls.Add(this.tabPageFS);
            this.tabControlDt.Controls.Add(this.tabPageExe);
            this.tabControlDt.Controls.Add(this.tabPageOnline);
            this.tabControlDt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDt.Location = new System.Drawing.Point(0, 0);
            this.tabControlDt.Name = "tabControlDt";
            this.tabControlDt.SelectedIndex = 0;
            this.tabControlDt.Size = new System.Drawing.Size(444, 495);
            this.tabControlDt.TabIndex = 0;
            // 
            // tabPageFS
            // 
            this.tabPageFS.Controls.Add(this.propertyGridFS);
            this.tabPageFS.Location = new System.Drawing.Point(4, 22);
            this.tabPageFS.Name = "tabPageFS";
            this.tabPageFS.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFS.Size = new System.Drawing.Size(436, 469);
            this.tabPageFS.TabIndex = 0;
            this.tabPageFS.Text = "ФС";
            this.tabPageFS.UseVisualStyleBackColor = true;
            // 
            // propertyGridFS
            // 
            this.propertyGridFS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridFS.Location = new System.Drawing.Point(3, 3);
            this.propertyGridFS.Name = "propertyGridFS";
            this.propertyGridFS.Size = new System.Drawing.Size(430, 463);
            this.propertyGridFS.TabIndex = 0;
            // 
            // tabPageExe
            // 
            this.tabPageExe.Controls.Add(this.propertyGridExe);
            this.tabPageExe.Location = new System.Drawing.Point(4, 22);
            this.tabPageExe.Name = "tabPageExe";
            this.tabPageExe.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageExe.Size = new System.Drawing.Size(436, 469);
            this.tabPageExe.TabIndex = 2;
            this.tabPageExe.Text = "Версия";
            this.tabPageExe.UseVisualStyleBackColor = true;
            // 
            // propertyGridExe
            // 
            this.propertyGridExe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridExe.Location = new System.Drawing.Point(3, 3);
            this.propertyGridExe.Name = "propertyGridExe";
            this.propertyGridExe.Size = new System.Drawing.Size(430, 463);
            this.propertyGridExe.TabIndex = 0;
            // 
            // tabPageOnline
            // 
            this.tabPageOnline.Controls.Add(this.webBrowserGo);
            this.tabPageOnline.Controls.Add(this.toolStripSrc);
            this.tabPageOnline.Location = new System.Drawing.Point(4, 22);
            this.tabPageOnline.Name = "tabPageOnline";
            this.tabPageOnline.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOnline.Size = new System.Drawing.Size(436, 469);
            this.tabPageOnline.TabIndex = 1;
            this.tabPageOnline.Text = "Онлайн-поиск";
            this.tabPageOnline.UseVisualStyleBackColor = true;
            // 
            // webBrowserGo
            // 
            this.webBrowserGo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserGo.Location = new System.Drawing.Point(3, 28);
            this.webBrowserGo.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserGo.Name = "webBrowserGo";
            this.webBrowserGo.Size = new System.Drawing.Size(430, 438);
            this.webBrowserGo.TabIndex = 1;
            // 
            // toolStripSrc
            // 
            this.toolStripSrc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelMd,
            this.toolStripTextBoxSign,
            this.toolStripButtonGo});
            this.toolStripSrc.Location = new System.Drawing.Point(3, 3);
            this.toolStripSrc.Name = "toolStripSrc";
            this.toolStripSrc.Size = new System.Drawing.Size(430, 25);
            this.toolStripSrc.TabIndex = 0;
            this.toolStripSrc.Text = "toolStrip1";
            // 
            // toolStripLabelMd
            // 
            this.toolStripLabelMd.Name = "toolStripLabelMd";
            this.toolStripLabelMd.Size = new System.Drawing.Size(61, 22);
            this.toolStripLabelMd.Text = "Сигнатура";
            // 
            // toolStripTextBoxSign
            // 
            this.toolStripTextBoxSign.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.toolStripTextBoxSign.Font = new System.Drawing.Font("Courier New", 10F);
            this.toolStripTextBoxSign.Name = "toolStripTextBoxSign";
            this.toolStripTextBoxSign.Size = new System.Drawing.Size(300, 25);
            this.toolStripTextBoxSign.Text = "00000000000000000000000000000000";
            // 
            // toolStripButtonGo
            // 
            this.toolStripButtonGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonGo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGo.Image")));
            this.toolStripButtonGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGo.Name = "toolStripButtonGo";
            this.toolStripButtonGo.Size = new System.Drawing.Size(47, 22);
            this.toolStripButtonGo.Text = "Искать";
            // 
            // FormDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 495);
            this.Controls.Add(this.tabControlDt);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDetails";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Свойства файла";
            this.tabControlDt.ResumeLayout(false);
            this.tabPageFS.ResumeLayout(false);
            this.tabPageExe.ResumeLayout(false);
            this.tabPageOnline.ResumeLayout(false);
            this.tabPageOnline.PerformLayout();
            this.toolStripSrc.ResumeLayout(false);
            this.toolStripSrc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlDt;
        private System.Windows.Forms.TabPage tabPageFS;
        private System.Windows.Forms.PropertyGrid propertyGridFS;
        private System.Windows.Forms.TabPage tabPageOnline;
        private System.Windows.Forms.ToolStrip toolStripSrc;
        private System.Windows.Forms.ToolStripLabel toolStripLabelMd;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSign;
        private System.Windows.Forms.ToolStripButton toolStripButtonGo;
        private System.Windows.Forms.WebBrowser webBrowserGo;
        private System.Windows.Forms.TabPage tabPageExe;
        private System.Windows.Forms.PropertyGrid propertyGridExe;
    }
}