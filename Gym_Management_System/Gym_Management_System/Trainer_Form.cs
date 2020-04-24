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
using System.Collections;
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
    public partial class Trainer_Form : Form
    {
        public string Trainer_Info;
        int result;
        string strAce = "Acceptance";
        string strRej = "Rejection";
        string strRev = "Reviewing";
        int CourseSale = 0;

        #region Connect_Database
        //SignInInterface Mysql DataBase Connection
        private const string dbServer = "server=127.0.0.1;port=3306;database=gym;user=root;password=**********";
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

        public Trainer_Form()
        {
            InitializeComponent();
            Win32.AnimateWindow(this.Handle, 1500, Win32.AW_BLEND);
        }

        /// <summary>
        /// Return to Login Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picLogo_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login_Page login = new Login_Page();
            login.Show();
        }

        /// <summary>
        /// Display Trainer Information
        /// </summary>
        private void BindDataGridTraInfo()
        {
            bool flag;  // Judge if search successfully
            ConnDB();
            //  Create an instance of System.Data.DataTable
            DataTable dt = new DataTable();
            // Create a command
            string sql = "Select * from Trainer where Name ='" + Trainer_Info + "'";

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
                string sql2 = "Select * from Trainer where Email ='" + Trainer_Info + "'";

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
                    string sql3 = "Select * from Trainer where TelNum ='" + Trainer_Info + "'";
                    // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                    MySqlCommand cmd3 = new MySqlCommand(sql3, conn);
                    MySqlDataAdapter ad3 = new MySqlDataAdapter(sql3, conn);
                    ad3.Fill(ds);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        flag = true;
                        cmd3.ExecuteNonQuery();
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
                    string pic = ds.Tables[0].Rows[0][11].ToString();
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
        /// Display the Course Order Information of Trainer
        /// </summary>
        private void Course_Order()
        {
            ConnDB();
            DataTable dt = new DataTable();
            string sql = "Select * from Course_Order Where Trainer_Name ='" + txtName.Text + "'";
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
        }

        /// <summary>
        /// Display the Course Order Information of Trainer via select the Date
        /// </summary>
        private void Course_DateOrder()
        {
            ConnDB();
            DataTable dt = new DataTable();
            string sql = string.Format("Select * from Course_Order Where Trainer_Name = '{0}' And Open_Date between '{1}'and '{2}'", txtName.Text, dateTimeStart.Text, dateTimeEnd.Text);
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
        }

        /// <summary>
        /// Show Trainer Course Schedule Information
        /// </summary>
        private void BindDataGridSchedule()
        {
            // Displays information on the control of DataGridView
            ConnDB();
            // Create an instance of data.datatable
            DataTable dt = new DataTable();
            // Create a sql query command 
            string sqlCou = "Select * From Course_Schedule Where Trainer_Name = '" + txtName.Text + "'";
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
            // Displays information on the control of DataGridView
            ConnDB();
            // Create an instance of data.datatable
            DataTable dt = new DataTable();
            // Create a sql query command 
            string sqlCou = string.Format("Select * from Course_Schedule Where Trainer_Name = ('{0}') And Status = ('{1}')", txtName.Text, strAce);
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
            // Displays information on the control of DataGridView
            ConnDB();
            // Create an instance of data.datatable
            DataTable dt = new DataTable();
            // Create a sql query command 
            string sqlCou = string.Format("Select * from Course_Schedule Where Trainer_Name = ('{0}') And Status = ('{1}')", txtName.Text, strRej);
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
            // Displays information on the control of DataGridView
            ConnDB();
            // Create an instance of data.datatable
            DataTable dt = new DataTable();
            // Create a sql query command 
            string sqlCou = string.Format("Select * from Course_Schedule Where Trainer_Name = ('{0}') And Status = ('{1}')", txtName.Text, strRev);
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
        /// Display the Schedule count of trainer
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
            string sqlAllCount = string.Format("Select count(*) From Course_Schedule Where Trainer_Name = ('{0}')", txtName.Text);
            string sqlAcpCount = string.Format("Select count(*) From Course_Schedule Where Trainer_Name = ('{0}') And Status = ('{1}')", txtName.Text, strAce);
            string sqlRejCount = string.Format("Select count(*) From Course_Schedule Where Trainer_Name = ('{0}') And Status = ('{1}')", txtName.Text, strRej);
            string sqlRevCount = string.Format("Select count(*) From Course_Schedule Where Trainer_Name = ('{0}') And Status = ('{1}')", txtName.Text, strRev);
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Display trainers' sale amount
        /// </summary>
        private void Trainer_SaleAmount()
        {
            ConnDB();
            // Create a DataSet object (equivalent to creating a virtual database in the foreground
            DataSet dsSale = new DataSet();
            // Create the SQL query command
            string strSale = string.Format("Select count(*) From Course_order Where Trainer_Name = '{0}' And Open_Date between '{1}'and '{2}'", txtName.Text, dateTimeStart.Text, dateTimeEnd.Text);
            // Save the results of the query to the virtual database
            MySqlDataAdapter daSale = new MySqlDataAdapter(strSale, conn);
            try
            {
                // Store the results of the query into a virtual table in the virtual database ds
                daSale.Fill(dsSale);
                // Display the Count data of Course Schedule
                txtSaleAmount.Text = dsSale.Tables[0].Rows[0][0].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Display trainers' sale volume
        /// </summary>
        private void Trainer_SaleVolume()
        {
            CourseSale = 0;
            ConnDB();
            // Create the array list to store the data from database
            ArrayList listSale = new ArrayList();   
            // Create the SQL query command
            string strSale = string.Format("Select Price from Course_order Where Trainer_Name = '{0}' And Open_Date between '{1}'and '{2}'", txtName.Text, dateTimeStart.Text, dateTimeEnd.Text);
            // Save the results of the query to the virtual database
            MySqlDataAdapter daSale = new MySqlDataAdapter(strSale, conn);
            // Create a DataSet object (equivalent to creating a virtual database in the foreground)
            DataSet dsSale = new DataSet();// Store the results of the query into a virtual table in the virtual database ds
            daSale.Fill(dsSale);
            // Copy the data of the data table tabuser to the DataTable object (take the data)
            DataTable dtSale = dsSale.Tables[0];

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drSale in dtSale.Rows)
            {
                // dr [0] means take the first column of the result
                listSale.Add(drSale[0].ToString().Trim());
            }

            for (int i = 0; i < listSale.Count; i++)
            {
                CourseSale += int.Parse(listSale[i].ToString());
                txtSaleVolume.Text = CourseSale.ToString();
            }
        }

        /// <summary>
        /// Compare the start time and end time
        /// </summary>
        private void DateCompare()
        {
            // Convert date string to date object
            DateTime t1 = DateTime.Parse(dateTimeStart.Text);
            DateTime t2 = DateTime.Parse(dateTimeEnd.Text);
            // Compare by DateTIme.Compare
            int compDate = DateTime.Compare(t1, t2);

            if (compDate > 0)  // The card's valid date out of deadline
            {
                txtSaleVolume.Text = "0";
                txtSaleAmount.Text = "0";
            }
        }

        /// <summary>
        /// Compare the datetime and schedule time
        /// </summary>
        private void DateCompareNow()
        {
            string strnow = DateTime.Now.ToString();
            // Convert date string to date object
            DateTime t1 = DateTime.Parse(strnow);
            DateTime t2 = DateTime.Parse(txtScheduleTime.Text);
            // Compare by DateTIme.Compare
            result = DateTime.Compare(t1, t2);
        }

        ///// <summary>
        ///// Display some info about the Course Schedule
        ///// </summary>
        //private void Course_Schedule()
        //{
        //    ConnDB();
        //    DataSet ds = new DataSet();
        //    // Write the mysql statement for the search and assign the value   
        //    string sql = "Select * from Course_Schedule Where Trainer_Name ='" + txtName.Text + "'";
        //    MySqlCommand cmd = new MySqlCommand(sql, conn);
        //    MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
        //    ad.Fill(ds);

        //    // The search contents is right
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        cmd.ExecuteNonQuery();
        //        // Assigns the [] value of row 0 of the 0th table in the cache
        //        txtCourseName.Text = ds.Tables[0].Rows[0][0].ToString();
        //        txtMemberName.Text = ds.Tables[0].Rows[0][1].ToString();
        //        txtTrainerName.Text = ds.Tables[0].Rows[0][2].ToString();
        //    }
        //    else
        //    {
        //        MessageBox.Show("The Course Schedule is not Exist !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}
        private void Trainer_Form_Load(object sender, EventArgs e)
        {
            // Set the visible of grop-box
            switchMember.Checked = true;
            switchCourse.Checked = true;
            switchSchedule.Checked = true;
            switchSale.Checked = true;

            BindDataGridTraInfo();
            BindDataGridSchedule();
            Schedule_Count();
            Course_Order();
            Trainer_SaleVolume();
            Trainer_SaleAmount();
           //  Course_Schedule();
        }

        /// <summary>
        /// Display the Course Order Data Table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchCourse_CheckedChanged(object sender, EventArgs e)
        {
            if (switchCourse.Checked == false)
            {
                DataGridOrder.Visible = false;
            }
            else
            {
                DataGridOrder.Visible = true;
            }
        }

        /// <summary>
        /// Dislpay Schedule Grop Box
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
        /// Set the visible of Member Info Grop Box
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
        /// Click the cells of Data Table to display schedule information 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string CouName = DataGridSchedule.CurrentRow.Cells[0].Value.ToString();
            txtCourseName.Text = CouName;

            string MemName = DataGridSchedule.CurrentRow.Cells[1].Value.ToString();
            txtMemberName.Text = MemName;

            string TraName = DataGridSchedule.CurrentRow.Cells[2].Value.ToString();
            txtTrainerName.Text = TraName;

            string SchTime = DataGridSchedule.CurrentRow.Cells[3].Value.ToString();
            txtScheduleTime.Text = SchTime;

            string EquipName = DataGridSchedule.CurrentRow.Cells[4].Value.ToString();
            txtEquipment.Text = EquipName;

            string Status = DataGridSchedule.CurrentRow.Cells[5].Value.ToString();
            txtStatus.Text = Status;
        }

        /// <summary>
        /// Dispose the booking schedule information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnaAccept_Click(object sender, EventArgs e)
        {
            if (txtCourseName.Text == "" || txtMemberName.Text == "" || txtTrainerName.Text == "")
            {
                MessageBox.Show("Please Select the Booking Information to be Processed !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(txtStatus.Text != "Reviewing")
            {
                MessageBox.Show("This Message Has Been Processed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ConnDB();
                // Update Booking information
                string sql = string.Format("Update Course_Schedule set Status = ('{0}') where Course_Name = ('{1}') And Member_Name = ('{2}') And Trainer_Name = ('{3}') And Schedule_Time = ('{4}')",
                    strAce, txtCourseName.Text, txtMemberName.Text, txtTrainerName.Text, txtScheduleTime.Text);
                // Connect and call the database for update
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                try
                {
                    // Executive command
                    cmd.ExecuteNonQuery();
                    BindDataGridSchedule();
                    Schedule_Count();
                    MessageBox.Show("Accept Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Reject the Course Booking Schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReject_Click(object sender, EventArgs e)
        {
            if (txtCourseName.Text == "" || txtMemberName.Text == "" || txtTrainerName.Text == "")
            {
                MessageBox.Show("Please Select the Booking Information to be Processed !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (txtStatus.Text != "Reviewing")
            {
                MessageBox.Show("This Message has been Processed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ConnDB();
                string sql = string.Format("Update Course_Schedule set Status = ('{0}') where Course_Name = ('{1}') And Member_Name = ('{2}') And Trainer_Name = ('{3}') And Schedule_Time = ('{4}')",
                    strRej, txtCourseName.Text, txtMemberName.Text, txtTrainerName.Text, txtScheduleTime.Text);
                // Connect and call the database for update
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                try
                {
                    // Executive command
                    cmd.ExecuteNonQuery();
                    BindDataGridSchedule();
                    Schedule_Count();
                    MessageBox.Show("Reject Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Display the whole info of schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picAll_Click(object sender, EventArgs e)
        {
            BindDataGridSchedule();
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
        /// Delete the overdue schedule course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DateCompareNow();
            ConnDB();
            if (txtCourseName.Text == "" || txtMemberName.Text == "" || txtTrainerName.Text == "")
            {
                MessageBox.Show("Please Select the Booking Information to Delete !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (result < 0 && txtStatus.Text != "Rejection" && txtStatus.Text != "Acceptance")
            {
                MessageBox.Show("No Permission to Delete Information Yet !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ConnDB();
                // Add the new course order into Course_Schedule table
                string sql = string.Format("Delete from Course_Schedule where Course_Name = ('{0}') And Member_Name = ('{1}') And Trainer_Name = ('{2}') And Schedule_Time = ('{3}') And Equipmemt = ('{4}') And Status = ('{5}') ",
                    txtCourseName.Text, txtMemberName.Text, txtTrainerName.Text, txtScheduleTime.Text, txtEquipment.Text, txtStatus.Text);
                // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    BindDataGridSchedule();
                    Schedule_Count();
                    MessageBox.Show("Delete Course Schedule Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Delete the Course Schedule Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Change the begin and end date time to display sale volume and amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            Trainer_SaleVolume();
            DateCompare();
            Trainer_SaleAmount();
            Course_DateOrder();
        }

        private void dateTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            Trainer_SaleVolume();
            DateCompare();
            Trainer_SaleAmount();
            Course_DateOrder();
        }

        /// <summary>
        /// Set the switch of group box's visiable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchSale_CheckedChanged(object sender, EventArgs e)
        {
            if (switchSale.Checked == false)
            {
                groupSale.Visible = false;
            }
            else
            {
                groupSale.Visible = true;
            }
        }
    }
}
