using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace OOP_paint.ShapeModels
{
    public abstract class ShapeBase
    {

        public Brush Stroke { get; set; } = Brushes.Black;
        public double StrokeThickness { get; set; } = 1;
        public abstract void Draw(Canvas canvas);
    }
}
