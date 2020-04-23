using System;

namespace Gym_Management_System.SDKModels
{
    /// <summary>
    /// 3D face angle detection structure
    /// </summary>
    public struct ASF_Face3DAngle
    {
        public IntPtr roll;
        public IntPtr yaw;
        public IntPtr pitch;
        /// <summary>
        /// Whether the test succeeds, 0 succeeds, others fail
        /// </summary>
        public IntPtr status;
        public int num;
    }
}
