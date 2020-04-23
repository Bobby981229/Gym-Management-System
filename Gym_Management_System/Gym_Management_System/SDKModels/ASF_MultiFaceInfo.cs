using System;

namespace Gym_Management_System.SDKModels
{
    /// <summary>
    /// Multi-face detection structure
    /// </summary>
    public struct ASF_MultiFaceInfo
    {
        /// <summary>
        /// Face Rect result set
        /// </summary>
        public IntPtr faceRects;

        /// <summary>
        /// Face angle result set, one-to-one correspondence with faceRects, corresponding to ASF_OrientCode
        /// </summary>
        public IntPtr faceOrients;
        /// <summary>
        /// Result set size
        /// </summary>
        public int faceNum;
        /// <summary>
        /// face ID, FaceID is not returned in IMAGE mode
        /// </summary>
        public IntPtr faceID;
    }
}
