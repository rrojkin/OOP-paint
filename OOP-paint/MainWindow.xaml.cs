using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using OOP_paint.ShapeModels;

namespace OOP_paint
{
   
    public partial class MainWindow : Window
    {

        private Dictionary<string, Func<ShapeBase>> shapeFactory;

        private double weight = 1.0;
        private Brush strokeBrush = Brushes.White;
        private Brush fillBrush = Brushes.Transparent;


        private ShapeBase currentShape;
        private Point startPoint;
        private bool isDrawing = false;
        private List<ShapeBase> shapes = new List<ShapeBase>();
        private ToggleButton selectedButton = null;
        private bool isBrushSelected = true;

        private UndoRedoManager UndoRedoManager = new UndoRedoManager();
        private FileManager fileManager = new FileManager();

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
            if (selectedButton == null) return;

            Point currentPoint = e.GetPosition(MainCanvas);

            if (currentShape == null)
            {
                currentShape = shapeFactory[selectedButton.Name]();
                currentShape.Fill = fillBrush;
                currentShape.StrokeThickness = weight;
                currentShape.Stroke = strokeBrush;
                currentShape.Start(currentPoint);
                currentShape.undoRedo = UndoRedoManager;
                isDrawing = true;
            }
            else
            {
                currentShape.OnClick(currentPoint);

                if (currentShape.IsFinished)
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
                { "PolylineButton", () => new ShapeModels.Polyline { Stroke = Brushes.White, StrokeThickness = 2 } },
                { "PolygonButton", () => new ShapeModels.Polygon { Stroke = Brushes.White, Fill = Brushes.Transparent, StrokeThickness = 2 } }
            };

        }

        private void WeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = e.NewValue;
            WeightTextbox.Text = ((int)value).ToString();
            weight = value;
        }

        private void WeightTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string inputChar = e.Text;
        }

        private void mycolorButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (isBrushSelected)
            {
                strokeBrush = button.Background;
                BrushColorButton.Background = strokeBrush;
            }
            else
            {
                fillBrush = button.Background;
                FillColorButton.Background = fillBrush;
            }
        }

        private void BrushColorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isBrushSelected) {
                isBrushSelected = true;
                FillColorButton.IsChecked = false;
            }
        }

        private void FillButtonColor_Click(object sender, RoutedEventArgs e)
        {
            if (isBrushSelected) {
                isBrushSelected = false;
                BrushColorButton.IsChecked = false;
            }
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            UndoRedoManager.Undo(shapes);
            RedrawCanvas();
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            UndoRedoManager.Redo(shapes);
            RedrawCanvas();
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            fileManager.OpenFile(ref shapes);
            RedrawCanvas();
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            fileManager.SaveFile(shapes);
            RedrawCanvas();
        }
    }
}
