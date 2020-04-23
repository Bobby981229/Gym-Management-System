using System;

namespace Gym_Management_System.SDKModels
{
    /// <summary>
    /// Face detection priority angle structure, ASF_OP_0_HIGHER_EXT is recommended
    /// </summary>
    public struct ASF_OrientPriority
    {
        public const int ASF_OP_0_ONLY = 0x1;
        public const int ASF_OP_90_ONLY = 0x2;
        public const int ASF_OP_270_ONLY = 0x3;
        public const int ASF_OP_180_ONLY = 0x4;
        public const int ASF_OP_0_HIGHER_EXT = 0x5;
    }
}
