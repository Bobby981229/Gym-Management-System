using Gym_Management_System.Entity;
using Gym_Management_System.SDKModels;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Gym_Management_System.Utils
{
    public class ImageUtil
    {
        public static Image readFromFile(string imageUrl)
        {
            Image img = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(imageUrl, FileMode.Open, FileAccess.Read);
                img = Image.FromStream(fs);
            }
            finally
            {
                fs.Close();
            }
            return img;
        }

        /// <summary>
        /// Get picture information
        /// </summary>
        /// <param name="image">photograph</param>
        /// <returns>Success or failure</returns>
        public static ImageInfo ReadBMP(Image image)
        {
            ImageInfo imageInfo = new ImageInfo();
            Image<Bgr, byte> my_Image = null;
            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap(image);
                if(bitmap == null)
                {
                    return null;
                }
                my_Image = new Image<Bgr, byte>(bitmap);
                imageInfo.format = ASF_ImagePixelFormat.ASVL_PAF_RGB24_B8G8R8;
                imageInfo.width = my_Image.Width;
                imageInfo.height = my_Image.Height;

                imageInfo.imgData = MemoryUtil.Malloc(my_Image.Bytes.Length);
                MemoryUtil.Copy(my_Image.Bytes, 0, imageInfo.imgData, my_Image.Bytes.Length);

                return imageInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (my_Image != null)
                {
                    my_Image.Dispose();
                }
                if(bitmap != null)
                {
                    bitmap.Dispose();
                }
            }
            return null;
        }

        /// <summary>
        /// Get image IR information
        /// </summary>
        /// <param name="image">photograph</param>
        /// <returns>Success or failure</returns>
        public static ImageInfo ReadBMP_IR(Bitmap bitmap)
        {
            ImageInfo imageInfo = new ImageInfo();
            Image<Bgr, byte> my_Image = null;
            Image<Gray, byte> gray_image = null;
            try
            {
                //Image grayscale conversion
                my_Image = new Image<Bgr, byte>(bitmap);
                gray_image = my_Image.Convert<Gray, byte>(); //Graying function
                imageInfo.format = ASF_ImagePixelFormat.ASVL_PAF_GRAY;
                imageInfo.width = gray_image.Width;
                imageInfo.height = gray_image.Height;
                imageInfo.imgData = MemoryUtil.Malloc(gray_image.Bytes.Length);
                MemoryUtil.Copy(gray_image.Bytes, 0, imageInfo.imgData, gray_image.Bytes.Length);

                return imageInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (my_Image != null)
                {
                    my_Image.Dispose();
                }
                if (gray_image != null)
                {
                    gray_image.Dispose();
                }
            }

            return null;
        }

        /// <summary>
        /// Mark the area of the image with a rectangle
        /// </summary>
        /// <param name="image">photograph</param>
        /// <param name="startX">The x-coordinate in the upper left corner of the rectangle</param>
        /// <param name="startY">The Y coordinate in the upper left corner of the rectangle</param>
        /// <param name="width">Rectangle width</param>
        /// <param name="height">Rectangle height</param>
        /// <returns>After marking the picture</returns>
        public static Image MarkRect(Image image, int startX, int startY, int width, int height)
        {
            Image clone = (Image)image.Clone();
            Graphics g = Graphics.FromImage(clone);
            try
            {
                Brush brush = new SolidBrush(Color.Red);
                Pen pen = new Pen(brush, 2);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawRectangle(pen, new Rectangle(startX, startY, width, height));
                return clone;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                g.Dispose();
            }
            return null;
        }

        /// <summary>
        /// Mark the area of the image with a rectangle, add age and gender
        /// </summary>
        /// <param name="image">photograph</param>
        /// <param name="startX">The x-coordinate in the upper left corner of the rectangle</param>
        /// <param name="startY">The Y coordinate in the upper left corner of the rectangle</param>
        /// <param name="width">Rectangle width</param>
        /// <param name="height">Rectangle height</param>
        /// <param name="age">Age</param>
        /// <param name="gender">Gebder</param>
        /// <returns>After marking the picture</returns>
        public static Image MarkRectAndString(Image image, int startX, int startY, int width, int height, int age, int gender,int showWidth)
        {
            Image clone = (Image)image.Clone();
            Graphics g = Graphics.FromImage(clone);
            try
            {
                Brush brush = new SolidBrush(Color.Red);
                int penWidth = image.Width / showWidth;
                Pen pen = new Pen(brush, penWidth > 1? 2* penWidth:2);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawRectangle(pen, new Rectangle(startX < 1 ? 0 : startX, startY < 1 ? 0 : startY, width, height));
                string genderStr = "";
                if(gender >= 0)
                {
                    if(gender == 0)
                    {
                        genderStr = "Male";
                    }
                    else if (gender == 1)
                    {
                        genderStr = "Female";
                    }
                }
                int fontSize = image.Width / showWidth;
                if (fontSize > 1)
                {
                    int temp = 12;
                    for (int i = 0; i < fontSize; i++)
                    {
                        temp += 6;
                    }
                    fontSize = temp;
                } else if (fontSize == 1) {
                    fontSize = 14;
                } else
                {
                    fontSize = 12;
                }
                g.DrawString(string.Format("Age:{0}   Gender:{1}", age, genderStr), new Font(FontFamily.GenericSerif, fontSize), brush, startX < 1?0:startX, (startY - 20)< 1?0: startY - 20);

                return clone;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                g.Dispose();
            }

            return null;
        }

        /// <summary>
        /// Scale the image to the specified width and height
        /// </summary>
        /// <param name="image">The original image</param>
        /// <param name="dstWidth">Target image width</param>
        /// <param name="dstHeight">Target image height</param>
        /// <returns></returns>
        public static Image ScaleImage(Image image, int dstWidth, int dstHeight)
        {
            Graphics g = null;
            try
            {
                //scale up and down           
                float scaleRate = getWidthAndHeight(image.Width, image.Height, dstWidth, dstHeight);
                int width = (int)(image.Width * scaleRate);
                int height = (int)(image.Height * scaleRate);

                //Adjust the width to an integer multiple of 4
                if (width % 4 != 0) {
                    width = width - width % 4;
                }

                Bitmap destBitmap = new Bitmap(width, height);
                g = Graphics.FromImage(destBitmap);
                g.Clear(Color.Transparent);

                //Sets the painting quality of the canvas         
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, new Rectangle((width - width) / 2, (height - height) / 2, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                return destBitmap;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (g != null)
                {
                    g.Dispose();
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the image scale
        /// </summary>
        /// <param name="oldWidth">The original image width</param>
        /// <param name="oldHeigt">The original image with high</param>
        /// <param name="newWidth">Target image width</param>
        /// <param name="newHeight">Target image height</param>
        /// <returns></returns>
        public static float getWidthAndHeight(int oldWidth,int oldHeigt,int newWidth,int newHeight)
        {
            //scale up and down           
            float scaleRate = 0.0f;
            if (oldWidth >= newWidth && oldHeigt >= newHeight)
            {
                int widthDis = oldWidth - newWidth;
                int heightDis = oldHeigt - newHeight;
                if (widthDis > heightDis)
                {
                    scaleRate = newWidth * 1f / oldWidth;
                }
                else
                {
                    scaleRate = newHeight * 1f / oldHeigt;
                }
            }
            else if (oldWidth >= newWidth && oldHeigt < newHeight)
            {
                scaleRate = newWidth * 1f / oldWidth;
            }
            else if (oldWidth < newWidth && oldHeigt >= newHeight)
            {
                scaleRate = newHeight * 1f / oldHeigt;
            }
            else
            {
                int widthDis = newWidth - oldWidth;
                int heightDis = newHeight - oldHeigt;
                if (widthDis > heightDis)
                {
                    scaleRate = newHeight * 1f / oldHeigt;
                }
                else
                {
                    scaleRate = newWidth * 1f / oldWidth;
                }
            }
            return scaleRate;
        }

        /// <summary>
        /// Cut images
        /// </summary>
        /// <param name="src">The original image</param>
        /// <param name="left">The left coordinate</param>
        /// <param name="top">At the top of the coordinates</param>
        /// <param name="right">Right coordinates</param>
        /// <param name="bottom">At the bottom of the coordinatesparam>
        /// <returns>Cropped images</returns>
        public static Image CutImage(Image src,int left,int top,int right,int bottom)
        {
            try
            {
                Bitmap srcBitmap = new Bitmap(src);
                int width = right - left;
                int height = bottom - top;
                Bitmap destBitmap = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(destBitmap))
                {
                    g.Clear(Color.Transparent);

                    //Sets the painting quality of the canvas         
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(srcBitmap, new Rectangle(0, 0, width, height), left, top, width, height, GraphicsUnit.Pixel);
                }

                return destBitmap;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }


    }
}
