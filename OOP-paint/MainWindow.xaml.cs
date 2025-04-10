using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using OOP_paint.ShapeModels;

namespace OOP_paint
{
   
    public partial class MainWindow : Window
    {

        private Dictionary<string, Func<ShapeBase>> shapeFactory;


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
        private ToggleButton selectedButton = null;


        public void shapeButtonClick(object sender, RoutedEventArgs e)
        {
            if (selectedButton != null)
            {
                selectedButton.IsChecked = false;
            }

            selectedButton = sender as ToggleButton;
            selectedButton.IsChecked = true;

            return;            
        }

        private void mainCanvas_RightMouseDown(object sender, MouseEventArgs e)
        {
            if (isDrawing && currentShape is ShapeModels.Polyline)
            {
                (currentShape as ShapeModels.Polyline)?.Finish();
                shapes.Add(currentShape);
                currentShape = null;
                isDrawing = false;
            }
        } 

        private void mainCanvas_LeftMouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Это сообщение для дебаггера.");

            if (selectedButton == null) return;


            Point currentPoint = e.GetPosition(MainCanvas);

            if (currentShape is ShapeModels.Polyline polyline)
            {
                (currentShape as ShapeModels.Polyline)?.AddPoint(currentPoint);
                RedrawCanvas();
                return;
            }

            if (currentShape == null && !isDrawing)
            {
                currentShape = shapeFactory[selectedButton.Name]();
                isDrawing = true;
                currentShape.Start(currentPoint);
            }
            else
            {
                isDrawing = false;
                currentShape.Update(currentPoint);
                shapes.Add(currentShape);
                currentShape = null;
            }

            RedrawCanvas();

            return;
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing || currentShape == null) return;

            Point endPoint = e.GetPosition(MainCanvas);

            currentShape.Update(endPoint);
            RedrawCanvas();

            return;
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

            shapeFactory = new Dictionary<string, Func<ShapeBase>>
            {
                { "RectangleButton", () => new ShapeModels.Rectangle { Stroke = Brushes.White, StrokeThickness = 2 } },
                { "EllipseButton", () => new ShapeModels.Ellipse { Stroke = Brushes.White, StrokeThickness = 2 } },
                { "LineButton", () => new ShapeModels.Line { Stroke = Brushes.White, StrokeThickness = 2 } },
                { "TriangleButton", () => new ShapeModels.Triangle { Stroke = Brushes.White, StrokeThickness = 2 } },
                { "PolylineButton", () => new ShapeModels.Polyline { Stroke = Brushes.White, StrokeThickness = 2 } }
            };

        }
    }
}
