using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Data;

namespace CalcMatriz
{
    public class Matriz {
        public int rows;
        public int columns;
        public double[,] array;
        public double det;
        public static List<double> detRegularity = new List<double>();

        public static double Eval(string expression)
        {
            DataTable t = new DataTable();
            t.Columns.Add("expression", typeof(string), expression);
            DataRow row = t.NewRow();
            t.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }


        public static Matriz stringToMatrix(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                string[] _str = str.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                Matriz _matrix = new Matriz(_str.Length, _str[0].Split(' ').Length);
                string[] _strWhitespace = _str[0].Split(' ');
                for (int i = 0; i < _str.Length; i++) {
                    for (int j = 0; j < _strWhitespace.Length; j++) {
                        _matrix.setValue(i, j, double.Parse((!(String.IsNullOrWhiteSpace(_str[i].Split(' ')[j])) ? _str[i].Split(' ')[j] : "0")));
                    }
                }
                return _matrix;
            } else {
                return new Matriz(0,0);
            }
        }

        public static Matriz scale(Matriz m, double x,double y)
        {
            Matriz _matrix = new Matriz(2, 2);
            _matrix.setValue(0, 0, x);
            _matrix.setValue(0, 1, 0);
            _matrix.setValue(1, 0, 0);
            _matrix.setValue(1, 1, y);
            return multiply(_matrix,m);
        }

        public static Matriz translate(Matriz m,double x, double y)
        {
            Matriz t = new Matriz(m.rows, m.columns);
            for(int j = 0;j<m.columns;j++)
            {
                t.setValue(0, j, x);
                t.setValue(1, j, -y);
            }
            return somarMatriz(t, m);
        }

        public static Matriz getInversa(Matriz g)
        {
            Matriz cof = new Matriz(g.rows, g.columns);
            for (int i = 0; i < g.rows; i++)
            {
                for (int j = 0; j < g.columns; j++)
                {
                    cof.setValue(i, j, Math.Pow(-1, i + j) * calculateDet(getWithoutLC(i, j, g)));
                }
            }
            Matriz adj = cof.getTransposta();

            return escalar(calculateDet(g), adj, "div");

        }

        public static Matriz rotate(Matriz m, double angle)
        {
            Matriz r = new Matriz(2, 2);
            r.setValue(0, 0, Math.Round(Math.Cos(angle * (Math.PI / 180)),2));
            r.setValue(0, 1, Math.Round(-Math.Sin(angle * (Math.PI / 180)),2));
            r.setValue(1, 0, Math.Round(Math.Sin(angle * (Math.PI / 180)),2));
            r.setValue(1, 1, Math.Round(Math.Cos(angle * (Math.PI / 180)),2));
            return multiply(r, m);

        }


        public Matriz switchRows(int i, int i2)
        {
            Matriz result = new Matriz(rows,columns);
            for (int r = 0; r < rows; r++)
            {
                for (int k = 0; k < columns; k++)
                {
                    if (r == i)
                    {
                        result.setValue(i, k, getValue(i2, k));
                    }
                    else if (r == i2)
                    {
                        result.setValue(i2, k, getValue(i, k));
                    }
                    else
                    {
                        result.setValue(r, k, getValue(r, k));
                    }
                }
            }
            return result;
        }


        public static Matriz multiplyQueue(Matriz g,double ope)
        {
            for(int i = 0;i<g.columns;i++)
            {
                g.setValue(0, i, g.getValue(0, i) * ope);
            }
            return g;
        }

        public static Matriz getWithoutLC(int i , int j,Matriz m)
        {
            Matriz r = new Matriz(m.rows - 1, m.columns - 1);
            int actualI = 0;
            for (int n = 0; n < m.rows; n++)
            {
                actualI = 0;
                for (int b = 0; b < m.columns; b++)
                {
                    if (b == j || n == i)
                    {
                        actualI++;
                        continue;
                    }

                    if (b > j)
                    {
                        if (n > i) r.setValue(n - 1, b - 1, m.getValue(n, b));
                        if (n < i) r.setValue(n, b - 1, m.getValue(n, b));
                    }
                    else if (b < j)
                    {
                        if (n > i) r.setValue(n - 1, b, m.getValue(n, b));
                        if (n < i) r.setValue(n, b, m.getValue(n, b));
                    }
                }
            }
            return r;
        }

        public static Matriz escalar(double value, Matriz r, string operation) {
            for (int i = 0; i < r.rows; i++) {
                for (int j = 0; j < r.columns; j++) {
                    // EASTER EGG :D //
                    switch (operation) {
                        case "sum":
                            r.setValue(i, j, r.array[i, j] + value);
                            break;
                        case "sub":
                            r.setValue(i, j, r.array[i, j] - value);
                            break;
                        case "mul":
                            r.setValue(i, j, r.array[i, j] * value);
                            break;
                        case "div":
                            r.setValue(i, j, r.array[i, j] / value);
                            break;
                        case "res":
                            r.setValue(i, j, r.array[i, j] % value);
                            break;
                        case "sqrt":
                            r.setValue(i, j, Math.Sqrt(r.array[i, j]));
                            break;
                        case "pow":
                            r.setValue(i, j, Math.Pow(r.array[i, j], value));
                            break;
                        case "log":
                            r.setValue(i, j, Math.Log10(r.array[i, j]));
                            break;
                        default:
                            break;
                    }
                }
            }
            return r;
        }


        public double getDiagonal(int i, int j, bool order) {
            double r = array[i, j];
            i++;
            j = order ? j + 1 : j - 1;
            for (int n = 0; n < rows - 1; n++) {
                r *= array[i, j];
                i++;
                j = order ? j + 1 : j - 1;
            }
            return r;
        } 

        public double getValue(int i, int j) {
            return array[i,j];
        }

        public Matriz getSarrusMethod() {
            Matriz r = new Matriz(this.rows, this.columns + 2);

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    r.setValue(i, j, array[i, j]);
                }
            }

            for (int i = 0; i < rows; i++) {
                for(int j = 0; j < 2; j++) {
                    r.setValue(i, j + 3, array[i, j]);
                }
            }
            return r;
        }

        public static Matriz getChioMethod(Matriz g)
        {
            for (int i = 1; i < g.rows; i++) 
            {
                for(int j = 1; j < g.columns; j++) 
                {
                    g.setValue(i, j, g.getValue(i,j)-(g.array[0, j] * g.array[i, 0]));
                }
            }
            return getWithoutLC(0,0,g);
        }



        public static double calculateDet(Matriz g) {
            double r = 0;
            if (g.rows == g.columns) {
                if (g.rows == 1){
                    return g.getValue(0, 0);
                }
                if (g.rows == 2) {
                    r = g.getDiagonal(0, 0, true) - g.getDiagonal(0, 1, false);
                    return r;
                } else if (g.rows == 3) {
                    Matriz m = g.getSarrusMethod();
                    r = m.getDiagonal(0, 0, true) + m.getDiagonal(0, 1, true) + m.getDiagonal(0, 2, true) - (m.getDiagonal(0, 2, false) + m.getDiagonal(0, 3, false) + m.getDiagonal(0, 4, false));
                    foreach (double b in detRegularity) {
                        r = r * b;
                    }
                    
                    return Math.Round(r);
                } else {
                    if(g.getValue(0,0) == 0) {  //                      Caso o primeiro elemento seja igual a zero
                        int e = 0;
                       for (int i = 1; i < g.rows; i++) {  //           Itera por toda a primeira coluna para achar um elemento diferente de zero
                           e++;
                           if(g.getValue(i,0) != 0) {  //               Caso ache algum diferente de zero
                               if (g.getValue(i, 0) == 1) {  //         Caso esse elemento seja igual a 1
                                   detRegularity.Add(-1);
                                   return calculateDet(getChioMethod(g.switchRows(0, i)));
                               } else if(g.getValue(i,0) != 1) {  //    Caso esse elemnto seja diferente de 1
                                   detRegularity.Add(g.getValue(i, 0));
                                   detRegularity.Add(-1);
                                   double factor = g.getValue(i, 0);
                                   for(int j = 0; j < g.columns;j++) {
                                       g.setValue(i, j, g.getValue(i,j) / factor);
                                   }
                                   return calculateDet(getChioMethod(g.switchRows(0, i)));
                               }
                           }
                       }
                       if (e == g.rows) {
                           return 0;
                       }
                    } else if (g.getValue(0, 0) == 1) {
                        return calculateDet(getChioMethod(g));
                    } else {
                        detRegularity.Add(g.getValue(0, 0));
                        double factor = g.getValue(0, 0);
                        for (int j = 0; j < g.columns; j++) {
                            g.setValue(0, j, g.getValue(0,j)/factor);
                        }
                        
                        return calculateDet(getChioMethod(g));
                    }
                    return 0;
                }
            } else {
                return double.NaN;
            }
        }


        public string getAllValues() {
            string r = "";
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {

                    r += (j==columns-1) ? array[i, j].ToString() : (array[i, j].ToString() + " ");
                }
                r += "\n";
            }
            return r;
        }

        public static Matriz somarMatriz(Matriz m1, Matriz m2) {
            Matriz r = new Matriz(m1.rows, m1.columns);
            if (m1.rows == m2.rows && m1.columns == m2.columns) {
                for (int i = 0; i < m1.rows; i++) {
                    for (int j = 0; j < m1.columns; j++) {
                        r.setValue(i, j, m1.array[i, j] + m2.array[i, j]);
                    }
                }
                return r;
            } else {
                return new Matriz(0, 0);
            }
        }

        public static Matriz subtrairMatriz(Matriz m1, Matriz m2) {
            Matriz r = new Matriz(m1.rows, m1.columns);
            if (m1.rows == m2.rows && m1.columns == m2.columns) {
                for (int i = 0; i < m1.rows; i++) {
                    for (int j = 0; j < m1.columns; j++) {
                        r.setValue(i, j, m1.array[i, j] - m2.array[i, j]);
                    }
                }
                return r;
            } else {
                return new Matriz(0, 0);
            }
            
        }

        public Matriz getTransposta() {
            Matriz r = new Matriz(columns, rows);
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < columns; j++) {
                    r.setValue(j, i, this.array[i, j]);
                }
            }
            return r;
        }

        public static Matriz multiply(Matriz m1, Matriz m2) {
            if (m1.columns == m2.rows) {
                Matriz r = new Matriz(m1.rows, m2.columns);

                double nome = 0;
                for (int i = 0; i < m1.rows; i++) {
                    for (int j = 0; j < m2.columns; j++) {
                        for (int n = 0; n < m1.columns; n++) {
                            nome += m1.array[i, n] * m2.array[n, j];
                        }
                        r.setValue(i, j, nome);
                        nome = 0;
                    }
                }
                return r;
            } else {
                return new Matriz(0, 0);
            }
        }

        public void setValue(int i, int j, double value) {
            array[i, j] = value;
        }

        public Matriz(int rowN, int columN,string formula)
        {
            rows = rowN;
            columns = columN;
            array = new double[rowN, columN];
            for (int i = 0; i < rowN; i++) {
                for (int j = 0; j < columN; j++) {
                    string a = formula.Replace("i", (i + 1).ToString());
                    a = a.Replace("j", (j + 1).ToString());
                    setValue(i, j, Eval(a));
                }
            }

        }


        public static Matriz collectionToMatriz(PointCollection pC,double xOffset,double yOffSet)
        {
            Matriz m = new Matriz(2, pC.Count);
            for (int i = 0; i < pC.Count; i++) {
                m.setValue(0, i, pC[i].X + xOffset);
                m.setValue(1, i, pC[i].Y + yOffSet);
            }
            return m;
        }


        public static PointCollection matrizToCollection(Matriz m, double xOffset, double yOffSet)
        {
            PointCollection p = new PointCollection();

            for (int i = 0; i < m.columns; i++) {
                Point point = new Point();
                point.X = m.getValue(0, i) + xOffset;
                point.Y = m.getValue(1, i)+yOffSet;
                p.Add(point);
            }
            return p;
        }

        public Matriz(int rowN, int columN) {
            rows = rowN;
            columns = columN;
            array = new double[rowN, columN];
        }
    }
}
