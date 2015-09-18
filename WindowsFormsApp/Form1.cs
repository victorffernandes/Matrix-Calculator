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
<<<<<<< HEAD
            Matriz m = new Matriz(2, 2);
            m.setValue(0, 0, 1);
            m.setValue(0, 1, 2);
            m.setValue(1, 0, 4);
            m.setValue(1, 1, 5);

            Matriz m2 = new Matriz(5, 5);
            m2.setValue(0, 0, 1);
            m2.setValue(0, 1, 2);
            m2.setValue(0, 2, 2);
            m2.setValue(0, 3, 2);
            m2.setValue(0, 4, 1);
            m2.setValue(1, 0, 4);
            m2.setValue(1, 1, 5);
            m2.setValue(1, 2, 1);
            m2.setValue(1, 3, 1);
            m2.setValue(1, 4, 9);
            m2.setValue(2, 0, 1);
            m2.setValue(2, 1, 2);
            m2.setValue(2, 2, 2);
            m2.setValue(2, 3, 8);
            m2.setValue(2, 4, 6);
            m2.setValue(3, 0, 2);
            m2.setValue(3, 3, 4);
            m2.setValue(3, 1, 2);
            m2.setValue(3, 2, 1);
            m2.setValue(4, 0, 1);
            m2.setValue(4, 1, 8);
            m2.setValue(4, 2, 7);
            m2.setValue(4, 3, 9);
            m2.setValue(4, 4, 3);
            Console.WriteLine(m2.getAllValues());
            Console.WriteLine(Matriz.calculateDet(m2));
=======

            Matriz example = new Matriz(2, 10, "i^2 - j^2");

            Console.WriteLine(example.getAllValues());
            Console.WriteLine(Matriz.calculateDet(example));
            Console.WriteLine("Rotate"+Matriz.rotate(example,90).getAllValues());
            Console.WriteLine("Translate"+(Matriz.translate(example, 2, 2).getAllValues()));
            Console.WriteLine("Scale"+Matriz.scale(example, 2, 2).getAllValues());
            Console.WriteLine("Squared"+Matriz.multiply(example, example.getTransposta()).getAllValues());
            Console.WriteLine("Sum by itself"+Matriz.somarMatriz(example, example).getAllValues());
            Console.WriteLine("Subtrated by itself"+Matriz.subtrairMatriz(example, example).getAllValues());


>>>>>>> origin/master
        }
    }
}