namespace Gym_Management_System
{
    partial class Registration_Page
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registration_Page));
            this.lblConfirmPsw = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.btnRegister = new CCWin.SkinControl.SkinButton();
            this.txtEnsurePsw = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.picTel = new CCWin.SkinControl.SkinPictureBox();
            this.picEmail = new CCWin.SkinControl.SkinPictureBox();
            this.picConPsw = new CCWin.SkinControl.SkinPictureBox();
            this.picPsw = new CCWin.SkinControl.SkinPictureBox();
            this.picUser = new CCWin.SkinControl.SkinPictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.picBackGround = new System.Windows.Forms.PictureBox();
            this.picTelError = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.picemailError = new System.Windows.Forms.PictureBox();
            this.picPassword = new System.Windows.Forms.PictureBox();
            this.picConfirmPasw = new System.Windows.Forms.PictureBox();
            this.picNameError = new System.Windows.Forms.PictureBox();
            this.lblTip = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picTel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConPsw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPsw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTelError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picemailError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConfirmPasw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNameError)).BeginInit();
            this.SuspendLayout();
            // 
            // lblConfirmPsw
            // 
            this.lblConfirmPsw.AutoSize = true;
            this.lblConfirmPsw.BackColor = System.Drawing.Color.White;
            this.lblConfirmPsw.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPsw.ForeColor = System.Drawing.Color.DarkGray;
            this.lblConfirmPsw.Location = new System.Drawing.Point(740, 572);
            this.lblConfirmPsw.Name = "lblConfirmPsw";
            this.lblConfirmPsw.Size = new System.Drawing.Size(301, 32);
            this.lblConfirmPsw.TabIndex = 47;
            this.lblConfirmPsw.Text = "Confirm the password...";
            this.lblConfirmPsw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblConfirmPsw_MouseDown);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.White;
            this.lblPassword.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.DarkGray;
            this.lblPassword.Location = new System.Drawing.Point(740, 518);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(267, 32);
            this.lblPassword.TabIndex = 46;
            this.lblPassword.Text = "Enter the password...";
            this.lblPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPassword_MouseDown);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.BackColor = System.Drawing.Color.White;
            this.lblEmail.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.DarkGray;
            this.lblEmail.Location = new System.Drawing.Point(740, 468);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(288, 32);
            this.lblEmail.TabIndex = 45;
            this.lblEmail.Text = "Enter E-mail address...";
            this.lblEmail.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblEmail_MouseDown);
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.BackColor = System.Drawing.Color.White;
            this.lblPhone.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.ForeColor = System.Drawing.Color.DarkGray;
            this.lblPhone.Location = new System.Drawing.Point(740, 416);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(282, 32);
            this.lblPhone.TabIndex = 44;
            this.lblPhone.Text = "Enter phone number...";
            this.lblPhone.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPhone_MouseDown);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.White;
            this.lblName.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.DarkGray;
            this.lblName.Location = new System.Drawing.Point(740, 366);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(292, 32);
            this.lblName.TabIndex = 43;
            this.lblName.Text = "Enter name only letters";
            this.lblName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblName_MouseDown);
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.Black;
            this.btnRegister.BaseColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRegister.BorderColor = System.Drawing.Color.Black;
            this.btnRegister.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.DownBack = null;
            this.btnRegister.Font = new System.Drawing.Font("Times New Roman", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRegister.GlowColor = System.Drawing.Color.Red;
            this.btnRegister.Location = new System.Drawing.Point(824, 659);
            this.btnRegister.MouseBack = null;
            this.btnRegister.MouseBaseColor = System.Drawing.Color.Blue;
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.NormlBack = null;
            this.btnRegister.Size = new System.Drawing.Size(147, 61);
            this.btnRegister.TabIndex = 42;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // txtEnsurePsw
            // 
            this.txtEnsurePsw.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnsurePsw.Location = new System.Drawing.Point(736, 568);
            this.txtEnsurePsw.MaxLength = 15;
            this.txtEnsurePsw.Name = "txtEnsurePsw";
            this.txtEnsurePsw.Size = new System.Drawing.Size(325, 39);
            this.txtEnsurePsw.TabIndex = 4;
            this.txtEnsurePsw.TextChanged += new System.EventHandler(this.txtConfirmPsw_TextChanged);
            this.txtEnsurePsw.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConfirmPsw_KeyPress);
            this.txtEnsurePsw.Leave += new System.EventHandler(this.txtConfirmPsw_Leave);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(736, 514);
            this.txtPassword.MaxLength = 16;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(325, 39);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(736, 464);
            this.txtEmail.MaxLength = 18;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(325, 39);
            this.txtEmail.TabIndex = 2;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            // 
            // txtTelephone
            // 
            this.txtTelephone.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelephone.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTelephone.Location = new System.Drawing.Point(736, 412);
            this.txtTelephone.MaxLength = 11;
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(325, 39);
            this.txtTelephone.TabIndex = 1;
            this.txtTelephone.TextChanged += new System.EventHandler(this.txtTelephone_TextChanged);
            this.txtTelephone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelephone_KeyPress);
            this.txtTelephone.Leave += new System.EventHandler(this.txtTelephone_Leave);
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(736, 362);
            this.txtName.MaxLength = 10;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(325, 39);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUser_KeyPress);
            this.txtName.Leave += new System.EventHandler(this.txtUser_Leave);
            this.txtName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtUser_MouseDown);
            // 
            // picTel
            // 
            this.picTel.BackColor = System.Drawing.Color.Transparent;
            this.picTel.Image = global::Gym_Management_System.Properties.Resources.old_phone_512;
            this.picTel.Location = new System.Drawing.Point(690, 409);
            this.picTel.Name = "picTel";
            this.picTel.Size = new System.Drawing.Size(40, 40);
            this.picTel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTel.TabIndex = 35;
            this.picTel.TabStop = false;
            // 
            // picEmail
            // 
            this.picEmail.BackColor = System.Drawing.Color.Transparent;
            this.picEmail.Image = global::Gym_Management_System.Properties.Resources.th;
            this.picEmail.Location = new System.Drawing.Point(690, 461);
            this.picEmail.Name = "picEmail";
            this.picEmail.Size = new System.Drawing.Size(40, 40);
            this.picEmail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEmail.TabIndex = 34;
            this.picEmail.TabStop = false;
            // 
            // picConPsw
            // 
            this.picConPsw.BackColor = System.Drawing.Color.Transparent;
            this.picConPsw.Image = global::Gym_Management_System.Properties.Resources.img_268354;
            this.picConPsw.Location = new System.Drawing.Point(690, 565);
            this.picConPsw.Name = "picConPsw";
            this.picConPsw.Size = new System.Drawing.Size(40, 40);
            this.picConPsw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picConPsw.TabIndex = 33;
            this.picConPsw.TabStop = false;
            // 
            // picPsw
            // 
            this.picPsw.BackColor = System.Drawing.Color.Transparent;
            this.picPsw.Image = global::Gym_Management_System.Properties.Resources.icons_password_51211;
            this.picPsw.Location = new System.Drawing.Point(690, 511);
            this.picPsw.Name = "picPsw";
            this.picPsw.Size = new System.Drawing.Size(40, 40);
            this.picPsw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPsw.TabIndex = 32;
            this.picPsw.TabStop = false;
            // 
            // picUser
            // 
            this.picUser.BackColor = System.Drawing.Color.Transparent;
            this.picUser.Image = global::Gym_Management_System.Properties.Resources._648969_star_ratings_5121;
            this.picUser.Location = new System.Drawing.Point(690, 359);
            this.picUser.Name = "picUser";
            this.picUser.Size = new System.Drawing.Size(40, 40);
            this.picUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUser.TabIndex = 36;
            this.picUser.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblTitle.Location = new System.Drawing.Point(267, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(803, 134);
            this.lblTitle.TabIndex = 31;
            this.lblTitle.Text = "Intellgent Management System\r\n               of Fitness Club";
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogo.Image = global::Gym_Management_System.Properties.Resources._333;
            this.picLogo.Location = new System.Drawing.Point(800, 170);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(171, 182);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 30;
            this.picLogo.TabStop = false;
            this.picLogo.Click += new System.EventHandler(this.picLogo_Click);
            // 
            // picBackGround
            // 
            this.picBackGround.BackColor = System.Drawing.Color.Transparent;
            this.picBackGround.Image = global::Gym_Management_System.Properties.Resources.background_gym_6;
            this.picBackGround.Location = new System.Drawing.Point(0, -5);
            this.picBackGround.Name = "picBackGround";
            this.picBackGround.Size = new System.Drawing.Size(1299, 764);
            this.picBackGround.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBackGround.TabIndex = 29;
            this.picBackGround.TabStop = false;
            // 
            // picTelError
            // 
            this.picTelError.BackColor = System.Drawing.Color.Transparent;
            this.picTelError.Cursor = System.Windows.Forms.Cursors.Help;
            this.picTelError.Image = global::Gym_Management_System.Properties.Resources.提示;
            this.picTelError.Location = new System.Drawing.Point(1075, 419);
            this.picTelError.Name = "picTelError";
            this.picTelError.Size = new System.Drawing.Size(38, 38);
            this.picTelError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTelError.TabIndex = 30;
            this.picTelError.TabStop = false;
            this.toolTip1.SetToolTip(this.picTelError, "Phone Number Error!");
            this.picTelError.Visible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.White;
            this.toolTip1.ForeColor = System.Drawing.Color.Black;
            // 
            // picemailError
            // 
            this.picemailError.BackColor = System.Drawing.Color.Transparent;
            this.picemailError.Cursor = System.Windows.Forms.Cursors.Help;
            this.picemailError.Image = global::Gym_Management_System.Properties.Resources.提示;
            this.picemailError.Location = new System.Drawing.Point(1075, 470);
            this.picemailError.Name = "picemailError";
            this.picemailError.Size = new System.Drawing.Size(38, 38);
            this.picemailError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picemailError.TabIndex = 49;
            this.picemailError.TabStop = false;
            this.toolTip1.SetToolTip(this.picemailError, "Email address format error!");
            this.picemailError.Visible = false;
            // 
            // picPassword
            // 
            this.picPassword.BackColor = System.Drawing.Color.Transparent;
            this.picPassword.Cursor = System.Windows.Forms.Cursors.Help;
            this.picPassword.Image = global::Gym_Management_System.Properties.Resources.提示;
            this.picPassword.Location = new System.Drawing.Point(1075, 521);
            this.picPassword.Name = "picPassword";
            this.picPassword.Size = new System.Drawing.Size(38, 38);
            this.picPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPassword.TabIndex = 49;
            this.picPassword.TabStop = false;
            this.toolTip1.SetToolTip(this.picPassword, "Password must contain \r\none uppercase letters,\r\none lowercase letters, \r\nand one " +
        "numbers!\r\nThe password length ≥ 6");
            this.picPassword.Visible = false;
            // 
            // picConfirmPasw
            // 
            this.picConfirmPasw.BackColor = System.Drawing.Color.Transparent;
            this.picConfirmPasw.Cursor = System.Windows.Forms.Cursors.Help;
            this.picConfirmPasw.Image = global::Gym_Management_System.Properties.Resources.提示;
            this.picConfirmPasw.Location = new System.Drawing.Point(1075, 572);
            this.picConfirmPasw.Name = "picConfirmPasw";
            this.picConfirmPasw.Size = new System.Drawing.Size(38, 38);
            this.picConfirmPasw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picConfirmPasw.TabIndex = 49;
            this.picConfirmPasw.TabStop = false;
            this.toolTip1.SetToolTip(this.picConfirmPasw, "Incorrect password, \r\nplease set it again!");
            this.picConfirmPasw.Visible = false;
            // 
            // picNameError
            // 
            this.picNameError.BackColor = System.Drawing.Color.Transparent;
            this.picNameError.Cursor = System.Windows.Forms.Cursors.Help;
            this.picNameError.Image = global::Gym_Management_System.Properties.Resources.提示;
            this.picNameError.Location = new System.Drawing.Point(1075, 368);
            this.picNameError.Name = "picNameError";
            this.picNameError.Size = new System.Drawing.Size(38, 38);
            this.picNameError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNameError.TabIndex = 30;
            this.picNameError.TabStop = false;
            this.toolTip1.SetToolTip(this.picNameError, "Please input your name");
            this.picNameError.Visible = false;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.BackColor = System.Drawing.Color.Transparent;
            this.lblTip.Font = new System.Drawing.Font("Times New Roman", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTip.ForeColor = System.Drawing.Color.Red;
            this.lblTip.Location = new System.Drawing.Point(698, 617);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(0, 32);
            this.lblTip.TabIndex = 50;
            // 
            // Registration_Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 754);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.picConfirmPasw);
            this.Controls.Add(this.picPassword);
            this.Controls.Add(this.picemailError);
            this.Controls.Add(this.lblConfirmPsw);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.txtEnsurePsw);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtTelephone);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.picTel);
            this.Controls.Add(this.picEmail);
            this.Controls.Add(this.picConPsw);
            this.Controls.Add(this.picPsw);
            this.Controls.Add(this.picUser);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picNameError);
            this.Controls.Add(this.picTelError);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.picBackGround);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Registration_Page";
            this.Text = "Registration_Page";
            this.Load += new System.EventHandler(this.Registration_Page_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picTel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConPsw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPsw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTelError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picemailError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picConfirmPasw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNameError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConfirmPsw;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblName;
        private CCWin.SkinControl.SkinButton btnRegister;
        private System.Windows.Forms.TextBox txtEnsurePsw;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.TextBox txtName;
        private CCWin.SkinControl.SkinPictureBox picTel;
        private CCWin.SkinControl.SkinPictureBox picEmail;
        private CCWin.SkinControl.SkinPictureBox picConPsw;
        private CCWin.SkinControl.SkinPictureBox picPsw;
        private CCWin.SkinControl.SkinPictureBox picUser;
        public System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.PictureBox picBackGround;
        private System.Windows.Forms.PictureBox picTelError;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox picemailError;
        private System.Windows.Forms.PictureBox picPassword;
        private System.Windows.Forms.PictureBox picConfirmPasw;
        private System.Windows.Forms.PictureBox picNameError;
        public System.Windows.Forms.Label lblTip;
    }
}