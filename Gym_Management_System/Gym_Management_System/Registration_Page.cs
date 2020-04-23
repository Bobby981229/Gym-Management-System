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
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace Gym_Management_System
{
    public partial class Registration_Page : Form
    {
        //SignInInterface Mysql DataBase Connection
        private const string dbServer = "server=127.0.0.1;port=3306;database=gym;user=root;password=LDF8705012";
        MySqlConnection conn;

        // Create a connection
        private void ConnDB()
        {
            // server address;The port number.Database;The user name;password
            string connectStr = dbServer; // username and password defined in MySQL
                                          // create a connection
            conn = new MySqlConnection(connectStr);
            // Open the connection
            try
            {
                conn.Open();//Mysql database did not start, this sentence error!
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }// Connect to database

        // Close the database
        private void CloseDB()
        {
            // close connection
            conn.Close();
        }

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

        public Registration_Page()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Registration_Page_Load(object sender, EventArgs e)
        {
            // Windows form starts style
            Win32.AnimateWindow(this.Handle, 800, Win32.AW_BLEND | Win32.AW_ACTIVATE);
            //Connect the Mysql Database
            ConnDB();
            CloseDB();

            // Set the background color is transparent
            picLogo.Parent = picBackGround;
            lblTitle.Parent = picBackGround;
            lblTip.Parent = picBackGround;
            picUser.Parent = picBackGround;
            picTel.Parent = picBackGround;
            picEmail.Parent = picBackGround;
            picPsw.Parent = picBackGround;
            picConPsw.Parent = picBackGround;
            picTelError.Parent = picBackGround;
            picemailError.Parent = picBackGround;
            picPassword.Parent = picBackGround;
            picNameError.Parent = picBackGround;
            picConfirmPasw.Parent = picBackGround;
            lblName.Focus();
        }

        private void lblName_MouseDown(object sender, MouseEventArgs e)
        {
            lblName.Visible = false;  // This label is disappear
            txtName.Focus();
        }

        private void lblPhone_MouseDown(object sender, MouseEventArgs e)
        {
            lblPhone.Visible = false;  // This label is disappear
            txtTelephone.Focus();
        }

        private void lblEmail_MouseDown(object sender, MouseEventArgs e)
        {
            lblEmail.Visible = false;  // This label is disappear
            txtEmail.Focus();
        }

        private void lblPassword_MouseDown(object sender, MouseEventArgs e)
        {
            lblPassword.Visible = false;  // This label is disappear
            txtPassword.Focus();
        }

        private void lblConfirmPsw_MouseDown(object sender, MouseEventArgs e)
        {
            lblConfirmPsw.Visible = false;  // This label is disappear
            txtEnsurePsw.Focus();
        }

        /// <summary>
        /// Register a new administrator account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Name can't be null
            if (txtName.Text == "")
            {
                picNameError.Visible = true;
            }

            // Telephone  can't be null
            if (txtTelephone.Text == "")
            {
                picTelError.Visible = true;
            }

            // E-mail can't be null
            if (txtEmail.Text == "")
            {
                picemailError.Visible = true;
            }

            // Password can't be null
            if (txtPassword.Text == "")
            {
                picPassword.Visible = true;
            }
             // Confirm Password can not be null
            if (txtEnsurePsw.Text == "")
            {
                picConfirmPasw.Visible = true;
            }

            // The warning icons are showing the Mysql command can not be executed
            if ((picNameError.Visible == true) || (picTelError.Visible == true) || (picemailError.Visible == true) || (picPassword.Visible == true) || (picConfirmPasw.Visible == true))
            {
                MessageBox.Show("Please confirm that the personal information \n is complete and accurate !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((txtName.Text == "") || (txtTelephone.Text == "") || (txtEmail.Text == "") || (txtPassword.Text == "") || (txtEnsurePsw.Text == ""))  // The text box can not be empty !
            {
                MessageBox.Show("Personal information cannot be empty !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ConnDB();  // Connect the Database
                
                // Mysql query command...........And the password is enciphered ！
                string strsql = string.Format("Insert into Administrator(Name, TelNum, Email, Password) values ('{0}', '{1}', '{2}', md5('{3}'))", txtName.Text, txtTelephone.Text, txtEmail.Text, txtPassword.Text);
                
                // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                MySqlCommand cmd = new MySqlCommand(strsql, conn);
                try
                {   // Execute the SQL command
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registered Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblTip.Text = "Waiting to jump to the login page !";
                    // The current page is hide 
                    this.Hide();
                    // Open the log in page 
                    Login_Page frmLogin = new Login_Page();
                    frmLogin.Show(); 
                }
                catch
                {
                    MessageBox.Show("The Administrator Already Exists !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Focus();// Set the cursor here
                }
                conn.Close();
            }          
        }

        // When mouse leave the textbox
        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblName.Visible = true;  // This label is show
            }
        }

        // When mouse leave the textbox
        private void txtTelephone_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTelephone.Text))
            {
                lblPhone.Visible = true;  // This label is show
            }
        }

        // When mouse leave the textbox
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblEmail.Visible = true;  // This label is show
            }
        }

        // When mouse leave the textbox
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblPassword.Visible = true;  // This label is show
            }
        }

        // When mouse leave the textbox
        private void txtConfirmPsw_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEnsurePsw.Text))
            {
                lblConfirmPsw.Visible = true;  // This label is show
            }
        }


        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can input English only
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == '\b')  
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            // Enter Key Event
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtTelephone.Focus(); // Set the cursor here
                lblPhone.Visible = false;
            }
        }

        private void txtTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only Numbers can be entered
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                txtEmail.Focus(); // Set the cursor here
                lblEmail.Visible = false;
            }
        }


        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Do not enter Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                txtEnsurePsw.Focus(); // Set the cursor here

                lblConfirmPsw.Visible = false;
            }
        }

        // Judge the telephone number format whether is correct 
        private void txtTelephone_TextChanged(object sender, EventArgs e)
        {
            string phone = txtTelephone.Text;

            // Telecom phone number regular expression
            string dianxin = @"^1[3578][01379]\d{8}$";
            Regex dReg = new Regex(dianxin);

            // China unicom mobile phone number regular expression      
            string liantong = @"^1[34578][01256]\d{8}$";
            Regex tReg = new Regex(liantong);

            // China Mobile phone number regular expression
            string yidong = @"^(134[012345678]\d{7}|1[34578][012356789]\d{8})$";
            Regex yReg = new Regex(yidong);

            // If the regular expression is match the input telephone
            if (dReg.IsMatch(phone) || tReg.IsMatch(phone) || yReg.IsMatch(phone))
            {
                // The error icon disappear
                picTelError.Visible = false;  
            }
            else
            {
                picTelError.Visible = true;
            }

            if (txtTelephone.Text == "")
            {
                picTelError.Visible = false;
            }
        }

        // Judge the Email format whether is correct 
        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string Eamil = txtEmail.Text;
            // Mailbox regular expression
            string regEmai = "\\w{1,}@\\w{1,}\\.com";
            Regex regex = new Regex(regEmai);

            // Mailbox filling conforms to the regular expression
            if (regex.IsMatch(Eamil))
            {
                // The error icon disappear
                picemailError.Visible = false;  
            }
            else
            {
                picemailError.Visible = true;
            }

            if (txtEmail.Text == "")
            {
                // The error icon disappear
                picemailError.Visible = false;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Do not enter Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5)) 
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                // Set the cursor here
                txtPassword.Focus(); 
                lblPassword.Visible = false;
            }
        }

        // Password regular expression
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            /*The password must contain at least one uppercase letter, one lowercase letter, and one number.
             * And the length of the password should be between 6 and 16 bits*/
            string paswRegex = "^(?:(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])).{6,16}$";
            Regex regex = new Regex(paswRegex);

            // The regular expression is match with ehtered password
            if (regex.IsMatch(password))  
            {
                picPassword.Visible = false;
            }
            else
            {
                picPassword.Visible = true;
            }

            if (txtPassword.Text == "")
            {
                picPassword.Visible = false;
            }
        }

        private void txtConfirmPsw_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Do not enter Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                // Set the cursor here
                btnRegister.Focus(); 
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                // Sender the click register button event
                btnRegister_Click(sender, e);
            }
        }

        // Confirm the inputed password
        private void txtConfirmPsw_TextChanged(object sender, EventArgs e)
        {
            // Confirm the password if it's correct ?
            if(txtEnsurePsw.Text == txtPassword.Text)
            {
                picConfirmPasw.Visible = false;
            }
            else
            {
                picConfirmPasw.Visible = true;
            }

            if (txtEnsurePsw.Text == "")
            {
                picConfirmPasw.Visible = false;
            }
        }

        private void txtUser_MouseDown(object sender, MouseEventArgs e)
        {
            lblName.Visible = false; // This label is disappear
            txtName.Focus();
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            if(txtName.Text != "")
            {
                picNameError.Visible = false;
            }
        }

        // Jump to login page
        private void picLogo_Click(object sender, EventArgs e)
        {
            Login_Page frmLogin = new Login_Page();
            frmLogin.Show();
            Hide();
        }
    }
}
