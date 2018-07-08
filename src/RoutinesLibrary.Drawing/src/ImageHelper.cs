using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace RoutinesLibrary.Drawing
{
    public class ImageHelper
    {
        public static Bitmap SetBackgroundColor(Bitmap bitmap, Color color, PixelFormat pixelFormat = PixelFormat.Format32bppRgb)
        {
            Bitmap temp = new Bitmap(bitmap.Width, bitmap.Height, pixelFormat);
            temp.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
            Graphics g = Graphics.FromImage(temp);
            g.Clear(color);
            g.DrawImage(bitmap, Point.Empty);

            return temp;
        }

        public static Bitmap Resize(Bitmap bitmap, int width, int height, PixelFormat pixelFormat = PixelFormat.Format32bppRgb)
        {
            Bitmap temp = new Bitmap(width, height, pixelFormat);
            temp.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
            Graphics g = Graphics.FromImage(temp);
            g.DrawImage(bitmap, 0, 0, width, height);

            return temp;
        }
    }
}
