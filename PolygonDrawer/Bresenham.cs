using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PolygonDrawer
{
    static class Bresenham
    {
        public static List<Point> CalculateBresenhamLine(int x0, int y0, int x1, int y1, out List<Point> vertices)
        {
            List<Point> pixels = new List<Point>();
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
             
            for (; ; )
            {
                pixels.Add(new Point(x0, y0));
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
            vertices = new List<Point> { pixels[0], pixels[pixels.Count - 1] };
            return pixels;
        }

        public static List<List<Point>> CalculateBresenhamCircle(int xc, int yc, int r)
        {
            int x = 0;
            int y = r;
            int p = 3 - 2 * r;
            if (r == 0) return null;
            List<List<Point>> pixels = new List<List<Point>>();
            Color color = Colors.Black;
            while (y >= x) // only formulate 1/8 of circle 
            {
                pixels.Add(new List<Point> { new Point(xc - x, yc - y), new Point(xc + x, yc - y) });
                pixels.Add(new List<Point> { new Point(xc - y, yc - x), new Point(xc + y, yc - x) });
                pixels.Add(new List<Point> { new Point(xc - x, yc + y), new Point(xc + x, yc + y) });
                pixels.Add(new List<Point> { new Point(xc - y, yc + x), new Point(xc + y, yc + x) });
                if (p < 0) p += 4 * x++ + 6;
                else p += 4 * (x++ - y--) + 10;
            }
            return pixels;
        }
    }
}
