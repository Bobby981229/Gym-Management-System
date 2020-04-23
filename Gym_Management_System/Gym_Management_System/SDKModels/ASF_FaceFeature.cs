using System;

namespace Gym_Management_System.SDKModels
{
    /// <summary>
    /// Facial feature structure
    /// </summary>
    public struct ASF_FaceFeature
    {
        /// <summary>
        /// Eigenvalue byte []
        /// </summary>
        public IntPtr feature;

        /// <summary>
        /// Result set size
        /// </summary>
        public int featureSize;
    }
}
