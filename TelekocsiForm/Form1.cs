using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TelekocsiForm
{
    public partial class FrmFo : Form
    {
        private List<Auto> autok = new List<Auto>();
        private List<Igeny> igenyek = new List<Igeny>();
        public FrmFo()
        {
            InitializeComponent();
            lbKimenet.Items.Clear();
        }

        private void btnBeolvasas_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader olvas = new StreamReader("autok.csv");
                olvas.ReadLine();
                while (!olvas.EndOfStream)
                {
                    string[] seged = olvas.ReadLine().Split(';');
                    autok.Add(new Auto(seged[0], seged[1], seged[2], seged[3], int.Parse(seged[4])));
                }
                olvas.Close();

                StreamReader olvas1 = new StreamReader("igenyek.csv");
                olvas1.ReadLine();
                while (!olvas1.EndOfStream)
                {
                    string[] seged = olvas1.ReadLine().Split(';');
                    igenyek.Add(new Igeny(seged[0], seged[1], seged[2], int.Parse(seged[3])));
                }
                olvas1.Close();

                btnSecond.Enabled = true;
                btnBeolvasas.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }

        private void btnSecond_Click(object sender, EventArgs e)
        {
            lbKimenet.Items.Add("2. Feladat");
            lbKimenet.Items.Add($"\t{autok.Count} autós hirdet fuvart");
            btnSecond.Enabled = false;
            btnThird.Enabled = true;
        }

        private void btnThird_Click(object sender, EventArgs e)
        {
            lbKimenet.Items.Clear();
            int ferohely = 0;
            foreach (var h in autok)
            {
                lbKimenet.Items.Clear();
                if (h.Indulas == "Budapest" && h.Cel == "Miskolc")
                {
                    ferohely += h.Ferohely;
                }
            }
            lbKimenet.Items.Add($"3. Feladat");
            lbKimenet.Items.Add($"\tÖsszesen {ferohely} férőhelyet hirdettek az autósok Budapestről Miskolcra");
            btnThird.Enabled = false;
            btnFourth.Enabled = true;
        }

        private void btnFourth_Click(object sender, EventArgs e)
        {
            lbKimenet.Items.Clear();
            int max = 0;
            string utv = "";
            var utvonalak = from h in autok
                            orderby h.Utvonal
                            group h by h.Utvonal into temp
                            select temp;

            foreach (var ut in utvonalak)
            {
                int fh = ut.Sum(x => x.Ferohely);
                if (max < fh)
                {
                    max = fh;
                    utv = ut.Key;
                }

            }
            lbKimenet.Items.Add("4. Feladat");
            lbKimenet.Items.Add("A legtöbb férőhelyet");
            lbKimenet.Items.Add($"({max})");
            lbKimenet.Items.Add($"a");
            lbKimenet.Items.Add($"{utv} útvonalon");
            lbKimenet.Items.Add($"ajánlották fel a hirdetők.");
            btnFourth.Enabled = false;
            btnFifth.Enabled = true;
        }

        private void btnFifth_Click(object sender, EventArgs e)
        {
            lbKimenet.Items.Clear();
            lbKimenet.Items.Add("5. Feladat");
            foreach (var ig in igenyek)
            {
                int i = ig.VanAuto(autok);

                if (i > -1)
                {
                    lbKimenet.Items.Add($"{ig.Azon} ==> {autok[i].Rendszam}");
                }
            }
            btnFifth.Enabled = false;
            btnSixth.Enabled = true;
        }

        private void btnSixth_Click(object sender, EventArgs e)
        {
            lbKimenet.Items.Clear();
            lbKimenet.Items.Add("6. Feladat (utasuzenetek.txt)");
            StreamWriter iras = new StreamWriter("utasuzenetek.txt");
            foreach (var ig in igenyek)
            {
                int i = ig.VanAuto(autok);

                if (i > -1)
                {
                    iras.WriteLine($"{ig.Azon}: Rendszám:{autok[i].Rendszam} Telefonszám:{autok[i].Telszam}");
                }
                else
                {
                    iras.WriteLine($"{ig.Azon}: Sajnos nem sikerült autót találni");
                }
                iras.WriteLine();
            }
            iras.Close();
            lbKimenet.Items.Add("Adatok fájlba írása megtörtént.");
        }

        private void btnKilepes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    }
