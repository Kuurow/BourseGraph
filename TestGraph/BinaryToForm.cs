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
        public string filePath;
        public long nbCotations = 0;
        public DataToBinary dataToBinary = new DataToBinary();

        public void Init()
        {
            filePath = dataToBinary.getBinaryFilePath();
            nbCotations = dataToBinary.getNbCotations();
        }

        public void rechercherMinMaxOuverture()
        {
            FileStream fs = File.Open(filePath, FileMode.Open);
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
        }
    }
}
