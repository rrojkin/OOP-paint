﻿using System.Windows;
using System.Windows.Controls;

namespace OOP_paint.ShapeModels
{
    public class Line : ShapeBase
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        private Point _initialPoint;

        public override void Start(Point startPoint)
        {
            _initialPoint = startPoint;
            StartPoint = startPoint;
            EndPoint = startPoint; // пока не тянем — линия нулевая
        }

        public override void Update(Point currentPoint)
        {
            EndPoint = currentPoint;
        }

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
                IsHitTestVisible = false
            };

            canvas.Children.Add(line);
        }
    }
}
