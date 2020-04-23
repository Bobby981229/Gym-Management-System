using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using MySql.Data.MySqlClient;

namespace Gym_Management_System
{
    public partial class MainForm : Form
    {
        #region Connect_Database
        MySqlDataAdapter ad;
        DataSet ds;
        //SignInInterface Mysql DataBase Connection
        private const string dbServer = "server=127.0.0.1;port=3306;database=gym;user=root;password=LDF8705012";
        MySqlConnection conn;

        // Create a connection
        private void ConnDB()
        {
            // username and password defined in MySQL
            string connectStr = dbServer;
           
            // create a connection
            conn = new MySqlConnection(connectStr);
            // Open the connection
            try
            {
                conn.Open();//Mysql database did not start, this sentence error!
            }
            catch
            {
                lblTip.Text = "No Connection Established!";
            }
        }

        // Close the database
        private void CloseDB()
        {
            // Close connection
            conn.Close();
        }
        #endregion

        public MainForm()
        {
            InitializeComponent();
            Win32.AnimateWindow(this.Handle, 2000, Win32.AW_BLEND);
            // Set the maxsize of screen
            Size size = new Size(1560, 830); 
            this.Size = size;
            // Set the size of background image
            this.picBackground.Size = new System.Drawing.Size(1560, 830);

            //Timing task routine
            Task.Run(() =>
            {
                while (true)
                {
                    //Sleep every 50 seconds
                    Thread.Sleep(50000);

                    FixedTimeBackupDB();

                }
            });
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            // Open the timer
            timer1.Start();  
            // Set the unit of timer
            timer1.Interval = 1000;
            // Set the background color is transparent
            lblTitle.Parent = picBackground;
            picLogo.Parent = picBackground;
            menuBar.Parent = picBackground;
            DateNavi.Parent = picBackground;
            ucledTime1.Parent = picBackground;
            groBoxAdmini.Parent = picBackground;
            groBoxCou.Parent = picBackground;
            //内向外扩展特效            
        }

        // Bind the ComboBox data source -- Course Name
        private void Course_Nmae()
        {
            // Connect the database
            ConnDB();
            // Create an ArrayList instance
            ArrayList list = new ArrayList();
            // Search all course name in Course Table
            string str = "Select Course_Name from Course";
            // Create an Data Adpter Object
            MySqlDataAdapter da = new MySqlDataAdapter(str, conn);
            // Set up DataSet objects (equivalent to setting up a virtual database in the foreground)
            DataSet ds = new DataSet();
            // The result of the query is stored in the virtual table
            da.Fill(ds);
            // Create a DataTable Object
            DataTable dt = ds.Tables[0];
            // Step by step, pull out the data of each row
            foreach (DataRow dr in dt.Rows)
            {
                // The data from the query is iterated into the array
                list.Add(dr[0].ToString().Trim());
            }
            // Put list data source the Course _Name comboBox
            comCouName.DataSource = list;
            // Close the connect
            conn.Close();
        }


        // Bind ComboBox data source -- Trainer
        private void Trainer_Nmae()
        {
            ConnDB();

            // Create an ArrayList instance
            ArrayList list = new ArrayList();

            // Search all trainer name in Trainer Table
            string str = "Select Name from Trainer";

            // Create an Data Adpter Object
            MySqlDataAdapter da = new MySqlDataAdapter(str, conn);

            // Set up DataSet objects (equivalent to setting up a virtual database in the foreground)
            DataSet ds = new DataSet();

            // The result of the query is stored in the virtual table
            da.Fill(ds);

            // Create a DataTable Object
            DataTable dt = ds.Tables[0];

            // Step by step, pull out the data of each row
            foreach (DataRow dr in dt.Rows)
            {
                // The data from the query is iterated into the array
                list.Add(dr[0].ToString().Trim());
            } 
            // Put list data source the Trainer Name comboBox
            comboTraName.DataSource = list;
            conn.Close();
        }      

        // Set the time tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Get the current date and time, display the concrete time
            lblItemTitle.Text = "Hello Administrator !   Welcome to the Gym Management System !";
            lblItemTimes.Text = "The Current Time: " + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
            ucledTime1.Value = DateTime.Now;
        }

        // Page Jumps
        private void navAddMe_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            // Open the Add  Member Page and set the visible of some controls
            Member_Ad member = new Member_Ad();
            member.groboTraInfor.Visible = false;
            member.lblTraTitle.Visible = false;
            member.Show();
        }

        // Page Jump
        private void navMutiMedia_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            // Open the Muti_Media Page
            MutiMedia_Form mutiMedia_Form = new MutiMedia_Form();
            mutiMedia_Form.Show();
        }

        // Page Jump
        private void navAccessSys_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            // Open the Access control management system page
            FaceForm faceForm = new FaceForm();
            faceForm.Show();
        }

        // Page Jump
        private void navAddTrainer_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            // Open new Trainer Add Form
            Member_Ad member = new Member_Ad();
            member.groboMeInfor.Visible = false;
            member.lblMeTitle.Visible = false;
            member.Show();
        }

        // Page Jump
        private void navMeInforMa_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            // Open Member Information Management page and set the attribute of some controls 
            Information_Management infoManage = new Information_Management();
            infoManage.groboTraInfor.Enabled = false;
            infoManage.groboTraInfor.Visible = false;
            infoManage.comBoxItems.Text = "Member";
            infoManage.Show();
        }

        // Page Jump
        private void navTraInfoMa_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            // Open Trainer Information Management page and set the attribute of some controls 
            Information_Management infoManage = new Information_Management();
            infoManage.groboMeInfor.Enabled = false;
            infoManage.groboMeInfor.Visible = false;
            infoManage.picDeadline.Visible = false;
            infoManage.comBoxItems.Text = "Trainer";
            infoManage.Show();
        }

        // Page Jump
        private void navAdminiMa_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            // The Administration Management -- gropbox is display
            groBoxAdmini.Visible = true;
            // Course Register grobox is not display
            groBoxCou.Visible = false;
            panel1.Visible = false;
            ConnDB();
            conn.Close();
        }

        // Control availability switch
        private void switchEdit_CheckedChanged(object sender, EventArgs e)
        {
            // The switch is close -- The controls can not use or input
            if (switchEdit.Checked == false)
            {
                txtEmail.ReadOnly = true;
                txtTel.ReadOnly = true;
                txtPassword.ReadOnly = true;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
            }  // The switch is open -- The controls can use or input
            else if (switchEdit.Checked == true)
            {
                txtEmail.ReadOnly = false;
                txtTel.ReadOnly = false;
                txtPassword.ReadOnly = false;
                btnDelete.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Administration GropBox's visibleis not show
            groBoxAdmini.Visible = false;
        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can only enter Numbers
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Enter Key Event
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtEmail.Focus();
            }
        }

        // Judge the telephone number format whether is correct 
        private void txtTel_TextChanged(object sender, EventArgs e)
        {
            string phone = txtTel.Text;

            // Telecom phone number regular expression
            string dianxin = @"^1[3578][01379]\d{8}$";
            Regex dReg = new Regex(dianxin);

            //China unicom mobile phone number regular expression  
            string liantong = @"^1[34578][01256]\d{8}$";
            Regex tReg = new Regex(liantong);

            //China Mobile phone number regular expression
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

            if (txtTel.Text == "")
            {
                // The error icon disappear
                picTelError.Visible = false;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can not enter Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5)) 
            {
                e.Handled = true;
            }

            // Enter Key Event
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
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

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can not enter Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5))
            {
                e.Handled = true;
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

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can not enter Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5))
            {
                e.Handled = true;
            }

            // Enter Key Event
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Start search button click event
                picSearchNum_Click(sender, e);
                txtSearch.Focus();
            }
        }

        /// <summary>
        /// Search Admin info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picSearchNum_Click(object sender, EventArgs e)
        {
            // Search contents can't  null
            if (txtSearch.Text == "")
            {
                MessageBox.Show("The search content cannot be empty !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ConnDB();  // Connect the Mysql Database
                bool flag;  // Judge the DataSet if is null
                ds = new DataSet();  // New a DataSet and pull the data into buffer for sql command
                // Write the mysql statement for the search and assign the value
                string sql = "Select * from Administrator where Name ='" + txtSearch.Text + "'";                  

                DataTable dt = new DataTable();  // Create a new DataTable to show the detail information
                ad = new MySqlDataAdapter(sql, conn);  // Add a Data_adapter for the data by sql command.... In order to display data on the form

                ad.Fill(dt); // Add card_order information into DataTable
                ad.Fill(ds); // Add card_order information into DataSet

                if (ds != null && ds.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                {
                    flag = true;   // Search successfully !
                }
                else
                {
                    string sql1 = "Select * from Administrator where TelNum ='" + txtSearch.Text + "'";
                    ad = new MySqlDataAdapter(sql1, conn);     // Add a Data_adapter for the data by sql7 command
                    ad.Fill(dt);   // Fill Data_adapter from datatable
                    ad.Fill(ds);   // Fill Data_adapter from buffer - DataSet -- Trainer Form
                    if (ds != null && ds.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                    {
                        flag = true;   // Search successfully !
                    }
                    else
                    {
                        string sql2 = "Select * from Administrator where Email ='" + txtSearch.Text + "'";
                        ad = new MySqlDataAdapter(sql2, conn);   // Add a Data_adapter for the data by sql8 command
                        ad.Fill(dt);   // Fill Data_adapter from datatable
                        ad.Fill(ds);   // Fill Data_adapter from buffer - DataSet -- Trainer Form
                        if (ds != null && ds.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                        {
                            flag = true;   // Search successfully !
                        }
                        else
                        {
                            flag = false; // Search unsuccessfully !
                        }
                    }
                }

                if (flag == true)  // This infor can be searched
                {
                    try
                    {
                        // Display the searching data on the DataGridView
                        DataGridViewAdmin.DataSource = dt;
                        // Assigns the [] value of row 0 of the 0th table in the cache                  
                        txtName.Text = ds.Tables[0].Rows[0][0].ToString();
                        txtTel.Text = ds.Tables[0].Rows[0][1].ToString();
                        txtEmail.Text = ds.Tables[0].Rows[0][2].ToString();
                        txtPassword.Text = ds.Tables[0].Rows[0][3].ToString();
                        lblTip.Text = "";
                    }
                    catch
                    {
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("The information is nonexistent ! \n Please try it again !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtSearch.Text = "";
                // picPassword.Visible = false;
                txtSearch.Focus();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            // When name textbox is not null, the error icon can't appear
            if (txtName.Text != "")
            {
                picNameError.Visible = false;
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // The name textbox can only enter English
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
                txtTel.Focus();
            }
        }

        /// <summary>
        /// Update Admin Account Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ConnDB();

            // The Name, teleophone number, Email, Password textbox are empty, error icon will appear
            if (txtName.Text == "")
            {
                picNameError.Visible = true;
            }

            if (txtTel.Text == "")
            {
                picTelError.Visible = true;
            }

            if (txtEmail.Text == "")
            {
                picemailError.Visible = true;
            }

            if (txtPassword.Text == "")
            {
                picPassword.Visible = true;
            }

            // When the error icons are showing, the query command can not to carry out
            if ((picNameError.Visible == true) || (picTelError.Visible == true) || (picemailError.Visible == true) || (picPassword.Visible == true))
            {
                lblTip.Text = "Please confirm that the personal information \n is complete and accurate ！";
            }   // The information can not be empty
            else if ((txtName.Text == "") || (txtTel.Text == "") || (txtEmail.Text == "") || (txtPassword.Text == "")) 
            {
                lblTip.Text = "Personal information cannot be empty ！";
            }
            else
            { 
                // Update administrator information sql command, and password encryption
                string query = string.Format("Update Administrator set TelNum = ('{0}'), Email = ('{1}'), Password = md5('{2}') where Name = ('{3}')", txtTel.Text.Trim(), txtEmail.Text.Trim(), txtPassword.Text.Trim(), txtName.Text.Trim());
                try
                {
                    // Connect and call the database for update
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Executive command
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                // The new information will show in the DataGridView control
                BindDataGrid();
                conn.Close();              
            }
        } 

        // Refresh the page, and clear the controls
        private void picRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtName.Text = "";
            txtTel.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            lblTip.Text = "";
            DataGridViewAdmin.DataSource = null;
            txtSearch.Focus();
        }

        // Displays information on the control of DataGridView
        private void BindDataGrid()
        {
            ConnDB();

            // Create an instance of data.datatable
            System.Data.DataTable dt = new DataTable();

            // Create a sql query command 
            string sql = "Select * from Administrator";

            // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            // Add a Data_adapter for the data by sql command.... In order to display data on the form
            MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
            try
            {
                // Add the data Source in to table buff
                ad.Fill(dt); 
                // Put the data source into DataGridView from buff
                DataGridViewAdmin.DataSource = dt;  
            }
            catch
            {
                MessageBox.Show("Mysql Error !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        // Display all the Administrators' information on DataGridView
        private void picShowAll_Click(object sender, EventArgs e)
        {
            // Calling method
            BindDataGrid();
        }

        //Delete Admin Account Info
        private void btnDelete_Click(object sender, EventArgs e)
        {
            ConnDB();
            //Gets the number of rows selected
            int row = DataGridViewAdmin.SelectedRows.Count;

            //Both do not exist at the same time, unselected and searched.
            if ((row <= 0) && (txtName.Text == ""))
            {
                picRefresh_Click(sender, e);
                MessageBox.Show("There is no data to be deleted !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if ((row > 0) && (txtName.Text == ""))  // The information in the table is selected, but not searched
            {
                // A prompt box pops up asking whether to continue deleting
                DialogResult result = MessageBox.Show("Confirm to delete this data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Confirm to delete this data
                if (result == DialogResult.Yes)
                {
                    for (int i = 0; i < row; i++)
                    {
                        // Locate the selected information in the table
                        string dgNum = DataGridViewAdmin.SelectedRows[i].Cells[0].Value.ToString();
                        // Craete the sql command, and the user name is key
                        string delstr = "Delete from Administrator where Name=" + "'" + dgNum + "'";
                        // Represents a transact-sql statement or stored procedure to execute against the SQL Server database                    
                        try
                        {
                            MySqlCommand delecmd = new MySqlCommand(delstr, conn);
                            delecmd.ExecuteNonQuery();  // Execute query command
                            MessageBox.Show("Administrator Account Delete Succseefully !", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    // The new information will show in the DataGridView control
                    BindDataGrid();
                    conn.Close();
                }
            }
            else if ((row <= 0) && (txtName.Text != ""))    // The information was searched but not selected
            {
                // Comfirm if wanna delete this data
                DialogResult result = MessageBox.Show("Confirm to delete this data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Confirm to delete this data
                if (result == DialogResult.Yes)
                {
                    // The DataGridView is not exist
                    if (DataGridViewAdmin.Rows.Count == 1)
                    {
                        MessageBox.Show("The entry " + txtSearch.Text + " is not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSearch.Text = "";
                    }
                    else
                    {
                        ConnDB();
                        // Create the query sql command
                        string strsql = "Delete from Administrator where Name ='" + txtName.Text + "'";
                        // Represents a transact-sql statement or stored procedure to execute against the SQL Server database                       
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(strsql, conn);
                            cmd.ExecuteNonQuery(); // Execute query command
                            MessageBox.Show("Administrator Account Delete Succseefully !", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            picRefresh_Click(sender, e);  // Refresh the page
                        }
                        catch
                        {
                            MessageBox.Show("The " + txtSearch.Text + " entry is not exists! Retry!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        // The new information will show in the DataGridView control
                        BindDataGrid();
                        conn.Close();
                    }
                }
            }
            else    // Both of two condition is true
            {
                MessageBox.Show("Cannot delete two data at the same time !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();            
        }

        // Page Jump
        private void navCourse_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            // The course management page is show 
            Course_Management course_Management = new Course_Management();
            course_Management.Show();
        }

        // Page Jump
        private void navStatistical_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            // The report__Statistics page is show 
            Report__Statistics report__Statistics = new Report__Statistics();
            report__Statistics.Show();
                
        }

        // Page Jump
        private void navCourseRe_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            // Courese Register page is show
            groBoxCou.Visible = true;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            txtMeName.Focus();
            ConnDB();
            conn.Close();
        }

        // Connect to the database to get the course name data information
        private void comCouName_Click(object sender, EventArgs e)
        {
            Course_Nmae();  // Calling  Course_Nmae() method
        }

        // Connect to the database to get the trainer name data information
        private void comboTraName_Click(object sender, EventArgs e)
        {
            Trainer_Nmae(); // Calling  Trainer_Nmae() method
        }


        private void txtMeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // This textbox can only input English
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            // Enter key event
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Clear the text content of controls
                picCouSearch_Click(sender, e);
            }
        }

         /// <summary>
         /// Query Course Order
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void picCouSearch_Click(object sender, EventArgs e)
        {
            // The course information can not be empty
            if (txtMeName.Text == "" && comboTraName.Text == "" && comCouName.Text == "")
            {
                MessageBox.Show("Please enter the query contents !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtMeName.Text == "" && comboTraName.Text == "" && comCouName.Text != "")
            {
                ConnDB();  // Connect the Mysql Database
                DataSet dsCN = new DataSet();  // New a DataSet and pull the data into buffer for sql command
                DataTable dtCN = new DataTable();  // Create a new DataTable to show the detail information
                string sqlCN = "Select * from Course_Order where Course_Name ='" + comCouName.Text + "'";
                MySqlDataAdapter adCN = new MySqlDataAdapter(sqlCN, conn);   // Add a Data_adapter for the data by sql command.... In order to display data on the form
                adCN.Fill(dtCN); // Add card_order information into DataTable
                adCN.Fill(dsCN); // Add card_order information into DataSet
                if (dsCN != null && dsCN.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                {
                    try
                    {
                        // Display the searching data on the DataGridView
                        DataGridView1.DataSource = dtCN;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("The information does not exist !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (txtMeName.Text == "" && comboTraName.Text != "" && comCouName.Text == "")  // Trainer Name
            {
                ConnDB();  // Connect the Mysql Database
                DataSet dsTra = new DataSet();  // New a DataSet and pull the data into buffer for sql command
                DataTable dtTra = new DataTable();  // Create a new DataTable to show the detail information
                string sqlTra = "Select * from Course_Order where Trainer_Name ='" + comboTraName.Text + "'";
                MySqlDataAdapter adTra = new MySqlDataAdapter(sqlTra, conn);   // Add a Data_adapter for the data by sql command.... In order to display data on the form
                adTra.Fill(dtTra); // Add card_order information into DataTable
                adTra.Fill(dsTra); // Add card_order information into DataSet
                if (dsTra != null && dsTra.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                {
                    try
                    {
                        // Display the searching data on the DataGridView
                        DataGridView1.DataSource = dtTra;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("The information does not exit !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (txtMeName.Text != "" && comboTraName.Text == "" && comCouName.Text == "")  // Member Name
            {
                ConnDB();  // Connect the Mysql Database
                DataSet dsCou = new DataSet();  // New a DataSet and pull the data into buffer for sql command
                DataTable dtCou = new DataTable();  // Create a new DataTable to show the detail information
                string sqlMe = "Select * from Course_Order where Member_Name ='" + txtMeName.Text + "'";
                MySqlDataAdapter adCou = new MySqlDataAdapter(sqlMe, conn);   // Add a Data_adapter for the data by sql command.... In order to display data on the form
                adCou.Fill(dtCou); // Add card_order information into DataTable
                adCou.Fill(dsCou); // Add card_order information into DataSet
                if (dsCou != null && dsCou.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                {
                    try
                    {
                        // Display the searching data on the DataGridView
                        DataGridView1.DataSource = dtCou;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("The information does not exit !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if ((txtMeName.Text != "") && (comCouName.Text == "") && (comboTraName.Text != ""))  // Search by Member Name and Course Name
            {
                ConnDB();  // Connect the Mysql Database
                DataSet dsCT = new DataSet();  // New a DataSet and pull the data into buffer for sql command
                DataTable dtCT = new DataTable();  // Create a new DataTable to show the detail information

                string sqlCT = "Select * from Course_Order where Member_Name='" + txtMeName.Text + "'";
                sqlCT += "And Trainer_Name='" + comboTraName.Text + "'";

                MySqlDataAdapter adCT = new MySqlDataAdapter(sqlCT, conn);   // Add a Data_adapter for the data by sql command.... In order to display data on the form
                adCT.Fill(dtCT); // Add card_order information into DataTable
                adCT.Fill(dsCT); // Add card_order information into DataSet
                if (dsCT != null && dsCT.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                {
                    try
                    {
                        // Display the searching data on the DataGridView
                        DataGridView1.DataSource = dtCT;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("The information does not exit !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if ((txtMeName.Text != "") && (comCouName.Text != "") && (comboTraName.Text == ""))  // Member Name and Course Name
            {
                ConnDB();  // Connect the Mysql Database
                DataSet dsMC = new DataSet();  // New a DataSet and pull the data into buffer for sql command
                DataTable dtMC = new DataTable();  // Create a new DataTable to show the detail information

                // Create the multi-conditional database query statement
                string sqlMC = "Select * from Course_Order where Member_Name='" + txtMeName.Text + "'";
                sqlMC += "And Course_Name='" + comCouName.Text + "'";

                MySqlDataAdapter adMC = new MySqlDataAdapter(sqlMC, conn);   // Add a Data_adapter for the data by sql command.... In order to display data on the form
                adMC.Fill(dtMC); // Add card_order information into DataTable
                adMC.Fill(dsMC); // Add card_order information into DataSet
                if (dsMC != null && dsMC.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                {
                    try
                    {
                        // Display the searching data on the DataGridView
                        DataGridView1.DataSource = dtMC;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("The information does not exit !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if ((txtMeName.Text != "") && (comboTraName.Text != "") && (comCouName.Text != ""))  // Total Query
            {
                ConnDB();  // Connect the Mysql Database
                DataSet dsMCT = new DataSet();  // New a DataSet and pull the data into buffer for sql command
                DataTable dtMCT = new DataTable();  // Create a new DataTable to show the detail information
                //string sqlMC = "Select * from Course_Order where (Member_Name ='" + txtMeName.Text + "')" + "( AND Course_Name ='" + comboxCoName.Text + "')";

                // Create the multi-conditional database query statement
                string sqlMCT = "Select * from Course_Order where Member_Name='" + txtMeName.Text + "'";
                sqlMCT += "And Course_Name='" + comCouName.Text + "'";
                sqlMCT += "And Trainer_Name='" + comboTraName.Text + "'";

                MySqlDataAdapter adMCT = new MySqlDataAdapter(sqlMCT, conn);   // Add a Data_adapter for the data by sql command.... In order to display data on the form
                adMCT.Fill(dtMCT); // Add card_order information into DataTable
                adMCT.Fill(dsMCT); // Add card_order information into DataSet
                if (dsMCT != null && dsMCT.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                {
                    try
                    {
                        // Display the searching data on the DataGridView
                        DataGridView1.DataSource = dtMCT;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("The information does not exit !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            txtMeName.Focus();
        }

        /// <summary>
        /// Course Registration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            ConnDB();
            // Gets the number of rows selected
            int rowRe = DataGridView1.SelectedRows.Count;

            // Both do not exist, not selected and not searched
            if (rowRe <= 0 && txtMeName.Text == "")
            {
                MessageBox.Show("There is no data to be registered !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                RefreshCourse();
            }
            else if (rowRe > 0 && txtMeName.Text != "")
            {
                MessageBox.Show("Cannot register for two courses at the same time !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (rowRe > 0 && txtMeName.Text == "")  // The information in the table is selected
            {
                // Loop through the number of rows in the table
                for (int i = 0; i < rowRe; i++)
                {
                    // Extract the information of the corresponding location
                    string dgRe = DataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                    string dgMe = DataGridView1.SelectedRows[i].Cells[1].Value.ToString();
                    string dgTra = DataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                    int dgTimes = int.Parse(DataGridView1.SelectedRows[i].Cells[4].Value.ToString());

                    if (dgTimes <= 0)
                    {
                        MessageBox.Show("The number of remaining courses is 0 !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Each registration reduces the number of classes by one
                        string Restr = string.Format("Update Course_Order set Times = (Times - 1) Where Course_Name = ('{0}') " +
                            "And Member_Name = ('{1}') And Trainer_Name = ('{2}')", dgRe, dgMe, dgTra);
                        // Represents a transact-sql statement or stored procedure to execute against the SQL Database
                        MySqlCommand Recmd = new MySqlCommand(Restr, conn);
                        try
                        {
                            Recmd.ExecuteNonQuery();
                            MessageBox.Show("Course Registration Successful ! ", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        DataSet ds1 = new DataSet();
                        string Ordersql1 = "Select * from Course_Order where Member_Name ='" + dgMe + "'";
                        MySqlDataAdapter ad1 = new MySqlDataAdapter(Ordersql1, conn);     // Add a Data_adapter for the data by Ordersql1 command
                        ad1.Fill(ds1);   // Fill Data_adapter from buffer - DataSet -- Trainer Form
                        txtTimes.Text = ds1.Tables[0].Rows[0][4].ToString();
                    }
                }
                conn.Close();
            }
            else if (rowRe <= 0 && txtMeName.Text != "" && comCouName.Text != "" && comboTraName.Text != "")
            {
                if (int.Parse(txtTimes.Text) <= 0)
                {
                    MessageBox.Show("The number of remaining courses is 0 !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Each registration reduces the number of classes by one
                    string Restrsql = string.Format("Update Course_Order set Times = (Times - 1) Where Course_Name = ('{0}') " +
                        "And Member_Name = ('{1}') And Trainer_Name = ('{2}')", comCouName.Text, txtMeName.Text, comboTraName.Text);
                    // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                    MySqlCommand Recmd1 = new MySqlCommand(Restrsql, conn);
                    try
                    {
                        Recmd1.ExecuteNonQuery();
                        picCouSearch_Click(sender, e);
                        MessageBox.Show("Course Registration Successful ! ", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    DataSet ds2 = new DataSet();
                    string Ordersql2 = "Select * from Course_Order where Member_Name ='" + txtMeName.Text + "'";
                    MySqlDataAdapter ad2 = new MySqlDataAdapter(Ordersql2, conn);     // Add a Data_adapter for the data by Ordersql2 command
                    ad2.Fill(ds2);   // Fill Data_adapter from buffer - DataSet -- Trainer Form
                    txtTimes.Text = ds2.Tables[0].Rows[0][4].ToString();
                }
            }
            picCouAll_Click(sender, e);
            conn.Close();
        }

        // Refresh and clear the contents of controls
        void RefreshCourse()
        {
            txtMeName.Text = "";
            comCouName.Text = "";
            comboTraName.Text = "";
            txtTimes.Text = "";
            DataGridView1.DataSource = null;
        }

        private void picCoufresh_Click(object sender, EventArgs e)
        {
            // Refresh and clear the contents of controls
            RefreshCourse();
        }

        /// <summary>
        /// Show the whole of course orders information on DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picCouAll_Click(object sender, EventArgs e)
        {
            // Displays information on the control of DataGridView
            ConnDB();

            // Create an instance of data.datatable
            DataTable dtCou = new DataTable();

            // Create a sql query command 
            string sqlCou = "Select * from Course_order";

            // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
            MySqlCommand cmdCou = new MySqlCommand(sqlCou, conn);

            // Add a Data_adapter for the data by sql command.... In order to display data on the form
            MySqlDataAdapter adCou = new MySqlDataAdapter(sqlCou, conn);
            try
            {
                // Add the data Source in to table buff
                adCou.Fill(dtCou);
                // Put the data source into DataGridView from buff
                DataGridView1.DataSource = dtCou;
            }
            catch
            {
                MessageBox.Show("Mysql Error !");
            }
            finally
            {
                conn.Close();
            }
        }

        // Close the Course Registeration page
        private void picCouClose_Click(object sender, EventArgs e)
        {
            groBoxCou.Visible = false;
        }

        private void navCashSys_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            panel1.Visible = true;
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
        }

        private void navAboutInfo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            MessageBox.Show("Intellgent Management System of Fitness Club \nCurrent Version: Beta 3.0(64 bit)\nCopyright © 2019-2020 Liu_Shangyuan \nAll Rights Reserved !" +
                "\n \nWarning: This computer program is protected by copyright law and international treaties. Unauthorized reproduction or dissemination " +
                "of this program (or any part of it) will result in severe civil sanctions and prosecution to the fullest extent permitted by law.", 
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void navSuppInfo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            string path = @"C:\Users\72946\Desktop\GU 2\Document\Help Document.pdf"; 
            System.Diagnostics.Process.Start(path); // Open this file。 
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        /// <summary>
        /// Click on the data table to display relative data in the corresponding control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string CouName = DataGridView1.CurrentRow.Cells[0].Value.ToString();
            comCouName.Text = CouName;

            string MeName = DataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtMeName.Text = MeName;

            string TraName = DataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboTraName.Text = TraName;

            string CouTimes = DataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtTimes.Text = CouTimes;         
        }

        private void DataGridViewAdmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string AdName= DataGridViewAdmin.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = AdName;

            string Tel= DataGridViewAdmin.CurrentRow.Cells[1].Value.ToString();
            txtTel.Text = Tel;

            string Email = DataGridViewAdmin.CurrentRow.Cells[2].Value.ToString();
            txtEmail.Text = Email;

            string Password = DataGridViewAdmin.CurrentRow.Cells[3].Value.ToString();
            txtPassword.Text = Password;
        }

        // Open the Excel Form to add Potential Customer
        private void navPotential_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            groBoxCou.Visible = false;
            groBoxAdmini.Visible = false;
            panel1.Visible = false;
            string path = @"C:\Users\72946\Desktop\GU 2\Document\Potential Customers Form.xlsx";
            System.Diagnostics.Process.Start(path); // Open this file。 
        }

        // Return to login form
        private void picLogo_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Page login = new Login_Page();
            login.Show();
        }

        /// <summary>
        /// Exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Whether to Exit the Management System ?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(result == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Back up the database regularly 
        /// </summary>
        private void FixedTimeBackupDB()
        {
            string lastRunTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            DateTime t1 = Convert.ToDateTime(lastRunTime);
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime t2 = Convert.ToDateTime(currentTime);
            int result = DateTime.Compare(t1, t2);

            DateTime dtNow = DateTime.Now;
            if (result <= 0 && DateTime.Now.Hour == 15 && DateTime.Now.Minute == 22)
            {

                lastRunTime = DateTime.Now.ToString("yyyy-MM-dd");

                //Begin Backp data
                MessageBox.Show("Start Backing Up the Data" + DateTime.Now.ToString(), "MySQL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// run cmd to process MySQL
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="strcmd"></param>
        /// <returns></returns>
        private string RunCmd(string strPath, string strcmd)
        {
            Process p = new Process();
            // Run cmd.exe program
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.WorkingDirectory = strPath;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.WriteLine(strcmd);
            p.StandardInput.WriteLine("exit");
            return p.StandardError.ReadToEnd();
        }

        /// <summary>
        /// Back-up Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBackup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            // Statement to call mysqldump to backup mysql database
            string backupsql = string.Format("mysqldump --host={0} --default-character-set=utf8 --lock-tables  --routines --force --port=3306 --user={1} --password={2} --quick  ", "127.0.0.1", "root", "LDF8705012");
            // Path to mysqldump
            string mysqldump = "D:\\MySQL\\mysql-8.0.16-winx64\\bin";
            // The name of the database to be backed up
            string strDB = "Gym";
            // Path to backup database
            string strDBpath = @"C:\Users\72946\Desktop\";

            // Determine whether the backup database path exists
            if (!Directory.Exists(strDBpath))
            {
                Directory.CreateDirectory(strDBpath);
            }

            // Backup Database
            if (!string.IsNullOrEmpty(strDB))
            {
                string filePath = strDBpath + strDB + ".sql";
                string cmd = backupsql + " " + strDB + " >" + filePath;
                // Run cmd to process MySQL
                string result = RunCmd(mysqldump, cmd);
                MessageBox.Show("Data Backup Successful！", "MySQL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Recovery Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navRecovery_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            // Call the system to open the window control to select the file path
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Statement to call mysqldump to backup mysql database
                string backupsql = string.Format("mysql --host={0} --default-character-set=utf8  --port=3306 --user={1} --password={2} ", "127.0.0.1", "root", "LDF8705012");
                // MySQL Path
                string mysqldump = "D:\\MySQL\\mysql-8.0.16-winx64\\bin";
                // The name of the database to be backed up
                string strDB = "gu";

                string filePath = ofd.FileName;
                MessageBox.Show(filePath, "Currently Selected Path", MessageBoxButtons.OK, MessageBoxIcon.Information);

                string cmd = backupsql + " " + strDB + " < \"" + filePath + "\"";
                string result = RunCmd(mysqldump, cmd);
                MessageBox.Show("Data Recovery Successful!", "MySQL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
