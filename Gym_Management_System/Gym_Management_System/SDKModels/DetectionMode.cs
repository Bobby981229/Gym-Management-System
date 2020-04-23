using System;

namespace Gym_Management_System.SDKModels
{
    /// <summary>
    /// Picture detection mode
    /// </summary>
    public struct DetectionMode
    {
        /// <summary>
        /// Video mode, generally used for continuous detection of multiple frames
        /// </summary>
        public const uint ASF_DETECT_MODE_VIDEO = 0x00000000;

        /// <summary>
        /// Image mode, generally used for single detection of static images
        /// </summary>
        public const uint ASF_DETECT_MODE_IMAGE = 0xFFFFFFFF;
    }
}
