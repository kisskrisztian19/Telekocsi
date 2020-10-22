using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelekocsiForm
{
    class Igeny
    {

        public string Azon { get; private set; }
        public string Indulas { get; private set; }
        public string Cel { get; private set; }
        public int Emberek { get; private set; }
        public string Utvonal { get; private set; }
        public Igeny(string azon, string indulas, string cel, int emberek)
        {
            Azon = azon;
            Indulas = indulas;
            Cel = cel;
            Emberek = emberek;
            Utvonal = Indulas + "-" + Cel;
        }

        public int VanAuto(List<Auto> autok)
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
