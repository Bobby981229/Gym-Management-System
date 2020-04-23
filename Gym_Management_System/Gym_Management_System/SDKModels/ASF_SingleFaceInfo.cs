using System;

namespace Gym_Management_System.SDKModels
{
    /// <summary>
    /// Single face detection structure
    /// </summary>
    public struct ASF_SingleFaceInfo
    {
        /// <summary>
        /// Face coordinate Rect result
        /// </summary>
        public MRECT faceRect;
        /// <summary>
        /// Face angle
        /// </summary>
        public int faceOrient;
    }
}
