using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
                StrokeThickness = StrokeThickness,
            };
            line.IsHitTestVisible = false;
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
            rect.IsHitTestVisible = false;
            canvas.Children.Add(rect);
        }
    }

    public class Ellipse : ShapeBase
    {
        public System.Windows.Point TopLeft { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public override void Draw(Canvas canvas)
        {
            var ellipse = new System.Windows.Shapes.Ellipse
            {
                Width = Width,
                Height = Height,
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };
            Canvas.SetLeft(ellipse, TopLeft.X);
            Canvas.SetTop(ellipse, TopLeft.Y);
            ellipse.IsHitTestVisible = false;
            canvas.Children.Add(ellipse);
        }
    }

    public class Polygon : ShapeBase
    {
        public PointCollection Points { get; set; } = new PointCollection();

        public Polygon() {}

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
            polygon.IsHitTestVisible = false;
            canvas.Children.Add(polygon);
        }
    }

    public class Polyline : ShapeBase
    {
        public PointCollection Points { get; set; } = new PointCollection();

        public Polyline() {}

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

    public class Triangle: Polyline
    {
        public System.Windows.Point StartPoint { get; set; }
        public System.Windows.Point EndPoint { get; set; }
        public override void Draw(Canvas canvas)
        {
        

            var polyline = new System.Windows.Shapes.Polyline
            {
                

                Points = new PointCollection()
                {
                    StartPoint,
                    new Point(EndPoint.X, StartPoint.Y),
                    new Point((StartPoint.X - (StartPoint.X - EndPoint.X)/2), StartPoint.Y + (StartPoint.X - EndPoint.X)),
                    StartPoint
                },
                Stroke = Stroke,
                StrokeThickness = StrokeThickness
            };

            polyline.IsHitTestVisible = false;
            canvas.Children.Add(polyline);
        }
    }
}
