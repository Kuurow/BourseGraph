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

    public struct ViewPort
    {
        public float DateConvertie { get; set; }
    }

    public partial class Form1 : Form
    {
        public float[] tabMinMaxAbsciseDate = new float[2];
        public float[] tabMinMaxOrdonneeOuv = new float[2];
        public long nbrPoints = 0;
        public int nbrViewPort = 1;
        public int pxMarge = 35;
        public float echelleOrd = 0;
        public float echelleAbs = 0;
        public float HauteurPanel = 0;
        public string dataBinaryFilePath = @"..\..\..\data.dat";
        BinaryToForm binaryForm = new BinaryToForm();
        Graphics graphics;

        public Form1()
        {
            InitializeComponent();
        }

        /*
         * METHODE A REVOIR (FONCTIONNEMENT INTERNE) ET VOIR POUR UN CHANGEMENT DE COULEUR POUR CHAQUE MOYENNE MOBILE
         */
        public void FaireMoyenneMobile(int nbJoursMM = 20)
        {
            Brush blue = new SolidBrush(Color.Blue);  // initialisation de la couleur
            Pen bluePen = new Pen(blue, 1);           // Crée le crayon

            FileStream fsMM = File.Open(dataBinaryFilePath, FileMode.Open);
            BinaryReader brMM = new BinaryReader(fsMM); //traduit de binaire en données lisibles 

            //n = n - 1;
            for (int i = nbJoursMM; i < nbrPoints - 1; i++) // Fait nombre de ligne-1
            {
                // Initialisation des point avec l'échelle en ordonnée
                fsMM.Seek(28 * i, SeekOrigin.Begin);
                float x1px = AdapterTailleAbs(brMM.ReadSingle());
                float y1px = AdapterTailleAbs(brMM.ReadSingle() / 2);

                fsMM.Seek(28 * (i + 1), SeekOrigin.Begin);
                // Initialisation des point avec l'échelle en abscise 
                float x2px = AdapterTailleAbs(brMM.ReadSingle());
                float y2px = AdapterTailleAbs(brMM.ReadSingle() / 2);

                graphics.DrawLine(bluePen, x1px, y1px + HauteurPanel + pxMarge, x2px, y2px + HauteurPanel + pxMarge); // La marge est prise en compte et le nombre de viewport
                //DrawStringFloatFormat(x1px, y1px + (HauteurPanel * n), y1px.ToString() + " " + x1px.ToString());
            }

            fsMM.Close();
            brMM.Close();
        }

        public void InitVariables()
        {
            graphics = fondGraph.CreateGraphics();

            float[] binaryTabMinMaxDate = binaryForm.rechercherMinMaxDate();
            float[] binaryTabMinMaxOuv = binaryForm.rechercherMinMaxOuverture();
            // min en [0] et max en [1]

            tabMinMaxAbsciseDate[0] = binaryTabMinMaxDate[0];
            tabMinMaxAbsciseDate[1] = binaryTabMinMaxDate[1];

            tabMinMaxOrdonneeOuv[0] = binaryTabMinMaxOuv[0];
            tabMinMaxOrdonneeOuv[1] = binaryTabMinMaxOuv[1];

            if (nbrViewPort != 1)
            {
                HauteurPanel = (fondGraph.Height - (pxMarge * (2 + nbrViewPort - 1))) / nbrViewPort; // Défini la hauteur de chaque ViewPort lorsqu'il y en a plus d'un
            }
            else
            {
                HauteurPanel = (fondGraph.Height - (pxMarge * 2)); // Défini la hauteur de l'unique ViewPort
            }
            echelleOrd = HauteurPanel / (tabMinMaxOrdonneeOuv[1] - tabMinMaxOrdonneeOuv[0]); // calcule de l'échelle des ordonnée 2
            echelleAbs = (fondGraph.Width - (pxMarge * 2)) / (tabMinMaxAbsciseDate[1] - tabMinMaxAbsciseDate[0]); // calcule de l'échelle des ordonnée 
        }
        public float AdapterTailleOrd(float y)
        {
            // adpate les donnée à la taille du view port
            return (HauteurPanel + pxMarge) - ((y - tabMinMaxOrdonneeOuv[0]) * echelleOrd);

        }
        public float AdapterTailleAbs(float x)
        {
            // adpate les donnée à la taille du view port
            return ((x - tabMinMaxAbsciseDate[0]) * echelleAbs) + pxMarge;

        }
        public void DrawStringFloatFormat(float x, float y, String text)
        {
            // Permet d'afficher sur le panel des variables
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();

            // Draw string to screen.
            graphics.DrawString(text, drawFont, drawBrush, x, y, drawFormat);
        }
        public void DrawLineGraphCourbe(int n = 1)
        {
            Brush red = new SolidBrush(Color.Red);  // initialisation de la couleur
            Pen redPen = new Pen(red, 1);           // Crée le crayon

            FileStream fs = File.Open(dataBinaryFilePath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs); //traduit de binaire en données lisibles 

            n = n - 1;
            for (int i = 0; i < nbrPoints - 1; i++) // Fait nombre de ligne-1
            {
                // Initialisation des point avec l'échelle en ordonnée
                fs.Seek(28 * i, SeekOrigin.Begin);
                float x1px = AdapterTailleAbs(br.ReadSingle());
                float y1px = AdapterTailleOrd(br.ReadSingle());

                fs.Seek(28 * (i + 1), SeekOrigin.Begin);
                // Initialisation des point avec l'échelle en abscise 
                float x2px = AdapterTailleAbs(br.ReadSingle());
                float y2px = AdapterTailleOrd(br.ReadSingle());

                graphics.DrawLine(redPen, x1px, y1px + (HauteurPanel * n) + (pxMarge * n), x2px, y2px + (HauteurPanel * n) + (pxMarge * n)); // La marge est prise en compte et le nombre de viewport
                //DrawStringFloatFormat(x1px, y1px + (HauteurPanel * n), y1px.ToString() + " " + x1px.ToString());
            }

            fs.Close();
            br.Close();
        }

        //public void ViewPortDraw()
        //{
        //    // Initialisation des repères 
        //    Brush white = new SolidBrush(Color.White); // initialisation de la couleur
        //    Pen whitePen = new Pen(white, 1);          // Crée le crayon

        //    Brush gray = new SolidBrush(Color.Gray);

        //    if (nbrViewPort != 1)
        //    {
        //        for (int i = 1; i < nbrViewPort + 1; i++) // Fait nombre de View port
        //        {

        //            graphics.DrawLine(whitePen, pxMarge, (HauteurPanel * i) - HauteurPanel + pxMarge * i, pxMarge, (HauteurPanel * i) + (pxMarge * i));
        //            graphics.DrawLine(whitePen, pxMarge, (HauteurPanel * i) + (pxMarge * i), fondGraph.Width - pxMarge, (HauteurPanel * i) + (pxMarge * i));
        //            //DrawStringFloatFormat( 200, 290 * i, HauteurPanel.ToString() + " " + i.ToString()); // affiche la hauteur du panel et son numéro
        //            DrawLineGraphCourbe(i);
        //        }
        //    }
        //    else
        //    {
        //        graphics.DrawLine(whitePen, pxMarge, pxMarge, pxMarge, HauteurPanel + pxMarge); // Axe ordonnées
        //        graphics.DrawLine(whitePen, pxMarge, HauteurPanel + pxMarge, fondGraph.Width - pxMarge, HauteurPanel + pxMarge); // Axe abscisses
        //        DrawLineGraphCourbe();
        //    }

        //}
        
        

        private void fondGraph_Paint(object sender, PaintEventArgs e)
        {
            Brush red = new SolidBrush(Color.Red); // initialisation de la couleur
            Pen redPen = new Pen(red, 3);           // Crée le crayon

            Brush blackBrush = new SolidBrush(Color.Black);

            Brush whiteBrush = new SolidBrush(Color.White);
            Pen whitePen = new Pen(whiteBrush, 1);

            nbrPoints = binaryForm.getNbrCotations();

            InitVariables();

            graphics.FillRectangle(blackBrush, 0, 0, fondGraph.Width, fondGraph.Height);

            //ViewPortDraw();
            graphics.DrawLine(whitePen, pxMarge, pxMarge, pxMarge, HauteurPanel + pxMarge); // Axe ordonnées
            graphics.DrawLine(whitePen, pxMarge, HauteurPanel + pxMarge, fondGraph.Width - pxMarge, HauteurPanel + pxMarge); // Axe abscisses
            DrawLineGraphCourbe();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            this.MaximizeBox = false;
        }

        private void btnAddMoyenne_Click(object sender, EventArgs e)
        {
            string valeurMoyenneMobile = valueMoyenneMobile.Text;
            int valeurMoyenneMobileConvertie;

            if (Int32.TryParse(valeurMoyenneMobile, out valeurMoyenneMobileConvertie))
            {
                FaireMoyenneMobile(valeurMoyenneMobileConvertie);
            }
            else 
            {
                string message = "ERREUR : La valeur que vous avez saisi pour ajouter une moyenne mobile est incorrecte ! " +
                                 "\nVoici votre valeur : " + valeurMoyenneMobile + 
                                 "\nVérifiez également que ce soit un nombre entier.";
                string title = "Erreur saisie moyenne mobile";
                MessageBox.Show(message, title);
            }
        }
    }
}
