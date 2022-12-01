using ArcSoftFace.Entity;
using ArcSoftFace.SDKModels;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Basic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ArcSoftFace.Utils
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
        /// 获取图片信息
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns>成功或失败</returns>

        public static ImageInfo ReadBMP(Image Image)
        {
            return ReadBMP((Bitmap)Image);
        }
        public static ImageInfo ReadBMP(Bitmap SrcBitmap)
        {
            ImageInfo imageInfo = new ImageInfo();
            int Width;
            int Height;
            int ByteOfSkip;
            IntPtr SrcPtr;
            int ByteOfWidth;
            int ColorDepth = 3;
            try
            {
                if (SrcBitmap == null)
                {
                    return null;
                }
                imageInfo.format = ASF_ImagePixelFormat.ASVL_PAF_RGB24_B8G8R8;
                imageInfo.width = SrcBitmap.Width;
                imageInfo.height = SrcBitmap.Height;

                BitmapData bmData = SrcBitmap.LockBits(new Rectangle(0, 0, SrcBitmap.Width, SrcBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int Stride = bmData.Stride;
                Width = SrcBitmap.Width;
                Height = SrcBitmap.Height;
                ByteOfSkip = GetBitmapSkip(SrcBitmap.Width, ColorDepth);
                SrcPtr = bmData.Scan0;
                ByteOfWidth = (Width) * ColorDepth + ByteOfSkip;
                imageInfo.imgData = MemoryUtil.Malloc(ByteOfWidth * Height);
                HsBasic.Memory.CopyPtr(SrcPtr, imageInfo.imgData, ByteOfWidth * Height);
                SrcBitmap.UnlockBits(bmData);
                return imageInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
            return null;
        }
        public static ImageInfo ReadBMP(Bitmap SrcBitmap, ImageInfo ImageInfo)
        {
            int Width;
            int Height;
            int ByteOfSkip;
            IntPtr SrcPtr;
            int ByteOfWidth;
            int ColorDepth = 3;
            try
            {
                if (SrcBitmap == null)
                {
                    return null;
                }


                BitmapData bmData = SrcBitmap.LockBits(new Rectangle(0, 0, SrcBitmap.Width, SrcBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int Stride = bmData.Stride;
                Width = SrcBitmap.Width;
                Height = SrcBitmap.Height;
                ByteOfSkip = GetBitmapSkip(SrcBitmap.Width, ColorDepth);
                SrcPtr = bmData.Scan0;
                ByteOfWidth = (Width) * ColorDepth + ByteOfSkip;

                if (ImageInfo == null)
                {
                    ImageInfo = new Entity.ImageInfo();
                    ImageInfo.imgData = MemoryUtil.Malloc(ByteOfWidth * Height);
                }
                ImageInfo.format = ASF_ImagePixelFormat.ASVL_PAF_RGB24_B8G8R8;
                ImageInfo.width = SrcBitmap.Width;
                ImageInfo.height = SrcBitmap.Height;

                HsBasic.Memory.CopyPtr(SrcPtr, ImageInfo.imgData, ByteOfWidth * Height);
                SrcBitmap.UnlockBits(bmData);
                return ImageInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
            return null;
        }
        static public int GetBitmapSkip(int ByteOfNumWidth, int ColorDepth)
        {
            int ByteOfSkip = ByteOfNumWidth * ColorDepth % 4;
            if (ByteOfSkip > 0) ByteOfSkip = 4 - ByteOfSkip;
            return ByteOfSkip;
        }
        /// <summary>
        /// 获取图片IR信息
        /// </summary>
        /// <param name="image">图片</param>
        /// <returns>成功或失败</returns>
        public static ImageInfo ReadBMP_IR(Bitmap bitmap)
        {
            ImageInfo imageInfo = new ImageInfo();
            Image<Bgr, byte> my_Image = null;
            Image<Gray, byte> gray_image = null;
            try
            {
                //图像灰度转化
                my_Image = new Image<Bgr, byte>(bitmap);
                gray_image = my_Image.Convert<Gray, byte>(); //灰度化函数
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
        /// 用矩形框标记图片指定区域
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="startX">矩形框左上角X坐标</param>
        /// <param name="startY">矩形框左上角Y坐标</param>
        /// <param name="width">矩形框宽度</param>
        /// <param name="height">矩形框高度</param>
        /// <returns>标记后的图片</returns>
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
        /// 用矩形框标记图片指定区域，添加年龄和性别标注
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="startX">矩形框左上角X坐标</param>
        /// <param name="startY">矩形框左上角Y坐标</param>
        /// <param name="width">矩形框宽度</param>
        /// <param name="height">矩形框高度</param>
        /// <param name="age">年龄</param>
        /// <param name="gender">性别</param>
        /// <returns>标记后的图片</returns>
        public static Image MarkRectAndString(Image image, int startX, int startY, int width, int height, int age, int gender, int showWidth)
        {
            Image clone = (Image)image.Clone();
            Graphics g = Graphics.FromImage(clone);
            try
            {
                Brush brush = new SolidBrush(Color.Red);
                int penWidth = image.Width / showWidth;
                Pen pen = new Pen(brush, penWidth > 1 ? 2 * penWidth : 2);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawRectangle(pen, new Rectangle(startX < 1 ? 0 : startX, startY < 1 ? 0 : startY, width, height));
                string genderStr = "";
                if (gender >= 0)
                {
                    if (gender == 0)
                    {
                        genderStr = "男";
                    }
                    else if (gender == 1)
                    {
                        genderStr = "女";
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
                }
                else if (fontSize == 1)
                {
                    fontSize = 14;
                }
                else
                {
                    fontSize = 12;
                }
                g.DrawString(string.Format("Age:{0}   Gender:{1}", age, genderStr), new Font(FontFamily.GenericSerif, fontSize), brush, startX < 1 ? 0 : startX, (startY - 20) < 1 ? 0 : startY - 20);

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
        /// 按指定宽高缩放图片
        /// </summary>
        /// <param name="image">原图片</param>
        /// <param name="dstWidth">目标图片宽</param>
        /// <param name="dstHeight">目标图片高</param>
        /// <returns></returns>
        public static Bitmap ScaleImage(Bitmap SrcBitmap, int dstWidth, int dstHeight)
        {
            Graphics g = null;
            try
            {
                Bitmap DstBitmap = new Bitmap(dstWidth, dstHeight);
                //按比例缩放           
                float scaleRate = getWidthAndHeight(SrcBitmap.Width, SrcBitmap.Height, dstWidth, dstHeight);
                int width = (int)(SrcBitmap.Width * scaleRate);
                int height = (int)(SrcBitmap.Height * scaleRate);

                //将宽度调整为4的整数倍
                if (width % 4 != 0)
                {
                    width = width - width % 4;
                }

                g = Graphics.FromImage(DstBitmap);
                g.Clear(Color.Transparent);

                //设置画布的描绘质量         
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(SrcBitmap, new Rectangle((width - width) / 2, (height - height) / 2, width, height), 0, 0, SrcBitmap.Width, SrcBitmap.Height, GraphicsUnit.Pixel);

                //SrcBitmap.Dispose();

                return DstBitmap;
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
        /// 按指定宽高缩放图片
        /// </summary>
        /// <param name="image">原图片</param>
        /// <param name="dstWidth">目标图片宽</param>
        /// <param name="dstHeight">目标图片高</param>
        /// <returns></returns>
        public static Bitmap ScaleImage(Bitmap SrcBitmap, float ZoomX, float ZoomY)
        {
            Graphics g = null;
            try
            {
                int width = (int)(SrcBitmap.Width * ZoomX);
                int height = (int)(SrcBitmap.Height * ZoomY);
                Bitmap DstBitmap = new Bitmap(width, height);
                //按比例缩放           


                //将宽度调整为4的整数倍
                if (width % 4 != 0)
                {
                    width = width - width % 4;
                }

                g = Graphics.FromImage(DstBitmap);
                g.Clear(Color.Transparent);

                //设置画布的描绘质量         
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(SrcBitmap, new Rectangle((width - width) / 2, (height - height) / 2, width, height), 0, 0, SrcBitmap.Width, SrcBitmap.Height, GraphicsUnit.Pixel);

                //SrcBitmap.Dispose();

                return DstBitmap;
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
        public static Bitmap ScaleImage(ref Bitmap SrcBitmap, int dstWidth, int dstHeight)
        {
            Graphics g = null;
            try
            {
                Bitmap DstBitmap = new Bitmap(dstWidth, dstHeight);
                //按比例缩放           
                float scaleRate = getWidthAndHeight(SrcBitmap.Width, SrcBitmap.Height, dstWidth, dstHeight);
                int width = (int)(SrcBitmap.Width * ((float)dstWidth / (float)SrcBitmap.Width));
                int height = (int)(SrcBitmap.Height * ((float)dstHeight / (float)SrcBitmap.Height));


                g = Graphics.FromImage(DstBitmap);
                g.Clear(Color.Transparent);

                //设置画布的描绘质量         
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(SrcBitmap, new Rectangle((width - width) / 2, (height - height) / 2, width, height), 0, 0, SrcBitmap.Width, SrcBitmap.Height, GraphicsUnit.Pixel);


                return DstBitmap;
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
        /// 获取图片缩放比例
        /// </summary>
        /// <param name="oldWidth">原图片宽</param>
        /// <param name="oldHeigt">原图片高</param>
        /// <param name="newWidth">目标图片宽</param>
        /// <param name="newHeight">目标图片高</param>
        /// <returns></returns>
        public static float getWidthAndHeight(int oldWidth, int oldHeigt, int newWidth, int newHeight)
        {
            //按比例缩放           
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
        /// 剪裁图片
        /// </summary>
        /// <param name="src">原图片</param>
        /// <param name="left">左坐标</param>
        /// <param name="top">顶部坐标</param>
        /// <param name="right">右坐标</param>
        /// <param name="bottom">底部坐标</param>
        /// <returns>剪裁后的图片</returns>
        /// 
        public static Bitmap CutImage(Bitmap SrcBitmap, MRECT mRECT)
        {
            return CutImage(SrcBitmap, mRECT.left, mRECT.top, mRECT.right, mRECT.bottom);
        }
        public static Bitmap CutImage(Bitmap SrcBitmap, int left, int top, int right, int bottom)
        {
            try
            {
                int width = right - left;
                int height = bottom - top;
                Bitmap DstBitmap = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(DstBitmap))
                {
                    g.Clear(Color.Transparent);

                    //设置画布的描绘质量         
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(SrcBitmap, new Rectangle(0, 0, width, height), left, top, width, height, GraphicsUnit.Pixel);
                }
                return DstBitmap;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }


    }
}
