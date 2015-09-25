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
            
            Polygon  polygon = new Polygon();
            polygon.FillRule = FillRule.Nonzero;
            polygon.Fill = new SolidColorBrush(Colors.DarkOrchid);
            polygon.Points = myPointCollection;


            canvas.Children.Add(polygon);
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

        private void Text(double x, double y, string text, Color color)
        {

            TextBlock textBlock = new TextBlock();

            textBlock.Text = text;

            textBlock.Foreground = new SolidColorBrush(color);

            Canvas.SetLeft(textBlock, x);

            Canvas.SetTop(textBlock, y);

            canvas.Children.Add(textBlock);

        }

        public  Point GetMousePositionWindowsForms() {
            Point point = Mouse.GetPosition(canvas);
            Text(point.X, point.Y, "(" + point.X + "," + point.Y + ")", Colors.Red);
            if(point.X > 0 && point.Y > 0) myPointCollection.Add(point);

            if (buttonsDisplay.Children.Count < 10)
            {
                Button b = new Button();
                b.Content = "Change";
                b.Name = "Button" + myPointCollection.IndexOf(point).ToString();
                b.Click += delegate
                {
                    int index = buttonsDisplay.Children.IndexOf(b);
                    myPointCollection[index] = new Point(double.Parse((xDisplay.Children[index] as TextBox).Text),
                    double.Parse((yDisplay.Children[index] as TextBox).Text));
                };
                b.Click += atualizeHUD();
                buttonsDisplay.Children.Add(b);

                TextBox tx = new TextBox();
                tx.Name = "X" + myPointCollection.IndexOf(point).ToString();
                tx.Text = point.X.ToString();
                xDisplay.Children.Add(tx);

                TextBox ty = new TextBox();
                ty.Name = "Y" + myPointCollection.IndexOf(point).ToString();
                ty.Text = point.Y.ToString();
                yDisplay.Children.Add(ty);
            }



            return new Point(point.X, point.Y);
        }

        public static void ClearPoints() {
            myPointCollection.Clear();
        }

        public static void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void exceptionsMatrixTextBox(ref Label label,TextBox sender2)
        {
            string _matrixInputText = (sender2 as TextBox).Text;
            if (String.IsNullOrEmpty(_matrixInputText) || String.IsNullOrWhiteSpace(_matrixInputText))
            {
                label.Content = "No matrix";
                label.Foreground = (Brush)new BrushConverter().ConvertFromString("#ededed");
            }
            else
            {
                string[] _matrixInputTextSplit = _matrixInputText.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                int count = 0;
                for (int i = 0; i < _matrixInputTextSplit.Length; i++)
                {
                    string[] _matrixInputSecondaryTextSplit = _matrixInputTextSplit[i].Split(' ');

                    if (i == 0)
                    {
                        count = _matrixInputSecondaryTextSplit.Length;
                    }
                    else if (count != _matrixInputSecondaryTextSplit.Length)
                    {
                        label.Content = "Err";
                        label.Foreground = (Brush)new BrushConverter().ConvertFromString("#e74c3c");
                        break;
                    }

                    for (int j = 0; j < _matrixInputSecondaryTextSplit.Length; j++)
                    {
                        if (String.IsNullOrWhiteSpace(_matrixInputSecondaryTextSplit[j])
                         || String.IsNullOrEmpty(_matrixInputSecondaryTextSplit[j]))
                        {
                            label.Content = "Err";
                            label.Foreground = (Brush)new BrushConverter().ConvertFromString("#e74c3c");
                            break;
                        }
                        label.Content = "Succ";
                        label.Foreground = (Brush)new BrushConverter().ConvertFromString("#2ecc71");
                    }
                }

                if ((string)label.Content == "Succ")
                {
                    try
                    {
                        Console.WriteLine(Matriz.stringToMatrix(_matrixInputText).getAllValues());
                        label.Content = "Succ";
                        label.Foreground = (Brush)new BrushConverter().ConvertFromString("#2ecc71");
                    }
                    catch
                    {
                        label.Content = "Err";
                        label.Foreground = (Brush)new BrushConverter().ConvertFromString("#e74c3c");
                    }
                }
            }
        }


        private void matrixInputTextChanged(object sender, TextChangedEventArgs e) {

            if ((sender as TextBox).Name == "matrixInput1")
            {
                exceptionsMatrixTextBox(ref validMatrix1, (sender as TextBox));
            }
            else
            {
                exceptionsMatrixTextBox(ref validMatrix2, (sender as TextBox));
            }
        }


        private void matrixOperation(object sender, RoutedEventArgs e) {
            if ((string)validMatrix1.Content == "Succ" && ((string)validMatrix2.Content == "Succ" || comboBoxOperation.Text == "Determinante" || comboBoxOperation.Text == "Inversa" || comboBoxOperation.Text == "Transposta"))
            {
                switch (comboBoxOperation.Text)
                {
                    case "Soma":
                        displayMatrix.Text = Matriz.somarMatriz(Matriz.stringToMatrix(matrixInput1.Text), Matriz.stringToMatrix(matrixInput2.Text)).getAllValues();
                        break;
                    case "Subtração":
                        displayMatrix.Text = Matriz.subtrairMatriz(Matriz.stringToMatrix(matrixInput1.Text), Matriz.stringToMatrix(matrixInput2.Text)).getAllValues();
                        break;
                    case "Multiplicação":
                        displayMatrix.Text = Matriz.multiply(Matriz.stringToMatrix(matrixInput1.Text), Matriz.stringToMatrix(matrixInput2.Text)).getAllValues();
                        break;
                    case "Multiplicação por Escalar":
                        try {
                            displayMatrix.Text = Matriz.escalar(double.Parse(matrixInput1.Text), Matriz.stringToMatrix(matrixInput2.Text), "mul").getAllValues();
                        } catch {
                            displayMatrix.Text = "Numero escalar não válido.";
                        }
                        break;
                    case "Determinante":
                        displayMatrix.Text = Matriz.calculateDet(Matriz.stringToMatrix(matrixInput1.Text)).ToString();
                        break;
                    case "Inversa":
                        displayMatrix.Text = Matriz.getInversa(Matriz.stringToMatrix(matrixInput1.Text)).getAllValues();
                        break;
                    case "Transposta":
                        displayMatrix.Text = Matriz.stringToMatrix(matrixInput1.Text).getTransposta().getAllValues();
                        break;
                            default: Console.WriteLine("Didn´t worked");
                        break;
                }
            }
        }

        public void atualizeHUD()
        {
            foreach(object t in canvas.Children)
            {
                if(t is TextBlock)canvas.Children.Remove(t as TextBlock);
            }
            for(int i = 0;i<myPointCollection.Count;i++)
            {
                (xDisplay.Children[i] as TextBox).Text = myPointCollection[i].X.ToString();
                (yDisplay.Children[i] as TextBox).Text = myPointCollection[i].Y.ToString();
                Text(myPointCollection[i].X, myPointCollection[i].Y, "(" + myPointCollection[i].X + "," + myPointCollection[i].Y + ")", Colors.Red);
            }
        }


        private void rotation(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            DrawCartesianGrid(25, "#555555");
            Polygon _p = new Polygon();
            _p.Fill = new SolidColorBrush(Colors.DarkOrchid);
            myPointCollection = Matriz.matrizToCollection(
                                    Matriz.rotate(
                                        Matriz.collectionToMatriz(myPointCollection,-125,-125),
                                        double.Parse(rotacionar.Text)
                                    ),125,125
                                );
            _p.Points = myPointCollection;
            canvas.Children.Add(_p);
            atualizeHUD();
        }

        private void translation(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            DrawCartesianGrid(25, "#555555");
            Polygon _p = new Polygon();
            _p.Fill = new SolidColorBrush(Colors.DarkOrchid);
            myPointCollection = Matriz.matrizToCollection(
                                    Matriz.translate(
                                        Matriz.collectionToMatriz(myPointCollection, 0,0),
                                        double.Parse(transladarX.Text), double.Parse(transladarY.Text)
                                    ), 0, 0);
            _p.Points = myPointCollection;
            canvas.Children.Add(_p);
            atualizeHUD();
        }

        private void escaling(object sender, RoutedEventArgs e)
        {
            
            canvas.Children.Clear();
            DrawCartesianGrid(25, "#555555");
            Polygon _p = new Polygon();
            _p.Fill = new SolidColorBrush(Colors.DarkOrchid);
            myPointCollection = Matriz.matrizToCollection(
                                    Matriz.translate(
                                        Matriz.collectionToMatriz(myPointCollection, 0, 0),
                                        double.Parse(escalonarX.Text), double.Parse(escalonarY.Text)
                                    ), 0, 0);
            _p.Points = myPointCollection;
            canvas.Children.Add(_p);
            atualizeHUD();
        }
    }
}
