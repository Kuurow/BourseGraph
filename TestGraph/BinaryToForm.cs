using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGraph
{
    class BinaryToForm
    {
        public float ouvMin = 0;
        public float ouvMax = 0;       
        public long nbCotations = 0;
        static public string dataBinaryFilePath = @"..\..\..\data.dat";

        public void setNombreCotations()
        {
            FileStream fs = File.Open(dataBinaryFilePath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs); //traduit de binaire en données lisibles 

            nbCotations = br.BaseStream.Length / 28;

            br.Close();
            fs.Close();
        }

        public float[] rechercherMinMaxOuverture()
        {
            setNombreCotations();
            FileStream fs = File.Open(dataBinaryFilePath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs); //traduit de binaire en données lisibles 

            fs.Seek(4, SeekOrigin.Begin);
            ouvMin = br.ReadSingle();
            fs.Seek(4, SeekOrigin.Begin);
            ouvMax = br.ReadSingle();

            for (int i = 1; i < nbCotations; i++)
            {
                fs.Seek((28 * i)+4, SeekOrigin.Begin);
                float valDeRecherche = br.ReadSingle();

                if (valDeRecherche < ouvMin) { ouvMin = valDeRecherche; }
                if (valDeRecherche > ouvMax) { ouvMax = valDeRecherche; }
            }

            fs.Close();
            br.Close();

            Console.WriteLine("ouvMin = " + ouvMin + " | ouvMax = " + ouvMax);
            float[] tabMinMax = { ouvMin, ouvMax };
            return tabMinMax;
        }

        public float[] rechercherMinMaxDate()
        {
            setNombreCotations();
            FileStream fs = File.Open(dataBinaryFilePath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs); //traduit de binaire en données lisibles 

            fs.Seek(0, SeekOrigin.Begin);
            float dateMin = br.ReadSingle();
            fs.Seek(0, SeekOrigin.Begin);
            float dateMax = br.ReadSingle();

            for (int i = 1; i < nbCotations; i++)
            {
                fs.Seek(28 * i, SeekOrigin.Begin);
                float valDeRecherche = br.ReadSingle();

                if (valDeRecherche < dateMin) { dateMin = valDeRecherche; }
                if (valDeRecherche > dateMax) { dateMax = valDeRecherche; }
            }

            fs.Close();
            br.Close();

            Console.WriteLine("ouvMin = " + dateMin + " | ouvMax = " + dateMax);
            float[] tabMinMax = { dateMin, dateMax };
            return tabMinMax;
        }

        public long getNbrCotations()
        {
            setNombreCotations();
            return nbCotations;
        }
    }
}
