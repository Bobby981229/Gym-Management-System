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
using DevExpress.XtraCharts;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace Gym_Management_System
{
    public partial class Report__Statistics : Form
    {

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
            // Open the connection
            try
            {
                conn.Open();//Mysql database did not start, this sentence error!
            }
            catch (Exception ex)
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


        public Report__Statistics()
        {
            InitializeComponent();
            Win32.AnimateWindow(this.Handle, 300, Win32.AW_CENTER | Win32.AW_ACTIVATE | Win32.AW_SLIDE);
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
                lblCourse.Text = "0";
                lblCard.Text = "0";
                lblCourseAmount.Text = "0";
                lblCardAmount.Text = "0";
            }
        }

        // Set the Series of the Chart
        private void Report__Statistics_Load(object sender, EventArgs e)
        {
            chartBarBMI.Series.Add("Hight");
            chartBarBMI.Series.Add("Weitht");
            chartExTime.Series.Add("Exercise Timers");
            chartSaleAmount.Series.Add("Course Sale");
            chartSaleAmount.Series.Add("Card Sale");
            chartSaleAmount.Series.Add("Sum");
            chartSaleVolume.Series.Add("Course Amount");
            chartSaleVolume.Series.Add("Card Amount");
            chartSaleVolume.Series.Add("Sum");
            DateCompare();  // Compare the start date and end date

            if (lblSumAmount.Text == "0" || lblSum.Text == "0")
            {
                lblCourseAmount.Text = "¥ 0";
                lblCardAmount.Text = "¥ 0";
                lblCourse.Text = "0";
                lblCard.Text = "0";
            }
        }

        // Display the data of BMI
        private void BarChartBMI()
        {
            ConnDB();
            // Set the width of the series (bar chart)
            chartBarBMI.Series[0]["PointWidth"] = "0.8";
            chartBarBMI.Series[1]["PointWidth"] = "0.8";
            chartBarBMI.Series[2]["PointWidth"] = "0.8";

            // Create the array list to store the data from database
            ArrayList listBMI = new ArrayList();
            ArrayList listName = new ArrayList();
            ArrayList listHight = new ArrayList();
            ArrayList listWeight = new ArrayList();

            // Create the SQL query command
            string strBMI = "Select BMI from BMI";
            string strName = "Select Name from BMI";
            string strHight = "Select Hight from BMI";
            string strWeight = "Select Weight from BMI";

            // Save the results of the query to the virtual database
            MySqlDataAdapter daBMI = new MySqlDataAdapter(strBMI, conn);
            MySqlDataAdapter daName = new MySqlDataAdapter(strName, conn);
            MySqlDataAdapter daHight = new MySqlDataAdapter(strHight, conn);
            MySqlDataAdapter daWeight = new MySqlDataAdapter(strWeight, conn);

            // Create a DataSet object (equivalent to creating a virtual database in the foreground)
            DataSet dsBMI = new DataSet();
            DataSet dsName = new DataSet();
            DataSet dsHight = new DataSet();
            DataSet dsWeight = new DataSet();

            // Store the results of the query into a virtual table in the virtual database ds
            daBMI.Fill(dsBMI);
            daName.Fill(dsName);
            daHight.Fill(dsHight);
            daWeight.Fill(dsWeight);

            // Copy the data of the data table tabuser to the DataTable object (take the data)
            DataTable dtBMI = dsBMI.Tables[0];
            DataTable dtName = dsName.Tables[0];
            DataTable dtHight = dsHight.Tables[0];
            DataTable dtWeight = dsWeight.Tables[0];

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drBMI in dtBMI.Rows)
            {
                // dr [0] means take the first column of the result
                listBMI.Add(drBMI[0].ToString().Trim());
            }

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drName in dtName.Rows)
            {
                // dr [0] means take the first column of the result
                listName.Add(drName[0].ToString().Trim());
            }

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drHight in dtHight.Rows)
            {
                // dr [0] means take the first column of the result
                listHight.Add(drHight[0].ToString().Trim());
            }

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drWeight in dtWeight.Rows)
            {
                // dr [0] means take the first column of the result
                listWeight.Add(drWeight[0].ToString().Trim());
            }

            // Display the current data on bar chart 
            chartBarBMI.Series[0].IsValueShownAsLabel = true;
            chartBarBMI.Series[1].IsValueShownAsLabel = true;
            chartBarBMI.Series[2].IsValueShownAsLabel = true;

            // Set the style of the series is Cylinder
            chartBarBMI.Series[0]["DrawingStyle"] = "Cylinder";
            chartBarBMI.Series[1]["DrawingStyle"] = "Cylinder";
            chartBarBMI.Series[2]["DrawingStyle"] = "Cylinder";

            // Bind data source
            this.chartBarBMI.Series[0].Points.DataBindXY(listName, listBMI); // Bind the horizontal and vertical coordinates of 0 to draw a bar chart
            this.chartBarBMI.Series[1].Points.DataBindXY(listName, listHight); // Bind the horizontal and vertical coordinates of 1 to draw a bar chart
            this.chartBarBMI.Series[2].Points.DataBindXY(listName, listWeight); // Bind the horizontal and vertical coordinates of 2 to draw a bar chart

            chartBarBMI.Series[0].ChartType = SeriesChartType.Column;// Plot type is a clustered column chart
            chartBarBMI.Series[1].ChartType = SeriesChartType.Column;// Plot type is a clustered column chart
            chartBarBMI.Series[2].ChartType = SeriesChartType.Column;// Plot type is a clustered column chart

            // Defines the color of the drawn cylinder
            this.chartBarBMI.Series[0].Color = Color.Red;
            this.chartBarBMI.Series[1].Color = Color.Blue;
            this.chartBarBMI.Series[2].Color = Color.DarkSalmon;
        }

        /// <summary>
        /// Display the sale of Card and Course
        /// </summary>
        private void SalePrice_Line()
        {
            ConnDB();
            int CourseAmount = 0;
            int CardAmount = 0;

            // Create the array list to store the data from database
            ArrayList listDate = new ArrayList();
            ArrayList listCourse = new ArrayList();
            ArrayList listCard = new ArrayList();
            ArrayList listSum = new ArrayList();

            // Create the SQL query command
            string strDate = string.Format("Select Date from course_sale where Date between '{0}'and '{1}'", this.dateTimeStart.Text.Trim(), this.dateTimeEnd.Text.Trim());
            string strCourse = string.Format("Select Profit from course_sale where Date between '{0}'and '{1}'", this.dateTimeStart.Text.Trim(), this.dateTimeEnd.Text.Trim());
            string strCard = string.Format("Select Profit from Card_Sale where Date between '{0}'and '{1}'", this.dateTimeStart.Text.Trim(), this.dateTimeEnd.Text.Trim());

            // Save the results of the query to the virtual database
            MySqlDataAdapter daDate = new MySqlDataAdapter(strDate, conn);
            MySqlDataAdapter daCourse = new MySqlDataAdapter(strCourse, conn);
            MySqlDataAdapter daCard = new MySqlDataAdapter(strCard, conn);

            // Create a DataSet object (equivalent to creating a virtual database in the foreground)
            DataSet dsDate = new DataSet();
            DataSet dsCourse = new DataSet();
            DataSet dsCard = new DataSet();

            // Store the results of the query into a virtual table in the virtual database ds
            daDate.Fill(dsDate);
            daCourse.Fill(dsCourse);
            daCard.Fill(dsCard);

            // Copy the data of the data table tabuser to the DataTable object (take the data)
            DataTable dtDate = dsDate.Tables[0];
            DataTable dtCourse = dsCourse.Tables[0];
            DataTable dtCard = dsCard.Tables[0];

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drDate in dtDate.Rows)
            {
                // dr [0] means take the first column of the result
                listDate.Add(drDate[0].ToString().Trim());
            }

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drCourse in dtCourse.Rows)
            {
                // dr [0] means take the first column of the result
                listCourse.Add(drCourse[0].ToString().Trim());
            }

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drCard in dtCard.Rows)
            {
                // dr [0] means take the first column of the result
                listCard.Add(drCard[0].ToString().Trim());
            }

            // Calculate the sum of the sale profit
            for(int i = 0; i < listCard.Count; i++)
            {
                listSum.Add(int.Parse(listCard[i].ToString()) + int.Parse(listCourse[i].ToString()));
                CardAmount += int.Parse(listCard[i].ToString());
                lblCardAmount.Text = "¥ " + CardAmount.ToString();
            }

            for (int j = 0; j < listCourse.Count; j++)
            {
                CourseAmount += int.Parse(listCourse[j].ToString());
                lblCourseAmount.Text = "¥ " + CourseAmount.ToString();
            }

            lblSumAmount.Text = "¥ " + (CardAmount + CourseAmount).ToString();

            // Define draw line graph
            chartSaleAmount.Series[0].ChartType = SeriesChartType.Line;
            chartSaleAmount.Series[1].ChartType = SeriesChartType.Line;
            chartSaleAmount.Series[2].ChartType = SeriesChartType.Line;

            // Set the line width
            chartSaleAmount.Series[0]["PointWidth"] = "3.0";
            chartSaleAmount.Series[1]["PointWidth"] = "3.0";
            chartSaleAmount.Series[2]["PointWidth"] = "3.0";

            // Set border width
            chartSaleAmount.Series[0].BorderWidth = 2;
            chartSaleAmount.Series[1].BorderWidth = 2;
            chartSaleAmount.Series[2].BorderWidth = 2;

            // Set the data to be displayed online
            chartSaleAmount.Series[0].IsValueShownAsLabel = true; 
            chartSaleAmount.Series[1].IsValueShownAsLabel = true;
            chartSaleAmount.Series[2].IsValueShownAsLabel = true;

            // Set data points label style
            chartSaleAmount.Series[0]["BarLabelStyle"] = "Center";
            chartSaleAmount.Series[1]["BarLabelStyle"] = "Center";
            chartSaleAmount.Series[2]["BarLabelStyle"] = "Center";

            // Graphic display mode
            chartSaleAmount.Series[0]["DrawingStyle"] = "Emboss";
            chartSaleAmount.Series[1]["DrawingStyle"] = "Emboss";
            chartSaleAmount.Series[2]["DrawingStyle"] = "Emboss";

            this.chartSaleAmount.ChartAreas[0].Area3DStyle.Enable3D = false;

            // Set the shape of the data points
            this.chartSaleAmount.Series[0].MarkerStyle = MarkerStyle.Diamond;
            this.chartSaleAmount.Series[1].MarkerStyle = MarkerStyle.Diamond;
            this.chartSaleAmount.Series[2].MarkerStyle = MarkerStyle.Diamond;

            // Set the size of the data points
            this.chartSaleAmount.Series[0].MarkerSize = 8;
            this.chartSaleAmount.Series[1].MarkerSize = 8;
            this.chartSaleAmount.Series[2].MarkerSize = 8;

            // Set the color of the data points
            this.chartSaleAmount.Series[0].MarkerColor = Color.Orange;
            this.chartSaleAmount.Series[1].MarkerColor = Color.Purple;
            this.chartSaleAmount.Series[2].MarkerColor = Color.Red;

            // Disassembly diagram
            this.chartSaleAmount.Series[0].ChartType = SeriesChartType.Line;
            this.chartSaleAmount.Series[1].ChartType = SeriesChartType.Line;
            this.chartSaleAmount.Series[2].ChartType = SeriesChartType.Line;

            // Bind the X and Y data from the database
            this.chartSaleAmount.Series[0].Points.DataBindXY(listDate, listCourse);
            this.chartSaleAmount.Series[1].Points.DataBindXY(listDate, listCard);
            this.chartSaleAmount.Series[2].Points.DataBindXY(listDate, listSum);

            // Set the color of the polyline
            this.chartSaleAmount.Series[0].Color = Color.Orange;
            this.chartSaleAmount.Series[1].Color = Color.Purple;
            this.chartSaleAmount.Series[2].Color = Color.Red;
        }


        // Display the sale amount of Card and Course
        private void SaleVolume_Line()
        {
            ConnDB();
            int CourseAmount = 0;
            int CardAmount = 0;

            // Create the array list to store the data from database
            ArrayList listDate = new ArrayList();
            ArrayList listCourse = new ArrayList();
            ArrayList listCard = new ArrayList();
            ArrayList listSum = new ArrayList();

            // Create the SQL query command

            string strDate = string.Format("select Date from course_sale where Date between '{0}'and '{1}'", this.dateTimeStart.Text.Trim(), this.dateTimeEnd.Text.Trim());
            string strCourse = string.Format("Select Volums from course_sale Where Date between '{0}'and '{1}'", this.dateTimeStart.Text.Trim(), this.dateTimeEnd.Text.Trim());
            string strCard = string.Format("Select Volums from Card_Sale Where Date between '{0}'and '{1}'", this.dateTimeStart.Text.Trim(), this.dateTimeEnd.Text.Trim());

            // string strDate = "Select Date from course_sale";
            //string strCourse = "Select Volums from course_sale";
            //string strCard = "Select Volums from Card_Sale";

            // Save the results of the query to the virtual database
            MySqlDataAdapter daDate = new MySqlDataAdapter(strDate, conn);
            MySqlDataAdapter daCourse = new MySqlDataAdapter(strCourse, conn);
            MySqlDataAdapter daCard = new MySqlDataAdapter(strCard, conn);

            // Create a DataSet object (equivalent to creating a virtual database in the foreground)
            DataSet dsDate = new DataSet();
            DataSet dsCourse = new DataSet();
            DataSet dsCard = new DataSet();

            // Store the results of the query into a virtual table in the virtual database ds
            daDate.Fill(dsDate);
            daCourse.Fill(dsCourse);
            daCard.Fill(dsCard);

            // Copy the data of the data table tabuser to the DataTable object (take the data)
            DataTable dtDate = dsDate.Tables[0];
            DataTable dtCourse = (dsCourse.Tables[0]);
            DataTable dtCard = (dsCard.Tables[0]);

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drDate in dtDate.Rows)
            {
                // dr [0] means take the first column of the result
                listDate.Add(drDate[0].ToString().Trim());
            }

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drCourse in dtCourse.Rows)
            {
                // dr [0] means take the first column of the result
                listCourse.Add(drCourse[0].ToString().Trim());
            }

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drCard in dtCard.Rows)
            {
                // dr [0] means take the first column of the result
                listCard.Add(drCard[0].ToString().Trim());
            }

            // Calculate the sum of the sale profit
            for (int i = 0; i < listCard.Count; i++)
            {
                listSum.Add(int.Parse(listCard[i].ToString()) + int.Parse(listCourse[i].ToString()));
                CardAmount += int.Parse(listCard[i].ToString());
                lblCard.Text = CardAmount.ToString();
            }

            for (int j = 0; j < listCourse.Count; j++)
            {
                CourseAmount += int.Parse(listCourse[j].ToString());
                lblCourse.Text = CourseAmount.ToString();
            }

            lblSum.Text = (CardAmount + CourseAmount).ToString();

            // Define draw line graph
            chartSaleVolume.Series[0].ChartType = SeriesChartType.Line;
            chartSaleVolume.Series[1].ChartType = SeriesChartType.Line;
            chartSaleVolume.Series[2].ChartType = SeriesChartType.Line;

            // Set the line width
            chartSaleVolume.Series[0]["PointWidth"] = "5.0"; 
            chartSaleVolume.Series[1]["PointWidth"] = "5.0";
            chartSaleVolume.Series[2]["PointWidth"] = "5.0";

            // Set border width
            chartSaleVolume.Series[0].BorderWidth = 2;
            chartSaleVolume.Series[1].BorderWidth = 2;
            chartSaleVolume.Series[2].BorderWidth = 2;

            // Set the data to display online
            chartSaleVolume.Series[0].IsValueShownAsLabel = true; 
            chartSaleVolume.Series[1].IsValueShownAsLabel = true;
            chartSaleVolume.Series[2].IsValueShownAsLabel = true;

            // Set data points label style
            chartSaleVolume.Series[0]["BarLabelStyle"] = "Center";
            chartSaleVolume.Series[1]["BarLabelStyle"] = "Center";
            chartSaleVolume.Series[2]["BarLabelStyle"] = "Center";

            // Graphics display embossed
            chartSaleVolume.Series[0]["DrawingStyle"] = "Emboss";
            chartSaleVolume.Series[1]["DrawingStyle"] = "Emboss";
            chartSaleVolume.Series[2]["DrawingStyle"] = "Emboss";

            this.chartSaleVolume.ChartAreas[0].Area3DStyle.Enable3D = false;

            // Set the shape of the data point
            this.chartSaleVolume.Series[0].MarkerStyle = MarkerStyle.Diamond;
            this.chartSaleVolume.Series[1].MarkerStyle = MarkerStyle.Diamond;
            this.chartSaleVolume.Series[2].MarkerStyle = MarkerStyle.Diamond;

            // Set the size of the data point
            this.chartSaleVolume.Series[0].MarkerSize = 8;
            this.chartSaleVolume.Series[1].MarkerSize = 8;
            this.chartSaleVolume.Series[2].MarkerSize = 8;

            // Sets the color of the data point
            this.chartSaleVolume.Series[0].MarkerColor = Color.Orange;
            this.chartSaleVolume.Series[1].MarkerColor = Color.Purple;
            this.chartSaleVolume.Series[2].MarkerColor = Color.Red;

            // Line chart
            this.chartSaleVolume.Series[0].ChartType = SeriesChartType.Line;
            this.chartSaleVolume.Series[1].ChartType = SeriesChartType.Line;
            this.chartSaleVolume.Series[2].ChartType = SeriesChartType.Line;

            // Bind the X and Y data from the database
            this.chartSaleVolume.Series[0].Points.DataBindXY(listDate, listCourse);
            this.chartSaleVolume.Series[1].Points.DataBindXY(listDate, listCard);
            this.chartSaleVolume.Series[2].Points.DataBindXY(listDate, listSum);

            // Sets the color of the line
            this.chartSaleVolume.Series[0].Color = Color.Orange;
            this.chartSaleVolume.Series[1].Color = Color.Purple;
            this.chartSaleVolume.Series[2].Color = Color.Red;
        }

        private void FillPieChart()
        {
            /*
            ConnDB();

            ArrayList listSitu = new ArrayList();
            ArrayList listBMI = new ArrayList();


            string strSitu = "Select Date from course_sale";
            string strBMI = "Select Volums from course_sale";

            MySqlDataAdapter daSitu = new MySqlDataAdapter(strSitu, conn);
            MySqlDataAdapter daBMI = new MySqlDataAdapter(strBMI, conn);

            // Create a DataSet object (equivalent to creating a virtual database in the foreground)
            DataSet dsSitu = new DataSet();
            DataSet dsBMI = new DataSet();

            daSitu.Fill(dsSitu);
            daBMI.Fill(dsBMI);

            DataTable dtSitu = dsSitu.Tables[0];
            DataTable dtBMI = dsBMI.Tables[0];

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drSitu in dtSitu.Rows)
            {
                // dr [0] means take the first column of the result
                listSitu.Add(drSitu[0].ToString().Trim());
            }

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drBMI in dtBMI.Rows)
            {
                // dr [0] means take the first column of the result
                listBMI.Add(drBMI[0].ToString().Trim());
            }
            */

            string[] x = new string[] { "A1", "A2", "A3" };
            int[] y = new int[] { 30, 78, 30 };
            // Real-time 3D picture
            this.chartBMIPie.ChartAreas[0].Area3DStyle.Enable3D = true;
            // Select chart type as pie chart
            this.chartBMIPie.Series[0].ChartType = SeriesChartType.Pie;
            //Bind xy data
            this.chartBMIPie.Series[0].Points.DataBindXY(x, y);
        }

        // Display the data of Exercise Time
        private void BarChartExTime()
        {
            ConnDB();
            // Set the width of the series (bar chart)
            chartExTime.Series[0]["PointWidth"] = "0.8";

            // Create the array list to store the data from database
            ArrayList listTimes = new ArrayList();
            ArrayList listName = new ArrayList();

            // Create the SQL query command
            string strTimes = "Select Exercise_Times from Card_Order";
            string strName = "Select Name from Card_Order";

            // Save the results of the query to the virtual database
            MySqlDataAdapter daTimes = new MySqlDataAdapter(strTimes, conn);
            MySqlDataAdapter daName = new MySqlDataAdapter(strName, conn);

            // Create a DataSet object (equivalent to creating a virtual database in the foreground)
            DataSet dsTimes = new DataSet();
            DataSet dsName = new DataSet();

            // Store the results of the query into a virtual table in the virtual database ds
            daTimes.Fill(dsTimes);
            daName.Fill(dsName);

            // Copy the data of the data table tabuser to the DataTable object (take the data)
            DataTable dtTimes = dsTimes.Tables[0];
            DataTable dtName = dsName.Tables[0];

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drBMI in dtTimes.Rows)
            {
                // dr [0] means take the first column of the result
                listTimes.Add(drBMI[0].ToString().Trim());
            }

            // Traverse line by line, fetch the data of each line
            foreach (DataRow drName in dtName.Rows)
            {
                // dr [0] means take the first column of the result
                listName.Add(drName[0].ToString().Trim());
            }

            // Display the current data on bar chart 
            chartExTime.Series[0].IsValueShownAsLabel = true;

            // Set the style of the series is Cylinder
            chartExTime.Series[0]["DrawingStyle"] = "Cylinder";

            // // Bind the horizontal and vertical coordinates of 0 to draw a bar chart
            this.chartExTime.Series[0].Points.DataBindXY(listName, listTimes); 

            // Plot type is a clustered column chart
            chartExTime.Series[0].ChartType = SeriesChartType.Column;

            // Defines the color of the drawn cylinder
            this.chartExTime.Series[0].Color = Color.Red;
        }

        private void CardOrder()
        {
            ConnDB(); // Connect the Database
            //Create an instance of system.data.datatable
            DataTable dtCard = new DataTable();
            // Create a command
            string sqlCard = string.Format("Select * from Card_Order where Registration_Date between '{0}'and '{1}'", this.dateTimeStart.Text.Trim(), this.dateTimeEnd.Text.Trim());

            // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
            // MySqlCommand cmd = new MySqlCommand(sqlCard, conn);
            MySqlDataAdapter adCard = new MySqlDataAdapter(sqlCard, conn);
            try
            {
                // Display the members data from the query in the DataGridView
                adCard.Fill(dtCard);
                DataGridViewTable.DataSource = dtCard;
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

        private void CourseOrder()
        {
            ConnDB(); // Connect the Database
            //Create an instance of system.data.datatable
            DataTable dtCourse = new DataTable();
            // Create a command
            string sqlCourse = string.Format("Select * from Course_Order where Open_Date between '{0}'and '{1}'", this.dateTimeStart.Text.Trim(), this.dateTimeEnd.Text.Trim());

            // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
            // MySqlCommand cmd = new MySqlCommand(sqlCard, conn);
            MySqlDataAdapter adCourse = new MySqlDataAdapter(sqlCourse, conn);
            try
            {
                // Display the members data from the query in the DataGridView
                adCourse.Fill(dtCourse);
                DataGridViewTable.DataSource = dtCourse;
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

        // Thw switch to show the sale of Card and Course
        private void switchSale_CheckedChanged(object sender, EventArgs e)
        {
            if (switchAmount.Checked == false)
            {
                chartSaleAmount.Visible = false;
                groupBoxAmount.Visible = false;
            }
            else if (switchAmount.Checked == true)
            {
                switchExTimes.Checked = false;
                switchBMI.Checked = false;
                switchVolume.Checked = false;
                switchCard.Checked = false;
                switchCourse.Checked = false;

                DataGridViewTable.Visible = false;
                chartExTime.Visible = false;
                chartSaleVolume.Visible = false;
                chartBarBMI.Visible = false;
                chartSaleAmount.Visible = true;
                groupBoxVolume.Visible = false;
                groupBoxAmount.Visible = true;
                SalePrice_Line();
            }
        }

        // Thw switch to show BMI and physic situation
        private void switchBMI_CheckedChanged(object sender, EventArgs e)
        {
            if (switchBMI.Checked == false)
            {
                chartBarBMI.Visible = false;
            }
            else if (switchBMI.Checked == true)
            {
                switchExTimes.Checked = false;
                switchAmount.Checked = false;
                switchVolume.Checked = false;
                switchCard.Checked = false;
                switchCourse.Checked = false;

                DataGridViewTable.Visible = false;
                chartExTime.Visible = false;
                chartSaleVolume.Visible = false;
                chartSaleAmount.Visible = false;
                chartBarBMI.Visible = true;
                groupBoxVolume.Visible = false;
                groupBoxAmount.Visible = false;
                BarChartBMI();
            }
        }

        // Thw switch to show sales quantity
        private void switchAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (switchVolume.Checked == false)
            {
                chartSaleVolume.Visible = false;
                groupBoxVolume.Visible = false;
            }
            else if (switchVolume.Checked == true)
            {
                switchExTimes.Checked = false;
                switchAmount.Checked = false;
                switchBMI.Checked = false;
                switchCard.Checked = false;
                switchCourse.Checked = false;

                DataGridViewTable.Visible = false;
                chartExTime.Visible = false;
                chartSaleAmount.Visible = false;
                chartBarBMI.Visible = false;
                chartSaleVolume.Visible = true;
                groupBoxVolume.Visible = true;
                groupBoxAmount.Visible = false;
                SaleVolume_Line();
            }
        }

        // Thw switch to show Member Exercise Times
        private void switchExTimes_CheckedChanged(object sender, EventArgs e)
        {          
            if (switchExTimes.Checked == false)
            {
                chartExTime.Visible = false;
            }
            else if (switchExTimes.Checked == true)
            {
                switchAmount.Checked = false;
                switchBMI.Checked = false;
                switchVolume.Checked = false;
                switchCard.Checked = false;
                switchCourse.Checked = false;

                DataGridViewTable.Visible = false;
                chartSaleVolume.Visible = false;
                chartSaleAmount.Visible = false;
                chartBarBMI.Visible = false;
                chartExTime.Visible = true;
                groupBoxVolume.Visible = false;
                groupBoxAmount.Visible = false;
                BarChartExTime();
            }
        }

        private void switchCard_CheckedChanged(object sender, EventArgs e)
        {
            if(switchCard.Checked == false)
            {
                DataGridViewTable.Visible = false;
            }
            else if(switchCard.Checked == true)
            {
                switchAmount.Checked = false;
                switchBMI.Checked = false;
                switchVolume.Checked = false;
                switchExTimes.Checked = false;
                switchCourse.Checked = false;
               
                chartSaleVolume.Visible = false;
                chartSaleAmount.Visible = false;
                chartBarBMI.Visible = false;
                chartExTime.Visible = false;
                DataGridViewTable.Visible = true;
                groupBoxVolume.Visible = false;
                groupBoxAmount.Visible = false;
                CardOrder();
            }
        }

        private void switchCourse_CheckedChanged(object sender, EventArgs e)
        {
            if (switchCourse.Checked == false)
            {
                DataGridViewTable.Visible = false;
            }
            else if (switchCourse.Checked == true)
            {
                switchAmount.Checked = false;
                switchBMI.Checked = false;
                switchVolume.Checked = false;
                switchExTimes.Checked = false;
                switchCard.Checked = false;

                chartSaleVolume.Visible = false;
                chartSaleAmount.Visible = false;
                chartBarBMI.Visible = false;
                chartExTime.Visible = false;
                DataGridViewTable.Visible = true;
                groupBoxVolume.Visible = false;
                groupBoxAmount.Visible = false;
                CourseOrder();
            }
        }

        // Return to the main page
        private void picLogo_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Sale Situation changed via start date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            SaleVolume_Line();
            SalePrice_Line();
            CardOrder();
            CourseOrder();
            DateCompare();  // Compare the start date and end date

            if(lblSumAmount.Text == "0" || lblSum.Text == "0")
            {
                lblCourseAmount.Text = "¥ 0";
                lblCardAmount.Text = "¥ 0";
                lblCourse.Text = "0";
                lblCard.Text = "0";
            }
        }

        // Sale Situation changed via end date
        private void dateTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            SaleVolume_Line();
            SalePrice_Line();
            CardOrder();
            CourseOrder();
            DateCompare();  // Compare the start date and end date

            if (lblSumAmount.Text == "0" || lblSum.Text == "0")
            {
                lblCourseAmount.Text = "¥ 0";
                lblCardAmount.Text = "¥ 0";
                lblCourse.Text = "0";
                lblCard.Text = "0";
            }
        }       
    }
}

