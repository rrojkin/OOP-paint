
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

        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }

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
            // A и B — основание (горизонтально)
            Point A = StartPoint;
            Point B = EndPoint;

            // C — вершина равнобедренного треугольника
            double midX = (A.X + B.X) / 2;
            double height = Math.Abs(B.X - A.X); // можно заменить на кастомную высоту

            // Вершина — выше (меньше по Y)
            Point C = new Point(midX, Math.Min(A.Y, B.Y) - height / 2);

            var polyline = new System.Windows.Shapes.Polyline
            {
                Points = new PointCollection { A, B, C, A }, // замкнутая фигура
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                IsHitTestVisible = false
            };

            canvas.Children.Add(polyline);
        }
    }
}

