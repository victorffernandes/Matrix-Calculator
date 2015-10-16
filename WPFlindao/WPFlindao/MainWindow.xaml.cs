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

namespace CalcMatriz
{
    public partial class MainWindow : Window
    {
        public static PointCollection myPointCollection = new PointCollection();
        public static Canvas canvas;
        public MainWindow() {
            
            Console.WriteLine();
            InitializeComponent();
            canvas = matrixCanvas;
            DrawCartesianGrid(25, "#555555");
            Polygon  polygon = new Polygon();
            polygon.FillRule = FillRule.Nonzero;
            polygon.Fill = new SolidColorBrush(Colors.DarkOrchid);
            polygon.Points = myPointCollection;

            canvas.Children.Add(polygon);
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
            bool added = false;
            if (point.X > 0 && point.Y > 0) {
                Text(point.X, point.Y, "(" + (point.X - 125) + ":" + (point.Y - 125) + ")", Colors.Red);
                myPointCollection.Add(point);
                added = true;
            }

            if (buttonsDisplay.Children.Count < 10 && added) {
                Button b = new Button();
                b.Click += new RoutedEventHandler(this.button_Click);
                b.Content = "Change";
                b.Name = "Button" + myPointCollection.IndexOf(point).ToString();

                buttonsDisplay.Children.Add(b);

                TextBox tx = new TextBox();
                tx.Name = "X" + myPointCollection.IndexOf(point).ToString();
                tx.Text = (point.X - 125).ToString();
                tx.MaxLength = 4;
                tx.MaxLines = 1;
                xDisplay.Children.Add(tx);

                TextBox ty = new TextBox();
                ty.Name = "Y" + myPointCollection.IndexOf(point).ToString();
                ty.Text = (point.Y-125).ToString();
                ty.MaxLength = 4;
                ty.MaxLines = 1;
                yDisplay.Children.Add(ty);
            }
            return new Point(point.X, point.Y);
        }

        protected void button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            // identify which button was clicked and perform necessary actions
            int index = buttonsDisplay.Children.IndexOf((sender as Button));
            myPointCollection[index] = new Point(double.Parse((xDisplay.Children[index] as TextBox).Text) + 125,
            double.Parse((yDisplay.Children[index] as TextBox).Text) + 125);
            atualizeHUD();
        }

        public void ClearPoints() {
            myPointCollection.Clear();
            List<TextBlock> textBlocks = canvas.Children.OfType<TextBlock>().ToList();
            foreach (TextBlock t in textBlocks) {
                canvas.Children.Remove(t);
            }
            xDisplay.Children.Clear();
            yDisplay.Children.Clear();
            buttonsDisplay.Children.Clear();
            
        }

        public static void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void exceptionsMatrixTextBox(ref Label label,TextBox sender2)
        {
            string _matrixInputText = (sender2 as TextBox).Text;
            if (String.IsNullOrEmpty(_matrixInputText) || String.IsNullOrWhiteSpace(_matrixInputText)) {
                label.Content = "No matrix";
                label.Foreground = (Brush)new BrushConverter().ConvertFromString("#ededed");
            } else {
                string[] _matrixInputTextSplit = _matrixInputText.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                int count = 0;
                for (int i = 0; i < _matrixInputTextSplit.Length; i++) {
                    string[] _matrixInputSecondaryTextSplit = _matrixInputTextSplit[i].Split(' ');

                    if (i == 0) {
                        count = _matrixInputSecondaryTextSplit.Length;
                    }
                    else if (count != _matrixInputSecondaryTextSplit.Length)
                    {
                        label.Content = "Error";
                        label.Foreground = (Brush)new BrushConverter().ConvertFromString("#e74c3c");
                        break;
                    }

                    for (int j = 0; j < _matrixInputSecondaryTextSplit.Length; j++) {
                        if (String.IsNullOrWhiteSpace(_matrixInputSecondaryTextSplit[j])
                         || String.IsNullOrEmpty(_matrixInputSecondaryTextSplit[j])) {
                            label.Content = "Error";
                            label.Foreground = (Brush)new BrushConverter().ConvertFromString("#e74c3c");
                            break;
                        }
                        label.Content = "Valid";
                        label.Foreground = (Brush)new BrushConverter().ConvertFromString("#2ecc71");
                    }
                }

                if ((string)label.Content == "Valid") {
                    try {
                        Console.WriteLine(Matriz.stringToMatrix(_matrixInputText).getAllValues());
                        label.Content = "Valid";
                        label.Foreground = (Brush)new BrushConverter().ConvertFromString("#2ecc71");
                    } catch {
                        label.Content = "Error";
                        label.Foreground = (Brush)new BrushConverter().ConvertFromString("#e74c3c");
                    }
                }
            }
        }


        private void matrixInputTextChanged(object sender, TextChangedEventArgs e) {

            if ((sender as TextBox).Name == "matrixInput1") {
                exceptionsMatrixTextBox(ref validMatrix1, (sender as TextBox));
            } else {
                exceptionsMatrixTextBox(ref validMatrix2, (sender as TextBox));
            }
        }


        private void matrixOperation(object sender, RoutedEventArgs e) {
            Matriz m1 = Matriz.stringToMatrix(matrixInput1.Text);
            Matriz m2 = Matriz.stringToMatrix(matrixInput2.Text);
            bool mEQUm = (m1.rows == m2.rows) && (m1.columns == m2.columns);
            mSizeIssue.Content = "";

            if ((string)validMatrix1.Content == "Valid" && ((string)validMatrix2.Content == "Valid"
                || comboBoxOperation.Text == "Determinant"
                || comboBoxOperation.Text == "Inverse"
                || comboBoxOperation.Text == "Transpose")) {
                switch (comboBoxOperation.Text) {
                    case "Sum":
                        if (mEQUm) displayMatrix.Text = Matriz.somarMatriz(m1, m2).getAllValues();
                        else mSizeIssue.Content = "Both must be the same size to do this operation!";
                        break;

                    case "Subtraction":
                        if (mEQUm) displayMatrix.Text = Matriz.subtrairMatriz(m1, m2).getAllValues();
                        else mSizeIssue.Content = "Both must be the same size to do this operation!";
                        break;

                    case "Multiplication":
                        if (mEQUm) displayMatrix.Text = Matriz.multiply(m1, m2).getAllValues();
                        else mSizeIssue.Content = "First Matrix's collumns number must be equal to\x0Dsecond Matrix's rows number!";
                        break;

                    case "Scalar Multiplication":
                        try {
                            displayMatrix.Text = Matriz.escalar(double.Parse(matrixInput1.Text), m2, "mul").getAllValues();
                        } catch {
                            displayMatrix.Text = "Invalid scalar value!";
                        }
                        break;

                    case "Determinant":
                        displayMatrix.Text = Matriz.calculateDet(m1).ToString();
                        break;

                    case "Inverse":
                        displayMatrix.Text = Matriz.getInversa(m1).getAllValues();
                        break;

                    case "Transpose":
                        displayMatrix.Text = Matriz.stringToMatrix(matrixInput1.Text).getTransposta().getAllValues();
                        break;

                    default:
                        Console.WriteLine("Exception @ matrixOperation!");
                        break;
                }
            }
        }

        public void atualizeHUD()
        {
            try {
                List<TextBlock> textBlocks = canvas.Children.OfType<TextBlock>().ToList();
                foreach (TextBlock t in textBlocks) {
                    canvas.Children.Remove(t);
                }
                for (int i = 0; i < myPointCollection.Count; i++) {
                    (xDisplay.Children[i] as TextBox).Text = (myPointCollection[i].X-125).ToString();
                    (yDisplay.Children[i] as TextBox).Text = (myPointCollection[i].Y-125).ToString();
                    Text(myPointCollection[i].X, myPointCollection[i].Y, "(" + (myPointCollection[i].X -125) + "," + (myPointCollection[i].Y-125) + ")", Colors.Red);
                }
            } catch {
            }
        }


        private void rotation(object sender, RoutedEventArgs e)
        {
            rotacionar.Text = Regex.Replace(rotacionar.Text, "[^0-9,]+", "", RegexOptions.Compiled);
            rotacionar.Text = (String.IsNullOrEmpty(rotacionar.Text) || String.IsNullOrWhiteSpace(rotacionar.Text)) ? "0" : rotacionar.Text;
            try {
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
            } catch {
            }
        }

        private void translation(object sender, RoutedEventArgs e)
        {
            transladarX.Text = Regex.Replace(transladarX.Text, "[^0-9,-]+", "", RegexOptions.Compiled);
            transladarX.Text = (String.IsNullOrEmpty(transladarX.Text) || String.IsNullOrWhiteSpace(transladarX.Text)) ? "0" : transladarX.Text;
            transladarY.Text = Regex.Replace(transladarY.Text, "[^0-9,-]+", "", RegexOptions.Compiled);
            transladarY.Text = (String.IsNullOrEmpty(transladarY.Text) || String.IsNullOrWhiteSpace(transladarY.Text)) ? "0" : transladarY.Text;
            try {
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
            } catch {
            }
        }

        private void escaling(object sender, RoutedEventArgs e)
        {
            escalonarX.Text = Regex.Replace(escalonarX.Text, "[^0-9,]+", "", RegexOptions.Compiled);
            escalonarX.Text = (String.IsNullOrEmpty(escalonarX.Text) || String.IsNullOrWhiteSpace(escalonarX.Text)) ? "1" : escalonarX.Text;
            escalonarY.Text = Regex.Replace(escalonarY.Text, "[^0-9,]+", "", RegexOptions.Compiled);
            escalonarY.Text = (String.IsNullOrEmpty(escalonarY.Text) || String.IsNullOrWhiteSpace(escalonarY.Text)) ? "1" : escalonarY.Text;
            try {
                canvas.Children.Clear();
                DrawCartesianGrid(25, "#555555");
                Polygon _p = new Polygon();
                _p.Fill = new SolidColorBrush(Colors.DarkOrchid);
                myPointCollection = Matriz.matrizToCollection(
                                        Matriz.scale(
                                            Matriz.collectionToMatriz(myPointCollection, -125, -125),
                                            double.Parse(escalonarX.Text), double.Parse(escalonarY.Text)
                                        ), 125, 125);
                _p.Points = myPointCollection;
                canvas.Children.Add(_p);
                atualizeHUD();
            } catch {
            }
        }

        private void canvas_leftmousebutton(object sender, MouseButtonEventArgs e)
        {
            ClearPoints();
        }
        
        public void createMatrixByFormula(ref TextBox xT, ref TextBox yT, ref TextBox fT, ref TextBox display, int inp)
        {
            try{
                if (int.Parse(xT.Text) <= 10 && int.Parse(yT.Text) <= 10) {
                    display.Text = new Matriz(int.Parse(xT.Text), int.Parse(yT.Text), fT.Text).getAllValues();
                    if (inp == 1) {
                        validSize1.Content = "";
                    } else if (inp == 2) {
                        validSize2.Content = "";
                    }
                } else {
                    if (inp == 1) {
                        validSize1.Content = "Max 10x10";
                    } else if (inp == 2) {
                        validSize2.Content = "Max 10x10";
                    }
                }
            } catch {
            }

        }


        private void clickFormula1(object sender, RoutedEventArgs e)
        {
            createMatrixByFormula(ref xFormula1, ref yFormula1, ref formula1, ref matrixInput1, 1);
        }

        private void clickFormula2(object sender, RoutedEventArgs e)
        {
            createMatrixByFormula(ref xFormula2, ref yFormula2, ref formula2, ref matrixInput2, 2);
        }

    }
}
