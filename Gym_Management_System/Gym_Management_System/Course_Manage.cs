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
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;

namespace Gym_Management_System
{    

    public partial class Course_Management : Form
    {
        // The price of course
        public int price;

        // The change volumes of courses times
        int volumes = 1;

        // The result of courses times
        int result;

        // The price after changing price
        int Price;

        #region Connect_Database
        //SignInInterface Mysql DataBase Connection
        private const string dbServer = "server=127.0.0.1;port=3306;database=gym;user=root;password=**********";
        MySqlConnection conn;

        public object MySQLConnect { get; private set; }

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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Close the database
        private void CloseDB()
        {
            conn.Close();
        }
        #endregion

        public Course_Management()
        {
            InitializeComponent();
            //Expand the effects from the inside out
            Win32.AnimateWindow(this.Handle, 300, Win32.AW_CENTER | Win32.AW_ACTIVATE | Win32.AW_SLIDE);
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Train_Ad_Load(object sender, EventArgs e)
        {
            // Bind data source to display all Course names
            Course_Nmae();
            // Bind data source to display all Member names
            Member_Nmae();
            // Bind data source to display all Trainer names
            Trainer_Nmae();
            // Bind data source to display all Course Price
            Course_Price();

            ConnDB();  // Connnect the Database
            conn.Close();
            switchEdit.Checked = true;
        }      

        // Return to main page
        private void picLogo_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        // Binding ComboBox data source - Course Name
        private void Course_Nmae()
        {
            ConnDB();
            // Create the array list and store the data into list
            ArrayList list = new ArrayList();
            // Query course name data mysql command
            string str = "Select Course_Name from Course";
            //  Create a new  Data Adapter
            MySqlDataAdapter da = new MySqlDataAdapter(str, conn);  

            // Create a new buffer table 
            DataSet ds = new DataSet();
            da.Fill(ds); // Add data
            DataTable dt = ds.Tables[0];

            // Store course name data into an array by traversing the loop
            foreach (DataRow dr in dt.Rows)
            {
                //dr [0] means take the first column of the result
                list.Add(dr[0].ToString().Trim());
            }
            comboxCoName.DataSource = list; // Assign a value to a drop-down list box
            conn.Close();
        }


        // Binding ComboBox data source - Trainers' name
        private void Trainer_Nmae()
        {
            ConnDB();
            // Create the array list and store the data into list
            ArrayList list = new ArrayList();
            // Query Trainer name data mysql command
            string str = "Select Name from Trainer";
            //  Create a new  Data Adapter
            MySqlDataAdapter da = new MySqlDataAdapter(str, conn);

            // Create a new buffer table 
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            // Store trainer name data into an array by traversing the loop
            foreach (DataRow dr in dt.Rows)
            {
                //dr [0] means take the first column of the result
                list.Add(dr[0].ToString().Trim());
            }
            comboTraName.DataSource = list;  // Assign a value to a drop-down list box
            conn.Close();
        }

        // Binding ComboBox data source - Member' name
        private void Member_Nmae()
        {
            ConnDB();
            // Create the array list and store the data into list
            ArrayList list = new ArrayList();
            // Query Member name data mysql command
            string str = "Select Name from Member";
            //  Create a new  Data Adapter
            MySqlDataAdapter da = new MySqlDataAdapter(str, conn);

            // Create a new buffer table 
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            // Store Member name data into an array by traversing the loop
            foreach (DataRow dr in dt.Rows)
            {
                // dr [0] means take the first column of the result
                list.Add(dr[0].ToString().Trim());
            }
            comboMeName.DataSource = list;  // Assign a value to a drop-down list box
            conn.Close();
        }

        // Search the courses's price
        private void Course_Price()
        {
            ConnDB();
            // New a DataSet and pull the data into buffer for sql command
            DataSet ds = new DataSet();
            // Search the ingormation by Number of user in Member table
            string sql = "Select * from Course where Course_Name ='" + comboxCoName.Text + "'";

            // Add a Data_adapter for the data by sql command
            MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
            try
            {
                ad.Fill(ds);
                // Put the query data into the buffer table and assigning values to variables
                price = int.Parse(ds.Tables[0].Rows[0][1].ToString());
                lblShowPrice.Text = ds.Tables[0].Rows[0][1].ToString();
                txtTimes.Text = "1";
            }
            catch
            {

            }
            finally
            {
                conn.Close();
            }            
        }

        // Show the whole of courses' name in combobox
        private void comboxCoName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Course_Price();
            txtSearch.Focus();
        }


        private void txtTimes_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cn only enter english characters
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            // Enter key event to save the editing information
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSave_Click(sender, e);
            }
        }

        // The price changing with the course times
        private void txtTimes_TextChanged(object sender, EventArgs e)
        {
            if(txtTimes.Text == "")
            {
                Course_Price();
            }
            else
            {
                // The result price  = times Pow course price per times
                int t = int.Parse(txtTimes.Text);
                int result = price * t;
                lblShowPrice.Text = result.ToString();
            }           
        }

        // Display the data on DataGridView Table
        private void BindDataGridCourse()
        {
            ConnDB();
            // Create an instance of System.Data.DataTable
            System.Data.DataTable dt = new DataTable();
            // Create a command
            string sql = "Select * from Course_Order";

            // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
            try
            {
                ad.Fill(dt);
                //  Show the whole Course order on DataGridView
                DataGridView1.DataSource = dt;
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

        // Display the data on DataGridView Table
        private void BindDataGridAddCourse()
        {
            ConnDB();
            //  Create an instance of System.Data.DataTable
            System.Data.DataTable dt = new DataTable();
            // Create a command
            string sql = "Select * from Course_Order where Course_Name  ='" + comboxCoName.Text + "'";

            // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
            try
            {
                ad.Fill(dt);
                DataGridView1.DataSource = dt;
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

        /// <summary>
        ///  Add course order info into database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (comboxCoName.Text == "" || comboTraName.Text == "" || comboMeName.Text == "" || datimpTime.Text == "" || txtTimes.Text == "" || lblShowPrice.Text == "0")
            {
                MessageBox.Show("Please complete the member personal information !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ConnDB();
                // New a DataSet and pull the data into buffer for sql command
                DataSet ds = new DataSet();
                // Write the mysql statement for the search and assign the value
                string sql = "Select * from course_sale where Date='" + datimpTime.Text + "'";
                // Add a Data_adapter for the data by sql command.... In order to display data on the form
                MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);   
                ad.Fill(ds); // Add card_order information into DataSet

                // The buff of dataset can not be empty !
                if (ds != null && ds.Tables[0].Rows.Count > 0)    
                {
                    // Fetch data from the cache table and assign values to variables
                    lblSale.Text = ds.Tables[0].Rows[0][1].ToString();
                    lblProfit.Text = ds.Tables[0].Rows[0][2].ToString();

                    // Modify and update variable data
                    result = int.Parse(lblSale.Text) + volumes;
                    Price = int.Parse(lblProfit.Text) + int.Parse(lblShowPrice.Text);

                    // Display the newest sale condition on the screen
                    lblSale.Text = result.ToString();
                    lblProfit.Text = Price.ToString();
                }

                // Add the new course order into Course_Order table
                string strsql = string.Format("Insert into Course_Order(Course_Name, Member_Name, Trainer_Name, Open_Date, Times, Price) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                comboxCoName.Text, comboMeName.Text, comboTraName.Text, datimpTime.Text, txtTimes.Text, lblShowPrice.Text);             

                // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                MySqlCommand cmd = new MySqlCommand(strsql, conn);
                try
                {
                    // Execute the mysql command
                    cmd.ExecuteNonQuery();
                    BindDataGridAddCourse();
                    MessageBox.Show("Add Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Course Order Information Adding Error !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ConnDB();
                // Create a new mysql command to insert or updata data of course_sale
                string saleSql = string.Format("Insert into Course_Sale(Date, Volums, Profit) values ('{0}', '{1}', '{2}')", datimpTime.Text, volumes, lblShowPrice.Text);
                string query2 = string.Format("Update Course_Sale set Volums = ('{0}'), Profit = ('{1}') Where Date = ('{2}')", result, Price, datimpTime.Text);

                MySqlCommand cmd2 = new MySqlCommand(saleSql, conn);
                MySqlCommand cmd3 = new MySqlCommand(query2, conn);

                // If today does not have sale record - Insert new data into Course_Sale table
                try
                {
                    cmd2.ExecuteNonQuery();
                }
                catch  // If add failed that means there is a recourd is exist, so execute cmd3 mysql commamd to updata the mysql command
                {
                    cmd3.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
                // After data edit, the data of combobox is refresh at the same time
                RefreshPage();
            }
        }

        // Display the whole data on table
        private void picShowAll_Click(object sender, EventArgs e)
        {
            BindDataGridCourse();
        }

        /// <summary>
        /// Search the course information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picSearchNum_Click(object sender, EventArgs e)
        {
            if(txtSearch.Text == "")
            {
                MessageBox.Show("Please enter the query information !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ConnDB();  // Connect the Mysql Database
                bool flag;  // Judge the DataSet if is null
                // New a DataSet and pull the data into buffer for sql command
                DataSet ds = new DataSet();
                // Write the mysql statement for the search and assign the value
                string sql = "Select * from Course_Order where Course_Name ='" + txtSearch.Text + "'";

                // Create a new DataTable to show the detail information
                DataTable dt = new DataTable();
                // Add a Data_adapter for the data by sql command.... In order to display data on the form
                MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);  
                ad.Fill(dt); // Add card_order information into DataTable
                ad.Fill(ds); // Add card_order information into DataSet


                // The buff of dataset can not be empty !
                if (ds != null && ds.Tables[0].Rows.Count > 0)    
                {
                    flag = true;   // Search successfully !
                }
                else
                {
                    // Search the course order by member name
                    string sql1 = "Select * from Course_Order where Member_Name ='" + txtSearch.Text + "'";
                    // Add a Data_adapter for the data by sql7 command
                    ad = new MySqlDataAdapter(sql1, conn);     
                    ad.Fill(dt);   // Fill Data_adapter from datatable
                    ad.Fill(ds);   // Fill Data_adapter from buffer - DataSet -- Trainer Form

                    // The buff of dataset can not be empty !
                    if (ds != null && ds.Tables[0].Rows.Count > 0)    
                    {
                        flag = true;   // Search successfully !
                    }
                    else
                    {
                        // Search the course order by trainer name
                        string sql2 = "Select * from Course_Order where Trainer_Name ='" + txtSearch.Text + "'";
                        // Add a Data_adapter for the data by sql8 command
                        ad = new MySqlDataAdapter(sql2, conn);   
                        ad.Fill(dt);   // Fill Data_adapter from datatable
                        ad.Fill(ds);   // Fill Data_adapter from buffer - DataSet -- Trainer Form

                        // The buff of dataset can not be empty !
                        if (ds != null && ds.Tables[0].Rows.Count > 0)    
                        {
                            flag = true;   // Search successfully !
                        }
                        else
                        {
                            flag = false; // Search unsuccessfully !
                        }
                    }
                }

                // This infor can be searched
                if (flag == true)  
                {
                    try
                    {
                        // Display the searching data on the DataGridView
                        DataGridView1.DataSource = dt;

                        // Assigns the [] value of row 0 of the 0th table in the cache                  
                        comboxCoName.Text = ds.Tables[0].Rows[0][0].ToString();
                        comboMeName.Text = ds.Tables[0].Rows[0][1].ToString();
                        comboTraName.Text = ds.Tables[0].Rows[0][2].ToString();
                        datimpTime.Text = ds.Tables[0].Rows[0][3].ToString();
                        txtTimes.Text = ds.Tables[0].Rows[0][4].ToString();
                        lblShowPrice.Text = ds.Tables[0].Rows[0][5].ToString();
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
                else
                {
                    MessageBox.Show("The information is non-existent ! \n Please try it again !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }

        /// <summary>
        /// Updata the data and information in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdata_Click(object sender, EventArgs e)
        {
            if(txtTimes.Text == "" || lblShowPrice.Text == "")
            {
                MessageBox.Show("Personal information cannot be empty !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ConnDB();
                // Updata Course_Name in Course_Order table 
                string query = string.Format("Update Course_Order set Course_Name = ('{0}'), Trainer_Name = ('{1}'), Open_Date = ('{2}'), Times = ('{3}'), Price = ('{4}') Where Member_Name = ('{5}')", comboxCoName.Text, comboTraName.Text, datimpTime.Text, txtTimes.Text, lblShowPrice.Text, comboMeName.Text);
                // Connect and call the database for update
                MySqlCommand cmd = new MySqlCommand(query, conn);   
                try
                {            
                    // Execute the Mysql Command
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                // After updata, the newest data will be showed on the dataGridView table
                BindDataGridCourse();
                conn.Close();
            }
        }

        // Control availability switch
        private void switchEdit_CheckedChanged(object sender, EventArgs e)
        {
            // The switch is close -- The controls can not use or input
            if (switchEdit.Checked == false)
            {
                comboMeName.Enabled = false;
                comboTraName.Enabled = false;
                comboxCoName.Enabled = false;
                datimpTime.Enabled = false;
                txtTimes.ReadOnly = true;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            } // The switch is open -- The controls can use or input
            else if (switchEdit.Checked == true)
            {
                comboMeName.Enabled = true;
                comboTraName.Enabled = true;
                comboxCoName.Enabled = true;
                datimpTime.Enabled = true;
                txtTimes.ReadOnly = false;
                btnAdd.Enabled = true;
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // This textbox can only input english characters
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                picSearchNum_Click(sender, e);
            }
        }

        private void RefreshPage()
        {
            Course_Nmae();
            txtTimes.Text = "";
            txtSearch.Text = "";
            lblShowPrice.Text = "";
        }

        // Refresh and clear the page
        private void picRefresh_Click(object sender, EventArgs e)
        {
            RefreshPage();
            DataGridView1.DataSource = null;
        }

        /// <summary>
        /// Delete the current course order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            ConnDB();
            //Get the number of selected rows
            int row = DataGridView1.SelectedRows.Count;

            // searched for information and deleted it
            if (txtTimes.Text != "") 
            {
                // Confirm if wanna delete this data
                DialogResult result = MessageBox.Show("Confirm to delete this data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                if (result == DialogResult.Yes)
                {
                    // This datais not exist !
                    if (DataGridView1.Rows.Count == 1)
                    {
                        MessageBox.Show("The entry " + txtSearch.Text + " is not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSearch.Text = "";
                    }
                    else
                    {
                        ConnDB();
                        // Delete the Course_Order by searching Member Name
                        string strsql = "Delete from Course_Order where Member_Name ='" + comboMeName.Text + "'";
                        MySqlCommand cmd = new MySqlCommand(strsql, conn);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Delete order info successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshPage();
                        }
                        catch
                        {
                            MessageBox.Show("The " + txtSearch.Text + " entry is not exists! Retry!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);  // The info in not exist!
                        }
                        conn.Close();
                        BindDataGridCourse();
                    }
                }
            }
            else if ((row > 0) && (txtTimes.Text == ""))  // Information in table selected but not searched
            {
                // Confirm if wanna delete this data
                DialogResult result = MessageBox.Show("Confirm to delete this data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);  
                // Ensure to delete the data
                if (result == DialogResult.Yes)
                {
                    // Traverse the loop to find the corresponding data
                    for (int i = 0; i < row; i++)
                    {
                        string dgCou = DataGridView1.SelectedRows[i].Cells[0].Value.ToString();  // Select the Course name as primary key
                        string dgMe = DataGridView1.SelectedRows[i].Cells[1].Value.ToString();   // Select the Member name as primary key
                        string dgTra = DataGridView1.SelectedRows[i].Cells[2].Value.ToString();  // Select the Trainer name as primary key

                        // Create the mysql delete command
                        string delCou = string.Format("Delete from Course_Order where Course_Name = ('{0}') And Member_Name = ('{1}') And Trainer_Name = ('{2}')", dgCou, dgMe, dgTra);
                        MySqlCommand deleCoucmd = new MySqlCommand(delCou, conn);
                        deleCoucmd.ExecuteNonQuery();
                        MessageBox.Show("Delete order info successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    conn.Close();
                    Course_Nmae();
                    // Refresh the DataGridView table
                    BindDataGridCourse();
                }
                RefreshPage();
            }
            else    // Both of two condition is true
            {
                MessageBox.Show("There is no data can be deleted !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtCouName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // This textbox can only input english characters
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == '\b' || e.KeyChar == '\u0020')  
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            // Auto focus on txtprice
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPrice.Focus();
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // This textbox can only input number
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSearchCou_KeyPress(object sender, KeyPressEventArgs e)
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
                picSearchCou_Click(sender, e);
            }
        }

        /// <summary>
        /// Query Course Informaton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picSearchCou_Click(object sender, EventArgs e)
        {
            if (txtSearchCou.Text == "")
            {
                MessageBox.Show("Please enter the query inforamtion !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ConnDB();  // Connect the Mysql Database
                bool flag;  // Judge the DataSet if is null
                // New a DataSet and pull the data into buffer for sql command
                DataSet ds = new DataSet();
                // Write the mysql statement for the search and assign the value
                string sql = "Select * from Course where Course_Name ='" + txtSearchCou.Text + "'";

                // Create a new DataTable to show the detail information
                DataTable dt = new DataTable();
                // Add a Data_adapter for the data by sql command.... In order to display data on the form
                MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);  
                ad.Fill(dt); // Add card_order information into DataTable
                ad.Fill(ds); // Add card_order information into DataSet

                // The buff of dataset can not be empty !
                if (ds != null && ds.Tables[0].Rows.Count > 0)  
                {
                    flag = true;   // Search successfully !
                }
                else
                {
                    flag = false; // Search unsuccessfully !
                }

                if (flag == true)  // This infor can be searched
                {
                    try
                    {
                        // Display the searching data on the DataGridView
                        DataGridView2.DataSource = dt;
                        // Assigns the [] value of row 0 of the 0th table in the cache                  
                        txtCouName.Text = ds.Tables[0].Rows[0][0].ToString();
                        txtPrice.Text = ds.Tables[0].Rows[0][1].ToString();
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
                else
                {
                    MessageBox.Show("The information is nonexistent ! \n Please try it again !", "Warninig", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                txtSearchCou.Text = "";
                txtSearchCou.Focus();
            }
        }

        //  Search the courses information
        private void BindDataGrid()
        {
            ConnDB();
            // Create an instance of System.Data.DataTable
            System.Data.DataTable dt = new DataTable();
            // Create a command
            string sql = "Select * from Course";
            // Execute the Mysql query command
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            // Create a new data adapter 
            MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
            try
            {
                ad.Fill(dt);
                // Display the infor and data on the DataGridView Table
                DataGridView2.DataSource = dt;
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

        /// <summary>
        /// Add the new courses and set the name and price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCou_Click(object sender, EventArgs e)
        {
            // The course name or course price can not be empty
            if (txtCouName.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Please complete the member personal information !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ConnDB();
                // Insert the new Course name and price
                string strsql = string.Format("Insert into Course(Course_Name, Price) values ('{0}', '{1}')", txtCouName.Text, txtPrice.Text);
                // Represents a transact-sql statement or stored procedure to execute against the MySQL Server database
                MySqlCommand cmd = new MySqlCommand(strsql, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                    // Show the new course on the Table
                    BindDataGrid();
                    MessageBox.Show("Add Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Course Information Adding Error !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
                Course_Nmae();
                // Refresh the combobox of Course name and price
                txtSearchCou.Text = "";
                txtCouName.Text = "";
                txtPrice.Text = "";
            }
        }

        // Clear and refresh the text contents
        private void picRefreshCou_Click(object sender, EventArgs e)
        {
            txtSearchCou.Text = "";
            txtPrice.Text = "";
            txtCouName.Text = "";
            DataGridView2.DataSource = null;
        }

        // Updata the combobox and connect the data-infor to display it
        private void picShowAllCou_Click(object sender, EventArgs e)
        {
            BindDataGrid();
        }

        /// <summary>
        /// Updata the Course setting --- Name and Price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateCou_Click(object sender, EventArgs e)
        {
            // If the textbox is null -- Can not to updata
            if (txtCouName.Text == "" || lblPrice.Text == "")
            {
                MessageBox.Show("Personal information cannot be empty ！", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ConnDB();
                // Create the update mysql command
                string query = string.Format("Update Course set Price = ('{0}') Where Course_Name = ('{1}')", txtPrice.Text, txtCouName.Text);
                // Connect and call the database for update  
                MySqlCommand cmd = new MySqlCommand(query, conn);                
                try
                {
                    // Execute the Mysql commmand
                    cmd.ExecuteNonQuery();
                    BindDataGrid(); // Display the data on the table list
                    MessageBox.Show("Update Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                Course_Nmae();
                conn.Close();               
            }
        }

        // Control availability switch
        private void ucSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            // The switch is close -- The controls can not use or input
            if (ucSwitch1.Checked == false)
            {
                txtCouName.ReadOnly = true;
                txtPrice.ReadOnly = true;
                btnAddCou.Enabled = false;
                btnDeleteCou.Enabled = false;
                btnUpdateCou.Enabled = false;
            }  
            else if (ucSwitch1.Checked == true)  // The switch is open -- The controls can use or input
            {
                txtCouName.ReadOnly = false;
                txtPrice.ReadOnly = false;
                btnAddCou.Enabled = true;
                btnDeleteCou.Enabled = true;
                btnUpdateCou.Enabled = true;
            }
        }

        /// <summary>
        /// Delete the Course information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCou_Click(object sender, EventArgs e)
        {
            ConnDB();
            // Get the number of selected rows
            int row = DataGridView2.SelectedRows.Count;
            // Both do not exist, not selected and searched
            if ((row <= 0) && (txtCouName.Text == ""))   
            {
                MessageBox.Show("There is no data to be deleted !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if ((row > 0) && (txtCouName.Text == "")) // The information in the table is selected but not searched
            {
                // Confirm delete if wanna delete this data
                DialogResult result = MessageBox.Show("Confirm to delete this data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                // Ensure to delete the data
                if (result == DialogResult.Yes)
                {
                    // Traverse the loop to find the data
                    for (int i = 0; i < row; i++)
                    {
                        // Select the cell of datagridview as primary key
                        string dgNum = DataGridView2.SelectedRows[i].Cells[0].Value.ToString();
                        // Delete the data by the selected primary key
                        string delstr = "Delete from Course where Course_Name=" + "'" + dgNum + "'";
                        MySqlCommand delecmd = new MySqlCommand(delstr, conn);
                        delecmd.ExecuteNonQuery();
                        MessageBox.Show("Delete course info successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // Updata the Course_Name from comboBox after delete the data
                    Course_Nmae();
                    conn.Close();                  
                    BindDataGrid();
                }
                Course_Nmae();
                txtSearchCou.Text = "";
                txtCouName.Text = "";
                txtPrice.Text = "";
            }
            else if ((row <= 0) && (txtCouName.Text != ""))  // searched for information but not selected
            {
                // Confirm if wanna delete this data
                DialogResult result = MessageBox.Show("Confirm to delete this data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                // Ensure to Delete the data
                if (result == DialogResult.Yes)
                {
                    if (DataGridView2.Rows.Count == 1)  // The searched information is not exist
                    {
                        MessageBox.Show("The entry " + txtSearch.Text + " is not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtSearchCou.Text = "";
                    }
                    else
                    {
                        ConnDB();
                        // Delete the Coures by the Course_Name
                        string strsql = "Delete from Course where Course_Name ='" + txtCouName.Text + "'";
                        MySqlCommand cmd = new MySqlCommand(strsql, conn);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Delete course info successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            picRefreshCou_Click(sender, e);
                        }
                        catch
                        {
                            MessageBox.Show("The " + txtSearchCou.Text + " entry is not exists! Retry!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        Course_Nmae();
                        conn.Close();
                        BindDataGrid();
                    }
                }
            }
            else    // Both of two condition is true
            {
                MessageBox.Show("Cannot delete two data at the same time !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        private void comboTraName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboMeName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Click on the data table to display relative data in the corresponding control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // int rows = e.RowIndex;
            string Course_Name = DataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtCouName.Text = Course_Name;

            string Price = DataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtPrice.Text = Price;
        }

        /// <summary>
        /// Click on the data table to display relative data in the corresponding control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string CouName = DataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboxCoName.Text = CouName;

            string CouMeName = DataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboMeName.Text = CouMeName;

            string CouTraName = DataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboTraName.Text = CouTraName;

            string dateTime = DataGridView1.CurrentRow.Cells[3].Value.ToString();
            datimpTime.Text = dateTime;

            string Times = DataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtTimes.Text = Times;

            string Price = DataGridView1.CurrentRow.Cells[5].Value.ToString();
            lblShowPrice.Text = Price;
        }

        private void groupCoInfor_Enter(object sender, EventArgs e)
        {

        }
    }
}
