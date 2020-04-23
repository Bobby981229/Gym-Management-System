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
    public partial class Member_Ad : Form
    {
        #region Connect_Database
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
                // Connect the database
                conn.Open();
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

        #region Parameter_Definition
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

        #region Correlation_VideoMode
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

        int volumes;
        int result;
        int Price;

        #region Initialization_Engine

        public Member_Ad()
        {
            InitializeComponent();
            // Expand the effects from the inside out
           // Win32.AnimateWindow(this.Handle, 2000, Win32.AW_BLEND);

            Win32.AnimateWindow(this.Handle, 300, Win32.AW_CENTER | Win32.AW_ACTIVATE | Win32.AW_SLIDE);
            CheckForIllegalCrossThreadCalls = false;
            // Initialization Engine
            InitEngines();
            irVideoSource.Hide();
        }

        /// <summary>
        /// Initialization engine
        /// </summary>
        private void InitEngines()
        {
            // Read configuration file
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
                    MessageBox.Show("Please put SDK related DLL into the folder under x86 or x64 corresponding to bin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Engine activation failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        FilterInfoCollection videoDevices;
        public int selectedDeviceIndex = 0;

        private void Main_Page_Load(object sender, EventArgs e)
        {            
            ConnDB();
            CloseDB();

            // Open the timer
            timer1.Start();
            // Set the unit of timer
            timer1.Interval = 1000;
            // Set the background color is transparent
            lblMeTitle.Parent = picBackGround;
            picLogo.Parent = picBackGround;
            lblTip.Parent = picBackGround;

            try
            {
                // Enumerate all video input devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                // The camera equipment does not exist
                if (videoDevices.Count == 0)
                {
                    throw new ApplicationException();
                }

                // Traversal loop searches all camera drive facilities
                foreach (FilterInfo device in videoDevices)
                {
                    // Display the name and model of the camera equipment
                    combxCameras.Items.Add(device.Name);
                }

                // Select and show the first one camera equipment's name 
                combxCameras.SelectedIndex = 0;

            }
            catch (ApplicationException)
            {
                combxCameras.Items.Add("No Local Capture Devices");
                videoDevices = null;
            }
        }

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
                                                trackRGBUnit.message = string.Format(" No{0} {1},{2}", result, similarity, string.Format("RGB{0}", isLiveness ? " Living Body" : " Non-living Body"));
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
                                                if(isLiveness == true)
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
                                            // Displays a message
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
                    // RGB camera loading
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

        /// <summary>
        /// Save the shot from the camera to the local location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPhotograph_Click_1(object sender, EventArgs e)
        {
            // Ensure the camera equipment is open
            if (rgbDeviceVideo == null)
            {
                MessageBox.Show("Please connect the video first !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (txtMeName.Text == "" && txtTraNumber.Text == "")
            {
                MessageBox.Show("Please complete the infomation of user !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (videoDev.IsRunning)
            {
                // Member Add page
                if(groboMeInfor.Visible == true)
                {
                    Bitmap bitmap = videoDev.GetCurrentVideoFrame();
                    // Display screenshot image on picturebox
                    picUserImage.Image = bitmap;
                    GetImagePath();

                    // Photo save local address
                    string fileName = txtMeName.Text + ".jpg"; //  " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")
                    bitmap.Save(@"C:\Users\72946\Desktop\GU 2\UserImages\" + fileName, ImageFormat.Jpeg);
                }
                else // Trainer Add page
                {
                    Bitmap bitmap = videoDev.GetCurrentVideoFrame();
                    // Display screenshot image on picturebox
                    picUserImage.Image = bitmap;
                    GetImagePath();

                    // Photo save local address
                    string fileName = txtTraName.Text + ".jpg"; //  " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + 
                    bitmap.Save(@"C:\Users\72946\Desktop\GU 2\UserImages\" + fileName, ImageFormat.Jpeg);
                }
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
            if (B < 18.4 && B >0)
            {
                MessageBox.Show("Thin Body", "BMI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (B >= 18.5 && B < 24.9)
            {
                MessageBox.Show("Normal Weight", "BMI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (B >= 25.0 && B < 29.9)
            {
                MessageBox.Show("Overweight", "BMI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (B >= 30.0)
            {
                MessageBox.Show("Obesity", "BMI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(B == 0)
            {
                txtHight.Text = "";
                txtWeight.Text = "";
                txtBMI.Clear();
                MessageBox.Show("Data Error, please enter again !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return BMI;
        }

        // Return to the home page
        private void picLogo_Click(object sender, EventArgs e)
        {
            // Close the camera Device
            if (videoDev.IsRunning)
            {
                videoDev.SignalToStop();
            }
            this.Hide();
        }

        // Set the time tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Get the current date and time, display the concrete time
            lblTheme.Text = "Hello Administrator ! Welcome to the Gym Management System!";
            lblItemTime.Text = "The Current Time: " + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss");
        }

        /// <summary>
        /// Display the amount to be paid according to the type of membership card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboxMeType_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        /// <summary>
        /// Select the type of identification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboxCerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMeCertiNumber.ReadOnly = false;
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

        /// <summary>
        /// Calculate the members's BMI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Select the date of registering membership card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datimpRegister_ValueChanged(object sender, EventArgs e)
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
        }

        private void txtMeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only english letters can be entered
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
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

        private void txtMeEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Forbid Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5))
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

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can only enter numbers
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMeAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Forbid Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5))
            {
                e.Handled = true;
            }
        }

        // Judge the telephone number format whether is correct 
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
                // The error icon appear
                picMeTelError.Visible = true;
            }

            if (txtMeTel.Text == "")
            {
                // The error icon disappear
                picMeTelError.Visible = false;
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
                picMeemailError.Visible = false;
            }
            else
            {
                // The error icon appear
                picMeemailError.Visible = true;
            }

            if(txtMeEmail.Text == "")
            {
                // The error icon disappear
                picMeemailError.Visible = false;
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
                catch
                {
                }
            }
            else
            {
                // The error icon disappear
                picMeCerNumber.Visible = false;
            }

            if (txtMeCertiNumber.Text == "")
            {
                // The error icon disappear
                picMeCerNumber.Visible = false;
            }
        }

        private void txtConsultant_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Can only enter the english characters
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void coboMeGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Focus on the datatimepicker
            datimpMeBirth.Focus();
        }

        // Convert pictures to binary stream
        public byte[] PhotoImageInsert(System.Drawing.Image imgPhoto)
        {
            // Convert Image to stream data and save it as byte []
            MemoryStream mstream = new MemoryStream();
            imgPhoto.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] byData = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(byData, 0, byData.Length);
            mstream.Close();
            return byData;
        }

        /// <summary>
        /// Save information data to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMeSave_Click(object sender, EventArgs e)
        {
            int ExTimes = 1;  // Set the exercise times as 1 when create a new member

            // When Member infor page is show, save the data into member database
            if (groboMeInfor.Visible == true)
            {
                // Ensure the important information should be filled
                if (txtMeNumber.Text == "" || txtMeName.Text == "" || txtMeTel.Text == "" || txtMeEmail.Text == "" || coboMeGender.Text == "" || txtMeCertiNumber.Text == "" || txtDeadline.Text == "" || comboxMeType.Text == "" || picUserImage.Image == null)
                {
                    MessageBox.Show("Please complete the member personal information !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (picMeTelError.Visible == true || picMeemailError.Visible == true || picMeCerNumber.Visible == true)  // If the Error icons are showing, the data can not to store in database
                {
                    MessageBox.Show("There is an error in member personal information !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    volumes = 1;
                    ConnDB();
                    // New a DataSet and pull the data into buffer for sql command
                    DataSet ds = new DataSet(); 

                    // Write the mysql statement for the search and assign the value
                    string searchSql = "Select * from Card_Sale where Date='" + datimpMeRegister.Text + "'";

                    // Add a Data_adapter for the data by sql command.... In order to display data on the form
                    MySqlDataAdapter adSearch = new MySqlDataAdapter(searchSql, conn);

                    // Add card_order information into DataSet
                    adSearch.Fill(ds);

                    // The buff of dataset can not be empty !
                    if (ds != null && ds.Tables[0].Rows.Count > 0)    
                    {
                        // Export data from the buffer table
                        string dsVolume = ds.Tables[0].Rows[0][1].ToString();
                        string dsProfit = ds.Tables[0].Rows[0][2].ToString();

                        // Modify the data in the table
                        result = int.Parse(dsVolume) + volumes;
                        Price = int.Parse(dsProfit) + int.Parse(txtAcincome.Text);
                    }

                    ConnDB();  // Connect the database
                    // Photo storage address
                    string fileName = @"C:\\Users\\72946\\Desktop\\GU 2\\UserImages\\" + txtMeName.Text + ".jpg";
                    // Insert the information data into the Member table
                    string strsql = string.Format("Insert into Member(Identification, Number, Name, TelNum, Email, Gender, Birthday, Certificate_Type, Certificate_Number, Address, Register_Date, Deadline, Consultant, PicURL)  values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}')",
                        txtMeIdentity.Text, txtMeNumber.Text, txtMeName.Text, txtMeTel.Text, txtMeEmail.Text, coboMeGender.Text, datimpMeBirth.Text, coboMeCerType.Text, txtMeCertiNumber.Text, txtMeAddress.Text, datimpMeRegister.Text, txtDeadline.Text, txtConsultant.Text, fileName);

                    // Insert the information data into the BMI table
                    string strsql1 = string.Format("Insert into BMI(Number, Name, Hight, Weight, BMI) values ('{0}', '{1}', '{2}', '{3}', '{4}')", txtMeNumber.Text, txtMeName.Text, txtHight.Text, txtWeight.Text, txtBMI.Text);

                    // Insert the information data into the Card_Order table
                    string strsql2 = string.Format("Insert into Card_Order(Number, Name, Registration_Date, Membe_Type, Price, Consultant, Exercise_Times) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", 
                        txtMeNumber.Text, txtMeName.Text, datimpMeRegister.Text, comboxMeType.Text, txtAcincome.Text, txtConsultant.Text, ExTimes);

                    // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                    MySqlCommand cmd = new MySqlCommand(strsql, conn);
                    MySqlCommand cmd1 = new MySqlCommand(strsql1, conn);
                    MySqlCommand cmd2 = new MySqlCommand(strsql2, conn);
                    try
                    {
                        //execute the SQL command
                        cmd.ExecuteNonQuery();
                        cmd1.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Member Information Added Successfully!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("The Member Information Already Exists !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMeNumber.Focus();// set the cursor here
                    }

                    ConnDB();
                    // Insert the sale data into the Card_Sale table
                    string saleSql = string.Format("Insert into Card_Sale(Date, Volums, Profit) values ('{0}', '{1}', '{2}')", datimpMeRegister.Text, volumes, txtAcincome.Text);
                    // Every time a new member is registered, the membership card sales record is updated
                    string saleUpdate = string.Format("Update Card_Sale set Volums = ('{0}'), Profit = ('{1}') Where Date = ('{2}')", result, Price, datimpMeRegister.Text);

                    // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                    MySqlCommand cmdVolume = new MySqlCommand(saleSql, conn);
                    MySqlCommand cmdProfit = new MySqlCommand(saleUpdate, conn);

                    try
                    {
                        // Execute the SQL command if today does not hava sale record
                        cmdVolume.ExecuteNonQuery();
                    }
                    catch
                    {
                        // If today has the sale record - Execute the SQL command
                        cmdProfit.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn.Close();
                    }
                    clearContents();
                }
            }
            else   //  Trainer Add Page
            {
                 // Ensure the important information should be filled
                if (txtTraNumber.Text == "" || txtTraName.Text == "" || txtTraTel.Text == "" || txtTraEmail.Text == "" || coboTraGender.Text == "" || txtTraCertiNumber.Text == "" || picUserImage.Image == null)
                {
                    MessageBox.Show("Please complete the trainer personal information !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (picTraTelError.Visible == true || picTraEmailError.Visible == true || picTraCerNumber.Visible == true)  // If the Error icons are showing, the data can not to store in database
                {
                    MessageBox.Show("There is an error in trainer personal information !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Photo storage address
                    string picName = @"C:\\Users\\72946\\Desktop\\GU 2\\UserImages\\" + txtTraName.Text + ".jpg";
                    ConnDB();
                    // Create the Mysql command insert the information data into the Trainer table
                    string strsql3 = string.Format("Insert into Trainer(Identification, Number, Name, TelNum, Email, Gender, Birthday, Certificate_Type, Certificate_Number, Address, Register_Date, PicURL)  values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')",
                        txtTraIdentity.Text, txtTraNumber.Text, txtTraName.Text, txtTraTel.Text, txtTraEmail.Text, coboTraGender.Text, datimpTraBirth.Text, coboTraCerType.Text, txtTraCertiNumber.Text, txtTraAddress.Text, datimpTraRegister.Text, picName);
                    // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                    MySqlCommand cmd3 = new MySqlCommand(strsql3, conn);
                    try
                    { 
                        // execute the SQL command
                        cmd3.ExecuteNonQuery();
                        MessageBox.Show("Trainer information added successfully!", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Trainer Information Adding Error !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtTraNumber.Focus();// set the cursor here
                    }                   
                    conn.Close();
                    clearContents();
                }
            }
            // After add - Close the camera device
            btnConnect.Text = "Connect Camera";
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

        private void coboTraGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTraTel.Focus();
        }

        // Select the trainers' certification type
        private void coboTraCerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTraCertiNumber.ReadOnly = false;
            txtTraCertiNumber.Text = "";
            txtTraCertiNumber.Focus();
        }

        // Judge the telephone number format whether is correct 
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
                // The error icon disappear
                picTraTelError.Visible = false;
            }
            else
            {
                // The error icon appear
                picTraTelError.Visible = true;
            }

            if (txtTraTel.Text == "")
            {
                // The error icon disappear
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
                picTraEmailError.Visible = false; // The error icon disappear
            }
            else
            {
                picTraEmailError.Visible = true;  // The error icon appear
            }

            if (txtTraEmail.Text == "")
            {
                picTraEmailError.Visible = false; // The error icon disappear
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
                        // The ID number is error
                        picTraCerNumber.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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

        // Clear the contents text of controls
        public void clearContents()
        {
            // Clear the Member add page text contents
            if(groboMeInfor.Visible == true)
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
                txtHight.Text = "";
                txtWeight.Text = "";
                txtBMI.Text = "";
                picUserImage.Image = null; 
            }
            else  // Clear the Trainer add page text contents
            {
                txtTraNumber.Text = "";
                txtTraName.Text = "";
                txtTraTel.Text = "";
                txtTraEmail.Text = "";
                txtTraCertiNumber.Text = "";
                txtTraAddress.Text = "";
                picUserImage.Image = null;
            }
        }

        // Refresh the page and clear the text contents
        private void picRefresh_Click(object sender, EventArgs e)
        {
            clearContents();
        }

        /// <summary>
        /// Change Member Physical Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHight_TextChanged(object sender, EventArgs e)
        {
            txtBMI.Text = "";
        }

        /// <summary>
        /// Change Member Physical Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            txtBMI.Text = "";
        }
    }
}
