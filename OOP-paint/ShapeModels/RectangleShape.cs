using System.Windows.Controls;
using System.Windows;

namespace OOP_paint.ShapeModels
{
    public class Rectangle : ShapeBase
    {
        public System.Windows.Point TopLeft { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        private Point _startPoint;

        public override void Draw(Canvas canvas)
        {
            var rect = new System.Windows.Shapes.Rectangle
            {
                Width = Width,
                Height = Height,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };

            Canvas.SetLeft(rect, TopLeft.X);
            Canvas.SetTop(rect, TopLeft.Y);
            rect.IsHitTestVisible = false;
            canvas.Children.Add(rect);
        }

        public override void Start(Point startPoint)
        {
            _startPoint = startPoint;
           
            TopLeft = startPoint;
            Width = 0;
            Height = 0;
        }

        public override void Update(Point currentPoint)
        {
            Width = Math.Abs(currentPoint.X - _startPoint.X);
            Height = Math.Abs(currentPoint.Y - _startPoint.Y);
            TopLeft = new Point(Math.Min(currentPoint.X, _startPoint.X), Math.Min(currentPoint.Y, _startPoint.Y));
        }
    }
}
