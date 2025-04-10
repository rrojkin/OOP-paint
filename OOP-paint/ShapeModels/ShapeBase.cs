using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace OOP_paint.ShapeModels
{
    public abstract class ShapeBase
    {

        public Brush Stroke { get; set; } = Brushes.Black;
        public double StrokeThickness { get; set; } = 1;

        public abstract void Start(Point startPoint);
        public abstract void Update(Point currentPoint);
        public abstract void Draw(Canvas canvas);


    }
}
