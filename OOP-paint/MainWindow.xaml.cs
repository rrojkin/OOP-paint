using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
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
                case "PolygonButton":
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
                    PolygonButton.IsChecked = false;
                    return;
                case allShapes.polyline:
                    PolylineButton.IsChecked = false;
                    return;
                case allShapes.none:
                    return;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LineButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
