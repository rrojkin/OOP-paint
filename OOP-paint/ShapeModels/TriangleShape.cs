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


//TODO: Логика построения треугольника говно. Переписать.
namespace OOP_paint.ShapeModels
{
    public class Triangle : Polyline
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
