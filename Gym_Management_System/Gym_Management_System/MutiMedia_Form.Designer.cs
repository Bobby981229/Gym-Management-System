namespace Gym_Management_System
{
    partial class MutiMedia_Form
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MutiMedia_Form));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripExit = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tSBtn_play = new System.Windows.Forms.ToolStripButton();
            this.tSBtn_stop = new System.Windows.Forms.ToolStripButton();
            this.tSBtn_openfile = new System.Windows.Forms.ToolStripButton();
            this.tSB_backward = new System.Windows.Forms.ToolStripButton();
            this.tSB_forward = new System.Windows.Forms.ToolStripButton();
            this.lblVolume = new System.Windows.Forms.ToolStripLabel();
            this.tbVideoTime = new System.Windows.Forms.ToolStripTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trackBarSpace = new System.Windows.Forms.TrackBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMediaName = new System.Windows.Forms.Label();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.scrollbarComponent1 = new HZH_Controls.Controls.ScrollbarComponent(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblRollTitle = new HZH_Controls.Controls.UCRollText();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.radioBut1 = new System.Windows.Forms.RadioButton();
            this.radioBut2 = new System.Windows.Forms.RadioButton();
            this.txtWebsite = new System.Windows.Forms.TextBox();
            this.btnPaly = new CCWin.SkinControl.SkinButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpace)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(903, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripFile
            // 
            this.ToolStripFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripOpen,
            this.toolStripSeparator1,
            this.ToolStripExit});
            this.ToolStripFile.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.ToolStripFile.Name = "ToolStripFile";
            this.ToolStripFile.Size = new System.Drawing.Size(54, 26);
            this.ToolStripFile.Text = "File";
            this.ToolStripFile.Click += new System.EventHandler(this.ToolStripFile_Click);
            // 
            // ToolStripOpen
            // 
            this.ToolStripOpen.Name = "ToolStripOpen";
            this.ToolStripOpen.Size = new System.Drawing.Size(128, 26);
            this.ToolStripOpen.Text = "Open";
            this.ToolStripOpen.Click += new System.EventHandler(this.ToolStripOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(125, 6);
            // 
            // ToolStripExit
            // 
            this.ToolStripExit.Name = "ToolStripExit";
            this.ToolStripExit.Size = new System.Drawing.Size(128, 26);
            this.ToolStripExit.Text = "Exit";
            this.ToolStripExit.Click += new System.EventHandler(this.ToolStripExit_Click_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSBtn_play,
            this.tSBtn_stop,
            this.tSBtn_openfile,
            this.tSB_backward,
            this.tSB_forward,
            this.lblVolume,
            this.tbVideoTime});
            this.toolStrip1.Location = new System.Drawing.Point(0, 471);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(903, 30);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "Volume: ";
            // 
            // tSBtn_play
            // 
            this.tSBtn_play.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBtn_play.Name = "tSBtn_play";
            this.tSBtn_play.Size = new System.Drawing.Size(49, 27);
            this.tSBtn_play.Text = "Play";
            this.tSBtn_play.Click += new System.EventHandler(this.tSBtn_play_Click);
            // 
            // tSBtn_stop
            // 
            this.tSBtn_stop.Image = global::Gym_Management_System.Properties.Resources.Stop;
            this.tSBtn_stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBtn_stop.Name = "tSBtn_stop";
            this.tSBtn_stop.Size = new System.Drawing.Size(70, 27);
            this.tSBtn_stop.Text = "Stop";
            this.tSBtn_stop.Click += new System.EventHandler(this.tSBtn_stop_Click);
            // 
            // tSBtn_openfile
            // 
            this.tSBtn_openfile.Image = global::Gym_Management_System.Properties.Resources.Open;
            this.tSBtn_openfile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBtn_openfile.Name = "tSBtn_openfile";
            this.tSBtn_openfile.Size = new System.Drawing.Size(76, 27);
            this.tSBtn_openfile.Text = "Open";
            this.tSBtn_openfile.Click += new System.EventHandler(this.tSBtn_openfile_Click);
            // 
            // tSB_backward
            // 
            this.tSB_backward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_backward.Image = global::Gym_Management_System.Properties.Resources.Fast_reverse;
            this.tSB_backward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_backward.Name = "tSB_backward";
            this.tSB_backward.Size = new System.Drawing.Size(24, 27);
            this.tSB_backward.Text = "toolStripButton1";
            this.tSB_backward.ToolTipText = "Bckward";
            this.tSB_backward.Click += new System.EventHandler(this.tSB_backward_Click);
            // 
            // tSB_forward
            // 
            this.tSB_forward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSB_forward.Image = global::Gym_Management_System.Properties.Resources.Fast_forward;
            this.tSB_forward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSB_forward.Name = "tSB_forward";
            this.tSB_forward.Size = new System.Drawing.Size(24, 27);
            this.tSB_forward.Text = "toolStripButton2";
            this.tSB_forward.ToolTipText = "Forward";
            this.tSB_forward.Click += new System.EventHandler(this.tSB_forward_Click);
            // 
            // lblVolume
            // 
            this.lblVolume.BackColor = System.Drawing.Color.Transparent;
            this.lblVolume.ForeColor = System.Drawing.Color.Crimson;
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(99, 27);
            this.lblVolume.Text = "Volume: 50";
            // 
            // tbVideoTime
            // 
            this.tbVideoTime.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbVideoTime.BackColor = System.Drawing.Color.White;
            this.tbVideoTime.Enabled = false;
            this.tbVideoTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVideoTime.ForeColor = System.Drawing.Color.Black;
            this.tbVideoTime.Name = "tbVideoTime";
            this.tbVideoTime.ReadOnly = true;
            this.tbVideoTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbVideoTime.Size = new System.Drawing.Size(165, 30);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // trackBarSpace
            // 
            this.trackBarSpace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarSpace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBarSpace.Location = new System.Drawing.Point(0, 442);
            this.trackBarSpace.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarSpace.Name = "trackBarSpace";
            this.trackBarSpace.Size = new System.Drawing.Size(911, 56);
            this.trackBarSpace.TabIndex = 5;
            this.trackBarSpace.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(148, 28);
            // 
            // 打开ToolStripMenuItem1
            // 
            this.打开ToolStripMenuItem1.Name = "打开ToolStripMenuItem1";
            this.打开ToolStripMenuItem1.Size = new System.Drawing.Size(147, 24);
            this.打开ToolStripMenuItem1.Text = "Open File";
            this.打开ToolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItemOpen_Click);
            // 
            // lblMediaName
            // 
            this.lblMediaName.AutoSize = true;
            this.lblMediaName.BackColor = System.Drawing.Color.White;
            this.lblMediaName.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMediaName.ForeColor = System.Drawing.Color.DeepPink;
            this.lblMediaName.Location = new System.Drawing.Point(806, 1);
            this.lblMediaName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMediaName.Name = "lblMediaName";
            this.lblMediaName.Size = new System.Drawing.Size(74, 26);
            this.lblMediaName.TabIndex = 7;
            this.lblMediaName.Text = "label1";
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarVolume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBarVolume.LargeChange = 1;
            this.trackBarVolume.Location = new System.Drawing.Point(318, 473);
            this.trackBarVolume.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(401, 56);
            this.trackBarVolume.TabIndex = 8;
            this.trackBarVolume.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.ContextMenuStrip = this.contextMenuStrip1;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(903, 419);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.ContextMenuStrip = this.contextMenuStrip1;
            this.panel2.Controls.Add(this.lblRollTitle);
            this.panel2.Controls.Add(this.picLogo);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(903, 419);
            this.panel2.TabIndex = 0;
            this.panel2.DoubleClick += new System.EventHandler(this.panel2_DoubleClick);
            // 
            // lblRollTitle
            // 
            this.lblRollTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRollTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblRollTitle.Font = new System.Drawing.Font("Times New Roman", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRollTitle.ForeColor = System.Drawing.Color.White;
            this.lblRollTitle.Location = new System.Drawing.Point(12, 358);
            this.lblRollTitle.MoveSleepTime = 100;
            this.lblRollTitle.Name = "lblRollTitle";
            this.lblRollTitle.RollStyle = HZH_Controls.Controls.RollStyle.BackAndForth;
            this.lblRollTitle.Size = new System.Drawing.Size(538, 47);
            this.lblRollTitle.TabIndex = 71;
            this.lblRollTitle.Text = "Multi-Media Player";
            // 
            // picLogo
            // 
            this.picLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogo.Image = global::Gym_Management_System.Properties.Resources._333;
            this.picLogo.Location = new System.Drawing.Point(773, 3);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(127, 142);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 32;
            this.picLogo.TabStop = false;
            this.picLogo.Click += new System.EventHandler(this.picLogo_Click);
            // 
            // radioBut1
            // 
            this.radioBut1.AutoSize = true;
            this.radioBut1.BackColor = System.Drawing.Color.White;
            this.radioBut1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioBut1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.radioBut1.ForeColor = System.Drawing.Color.Black;
            this.radioBut1.Location = new System.Drawing.Point(89, 1);
            this.radioBut1.Name = "radioBut1";
            this.radioBut1.Size = new System.Drawing.Size(112, 26);
            this.radioBut1.TabIndex = 68;
            this.radioBut1.Text = "Address 1\r\n";
            this.radioBut1.UseVisualStyleBackColor = false;
            this.radioBut1.CheckedChanged += new System.EventHandler(this.radioBut1_CheckedChanged);
            // 
            // radioBut2
            // 
            this.radioBut2.AutoSize = true;
            this.radioBut2.BackColor = System.Drawing.Color.White;
            this.radioBut2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioBut2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioBut2.ForeColor = System.Drawing.Color.Black;
            this.radioBut2.Location = new System.Drawing.Point(207, 1);
            this.radioBut2.Name = "radioBut2";
            this.radioBut2.Size = new System.Drawing.Size(112, 26);
            this.radioBut2.TabIndex = 69;
            this.radioBut2.Text = "Address 2\r\n";
            this.radioBut2.UseVisualStyleBackColor = false;
            this.radioBut2.CheckedChanged += new System.EventHandler(this.radioBut2_CheckedChanged);
            // 
            // txtWebsite
            // 
            this.txtWebsite.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWebsite.Location = new System.Drawing.Point(332, 3);
            this.txtWebsite.Multiline = true;
            this.txtWebsite.Name = "txtWebsite";
            this.txtWebsite.Size = new System.Drawing.Size(395, 25);
            this.txtWebsite.TabIndex = 70;
            this.txtWebsite.TabStop = false;
            // 
            // btnPaly
            // 
            this.btnPaly.BackColor = System.Drawing.Color.Black;
            this.btnPaly.BaseColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnPaly.BorderColor = System.Drawing.Color.Black;
            this.btnPaly.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnPaly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPaly.DownBack = null;
            this.btnPaly.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaly.ForeColor = System.Drawing.Color.White;
            this.btnPaly.GlowColor = System.Drawing.Color.Red;
            this.btnPaly.Location = new System.Drawing.Point(733, 0);
            this.btnPaly.MouseBack = null;
            this.btnPaly.MouseBaseColor = System.Drawing.Color.Blue;
            this.btnPaly.Name = "btnPaly";
            this.btnPaly.NormlBack = null;
            this.btnPaly.Size = new System.Drawing.Size(65, 28);
            this.btnPaly.TabIndex = 65;
            this.btnPaly.Text = "Play";
            this.btnPaly.UseVisualStyleBackColor = false;
            this.btnPaly.Click += new System.EventHandler(this.btnPaly_Click);
            // 
            // MutiMedia_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 501);
            this.Controls.Add(this.btnPaly);
            this.Controls.Add(this.txtWebsite);
            this.Controls.Add(this.lblMediaName);
            this.Controls.Add(this.radioBut2);
            this.Controls.Add(this.trackBarVolume);
            this.Controls.Add(this.radioBut1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.trackBarSpace);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MutiMedia_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Muti-Media";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MutiMedia_Form_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpace)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripButton tSBtn_play;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar trackBarSpace;
        private System.Windows.Forms.ToolStripButton tSBtn_stop;
        private System.Windows.Forms.Label lblMediaName;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.ToolStripButton tSB_backward;
        private System.Windows.Forms.ToolStripButton tSB_forward;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem1;
        private HZH_Controls.Controls.ScrollbarComponent scrollbarComponent1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel lblVolume;
        private System.Windows.Forms.ToolStripMenuItem ToolStripFile;
        private System.Windows.Forms.ToolStripMenuItem ToolStripOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripExit;
        public System.Windows.Forms.ToolStripButton tSBtn_openfile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioBut1;
        private System.Windows.Forms.RadioButton radioBut2;
        private System.Windows.Forms.TextBox txtWebsite;
        private CCWin.SkinControl.SkinButton btnPaly;
        private HZH_Controls.Controls.UCRollText lblRollTitle;
        public System.Windows.Forms.ToolStripTextBox tbVideoTime;
    }
}

