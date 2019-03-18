using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ProgramovatelnaKalkulacka
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();

            // Nastaveni vysky na 600px pro spravne zobrazeni
            this.Height = 600;
        }


        #region Vypocet Polynomu
        // Vypocet Polynomu

        // Preddefinovane hodnoty pro snadnejsi debugging a zaroven pro jednoduche instrukce 
        private void Form_Load(object sender, EventArgs e)
        {
            tbPolynom.Text = "y = x^2 - 3x + 3";
            tbPocatek.Text = "-5";
            tbKonec.Text = "5";
            tbPocet.Text = "10";
        }

        private DataTable dtVysledek;
        private void button26_Click(object sender, EventArgs e) {
            try {
                this.Cursor = Cursors.WaitCursor;

                if (dtVysledek != null)
                {
                    dtVysledek.Clear();
                    dgBody.Refresh();
                    grafPolynom.DataBind();
                    grafPolynom.Update();
                }

                // Vypocet hodnot zavisle promenne X pro nezavisle promenne Y
                dtVysledek = Polynom.VypoctiPolynom(tbPolynom.Text.Trim(), Convert.ToDouble(tbPocatek.Text), Convert.ToDouble(tbKonec.Text), Convert.ToInt32(tbPocet.Text));
                // Vytvoreni dat pro DataGrid
                dgBody.DataSource = dtVysledek;
                dgBody.Refresh();

                // Vytvoreni dat pro graf
                grafPolynom.DataSource = dtVysledek;
                grafPolynom.DataBind();
                grafPolynom.Update();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.ToString());
            }
            string input = "Zadaný polynom: " + tbPolynom.Text.Trim() + " byl vykreslen.";
            Logging.WriteLog(input, "C:\\temp\\Kalkulacka");
        }
        #endregion

        #region Kalkulator
        private decimal MemoryStore = 0;
        public decimal vysledek = 0;

        private void btnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string vystup;
            if (Kalkulacka.jeOperator(btn.Text))
            {
                vystup = " " + btn.Text + " ";
            }
            else
            {
                vystup = btn.Text;
            }

            updateDisplay(vystup);

        }

        private void updateDisplay(string update, Boolean replace = false)
        {
            if (tbDisplay.Text == "0" || replace)
            {
                tbDisplay.Text = update;
            }
            else
            {
                tbDisplay.Text += update;
            }

        }

        private void btnVypocet(object sender, EventArgs e)
        {
            tbDisplay.Text = Kalkulacka.vypocitej(tbDisplay.Text);
        }

        private void smazatVseClick(object sender, EventArgs e)
        {
            tbDisplay.Text = Kalkulacka.smazatVse(tbDisplay.Text);
        }

        private void smazatJedenZnakClick(object sender, EventArgs e)
        {
            tbDisplay.Text = Kalkulacka.smazatJedenZnak(tbDisplay.Text);
        }

        private void odmocnitClick(object sender, EventArgs e)
        {
            tbDisplay.Text = Convert.ToString(Math.Sqrt(Convert.ToDouble(tbDisplay.Text)));
        }


        private void MSClick(object sender, EventArgs e)
        {
           MemoryStore = Kalkulacka.ulozDoPameti(Convert.ToDecimal(tbDisplay.Text));
        }

        private void MRClick(object sender, EventArgs e)
        {
            tbDisplay.Text = Kalkulacka.ziskejZPameti(Convert.ToString(MemoryStore), tbDisplay.Text);
        }

        private void MCClick(object sender, EventArgs e)
        {
            MemoryStore = 0;
        }
        #endregion

        #region Hlasovaci Prava
        private double plochaCelkem;
        private double plochaZadanychJednotek;
        private double plochaVsechJednotek;
        private XmlDocument doc = new XmlDocument();
        OpenFileDialog ofd = new OpenFileDialog();

        private void importXMLFileDialog_Click(object sender, EventArgs e)
        {
            ofd.Filter = "XML|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                doc = HlasovaciPrava.nactiXML(ofd.FileName);
                label6.Text = ofd.FileName;
                plochaCelkem = HlasovaciPrava.plochaVsechJednotek(doc);
            }
        }
        private void btnJednotky_Click(object sender, EventArgs e)
        {
            List<string> bytoveJednotky = HlasovaciPrava.nactiJednotky(tbJednotky.Text);
            plochaZadanychJednotek = HlasovaciPrava.vypoctiPlochuZadanychJednotek(bytoveJednotky,doc);
            plochaVsechJednotek = HlasovaciPrava.plochaVsechJednotek(doc);
            if (HlasovaciPrava.jeUsnasenischopna(plochaVsechJednotek, plochaZadanychJednotek))
            {
                label7.ForeColor = System.Drawing.Color.Green;
                label7.Text = string.Format("Schuze je usnasenischopna, podil zucastnenych vlastniku jednotek je: {0} %", Math.Round(plochaZadanychJednotek / plochaVsechJednotek * 100,2));
            }
            else {
                label7.ForeColor = System.Drawing.Color.Red;
                label7.Text = string.Format("Schuze neni usnasenischopna, podil zucastnenych vlastniku jednotek je: {0} %", plochaZadanychJednotek / plochaVsechJednotek * 100);
            }
        }
        
        #endregion


    }
}
