using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Gym_Management_System.SDKModels;
using Gym_Management_System.SDKUtil;
using Gym_Management_System.Utils;
using Gym_Management_System.Entity;
using System.IO;
using System.Configuration;
using System.Threading;
using AForge.Video.DirectShow;
using Gym_Management_System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using AForge.Video;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Data;

namespace Gym_Management_System
{
    public partial class Information_Management : Form
    {
        #region Connect to the database
        MySqlDataAdapter ad;
        DataSet ds;
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
            // Open the Connection
            try
            {
                conn.Open();//Mysql database did not start, this sentence error!
            }
            catch
            {
                lblTip.Text = "No Connection Established!";
            }
        }// Connect to database

        // Close the database
        private void CloseDB()
        {
            // close connection
            conn.Close();
        }
        #endregion

        // Query the whole members information
        private void BindDataGridMe()
        {
            ConnDB();
            //Create an instance of system.data.datatable
            System.Data.DataTable dt = new DataTable();
            // Create a command
            string sql = "Select * from Member";

            // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
            try
            {
                // Display the members data from the query in the DataGridView
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

        // Query the whole trainer information
        private void BindDataGridTra()
        {
            ConnDB();
            //Create an instance of system.data.datatable
            System.Data.DataTable dt = new DataTable();
            // Create a command to search trainer info
            string sql = "Select * from Trainer";
            // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
            try
            {
                // Display the trainer data from the query in the DataGridView
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

        #region Parameter definition
        /// <summary>
        /// Engine Handle
        /// </summary>
        private IntPtr pImageEngine = IntPtr.Zero;

        /// <summary>
        /// Save a list of comparison images
        /// </summary>
        private List<string> imagePathList = new List<string>();

        /// <summary>
        /// List of face features in left gallery
        /// </summary>
        private List<IntPtr> imagesFeatureList = new List<IntPtr>();

        /// <summary>
        /// Similarity
        /// </summary>
        private float threshold = 0.8f;

        /// <summary>
        /// Is it a binocular camera
        /// </summary>
        private bool isDoubleShot = false;

        /// <summary>
        /// RGB Camera index
        /// </summary>
        private int rgbCameraIndex = 0;
        /// <summary>
        /// IR Camera index
        /// </summary>
        private int irCameraIndex = 0;
        #endregion

        #region Correlation in video mode
        /// <summary>
        /// Video engine Handle
        /// </summary>
        private IntPtr pVideoEngine = IntPtr.Zero;
        /// <summary>
        /// RGB Video engine FR Handle dispose   FR and the picture engine is separated to reduce the problem of commandeering the engine
        /// </summary>
        private IntPtr pVideoRGBImageEngine = IntPtr.Zero;
        /// <summary>
        /// IR Video engine FR Handle dispose   FR and the picture engine is separated to reduce the problem of commandeering the engine
        /// </summary>
        private IntPtr pVideoIRImageEngine = IntPtr.Zero;
        /// <summary>
        /// Video input device information
        /// </summary>
        private FilterInfoCollection filterInfoCollection;
        /// <summary>
        /// RGB Camera equipment
        /// </summary>
        private VideoCaptureDevice rgbDeviceVideo;
        /// <summary>
        /// IR Camera equipment
        /// </summary>
        private VideoCaptureDevice irDeviceVideo;
        #endregion

        #region Initialization engine
        /// <summary>
        /// Initialization engine
        /// </summary>
        private void InitEngines()
        {
            //Read configuration file
            AppSettingsReader reader = new AppSettingsReader();
            string appId = (string)reader.GetValue("APP_ID", typeof(string));
            string sdkKey64 = (string)reader.GetValue("SDKKEY64", typeof(string));
            string sdkKey32 = (string)reader.GetValue("SDKKEY32", typeof(string));
            rgbCameraIndex = (int)reader.GetValue("RGB_CAMERA_INDEX", typeof(int));
            irCameraIndex = (int)reader.GetValue("IR_CAMERA_INDEX", typeof(int));
            // Determine the number of CPU bits
            var is64CPU = Environment.Is64BitProcess;
            if (string.IsNullOrWhiteSpace(appId) || string.IsNullOrWhiteSpace(is64CPU ? sdkKey64 : sdkKey32))
            {
                MessageBox.Show(string.Format("Please configure APP_ID and SDKKEY {0} first in the App.config configuration file!", is64CPU ? "64" : "32"));
                return;
            }

            ///Activate the engine online If there is an error:
            ///1. Please make sure that the SDK library downloaded from the official website has been placed in the corresponding bin
            ///2. The currently selected CPU is x86 or x64
            int retCode = 0;
            try
            {
                retCode = ASFFunctions.ASFActivation(appId, is64CPU ? sdkKey64 : sdkKey32);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unable to load DLL"))
                {
                    MessageBox.Show("Please put SDK related DLL into the folder under x86 or x64 corresponding to bin!");
                }
                else
                {
                    MessageBox.Show("Engine activation failed!");
                }
                return;
            }
            Console.WriteLine("Activate Result:" + retCode);

            // Initialization engine
            uint detectMode = DetectionMode.ASF_DETECT_MODE_IMAGE;
            // Detect the angle priority of the face in Video mode
            int videoDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_HIGHER_EXT;
            // Detect the angle priority of a face in Image mode
            int imageDetectFaceOrientPriority = ASF_OrientPriority.ASF_OP_0_ONLY;
            // The proportion of the human face in the picture. 
            //If you need to adjust the detection face size, please modify this value. The valid value is 2-32.
            int detectFaceScaleVal = 16;
            // Maximum number of faces to be detected
            int detectFaceMaxNum = 5;
            // Combination of detection functions that need to be initialized when the engine is initialized
            int combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_AGE | FaceEngineMask.ASF_GENDER | FaceEngineMask.ASF_FACE3DANGLE;
            // Initialize the engine. Normal value is 0. For other return values, please refer to-http://ai.arcsoft.com.cn/bbs/forum.php?mod=viewthread&tid=19&_dsign=dbad527e
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pImageEngine);
            Console.WriteLine("InitEngine Result:" + retCode);

            //  Initializes the face detection engine in video mode
            uint detectModeVideo = DetectionMode.ASF_DETECT_MODE_VIDEO;
            int combinedMaskVideo = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION;
            retCode = ASFFunctions.ASFInitEngine(detectModeVideo, videoDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMaskVideo, ref pVideoEngine);
            // FR video dedicated FR engine
            detectFaceMaxNum = 1;
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_LIVENESS;
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoRGBImageEngine);

            //  FR engine for IR video
            combinedMask = FaceEngineMask.ASF_FACE_DETECT | FaceEngineMask.ASF_FACERECOGNITION | FaceEngineMask.ASF_IR_LIVENESS;
            retCode = ASFFunctions.ASFInitEngine(detectMode, imageDetectFaceOrientPriority, detectFaceScaleVal, detectFaceMaxNum, combinedMask, ref pVideoIRImageEngine);

            Console.WriteLine("InitVideoEngine Result:" + retCode);

            initVideo();
        }
        #endregion

        //FilterInfoCollection videoDevices;
        public int selectedDeviceIndex = 0;

        private int compareFeature(IntPtr feature, out float similarity)
        {
            int result = -1;
            similarity = 0f;
            // If the face database is not empty, perform face matching
            if (imagesFeatureList != null && imagesFeatureList.Count > 0)
            {
                for (int i = 0; i < imagesFeatureList.Count; i++)
                {
                    // Invoke face matching method for matching
                    ASFFunctions.ASFFaceFeatureCompare(pVideoRGBImageEngine, feature, imagesFeatureList[i], ref similarity);
                    if (similarity >= threshold)
                    {
                        result = i;
                        break;
                    }
                }
            }
            return result;
        }


        public Information_Management()
        {
            InitializeComponent();
            Win32.AnimateWindow(this.Handle, 800, Win32.AW_BLEND | Win32.AW_ACTIVATE);
            CheckForIllegalCrossThreadCalls = false;
            // Initialization engine
            InitEngines();
            irVideoSource.Hide();
        }

        // Return to the main page
        private void picLogo_Click(object sender, EventArgs e)
        {
            // Close the camera device
            if (videoDev.IsRunning)
            {
                videoDev.SignalToStop();
            }
            this.Hide();
        }

        private void Information_Management_Load(object sender, EventArgs e)
        {           
            ConnDB();
            CloseDB();
        }

        private FaceTrackUnit trackRGBUnit = new FaceTrackUnit();
        private FaceTrackUnit trackIRUnit = new FaceTrackUnit();
        private Font font = new Font(FontFamily.GenericSerif, 10f, FontStyle.Bold);
        private SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        private SolidBrush blueBrush = new SolidBrush(Color.Blue);
        private bool isRGBLock = false;
        private MRECT allRect = new MRECT();
        private object rectLock = new object();

        #region RGB camera
        /// <summary>
        /// RGB camera Paint event, the image is displayed on the form, each frame image is obtained, and processed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoDev_Paint(object sender, PaintEventArgs e)
        {
            if (videoDev.IsRunning)
            {
                // Get the picture under the current RGB camera
                Bitmap bitmap = videoDev.GetCurrentVideoFrame();
                if (bitmap == null)
                {
                    return;
                }
                // Detect human faces and get Rect box
                ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pVideoEngine, bitmap);
                // Get the biggest face
                ASF_SingleFaceInfo maxFace = FaceUtil.GetMaxFace(multiFaceInfo);
                // Get the Rect
                MRECT rect = maxFace.faceRect;
                // Detect the largest face under the RGB camera
                Graphics g = e.Graphics;
                float offsetX = videoDev.Width * 1f / bitmap.Width;
                float offsetY = videoDev.Height * 1f / bitmap.Height;
                float x = rect.left * offsetX;
                float width = rect.right * offsetX - x;
                float y = rect.top * offsetY;
                float height = rect.bottom * offsetY - y;
                // Frame according to Rect
                g.DrawRectangle(Pens.Red, x, y, width, height);
                if (trackRGBUnit.message != "" && x > 0 && y > 0)
                {
                    //  Display the detection result of the previous frame on the page
                    g.DrawString(trackRGBUnit.message, font, trackRGBUnit.message.Contains("Living Body") ? blueBrush : yellowBrush, x, y - 15);
                }

                // Guaranteed to detect only one frame to prevent page freezes and other memory usage
                if (isRGBLock == false)
                {
                    isRGBLock = true;
                    // Asynchronous processing to extract feature values and comparisons, otherwise the page will compare cards
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        if (rect.left != 0 && rect.right != 0 && rect.top != 0 && rect.bottom != 0)
                        {
                            try
                            {
                                lock (rectLock)
                                {
                                    allRect.left = (int)(rect.left * offsetX);
                                    allRect.top = (int)(rect.top * offsetY);
                                    allRect.right = (int)(rect.right * offsetX);
                                    allRect.bottom = (int)(rect.bottom * offsetY);
                                }

                                bool isLiveness = false;

                                // It Is important to adjust the image data
                                ImageInfo imageInfo = ImageUtil.ReadBMP(bitmap);
                                if (imageInfo == null)
                                {
                                    return;
                                }
                                int retCode_Liveness = -1;
                                // RGB live detection
                                ASF_LivenessInfo liveInfo = FaceUtil.LivenessInfo_RGB(pVideoRGBImageEngine, imageInfo, multiFaceInfo, out retCode_Liveness);
                                // Judging test results
                                if (retCode_Liveness == 0 && liveInfo.num > 0)
                                {
                                    int isLive = MemoryUtil.PtrToStructure<int>(liveInfo.isLive);
                                    isLiveness = (isLive == 1) ? true : false;
                                }
                                if (imageInfo != null)
                                {
                                    MemoryUtil.Free(imageInfo.imgData);
                                }
                                if (isLiveness)
                                {
                                    // Extracting facial features
                                    IntPtr feature = FaceUtil.ExtractFeature(pVideoRGBImageEngine, bitmap, maxFace);
                                    float similarity = 0f;
                                    // Get comparison result
                                    int result = compareFeature(feature, out similarity);
                                    MemoryUtil.Free(feature);
                                    if (result > -1)
                                    {
                                        // Put the comparison result into the display message for the latest display
                                        trackRGBUnit.message = string.Format(" {0}号 {1},{2}", result, similarity, string.Format("RGB{0}", isLiveness ? " Living Body" : " Non-living Body"));
                                        if (isLiveness == true)
                                        {
                                            btnPhotograph.Enabled = true;
                                        }
                                        else
                                        {
                                            btnPhotograph.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        // Displays a message... if isLiveness is true - Living Body, else false - Non-Living Body
                                        trackRGBUnit.message = string.Format("RGB{0}", isLiveness ? " Living Body" : " Non-living Body");
                                        if (isLiveness == true)
                                        {
                                            btnPhotograph.Enabled = true;
                                        }
                                        else
                                        {
                                            btnPhotograph.Enabled = false;
                                        }
                                    }
                                }
                                else
                                {
                                    // Displays a message...
                                    trackRGBUnit.message = string.Format("RGB{0}", isLiveness ? " Living Body" : " Non-living Body");
                                    if (isLiveness == true)
                                    {
                                        btnPhotograph.Enabled = true;
                                    }
                                    else
                                    {
                                        btnPhotograph.Enabled = false;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                if (bitmap != null)
                                {
                                    bitmap.Dispose();
                                }
                                isRGBLock = false;
                            }
                        }
                        else
                        {
                            lock (rectLock)
                            {
                                allRect.left = 0;
                                allRect.top = 0;
                                allRect.right = 0;
                                allRect.bottom = 0;
                            }
                        }
                        isRGBLock = false;
                    }));
                }
            }
        }
        #endregion

        #region The camera plays the completion event
        /// <summary>
        /// The camera plays the completion event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        private void videoDev_PlayingFinished(object sender, ReasonToFinishPlaying reason)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                // chooseImgBtn.Enabled = true;
                // matchBtn.Enabled = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Camera initialization
        /// <summary>
        /// Camera initialization
        /// </summary>
        private void initVideo()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            // If no camera is available, the "Enable Camera" button is disabled, otherwise it is available
            if (filterInfoCollection.Count == 0)
            {
                btnConnect.Enabled = false;
            }
            else
            {
                btnConnect.Enabled = true;
            }
        }
        #endregion

        #region Camera button click event
        /// <summary>
        /// Camera button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            //Sit down at the beginning of the click to initialize the detection to prevent the camera from starting 
            // when the program starts, and unplug the camera before clicking the camera button
            initVideo();
            // Cameras must be available
            if (filterInfoCollection.Count == 0)
            {
                MessageBox.Show("Camera not detected, make sure the camera or driver is installed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (videoDev.IsRunning || irVideoSource.IsRunning)
            {
                btnConnect.Text = "Connect Camera";
                // Close the Camera
                if (irVideoSource.IsRunning)
                {
                    irVideoSource.SignalToStop();
                    irVideoSource.Hide();
                }
                if (videoDev.IsRunning)
                {
                    videoDev.SignalToStop();
                }
            }
            else
            {
                videoDev.Show();
                irVideoSource.Show();
                btnConnect.Text = "Close Camera";
                // Get the total number of filterInfoCollection
                int maxCameraCount = filterInfoCollection.Count;
                // If two different camera indexes are configured
                if (rgbCameraIndex != irCameraIndex && maxCameraCount >= 2)
                {
                    //RGB camera loading
                    rgbDeviceVideo = new VideoCaptureDevice(filterInfoCollection[rgbCameraIndex < maxCameraCount ? rgbCameraIndex : 0].MonikerString);
                    rgbDeviceVideo.VideoResolution = rgbDeviceVideo.VideoCapabilities[0];
                    videoDev.VideoSource = rgbDeviceVideo;
                    videoDev.Start();

                    //IR camera loading
                    irDeviceVideo = new VideoCaptureDevice(filterInfoCollection[irCameraIndex < maxCameraCount ? irCameraIndex : 0].MonikerString);
                    irDeviceVideo.VideoResolution = irDeviceVideo.VideoCapabilities[0];
                    irVideoSource.VideoSource = irDeviceVideo;
                    irVideoSource.Start();
                    // The dual camera flag is set to true
                    isDoubleShot = true;
                }
                else
                {
                    // Only RGB camera is on, IR camera controls are hidden
                    rgbDeviceVideo = new VideoCaptureDevice(filterInfoCollection[rgbCameraIndex <= maxCameraCount ? rgbCameraIndex : 0].MonikerString);
                    rgbDeviceVideo.VideoResolution = rgbDeviceVideo.VideoCapabilities[0];
                    videoDev.VideoSource = rgbDeviceVideo;
                    videoDev.Start();
                    irVideoSource.Hide();
                }
            }
        }
        #endregion

        #region Get the directory
        // Create the file and get the directory
        private string GetImagePath()
        {
            // Save photos in the root directory of the project
            string UserImages = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)
                         + Path.DirectorySeparatorChar.ToString() + "UserImages";
            // If the photo already exists, overwrite
            if (!Directory.Exists(UserImages))
            {
                Directory.CreateDirectory(UserImages);
            }
            return UserImages;
        }
        #endregion

        // Save the shot from the camera to the local location
        private void btnPhotograph_Click(object sender, EventArgs e)
        {
            // Ensure the camera equipment is open
            if (rgbDeviceVideo == null)
            {
                MessageBox.Show("Please connect the video first !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (txtMeName.Text == "" && txtTraName.Text == "")
            {
                MessageBox.Show("Please complete the infomation of user !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (videoDev.IsRunning)  // Member manage page
            {
                Bitmap bitmap = videoDev.GetCurrentVideoFrame();
                // Display screenshot image on picturebox
                picMeImage.Image = bitmap;
                picTraImage.Image = bitmap;
                GetImagePath();
                try
                {
                    // Save the member images
                    if (groboMeInfor.Visible == true)
                    {
                        // Photo save local address
                        string fileName1 = txtMeName.Text + ".jpg";
                        bitmap.Save(@"C:\Users\72946\Desktop\GU 2\UserImages\" + fileName1, ImageFormat.Jpeg);
                    }
                    else if (groboTraInfor.Visible == true)  // Store the trainers images
                    {
                        // Photo save local address
                        string fileName2 = txtTraName.Text + ".jpg";
                        bitmap.Save(@"C:\Users\72946\Desktop\GU 2\UserImages\" + fileName2, ImageFormat.Jpeg);
                    }
                }
                catch
                {

                }                        
            }
        }

        // Select the type of identification
        private void coboMeCerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When the swicth is open the info can changed
            if (switchEdit.Checked == true)
            {
                txtMeCertiNumber.ReadOnly = false;
            }
            else
            {
                txtMeCertiNumber.ReadOnly = true;
            }
            txtMeCertiNumber.Text = "";
            txtMeCertiNumber.Focus();

            if (comboxMeType.Text == "Whole-year Card")
            {
                txtAcincome.Text = "3000";
            }
            else if (comboxMeType.Text == "Half-year Card")
            {
                txtAcincome.Text = "2000";
            }
            else if (comboxMeType.Text == "Season Card")
            {
                txtAcincome.Text = "800";
            }
            else if (comboxMeType.Text == "Month Card")
            {
                txtAcincome.Text = "400";
            }

            // If the customer is a student -- 20% discount
            if (comboxMeType.Text == "Whole-year Card" && coboMeCerType.Text == "Student Card")
            {
                txtAcincome.Text = "2400";
            }
            else if (comboxMeType.Text == "Half-year Card" && coboMeCerType.Text == "Student Card")
            {
                txtAcincome.Text = "1600";
            }
            else if (comboxMeType.Text == "Season Card" && coboMeCerType.Text == "Student Card")
            {
                txtAcincome.Text = "640";
            }
            else if (comboxMeType.Text == "Month Card" && coboMeCerType.Text == "Student Card")
            {
                txtAcincome.Text = "320";
            }
        }

        // Display the amount to be paid according to the type of membership card
        private void comboxMeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the date of opening card
            datimpMeRegister.Value.ToString("yyyy-MM-dd");
            DateTime dt = datimpMeRegister.Value;
            if (comboxMeType.Text == "Whole-year Card")
            {
                lblShowPrice.Text = "3000";
                txtAcincome.Text = "3000";
                // Adjust the corresponding expiration date
                txtDeadline.Text = dt.AddYears(1).ToString("yyyy-MM-dd");
            }
            else if (comboxMeType.Text == "Half-year Card")
            {
                lblShowPrice.Text = "2000";
                txtAcincome.Text = "2000";
                // Adjust the corresponding expiration date
                txtDeadline.Text = dt.AddMonths(6).ToString("yyyy-MM-dd");
            }
            else if (comboxMeType.Text == "Season Card")
            {
                lblShowPrice.Text = "800";
                txtAcincome.Text = "800";
                // Adjust the corresponding expiration date
                txtDeadline.Text = dt.AddMonths(3).ToString("yyyy-MM-dd");
            }
            else if (comboxMeType.Text == "Month Card")
            {
                lblShowPrice.Text = "400";
                txtAcincome.Text = "400";
                // Adjust the corresponding expiration date
                txtDeadline.Text = dt.AddDays(30).ToString("yyyy-MM-dd");
            }

            // If the customer is a student -- 20% discount
            if (comboxMeType.Text == "Whole-year Card" && coboMeCerType.Text == "Student Card")
            {
                txtAcincome.Text = "2400";
            }
            else if (comboxMeType.Text == "Half-year Card" && coboMeCerType.Text == "Student Card")
            {
                txtAcincome.Text = "1600";
            }
            else if (comboxMeType.Text == "Season Card" && coboMeCerType.Text == "Student Card")
            {
                txtAcincome.Text = "640";
            }
            else if (comboxMeType.Text == "Month Card" && coboMeCerType.Text == "Student Card")
            {
                txtAcincome.Text = "320";
            }
        }

        // Select the date of registering membership card
        private void datimpMeRegister_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = datimpMeRegister.Value;
            // Adjust the corresponding expiration date
            if (comboxMeType.Text == "Whole-year Card")
            {
                txtDeadline.Text = dt.AddYears(1).ToString("yyyy-MM-dd");
            }
            else if (comboxMeType.Text == "Half-year Card")
            {
                txtDeadline.Text = dt.AddMonths(6).ToString("yyyy-MM-dd");
            }
            else if (comboxMeType.Text == "Season Card")
            {
                txtDeadline.Text = dt.AddMonths(3).ToString("yyyy-MM-dd");
            }
            else if (comboxMeType.Text == "Month Card")
            {
                txtDeadline.Text = dt.AddDays(30).ToString("yyyy-MM-dd");
            }
        }

        // Change the management page 
        private void comBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Change to Member info manage page
            if (comBoxItems.Text == "Member")
            {
                groboMeInfor.Visible = true;
                groboMeInfor.Enabled = true;
                picDeadline.Visible = true;
                groboTraInfor.Visible = false;
                groboTraInfor.Enabled = false;
                switchEdit.Checked = false;
                picMeImage.Image = null;
                clearContents();
                txtSearch.Text = "";
                txtSearch.Focus();
            }// Change to Trainer info manage page
            else if (comBoxItems.Text == "Trainer")
            {
                groboMeInfor.Visible = false;
                groboMeInfor.Enabled = false;
                picDeadline.Visible = false;
                groboTraInfor.Visible = true;
                groboTraInfor.Enabled = true;
                switchEdit.Checked = false;
                picTraImage.Image = null;
                clearContents();
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }

        // Control availability switch
        private void switchEdit_CheckedChanged(object sender, EventArgs e)
        {
            // The switch is close -- The controls can not use or input
            if (switchEdit.Checked == false && comBoxItems.Text == "Trainer")
            {
                txtTraAddress.ReadOnly = true;
                txtTraCertiNumber.ReadOnly = true;
                txtTraEmail.ReadOnly = true;
                txtTraName.ReadOnly = true;
                txtTraNumber.ReadOnly = true;
                txtTraTel.ReadOnly = true;
                coboTraCerType.Enabled = false;
                coboTraGender.Enabled = false;
                datimpTraBirth.Enabled = false;
                datimpTraRegister.Enabled = false;
                btnPhotograph.Enabled = false;
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
            } // The switch is open -- The controls can use or input
            else if (switchEdit.Checked == true && comBoxItems.Text == "Trainer")
            {
                txtTraAddress.ReadOnly = false;
                txtTraCertiNumber.ReadOnly = false;
                txtTraEmail.ReadOnly = false;
                txtTraName.ReadOnly = false;
                txtTraNumber.ReadOnly = false;
                txtTraTel.ReadOnly = false;
                coboTraCerType.Enabled = true;
                coboTraGender.Enabled = true;
                datimpTraBirth.Enabled = true;
                datimpTraRegister.Enabled = true;
                btnPhotograph.Enabled = true;
                btnSave.Enabled = true;
                btnDelete.Enabled = true;
            }// The switch is close -- The controls can not use or input
            else if (switchEdit.Checked == false && comBoxItems.Text == "Member")
            {
                txtMeAddress.ReadOnly = true;
                txtMeCertiNumber.ReadOnly = true;
                txtMeEmail.ReadOnly = true;
                txtMeName.ReadOnly = true;
                txtMeNumber.ReadOnly = true;
                txtMeTel.ReadOnly = true;
                txtConsultant.ReadOnly = true;
                coboMeCerType.Enabled = false;
                coboMeGender.Enabled = false;
                comboxMeType.Enabled = false;
                datimpMeBirth.Enabled = false;
                datimpMeRegister.Enabled = false;
                btnPhotograph.Enabled = false;
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
            }  // The switch is open -- The controls can use or input
            else if (switchEdit.Checked == true && comBoxItems.Text == "Member")
            {
                txtMeAddress.ReadOnly = false;
                //txtMeCertiNumber.ReadOnly = false;
                txtMeEmail.ReadOnly = false;
                txtMeName.ReadOnly = false;
                // txtMeNumber.ReadOnly = false;
                txtMeTel.ReadOnly = false;
                txtConsultant.ReadOnly = false;
                coboMeCerType.Enabled = true;
                coboMeGender.Enabled = true;
                comboxMeType.Enabled = true;
                datimpMeBirth.Enabled = true;
                datimpMeRegister.Enabled = true;
                btnPhotograph.Enabled = true;
                btnSave.Enabled = true;
                btnDelete.Enabled = true;

            }
        }

        private void txtMeTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only numbers can be entered
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMeTel_TextChanged(object sender, EventArgs e)
        {
            string phone = txtMeTel.Text;
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
                picMeTelError.Visible = false;
            }
            else
            {
                picMeTelError.Visible = true;
            }

            if (txtMeTel.Text == "")
            {
                // The error icon disappear
                picMeTelError.Visible = false;
            }
        }

        private void txtMeEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Forbid Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5)) //禁止输入汉字
            {
                e.Handled = true;
            }
        }

        // Judge the Email format whether is correct 
        private void txtMeEmail_TextChanged(object sender, EventArgs e)
        {
            string Eamil = txtMeEmail.Text;
            // Mailbox regular expression
            string regEmai = "\\w{1,}@\\w{1,}\\.com";
            Regex regex = new Regex(regEmai);

            // Mailbox filling conforms to the regular expression
            if (regex.IsMatch(Eamil)) 
            {
                // The error icon disappear
                picMEemailError.Visible = false;
            }
            else
            {
                picMEemailError.Visible = true;
            }

            if (txtMeEmail.Text == "")
            {
                // The error icon disappear
                picMEemailError.Visible = false;
            }
        }

        private void txtMeNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only numbers and letters can be entered
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
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
                txtSearch.Focus();
            }
        }

        private void txtMeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can only enter English Characters
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtConsultant_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can only enter English Characters
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtMeCertiNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can only enter numbers
            if (e.KeyChar < '0' || e.KeyChar > '9')
            {
                e.Handled = true;
            }

            // If the 18th digit of the ID is entered as x or X, it is not blocked
            if ((txtMeCertiNumber.SelectionStart == 17) && (e.KeyChar == 'x' || e.KeyChar == 'X'))
            {
                e.Handled = false;
            }

            if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }

        // Judge the ID Number format whether is correct
        private void txtMeCertiNumber_TextChanged(object sender, EventArgs e)
        {
            if (coboMeCerType.Text == "Identity Card")
            {
                string icd = txtMeCertiNumber.Text;
                try
                {
                    // ID Number regular expression
                    Regex reg = new Regex(@"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$");
                    if (reg.IsMatch(icd))
                    {
                        int iSum = 0;
                        for (int i = 17; i >= 0; i--)
                        {
                            iSum += (int)(System.Math.Pow(2, i) % 11) * int.Parse(icd[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);
                        }
                        if (iSum % 11 == 1)
                        {
                            // Verify date of birth
                            string Mybirth = icd.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                            DateTime Mytime = new DateTime();
                            if (DateTime.TryParse(Mybirth, out Mytime))
                            {
                                if (Mytime.Year > 1940)
                                {
                                    picMeCerNumber.Visible = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        picMeCerNumber.Visible = true;
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                picMeCerNumber.Visible = false;
            }

            if (txtMeCertiNumber.Text == "")
            {
                picMeCerNumber.Visible = false;
            }
        }

        private void coboMeGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            datimpMeBirth.Focus();
        }

        private void txtMeAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Forbid Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5))
            {
                e.Handled = true;
            }
        }

        private void txtTraTel_TextChanged(object sender, EventArgs e)
        {
            string phone = txtTraTel.Text;
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
                picTraTelError.Visible = false;  
            }
            else
            {
                picTraTelError.Visible = true;
            }

            if (txtTraTel.Text == "")
            {
                picTraTelError.Visible = false;
            }
        }

        // Judge the Email format whether is correct 
        private void txtTraEmail_TextChanged(object sender, EventArgs e)
        {
            string Eamil = txtTraEmail.Text;
            // Mailbox regular expression
            string regEmai = "\\w{1,}@\\w{1,}\\.com";
            Regex regex = new Regex(regEmai);

            // Mailbox filling conforms to the regular expression
            if (regex.IsMatch(Eamil))   
            {
                // The error icon disappear
                picTraEmailError.Visible = false;
            }
            else
            {
                picTraEmailError.Visible = true;
            }

            if (txtTraEmail.Text == "")
            {
                // The error icon disappear
                picTraEmailError.Visible = false;
            }
        }

        // Judge the ID Number format whether is correct 
        private void txtTraCertiNumber_TextChanged(object sender, EventArgs e)
        {
            if (coboTraCerType.Text == "Identity Card")
            {
                string icd = txtTraCertiNumber.Text;
                try
                {
                    // ID Number regular expression
                    Regex reg = new Regex(@"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$");
                    if (reg.IsMatch(icd))
                    {
                        int iSum = 0;
                        for (int i = 17; i >= 0; i--)
                        {
                            iSum += (int)(System.Math.Pow(2, i) % 11) * int.Parse(icd[17 - i].ToString(), System.Globalization.NumberStyles.HexNumber);
                        }
                        if (iSum % 11 == 1)
                        {
                            // Verify date of birth
                            string Mybirth = icd.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                            DateTime Mytime = new DateTime();
                            if (DateTime.TryParse(Mybirth, out Mytime))
                            {
                                if (Mytime.Year > 1940)
                                {
                                    picTraCerNumber.Visible = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        picTraCerNumber.Visible = true;
                    }
                }
                catch
                {
                }
            }
            else
            {
                picTraCerNumber.Visible = false;
            }

            if (txtTraCertiNumber.Text == "")
            {
                picTraCerNumber.Visible = false;
            }
        }

        private void coboTraCerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // txtTraCertiNumber.ReadOnly = false;
            txtTraCertiNumber.Text = "";
            txtTraCertiNumber.Focus();
        }

        /// <summary>
        /// Query member or trainer info
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

                // Judge the current object of role
                if (groboMeInfor.Visible == true || comBoxItems.Text == "Member") 
                {
                    bool flag;  // Judge the DataSet if is null
                    ds = new DataSet();  // New a DataSet and pull the data into buffer for sql command
                    DataSet ds1 = new DataSet();  // New a DataSet and pull the data into buffer for sql4 command
                    string sql = "Select * from Member where Number ='" + txtSearch.Text + "'"; // Search the ingormation by Number of user in Member table
                    string sql4 = "Select * from Card_order where Number='" + txtSearch.Text + "'"; // Search the ingormation by Number of user in Card_order table

                    System.Data.DataTable dt = new DataTable();  // Create a new DataTable to show the detail information
                    ad = new MySqlDataAdapter(sql, conn);  // Add a Data_adapter for the data by sql command.... In order to display data on the form
                    MySqlDataAdapter ad1 = new MySqlDataAdapter(sql4, conn); // // Add a Data_adapter for the data by sql4 command....
                    ad.Fill(dt);  // Single query and show on the DataGridView
                    ad.Fill(ds);  // ADD DATA INTO DATASET BUFFER
                    ad1.Fill(ds1);  // Search Card_Order form

                    if ((ds != null && ds.Tables[0].Rows.Count > 0) || (ds1 != null && ds1.Tables[0].Rows.Count > 0))  // The buff of dataset can not be empty !
                    {
                        flag = true;  // Search successfully !
                    }
                    else
                    {
                        string sql2 = "Select * from Member where Name ='" + txtSearch.Text + "'";   // Search member infor by the Name in Member table
                        string sql5 = "Select * from Card_order where Name='" + txtSearch.Text + "'";  // Search the ingormation by Name of user in Card_order table
                        
                        ad = new MySqlDataAdapter(sql2, conn);  // Create a new Data_adapter and run the sql2 command -- Member Form
                        ad.Fill(dt);  // Fill Data_adapter from datatable
                        ad1 = new MySqlDataAdapter(sql5, conn); // Create a new Data_adapter and run the sql5 command -- Card_Order Form
                        ad.Fill(ds);  // Fill Data_adapter from buffer - DataSet -- Member Form
                        ad1.Fill(ds1);  // Fill Data_adapter from buffer - DataSet --  Card_Order Form

                        // The buff of dataset can not be empty !
                        if ((ds != null && ds.Tables[0].Rows.Count > 0) || (ds1 != null && ds1.Tables[0].Rows.Count > 0)) 
                        {
                            flag = true;  // Search successfully !
                        }
                        else
                        {
                            // Search member infor by the phone number in Member table
                            string sql3 = "Select * from Member where TelNum ='" + txtSearch.Text + "'"; 
                            ad = new MySqlDataAdapter(sql3, conn);  // Create a new data adapter to run sql3 command in Mysql 
                            ad.Fill(dt); // The Data adapter is filled by data of Datatable
                            ad.Fill(ds); // The Data adapter is filled by data buff of DataSet

                            if (ds != null && ds.Tables[0].Rows.Count > 0)   // The buff of dataset can not be empty !
                            {
                                flag = true;  // Search successfully !
                            }
                            else
                            {
                                flag = false;    // Search unsuccessfully !
                            }
                        }
                    }

                    if (flag == true)  // This infor can be searched
                    {
                        try
                        {
                            DataGridView1.DataSource = dt; // Display the searching data on the DataGridView
                            // Assigns the [] value of row 0 of the 0th table in the cache from DataSet
                            txtMeNumber.Text = ds.Tables[0].Rows[0][1].ToString();
                            txtMeName.Text = ds.Tables[0].Rows[0][2].ToString();
                            txtMeTel.Text = ds.Tables[0].Rows[0][3].ToString();
                            txtMeEmail.Text = ds.Tables[0].Rows[0][4].ToString();
                            coboMeGender.Text = ds.Tables[0].Rows[0][5].ToString();
                            datimpMeBirth.Text = ds.Tables[0].Rows[0][6].ToString();
                            coboMeCerType.Text = ds.Tables[0].Rows[0][7].ToString();
                            txtMeCertiNumber.Text = ds.Tables[0].Rows[0][8].ToString();
                            txtMeAddress.Text = ds.Tables[0].Rows[0][9].ToString();
                            datimpMeRegister.Text = ds.Tables[0].Rows[0][10].ToString();
                            txtDeadline.Text = ds.Tables[0].Rows[0][11].ToString();
                            txtConsultant.Text = ds.Tables[0].Rows[0][12].ToString();
                            string pic = ds.Tables[0].Rows[0][13].ToString();
                            this.picMeImage.Image = Image.FromFile(pic);
                            // Show the Card_Order information from DataSet 2
                            comboxMeType.Text = ds1.Tables[0].Rows[0][3].ToString();
                            txtAcincome.Text = ds1.Tables[0].Rows[0][4].ToString();
                            txtSearch.Text = "";
                        }
                        catch
                        {
                            // MessageBox.Show("Mysql Error ！");
                        }
                        finally
                        {
                            conn.Close();  //The connect is closed !
                        }

                    }
                    else
                    {
                        MessageBox.Show("The member information is non-existent ! \n Please try it again !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                        // The information is nonexistent
                    }
                }
                else if (groboTraInfor.Visible == true || comBoxItems.Text == "Trainer")
                {
                    bool flag1;  // Judge the DataSet if is null
                    ds = new DataSet();  // New a DataSet and pull the data into buffer for sql command
                    string sql6 = "Select * from Trainer where Number ='" + txtSearch.Text + "'"; // Write the mysql statement for the search and assign the value

                    System.Data.DataTable dt = new DataTable();  // Create a new DataTable to show the detail information
                    MySqlDataAdapter ad2 = new MySqlDataAdapter(sql6, conn);   // Add a Data_adapter for the data by sql6 command.... In order to display data on the form
                    ad2.Fill(dt); // Add card_order information into DataTable
                    ad2.Fill(ds);   // Add card_order information into DataSet
                    if (ds != null && ds.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                    {
                        flag1 = true;   // Search successfully !
                    }
                    else
                    {
                        string sql7 = "Select * from Trainer where Name ='" + txtSearch.Text + "'";
                        ad2 = new MySqlDataAdapter(sql7, conn);     // Add a Data_adapter for the data by sql7 command
                        ad2.Fill(dt);   // Fill Data_adapter from datatable
                        ad2.Fill(ds);   // Fill Data_adapter from buffer - DataSet -- Trainer Form
                        if (ds != null && ds.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                        {
                            flag1 = true;   // Search successfully !
                        }
                        else
                        {
                            string sql8 = "Select * from Trainer where TelNum ='" + txtSearch.Text + "'";
                            ad2 = new MySqlDataAdapter(sql8, conn);   // Add a Data_adapter for the data by sql8 command
                            ad2.Fill(dt);   // Fill Data_adapter from datatable
                            ad2.Fill(ds);   // Fill Data_adapter from buffer - DataSet -- Trainer Form
                            if (ds != null && ds.Tables[0].Rows.Count > 0)    // The buff of dataset can not be empty !
                            {
                                flag1 = true;   // Search successfully !
                            }
                            else
                            {
                                flag1 = false; // Search unsuccessfully !
                            }
                        }
                    }

                    if (flag1 == true)  // This infor can be searched
                    {
                        try
                        {
                            // Display the searching data on the DataGridView
                            DataGridView1.DataSource = dt;
                            // Assigns the [] value of row 0 of the 0th table in the cache
                            txtTraNumber.Text = ds.Tables[0].Rows[0][1].ToString();
                            txtTraName.Text = ds.Tables[0].Rows[0][2].ToString();
                            txtTraTel.Text = ds.Tables[0].Rows[0][3].ToString();
                            txtTraEmail.Text = ds.Tables[0].Rows[0][4].ToString();
                            coboTraGender.Text = ds.Tables[0].Rows[0][5].ToString();
                            datimpTraBirth.Text = ds.Tables[0].Rows[0][6].ToString();
                            coboTraCerType.Text = ds.Tables[0].Rows[0][7].ToString();
                            txtTraCertiNumber.Text = ds.Tables[0].Rows[0][8].ToString();
                            txtTraAddress.Text = ds.Tables[0].Rows[0][9].ToString();
                            datimpTraRegister.Text = ds.Tables[0].Rows[0][10].ToString();
                            string pic = ds.Tables[0].Rows[0][11].ToString();
                            this.picTraImage.Image = Image.FromFile(pic);
                        }
                        catch
                        {
                            // MessageBox.Show("Mysql Error ！");
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("The trainer information is non-existent ! \n Please try it again !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            txtSearch.Text = "";
        }

        // Clear the contents text of controls
        public void clearContents()
        {
            // Clear the Member add page text contents
            if (groboMeInfor.Visible == true)
            {
                lblShowPrice.Text = "";
                txtAcincome.Text = "";
                txtMeNumber.Text = "";
                txtMeName.Text = "";
                txtMeTel.Text = "";
                txtMeEmail.Text = "";
                txtMeCertiNumber.Text = "";
                txtMeAddress.Text = "";
                txtDeadline.Text = "";
                txtConsultant.Text = "";
                DataGridView1.DataSource = null;
                picMeImage.Image = null;
            }
            else  // Clear the Trainer add page text contents
            {
                txtTraNumber.Text = "";
                txtTraName.Text = "";
                txtTraTel.Text = "";
                txtTraEmail.Text = "";
                txtTraCertiNumber.Text = "";
                txtTraAddress.Text = "";
                DataGridView1.DataSource = null;
                picTraImage.Image = null;
            }
        }

        // Refresh the page and clear the text contents
        private void picRefresh_Click(object sender, EventArgs e)
        {
            clearContents();
        }

        /// <summary>
        /// Show all Member personal information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picShowAll_Click(object sender, EventArgs e)
        {
            clearContents();
            // Show the Member information
            if (groboMeInfor.Visible == true || comBoxItems.Text == "Member")
            {
                if (DataGridView1.Rows.Count == 1)
                {
                    MessageBox.Show("The database is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else 
                {
                    // Create an instance of System.Data.DataTable
                    System.Data.DataTable dt = new DataTable();
                    // Create the query command to search Member
                    string sql = "Select * from Member";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    // Show the information on DataGridView
                    BindDataGridMe();
                }
            }   // Show the Trainer information
            else if (groboTraInfor.Visible == true || comBoxItems.Text == "Trainer")
            {
                if (DataGridView1.Rows.Count == 1)
                {
                    MessageBox.Show("The database is empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    // Create an instance of System.Data.DataTable
                    System.Data.DataTable dt = new DataTable();
                    // Create the query command to search trainer
                    string sql = "Select * from Trainer";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    BindDataGridTra();
                }
            }
        }

        /// <summary>
        /// Save information data to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ConnDB();  // Connect the database
            // When trainer infor page is show, save the data into member database
            if (groboTraInfor.Visible == true)
            {
                // Ensure the important information should be filled
                if (txtTraNumber.Text == "" || txtTraName.Text == "" || txtTraTel.Text == "" || txtTraEmail.Text == "" || coboTraGender.Text == "" || txtTraCertiNumber.Text == "" || picTraImage.Image == null)
                {
                    MessageBox.Show("Please complete the trainer personal information !", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (picTraTelError.Visible == true || picTraEmailError.Visible == true || picTraCerNumber.Visible == true)   // If the Error icons are showing, the data can not to store in database
                {
                    MessageBox.Show("There is an error in trainer personal information !", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {   
                    // Photo storage address
                    string picTraURL = @"C:\\Users\\72946\\Desktop\\GU 2\\UserImages\\" + txtTraName.Text + ".jpg";

                    // Updata the trainer' personal information
                    string query = string.Format("Update Trainer set Name = ('{0}'), TelNum = ('{1}'), Email = ('{2}'), Gender = ('{3}'), Birthday = ('{4}'), Certificate_Type = ('{5}'),  Certificate_Number = ('{6}'),  Address = ('{7}'), Register_Date = ('{8}'), PicURL = ('{9}') where Number = ('{10}')"
                        , txtTraName.Text, txtTraTel.Text, txtTraEmail.Text, coboTraGender.Text, datimpTraBirth.Text, coboTraCerType.Text, txtTraCertiNumber.Text, txtTraAddress.Text, datimpTraRegister.Text, picTraURL, txtTraNumber.Text);
                    try
                    {
                        // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Update Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Trainer Information Update Error !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    BindDataGridTra();
                    conn.Close();
                }
            }
            else if (groboMeInfor.Visible == true)   // When member infor page is show, save the data into member database
            {
                // Ensure the important information should be filled
                if (txtMeNumber.Text == "" || txtMeName.Text == "" || txtMeTel.Text == "" || txtMeEmail.Text == "" || coboMeGender.Text == "" || txtMeCertiNumber.Text == "" || txtDeadline.Text == "" || comboxMeType.Text == "" || picMeImage.Image == null)
                {
                    MessageBox.Show("Please complete the member personal information !", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (picMeTelError.Visible == true || picMEemailError.Visible == true || picMeCerNumber.Visible == true)   // If the Error icons are showing, the data can not to store in database
                {
                    MessageBox.Show("There is an error in member personal information !", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    // Photo storage address
                    string picMeURL = @"C:\\Users\\72946\\Desktop\\GU 2\\UserImages\\" + txtMeName.Text + ".jpg";

                    // Updata the members' personal information
                    string query1 = string.Format("Update Member set Name = ('{0}'), TelNum = ('{1}'), Email = ('{2}'), Gender = ('{3}'), Birthday = ('{4}'), Certificate_Type = ('{5}'),  Certificate_Number = ('{6}'),  Address = ('{7}'), Register_Date = ('{8}'), Deadline = ('{9}'), Consultant = ('{10}'), PicURL = ('{11}') where Number = ('{12}')"
                        , txtMeName.Text, txtMeTel.Text, txtMeEmail.Text, coboMeGender.Text, datimpMeBirth.Text, coboMeCerType.Text, txtMeCertiNumber.Text, txtMeAddress.Text, datimpMeRegister.Text, txtDeadline.Text, txtConsultant.Text, picMeURL, txtMeNumber.Text);
                    // Updata the member card information
                    string query2 = string.Format("Update Card_order set Name = ('{0}'), Registration_Date = ('{1}'), Membe_Type = ('{2}'), Price = ('{3}'), Consultant  = ('{4}') where Number = ('{5}')" , txtMeName.Text, datimpMeRegister.Text, comboxMeType.Text, txtAcincome.Text, txtConsultant.Text, txtMeNumber.Text);
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand(query1, conn);  // connect and call the database for update -- Member
                        MySqlCommand cmd2 = new MySqlCommand(query2, conn);  // connect and call the database for update -- Card_Order
                        //execute the SQL command
                        cmd.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Update Successfully !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Member Information Update Error !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // Display the information on the DataGridView
                    BindDataGridMe();
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Delete the members or trainers data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // The member infor page shows
            if (groboMeInfor.Visible == true)
            {
                // Get the number of selected rows
                int row = DataGridView1.SelectedRows.Count;

                //  Both do not exist, not selected and searched
                if ((row <= 0) && (txtMeName.Text == ""))
                {
                    MessageBox.Show("There is no data to be deleted !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                } // Information in table selected but not searched
                else if ((row > 0) && (txtMeName.Text == ""))
                {
                    ConnDB();
                    // Confirm if wanna delete this data
                    DialogResult result = MessageBox.Show("Confirm to delete this data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                    if (result == DialogResult.Yes)
                    {
                        for (int i = 0; i < row; i++)
                        {
                            // Delete the data from the Member and Card_Order Form by Member name
                            string cell = DataGridView1.SelectedRows[i].Cells[1].Value.ToString();
                            string delstr = "Delete from Member where Number=" + "'" + cell + "'";
                            string delCardstr = "Delete from Card_Order where Number=" + "'" + cell + "'";

                            // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                            MySqlCommand delecmd = new MySqlCommand(delstr, conn);
                            MySqlCommand deleCarcmd = new MySqlCommand(delCardstr, conn);
                            try
                            {
                                // Run the sql command
                                delecmd.ExecuteNonQuery();
                                deleCarcmd.ExecuteNonQuery();
                                MessageBox.Show("Delete Data Successfuly !", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }                                                 
                        }
                        conn.Close();
                        BindDataGridMe();
                    }

                }
                else if ((row <= 0) && (txtMeName.Text != ""))   //  Searched for information but not selected
                {
                    // Confirm if wanna delete this data
                    DialogResult result = MessageBox.Show("Confirm to delete this data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                    if (result == DialogResult.Yes)
                    {
                        if (DataGridView1.Rows.Count == 1)
                        {
                            MessageBox.Show("The entry " + txtSearch.Text + " is not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtSearch.Text = "";
                        }
                        else
                        {
                            ConnDB();
                            // Delete the member information from member and Card_Order by Member number
                            string strsql = "Delete from Member where Number ='" + txtMeNumber.Text + "'";
                            string strCardstr = "Delete from Card_Order where Number ='" + txtMeNumber.Text + "'";

                            // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                            MySqlCommand cmd = new MySqlCommand(strsql, conn);
                            MySqlCommand cardCmd = new MySqlCommand(strCardstr, conn);
                            try
                            {
                                // Run the sql command
                                cmd.ExecuteNonQuery();
                                cardCmd.ExecuteNonQuery();
                                clearContents();
                                MessageBox.Show("Delete Data Successfuly !", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch
                            {
                                MessageBox.Show("The " + txtSearch.Text + " entry is not exists! Retry!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            conn.Close();
                            BindDataGridMe();
                        }
                    }
                }
                else    // Both of two condition is true
                {
                    MessageBox.Show("Cannot delete two data at the same time !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
            else  // Manage the trainers' information
            {
                ConnDB();
                int row = DataGridView1.SelectedRows.Count;  // Get the number of selected rows

                // Both do not exist, not selected and searched
                if ((row <= 0) && (txtTraName.Text == "")) 
                {
                    MessageBox.Show("There is no data to be deleted !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if ((row > 0) && (txtTraName.Text == ""))  // Information in table selected but not searched
                {
                    DialogResult result = MessageBox.Show("Confirm to delete this data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);  // Comfirm if wanna delete this data
                    if (result == DialogResult.Yes)
                    {
                        for (int i = 0; i < row; i++)
                        {
                            //Locate the position of Primary Key
                            string tracell = DataGridView1.SelectedRows[i].Cells[1].Value.ToString();

                            // Delete the trainer information from trainer by trainer number
                            string delTrastr = "Delete from Trainer where Number=" + "'" + tracell + "'";
                            MySqlCommand Tracmd = new MySqlCommand(delTrastr, conn);
                            try
                            {
                                Tracmd.ExecuteNonQuery();  // Run the sql command
                                MessageBox.Show("Delete Data Successfuly !", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                        conn.Close();
                        BindDataGridTra();
                    }

                }
                else if ((row <= 0) && (txtTraName.Text != ""))   // Searched for information but not selected on DataGridView
                {
                    // Confirm if wanna delete this data
                    DialogResult result = MessageBox.Show("Confirm to delete this data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);  
                    if (result == DialogResult.Yes)
                    {
                        if (DataGridView1.Rows.Count == 1)
                        {
                            MessageBox.Show("The entry " + txtSearch.Text + " is not exist!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtSearch.Text = "";
                        }
                        else
                        {
                            ConnDB();
                            // Create Mysql command to delete the trainer information by trainer number
                            string strsql = "Delete from Trainer where Number ='" + txtTraNumber.Text + "'";
                            MySqlCommand cmd = new MySqlCommand(strsql, conn);
                            try
                            {
                                cmd.ExecuteNonQuery();  // Run the sql command
                                clearContents();
                                MessageBox.Show("Delete Data Successfuly !", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch
                            {
                                MessageBox.Show("The " + txtSearch.Text + " entry is not exists! Retry!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            conn.Close();
                            BindDataGridTra();
                        }
                    }
                }
                else    // Both of two condition is true
                {
                    MessageBox.Show("Cannot delete two data at the same time !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
        }

        /// <summary>
        /// Click on the data table to display relative data in the corresponding control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(groboMeInfor.Visible == true)
            {
                string CouNumber = DataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtMeNumber.Text = CouNumber;

                string CouName = DataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtMeName.Text = CouName;

                string Tel = DataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtMeTel.Text = Tel;

                string Email = DataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtMeEmail.Text = Email;

                string Gender = DataGridView1.CurrentRow.Cells[5].Value.ToString();
                coboMeGender.Text = Gender;

                string Birth = DataGridView1.CurrentRow.Cells[6].Value.ToString();
                datimpMeBirth.Text = Birth;

                string CertyType = DataGridView1.CurrentRow.Cells[7].Value.ToString();
                coboMeCerType.Text = CertyType;

                string IDNumber = DataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtMeCertiNumber.Text = IDNumber;

                string Address = DataGridView1.CurrentRow.Cells[9].Value.ToString();
                txtMeAddress.Text = Address;

                string RegisterDtae = DataGridView1.CurrentRow.Cells[10].Value.ToString();
                datimpMeRegister.Text = RegisterDtae;

                string Deadline = DataGridView1.CurrentRow.Cells[11].Value.ToString();
                txtDeadline.Text = Deadline;

                string Consultant = DataGridView1.CurrentRow.Cells[12].Value.ToString();
                txtConsultant.Text = Consultant;

                string pic = DataGridView1.CurrentRow.Cells[13].Value.ToString();
                picMeImage.Image = Image.FromFile(pic);

                DataSet ds1 = new DataSet();  // New a DataSet and pull the data into buffer for sql4 command
                string sql = "Select * from Card_order where Number='" + CouNumber + "'";
                MySqlDataAdapter ad1 = new MySqlDataAdapter(sql, conn); // // Add a Data_adapter for the data by sql4 command....
                ad1.Fill(ds1);  // Search Card_Order form
                comboxMeType.Text = ds1.Tables[0].Rows[0][3].ToString();
                txtAcincome.Text = ds1.Tables[0].Rows[0][4].ToString();
            }
            else
            {
                string TraNumber = DataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtTraNumber.Text = TraNumber;

                string TraName = DataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtTraName.Text = TraName;

                string TraTel = DataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtTraTel.Text = TraTel;

                string TraEmail = DataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtTraEmail.Text = TraEmail;

                string TraGender = DataGridView1.CurrentRow.Cells[5].Value.ToString();
                coboTraGender.Text = TraGender;

                string TraBirth = DataGridView1.CurrentRow.Cells[6].Value.ToString();
                datimpTraBirth.Text = TraBirth;

                string TraCertificate = DataGridView1.CurrentRow.Cells[7].Value.ToString();
                coboTraCerType.Text = TraCertificate;

                string TraCerNumber = DataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtTraCertiNumber.Text = TraCerNumber;

                string TraAddewss = DataGridView1.CurrentRow.Cells[9].Value.ToString();
                txtTraAddress.Text = TraAddewss;

                string TraRegisterDate = DataGridView1.CurrentRow.Cells[10].Value.ToString();
                datimpTraRegister.Text = TraRegisterDate;

                string Trapic = DataGridView1.CurrentRow.Cells[11].Value.ToString();
                picTraImage.Image = Image.FromFile(Trapic);
            }
        }

        /// <summary>
        /// Display the out of deadline member card info on datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picDeadline_Click(object sender, EventArgs e)
        {
            string strnow = DateTime.Now.ToShortDateString();
            ConnDB();
            // Create an instance of System.Data.DataTable
            DataTable dt = new DataTable();
            // Create the query command to search Member
            string sql = "Select * from Member Where Deadline <='" + strnow + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter ad = new MySqlDataAdapter(sql, conn);
            try
            {
                // Display the members data from the query in the DataGridView
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
    }
}
