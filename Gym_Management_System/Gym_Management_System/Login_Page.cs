/*
 * Name: Liu Shangyuan
 * 
 * SCN: 197076658
 * 
 * School: BUAA
 * 
 * Version: Gym Management System - Final
 * 
 * */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Gym_Management_System;
using MySql.Data.MySqlClient;
using DevExpress.XtraSplashScreen;
using System.Threading;

namespace Gym_Management_System
{
    public partial class Login_Page : Form
    {
        private string code; // After clicking, the text box content is empty

        #region Connect_Database
        //SignInInterface Mysql DataBase Connection
        private const string dbServer = "server=127.0.0.1;port=3306;database=gym;user=root;password=**********";
        MySqlConnection conn;

        // Create a connection
        private void ConnDB()
        {
            // server address;The port number.Database;The user name;password
            string connectStr = dbServer; // username and password defined in MySQL
                                          // create a connection
            conn = new MySqlConnection(connectStr);
            // Open the Connection
            try
            {
                conn.Open();//Mysql database did not start, this sentence error!
            }
            catch
            {
                // throw exception out
                MessageBox.Show("No Connection Established !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }// Connect to database

        // Close the database
        private void CloseDB()
        {
            // close connection
            conn.Close();
        }
        #endregion

        // Rewrite the capital key
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        // Determine whether to press the capital key
        public static bool CapsLockStatus
        {
            get
            {
                byte[] bs = new byte[256];
                GetKeyboardState(bs);
                return (bs[0x14] == 1);
            }
        }

        public Login_Page()
        {
            InitializeComponent();
        }

        // Random digit verification code
        private void GenCode()
        {
            Random r = new Random();
            string str = "";
            for (int i = 0; i < 5; i++)
            {
                str += r.Next(0, 10);
            }
            // str = string.Copy(str);
            code = str;
            // MessageBox.Show(str);
            Bitmap bmp = new Bitmap(120, 40);
            Graphics g = Graphics.FromImage(bmp);

            // Randomly generated number
            for (int i = 0; i < 5; i++)
            {
                Point p = new Point(i * 20, 0);  // Set Size
                string[] fonts = { "Times New Roman", "Times New Roman", "Times New Roman", "Times New Roman", "Times New Roman" };  // Set Fonts
                Color[] cl = { Color.Orange, Color.Blue, Color.Red, Color.Green, Color.Yellow };  // Set Color
                g.DrawString(str[i].ToString(), new Font(fonts[r.Next(0, 5)], 20, FontStyle.Bold), new SolidBrush(cl[r.Next(0, 5)]), p); // Set Style
            }

            // Draw a line to identify obstacles
            for (int i = 0; i < 20; i++)
            {
                Pen pen = new Pen(Brushes.Black);  // Set Pen Color
                Point p1 = new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height));
                Point p2 = new Point(r.Next(0, bmp.Width), r.Next(0, bmp.Height));
                g.DrawLine(pen, p1, p2);
            }
            for (int i = 0; i < 500; i++)
            {
                bmp.SetPixel(r.Next(0, bmp.Width), r.Next(0, bmp.Height), Color.Black);
            }
            // Put bmp into picturbox
            picNumber.Image = bmp;
            txtNumber.Text = "";
        }

        private void Login_Page_Load(object sender, EventArgs e)
        {
            // Windows form starts style
            Win32.AnimateWindow(this.Handle, 1500, Win32.AW_BLEND);
            ConnDB();
            CloseDB();
            // Set the background color is transparent
            picLogo.Parent = picBackGround;
            picUser.Parent = picBackGround;
            picPsw.Parent = picBackGround;
            lblTitle.Parent = picBackGround;
            lblRegistration.Parent = picBackGround;
            picNameError.Parent = picBackGround;
            picPaswError.Parent = picBackGround;
            picNumberError.Parent = picBackGround;
            picNumber.Parent = picBackGround;
            picKey.Parent = picBackGround;
            ckbAdmin.Parent = picBackGround;
            ckbTrainer.Parent = picBackGround;
            ckbMember.Parent = picBackGround;
            ckbAdmin.Checked = true;
            lblName.Focus();
            GenCode(); // Show the digit verification code
        }

        private void picNumber_Click(object sender, EventArgs e)
        {
            //Producing a New Verification Code
            GenCode();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Forbidden to input Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5)) 
            {
                e.Handled = true;
            }

            // Enter key Event
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
                lblPassword.Visible = false;
            }
        }

        /// <summary>
        /// System resource loading
        /// </summary>
        private void Loading()
        {
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            Thread.Sleep(1500);
        }

        /// <summary>
        /// Loading completed
        /// </summary>
        private void LoadingCompleted()
        {
            SplashScreenManager.CloseForm();
        }

        /// <summary>
        /// Admin & Member & Trainer Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {          
            // Name can't be null
            if (txtName.Text == "")
            {
                picNameError.Visible = true;
            }
            else
            {
                picNameError.Visible = false;
            }

            // The user password can't be null
            if (txtPassword.Text == "")
            {
                picPaswError.Visible = true;  
            }
            else
            {
                picPaswError.Visible = false;
            }

            // The digita code can't be null
            if(txtNumber.Text != code)
            {
                picNumberError.Visible = true;
            }
            else
            {
                picNumberError.Visible = false;
            }

             //  If the warning icons are showing, can not to connect database
            if((picNameError.Visible == true) || (picPaswError.Visible == true))
            {
                txtNumber.Text = "";
                MessageBox.Show("Please Enter the Correct Account and Password !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(picNumberError.Visible == true)  // If the digit verification code is error or null
            {
                GenCode(); // Change a new code
                MessageBox.Show("Verification Code Error !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumber.Text = "";
            }
            else if (ckbAdmin.Checked == false && ckbTrainer.Checked == false && ckbMember.Checked == false)
            {
                MessageBox.Show("Please Select Identity Information to Login !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(ckbAdmin.Checked == true)
            {
                try
                {
                    ConnDB();
                    int key = 0;    // Read the number of records that meet the condition
                    // Connect the Mysql Database and verificate the password by User Name
                    string sql = string.Format("select count(*) from Administrator where (Name = '{0}'and Password = md5('{1}'))", txtName.Text, txtPassword.Text);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);   // Execute the query command
                    // Determine the correctness of passwoed with User Name
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result <= 0)  //If the user name is not match with password
                    {
                        // Connect the Mysql Database and verificate the password by Email
                        string sql1 = string.Format("select count(*) from Administrator where (Email = '{0}' and Password = md5('{1}'))", txtName.Text, txtPassword.Text);
                        MySqlCommand cmd1 = new MySqlCommand(sql1, conn);  // Execute the query command
                        // Determine the correctness of passwoed with Email
                        result = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (result <= 0)  //If the Email is not match with password
                        {
                            // Connect the Mysql Database and verificate the password by phone number
                            string sql2 = string.Format("select count(*) from Administrator where (TelNum = '{0}' and Password = md5('{1}'))", txtName.Text, txtPassword.Text);
                            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);   // Execute the query command
                            // Determine the correctness of passwoed with phone number
                            result = Convert.ToInt32(cmd2.ExecuteScalar());
                            if (result <= 0)  //If the phone number is not match with password
                            {
                                GenCode();  // Change a new code
                                // MessageBox.Show("Please Enter the Correct Account Number or Password !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (result > 0)   // Password is matched
                            {
                                key = 1;   // return Key
                            }
                        }
                        else if (result > 0)
                        {
                            key = 1;
                        }
                    }
                    else if (result > 0)
                    {
                        key = 1;
                    }

                    // The account ID and password is correct and matched
                    if (key > 0)
                    {
                        // MessageBox.Show("Login successfully, please wait while the system is opening...", "Succseefully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();   // Close the currect form
                        Loading();
                        MainForm mainForm = new MainForm();  // OPen the main page
                        mainForm.Show();
                        LoadingCompleted();
                    }
                    else  // The account ID and password is not correct and matched
                    {
                        GenCode(); // Change the code
                        MessageBox.Show("Please Enter the Correct Account Number or Password !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Claer the ID, Password and Code textBox
                        txtNumber.Text = "";
                        txtName.Focus();
                        txtName.Text = "";
                        txtPassword.Text = "";
                        lblName.Visible = true;
                        lblPassword.Visible = true;
                        lblNumber.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    CloseDB();  // Close Connection
                }
            }
            else if(ckbTrainer.Checked == true)   // Trainer Account Log in
            {
                try
                {
                    ConnDB();
                    int key = 0;    // Read the number of records that meet the condition
                    // Connect the Mysql Database and verificate the password by User Name
                    string sql = string.Format("select count(*) from Trainer where (Name = '{0}'and Number = '{1}')", txtName.Text, txtPassword.Text);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);   // Execute the query command
                    // Determine the correctness of passwoed with User Name
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result <= 0)  //If the user name is not match with password
                    {
                        // Connect the Mysql Database and verificate the password by Email
                        string sql1 = string.Format("select count(*) from Trainer where (Email = '{0}' and Number = '{1}')", txtName.Text, txtPassword.Text);
                        MySqlCommand cmd1 = new MySqlCommand(sql1, conn);  // Execute the query command
                        // Determine the correctness of passwoed with Email
                        result = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (result <= 0)  //If the Email is not match with password
                        {
                            // Connect the Mysql Database and verificate the password by phone number
                            string sql2 = string.Format("select count(*) from Trainer where (TelNum = '{0}' and Number = '{1}')", txtName.Text, txtPassword.Text);
                            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);   // Execute the query command
                            // Determine the correctness of passwoed with phone number
                            result = Convert.ToInt32(cmd2.ExecuteScalar());
                            if (result <= 0)  //If the phone number is not match with password
                            {
                                GenCode();  // Change a new code
                                // MessageBox.Show("Please Enter the Correct Account Number or Password !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (result > 0)   // Password is matched
                            {
                                key = 1;   // return Key
                            }
                        }
                        else if (result > 0)
                        {
                            key = 1;
                        }
                    }
                    else if (result > 0)
                    {
                        key = 1;
                    }

                    // The account ID and password is correct and matched
                    if (key > 0)
                    {
                        // MessageBox.Show("Login successfully, please wait while the system is opening...", "Succseefully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();   // Close the currect form
                        Trainer_Form trainer = new Trainer_Form();
                        trainer.Trainer_Info = txtName.Text;
                        trainer.Show();
                    }
                    else  // The account ID and password is not correct and matched
                    {
                        GenCode(); // Change the code
                        MessageBox.Show("Please Enter the Correct Account Number or Password !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Claer the ID, Password and Code textBox
                        txtNumber.Text = "";
                        txtName.Focus();
                        txtName.Text = "";
                        txtPassword.Text = "";
                        lblName.Visible = true;
                        lblPassword.Visible = true;
                        lblNumber.Visible = true;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                     CloseDB();
                }
            }
            else if(ckbMember.Checked == true)  // Member Account Login
            {
                try
                {
                    ConnDB();
                    int key = 0;    // Read the number of records that meet the condition
                    // Connect the Mysql Database and verificate the password by User Name
                    string sql = string.Format("select count(*) from Member where (Name = '{0}'and Number = '{1}')", txtName.Text, txtPassword.Text);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);   // Execute the query command
                    // Determine the correctness of passwoed with User Name
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result <= 0)  //If the user name is not match with password
                    {
                        // Connect the Mysql Database and verificate the password by Email
                        string sql1 = string.Format("select count(*) from Member where (Email = '{0}' and Number = '{1}')", txtName.Text, txtPassword.Text);
                        MySqlCommand cmd1 = new MySqlCommand(sql1, conn);  // Execute the query command
                        // Determine the correctness of passwoed with Email
                        result = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (result <= 0)  //If the Email is not match with password
                        {
                            // Connect the Mysql Database and verificate the password by phone number
                            string sql2 = string.Format("select count(*) from Member where (TelNum = '{0}' and Number = '{1}')", txtName.Text, txtPassword.Text);
                            MySqlCommand cmd2 = new MySqlCommand(sql2, conn);   // Execute the query command
                            // Determine the correctness of passwoed with phone number
                            result = Convert.ToInt32(cmd2.ExecuteScalar());
                            if (result <= 0)  //If the phone number is not match with password
                            {
                                GenCode();  // Change a new code
                                // MessageBox.Show("Please Enter the Correct Account Number or Password !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (result > 0)   // Password is matched
                            {
                                key = 1;   // return Key
                            }
                        }
                        else if (result > 0)
                        {
                            key = 1;
                        }
                    }
                    else if (result > 0)
                    {
                        key = 1;
                    }

                    // The account ID and password is correct and matched
                    if (key > 0)
                    {
                        // MessageBox.Show("Login successfully, please wait while the system is opening...", "Succseefully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();   // Close the currect form
                        Member_Form member = new Member_Form();
                        member.member_Info = txtName.Text;
                        member.member_Number = txtPassword.Text;
                        member.Show();
                    }
                    else  // The account ID and password is not correct and matched
                    {
                        GenCode(); // Change the code
                        MessageBox.Show("Please Enter the Correct Account Number or Password !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Claer the ID, Password and Code textBox
                        txtNumber.Text = "";
                        txtName.Focus();
                        txtName.Text = "";
                        txtPassword.Text = "";
                        lblName.Visible = true;
                        lblPassword.Visible = true;
                        lblNumber.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    CloseDB();
                }
            }
        }

        private void lblName_MouseDown(object sender, MouseEventArgs e)
        {
            lblName.Visible = false;  // This label is disappear
            txtName.Focus();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            // Exit out of focus and reappear
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblName.Visible = true;  // This label is show
            }
        }

        private void lblPassword_MouseDown(object sender, MouseEventArgs e)
        {
            lblPassword.Visible = false;  // This label is disappear
            txtPassword.Focus(); 
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            // Exit out of focus and reappear
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblPassword.Visible = true;  // This label is show
            }
        }

        private void lblNumber_MouseDown(object sender, MouseEventArgs e)
        {
            lblNumber.Visible = false;  // This label is disappear
            txtNumber.Focus();
        }

        private void txtNumber_Leave(object sender, EventArgs e)
        {
            // Exit out of focus and reappear
            if (string.IsNullOrEmpty(txtNumber.Text))
            {
                lblNumber.Visible = true;  // This label is show
            }
        }

        // Change the register label style when the mouse Hover on label
        private void lblRegistration_MouseHover(object sender, EventArgs e)
        {
            this.lblRegistration.Font = new System.Drawing.Font("Times New Roman", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold |
    System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        // Change the register label style when the mouse leave label
        private void lblRegistration_MouseLeave(object sender, EventArgs e)
        {
            this.lblRegistration.Font = new System.Drawing.Font("Times New Roman", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold |
    System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void lblRegistration_Click(object sender, EventArgs e)
        {
            // Open the registeration form
            Registration_Page frmRegister = new Registration_Page(); 
            frmRegister.Show();
            this.Hide(); // The current form is hide
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))  // Limit enter 
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)  // Enter key Evennt
            {
                btnLogin_Click(sender, e);  // Click the login button 
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Forbidden to input Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5)) 
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)  // Enter key Evennt
            {
                txtNumber.Focus(); 
                lblNumber.Visible = false;
            }
        }

        // User ID textBox input change
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if(txtName.Text != "")
            {
                // The warning icon is disappear
                picNameError.Visible = false;
            }
        }

        // Password textBox input change
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != "")
            {
                // The warning icon is disappear
                picPaswError.Visible = false;
            }
        }

        // Code textBox input change
        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtNumber.Text == code)
            {
                // The warning icon is disappear
                picNumberError.Visible = false;
            }
            else
            {
                picNumberError.Visible = true;
            }
        }

        // Set the login indenfication Admin or Trainer
        private void ckbAdmin_CheckedChangeEvent(object sender, EventArgs e)
        {
            ckbTrainer.Checked = false;
            ckbMember.Checked = false;
        }

        private void ckbTrainer_CheckedChangeEvent(object sender, EventArgs e)
        {
            ckbAdmin.Checked = false;
            ckbMember.Checked = false;
        }

        private void ckbMember_CheckedChangeEvent(object sender, EventArgs e)
        {
            ckbAdmin.Checked = false;
            ckbTrainer.Checked = false;
        }
    }
}
