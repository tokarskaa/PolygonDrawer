using System;
using System.Collections.Generic;
using System.Windows;

namespace PolygonDrawer
{
    class Line
    {
        public List<Vertex> Vertices { get; set; }
        public bool Vertical { get; set; }
        public bool Horizontal { get; set; }
        public bool FixedLength { get; set; }
        public double Length { get; set; }
        public Line(Vertex v1, Vertex v2)
        {
            Vertices = new List<Vertex> { v1, v2 };
        }


        public bool IsPointInLine(Point point)
        {
            List<Point> pixels = Bresenham.CalculateBresenhamLine((int)Vertices[0].GetX(), (int)Vertices[0].GetY(), (int)Vertices[1].GetX(), (int)Vertices[1].GetY(), out List<Point> v);
            foreach(var pixel in pixels)
            {
                if (point.X >= pixel.X - 5 && point.X <= pixel.X + 5 && point.Y >= pixel.Y - 5 && point.Y <= pixel.Y + 5)
                    return true;
            }
            return false;
        }

        public Point GetMiddlePoint()
        {
            double x = Math.Abs((Vertices[0].GetX() + Vertices[1].GetX()) / 2);
            double y = Math.Abs((Vertices[0].GetY() + Vertices[1].GetY()) / 2);
            return new Point(x, y);
        }

        public int GetLength()
        {
            double a = Math.Abs(Vertices[0].GetX() - Vertices[1].GetX());
            double b = Math.Abs(Vertices[0].GetY() - Vertices[1].GetY());
            return (int)Math.Sqrt(a * a + b * b);
        }

        public bool HasConstraint()
        {
            return Horizontal || Vertical || FixedLength;
        }
        
    }
}