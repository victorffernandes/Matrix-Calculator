using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    public class Matriz {
        public int rows;
        public int columns;
        public double[,] array;
        public double det;


        public static Matriz getWithoutLC(int i , int j,Matriz m)
        {
            Matriz r = new Matriz(m.rows - 1 , m.columns - 1);
            int actualI = 0;
            for(int n = 0; n < m.rows;n++)
            {
                actualI = 0;
                for(int b = 0;b<m.columns;b++)
                {
                    if(b == j || n == i)
                    {
                        actualI++;
                        continue;
                    }
                    r.setValue(n-1, b - actualI,m.getValue(n, b));
                }
            }
            Console.WriteLine(r.getAllValues());
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


        public static double calculateDet(Matriz g) {
            double r = 0;
            if (g.rows == g.columns) {
                if (g.rows == 2) {
                    r = g.getDiagonal(0, 0, true) - g.getDiagonal(0, 1, false);
                } else if (g.rows == 3) {
                    Matriz m = g.getSarrusMethod();
                    r = m.getDiagonal(0, 0, true) + m.getDiagonal(0, 1, true) + m.getDiagonal(0, 2, true) - (m.getDiagonal(0, 2, false) + m.getDiagonal(0, 3, false) + m.getDiagonal(0, 4, false));
                } else {
                    for(int i = 0;i<g.columns;i++)
                    {
                        r += g.array[0, i] * Math.Pow(-1,i+0) * calculateDet(getWithoutLC(0, i , g));
                    }
                }
            } else {
                return double.NaN;
            }
            return r;
        }


        public string getAllValues() {
            string r = "";
            for (int i = 0; i < rows; i++) {
                r += "[ ";
                for (int j = 0; j < columns; j++) {
                    r += array[i, j].ToString() + " ";
                }
                r += "]" + "\n";
            }
            return r;
        }

        public static Matriz somarMatriz(Matriz m1, Matriz m2) {
            Matriz r = new Matriz(m1.rows, m1.columns);

            for (int i = 0; i < m1.rows; i++) {
                for (int j = 0; j < m1.columns; j++) {
                    r.setValue(i, j, m1.array[i, j] + m2.array[i, j]); 
                }
            }
            return r;
        }

        public static Matriz subtrairMatriz(Matriz m1, Matriz m2)
        {
            Matriz r = new Matriz(m1.rows, m1.columns);

            for (int i = 0; i < m1.rows; i++) {
                for (int j = 0; j < m1.columns; j++) {
                    r.setValue(i, j, m1.array[i, j] - m2.array[i, j]);
                }
            }
            return r;
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
            if (m1.columns == m2.rows)
            {

                Matriz r = new Matriz(m1.rows, m2.columns);

                double nome = 0;
                for (int i = 0; i < m1.rows; i++)
                {
                    for (int j = 0; j < m2.columns; j++)
                    {
                        for (int n = 0; n < m1.columns; n++)
                        {
                            nome += m1.array[i, n] * m2.array[n, j];
                        }
                        r.setValue(i, j, nome);
                        nome = 0;
                    }
                }
                return r;
            }
            throw (new Exception("deu Ruim"));
        }

        public void setValue(int i, int j, double value) {
            array[i, j] = value;
            //det = calculateDet();
        }

        public Matriz(int rowN, int columN) {
            rows = rowN;
            columns = columN;
            array = new double[rowN, columN];
        }
    }
}
