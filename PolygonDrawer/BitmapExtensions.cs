using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PolygonDrawer
{
    public static class BitmapExtensions
    {
        private static void SetPixels(this WriteableBitmap wbm, List<Point> pixels, Color c)
        {
            
            if (!wbm.Format.Equals(PixelFormats.Bgra32)) return;
            wbm.Lock();
            IntPtr buff = wbm.BackBuffer;
            int Stride = wbm.BackBufferStride;
            unsafe
            {
                byte* pbuff = (byte*)buff.ToPointer();
                foreach (Point pixel in pixels)
                {
                    if (pixel.Y > wbm.PixelHeight - 1 || pixel.X > wbm.PixelWidth - 1) return;
                    if (pixel.Y < 0 || pixel.X < 0) return;
                    int loc = (int)pixel.Y * Stride + (int)pixel.X * 4;
                    pbuff[loc] = c.B;
                    pbuff[loc + 1] = c.G;
                    pbuff[loc + 2] = c.R;
                    pbuff[loc + 3] = c.A;
                    wbm.AddDirtyRect(new Int32Rect((int)pixel.X, (int)pixel.Y, 1, 1));
                }
            }
            wbm.Unlock();
        }
        public static List<Point> DrawBresenhamLine(this WriteableBitmap wbm, int x0, int y0, int x1, int y1, Color color)
        {
            wbm.SetPixels(Bresenham.CalculateBresenhamLine(x0, y0, x1, y1, out List<Point> vertices), color);
            return vertices;
        }
        public static List<List<Point>> DrawBresenhamCircle(this WriteableBitmap wbm, int x0, int y0, int r)
        {
            List<List<Point>> pairs = Bresenham.CalculateBresenhamCircle(x0, y0, r);
            List<Point> pixels = new List<Point>();
            foreach(var pair in pairs)
            {
                pixels.Add(pair[0]);
                pixels.Add(pair[1]);
            }
            wbm.SetPixels(pixels, Colors.Black);
            return pairs;
        }
        

    }


}
