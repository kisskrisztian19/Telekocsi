using System.Collections.Generic;
namespace Telekocsi
{
    class Igenylo
    {

        public string Azon { get; private set; }
        public string Indulas { get; private set; }
        public string Cel { get; private set; }
        public int Emberek { get; private set; }
        public string Utvonal { get; private set; }
        public Igenylo(string azon, string indulas, string cel, int emberek)
        {
            Azon = azon;
            Indulas = indulas;
            Cel = cel;
            Emberek = emberek;
            Utvonal = Indulas + "-" + Cel;
        }

        public int VanAuto(List<Hirdetok> autok)
        {
            int i = 0;
            while (i < autok.Count && !(Utvonal == autok[i].Utvonal && Emberek <= autok[i].Ferohely))
            {
                i++;
            }

            if (i < autok.Count)
            {
                return i;
            }
            else
            {
                return -1;
            }
        }
    }
}