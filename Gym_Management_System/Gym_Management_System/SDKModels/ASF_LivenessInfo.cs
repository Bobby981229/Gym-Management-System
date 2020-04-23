using System;

namespace Gym_Management_System.SDKModels
{
    public struct ASF_LivenessInfo
    {
        /// <summary>
        /// Living Person? 
        /// 0: non-real person; 1: real person; -1: uncertain; -2: number of incoming faces> 1;
        /// </summary>
        public IntPtr isLive;
        /// <summary>
        /// Result set size
        /// </summary>
        public int num;
    }
}
