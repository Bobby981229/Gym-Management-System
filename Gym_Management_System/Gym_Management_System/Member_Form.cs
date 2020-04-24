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
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Management_System
{
    public partial class Member_Form : Form
    {
        public string member_Info;
        public string member_Number;
        public int times;
        public string strAce = "Acceptance";
        public string strRej = "Rejection";
        public string strRev = "Reviewing";

        #region Connect_Database
        //SignInInterface Mysql DataBase Connection
        public const string dbServer = "server=127.0.0.1;port=3306;database=gym;user=root;password=**********";
        MySqlConnection conn;

        // Create a connection
        private void ConnDB()
        {
            // Server address- The port number.Database;The user name;password
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

        public Member_Form()
        {
            InitializeComponent();
            Win32.AnimateWindow(this.Handle, 1500, Win32.AW_BLEND);
        }

        /// <summary>
        /// Display the Information of Member
        /// </summary>
        private void BindDataGridInfo()
        {
            bool flag;  // Judge if search successfully
            ConnDB();
            //  Create an instance of System.Data.DataTable
            DataTable dt = new DataTable();
            // Create a command
            string sql = "Select * from Member where Name ='" + member_Info + "'";

            // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
            ad.Fill(ds);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                flag = true;
                cmd.ExecuteNonQuery();
            }
            else
            {
                DataTable dt2 = new DataTable();
                // Create a command
                string sql2 = "Select * from Member where Email ='" + member_Info + "'";

                // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                MySqlDataAdapter ad2 = new MySqlDataAdapter(sql2, conn);
                ad2.Fill(ds);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    flag = true;
                    cmd2.ExecuteNonQuery();
                }
                else
                {
                    DataTable dt3 = new DataTable();
                    // Create a command
                    string sql3 = "Select * from Member where TelNum ='" + member_Info + "'";

                    // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                    MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                    MySqlDataAdapter ad3 = new MySqlDataAdapter(sql3, conn);
                    ad3.Fill(ds);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }

            // Mysql Execute successfully and display the contents on controls
            if (flag == true)
            {
                try
                {
                    txtNumber.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtName.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtDeadline.Text = ds.Tables[0].Rows[0][11].ToString();
                    string pic = ds.Tables[0].Rows[0][13].ToString();
                    this.picImage.Image = Image.FromFile(pic);
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
        }

        /// <summary>
        /// Display the Course Order Information of Member
        /// </summary>
        private void Course_Order()
        {
            ConnDB();
            DataTable dt = new DataTable();
            string sql = "Select * from Course_Order Where Member_Name ='" + txtName.Text + "'";
            MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
            try
            {
                ad.Fill(dt);
                DataGridOrder.DataSource = dt;
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

        /// <summary>
        /// Display Physical Info of Member
        /// </summary>
        private void Member_PhyInfo()
        {
            ConnDB();
            DataSet ds = new DataSet();
            // Write the mysql statement for the search and assign the value   
            string sql = "Select * from BMI Where Number ='" + member_Number + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
            ad.Fill(ds);
            try
            {
                // The search contents is right
                cmd.ExecuteNonQuery();

                // Assigns the [] value of row 0 of the 0th table in the cache
                txtHight.Text = ds.Tables[0].Rows[0][2].ToString();
                txtWeight.Text = ds.Tables[0].Rows[0][3].ToString();
                txtBMI.Text = ds.Tables[0].Rows[0][4].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                // MessageBox.Show("Mysql Error !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }  
        }

        /// <summary>
        /// Show the Schedule Info of the Course Order
        /// </summary>
        private void BindDataGridSchedule()
        {
            // Displays information on the control of DataGridView
            ConnDB();
            // Create an instance of data.datatable
            DataTable dt = new DataTable();
            // Create a sql query command 
            string sqlCou = "Select * from Course_Schedule Where Member_Name = '" + txtName.Text.Trim() + "'";
            // Add a Data_adapter for the data by sql command.... In order to display data on the form
            MySqlDataAdapter ad = new MySqlDataAdapter(sqlCou, conn);
            try
            {
                // Add the data Source in to table buff
                ad.Fill(dt);               
                // Put the data source into DataGridView from buff
                DataGridSchedule.DataSource = dt;
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

        /// <summary>
        /// Display the Schedule count of member
        /// </summary>
        private void Schedule_Count()
        {
            // Displays information on the control of DataGridView
            ConnDB();
            DataSet dsAll = new DataSet();
            DataSet dsAcp = new DataSet();
            DataSet dsRej = new DataSet();
            DataSet dsRev = new DataSet();
            // Mysql Search Count Command
            string sqlAllCount = string.Format("Select count(*) From Course_Schedule Where Member_Name = ('{0}')", txtName.Text);
            string sqlAcpCount = string.Format("Select count(*) From Course_Schedule Where Member_Name = ('{0}') And Status = ('{1}')", txtName.Text, strAce);
            string sqlRejCount = string.Format("Select count(*) From Course_Schedule Where Member_Name = ('{0}') And Status = ('{1}')", txtName.Text, strRej);
            string sqlRevCount = string.Format("Select count(*) From Course_Schedule Where Member_Name = ('{0}') And Status = ('{1}')", txtName.Text, strRev);
            MySqlDataAdapter adAll = new MySqlDataAdapter(sqlAllCount, conn);
            MySqlDataAdapter adAcp = new MySqlDataAdapter(sqlAcpCount, conn);
            MySqlDataAdapter adRej = new MySqlDataAdapter(sqlRejCount, conn);
            MySqlDataAdapter adRev = new MySqlDataAdapter(sqlRevCount, conn);
            try
            {
                adAll.Fill(dsAll);
                adAcp.Fill(dsAcp);
                adRej.Fill(dsRej);
                adRev.Fill(dsRev);

                // Display the Count data of Course Schedule
                lblShowAll.Text = dsAll.Tables[0].Rows[0][0].ToString();
                lblShowAccept.Text = dsAcp.Tables[0].Rows[0][0].ToString();
                lblShowReject.Text = dsRej.Tables[0].Rows[0][0].ToString();
                lblShowReview.Text = dsRev.Tables[0].Rows[0][0].ToString();
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

        // Reture to the Login form
        private void picLogo_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Page login = new Login_Page();
            login.Show();
        }

        private void Member_Form_Load(object sender, EventArgs e)
        {
            // Set the visible of grop-box
            switchMember.Checked = true;
            switchCourse.Checked = true;
            switchSchedule.Checked = true;

            BindDataGridInfo();
            Member_PhyInfo();
            BindDataGridSchedule();
            Schedule_Count();
            Course_Order();
        }     

        /// <summary>
        /// Set the visible of Course Info Data Table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchCourse_CheckedChanged(object sender, EventArgs e)
        {
            if (switchCourse.Checked == false)
            {
                groupScheData.Visible = false;
            }
            else
            {
                groupScheData.Visible = true;
            }
        }

        /// <summary>
        /// Set the visible of Course Schedule Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchSchedule_CheckedChanged(object sender, EventArgs e)
        {
            if (switchSchedule.Checked == false)
            {
                groupBoxSchedule.Visible = false;
            }
            else
            {
                groupBoxSchedule.Visible = true;
            }
        }

        /// <summary>
        /// Set the visible of Member Info Grop-box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchMember_CheckedChanged(object sender, EventArgs e)
        {
            if (switchMember.Checked == false)
            {
                groupBoxInfo.Visible = false;
            }
            else
            {
                groupBoxInfo.Visible = true;
            }
        }

        /// <summary>
        /// Display the detail information of Course Order 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string CouName = DataGridOrder.CurrentRow.Cells[0].Value.ToString();
            txtCourseName.Text = CouName;

            string MemName = DataGridOrder.CurrentRow.Cells[1].Value.ToString();
            txtMemberName.Text = MemName;

            string TraName = DataGridOrder.CurrentRow.Cells[2].Value.ToString();
            txtTrainerName.Text = TraName;

            string ExTimes = DataGridOrder.CurrentRow.Cells[4].Value.ToString();
            times = int.Parse(ExTimes);

            txtEquipment.Text = "";
        }

        /// <summary>
        /// Booking the date time of course schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBooking_Click(object sender, EventArgs e)
        {
            string Data_Time = dtpDate.Text + " " + dtpTime.Text;
            string status = "Reviewing";
            if (txtCourseName.Text == ""  || txtMemberName.Text == "" || txtTrainerName.Text == "" || txtEquipment.Text == "")
            {
                MessageBox.Show("Please Input the Course Schedule Completely ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(times == 0)  // Judge the course time 
            {
                MessageBox.Show("The Remaining Course Times is 0, Cannot be Reserved ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ConnDB();
                // Add the new course order into Course_Schedule table
                string sql = string.Format("Insert into Course_Schedule(Course_Name, Member_Name, Trainer_Name, Schedule_Time, Equipmemt, Status) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                    txtCourseName.Text, txtMemberName.Text, txtTrainerName.Text, Data_Time, txtEquipment.Text, status);
                // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    BindDataGridSchedule();
                    Schedule_Count();
                    MessageBox.Show("Course Schedule Add Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Course Time is Repeated, Please Select a New Date Time !", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Show Member Course Schedule Information
        /// </summary>
        private void BindDataAllSchedule()
        {
            // Displays information on the control of DataGridView
            ConnDB();
            // Create an instance of data.datatable
            DataTable dt = new DataTable();
            // Create a sql query command 
            string sqlCou = "Select * from Course_Schedule Where Member_Name = '" + txtName.Text + "'";
            // Add a Data_adapter for the data by sql command.... In order to display data on the form
            MySqlDataAdapter ad = new MySqlDataAdapter(sqlCou, conn);
            try
            {
                // Add the data Source in to table buff
                ad.Fill(dt);
                // Put the data source into DataGridView from buff
                DataGridSchedule.DataSource = dt;
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

        /// <summary>
        /// Display the Acception Course Schedule
        /// </summary>
        private void BindDataAcceptSchedule()
        {
            string strAce = "Acceptance";
            // Displays information on the control of DataGridView
            ConnDB();
            // Create an instance of data.datatable
            DataTable dt = new DataTable();
            // Create a sql query command 
            string sqlCou = string.Format("Select * from Course_Schedule Where Member_Name = ('{0}') And Status = ('{1}')", txtName.Text, strAce);
            // Add a Data_adapter for the data by sql command.... In order to display data on the form
            MySqlDataAdapter ad = new MySqlDataAdapter(sqlCou, conn);
            try
            {
                // Add the data Source in to table buff
                ad.Fill(dt);
                // Put the data source into DataGridView from buff
                DataGridSchedule.DataSource = dt;
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

        /// <summary>
        /// Display the Rejection Course Schedule
        /// </summary>
        private void BindDataRejectSchedule()
        {
            string strRej = "Rejection";
            // Displays information on the control of DataGridView
            ConnDB();
            // Create an instance of data.datatable
            DataTable dt = new DataTable();
            // Create a sql query command 
            string sqlCou = string.Format("Select * from Course_Schedule Where Member_Name = ('{0}') And Status = ('{1}')", txtName.Text, strRej);
            // Add a Data_adapter for the data by sql command.... In order to display data on the form
            MySqlDataAdapter ad = new MySqlDataAdapter(sqlCou, conn);
            try
            {
                // Add the data Source in to table buff
                ad.Fill(dt);
                // Put the data source into DataGridView from buff
                DataGridSchedule.DataSource = dt;
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

        /// <summary>
        /// Display the Reviewing Course Schedule
        /// </summary>
        private void BindDataReviewSchedule()
        {
            string strRev = "Reviewing";
            // Displays information on the control of DataGridView
            ConnDB();
            // Create an instance of data.datatable
            DataTable dt = new DataTable();
            // Create a sql query command 
            string sqlCou = string.Format("Select * from Course_Schedule Where Member_Name = ('{0}') And Status = ('{1}')", txtName.Text, strRev);
            // Add a Data_adapter for the data by sql command.... In order to display data on the form
            MySqlDataAdapter ad = new MySqlDataAdapter(sqlCou, conn);
            try
            {
                // Add the data Source in to table buff
                ad.Fill(dt);
                // Put the data source into DataGridView from buff
                DataGridSchedule.DataSource = dt;
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

        /// <summary>
        /// Display the whole info of schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picAll_Click(object sender, EventArgs e)
        {
            BindDataAllSchedule();
        }

        /// <summary>
        /// Display the acception info of course schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picAccept_Click(object sender, EventArgs e)
        {
            BindDataAcceptSchedule();
        }

        /// <summary>
        /// Display the reviewing info of course schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picReview_Click(object sender, EventArgs e)
        {
            BindDataReviewSchedule();
        }

        /// <summary>
        /// Display the rejection info of course schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picReject_Click(object sender, EventArgs e)
        {
            BindDataRejectSchedule();
        }

        /// <summary>
        /// Limited Input and Key Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEquipment_KeyPress(object sender, KeyPressEventArgs e)
        {
            // This textbox can only input english characters
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == '\b')  //只能输入英文
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            //  Enter auto click the search event
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBooking_Click(sender, e);
            }
        }

        /// <summary>
        /// Limited Input of Hight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHight_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can only enter numbers
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && txtHight.Text.Trim() == "")
            {
                e.Handled = true;
            }

            // The decimal point can only be used once
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                //Cancel entry
                e.Handled = true;
            }

            //  Keep one decimal and after decimal point have two number
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) +
                2 && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                e.Handled = true;
            }

            // The first digit is 0, the second digit must be the decimal pointd
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Limited Input of Weight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can only enter numbers
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Calculate BMI index based on user's height and weight
        /// </summary>
        /// <returns></returns>
        string calculateBMI()
        {
            double h = Convert.ToDouble(txtHight.Text);
            double w = Convert.ToDouble(txtWeight.Text);
            double b = w / Math.Pow(h, 2);
            double B = Math.Round(b, 2);

            string BMI = Convert.ToString(B);
            txtBMI.Text = BMI;
            // According to different BMI index ranges, remind the corresponding physical health situation
            if (B < 18.4 && B > 0)
            {
                MessageBox.Show("Thin Body", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (B >= 18.5 && B < 24.9)
            {
                MessageBox.Show("Normal Weight", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (B >= 25.0 && B < 29.9)
            {
                MessageBox.Show("Overweight", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (B >= 30.0)
            {
                MessageBox.Show("Obesity", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (B == 0)
            {
                txtHight.Text = "";
                txtWeight.Text = "";
                txtBMI.Clear();
                MessageBox.Show("Data Error, please enter again !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return BMI;
        }

        private void btnBMI_Click(object sender, EventArgs e)
        {
            if (txtHight.Text == "" || txtWeight.Text == "")
            {
                MessageBox.Show("The height and weight of members can not be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                calculateBMI();
            }
        }

        /// <summary>
        /// Change and Update Member Physical Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHight_TextChanged(object sender, EventArgs e)
        {
            txtBMI.Text = "";
        }

        /// <summary>
        /// Update the Member's Personal Physical Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtHight.Text == "" || txtWeight.Text == "" || txtBMI.Text == "")
            {
                MessageBox.Show("Please Enter Personal Info Completely", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ConnDB();
                // Update personal BMI Index MySQL Command
                string strBMI = string.Format("Update BMI set Hight = ('{0}'), Weight = ('{1}'), BMI = ('{2}') where Number = ('{3}')", 
                    txtHight.Text, txtWeight.Text, txtBMI.Text, txtNumber.Text);
                // Connect and call the database to update
                MySqlCommand cmd = new MySqlCommand(strBMI, conn);
                try
                {
                    // Executive command
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }
    }
}
