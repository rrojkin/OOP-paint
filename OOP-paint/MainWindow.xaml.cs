using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using OOP_paint.Models;

namespace OOP_paint
{
   
    public partial class MainWindow : Window
    {
        private enum allShapes
        {
            line,
            rect,
            ellipse,
            polygon,
            polyline,
            none
        }

        private allShapes selectedShape = allShapes.none;
        private ShapeBase currentShape;
        private Point startPoint;
        private bool isDrawing = false;
        private List<ShapeBase> shapes = new List<ShapeBase>();


        public void shapeButtonClick(object sender, RoutedEventArgs e)
        {
            deselectButton(selectedShape);
            ToggleButton button= sender as ToggleButton;
            if (button == null) return;

            switch (button.Name)
            {
                case "LineButton":        
                    selectedShape = allShapes.line;
                    break;
                case "RectangleButton": 
                    selectedShape = allShapes.rect;
                    break;
                case "EllipseButton":
                    selectedShape = allShapes.ellipse;
                    break;
                case "TriangleButton":
                    selectedShape = allShapes.polygon;
                    break;    
                case "PolylineButton":
                    selectedShape = allShapes.polyline;
                    break;
                default: return;
            }

            button.IsChecked = true;
        }

        private void deselectButton(allShapes shape)
        {
            switch (shape)
            {
                case allShapes.line:
                    LineButton.IsChecked = false;
                    return; 
                case allShapes.rect:
                    RectangleButton.IsChecked = false;
                    return;
                case allShapes.ellipse:
                    EllipseButton.IsChecked = false;
                    return;        
                case allShapes.polygon:
                    TriangleButton.IsChecked = false;
                    return;
                case allShapes.polyline:
                    PolylineButton.IsChecked = false;
                    return;
                case allShapes.none:
                    return;
            }
        }

        private void mainCanvas_RightMouseDown(object sender, MouseEventArgs e)
        {
            if (selectedShape != allShapes.polyline) return;

            if (isDrawing && currentShape is Models.Polyline)
            {
                shapes.Add(currentShape);
                isDrawing = false;
                currentShape = null;
            }
        } 

        private void mainCanvas_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Это сообщение для дебаггера.");

            if (selectedShape == allShapes.none) return;

            Point currentPoint = e.GetPosition(MainCanvas);

            if (currentShape == null)
            {
                startPoint = currentPoint;
                isDrawing = true;

                switch (selectedShape)
                {
                    case allShapes.line:
                        currentShape = new Models.Line()
                        {
                            StartPoint = startPoint,
                            EndPoint = startPoint,
                            Stroke = Brushes.White,
                            StrokeThickness = 2
                        };
                        break;

                    case allShapes.rect:
                        currentShape = new Models.Rectangle
                        {
                            TopLeft = startPoint,
                            Width = 0,
                            Height = 0,
                            Stroke = Brushes.White,
                            StrokeThickness = 2,
                        };
                        break;

                    case allShapes.ellipse:
                        currentShape = new Models.Ellipse
                        {
                            TopLeft = startPoint,
                            Width = 0,
                            Height = 0,
                            Stroke = Brushes.White,
                            StrokeThickness = 2,
                        };
                        break;

                    case allShapes.polygon:
                        currentShape = new Models.Triangle()
                        {
                            Stroke = Brushes.White,
                            StrokeThickness = 2,
                            StartPoint = currentPoint,
                            EndPoint = currentPoint
                        };

                        Debug.WriteLine("Done");
                        break;

                        currentShape = new Models.Polygon(new PointCollection { startPoint })
                        {
                            Stroke = Brushes.White,
                            StrokeThickness = 2
                        };
                        break;

                    case allShapes.polyline:
                        currentShape = new Models.Polyline(new PointCollection { startPoint })
                        {
                            Stroke = Brushes.White,
                            StrokeThickness = 2
                        };
                        break;
                }
            }
            else
            {
                // Второй клик, конец рисования
                if (selectedShape == allShapes.polygon)
                {
                    if (currentShape is Models.Triangle triangle)
                    {
                        
                    }
                }
                else
                {
                    if (currentShape is Models.Line line)
                    {
                        line.EndPoint = currentPoint;
                    }
                    else if (currentShape is Models.Rectangle rect)
                    {
                        rect.Width = Math.Abs(currentPoint.X - startPoint.X);
                        rect.Height = Math.Abs(currentPoint.Y - startPoint.Y);
                        rect.TopLeft = new Point(Math.Min(startPoint.X, currentPoint.X), Math.Min(startPoint.Y, currentPoint.Y));
                    }
                    else if (currentShape is Models.Ellipse ellipse)
                    {
                        ellipse.Width = Math.Abs(currentPoint.X - startPoint.X);
                        ellipse.Height = Math.Abs(currentPoint.Y - startPoint.Y);
                        ellipse.TopLeft = new Point(Math.Min(startPoint.X, currentPoint.X), Math.Min(startPoint.Y, currentPoint.Y));
                    }
                    else if (currentShape is Models.Polyline polyline)
                    {
                        polyline.Points.Add(currentPoint);
                    }
                }

                if (currentShape is not Models.Polyline || currentShape is Models.Triangle)
                {
                    shapes.Add(currentShape);
                    currentShape = null;
                    isDrawing = false;
                }
            }

            RedrawCanvas();
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing || currentShape == null) return;

            Point endPoint = e.GetPosition(MainCanvas);

            switch (currentShape)
            {
                case Models.Line line:
                    line.EndPoint = endPoint;
                    break;

                case Models.Rectangle rect:
                    rect.Width = Math.Abs(endPoint.X - startPoint.X);
                    rect.Height = Math.Abs(endPoint.Y - startPoint.Y);
                    rect.TopLeft = new Point(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y));
                    break;

                case Models.Ellipse ellipse:
                    ellipse.Width = Math.Abs(endPoint.X - startPoint.X);
                    ellipse.Height = Math.Abs(endPoint.Y - startPoint.Y);
                    ellipse.TopLeft = new Point(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y));
                    break;

                case Models.Triangle triangle:
                    triangle.EndPoint = endPoint;
                    
                    break;

                    //if (polygon.Points.Count > 1)
                    //{
                    //    polygon.Points[polygon.Points.Count - 1] = endPoint; // Adjust last point while drawing
                    //}

                case Models.Polyline polyline:
                    if (polyline.Points.Count > 1)
                    {
                        
                        polyline.Points[polyline.Points.Count - 1] = endPoint; 
                    } else
                    {
                        polyline.Points.Add(endPoint);
                    }
                    break;
            }

            // Перерисовываем холст
            RedrawCanvas();
        }

        private void RedrawCanvas()
        {
            MainCanvas.Children.Clear(); 

            foreach (var shape in shapes)
            {
                shape.Draw(MainCanvas);
            }

            if (currentShape != null)
            {
                currentShape.Draw(MainCanvas);
            }
        }


        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
