using System;
using System.Runtime.InteropServices;

namespace Gym_Management_System.SDKUtil
{
    /// <summary>
    /// Package functions related to face recognition in SDK
    /// </summary>
    public class ASFFunctions
    {

        /// <summary>
        /// SDK dynamic link library path
        /// </summary>
        public const string Dll_PATH = "libarcsoft_face_engine.dll";

        /// <summary>
        /// Activate the face recognition SDK engine function
        /// </summary>
        /// <param name="appId">AppID corresponding to SDK</param>
        /// <param name="sdkKey">SDKKey corresponding to SDK</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFActivation(string appId, string sdkKey);

        /// <summary>
        /// Initialize the engine
        /// </summary>
        /// <param name="detectMode">AF_DETECT_MODE_VIDEO Video mode | AF_DETECT_MODE_IMAGE Picture mode</param>
        /// <param name="detectFaceOrientPriority">Detect the angle priority of the face, recommended：ASF_OrientPriority.ASF_OP_0_HIGHER_EXT</param>
        /// <param name="detectFaceScaleVal">Minimum face size for numerical representation</param>
        /// <param name="detectFaceMaxNum">Maximum number of faces to be detected</param>
        /// <param name="combinedMask">The user selects the combination of functions to be detected, which can be single or multiple</param>
        /// <param name="pEngine">Initialize the returned engine handle</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFInitEngine(uint detectMode, int detectFaceOrientPriority, int detectFaceScaleVal, int detectFaceMaxNum, int combinedMask, ref IntPtr pEngine);

        /// <summary>
        /// Face Detection
        /// </summary>
        /// <param name="pEngine">Engine handle</param>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        /// <param name="format">Image color space</param>
        /// <param name="imgData">Image data</param>
        /// <param name="detectedFaces">Face detection results</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFDetectFaces(IntPtr pEngine, int width, int height, int format, IntPtr imgData, IntPtr detectedFaces);

        /// <summary>
        /// Face information detection (age / gender / face 3D angle)
        /// </summary>
        /// <param name="pEngine">Engine handle</param>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        /// <param name="format">Image color space</param>
        /// <param name="imgData">Image data</param>
        /// <param name="detectedFaces">Face information, the user selects the face to be used according to the function to be detected</param>
        /// <param name="combinedMask">Only the functions that need to be specified during initialization are supported. During the process, 
        /// further filtering is continued in this already specified function set. For example, the age and gender are specified during initialization. 
        /// During the process, only the age can be detected, but the age and gender cannot be detected. External function</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFProcess(IntPtr pEngine, int width, int height, int format, IntPtr imgData, IntPtr detectedFaces, int combinedMask);


        /// <summary>
        /// Single face feature extraction
        /// </summary>
        /// <param name="pEngine">Engine handle</param>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        /// <param name="format">Image color space</param>
        /// <param name="imgData">Image data</param>
        /// <param name="faceInfo">Individual face position and angle information</param>
        /// <param name="faceFeature">Facial features</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFFaceFeatureExtract(IntPtr pEngine, int width, int height, int format, IntPtr imgData, IntPtr faceInfo, IntPtr faceFeature);

        /// <summary>
        /// Face feature comparison
        /// </summary>
        /// <param name="pEngine">Engine handle</param>
        /// <param name="faceFeature1">Face features to be compared</param>
        /// <param name="faceFeature2"> To compare facial features</param>
        /// <param name="similarity">Similarity (0 ~ 1)</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFFaceFeatureCompare(IntPtr pEngine, IntPtr faceFeature1, IntPtr faceFeature2, ref float similarity);

        /// <summary>
        /// Get age information
        /// </summary>
        /// <param name="pEngine">Engine handle</param>
        /// <param name="ageInfo">Detected age information</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetAge(IntPtr pEngine, IntPtr ageInfo);

        /// <summary>
        /// Get gender information
        /// </summary>
        /// <param name="pEngine">Engine handle</param>
        /// <param name="genderInfo">Detected gender information</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetGender(IntPtr pEngine, IntPtr genderInfo);

        /// <summary>
        /// Get 3D angle information
        /// </summary>
        /// <param name="pEngine">Engine handle</param>
        /// <param name="p3DAngleInfo">Face 3D angle information detected</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetFace3DAngle(IntPtr pEngine, IntPtr p3DAngleInfo);

        /// <summary>
        /// Get RGB live results
        /// </summary>
        /// <param name="hEngine">Engine handle</param>
        /// <param name="livenessInfo">Biopsy information</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetLivenessScore(IntPtr hEngine,IntPtr livenessInfo);

        /// <summary>
        /// This interface currently only supports single-face IR live detection (
        /// does not support detection of age, gender, 3D angle),
        /// the first face is taken by default
        /// </summary>
        /// <param name="pEngine">Engine handle</param>
        /// <param name="width">Picture width</param>
        /// <param name="height">Picture height</param>
        /// <param name="format">Color space format</param>
        /// <param name="imgData">Picture data</param>
        /// <param name="faceInfo">Face information. The user selects the face to be used according to the function to be detected.</param>
        /// <param name="combinedMask">Currently only supports passing in the ASF_IR_LIVENESS attribute, and the initialization 
        /// interface needs to pass in </param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFProcess_IR(IntPtr pEngine, int width, int height, int format, IntPtr imgData, IntPtr faceInfo, int combinedMask);

        /// <summary>
        /// Get IR Live Results
        /// </summary>
        /// <param name="pEngine">Engine handle</param>
        /// <param name="irLivenessInfo">Live IR results detected</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFGetLivenessScore_IR(IntPtr pEngine, IntPtr irLivenessInfo);

        /// <summary>
        /// Destroy the engine
        /// </summary>
        /// <param name="pEngine">Engine handle</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ASFUninitEngine(IntPtr pEngine);

        /// <summary>
        /// Get version information
        /// </summary>
        /// <param name="pEngine">Engine handle</param>
        /// <returns>Call result</returns>
        [DllImport(Dll_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ASFGetVersion(IntPtr pEngine);
    }
}