using System;

namespace Gym_Management_System.Entity
{
    public class ImageInfo
    {
        /// <summary>
        /// Pixel data of the picture
        /// </summary>
        public IntPtr imgData { get; set; }

        /// <summary>
        /// Picture pixel width
        /// </summary>
        public int width { get; set; }

        /// <summary>
        /// Picture pixels high
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// Image Format
        /// </summary>
        public int format { get; set; }
    }
}
