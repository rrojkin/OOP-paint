using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOP_paint.ShapeModels
{
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
}
