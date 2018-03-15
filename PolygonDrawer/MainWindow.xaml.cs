using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PolygonDrawer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WriteableBitmap wbmap;
        bool IsDrawing;
        bool VertexDrag;
        bool PolygonDrag;
        List<Point> line;
        MainWindowViewModel vm;
        bool polygonFinished;
        bool polygonStarted;
        double actheight;
        double actwidth;
        public MainWindow()
        {
            InitializeComponent();
           // double width = MainGrid.ColumnDefinitions[1].ActualWidth;
            IsDrawing = false;
            VertexDrag = false;
            PolygonDrag = false;
            polygonFinished = false;
            wbmap = new WriteableBitmap(1067, 657, 300, 300, PixelFormats.Bgra32, null);
            vm = new MainWindowViewModel(wbmap.PixelWidth * wbmap.PixelHeight * wbmap.Format.BitsPerPixel / 8);
            wbmap.CopyPixels(vm.BitmapArraySource, wbmap.BackBufferStride, 0);
            Console.WriteLine("hello");
           
            MainImage.Source = wbmap;
            double height = MainImage.Height;
            double width = MainImage.Width;
            actheight = MainImage.ActualHeight;
            actwidth = MainImage.ActualWidth;
            Console.WriteLine("height: " + height + " width: " + width + " actual height: " + actheight + " actual width: " + actwidth);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Polygons.Count == 0)
                return;
            polygonFinished = false;
            vm.Polygons.RemoveAt(vm.Polygons.Count - 1);
            vm.LineLabels = new List<LineLabel>();
            polygonStarted = false;
            polygonFinished = false;
            vm.DrawPolygons(wbmap);
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            double height = MainImage.Height;
            double width = MainImage.Width;
            Point mouse = e.GetPosition(MainImage);
            actheight = MainImage.ActualHeight;
            actwidth = MainImage.ActualWidth;
            mouse.X = (mouse.X / actwidth) * 1067;
            mouse.Y = (mouse.Y / actheight) * 657;
            Console.WriteLine("height: " + height + " width: " + width + " actual height: " + actheight + " actual width: " + actwidth);
            double wbmp = wbmap.Width;
            double hbmp = wbmap.Height;
            Console.WriteLine("bitmap height: " + hbmp + "bitmap width: " + wbmp);
           
            Console.WriteLine("x: " + mouse.X + " y: " + mouse.Y);

            //   double wdiff = 1067 - actwidth;
            //   double hdiff = 657 - actheight;
            Console.WriteLine("x: " + mouse.X + " y: " + mouse.Y);
            if (!polygonFinished && polygonStarted)
            {
                Vertex v = new Vertex(wbmap.DrawBresenhamCircle((int)mouse.X, (int)mouse.Y, 6), mouse);
                if (!IsDrawing)
                {
                    vm.Start = mouse;
                    IsDrawing = true;
                }
                else
                {
                    if (vm.Polygon.IsStartPoint(mouse))
                    {
                        polygonFinished = true;
                        IsDrawing = false;
                        wbmap.WritePixels(new Int32Rect(0, 0, wbmap.PixelWidth, wbmap.PixelHeight), vm.BitmapArraySource, wbmap.BackBufferStride, 0);
                        line = wbmap.DrawBresenhamLine((int)vm.NewLine.X, (int)vm.NewLine.Y, (int)vm.Polygon.Lines[0].Vertices[0].GetX(), (int)vm.Polygon.Lines[0].Vertices[0].GetY(), Colors.Black);
                    }
                    vm.Polygon.AddLine(new Line(Vertex.GetVertexFromMiddlePoint(line[0], 6), Vertex.GetVertexFromMiddlePoint(line[1], 6)));
                }
                vm.NewLine = mouse;
                wbmap.CopyPixels(vm.BitmapArraySource, wbmap.BackBufferStride, 0);
            }
            else
            {
                if(vm.IsPointInPolygonVertices(mouse))
                {
                    vm.DraggedVertex = vm.Polygon.GetVertexFromPoint(mouse);
                    vm.Adjacent = vm.Polygon.GetAdjacentLines(mouse);
                    VertexDrag = true;
                }

                else if(vm.IsPointInPolygonLines(mouse))
                {
                    PolygonDrag = true;
                    vm.Waypoint = mouse;
                }
            }
            
        }

        private void MainBorder_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            Point mouse = e.GetPosition(MainImage);
            actheight = MainImage.ActualHeight;
            actwidth = MainImage.ActualWidth;
            mouse.X = (mouse.X / actwidth) * 1067;
            mouse.Y = (mouse.Y / actheight) * 657;
            if (mouse.X > 1067 || mouse.X <= 0 || mouse.Y > 657 || mouse.Y <= 0)
                return;
            if (!polygonFinished && polygonStarted)
            {
                if (IsDrawing)
                {
                    wbmap.WritePixels(new Int32Rect(0, 0, wbmap.PixelWidth, wbmap.PixelHeight), vm.BitmapArraySource, wbmap.BackBufferStride, 0);
                    line = wbmap.DrawBresenhamLine((int)vm.NewLine.X, (int)vm.NewLine.Y, (int)mouse.X, (int)mouse.Y, Colors.Black);
                }
            }
            if(polygonFinished)
            {
                if (vm.IsPointInPolygonVertices(mouse))
                    Cursor = Cursors.Hand;
                else if (VertexDrag && e.LeftButton == MouseButtonState.Pressed)
                {
                    Cursor = Cursors.Hand;
                    vm.Polygon.MoveVertex(vm.DraggedVertex, mouse, out vm.DraggedVertex);
                    vm.DrawPolygons(wbmap);
                }
                else if (PolygonDrag && e.LeftButton == MouseButtonState.Pressed)
                {
                    Cursor = Cursors.Hand;
                    vm.MovePolygon(vm.Waypoint, mouse, out vm.Waypoint);
                    vm.DrawPolygons(wbmap);
                }
            }
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point mouse = new Point((int)e.GetPosition(MainImage).X, (int)e.GetPosition(MainImage).Y); 
            if(VertexDrag)
            {
                VertexDrag = false;
            }
            if(PolygonDrag)
            {
                PolygonDrag = false;
            }
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mouse = e.GetPosition(MainImage);
            actheight = MainImage.ActualHeight;
            actwidth = MainImage.ActualWidth;
            mouse.X = (mouse.X / actwidth) * 1067;
            mouse.Y = (mouse.Y / actheight) * 657;
            if (vm.IsPointInPolygonVertices(mouse))
            {
                vm.ToBeDeleted = vm.Polygon.GetVertexFromPoint(mouse);
                ContextMenu cm = this.FindResource("vertexMenu") as ContextMenu;
                cm.IsOpen = true;
            }
            else if (vm.IsPointInPolygonLines(mouse))
            {
                vm.SelectedLine = vm.Polygon.GetLineFromPoint(mouse);
                ContextMenu cm;
                if (vm.SelectedLine.HasConstraint())
                    cm = FindResource("constrainedMenu") as ContextMenu;
                else
                    cm = FindResource("lineMenu") as ContextMenu;
                cm.IsOpen = true;
            }
        }

        private void DeleteVertex_Click(object sender, RoutedEventArgs e)
        {
            vm.Polygon.DeleteVertex(vm.ToBeDeleted);
            vm.DrawPolygons(wbmap);
        }

        private void AddVertex_Click(object sender, RoutedEventArgs e)
        {
            vm.Polygon.AddVertexOnLine(vm.SelectedLine);
            vm.DrawPolygons(wbmap);
        }

        private void Vertical_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Polygon.SetLineVertically(vm.SelectedLine))
            {
                vm.LineLabels.Add(new LineLabel(vm.SelectedLine, "V", LabelCanvas));
            }
            vm.DrawPolygons(wbmap);
        }

        private void Horizontal_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Polygon.SetLineHorizontally(vm.SelectedLine))
                vm.LineLabels.Add(new LineLabel(vm.SelectedLine, "H", LabelCanvas));
            vm.DrawPolygons(wbmap);
        }

        private void Length_Click(object sender, RoutedEventArgs e)
        {
            vm.LineLabels.Add(new LineLabel(vm.SelectedLine, "L", LabelCanvas));
            LengthPopup popup = new LengthPopup();
            popup.text.Text= vm.SelectedLine.GetLength().ToString();
            popup.ShowDialog();
            vm.SelectedLine.Length = popup.Length == 0 ? vm.SelectedLine.GetLength() : popup.Length;
            vm.SelectedLine.FixedLength = true;
            vm.DrawPolygons(wbmap);
        }

        private void RemoveConstraint_Click(object sender, RoutedEventArgs e)
        {
            vm.SelectedLine.Vertical = vm.SelectedLine.Horizontal = vm.SelectedLine.FixedLength = false;
            vm.DrawPolygons(wbmap);
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            polygonStarted = true;
            polygonFinished = false;
            vm.AddPolygon();
        }
    }
}   
