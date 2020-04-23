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
using MySql.Data.MySqlClient;
using System.Data;
using System.Runtime.InteropServices;
using System.Media;
using DevExpress.CodeParser.Diagnostics;
//using AForge.Imaging;

namespace Gym_Management_System
{
    public partial class FaceForm : Form
    {
        MySqlDataAdapter ad;
        DataSet ds;
        string picName;
        int compNum;
        bool flag2;

        // Define Sound Player
        SoundPlayer successSound = new SoundPlayer(Gym_Management_System.Properties.Resources.success);
        SoundPlayer errorSound = new SoundPlayer(Gym_Management_System.Properties.Resources.error);

        #region External function declaration
        //External function declaration: make the device sound
        [DllImport("OUR_IDR.dll", EntryPoint = "idr_beep", CallingConvention = CallingConvention.StdCall)]
        static extern byte idr_beep(UInt32 xms);// xms unit is millisecond

        //Read the device number, can be used as a software dongle, 
        //or you can check the warranty period on the company's website based on this number
        [DllImport("OUR_IDR.dll", EntryPoint = "pcdgetdevicenumber", CallingConvention = CallingConvention.StdCall)]
        static extern byte pcdgetdevicenumber(byte[] devicenumber);// devicenumber is used to return the number

        // Read-only card number
        [DllImport("OUR_IDR.dll", EntryPoint = "idr_read", CallingConvention = CallingConvention.StdCall)]
        public static extern byte idr_read(byte[] serial);// serial return card number

        // Read-only card number, read-only once, must be removed to read again
        [DllImport("OUR_IDR.dll", EntryPoint = "idr_read_once", CallingConvention = CallingConvention.StdCall)]
        public static extern byte idr_read_once(byte[] serial);//serial return card number

        // Send display to card reader
        //[DllImport("OUR_IDR.dll", EntryPoint = "lcddispfull", CallingConvention = CallingConvention.StdCall)]
        //static extern byte lcddispfull(string lcdstr);
        #endregion

        #region Connect_Database
        //SignInInterface Mysql DataBase Connection
        private const string dbServer = "server=127.0.0.1;port=3306;database=gym;user=root;password=LDF8705012";
        MySqlConnection conn;
     
        // Create a connection
        private void ConnDB()
        {
            // server address;The port number.Database;The user name;password
            string connectStr = dbServer;
            // create a connection   // username and password defined in MySQL
            conn = new MySqlConnection(connectStr);
            // Connect Database
            try
            {
                conn.Open();     //Mysql database did not start, this sentence error!
            }
            catch
            {
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

        #region Parameter_Definition
        /// <summary>
        /// Engine Handle
        /// </summary>
        private IntPtr pImageEngine = IntPtr.Zero;

        /// <summary>
        /// Save the image path on the right
        /// </summary>
        private string image1Path;

        /// <summary>
        /// Image maximum size
        /// </summary>
        private long maxSize = 1024 * 1024 * 2;

        /// <summary>
        /// Face features in the picture on the right
        /// </summary>
        private IntPtr image1Feature;

        /// <summary>
        /// Save a list of comparison images
        /// </summary>
        private List<string> imagePathList = new List<string>();

        /// <summary>
        /// List of face features in left gallery
        /// </summary>
        private List<IntPtr> imagesFeatureList = new List<IntPtr>();

        /// <summary>
        /// Semantic Similarity
        /// </summary>
        private float threshold = 0.8f;

        /// <summary>
        /// Used to mark whether the alignment results need to be cleared
        /// </summary>
        private bool isCompare = false;

        /// <summary>
        /// Is it a binocular camera
        /// </summary>
        private bool isDoubleShot = false;

        /// <summary>
        /// Allowable error range
        /// </summary>
        private int allowAbleErrorRange = 40;

        /// <summary>
        /// RGB Camera Index
        /// </summary>
        private int rgbCameraIndex = 0;
        /// <summary>
        /// IR Camera Index
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

        #region Initialization
        public FaceForm()
        {
            InitializeComponent();
            Win32.AnimateWindow(this.Handle, 300, Win32.AW_BLEND | Win32.AW_ACTIVATE);
            CheckForIllegalCrossThreadCalls = false;
            // Initialization Engine
            InitEngines();
            // Hide the camera image window
            videoDev.Hide();
            irVideoSource.Hide();
            // The threshold control is not available
            txtThreshold.Enabled = false;
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

        #region Camera_Initialization
        /// <summary>
        /// Camera_Initialization
        /// </summary>
        private void initVideo()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            // If no camera is available, the "Enable Camera" button is disabled, otherwise it is available
            if (filterInfoCollection.Count == 0)
            {
                cameraSwitch.Enabled = false;
            }
            else
            {
                cameraSwitch.Enabled = true;
            }
        }
        #endregion


        #region RegisterFace_Events
        private object locker = new object();
        /// <summary>
        /// Face library image selection button event
        /// </summary>
        private void btnChooseImg_Click(object sender, EventArgs e)
        {
            lock (locker)
            {
                // Open the local file and select the images
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select the Images";
                openFileDialog.Filter = "Images File|*.bmp;*.jpg;*.jpeg;*.png";
                openFileDialog.Multiselect = true;
                openFileDialog.FileName = string.Empty;
                listView.Refresh();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    List<string> imagePathListTemp = new List<string>();

                    // Save the image path and display it
                    string[] fileNames = openFileDialog.FileNames;
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        // Image format judgment
                        if (checkImage(fileNames[i]))
                        {
                            imagePathListTemp.Add(fileNames[i]);
                        }
                    }
                    
                    var numStart = imagePathList.Count;
                    int isGoodImage = 0;         

                    // Face detection and facial feature extraction
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        // Do not click the button
                        Invoke(new Action(delegate
                        {
                            btnChooseImg.Enabled = false;
                            btnMatch.Enabled = false;
                            btnClearImage.Enabled = false;
                            btnSelectImg.Enabled = false;
                            cameraSwitch.Enabled = false;
                        }));

                        // Face detection and clipping
                        for (int i = 0; i < imagePathListTemp.Count; i++)
                        {
                            Image image = ImageUtil.readFromFile(imagePathListTemp[i]);
                            if (image == null)
                            {
                                continue;
                            }
                            if (image.Width > 1536 || image.Height > 1536)
                            {
                                image = ImageUtil.ScaleImage(image, 1536, 1536);
                            }
                            if (image == null)
                            {
                                continue;
                            }
                            if (image.Width % 4 != 0)
                            {
                                image = ImageUtil.ScaleImage(image, image.Width - (image.Width % 4), image.Height);
                            }
                            // Face detection
                            ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, image);
                            // Judge test results
                            if (multiFaceInfo.faceNum > 0)
                            {
                                imagePathList.Add(imagePathListTemp[i]);
                                MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects);
                                image = ImageUtil.CutImage(image, rect.left, rect.top, rect.right, rect.bottom);
                            }
                            else
                            {
                                if (image != null)
                                {
                                    image.Dispose();
                                }
                                continue;
                            }

                            // According to the face
                            this.Invoke(new Action(delegate
                            {
                                if (image == null)
                                {
                                    image = ImageUtil.readFromFile(imagePathListTemp[i]);

                                    if (image.Width > 1536 || image.Height > 1536)
                                    {
                                        image = ImageUtil.ScaleImage(image, 1536, 1536);
                                    }
                                }

                                imageLists.Images.Add(imagePathListTemp[i], image);
                                listView.Items.Add((numStart + isGoodImage) + "", imagePathListTemp[i]);
                                listView.Refresh();
                                isGoodImage += 1;
                                if (image != null)
                                {
                                    image.Dispose();
                                }
                                //picName = Path.GetFileNameWithoutExtension(imageLists.Images.Keys[isGoodImage]);
                                //imageLists.Images.Add(imagePathListTemp[i], image);
                                //// ImageList.Items.Add((numStart + isGoodImage) + "号", imagePathListTemp[i]);
                                //listView.Items.Add(picName, imagePathListTemp[i]);

                                //listView.Refresh();
                                //isGoodImage += 1;
                                //if (image != null)
                                //{
                                //    image.Dispose();
                                //}
                                ////  Find the image name (File path), and withou the extension
                            }));
                        }

                        // Extracting facial features
                        for (int i = numStart; i < imagePathList.Count; i++)
                        {
                            ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();
                            Image image = ImageUtil.readFromFile(imagePathList[i]);
                            if (image == null)
                            {
                                continue;
                            }
                            IntPtr feature = FaceUtil.ExtractFeature(pImageEngine, image, out singleFaceInfo);
                            this.Invoke(new Action(delegate
                            {
                                if (singleFaceInfo.faceRect.left == 0 && singleFaceInfo.faceRect.right == 0)
                                {
                                    AppendText(string.Format("{0}No face Detected\r\n", i));
                                }
                                else
                                {
                                    AppendText(string.Format("Face feature value {0} has been extracted，[left:{1},right:{2},top:{3},bottom:{4},orient:{5}]\r\n"
                                    , i, singleFaceInfo.faceRect.left, singleFaceInfo.faceRect.right, singleFaceInfo.faceRect.top, singleFaceInfo.faceRect.bottom, singleFaceInfo.faceOrient));
                                    imagesFeatureList.Add(feature);
                                }
                            }));
                            if (image != null)
                            {
                                image.Dispose();
                            }
                        }

                        // Allow to click the button
                        Invoke(new Action(delegate
                        {
                            btnChooseImg.Enabled = true;
                            btnClearImage.Enabled = true;
                            cameraSwitch.Enabled = true;

                            if (cameraSwitch.Checked == false)
                            {
                                btnSelectImg.Enabled = true;
                                btnMatch.Enabled = true;
                            }
                            else
                            {
                                btnSelectImg.Enabled = false;
                                btnMatch.Enabled = false;
                            }
                        }));
                    }));

                }
            }
        }
        #endregion

        #region ClearFaceLibrary_Event
        /// <summary>
        /// Clear Face Library Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            // Clear data
            imageLists.Images.Clear();
            listView.Items.Clear();
            foreach (IntPtr intptr in imagesFeatureList)
            {
                MemoryUtil.Free(intptr);
            }
            imagesFeatureList.Clear();
            imagePathList.Clear();
        }
        #endregion

        #region SelectDentifyGraph_Event
        /// <summary>
        /// Select the identify graph button event
        /// </summary>
        private void btnSelectImg_Click(object sender, EventArgs e)
        {
            lblCompareInfo.Text = "";
            // Determine if the engine was initialized successfully
            if (pImageEngine == IntPtr.Zero)
            {
                // Disable the relevant function button
                ControlsEnable(false, btnChooseImg, btnMatch, btnClearImage, btnSelectImg);
                MessageBox.Show("Please initialize the engine first !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // Select Image
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select Image";
            openFileDialog.Filter = "Picture File|*.bmp;*.jpg;*.jpeg;*.png";
            openFileDialog.Multiselect = false;
            openFileDialog.FileName = string.Empty;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {    
                image1Path = openFileDialog.FileName;
                // Check Image Format
                if (!checkImage(image1Path))
                {
                    return;
                }
                DateTime detectStartTime = DateTime.Now;
                AppendText(string.Format("------------------------------Starting test，Time:{0}------------------------------\n", detectStartTime.ToString("yyyy-MM-dd HH:mm:ss:ms")));

                //获取文件，拒绝过大的图片
                FileInfo fileInfo = new FileInfo(image1Path);
                if (fileInfo.Length > maxSize)
                {
                    MessageBox.Show("Image file is up to 2MB, please compress it before importing !");
                    AppendText(string.Format("------------------------------End of the test，Time:{0}------------------------------\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    AppendText("\n");
                    return;
                }

                // Image data acquisition failed, please try again later
                Image srcImage = ImageUtil.readFromFile(image1Path);
                if (srcImage == null)
                {
                    MessageBox.Show("Image data acquisition failed, please try again later !");
                    AppendText(string.Format("------------------------------End of the test，Time:{0}------------------------------\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    AppendText("\n");
                    return;
                }
                if (srcImage.Width > 1536 || srcImage.Height > 1536)
                {
                    srcImage = ImageUtil.ScaleImage(srcImage, 1536, 1536);
                }
                if (srcImage == null)
                {
                    MessageBox.Show("Image data acquisition failed, please try again later !");
                    AppendText(string.Format("------------------------------End of the test，Time:{0}------------------------------\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    AppendText("\n");
                    return;
                }
                // Adjust the image width to be a multiple of 4
                if (srcImage.Width % 4 != 0)
                {
                    srcImage = ImageUtil.ScaleImage(srcImage, srcImage.Width - (srcImage.Width % 4), srcImage.Height);
                }
                // Adjust the picture data, very important
                ImageInfo imageInfo = ImageUtil.ReadBMP(srcImage);
                if (imageInfo == null)
                {
                    MessageBox.Show("Image data acquisition failed, please try again later !");
                    AppendText(string.Format("------------------------------End of the test，Time:{0}------------------------------\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    AppendText("\n");
                    return;
                }
                // Face detection
                ASF_MultiFaceInfo multiFaceInfo = FaceUtil.DetectFace(pImageEngine, imageInfo);
                // Age detection
                int retCode_Age = -1;
                ASF_AgeInfo ageInfo = FaceUtil.AgeEstimation(pImageEngine, imageInfo, multiFaceInfo, out retCode_Age);
                // Gender tests
                int retCode_Gender = -1;
                ASF_GenderInfo genderInfo = FaceUtil.GenderEstimation(pImageEngine, imageInfo, multiFaceInfo, out retCode_Gender);

                // 3D Angle检测
                int retCode_3DAngle = -1;
                ASF_Face3DAngle face3DAngleInfo = FaceUtil.Face3DAngleDetection(pImageEngine, imageInfo, multiFaceInfo, out retCode_3DAngle);

                MemoryUtil.Free(imageInfo.imgData);

                if (multiFaceInfo.faceNum < 1)
                {
                    srcImage = ImageUtil.ScaleImage(srcImage, picImageCompare.Width, picImageCompare.Height);
                    image1Feature = IntPtr.Zero;
                    picImageCompare.Image = srcImage;
                    AppendText(string.Format("{0} - No face detected !\n\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                    AppendText(string.Format("------------------------------End of the test，Time:{0}------------------------------\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                    AppendText("\n");
                    return;
                }

                MRECT temp = new MRECT();
                int ageTemp = 0;
                int genderTemp = 0;
                int rectTemp = 0;

                // Mark out detected faces
                for (int i = 0; i < multiFaceInfo.faceNum; i++)
                {
                    MRECT rect = MemoryUtil.PtrToStructure<MRECT>(multiFaceInfo.faceRects + MemoryUtil.SizeOf<MRECT>() * i);
                    int orient = MemoryUtil.PtrToStructure<int>(multiFaceInfo.faceOrients + MemoryUtil.SizeOf<int>() * i);
                    int age = 0;

                    if (retCode_Age != 0)
                    {
                        AppendText(string.Format("Age test failed，return{0}!\n\n", retCode_Age));
                    }
                    else
                    {
                        age = MemoryUtil.PtrToStructure<int>(ageInfo.ageArray + MemoryUtil.SizeOf<int>() * i);
                    }

                    int gender = -1;
                    if (retCode_Gender != 0)
                    {
                        AppendText(string.Format("Gender test failed，return{0}!\n\n", retCode_Gender));
                    }
                    else
                    {
                        gender = MemoryUtil.PtrToStructure<int>(genderInfo.genderArray + MemoryUtil.SizeOf<int>() * i);
                    }

                    int face3DStatus = -1;
                    float roll = 0f;
                    float pitch = 0f;
                    float yaw = 0f;

                    if (retCode_3DAngle != 0)
                    {
                        AppendText(string.Format("3DAngle test failed，return{0}!\n\n", retCode_3DAngle));
                    }
                    else
                    {
                        // Angle status Non-zero means the face is untrustworthy
                        face3DStatus = MemoryUtil.PtrToStructure<int>(face3DAngleInfo.status + MemoryUtil.SizeOf<int>() * i);

                        // Roll is the roll angle, pitch is the pitch angle, and yaw is the yaw angle
                        roll = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.roll + MemoryUtil.SizeOf<float>() * i);
                        pitch = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.pitch + MemoryUtil.SizeOf<float>() * i);
                        yaw = MemoryUtil.PtrToStructure<float>(face3DAngleInfo.yaw + MemoryUtil.SizeOf<float>() * i);
                    }

                    int rectWidth = rect.right - rect.left;
                    int rectHeight = rect.bottom - rect.top;

                    // Find the largest face
                    if (rectWidth * rectHeight > rectTemp)
                    {
                        rectTemp = rectWidth * rectHeight;
                        temp = rect;
                        ageTemp = age;
                        genderTemp = gender;
                    }
                    AppendText(string.Format("{0} - Face coordinates:[left:{1},top:{2},right:{3},bottom:{4},orient:{5},roll:{6},pitch:{7},yaw:{8},status:{11}] Age:{9} Gender:{10}\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), rect.left, rect.top, rect.right, rect.bottom, orient, roll, pitch, yaw, age, (gender >= 0 ? gender.ToString() : ""), face3DStatus));
                }

                AppendText(string.Format("{0} - Number of faces:{1}\n\n", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), multiFaceInfo.faceNum));

                DateTime detectEndTime = DateTime.Now;
                AppendText(string.Format("------------------------------End of test，Time:{0}------------------------------\n", detectEndTime.ToString("yyyy-MM-dd HH:mm:ss:ms")));
                AppendText("\n");
                ASF_SingleFaceInfo singleFaceInfo = new ASF_SingleFaceInfo();

                // Extracting facial features
                image1Feature = FaceUtil.ExtractFeature(pImageEngine, srcImage, out singleFaceInfo);

                // Clear last match
                for (int i = 0; i < imagesFeatureList.Count; i++)
                {
                    listView.Items[i].Text = string.Format("No {0}", i);
                }
                // Get scaling
                float scaleRate = ImageUtil.getWidthAndHeight(srcImage.Width, srcImage.Height, picImageCompare.Width, picImageCompare.Height);
                // Zoom picture
                srcImage = ImageUtil.ScaleImage(srcImage, picImageCompare.Width, picImageCompare.Height);
                // Add tag
                srcImage = ImageUtil.MarkRectAndString(srcImage, (int)(temp.left * scaleRate), (int)(temp.top * scaleRate), (int)(temp.right * scaleRate) - (int)(temp.left * scaleRate), (int)(temp.bottom * scaleRate) - (int)(temp.top * scaleRate), ageTemp, genderTemp, picImageCompare.Width);

                // Show tagged image
                picImageCompare.Image = srcImage;
            }
        }
        #endregion

        #region Show_Tagged_Image
        /// <summary>
        /// Show tagged image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMatch_Click(object sender, EventArgs e)
        {
            if (imagesFeatureList.Count == 0)
            {
                MessageBox.Show("Please register face !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (image1Feature == IntPtr.Zero)
            {
                if (picImageCompare.Image == null)
                {
                    MessageBox.Show("Please select the identification chart !", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(" The comparison failed, and the eigenvalues were not extracted in the recognition map.!", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            //  The tag has been matched, and the comparison result must be cleared when the video is opened.
            isCompare = true;
            float compareSimilarity = 0f;
            int compareNum = 0;
            AppendText(string.Format("------------------------------Start matching，Time:{0}------------------------------\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));

            for (int i = 0; i < imagesFeatureList.Count; i++)
            {
                IntPtr feature = imagesFeatureList[i];
                float similarity = 0f;
                int ret = ASFFunctions.ASFFaceFeatureCompare(pImageEngine, image1Feature, feature, ref similarity);

                //  Increase outlier handling
                if (similarity.ToString().IndexOf("E") > -1)
                {
                    similarity = 0f;
                }
                AppendText(string.Format("Comparison result with No:{0}:{1}\r\n", i, similarity));
                listView.Items[i].Text = string.Format("No:{0}({1})", i, similarity);

                if (similarity > compareSimilarity)
                {
                    compareSimilarity = similarity;
                    compareNum = i;
                }
            }
            if (compareSimilarity > 0)
            {
                lblCompareInfo.Text = " " + compareNum + "," + compareSimilarity;
            }
            AppendText(string.Format("------------------------------Match ends，Time:{0}------------------------------\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ms")));
        }
        #endregion

        #region Video_Detection_Related
        /// <summary>
        /// Camera button click event - Camera button click event, camera Paint event, feature comparison, camera playback completion event>
        /// </summary>  
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cameraSwitch_CheckedChanged(object sender, EventArgs e)
        {
            txtCardNumber.Focus();
            // Sit down at the beginning of the click to initialize the detection to prevent the camera from starting 
            // when the program starts, and unplug the camera before clicking the camera button
            initVideo();

            // Must have a camera available
            if (filterInfoCollection.Count == 0)
            {
                MessageBox.Show("Camera not detected, make sure the camera or driver is installed!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (videoDev.IsRunning || irVideoSource.IsRunning)
            {
                //btnConnCam.Text = "Connect Camera";
                //CTurn off the camera
                if (irVideoSource.IsRunning)
                {
                    irVideoSource.SignalToStop();
                    irVideoSource.Hide();
                }
                if (videoDev.IsRunning)
                {
                    videoDev.SignalToStop();
                    videoDev.Hide();
                }

                // "Select recognition map", "Start matching" buttons are available, threshold control is disabled
                btnSelectImg.Enabled = true;
                btnMatch.Enabled = true;
                txtThreshold.Enabled = false;
            }
            else
            {
                if (isCompare)
                {
                    // Match result clear
                    for (int i = 0; i < imagesFeatureList.Count; i++)
                    {
                        listView.Items[i].Text = string.Format("No {0}", i);
                    }
                    lblCompareInfo.Text = "";
                    isCompare = false;
                }

                //The "Select identification map" and "Start matching" buttons are disabled, the threshold control is available, and the camera control is displayed
                txtThreshold.Enabled = true;
                videoDev.Show();
                irVideoSource.Show();
                btnSelectImg.Enabled = false;
                btnMatch.Enabled = false;

                //btnConnCam.Text = "Close Camera";
                // Get the total number of filterInfoCollection
                int maxCameraCount = filterInfoCollection.Count;

                // If two different camera indexes are configured
                if (rgbCameraIndex != irCameraIndex && maxCameraCount >= 2)
                {
                    // RGB Camera loading
                    rgbDeviceVideo = new VideoCaptureDevice(filterInfoCollection[rgbCameraIndex < maxCameraCount ? rgbCameraIndex : 0].MonikerString);
                    rgbDeviceVideo.VideoResolution = rgbDeviceVideo.VideoCapabilities[0];
                    videoDev.VideoSource = rgbDeviceVideo;
                    videoDev.Start();

                    // IR Camera loading
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

        private FaceTrackUnit trackRGBUnit = new FaceTrackUnit();
        private FaceTrackUnit trackIRUnit = new FaceTrackUnit();
        private Font font = new Font(FontFamily.GenericSerif, 10f, FontStyle.Bold);
        private SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
        private SolidBrush blueBrush = new SolidBrush(Color.Blue);
        private bool isRGBLock = false;
        private bool isIRLock = false;
        private MRECT allRect = new MRECT();
        private object rectLock = new object();

        /// <summary>
        /// RGB camera Paint event, the image is displayed on the form, each frame image is obtained, and processed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoSource_Paint(object sender, PaintEventArgs e)
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
                // Get Rect
                MRECT rect = maxFace.faceRect;
                // Detect the largest face under the RGB camera
                Graphics g = e.Graphics;
                float offsetX = videoDev.Width * 1f / bitmap.Width;
                float offsetY = videoDev.Height * 1f / bitmap.Height;
                float x = rect.left * offsetX;
                float width = rect.right * offsetX - x;
                float y = rect.top * offsetY;
                float height = rect.bottom * offsetY - y;
                string num1 = "07561196";

                // Frame according to Rect
                g.DrawRectangle(Pens.Red, x, y, width, height);
                if (trackRGBUnit.message != "" && x > 0 && y > 0)
                {
                    // Display the detection result of the previous frame on the page
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

                                // It ’s important to adjust the image data
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

                                        // Match Successfully
                                        if (result == 0)
                                        {
                                            txtCardNumber.Text = num1;
                                            picSearchNum_Click(sender, e);
                                            txtCardNumber.Text = "";
                                            MessageBox.Show("Successful Authentication !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    else
                                    {
                                        // Show message
                                        trackRGBUnit.message = string.Format("RGB{0}", isLiveness ? " Living Body" : " Non-living Body");
                                        //errorSound.Play();
                                       // MessageBox.Show("Authentication Failed !", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    // Show message
                                    trackRGBUnit.message = string.Format("RGB{0}", isLiveness ? " Living Body" : " Non-living Body");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                if(bitmap != null)
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

        /// <summary>
        /// RGB camera Paint event, synchronize RGB face frame, compare IR face detection for live body detection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void irVideoSource_Paint(object sender, PaintEventArgs e)
        {
            if (isDoubleShot && irVideoSource.IsRunning)
            {
                // If dual camera and IR camera work, get IR camera picture
                Bitmap irBitmap = irVideoSource.GetCurrentVideoFrame();
                if (irBitmap == null)
                {
                    return;
                }
                // Get Rect
                MRECT rect = new MRECT();
                lock (rectLock)
                {
                    rect = allRect;
                }
                float irOffsetX = irVideoSource.Width * 1f / irBitmap.Width;
                float irOffsetY = irVideoSource.Height * 1f / irBitmap.Height;
                float offsetX = irVideoSource.Width * 1f / videoDev.Width;
                float offsetY = irVideoSource.Height * 1f / videoDev.Height;
                // Detect the largest face under the IR camera
                Graphics g = e.Graphics;

                float x = rect.left * offsetX;
                float width = rect.right * offsetX - x;
                float y = rect.top * offsetY;
                float height = rect.bottom * offsetY - y;
                // Frame according to Rect
                g.DrawRectangle(Pens.Red, x, y, width, height);
                if (trackIRUnit.message != "" && x > 0 && y > 0)
                {
                    // Display the detection result of the previous frame on the page
                    g.DrawString(trackIRUnit.message, font, trackIRUnit.message.Contains("Living Body") ? blueBrush : yellowBrush, x, y - 15);
                }

                // Guaranteed to detect only one frame to prevent page freezes and other memory usage
                if (isIRLock == false)
                {
                    isIRLock = true;
                    // Asynchronous processing to extract feature values and comparisons, otherwise the page will compare cards
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        if (rect.left != 0 && rect.right != 0 && rect.top != 0 && rect.bottom != 0)
                        {
                            bool isLiveness = false;
                            try
                            {
                                // Get the picture under the current camera
                                if (irBitmap != null)
                                {
                                    // Detect human faces and get Rect box
                                    ASF_MultiFaceInfo irMultiFaceInfo = FaceUtil.DetectFace(pVideoIRImageEngine, irBitmap);
                                    if (irMultiFaceInfo.faceNum <= 0)
                                    {
                                        return;
                                    }
                                    // Get the biggest face
                                    ASF_SingleFaceInfo irMaxFace = FaceUtil.GetMaxFace(irMultiFaceInfo);
                                    // Get Rect
                                    MRECT irRect = irMaxFace.faceRect;

                                    // Determine whether the offset between the face frame detected by the RGB picture and the face frame detected by the IR camera is within the tolerance
                                    if (isInAllowErrorRange(rect.left * offsetX / irOffsetX, irRect.left) && isInAllowErrorRange(rect.right * offsetX / irOffsetX, irRect.right)
                                        && isInAllowErrorRange(rect.top * offsetY / irOffsetY, irRect.top) && isInAllowErrorRange(rect.bottom * offsetY / irOffsetY, irRect.bottom))
                                    {
                                        int retCode_Liveness = -1;
                                        // Convert the image to grayscale, and then obtain the image data
                                        ImageInfo irImageInfo = ImageUtil.ReadBMP_IR(irBitmap);
                                        if (irImageInfo == null)
                                        {
                                            return;
                                        }
                                        //IR Live detection
                                        ASF_LivenessInfo liveInfo = FaceUtil.LivenessInfo_IR(pVideoIRImageEngine, irImageInfo, irMultiFaceInfo, out retCode_Liveness);

                                        // Judging test results
                                        if (retCode_Liveness == 0 && liveInfo.num > 0)
                                        {
                                            int isLive = MemoryUtil.PtrToStructure<int>(liveInfo.isLive);
                                            isLiveness = (isLive == 1) ? true : false;
                                        }
                                        if (irImageInfo != null)
                                        {
                                            MemoryUtil.Free(irImageInfo.imgData);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                trackIRUnit.message = string.Format("IR{0}", isLiveness ? " Living Body" : " Non-living Body");
                                if (irBitmap != null)
                                {
                                    irBitmap.Dispose();
                                }
                                isIRLock = false;
                            }
                        }
                        else
                        {
                            trackIRUnit.message = string.Empty;
                        }
                        isIRLock = false;
                    }));
                }
            }
        }


        /// <summary>
        /// Get feature comparison results
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Camera playback completion event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reason"></param>
        private void videoSource_PlayingFinished(object sender, AForge.Video.ReasonToFinishPlaying reason)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                btnSelectImg.Enabled = true;
                btnMatch.Enabled = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #region Threshold_Correlation
        /// <summary>
        /// Threshold text box key press event, check whether the input is correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtThreshold_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Prevent keys from being entered from the keyboard
            e.Handled = true;

            // Is a numeric key, back key, can be entered, others cannot be entered
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == '.')
            {
                // The contents of the channel's current text box
                string thresholdStr = txtThreshold.Text.Trim();
                int countStr = 0;
                int startIndex = 0;

                // If the current input is "."
                if (e.KeyChar == '.')
                {
                    countStr = 1;
                }

                // Check if the current content contains.
                if (thresholdStr.IndexOf('.', startIndex) > -1)
                {
                    countStr += 1;
                }

                // If you have entered more than 12 characters,
                if (e.KeyChar != 8 && (thresholdStr.Length > 12 || countStr > 1))
                {
                    return;
                }
                e.Handled = false;
            }
        }

        /// <summary>
        /// Threshold text box key up event, check whether the threshold is correct, incorrectly changed to 0.8f
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtThreshold_KeyUp(object sender, KeyEventArgs e)
        {
            // If you enter something incorrectly change to the default value
            if (!float.TryParse(txtThreshold.Text.Trim(), out threshold))
            {
                threshold = 0.8f;
            }
        }
        #endregion

        #region Form closed
        /// <summary>
        /// Form Closed Event
        /// </summary>
        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // Turn off the Camera
                if (videoDev.IsRunning)
                {
                    cameraSwitch_CheckedChanged(sender, e);
                }

                // Destroy the engine
                int retCode = ASFFunctions.ASFUninitEngine(pImageEngine);
                Console.WriteLine("UninitEngine pImageEngine Result:" + retCode);
                // Destroy the engine
                retCode = ASFFunctions.ASFUninitEngine(pVideoEngine);
                Console.WriteLine("UninitEngine pVideoEngine Result:" + retCode);

                // Destroy the engine
                retCode = ASFFunctions.ASFUninitEngine(pVideoRGBImageEngine);
                Console.WriteLine("UninitEngine pVideoImageEngine Result:" + retCode);

                // Destroy the engine
                retCode = ASFFunctions.ASFUninitEngine(pVideoIRImageEngine);
                Console.WriteLine("UninitEngine pVideoIRImageEngine Result:" + retCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UninitEngine pImageEngine Error:" + ex.Message);
            }
        }
        #endregion

        #region Public_Method
        /// <summary>
        /// Restore Use / Disable Control List Control
        /// </summary>
        /// <param name="isEnable"></param>
        /// <param name="controls">List of controls</param>
        private void ControlsEnable(bool isEnable,params Control[] controls)
        {
            if(controls == null || controls.Length <= 0)
            {
                return;
            }
            foreach(Control control in controls)
            {
                control.Enabled = isEnable;
            }
        }

        /// <summary>
        ///  Checkout picture
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private bool checkImage(string imagePath)
        {
            if (imagePath == null)
            {
                AppendText("The picture does not exist, please confirm before importing!\r\n");
                return false;
            }
            try
            {
                // Determine whether the picture is normal, such as changing the suffix of other files to .jpg, this will report an error
                Image image = ImageUtil.readFromFile(imagePath);
                if(image == null)
                {
                    throw new Exception();
                }else
                {
                    image.Dispose();
                }
            }
            catch
            {
                AppendText(string.Format("{0} There is a problem with the picture format, please confirm before importing!\r\n", imagePath));
                return false;
            }
            FileInfo fileCheck = new FileInfo(imagePath);
            if (fileCheck.Exists == false)
            {
                AppendText(string.Format("{0} Does not exist!\r\n", fileCheck.Name));
                return false;
            }
            else if (fileCheck.Length > maxSize)
            {
                AppendText(string.Format("{0} Image size exceeds 2M, please compress before importing!\r\n", fileCheck.Name));
                return false;
            }
            else if (fileCheck.Length < 2)
            {
                AppendText(string.Format("{0} The image quality is too low, please select again!\r\n", fileCheck.Name));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Append public method
        /// </summary>
        /// <param name="message"></param>
        private void AppendText(string message)
        {
            logBox.AppendText(message);
        }

        /// <summary>
        /// Determine whether parameter 0 and parameter 1 are within the tolerance range
        /// </summary>
        /// <param name="arg0">Parameter 0</param>
        /// <param name="arg1">Parameter 2</param>
        /// <returns></returns>
        private bool isInAllowErrorRange(float arg0, float arg1)
        {
            bool rel = false;
            if (arg0 > arg1 - allowAbleErrorRange && arg0 < arg1 + allowAbleErrorRange)
            {
                rel = true;
            }
            return rel;
        }
        #endregion

        /// <summary>
        /// Get the path of User Images
        /// </summary>
        private string GetImagePath()
        {
            string UserImages = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)
                         + Path.DirectorySeparatorChar.ToString() + "UserImages";
            if (!Directory.Exists(UserImages))
            {
                Directory.CreateDirectory(UserImages);
            }
            return UserImages;
        }

        ///// <summary>
        ///// Store the images on the local address, and show the images on the picturebox
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnPhotograph_Click(object sender, EventArgs e)
        //{
        //    // The video device must be opened
        //    if (rgbDeviceVideo == null)
        //    {
        //        MessageBox.Show("Please connect the video first !");
        //    }
        //    else if (videoDev.IsRunning)
        //    {
        //        Bitmap bitmap = videoDev.GetCurrentVideoFrame();
        //        // picUserImage.Image = bitmap;
        //        GetImagePath();
        //        string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".jpg";
        //        bitmap.Save(@"C:\Users\72946\Desktop\GU 2\Gym Management System 3.0\Gym_Management_System\Gym_Management_System\bin\x64\Debug\UserImages\" + fileName, ImageFormat.Jpeg);

        //        //bitmap.Save(@"D:\" + fileName, ImageFormat.Jpeg);
        //        //bitmap.Dispose();
        //        //DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff");
        //    }
        //}

        private void FaceForm_Load(object sender, EventArgs e)
        {
            // Open the timer
            timer1.Start();
            // Set the unit of timer
            timer1.Interval = 1000;
            // Connect the Database
            ConnDB();
            CloseDB();
        }

        /// <summary>
        /// Reture to the main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picLogo_Click(object sender, EventArgs e)
        {
            // Stop the video device
            if (videoDev.IsRunning)
            {
                videoDev.SignalToStop();
            }
            this.Hide(); 
        }

        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Forbid Chinese characters
            if ((e.KeyChar >= (char)0x4e00) && (e.KeyChar <= (char)0x9fa5))
            {
                e.Handled = true;
            }

            // Auto click the search button to search the information
            if (e.KeyChar == (char)Keys.Enter)
            {
                picSearchNum_Click(sender, e);
                txtCardNumber.Text = "";
            }
        }

        /// <summary>
        /// The method to compare the datatime
        /// </summary>
        private void DateCompare()
        {
            string strnow = DateTime.Now.ToShortDateString();
            // Convert date string to date object
            DateTime t1 = DateTime.Parse(strnow);
            DateTime t2 = DateTime.Parse(txtDeadline.Text);
            // Compare by DateTIme.Compare
            compNum = DateTime.Compare(t1, t2);
        }

        /// <summary>
        /// Convert byte [] array to Image
        /// </summary>
        /// <param name="btImage">byte[]</param>
        /// <returns>Image img</returns>
        public Image ByteToImg(byte[] btImage)
        {
            MemoryStream memStream = new MemoryStream();
            //Stream memStream = null;
            memStream.Write(btImage, 0, btImage.Length);
            memStream.Position = 0;
            memStream.Seek(0, SeekOrigin.Begin);
            //Bitmap bmp = new Bitmap(memStream, true);
            Image img;
            try
            {
                img = Image.FromStream(memStream, true);
                memStream.Close();
                //img = new Bitmap(memStream);

            }
            catch
            {
                img = null;
            }
            finally
            {
                //memStream.Close();
            }
            return img;
        }

        /// <summary>
        /// Search the Member information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picSearchNum_Click(object sender, EventArgs e)
        {
            // The search contents can not be empty
            if (txtCardNumber.Text == "")
            {
                MessageBox.Show("The search content cannot be empty !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                bool flag;  // Judge if search successfully
                ConnDB();
                ds = new DataSet();
                DataSet dsTime = new DataSet();
                // Write the mysql statement for the search and assign the value   
                string sql = "Select * from Member where Number ='" + txtCardNumber.Text + "'";
                string Timessql = "Select * from Card_Order where Number ='" + txtCardNumber.Text + "'";
                // Update the Exercise times by card number
                string exNusql1 = string.Format("Update Card_order set Exercise_Times = ( Exercise_Times + '1') Where Number = ('{0}')", txtCardNumber.Text);

                // Represents a transact-sql statement or stored procedure to execute against the SQL Server database
                MySqlCommand exNucmd1 = new MySqlCommand(exNusql1, conn);
                MySqlCommand Times = new MySqlCommand(Timessql, conn);
                ad = new MySqlDataAdapter(sql, conn);
                MySqlDataAdapter adTimes = new MySqlDataAdapter(Timessql, conn);
                ad.Fill(ds);
                adTimes.Fill(dsTime);

                // The search contents is right
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    flag = true;
                    exNucmd1.ExecuteNonQuery();
                    Times.ExecuteNonQuery();
                }
                else // Search failed
                {
                    // Search the personal by Member name
                    string sql2 = "Select * from Member where Name ='" + txtCardNumber.Text + "'";
                    string Timessql2 = "Select * from Card_Order where Name ='" + txtCardNumber.Text + "'";
                    // Update the Exercise Times by Member name
                    string exNusql2 = string.Format("Update Card_order set Exercise_Times = ( Exercise_Times + '1') Where Name = ('{0}')", txtCardNumber.Text);
                    MySqlCommand exNucmd2 = new MySqlCommand(exNusql2, conn);
                    MySqlCommand Times2 = new MySqlCommand(Timessql2, conn);
                    ad = new MySqlDataAdapter(sql2, conn);
                    MySqlDataAdapter adTimes2 = new MySqlDataAdapter(Timessql2, conn);
                    ad.Fill(ds);
                    adTimes2.Fill(dsTime);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        flag = true;
                        exNucmd2.ExecuteNonQuery();
                        Times2.ExecuteNonQuery();
                    }
                    else
                    {
                        // Search the personal by Member Telephone number
                        string sql3 = "Select * from Member where TelNum ='" + txtCardNumber.Text + "'";
                        ad = new MySqlDataAdapter(sql3, conn);
                        ad.Fill(ds);
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

                //  Judge the member personal information
                if (flag == true)
                {
                    try
                    {
                        Clear();
                        successSound.Play();
                        // Assigns the [] value of row 0 of the 0th table in the cache
                        txtIdentity.Text = ds.Tables[0].Rows[0][0].ToString();
                        txtNumber.Text = ds.Tables[0].Rows[0][1].ToString();
                        txtName.Text = ds.Tables[0].Rows[0][2].ToString();
                        txtTel.Text = ds.Tables[0].Rows[0][3].ToString();
                        txtEmail.Text = ds.Tables[0].Rows[0][4].ToString();
                        txtGender.Text = ds.Tables[0].Rows[0][5].ToString();
                        txtBirth.Text = ds.Tables[0].Rows[0][6].ToString();
                        txtAddress.Text = ds.Tables[0].Rows[0][9].ToString();
                        txtRegister.Text = ds.Tables[0].Rows[0][10].ToString();
                        txtDeadline.Text = ds.Tables[0].Rows[0][11].ToString();
                        txtConsultant.Text = dsTime.Tables[0].Rows[0][5].ToString();
                        txtTimes.Text = dsTime.Tables[0].Rows[0][6].ToString();
                        // Show the user images
                        string pic = ds.Tables[0].Rows[0][13].ToString();
                        picUserImage.Image = Image.FromFile(pic);

                    }
                    catch (Exception ex)
                    {
                         MessageBox.Show(ex.ToString());
                    }

                    // Compare the data and deadline
                    DateCompare();
                    // The card is valid to use
                    if (compNum == 0)   // Today is the deadline
                    {
                        MessageBox.Show("This membership card is about to expire !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (compNum > 0)  // The card's valid date out of deadline
                    {
                        errorSound.Play();
                        MessageBox.Show("The Membership Card isn't Valid !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {                   
                    ConnDB();
                    DataSet ds2 = new DataSet();
                    // Write the mysql statement for the search and assign the value   
                    string Trasql = "Select * from Trainer where Number ='" + txtCardNumber.Text + "'";
                    MySqlDataAdapter ad2 = new MySqlDataAdapter(Trasql, conn);
                    ad2.Fill(ds2);
                    // The search contents is right
                    if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                    {
                        flag2 = true; 
                    }
                    else
                    {
                        // Write the mysql statement for the search by trainer name
                        string Trasql2 = "Select * from Trainer where Name ='" + txtCardNumber.Text + "'";
                        ad2 = new MySqlDataAdapter(Trasql2, conn);
                        ad2.Fill(ds2);
                        if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            // Write the mysql statement for the search by trainer Telephone number
                            string Trasql3 = "Select * from Trainer where TelNum ='" + txtCardNumber.Text + "'";
                            ad2 = new MySqlDataAdapter(Trasql3, conn);
                            ad2.Fill(ds2);
                            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                            {
                                flag = true;
                            }
                            else
                            {
                                flag2 = false;
                            }
                        }
                    }

                    //  Judge the trainer personal information
                    if (flag2 == true)
                    {
                        try
                        {
                            Clear();
                            successSound.Play();
                            // Assigns the [] value of row 0 of the 0th table in the cache
                            txtIdentity.Text = ds2.Tables[0].Rows[0][0].ToString();
                            txtNumber.Text = ds2.Tables[0].Rows[0][1].ToString();
                            txtName.Text = ds2.Tables[0].Rows[0][2].ToString();
                            txtTel.Text = ds2.Tables[0].Rows[0][3].ToString();
                            txtEmail.Text = ds2.Tables[0].Rows[0][4].ToString();
                            txtGender.Text = ds2.Tables[0].Rows[0][5].ToString();
                            txtBirth.Text = ds2.Tables[0].Rows[0][6].ToString();
                            txtAddress.Text = ds2.Tables[0].Rows[0][9].ToString();
                            txtRegister.Text = ds2.Tables[0].Rows[0][10].ToString();
                            txtDeadline.Text = "";
                            // Show the user images
                            string pic = ds2.Tables[0].Rows[0][11].ToString();
                            this.picUserImage.Image = Image.FromFile(pic);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                    {
                        Clear();
                        errorSound.Play();
                        MessageBox.Show("The User Information Doesn't Exist ! \n Please try it again !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }       


        public void Clear()
        {
            txtNumber.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtBirth.Text = "";
            txtCardNumber.Text = "";
            txtDeadline.Text = "";
            txtEmail.Text = "";
            txtIdentity.Text = "";
            txtGender.Text = "";
            txtRegister.Text = "";
            txtTel.Text = "";
            txtConsultant.Text = "";
            txtTimes.Text = "";
            picUserImage.Image = null;
        }

        // Show the current time at ledTime control
        private void timer1_Tick(object sender, EventArgs e)
        {
            ucledTime1.Value = DateTime.Now;
        }

        // Refresh and clear the page inofor
        private void picRefresh_Click(object sender, EventArgs e)
        {
            Clear();
            txtCardNumber.Focus();
        }

        /// <summary>
        /// Display the card number via Card Reader
        /// </summary>
        public void ReadCard()
        {
            byte status; //Store the return value
            byte[] myserial = new byte[5]; //Card serial number
            status = idr_read(myserial);

            switch (status)
            {
                case 0:
                    idr_beep(50);
                    txtCardNumber.Text = System.Convert.ToString(myserial[1] * 256 * 256 * 256 + myserial[2] * 256 * 256 + myserial[3] * 256 + myserial[4]);
                    // MessageBox.Show("Card reading successful，Card Number: " + System.Convert.ToString(myserial[1] * 256 * 256 * 256 + myserial[2] * 256 * 256 + myserial[3] * 256 + myserial[4]));
                    break;
                case 8:
                    MessageBox.Show("Please place the card in the sensing area !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;

                default:
                    MessageBox.Show("Card reading failed, Please make sure the hardware connection is normal !" + status, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }    
    }
}
