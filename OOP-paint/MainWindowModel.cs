using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace OOP_paint.Models
{
    public abstract class ShapeBase
    {
        public Brush Stroke { get; set; } = Brushes.Black;
        public double StrokeThickness { get; set; } = 1;

        public abstract void Draw(Canvas canvas);
    }

    public class Line : ShapeBase
    {
        public System.Windows.Point StartPoint { get; set; }
        public System.Windows.Point EndPoint { get; set; }

        public override void Draw(Canvas canvas)
        {
            var line = new System.Windows.Shapes.Line
            {
                X1 = StartPoint.X,
                Y1 = StartPoint.Y,
                X2 = EndPoint.X,
                Y2 = EndPoint.Y,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
            canvas.Children.Add(line);
        }
    }

    public class Rectangle : ShapeBase
    {
        public System.Windows.Point TopLeft { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

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
            canvas.Children.Add(rect);
        }
    }

    public class Ellipse : ShapeBase
    {
        public System.Windows.Point Center { get; set; }
        public double RadiusX { get; set; }
        public double RadiusY { get; set; }

        public override void Draw(Canvas canvas)
        {
            var ellipse = new System.Windows.Shapes.Ellipse
            {
                Width = RadiusX * 2,
                Height = RadiusY * 2,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
            Canvas.SetLeft(ellipse, Center.X - RadiusX);
            Canvas.SetTop(ellipse, Center.Y - RadiusY);
            canvas.Children.Add(ellipse);
        }
    }

    public class Polygon : ShapeBase
    {
        public PointCollection Points { get; set; }

        public Polygon(PointCollection points)
        {
            Points = points;
        }

        public override void Draw(Canvas canvas)
        {
            var polygon = new System.Windows.Shapes.Polygon
            {
                Points = Points,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
            canvas.Children.Add(polygon);
        }
    }

    public class Polyline : ShapeBase
    {
        public PointCollection Points { get; set; }

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
            canvas.Children.Add(polyline);
        }
    }
}
