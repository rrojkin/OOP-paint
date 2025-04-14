using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using OOP_paint;

namespace OOP_paint.ShapeModels
{
    public abstract class ShapeBase
    {

        public UndoRedoManager undoRedo;

        public Brush Stroke { get; set; } = Brushes.Black;
        public virtual Brush Fill { get; set; } = Brushes.Transparent;
        public double StrokeThickness { get; set; } = 1;

        public abstract void Start(Point startPoint);
        public abstract void Update(Point currentPoint);
        public abstract void Draw(Canvas canvas);
        public abstract void OnClick(Point clickPoint); 
        public virtual bool IsFinished { get; protected set; } = false;
    }
}
