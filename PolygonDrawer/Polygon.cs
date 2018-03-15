using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PolygonDrawer
{
    class Polygon
    {
        public List<Line> Lines { get; set; }
        public Polygon()
        {
            Lines = new List<Line>();
        }

        public bool IsStartPoint(Point point)
        {
            if (Lines.Count == 0)
                return false;
            return Lines[0].Vertices[0].IsPointInVertex(point);
        }

        public void AddLine(Line l)
        {
            if (Lines.Count != 0)
            { 
                l.Vertices[0] = Lines[Lines.Count - 1].Vertices[1];
                if (l.Vertices[1].Equals(Lines[0].Vertices[0]))
                    l.Vertices[1] = Lines[0].Vertices[0];
            }
            Lines.Add(l);
        }
        public Vertex GetVertexFromPoint(Point point)
        {
            if (Lines.Count == 0)
                return null;
            foreach(var line in Lines)
            {
                if (line.Vertices[0].IsPointInVertex(point))
                    return line.Vertices[0];
            }
            return null;
        }

        public Line GetLineFromPoint(Point point)
        {
            if (Lines.Count == 0)
                return null;
            foreach(var line in Lines)
            {
                if (line.IsPointInLine(point))
                    return line;
            }
            return null;
        }

        public bool IsPointInVertices(Point point)
        {
            if (GetVertexFromPoint(point) == null)
                return false;
            return true;
        }

        public bool IsPointInLines(Point point)
        {
            if (GetLineFromPoint(point) == null)
                return false;
            return true;
        }

        public List<Line> GetAdjacentLines(Point point)
        {
            Line l1 = null, l2 = null;
            List<Line> adjacent = new List<Line>();
            foreach(Line line in Lines)
            {
                if (line.Vertices[0].IsPointInVertex(point))
                    l1 = line;
                if (line.Vertices[1].IsPointInVertex(point))
                    l2 = line;
            }
            adjacent.Add(l1);
            adjacent.Add(l2);
            return adjacent;
        }

        public List<Line> GetAdjacentLines(Line l)
        {
            List<Line> adjacent = GetAdjacentLines(l.Vertices[0].MiddlePoint);
            adjacent.AddRange(GetAdjacentLines(l.Vertices[1].MiddlePoint));
            adjacent.Distinct();
            return adjacent;
        }

        public void MoveVertex(Vertex vertex, Point point, out Vertex newVertex)
        {
            List<Line> lines = Lines.FindAll(x => x.Vertices[0].Equals(vertex) || x.Vertices[1].Equals(vertex));
            newVertex = Vertex.GetVertexFromMiddlePoint(point, 6);
            foreach (Line l in lines)
            {
                if (l.Vertices[0].Equals(vertex))
                    l.Vertices[0] = newVertex;
                else
                    l.Vertices[1] = newVertex;
            }
        }

        public void DeleteVertex(Vertex v)
        {
            if (Lines.Count <= 3)
                return;
            List<Line> adjacent = GetAdjacentLines(v.MiddlePoint);
            adjacent[0].Vertices[0] = adjacent[1].Vertices[0];
            adjacent[0].Horizontal = adjacent[0].Vertical = false;
            Lines.Remove(adjacent[1]);
        }

        public void AddVertexOnLine(Line l)
        {
            Vertex newVertex = new Vertex(Bresenham.CalculateBresenhamCircle((int)l.GetMiddlePoint().X, (int)l.GetMiddlePoint().Y, 6), l.GetMiddlePoint());
            Line newLine = new Line(l.Vertices[0], newVertex);
            int index = Lines.FindIndex(x => x.Vertices[0].Equals(l.Vertices[0]) && x.Vertices[1].Equals(l.Vertices[1]));
            Lines.Insert(index + 1, newLine);
            index = Lines.FindIndex(x => x.Vertices[1].Equals(l.Vertices[1]));
            Lines[index].Vertices[0] = newVertex;
            Lines[index].Vertical = Lines[index].Horizontal = false;
        }

        public void MovePolygon(Point vector)
        {
            foreach(var line in Lines)
                line.Vertices[0].SetMiddlePoint(new Point(line.Vertices[0].GetX() + vector.X, line.Vertices[0].GetY() + vector.Y));
        }

        public bool SetLineVertically(Line l)
        {
            List<Line> adjacent = GetAdjacentLines(l);
            if (adjacent[0].Vertical || adjacent[1].Vertical || l.Horizontal)
                return false;
            l.Vertical = true;
            return true;
        }

        public bool ApplyVertical(Line line)
        {
            double eps = 0.2;
            double n;
            if (line.Vertices[0].GetX() > line.Vertices[1].GetX())
                n = -0.1;
            else
                n = 0.1;
            line.Vertices[0].SetMiddlePoint(new Point(line.Vertices[0].GetX() + n, line.Vertices[0].GetY()));
            line.Vertices[1].SetMiddlePoint(new Point(line.Vertices[1].GetX() - n, line.Vertices[1].GetY()));
            return Math.Abs(line.Vertices[0].GetX() - line.Vertices[1].GetX()) <= eps;
        }
        public bool SetLineHorizontally(Line l)
        {
            List<Line> adjacent = GetAdjacentLines(l);
            if (l.Vertical || adjacent[0].Horizontal || adjacent[1].Horizontal)
                return false;
            l.Horizontal = true;
            return true;
        }

        public bool ApplyHorizontal(Line line)
        {
            double eps = 0.2;
            double n;
            if (line.Vertices[0].GetY() > line.Vertices[1].GetY())
                n = -0.1;
            else
                n = 0.1;
            line.Vertices[0].SetMiddlePoint(new Point(line.Vertices[0].GetX(), line.Vertices[0].GetY() + n));
            line.Vertices[1].SetMiddlePoint(new Point(line.Vertices[1].GetX(), line.Vertices[1].GetY() - n));
            return Math.Abs(line.Vertices[0].GetY() - line.Vertices[1].GetY()) <= eps;
        }


        public bool ApplyLength(Line line)
        {
            double n, m;
            double eps = 1;
            if (line.Vertices[0].GetY() > line.Vertices[1].GetY())
            {
                if (line.Length > line.GetLength())
                    n = 0.1;
                else
                    n = -0.1;
            }
            else
            {
                if (line.Length > line.GetLength())
                    n = -0.1;
                else
                    n = 0.1;

            }
            if (line.Vertices[0].GetX() > line.Vertices[1].GetX())
            {
                if (line.Length > line.GetLength())
                    m = 0.1;
                else
                    m = -0.1;
            }
            else
            {
                if (line.Length > line.GetLength())
                    m = -0.1;
                else
                    m = 0.1;
            }
            line.Vertices[0].SetMiddlePoint(new Point(line.Vertices[0].GetX() + m, line.Vertices[0].GetY() + n));
            line.Vertices[1].SetMiddlePoint(new Point(line.Vertices[1].GetX() - m, line.Vertices[1].GetY() - n));
            return Math.Abs(line.Length - line.GetLength()) <= eps;
        }

        public void RecalculatePolygon()
        {
            int done = 0;
            while (done != 3 * Lines.Count)
            {
                done = 0;
                foreach (Line l in Lines)
                {
                    if (l.Horizontal)
                        done = ApplyHorizontal(l) ? done + 1 : done;
                    else
                        done += 1;
                    if (l.Vertical)
                        done = ApplyVertical(l) ? done + 1 : done;
                    else
                        done += 1;
                    if (l.FixedLength)
                        done = ApplyLength(l) ? done + 1 : done;
                    else
                        done += 1;
                }
            }
        }

    }
}
