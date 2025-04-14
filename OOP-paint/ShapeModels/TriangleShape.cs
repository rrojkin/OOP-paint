
//TODO: Логика построения треугольника говно. Переписать.
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OOP_paint.ShapeModels
{
    public class Triangle : ShapeBase
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public override void Start(Point startPoint)
        {
            StartPoint = startPoint;
            EndPoint = startPoint; // Пока не двинули мышь
        }

        public override void Update(Point currentPoint)
        {
            EndPoint = currentPoint;
        }

        public override void Draw(Canvas canvas)
        {
            Point A = StartPoint;
            Point B = EndPoint;

            double midX = (A.X + B.X) / 2;
            double height = Math.Abs(B.X - A.X);

            Point C = new Point(midX, Math.Min(A.Y, B.Y) - height / 2);

            var polyline = new System.Windows.Shapes.Polyline
            {
                Points = new PointCollection { A, B, C, A }, 
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                IsHitTestVisible = false,
                Fill = Fill
            };

            canvas.Children.Add(polyline);
        }

        public override void OnClick(Point clickPoint)
        {
            Update(clickPoint);
            IsFinished = true;
            undoRedo.Push(this);
        }
    }
}

