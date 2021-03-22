using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGraph
{
    public partial class Form1 : Form
    {
        public int Echelle = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void FixerEchelle(int Echelle)
        {
            this.Echelle = Echelle;
        }

        private void fondGraph_Paint(object sender, PaintEventArgs e)
        {
            int yMin = 20;
            int yMax = fondGraph.Height - 20;
            int xMin = 20;
            int xMax = fondGraph.Width - 20;
            Graphics graphics = fondGraph.CreateGraphics();

            Brush redBrush = new SolidBrush(Color.Red);
            Pen redPen = new Pen(redBrush, 1);

            Brush blackBrush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(blackBrush, 1);

            Brush whiteBrush = new SolidBrush(Color.White);
            Pen whitePen = new Pen(whiteBrush, 1);

            graphics.FillRectangle(blackBrush, 0, 0, fondGraph.Width, fondGraph.Height);

            graphics.DrawLine(whitePen, xMin-5, yMax, xMax, yMax);
            graphics.DrawLine(whitePen, xMin, yMax+5, xMin, yMin);
           
            Point[] points =
            {
                new Point(xMin + 10, yMax - 10),
                new Point(xMin + 400, yMax - 400),
                new Point(xMin + 500, yMax - 10),
                new Point(xMin + 700, yMax - 500)
            };
            graphics.DrawLines(redPen, points);

            string hauteurMax = yMax.ToString();
            string largeurMax = xMax.ToString();

            graphics.DrawString("0", DefaultFont, whiteBrush, xMin - 15, yMax + 5);
            graphics.DrawString(hauteurMax, DefaultFont, whiteBrush, 0, yMin);
            graphics.DrawString(largeurMax, DefaultFont, whiteBrush, xMax - 20, yMax + 5);

        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            this.MaximizeBox = false;
        }
    }
}
