namespace Gym_Management_System
{
    partial class Login_Page
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, null, true, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Page));
            this.lblRegistration = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.picNumber = new System.Windows.Forms.PictureBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.picBackGround = new System.Windows.Forms.PictureBox();
            this.btnLogin = new CCWin.SkinControl.SkinButton();
            this.picPsw = new CCWin.SkinControl.SkinPictureBox();
            this.picUser = new CCWin.SkinControl.SkinPictureBox();
            this.picPaswError = new System.Windows.Forms.PictureBox();
            this.picNameError = new System.Windows.Forms.PictureBox();
            this.picNumberError = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ckbAdmin = new HZH_Controls.Controls.UCCheckBox();
            this.ckbTrainer = new HZH_Controls.Controls.UCCheckBox();
            this.ckbMember = new HZH_Controls.Controls.UCCheckBox();
            this.picKey = new CCWin.SkinControl.SkinPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPsw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPaswError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNameError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNumberError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKey)).BeginInit();
            this.SuspendLayout();
            // 
            // splashScreenManager1
            // 
            splashScreenManager1.ClosingDelay = 500;
            // 
            // lblRegistration
            // 
            this.lblRegistration.AutoSize = true;
            this.lblRegistration.BackColor = System.Drawing.Color.Transparent;
            this.lblRegistration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRegistration.Font = new System.Drawing.Font("Times New Roman", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistration.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblRegistration.Location = new System.Drawing.Point(762, 685);
            this.lblRegistration.Name = "lblRegistration";
            this.lblRegistration.Size = new System.Drawing.Size(191, 30);
            this.lblRegistration.TabIndex = 35;
            this.lblRegistration.Text = "Register Account";
            this.lblRegistration.Click += new System.EventHandler(this.lblRegistration_Click);
            this.lblRegistration.MouseLeave += new System.EventHandler(this.lblRegistration_MouseLeave);
            this.lblRegistration.MouseHover += new System.EventHandler(this.lblRegistration_MouseHover);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.BackColor = System.Drawing.Color.White;
            this.lblNumber.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumber.ForeColor = System.Drawing.Color.DarkGray;
            this.lblNumber.Location = new System.Drawing.Point(736, 516);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(116, 32);
            this.lblNumber.TabIndex = 32;
            this.lblNumber.Text = "Captcha";
            this.lblNumber.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblNumber_MouseDown);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.White;
            this.lblName.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.DarkGray;
            this.lblName.Location = new System.Drawing.Point(737, 379);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(276, 32);
            this.lblName.TabIndex = 33;
            this.lblName.Text = "Name / Tel / E-mail...  ";
            this.lblName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblName_MouseDown);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.White;
            this.lblPassword.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.DarkGray;
            this.lblPassword.Location = new System.Drawing.Point(737, 451);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(274, 32);
            this.lblPassword.TabIndex = 34;
            this.lblPassword.Text = "Enter the password... ";
            this.lblPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPassword_MouseDown);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblTitle.Location = new System.Drawing.Point(444, 217);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(803, 134);
            this.lblTitle.TabIndex = 28;
            this.lblTitle.Text = "Intellgent Management System\r\n               of Fitness Club";
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogo.Image = global::Gym_Management_System.Properties.Resources._333;
            this.picLogo.Location = new System.Drawing.Point(782, 20);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(171, 192);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 31;
            this.picLogo.TabStop = false;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(733, 376);
            this.txtName.MaxLength = 18;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(305, 39);
            this.txtName.TabIndex = 0;
            this.txtName.TabStop = false;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // txtNumber
            // 
            this.txtNumber.BackColor = System.Drawing.Color.White;
            this.txtNumber.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumber.Location = new System.Drawing.Point(733, 513);
            this.txtNumber.MaxLength = 5;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(126, 39);
            this.txtNumber.TabIndex = 2;
            this.txtNumber.TabStop = false;
            this.txtNumber.TextChanged += new System.EventHandler(this.txtNumber_TextChanged);
            this.txtNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            this.txtNumber.Leave += new System.EventHandler(this.txtNumber_Leave);
            // 
            // picNumber
            // 
            this.picNumber.BackColor = System.Drawing.Color.Transparent;
            this.picNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picNumber.Location = new System.Drawing.Point(921, 513);
            this.picNumber.Name = "picNumber";
            this.picNumber.Size = new System.Drawing.Size(142, 38);
            this.picNumber.TabIndex = 30;
            this.picNumber.TabStop = false;
            this.toolTip1.SetToolTip(this.picNumber, "Click to replace verification code !");
            this.picNumber.Click += new System.EventHandler(this.picNumber_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.AcceptsReturn = true;
            this.txtPassword.AcceptsTab = true;
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(733, 447);
            this.txtPassword.MaxLength = 16;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(305, 39);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TabStop = false;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // picBackGround
            // 
            this.picBackGround.BackColor = System.Drawing.Color.Transparent;
            this.picBackGround.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBackGround.Image = global::Gym_Management_System.Properties.Resources.yAzUNP;
            this.picBackGround.Location = new System.Drawing.Point(-8, -1);
            this.picBackGround.Name = "picBackGround";
            this.picBackGround.Size = new System.Drawing.Size(1305, 743);
            this.picBackGround.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBackGround.TabIndex = 25;
            this.picBackGround.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Black;
            this.btnLogin.BaseColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLogin.BorderColor = System.Drawing.Color.Black;
            this.btnLogin.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.DownBack = null;
            this.btnLogin.Font = new System.Drawing.Font("Times New Roman", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogin.GlowColor = System.Drawing.Color.Red;
            this.btnLogin.Location = new System.Drawing.Point(782, 613);
            this.btnLogin.MouseBack = null;
            this.btnLogin.MouseBaseColor = System.Drawing.Color.Blue;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.NormlBack = null;
            this.btnLogin.Size = new System.Drawing.Size(147, 61);
            this.btnLogin.TabIndex = 36;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // picPsw
            // 
            this.picPsw.BackColor = System.Drawing.Color.Transparent;
            this.picPsw.Image = global::Gym_Management_System.Properties.Resources.icons_password_5121;
            this.picPsw.Location = new System.Drawing.Point(679, 443);
            this.picPsw.Name = "picPsw";
            this.picPsw.Size = new System.Drawing.Size(48, 48);
            this.picPsw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPsw.TabIndex = 37;
            this.picPsw.TabStop = false;
            // 
            // picUser
            // 
            this.picUser.BackColor = System.Drawing.Color.Transparent;
            this.picUser.Image = global::Gym_Management_System.Properties.Resources._648969_star_ratings_5121;
            this.picUser.Location = new System.Drawing.Point(679, 372);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(48, 48);
            this.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUser.TabIndex = 38;
            this.picUser.TabStop = false;
            // 
            // picPaswError
            // 
            this.picPaswError.BackColor = System.Drawing.Color.Transparent;
            this.picPaswError.Cursor = System.Windows.Forms.Cursors.Help;
            this.picPaswError.Image = global::Gym_Management_System.Properties.Resources.提示;
            this.picPaswError.Location = new System.Drawing.Point(1049, 450);
            this.picPaswError.Name = "picPaswError";
            this.picPaswError.Size = new System.Drawing.Size(38, 38);
            this.picPaswError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPaswError.TabIndex = 103;
            this.picPaswError.TabStop = false;
            this.toolTip1.SetToolTip(this.picPaswError, "Please enter your login password !");
            this.picPaswError.Visible = false;
            // 
            // picNameError
            // 
            this.picNameError.BackColor = System.Drawing.Color.Transparent;
            this.picNameError.Cursor = System.Windows.Forms.Cursors.Help;
            this.picNameError.Image = global::Gym_Management_System.Properties.Resources.提示;
            this.picNameError.Location = new System.Drawing.Point(1049, 379);
            this.picNameError.Name = "picNameError";
            this.picNameError.Size = new System.Drawing.Size(38, 38);
            this.picNameError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNameError.TabIndex = 102;
            this.picNameError.TabStop = false;
            this.toolTip1.SetToolTip(this.picNameError, "Enter name, \r\nphone number, \r\nemail address to login");
            this.picNameError.Visible = false;
            // 
            // picNumberError
            // 
            this.picNumberError.BackColor = System.Drawing.Color.Transparent;
            this.picNumberError.Cursor = System.Windows.Forms.Cursors.Help;
            this.picNumberError.Image = global::Gym_Management_System.Properties.Resources.提示;
            this.picNumberError.Location = new System.Drawing.Point(874, 516);
            this.picNumberError.Name = "picNumberError";
            this.picNumberError.Size = new System.Drawing.Size(38, 38);
            this.picNumberError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNumberError.TabIndex = 103;
            this.picNumberError.TabStop = false;
            this.toolTip1.SetToolTip(this.picNumberError, "Verification Code Error !");
            this.picNumberError.Visible = false;
            // 
            // ckbAdmin
            // 
            this.ckbAdmin.BackColor = System.Drawing.Color.Transparent;
            this.ckbAdmin.Checked = false;
            this.ckbAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckbAdmin.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbAdmin.ForeColor = System.Drawing.Color.White;
            this.ckbAdmin.Location = new System.Drawing.Point(660, 568);
            this.ckbAdmin.Name = "ckbAdmin";
            this.ckbAdmin.Padding = new System.Windows.Forms.Padding(1);
            this.ckbAdmin.Size = new System.Drawing.Size(212, 30);
            this.ckbAdmin.TabIndex = 107;
            this.ckbAdmin.TextValue = "Adminstration";
            this.ckbAdmin.CheckedChangeEvent += new System.EventHandler(this.ckbAdmin_CheckedChangeEvent);
            // 
            // ckbTrainer
            // 
            this.ckbTrainer.BackColor = System.Drawing.Color.Transparent;
            this.ckbTrainer.Checked = false;
            this.ckbTrainer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckbTrainer.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbTrainer.ForeColor = System.Drawing.Color.White;
            this.ckbTrainer.Location = new System.Drawing.Point(878, 568);
            this.ckbTrainer.Name = "ckbTrainer";
            this.ckbTrainer.Padding = new System.Windows.Forms.Padding(1);
            this.ckbTrainer.Size = new System.Drawing.Size(138, 30);
            this.ckbTrainer.TabIndex = 107;
            this.ckbTrainer.TextValue = "Trainer";
            this.ckbTrainer.CheckedChangeEvent += new System.EventHandler(this.ckbTrainer_CheckedChangeEvent);
            // 
            // ckbMember
            // 
            this.ckbMember.BackColor = System.Drawing.Color.Transparent;
            this.ckbMember.Checked = false;
            this.ckbMember.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckbMember.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbMember.ForeColor = System.Drawing.Color.White;
            this.ckbMember.Location = new System.Drawing.Point(1022, 568);
            this.ckbMember.Name = "ckbMember";
            this.ckbMember.Padding = new System.Windows.Forms.Padding(1);
            this.ckbMember.Size = new System.Drawing.Size(138, 30);
            this.ckbMember.TabIndex = 107;
            this.ckbMember.TextValue = "Member";
            this.ckbMember.CheckedChangeEvent += new System.EventHandler(this.ckbMember_CheckedChangeEvent);
            // 
            // picKey
            // 
            this.picKey.BackColor = System.Drawing.Color.Transparent;
            this.picKey.Image = global::Gym_Management_System.Properties.Resources.验证码;
            this.picKey.Location = new System.Drawing.Point(679, 513);
            this.picKey.Name = "picKey";
            this.picKey.Size = new System.Drawing.Size(48, 48);
            this.picKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picKey.TabIndex = 108;
            this.picKey.TabStop = false;
            // 
            // Login_Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 733);
            this.Controls.Add(this.picKey);
            this.Controls.Add(this.ckbMember);
            this.Controls.Add(this.ckbTrainer);
            this.Controls.Add(this.ckbAdmin);
            this.Controls.Add(this.picNumberError);
            this.Controls.Add(this.picPaswError);
            this.Controls.Add(this.picNameError);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.picPsw);
            this.Controls.Add(this.picUser);
            this.Controls.Add(this.lblRegistration);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.picNumber);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.picBackGround);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Login_Page";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log in";
            this.Load += new System.EventHandler(this.Login_Page_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPsw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPaswError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNameError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNumberError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picKey)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRegistration;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPassword;
        public System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.PictureBox picNumber;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox picBackGround;
        private CCWin.SkinControl.SkinButton btnLogin;
        private CCWin.SkinControl.SkinPictureBox picPsw;
        private CCWin.SkinControl.SkinPictureBox picUser;
        private System.Windows.Forms.PictureBox picPaswError;
        private System.Windows.Forms.PictureBox picNameError;
        private System.Windows.Forms.PictureBox picNumberError;
        private System.Windows.Forms.ToolTip toolTip1;
        private HZH_Controls.Controls.UCCheckBox ckbAdmin;
        private HZH_Controls.Controls.UCCheckBox ckbTrainer;
        private HZH_Controls.Controls.UCCheckBox ckbMember;
        private CCWin.SkinControl.SkinPictureBox picKey;
    }
}