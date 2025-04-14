using System.Windows;
using System.Windows.Controls;

namespace OOP_paint.ShapeModels
{
    public class Ellipse : ShapeBase
    {
        public System.Windows.Point TopLeft { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public Point _startPoint;

        public override void Draw(Canvas canvas)
        {
            var ellipse = new System.Windows.Shapes.Ellipse
            {
                Width = Width,
                Height = Height,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                Fill = Fill
            };
            Canvas.SetLeft(ellipse, TopLeft.X);
            Canvas.SetTop(ellipse, TopLeft.Y);
            ellipse.IsHitTestVisible = false;
            canvas.Children.Add(ellipse);
        }

        public override void Start(Point startPoint)
        {
            TopLeft = startPoint;
            _startPoint = startPoint;
            Height = 0;
            Width = 0;
        }

        public override void Update(Point currentPoint)
        {
            Width = Math.Abs(currentPoint.X - _startPoint.X);
            Height = Math.Abs(currentPoint.Y - _startPoint.Y);
            TopLeft = new Point(Math.Min(currentPoint.X, _startPoint.X), Math.Min(currentPoint.Y, _startPoint.Y));
        }

        public override void OnClick(Point clickPoint)
        {
            Update(clickPoint);
            IsFinished = true; // линия завершается после одного клика
        }

    }
}
