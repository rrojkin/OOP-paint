using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace OOP_paint.ShapeModels
{
    public class Polygon: Polyline
    {

        public override void Finish()
        {
            if (Points.Count >= 2)
            {
                Points.RemoveAt(Points.Count - 1);
            }

            if (Points.Count >= 3 && Points[0] != Points[^1])
            {
                Points.Add(Points[0]);
            }

            IsFinished = true;
            undoRedo.Push(this);
        }


        public override void Draw(Canvas canvas)
        {
            var polygon = new System.Windows.Shapes.Polygon
            {
                Points = Points,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                Fill = Fill,
                IsHitTestVisible = false
            };

            canvas.Children.Add(polygon);
        }
    }
}
