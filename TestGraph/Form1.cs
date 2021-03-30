using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGraph
{
    public partial class Form1 : Form
    {
        static public string dataBinaryFilePath = @"..\..\..\data.dat";
        public int Echelle = 1;
        public float[] tabMinMaxOuverture = new float[2];
        public int yMin = 20;
        public int yMax = 0;
        public int xMin = 20;
        public int xMax = 0;
        public float euroParPixel = 0;
        public float[] tabMinMaxDate = new float[2];
        public float joursParPixel = 0;
        public long nbCotations = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public void Initialiser()
        {             
            yMax = fondGraph.Height - 20;        
            xMax = fondGraph.Width - 20;

            BinaryToForm binaryToForm = new BinaryToForm();

            tabMinMaxOuverture = binaryToForm.rechercherMinMaxOuverture();

            nbCotations = binaryToForm.getNbrCotations();

            euroParPixel = yMax / tabMinMaxOuverture[1];

            //Console.WriteLine(tabMinMaxOuverture[0]);
            //Console.WriteLine(tabMinMaxOuverture[1]);

            //Console.WriteLine("europarpix " + euroParPixel);

            tabMinMaxDate = binaryToForm.rechercherMinMaxDate();

            joursParPixel = xMax / tabMinMaxDate[1];
        }

        private void FixerEchelle(int EchelleChangee)
        {
            Echelle = EchelleChangee;
        }

        private void fondGraph_Paint(object sender, PaintEventArgs e)
        {
            Initialiser();
            
            Graphics graphics = fondGraph.CreateGraphics();

            Brush redBrush = new SolidBrush(Color.Red);
            Pen redPen = new Pen(redBrush, 1);

            Brush blackBrush = new SolidBrush(Color.Black);           

            Brush whiteBrush = new SolidBrush(Color.White);
            Pen whitePen = new Pen(whiteBrush, 1);

            graphics.FillRectangle(blackBrush, 0, 0, fondGraph.Width, fondGraph.Height);

            graphics.DrawLine(whitePen, xMin-5, yMax, xMax, yMax);
            graphics.DrawLine(whitePen, xMin, yMax+5, xMin, yMin);                       

            string hauteurMax = yMax.ToString();
            string largeurMax = xMax.ToString();

            graphics.DrawString("0", DefaultFont, whiteBrush, xMin - 15, yMax + 5);
            graphics.DrawString(tabMinMaxOuverture[1].ToString(), DefaultFont, whiteBrush, 0, yMin - 15);
            graphics.DrawString(tabMinMaxDate[1].ToString(), DefaultFont, whiteBrush, xMax - 20, yMax + 5);


            FileStream fs = File.Open(dataBinaryFilePath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs); //traduit de binaire en données lisibles 



            for (int i = 0; i < nbCotations-1; i++)
            {
                fs.Seek((28 * i), SeekOrigin.Begin); 
                float dateActuelle = br.ReadSingle(); // date soit coordonnée X
                float ouvertureActuelle = br.ReadSingle(); // ouverture soit coordonnée Y

                fs.Seek((28 * (i + 1)), SeekOrigin.Begin);
                float dateSuivante = br.ReadSingle(); // date soit coordonnée X
                float ouvertureSuivante = br.ReadSingle(); // ouverture soit coordonnée Y

                Point pointActuel = new Point((int)(joursParPixel * dateActuelle - xMax + xMin), (int)(euroParPixel * ouvertureActuelle));
                Point pointSuivant = new Point((int)(joursParPixel * dateSuivante - xMax + xMin), (int)(euroParPixel * ouvertureSuivante));
                graphics.DrawLine(redPen, pointActuel, pointSuivant);

                Console.WriteLine("actu : " + dateActuelle + " " + ouvertureActuelle);
                Console.WriteLine("suiv : " + dateSuivante + " " + ouvertureSuivante);
            }

            Console.WriteLine(nbCotations);

            fs.Close();
            br.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            this.MaximizeBox = false;
        }
    }
}
