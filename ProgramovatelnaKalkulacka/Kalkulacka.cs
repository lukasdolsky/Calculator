using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramovatelnaKalkulacka
{
    class Kalkulacka
    {


        public static string vypocitej(string input) {
            // Funguje pouze pro vypocet dvou operandu (a^x)
            // V pripade vice operatoru vznika exception
            string vysledek = "";
            if (input.Contains('^'))
            {
                int index = input.IndexOf('^');
                try
                {
                    double cislo1 = Convert.ToDouble(input.Substring(0, index));
                    double cislo2 = Convert.ToDouble(input.Substring(index + 1));
                    vysledek = Convert.ToString(Math.Pow(cislo1, cislo2));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                try
                {
                    var ziskanyVysledek = new DataTable().Compute(input, null);
                    vysledek = Convert.ToString(ziskanyVysledek);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            Logging.WriteLog(input + " = " + vysledek, "C:\\temp\\Kalkulacka");
            return vysledek;
        }

        public static bool jeOperator(string text) {
            string[] operandy = new string[] { "+", "-", "^", "*", "/" };
            return operandy.Contains(text);
        }

        public static string smazatVse(string text) {
            return "0";
        }

        public static decimal ulozDoPameti(decimal cislo) {
            return cislo;
        }

        public static string ziskejZPameti(string MemoryStore, string vysledek)
        {
            if (vysledek == "0")
            {
                vysledek = MemoryStore;
            }
            else
            {
                vysledek += MemoryStore;
            }
            return vysledek;
        }

        public static string smazatJedenZnak(string text)
        {
            bool smazano = false;
            // V pripade prazdnych znaku je volana rekurze
            while (text.Length > 0)
            {
                string dalsiZnak = text.Substring(text.Length - 1);
                if (dalsiZnak != " ")
                {
                    if (smazano)
                    {
                        break;
                    }
                    smazano = true;
                }
                text = text.Substring(0, text.Length - 1);
            }
            if (text.Length == 0)
            {
                text = "0";
            }
            return text;
        }


    }
}
