﻿namespace Telekocsi
{
    class Hirdetok
    {

        public string Indulas { get; private set; }
        public string Cel { get; private set; }
        public string Rendszam { get; private set; }
        public string Telszam { get; private set; }
        public int Ferohely { get; private set; }
        public string Utvonal { get; private set; }
        public Hirdetok(string indulas, string cel, string rendszam, string telszam, int ferohely)
        {
            Indulas = indulas;
            Cel = cel;
            Rendszam = rendszam;
            Telszam = telszam;
            Ferohely = ferohely;
            Utvonal = Indulas + "-" + Cel;
        }
    }
}