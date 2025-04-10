using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OOP_paint.ShapeModels
{
    public class Polyline : ShapeBase
    {
        public PointCollection Points { get; set; } = new PointCollection();

        public override void Start(Point startPoint)
        {
            Points.Add(startPoint);

            Points.Add(startPoint);
        }

        public override void Update(Point currentPoint)
        {
            if (Points.Count >= 2)
            {
                Points[Points.Count - 1] = currentPoint;
            }
        }

        public void AddPoint(Point point)
        {
            if (Points.Count >= 2)
            {
                Points[Points.Count - 1] = point;

                Points.Add(point);
            }
        }

        public virtual void Finish()
        {
            if (Points.Count >= 2)
            {
                Points.RemoveAt(Points.Count - 1);
            }
        }

        public override void Draw(Canvas canvas)
        {
            var polyline = new System.Windows.Shapes.Polyline
            {
                Points = Points,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                IsHitTestVisible = false
            };
            canvas.Children.Add(polyline);
        }
    }
}
