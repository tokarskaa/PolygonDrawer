using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PolygonDrawer
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public byte[] BitmapArraySource;
        int ArrayLenght;
        public List<Polygon> Polygons { get; set; }
        public Polygon Polygon { get; set; }
        public List<LineLabel> LineLabels { get; set; }
        public Point Start { get; set; }
        public Point NewLine { get; set; }
        public Vertex DraggedVertex;
        public Vertex ToBeDeleted { get; set; }
        public Line SelectedLine { get; set; }
        public Point Waypoint;
        public List<Point> Line { get;set; }
        public List<Line> Adjacent { get; set; }
        public MainWindowViewModel(int arrayLength)
        {
            BitmapArraySource = new byte[arrayLength];
            Polygon = new Polygon();
            ArrayLenght = arrayLength;
            LineLabels = new List<LineLabel>();
            Polygons = new List<Polygon>();
        }
        

        public void DrawPolygon(WriteableBitmap wbm, Polygon polygon)
        {

            polygon.RecalculatePolygon();
            foreach (var label in LineLabels)
                label.UpdateLabel();
            foreach(Line l in polygon.Lines)
            {
                wbm.DrawBresenhamLine((int)l.Vertices[0].GetX(), (int)l.Vertices[0].GetY(), (int)l.Vertices[1].GetX(), (int)l.Vertices[1].GetY(), Colors.Black);
                wbm.DrawBresenhamCircle((int)l.Vertices[0].GetX(), (int)l.Vertices[0].GetY(), 6);
            }
        }

        public void DrawPolygons(WriteableBitmap wbm)
        {
            byte[] array = new byte[ArrayLenght];
            wbm.WritePixels(new Int32Rect(0, 0, wbm.PixelWidth, wbm.PixelHeight), array, wbm.BackBufferStride, 0);
            foreach (var p in Polygons)
            {
                DrawPolygon(wbm, p);
            }
                
        }

        public void MovePolygon(Point waypoint, Point point, out Point newWaypoint)
        {
            double XDifference = point.X - waypoint.X;
            double YDifference = point.Y - waypoint.Y;
            newWaypoint = point;
            Polygon.MovePolygon(new Point(XDifference, YDifference));
        }

        public void AddPolygon()
        {
            Polygon p = new Polygon();
            Polygons.Add(p);
            Polygon = p;
        }

        public bool IsPointInPolygonVertices(Point point)
        {
            foreach(var p in Polygons)
            {
                if(p.IsPointInVertices(point))
                {
                    Polygon = p;
                    return true;
                }
            }
            return false;
        }

        public bool IsPointInPolygonLines(Point point)
        {
            foreach (var p in Polygons)
            {
                if (p.IsPointInLines(point))
                {
                    Polygon = p;
                    return true;
                }
            }
            return false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
