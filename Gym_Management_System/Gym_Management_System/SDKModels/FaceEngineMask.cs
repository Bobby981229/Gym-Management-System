using System;

namespace Gym_Management_System.SDKModels
{
    /// <summary>
    /// Engine method type structure, the type used during initialization is passed in with 
    /// | connection, such as ASF_NONE | ASF_FACE_DETECT | ASF_FACERECOGNITION
    /// </summary>
    public struct FaceEngineMask
    {
        /// <summary>
        /// No method initialization method type
        /// </summary>
        public const int ASF_NONE = 0x00000000;
        /// <summary>
        /// Face detection method type constants
        /// </summary>
        public const int ASF_FACE_DETECT = 0x00000001;
        /// <summary>
        /// Face recognition method type constant, including image feature extraction and feature comparison
        /// </summary>
        public const int ASF_FACERECOGNITION = 0x00000004;
        /// <summary>
        /// Age detection method type constant
        /// </summary>
        public const int ASF_AGE = 0x00000008;
        /// <summary>
        /// Gender detection method type constant
        /// </summary>
        public const int ASF_GENDER = 0x00000010;
        /// <summary>
        /// 3D angle detection method type constant
        /// </summary>
        public const int ASF_FACE3DANGLE = 0x00000020;
        /// <summary>
        /// RGB living body
        /// </summary>
        public const int ASF_LIVENESS = 0x00000080;
        /// <summary>
        /// Infrared living body
        /// </summary>
        public const int ASF_IR_LIVENESS = 0x00000400;
    }
}
