namespace Telekocsi
{
    class Igenylo
    {

        public string Azon { get; private set; }
        public string Indulas { get; private set; }
        public string Cel { get; private set; }
        public int Emberek { get; private set; }
        public Igenylo(string azon, string indulas, string cel, int emberek)
        {
            Azon = azon;
            Indulas = indulas;
            Cel = cel;
            Emberek = emberek;
        }
    }
}