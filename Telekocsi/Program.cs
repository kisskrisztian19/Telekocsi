using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Telekocsi
{
    class Program
    {
        static List<Hirdetok> Hirdetes = new List<Hirdetok>();
        static List<Igenylo> Igenyek = new List<Igenylo>();
        static void ElsoFeladat()
        {
            StreamReader olvas = new StreamReader("autok.csv");
            olvas.ReadLine();
            while (!olvas.EndOfStream)
            {
                string[] seged = olvas.ReadLine().Split(';');
                Hirdetes.Add(new Hirdetok(seged[0], seged[1], seged[2], seged[3], int.Parse(seged[4])));
            }
            olvas.Close();
        }
        static void MasodikFeladat()
        {
            Console.WriteLine($"2. Feladat \n\t{Hirdetes.Count} autós hirdet fuvart.");
        }
        static void HarmadikFeladat()
        {
            int ferohely = 0;
            foreach (var h in Hirdetes)
            {
                if (h.Indulas == "Budapest" && h.Cel == "Miskolc")
                {
                    ferohely += h.Ferohely;
                }
            }
            Console.WriteLine($"3. Feladat\n\tÖsszesen {ferohely} férőhelyet hirdettek az autósok Budapestről Miskolcra");
        }
        static void NegyedikFeladat()
        {
            #region Dictionary
            /* Dictionary<string, int> utvonalak = new Dictionary<string, int>();

                foreach (var h in Hirdetes)
                {
                    if (!utvonalak.ContainsKey(h.Utvonal))
                    {
                        utvonalak.Add(h.Utvonal, h.Ferohely);
                    }
                    else
                    {
                        utvonalak[h.Utvonal] = utvonalak[h.Utvonal] + h.Ferohely;
                    }
                }
                int max = 0;
                string utv = "";
                foreach (var u in utvonalak)
                {
                    if (u.Value > max)
                    {
                        max = u.Value;
                        utv = u.Key;
                    }
                }
                Console.WriteLine("4. Feladat");
                Console.WriteLine($"\t{max} - {utv}"); */
            #endregion
            int max = 0;
            string utv = "";
            var utvonalak = from h in Hirdetes
                            orderby h.Utvonal
                            group h by h.Utvonal into temp
                            select temp;

            foreach (var ut in utvonalak)
            {
                int fh = ut.Sum( x => x.Ferohely );
                if (max < fh)
                {
                    max = fh;
                    utv = ut.Key;
                }
                
            }
            Console.WriteLine("4. Feladat");
            Console.WriteLine($"\t{utv} - {max}");

        } 
        static void OtodikFeladat()
        {
            StreamReader olvas = new StreamReader("igenyek.csv");
            olvas.ReadLine();
            while (!olvas.EndOfStream)
            {
                string[] seged = olvas.ReadLine().Split(';');
                Igenyek.Add(new Igenylo(seged[0], seged[1], seged[2], int.Parse(seged[3])));
            }
            olvas.Close();
            Console.WriteLine("5. Feladat");
            foreach (var h in Hirdetes)
            {
                foreach (var i in Igenyek)
                {
                    if (h.Indulas == i.Indulas && h.Cel == i.Cel && h.Ferohely >= i.Emberek)
                    {
                        Console.WriteLine($"\t{i.Azon} ==> {h.Rendszam}");
                    }
                }
            }
        }
        static void HatodikFeladat()
        {
            Console.WriteLine("6. Feladat (utasuzenetek.txt)");
            StreamWriter iras = new StreamWriter("utasuzenetek.txt");
            foreach (var ig in Igenyek)
            {
                int i = ig.VanAuto(Hirdetes);

                if (i > -1)
                {
                    iras.WriteLine($"{ig.Azon}: Rendszám:{Hirdetes[i].Rendszam} Telefonszám:{Hirdetes[i].Telszam}");
                }
                else
                {
                    iras.WriteLine($"{ig.Azon}: Sajnos nem sikerült autót találni");
                }
                iras.WriteLine();
            }
            iras.Close();
        }
        static void Main(string[] args)
        {
            ElsoFeladat();
            MasodikFeladat();
            HarmadikFeladat();
            NegyedikFeladat();
            OtodikFeladat();
            HatodikFeladat();
            Console.ReadLine();
        }
    }
}
