using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using OOP_paint.ShapeModels;

namespace TrapezoidPlugin
{
    // 1) Класс самой фигуры — наследник ShapeBase
    public class TrapezoidShape : ShapeBase
    {
        public Point _start, _end;

        public override void Start(Point startPoint)
        {
            _start = _end = startPoint;
        }

        public override void Update(Point currentPoint)
        {
            _end = currentPoint;
        }

        public override void Draw(Canvas canvas)
        {
            var A = _start;
            var C = _end;
            double w = Math.Abs(C.X - A.X);
            double tw = w * 0.6;    // верхнее основание = 60% нижнего
            double left = Math.Min(A.X, C.X);
            double top = Math.Min(A.Y, C.Y);

            // Вершины: p1–p4
            var p1 = new Point(left, top + (C.Y > A.Y ? w : 0));
            var p2 = new Point(left + w, p1.Y);
            var p3 = new Point(left + w - (w - tw) / 2, top);
            var p4 = new Point(left + (w - tw) / 2, top);

            var poly = new Polygon
            {
                Points = new PointCollection { p1, p2, p3, p4 },
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                Fill = Fill,
                IsHitTestVisible = false
            };
            canvas.Children.Add(poly);
        }

        public override void OnClick(Point clickPoint)
        {
            _end = clickPoint;
            IsFinished = true;
            undoRedo.Push(this);
        }
    }

    // 2) Класс-плагин — фабрика фигур
    public class TrapezoidPlugin : IShapePlugin
    {
        public string Name => "Trapezoid";

        public ShapeBase CreateShape()
            => new TrapezoidShape
            {
                Stroke = Brushes.White,
                Fill = Brushes.Transparent,
                StrokeThickness = 2
            };
    }
}
