using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {




        public Form1()
        {
            InitializeComponent();

            Matriz example = new Matriz(2, 10, "i^2 - j^2");

            Console.WriteLine(example.getAllValues());
            Console.WriteLine(Matriz.calculateDet(example));
            Console.WriteLine("Rotate"+Matriz.rotate(example,90).getAllValues());
            Console.WriteLine("Translate"+(Matriz.translate(example, 2, 2).getAllValues()));
            Console.WriteLine("Scale"+Matriz.scale(example, 2, 2).getAllValues());
            Console.WriteLine("Squared"+Matriz.multiply(example, example.getTransposta()).getAllValues());
            Console.WriteLine("Sum by itself"+Matriz.somarMatriz(example, example).getAllValues());
            Console.WriteLine("Subtrated by itself"+Matriz.subtrairMatriz(example, example).getAllValues());


        }
    }
}