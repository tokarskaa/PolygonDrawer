using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PolygonDrawer
{
    class LineLabel
    {
        public Label Label { get; set; }
        private Line line;

        public LineLabel(Line line, String text, Canvas canvas)
        {
            this.line = line;
            Label = new Label();
            Label.Content = text;
            canvas.Children.Add(Label);
        }

        public void UpdateLabel()
        {
            if (line.HasConstraint())
            {
                Label.Visibility = System.Windows.Visibility.Visible;
                Canvas.SetLeft(Label, line.GetMiddlePoint().X);
                Canvas.SetTop(Label, line.GetMiddlePoint().Y);
            }
            else
                Label.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
