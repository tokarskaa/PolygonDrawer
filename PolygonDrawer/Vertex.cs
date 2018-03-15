using System;
using System.Collections.Generic;
using System.Windows;

namespace PolygonDrawer
{
    class Vertex
    {
        List<List<Point>> Pixels { get; set; }
        public Point MiddlePoint { get; set; }
        public Vertex(List<List<Point>> pixels, Point middlePoint)
        {
            Pixels = pixels;
            MiddlePoint = middlePoint;
        }

        public double GetX()
        {
            return MiddlePoint.X;
        }

        public double GetY()
        {
            return MiddlePoint.Y;
        }

        public void SetMiddlePoint(Point point)
        {
            MiddlePoint = point;
            Pixels = Bresenham.CalculateBresenhamCircle((int)point.X, (int)point.Y, 6);
        }

        public static Vertex GetVertexFromMiddlePoint(Point middlePoint, int r)
        {
            List<List<Point>> pixels = Bresenham.CalculateBresenhamCircle((int)middlePoint.X, (int)middlePoint.Y, r);
            return new Vertex(pixels, middlePoint);
        }

        public bool IsPointInVertex(Point point)
        {
            foreach (var pair in Pixels)
            {
                if ((int)point.Y == pair[0].Y && (int)point.X >= pair[0].X && (int)point.X <= pair[1].X)
                    return true;
            }
            return false;
        }

        public override bool Equals(object o)
        {
            Vertex v = o as Vertex;
            return (GetX() == v.GetX() && GetY() == v.GetY());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}