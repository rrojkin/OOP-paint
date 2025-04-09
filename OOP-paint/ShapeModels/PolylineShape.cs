using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace OOP_paint.ShapeModels
{
    public class Polyline : ShapeBase
    {
        public PointCollection Points { get; set; } = new PointCollection();

        public Polyline() { }

        public Polyline(PointCollection points)
        {
            Points = points;
        }

        public override void Draw(Canvas canvas)
        {
            var polyline = new System.Windows.Shapes.Polyline
            {
                Points = Points,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
            polyline.IsHitTestVisible = false;
            canvas.Children.Add(polyline);
        }
    }
}
