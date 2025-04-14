using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace OOP_paint.ShapeModels
{
    public abstract class ShapeBase
    {

        public Brush Stroke { get; set; } = Brushes.Black;
        public virtual Brush Fill { get; set; } = Brushes.Transparent;
        public double StrokeThickness { get; set; } = 1;

        public abstract void Start(Point startPoint);
        public abstract void Update(Point currentPoint);
        public abstract void Draw(Canvas canvas);
        public abstract void OnClick(Point clickPoint); // вот это ты теперь будешь вызывать
        public virtual bool IsFinished { get; protected set; } = false; // по умолчанию false
    }
}
