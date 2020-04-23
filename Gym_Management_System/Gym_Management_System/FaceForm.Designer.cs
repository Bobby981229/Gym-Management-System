namespace Gym_Management_System
{
    partial class FaceForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceForm));
            this.picImageCompare = new System.Windows.Forms.PictureBox();
            this.logBox = new System.Windows.Forms.TextBox();
            this.imageLists = new System.Windows.Forms.ImageList(this.components);
            this.listView = new System.Windows.Forms.ListView();
            this.videoDev = new AForge.Controls.VideoSourcePlayer();
            this.txtThreshold = new System.Windows.Forms.TextBox();
            this.irVideoSource = new AForge.Controls.VideoSourcePlayer();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.groBoxInfor = new System.Windows.Forms.GroupBox();
            this.lblConsultant = new System.Windows.Forms.Label();
            this.lblExTimes = new System.Windows.Forms.Label();
            this.txtIdentity = new System.Windows.Forms.TextBox();
            this.lblIdentity = new System.Windows.Forms.Label();
            this.picUserImage = new System.Windows.Forms.PictureBox();
            this.lblBirth = new System.Windows.Forms.Label();
            this.lblUserImage = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.lblDeadline = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblRegister = new System.Windows.Forms.Label();
            this.txtConsultant = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtTimes = new System.Windows.Forms.TextBox();
            this.txtDeadline = new System.Windows.Forms.TextBox();
            this.lblEamil = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.lblTel = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.txtBirth = new System.Windows.Forms.TextBox();
            this.txtRegister = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblCompareImage = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.lblImageList = new System.Windows.Forms.Label();
            this.picSearchNum = new System.Windows.Forms.PictureBox();
            this.cameraSwitch = new HZH_Controls.Controls.UCSwitch();
            this.btnChooseImg = new CCWin.SkinControl.SkinButton();
            this.btnClearImage = new CCWin.SkinControl.SkinButton();
            this.btnSelectImg = new CCWin.SkinControl.SkinButton();
            this.btnMatch = new CCWin.SkinControl.SkinButton();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.ucledTime1 = new HZH_Controls.Controls.UCLEDTime();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.picRefresh = new System.Windows.Forms.PictureBox();
            this.lblCompareInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picImageCompare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.groBoxInfor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh)).BeginInit();
            this.SuspendLayout();
            // 
            // picImageCompare
            // 
            this.picImageCompare.BackColor = System.Drawing.Color.White;
            this.picImageCompare.Location = new System.Drawing.Point(644, 259);
            this.picImageCompare.Margin = new System.Windows.Forms.Padding(4);
            this.picImageCompare.Name = "picImageCompare";
            this.picImageCompare.Size = new System.Drawing.Size(657, 482);
            this.picImageCompare.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picImageCompare.TabIndex = 1;
            this.picImageCompare.TabStop = false;
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.Color.Black;
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logBox.ForeColor = System.Drawing.Color.White;
            this.logBox.Location = new System.Drawing.Point(665, 816);
            this.logBox.Margin = new System.Windows.Forms.Padding(4);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(1148, 180);
            this.logBox.TabIndex = 31;
            // 
            // imageLists
            // 
            this.imageLists.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageLists.ImageStream")));
            this.imageLists.TransparentColor = System.Drawing.Color.Transparent;
            this.imageLists.Images.SetKeyName(0, "alai_152784032385984494.jpg");
            // 
            // listView
            // 
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.imageLists;
            this.listView.Location = new System.Drawing.Point(1319, 261);
            this.listView.Margin = new System.Windows.Forms.Padding(4);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(697, 480);
            this.listView.TabIndex = 33;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // videoDev
            // 
            this.videoDev.BackColor = System.Drawing.Color.White;
            this.videoDev.Location = new System.Drawing.Point(644, 259);
            this.videoDev.Margin = new System.Windows.Forms.Padding(4);
            this.videoDev.Name = "videoDev";
            this.videoDev.Size = new System.Drawing.Size(657, 482);
            this.videoDev.TabIndex = 38;
            this.videoDev.Text = "videoSource";
            this.videoDev.VideoSource = null;
            this.videoDev.PlayingFinished += new AForge.Video.PlayingFinishedEventHandler(this.videoSource_PlayingFinished);
            this.videoDev.Paint += new System.Windows.Forms.PaintEventHandler(this.videoSource_Paint);
            // 
            // txtThreshold
            // 
            this.txtThreshold.BackColor = System.Drawing.SystemColors.Window;
            this.txtThreshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtThreshold.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtThreshold.Location = new System.Drawing.Point(1734, 216);
            this.txtThreshold.Margin = new System.Windows.Forms.Padding(4);
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Size = new System.Drawing.Size(79, 29);
            this.txtThreshold.TabIndex = 40;
            this.txtThreshold.Text = "0.8";
            this.txtThreshold.Visible = false;
            this.txtThreshold.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtThreshold_KeyPress);
            this.txtThreshold.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtThreshold_KeyUp);
            // 
            // irVideoSource
            // 
            this.irVideoSource.BackColor = System.Drawing.Color.White;
            this.irVideoSource.Location = new System.Drawing.Point(1131, 259);
            this.irVideoSource.Margin = new System.Windows.Forms.Padding(4);
            this.irVideoSource.Name = "irVideoSource";
            this.irVideoSource.Size = new System.Drawing.Size(170, 154);
            this.irVideoSource.TabIndex = 38;
            this.irVideoSource.Text = "videoSource";
            this.irVideoSource.VideoSource = null;
            this.irVideoSource.Visible = false;
            this.irVideoSource.PlayingFinished += new AForge.Video.PlayingFinishedEventHandler(this.videoSource_PlayingFinished);
            this.irVideoSource.Paint += new System.Windows.Forms.PaintEventHandler(this.irVideoSource_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 42F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(682, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(686, 78);
            this.lblTitle.TabIndex = 89;
            this.lblTitle.Text = "Access Control System";
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogo.Image = global::Gym_Management_System.Properties.Resources._333;
            this.picLogo.Location = new System.Drawing.Point(1783, 14);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(184, 191);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 88;
            this.picLogo.TabStop = false;
            this.picLogo.Click += new System.EventHandler(this.picLogo_Click);
            // 
            // groBoxInfor
            // 
            this.groBoxInfor.BackColor = System.Drawing.Color.Black;
            this.groBoxInfor.Controls.Add(this.lblConsultant);
            this.groBoxInfor.Controls.Add(this.lblExTimes);
            this.groBoxInfor.Controls.Add(this.txtIdentity);
            this.groBoxInfor.Controls.Add(this.lblIdentity);
            this.groBoxInfor.Controls.Add(this.picUserImage);
            this.groBoxInfor.Controls.Add(this.lblBirth);
            this.groBoxInfor.Controls.Add(this.lblUserImage);
            this.groBoxInfor.Controls.Add(this.lblNumber);
            this.groBoxInfor.Controls.Add(this.txtAddress);
            this.groBoxInfor.Controls.Add(this.txtEmail);
            this.groBoxInfor.Controls.Add(this.txtTel);
            this.groBoxInfor.Controls.Add(this.lblDeadline);
            this.groBoxInfor.Controls.Add(this.lblName);
            this.groBoxInfor.Controls.Add(this.lblGender);
            this.groBoxInfor.Controls.Add(this.lblRegister);
            this.groBoxInfor.Controls.Add(this.txtConsultant);
            this.groBoxInfor.Controls.Add(this.lblAddress);
            this.groBoxInfor.Controls.Add(this.txtTimes);
            this.groBoxInfor.Controls.Add(this.txtDeadline);
            this.groBoxInfor.Controls.Add(this.lblEamil);
            this.groBoxInfor.Controls.Add(this.txtNumber);
            this.groBoxInfor.Controls.Add(this.lblTel);
            this.groBoxInfor.Controls.Add(this.txtName);
            this.groBoxInfor.Controls.Add(this.txtGender);
            this.groBoxInfor.Controls.Add(this.txtBirth);
            this.groBoxInfor.Controls.Add(this.txtRegister);
            this.groBoxInfor.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groBoxInfor.ForeColor = System.Drawing.Color.White;
            this.groBoxInfor.Location = new System.Drawing.Point(12, 95);
            this.groBoxInfor.Name = "groBoxInfor";
            this.groBoxInfor.Size = new System.Drawing.Size(606, 830);
            this.groBoxInfor.TabIndex = 87;
            this.groBoxInfor.TabStop = false;
            this.groBoxInfor.Text = "User Information";
            // 
            // lblConsultant
            // 
            this.lblConsultant.AutoSize = true;
            this.lblConsultant.BackColor = System.Drawing.Color.Black;
            this.lblConsultant.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.lblConsultant.ForeColor = System.Drawing.Color.White;
            this.lblConsultant.Location = new System.Drawing.Point(28, 739);
            this.lblConsultant.Name = "lblConsultant";
            this.lblConsultant.Size = new System.Drawing.Size(122, 27);
            this.lblConsultant.TabIndex = 83;
            this.lblConsultant.Text = "Consultant:";
            // 
            // lblExTimes
            // 
            this.lblExTimes.AutoSize = true;
            this.lblExTimes.BackColor = System.Drawing.Color.Black;
            this.lblExTimes.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.lblExTimes.ForeColor = System.Drawing.Color.White;
            this.lblExTimes.Location = new System.Drawing.Point(317, 739);
            this.lblExTimes.Name = "lblExTimes";
            this.lblExTimes.Size = new System.Drawing.Size(164, 27);
            this.lblExTimes.TabIndex = 83;
            this.lblExTimes.Text = "Exercise Times:";
            // 
            // txtIdentity
            // 
            this.txtIdentity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtIdentity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIdentity.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold);
            this.txtIdentity.ForeColor = System.Drawing.Color.Red;
            this.txtIdentity.Location = new System.Drawing.Point(317, 385);
            this.txtIdentity.Name = "txtIdentity";
            this.txtIdentity.ReadOnly = true;
            this.txtIdentity.Size = new System.Drawing.Size(147, 32);
            this.txtIdentity.TabIndex = 81;
            this.txtIdentity.TabStop = false;
            // 
            // lblIdentity
            // 
            this.lblIdentity.AutoSize = true;
            this.lblIdentity.BackColor = System.Drawing.Color.Black;
            this.lblIdentity.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.lblIdentity.ForeColor = System.Drawing.Color.White;
            this.lblIdentity.Location = new System.Drawing.Point(312, 338);
            this.lblIdentity.Name = "lblIdentity";
            this.lblIdentity.Size = new System.Drawing.Size(93, 27);
            this.lblIdentity.TabIndex = 80;
            this.lblIdentity.Text = "Identity:";
            // 
            // picUserImage
            // 
            this.picUserImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.picUserImage.Location = new System.Drawing.Point(257, 80);
            this.picUserImage.Name = "picUserImage";
            this.picUserImage.Size = new System.Drawing.Size(326, 240);
            this.picUserImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUserImage.TabIndex = 46;
            this.picUserImage.TabStop = false;
            // 
            // lblBirth
            // 
            this.lblBirth.AutoSize = true;
            this.lblBirth.BackColor = System.Drawing.Color.Black;
            this.lblBirth.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.lblBirth.ForeColor = System.Drawing.Color.White;
            this.lblBirth.Location = new System.Drawing.Point(28, 338);
            this.lblBirth.Name = "lblBirth";
            this.lblBirth.Size = new System.Drawing.Size(145, 27);
            this.lblBirth.TabIndex = 79;
            this.lblBirth.Text = "Date of Birth:";
            // 
            // lblUserImage
            // 
            this.lblUserImage.AutoSize = true;
            this.lblUserImage.BackColor = System.Drawing.Color.Black;
            this.lblUserImage.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserImage.ForeColor = System.Drawing.Color.White;
            this.lblUserImage.Location = new System.Drawing.Point(312, 41);
            this.lblUserImage.Name = "lblUserImage";
            this.lblUserImage.Size = new System.Drawing.Size(127, 27);
            this.lblUserImage.TabIndex = 59;
            this.lblUserImage.Text = "User Image:";
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.BackColor = System.Drawing.Color.Black;
            this.lblNumber.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumber.ForeColor = System.Drawing.Color.White;
            this.lblNumber.Location = new System.Drawing.Point(28, 41);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(146, 27);
            this.lblNumber.TabIndex = 59;
            this.lblNumber.Text = "User Number:";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold);
            this.txtAddress.ForeColor = System.Drawing.Color.White;
            this.txtAddress.Location = new System.Drawing.Point(130, 643);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ReadOnly = true;
            this.txtAddress.Size = new System.Drawing.Size(362, 73);
            this.txtAddress.TabIndex = 72;
            this.txtAddress.TabStop = false;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold);
            this.txtEmail.ForeColor = System.Drawing.Color.White;
            this.txtEmail.Location = new System.Drawing.Point(28, 583);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(267, 32);
            this.txtEmail.TabIndex = 72;
            this.txtEmail.TabStop = false;
            // 
            // txtTel
            // 
            this.txtTel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTel.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold);
            this.txtTel.ForeColor = System.Drawing.Color.White;
            this.txtTel.Location = new System.Drawing.Point(28, 484);
            this.txtTel.Name = "txtTel";
            this.txtTel.ReadOnly = true;
            this.txtTel.Size = new System.Drawing.Size(213, 32);
            this.txtTel.TabIndex = 72;
            this.txtTel.TabStop = false;
            // 
            // lblDeadline
            // 
            this.lblDeadline.AutoSize = true;
            this.lblDeadline.BackColor = System.Drawing.Color.Black;
            this.lblDeadline.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.lblDeadline.ForeColor = System.Drawing.Color.White;
            this.lblDeadline.Location = new System.Drawing.Point(317, 536);
            this.lblDeadline.Name = "lblDeadline";
            this.lblDeadline.Size = new System.Drawing.Size(104, 27);
            this.lblDeadline.TabIndex = 60;
            this.lblDeadline.Text = "Deadline:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Black;
            this.lblName.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(28, 140);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(125, 27);
            this.lblName.TabIndex = 61;
            this.lblName.Text = "User Name:";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.BackColor = System.Drawing.Color.Black;
            this.lblGender.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.lblGender.ForeColor = System.Drawing.Color.White;
            this.lblGender.Location = new System.Drawing.Point(28, 239);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(139, 27);
            this.lblGender.TabIndex = 66;
            this.lblGender.Text = "User Gender:";
            // 
            // lblRegister
            // 
            this.lblRegister.AutoSize = true;
            this.lblRegister.BackColor = System.Drawing.Color.Black;
            this.lblRegister.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.lblRegister.ForeColor = System.Drawing.Color.White;
            this.lblRegister.Location = new System.Drawing.Point(317, 437);
            this.lblRegister.Name = "lblRegister";
            this.lblRegister.Size = new System.Drawing.Size(175, 27);
            this.lblRegister.TabIndex = 65;
            this.lblRegister.Text = "Date of Register:";
            // 
            // txtConsultant
            // 
            this.txtConsultant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtConsultant.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConsultant.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsultant.ForeColor = System.Drawing.Color.White;
            this.txtConsultant.Location = new System.Drawing.Point(28, 784);
            this.txtConsultant.Name = "txtConsultant";
            this.txtConsultant.ReadOnly = true;
            this.txtConsultant.Size = new System.Drawing.Size(139, 32);
            this.txtConsultant.TabIndex = 70;
            this.txtConsultant.TabStop = false;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.BackColor = System.Drawing.Color.Black;
            this.lblAddress.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.lblAddress.ForeColor = System.Drawing.Color.White;
            this.lblAddress.Location = new System.Drawing.Point(28, 643);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(96, 27);
            this.lblAddress.TabIndex = 68;
            this.lblAddress.Text = "Address:";
            // 
            // txtTimes
            // 
            this.txtTimes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTimes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTimes.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimes.ForeColor = System.Drawing.Color.White;
            this.txtTimes.Location = new System.Drawing.Point(317, 784);
            this.txtTimes.Name = "txtTimes";
            this.txtTimes.ReadOnly = true;
            this.txtTimes.Size = new System.Drawing.Size(86, 32);
            this.txtTimes.TabIndex = 70;
            this.txtTimes.TabStop = false;
            // 
            // txtDeadline
            // 
            this.txtDeadline.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDeadline.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeadline.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold);
            this.txtDeadline.ForeColor = System.Drawing.Color.Red;
            this.txtDeadline.Location = new System.Drawing.Point(317, 583);
            this.txtDeadline.Name = "txtDeadline";
            this.txtDeadline.ReadOnly = true;
            this.txtDeadline.Size = new System.Drawing.Size(161, 32);
            this.txtDeadline.TabIndex = 70;
            this.txtDeadline.TabStop = false;
            // 
            // lblEamil
            // 
            this.lblEamil.AutoSize = true;
            this.lblEamil.BackColor = System.Drawing.Color.Black;
            this.lblEamil.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.lblEamil.ForeColor = System.Drawing.Color.White;
            this.lblEamil.Location = new System.Drawing.Point(28, 536);
            this.lblEamil.Name = "lblEamil";
            this.lblEamil.Size = new System.Drawing.Size(132, 27);
            this.lblEamil.TabIndex = 68;
            this.lblEamil.Text = "User E-mail:";
            // 
            // txtNumber
            // 
            this.txtNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNumber.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumber.ForeColor = System.Drawing.Color.White;
            this.txtNumber.Location = new System.Drawing.Point(28, 88);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.ReadOnly = true;
            this.txtNumber.Size = new System.Drawing.Size(213, 32);
            this.txtNumber.TabIndex = 69;
            this.txtNumber.TabStop = false;
            // 
            // lblTel
            // 
            this.lblTel.AutoSize = true;
            this.lblTel.BackColor = System.Drawing.Color.Black;
            this.lblTel.Font = new System.Drawing.Font("Times New Roman", 13.8F);
            this.lblTel.ForeColor = System.Drawing.Color.White;
            this.lblTel.Location = new System.Drawing.Point(28, 437);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(213, 27);
            this.lblTel.TabIndex = 68;
            this.lblTel.Text = "User Phone Number:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold);
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(28, 187);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(213, 32);
            this.txtName.TabIndex = 74;
            this.txtName.TabStop = false;
            // 
            // txtGender
            // 
            this.txtGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtGender.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGender.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold);
            this.txtGender.ForeColor = System.Drawing.Color.White;
            this.txtGender.Location = new System.Drawing.Point(28, 286);
            this.txtGender.Name = "txtGender";
            this.txtGender.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(125, 32);
            this.txtGender.TabIndex = 71;
            this.txtGender.TabStop = false;
            // 
            // txtBirth
            // 
            this.txtBirth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtBirth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBirth.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold);
            this.txtBirth.ForeColor = System.Drawing.Color.White;
            this.txtBirth.Location = new System.Drawing.Point(28, 385);
            this.txtBirth.Name = "txtBirth";
            this.txtBirth.ReadOnly = true;
            this.txtBirth.Size = new System.Drawing.Size(166, 32);
            this.txtBirth.TabIndex = 71;
            this.txtBirth.TabStop = false;
            // 
            // txtRegister
            // 
            this.txtRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRegister.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRegister.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold);
            this.txtRegister.ForeColor = System.Drawing.Color.White;
            this.txtRegister.Location = new System.Drawing.Point(317, 484);
            this.txtRegister.Name = "txtRegister";
            this.txtRegister.ReadOnly = true;
            this.txtRegister.Size = new System.Drawing.Size(166, 32);
            this.txtRegister.TabIndex = 71;
            this.txtRegister.TabStop = false;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Black;
            this.lblSearch.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(639, 140);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(209, 26);
            this.lblSearch.TabIndex = 85;
            this.lblSearch.Text = "Seach Information:";
            // 
            // lblCompareImage
            // 
            this.lblCompareImage.AutoSize = true;
            this.lblCompareImage.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompareImage.ForeColor = System.Drawing.Color.White;
            this.lblCompareImage.Location = new System.Drawing.Point(639, 202);
            this.lblCompareImage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCompareImage.Name = "lblCompareImage";
            this.lblCompareImage.Size = new System.Drawing.Size(198, 26);
            this.lblCompareImage.TabIndex = 84;
            this.lblCompareImage.Text = "Face Recognition:";
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCardNumber.Location = new System.Drawing.Point(865, 134);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(250, 36);
            this.txtCardNumber.TabIndex = 0;
            this.txtCardNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCardNumber_KeyPress);
            // 
            // lblImageList
            // 
            this.lblImageList.AutoSize = true;
            this.lblImageList.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblImageList.ForeColor = System.Drawing.Color.White;
            this.lblImageList.Location = new System.Drawing.Point(1314, 212);
            this.lblImageList.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageList.Name = "lblImageList";
            this.lblImageList.Size = new System.Drawing.Size(169, 26);
            this.lblImageList.TabIndex = 83;
            this.lblImageList.Text = "Face Database:";
            // 
            // picSearchNum
            // 
            this.picSearchNum.BackColor = System.Drawing.Color.Transparent;
            this.picSearchNum.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSearchNum.Image = global::Gym_Management_System.Properties.Resources.搜索;
            this.picSearchNum.Location = new System.Drawing.Point(1131, 131);
            this.picSearchNum.Name = "picSearchNum";
            this.picSearchNum.Size = new System.Drawing.Size(45, 45);
            this.picSearchNum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSearchNum.TabIndex = 88;
            this.picSearchNum.TabStop = false;
            this.picSearchNum.Click += new System.EventHandler(this.picSearchNum_Click);
            // 
            // cameraSwitch
            // 
            this.cameraSwitch.BackColor = System.Drawing.Color.Transparent;
            this.cameraSwitch.Checked = false;
            this.cameraSwitch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cameraSwitch.FalseColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.cameraSwitch.FalseTextColr = System.Drawing.Color.White;
            this.cameraSwitch.Location = new System.Drawing.Point(856, 202);
            this.cameraSwitch.Name = "cameraSwitch";
            this.cameraSwitch.Size = new System.Drawing.Size(83, 31);
            this.cameraSwitch.SwitchType = HZH_Controls.Controls.SwitchType.Ellipse;
            this.cameraSwitch.TabIndex = 90;
            this.cameraSwitch.Texts = null;
            this.cameraSwitch.TrueColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.cameraSwitch.TrueTextColr = System.Drawing.Color.White;
            this.cameraSwitch.CheckedChanged += new System.EventHandler(this.cameraSwitch_CheckedChanged);
            // 
            // btnChooseImg
            // 
            this.btnChooseImg.BackColor = System.Drawing.Color.Black;
            this.btnChooseImg.BaseColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnChooseImg.BorderColor = System.Drawing.Color.Black;
            this.btnChooseImg.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnChooseImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChooseImg.DownBack = null;
            this.btnChooseImg.Font = new System.Drawing.Font("Times New Roman", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnChooseImg.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnChooseImg.GlowColor = System.Drawing.Color.Red;
            this.btnChooseImg.Location = new System.Drawing.Point(1361, 765);
            this.btnChooseImg.MouseBack = null;
            this.btnChooseImg.MouseBaseColor = System.Drawing.Color.Blue;
            this.btnChooseImg.Name = "btnChooseImg";
            this.btnChooseImg.NormlBack = null;
            this.btnChooseImg.Size = new System.Drawing.Size(233, 44);
            this.btnChooseImg.TabIndex = 92;
            this.btnChooseImg.Text = "Face Registration";
            this.btnChooseImg.UseVisualStyleBackColor = false;
            this.btnChooseImg.Click += new System.EventHandler(this.btnChooseImg_Click);
            // 
            // btnClearImage
            // 
            this.btnClearImage.BackColor = System.Drawing.Color.Black;
            this.btnClearImage.BaseColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClearImage.BorderColor = System.Drawing.Color.Black;
            this.btnClearImage.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnClearImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearImage.DownBack = null;
            this.btnClearImage.Font = new System.Drawing.Font("Times New Roman", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnClearImage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClearImage.GlowColor = System.Drawing.Color.Red;
            this.btnClearImage.Location = new System.Drawing.Point(1689, 765);
            this.btnClearImage.MouseBack = null;
            this.btnClearImage.MouseBaseColor = System.Drawing.Color.Blue;
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.NormlBack = null;
            this.btnClearImage.Size = new System.Drawing.Size(179, 44);
            this.btnClearImage.TabIndex = 92;
            this.btnClearImage.Text = "Clear Face";
            this.btnClearImage.UseVisualStyleBackColor = false;
            this.btnClearImage.Click += new System.EventHandler(this.btnClearImage_Click);
            // 
            // btnSelectImg
            // 
            this.btnSelectImg.BackColor = System.Drawing.Color.Black;
            this.btnSelectImg.BaseColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSelectImg.BorderColor = System.Drawing.Color.Black;
            this.btnSelectImg.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSelectImg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectImg.DownBack = null;
            this.btnSelectImg.Font = new System.Drawing.Font("Times New Roman", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnSelectImg.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSelectImg.GlowColor = System.Drawing.Color.Red;
            this.btnSelectImg.Location = new System.Drawing.Point(1063, 765);
            this.btnSelectImg.MouseBack = null;
            this.btnSelectImg.MouseBaseColor = System.Drawing.Color.Blue;
            this.btnSelectImg.Name = "btnSelectImg";
            this.btnSelectImg.NormlBack = null;
            this.btnSelectImg.Size = new System.Drawing.Size(203, 44);
            this.btnSelectImg.TabIndex = 92;
            this.btnSelectImg.Text = "Choose Image";
            this.btnSelectImg.UseVisualStyleBackColor = false;
            this.btnSelectImg.Click += new System.EventHandler(this.btnSelectImg_Click);
            // 
            // btnMatch
            // 
            this.btnMatch.BackColor = System.Drawing.Color.Black;
            this.btnMatch.BaseColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnMatch.BorderColor = System.Drawing.Color.Black;
            this.btnMatch.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnMatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMatch.DownBack = null;
            this.btnMatch.Font = new System.Drawing.Font("Times New Roman", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnMatch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnMatch.GlowColor = System.Drawing.Color.Red;
            this.btnMatch.Location = new System.Drawing.Point(744, 765);
            this.btnMatch.MouseBack = null;
            this.btnMatch.MouseBaseColor = System.Drawing.Color.Blue;
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.NormlBack = null;
            this.btnMatch.Size = new System.Drawing.Size(224, 44);
            this.btnMatch.TabIndex = 92;
            this.btnMatch.Text = "Begin to Match";
            this.btnMatch.UseVisualStyleBackColor = false;
            this.btnMatch.Click += new System.EventHandler(this.btnMatch_Click);
            // 
            // ucledTime1
            // 
            this.ucledTime1.BackColor = System.Drawing.Color.Transparent;
            this.ucledTime1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucledTime1.ForeColor = System.Drawing.Color.White;
            this.ucledTime1.LineWidth = 8;
            this.ucledTime1.Location = new System.Drawing.Point(70, 931);
            this.ucledTime1.Name = "ucledTime1";
            this.ucledTime1.Size = new System.Drawing.Size(363, 65);
            this.ucledTime1.TabIndex = 95;
            this.ucledTime1.Value = new System.DateTime(2020, 2, 12, 16, 19, 16, 697);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // picRefresh
            // 
            this.picRefresh.BackColor = System.Drawing.Color.Transparent;
            this.picRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picRefresh.Image = global::Gym_Management_System.Properties.Resources.刷新;
            this.picRefresh.Location = new System.Drawing.Point(1131, 193);
            this.picRefresh.Name = "picRefresh";
            this.picRefresh.Size = new System.Drawing.Size(45, 45);
            this.picRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRefresh.TabIndex = 101;
            this.picRefresh.TabStop = false;
            this.picRefresh.Click += new System.EventHandler(this.picRefresh_Click);
            // 
            // lblCompareInfo
            // 
            this.lblCompareInfo.AutoSize = true;
            this.lblCompareInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCompareInfo.Location = new System.Drawing.Point(889, 14);
            this.lblCompareInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCompareInfo.Name = "lblCompareInfo";
            this.lblCompareInfo.Size = new System.Drawing.Size(0, 20);
            this.lblCompareInfo.TabIndex = 37;
            // 
            // FaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1920, 973);
            this.Controls.Add(this.picRefresh);
            this.Controls.Add(this.ucledTime1);
            this.Controls.Add(this.btnMatch);
            this.Controls.Add(this.btnSelectImg);
            this.Controls.Add(this.btnClearImage);
            this.Controls.Add(this.btnChooseImg);
            this.Controls.Add(this.cameraSwitch);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picSearchNum);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.groBoxInfor);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblCompareImage);
            this.Controls.Add(this.txtCardNumber);
            this.Controls.Add(this.lblImageList);
            this.Controls.Add(this.txtThreshold);
            this.Controls.Add(this.irVideoSource);
            this.Controls.Add(this.videoDev);
            this.Controls.Add(this.lblCompareInfo);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.picImageCompare);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FaceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Access Control System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Closed);
            this.Load += new System.EventHandler(this.FaceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picImageCompare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.groBoxInfor.ResumeLayout(false);
            this.groBoxInfor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picImageCompare;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.ImageList imageLists;
        private System.Windows.Forms.ListView listView;
        private AForge.Controls.VideoSourcePlayer videoDev;
        private System.Windows.Forms.TextBox txtThreshold;
        private AForge.Controls.VideoSourcePlayer irVideoSource;
        public System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.GroupBox groBoxInfor;
        private System.Windows.Forms.PictureBox picUserImage;
        private System.Windows.Forms.Label lblBirth;
        private System.Windows.Forms.Label lblUserImage;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label lblDeadline;
        private System.Windows.Forms.Label lblRegister;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.TextBox txtDeadline;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox txtRegister;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.TextBox txtBirth;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblCompareImage;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.Label lblImageList;
        private System.Windows.Forms.PictureBox picSearchNum;
        private HZH_Controls.Controls.UCSwitch cameraSwitch;
        private CCWin.SkinControl.SkinButton btnChooseImg;
        private CCWin.SkinControl.SkinButton btnClearImage;
        private CCWin.SkinControl.SkinButton btnSelectImg;
        private CCWin.SkinControl.SkinButton btnMatch;
        private System.Windows.Forms.TextBox txtIdentity;
        private System.Windows.Forms.Label lblIdentity;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEamil;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.IO.Ports.SerialPort serialPort1;
        public HZH_Controls.Controls.UCLEDTime ucledTime1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox picRefresh;
        private System.Windows.Forms.Label lblExTimes;
        private System.Windows.Forms.TextBox txtTimes;
        private System.Windows.Forms.Label lblConsultant;
        private System.Windows.Forms.TextBox txtConsultant;
        private System.Windows.Forms.Label lblCompareInfo;
    }
}

