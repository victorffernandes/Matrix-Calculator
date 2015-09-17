using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace WPFlindao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static PointCollection myPointCollection = new PointCollection();
        public static Canvas canvas;

        public MainWindow() {
            InitializeComponent();
            canvas = matrixCanvas;

            DrawCartesianGrid(25, "#555555");

            Polygon p = new Polygon();
            p.Fill = new SolidColorBrush(Colors.DarkOrchid);
            p.Points = myPointCollection;
            canvas.Children.Add(p);
            /*

                 1        1        1
                 +        +        +
            1 + 23 + 1 + 23 + 1 + 23 + 1
                 +        +        +
                 1        1        1
             
             
             */
        }

        public void DrawLine(int X1, int Y1, int X2, int Y2, String color, int thickness) {

            Line line = new Line();
            line.X1 = X1;
            line.X2 = X2;
            line.Y1 = Y1;
            line.Y2 = Y2;

            line.StrokeThickness = thickness;
            line.Stroke = (Brush)new BrushConverter().ConvertFromString(color);

            canvas.Children.Add(line);

        }

        public void DrawCartesianGrid(int gridsize, string color) {

            for (int i = 0; i < 250 - gridsize; i++) {
                DrawLine(gridsize * i, 0, gridsize * i, 250, color, 1);
                DrawLine(0, gridsize * i, 250, gridsize * i, color, 1);
            }

            DrawLine(0, 250 / 2, 250, 250 / 2, "#ededed", 1);
            DrawLine(250 / 2, 0, 250 / 2, 250, "#ededed", 1);
        }

        public static Point GetMousePositionWindowsForms() {
            Point point = Mouse.GetPosition(canvas);
            Console.WriteLine(point.X + " | " + point.Y);
            if(point.X > 0 && point.Y > 0) myPointCollection.Add(point);
            return new Point(point.X, point.Y);
        }

        public static void ClearPoints() {
            myPointCollection.Clear();
        }

        public static void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void matrixInputTextChanged(object sender, TextChangedEventArgs e) {
            string _matrixInputText = (sender as TextBox).Text;
            if (String.IsNullOrEmpty(_matrixInputText) || String.IsNullOrWhiteSpace(_matrixInputText)) {
                validMatrix1.Content = "No matrix";
                validMatrix1.Foreground = (Brush)new BrushConverter().ConvertFromString("#ededed");
            } else {
                string[] _matrixInputTextSplit = _matrixInputText.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                int count = 0;
                Console.WriteLine("\n");
                for (int i = 0; i < _matrixInputTextSplit.Length; i++) {
                    string[] _matrixInputSecondaryTextSplit = _matrixInputTextSplit[i].Split(' ');

                    if (i == 0) {
                        count = _matrixInputSecondaryTextSplit.Length;
                    } else if (count != _matrixInputSecondaryTextSplit.Length) {
                        validMatrix1.Content = "Err";
                        validMatrix1.Foreground = (Brush)new BrushConverter().ConvertFromString("#e74c3c");
                        break;
                    }

                    for (int j = 0; j < _matrixInputSecondaryTextSplit.Length; j++) {
                        if (String.IsNullOrWhiteSpace(_matrixInputSecondaryTextSplit[j])
                         || String.IsNullOrEmpty(_matrixInputSecondaryTextSplit[j])) {
                            validMatrix1.Content = "Err";
                            validMatrix1.Foreground = (Brush)new BrushConverter().ConvertFromString("#e74c3c");
                            break;
                        }
                        validMatrix1.Content = "Succ";
                        validMatrix1.Foreground = (Brush)new BrushConverter().ConvertFromString("#2ecc71");
                    }
                }

                if ((string)validMatrix1.Content == "Succ") {
                    Console.WriteLine(Matriz.stringToMatrix(_matrixInputText).getAllValues());
                }
            }
        }


        private void matrixOperation(object sender, RoutedEventArgs e) {
            //comboBoxOperation.
            if ((string)validMatrix1.Content == "Succ" && (string)validMatrix2.Content == "Succ") {
                switch (comboBoxOperation) {
                    case "soma": Console.WriteLine("Fds - Fim de semana");
                }
            }
        }
    }
}
