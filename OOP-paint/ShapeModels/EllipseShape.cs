using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOP_paint.ShapeModels
{
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
}
