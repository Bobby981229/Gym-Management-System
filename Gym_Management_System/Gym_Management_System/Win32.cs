using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Management_System
{
    public class Win32
    {
        /// <summary>
        /// Perform the animation
        /// </summary>
        /// <param name="whnd">Handle to the control</param>
        /// <param name="dwtime">Animation time</param>
        /// <param name="dwflag">Animation group name</param>
        /// <returns>bool index，Whether the animation is successful or not</returns>
        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);

        /// <summary>
        /// Open the window from left to right
        /// </summary>
        public const Int32 AW_HOR_POSITIVE = 0x00000001;
        /// <summary>
        /// Open the window from right to left
        /// </summary>
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;
        /// <summary>
        /// Open the window from top to bottom
        /// </summary>
        public const Int32 AW_VER_POSITIVE = 0x00000004;
        /// <summary>
        /// Open the window from bottom to top
        /// </summary>
        public const Int32 AW_VER_NEGATIVE = 0x00000008;
        /// <summary>
        /// If the AW HIDE flag is used, the window is overlapped inward. 
        /// If the AW HIDE flag is not used, the window is expanded outwards.
        /// </summary>
        public const Int32 AW_CENTER = 0x00000010;
        /// <summary>
        /// Hide the window and display it by default.
        /// </summary>
        public const Int32 AW_HIDE = 0x00010000;
        /// <summary>
        /// Activate the window. Do not use the AW HIDE flag after it has been used.
        /// </summary>
        public const Int32 AW_ACTIVATE = 0x00020000;
        /// <summary>
        /// Use the sliding type. The default is the scroll animation type. When the AW CENTER flag is used, the flag is ignored.
        /// </summary>
        public const Int32 AW_SLIDE = 0x00040000;
        /// <summary>
        /// Handle to window，duration of animation， animation type
        /// </summary>
        public const Int32 AW_BLEND = 0x00080000;
    }
}
