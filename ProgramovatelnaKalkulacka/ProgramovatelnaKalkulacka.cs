using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramovatelnaKalkulacka
{
    class ProgramovatelnaKalkulacka
    {
        private static string polynomString;
        private static double memory1;
        private static double memory2;
        private static double memory3;
        private static double memory4;
        private static double memory5;
        private static double memory6;
        private static double memory7;
        private static double memory8;
        private static double memory9;
        private static double memory0;
        public static void nahravejMakro() {
        
        }
        

        public static void ziskejPolynom(string polynom)
        {
            polynomString = polynom;
        }

        public static string smazatVse()
        {
            return "";
        }

        public static string zadejVstup(Button btn)
        {
            string vystup;

            switch (btn.Text)
            {
                case "MS(x)":
                    vystup = " " + "MS(";
                    break;
                case "MR(x)":
                    vystup = " " + "MR(";
                    break;
                case "Enter":
                    vystup = Environment.NewLine;
                    break;
                case "IFP(x)":
                    vystup = " " + "IFP(";
                    break;
                case "IFN(x)":
                    vystup = " " + "IFN(";
                    break;
                case "IFZ(x)":
                    vystup = " " + "IFZ(";
                    break;
                default:
                    if (Kalkulacka.jeOperator(btn.Text))
                    {
                        vystup = " " + btn.Text + " ";
                    }
                    else
                    {
                        vystup = btn.Text;
                    }
                    break;
            }
            return vystup;
        }

        public static void provedOperace(TextBox textBox) {
            int cisloPameti;
            double argument;
            foreach (string line in textBox.Lines)
            {
                if (line.Contains("MS")) {
                    line.Replace(" ", "");
                    cisloPameti = Convert.ToInt32(line.Substring(line.IndexOf("(")+1,1));
                    argument = Convert.ToDouble(line.Substring(0,line.IndexOf("MS")));
                    ulozDoPameti(cisloPameti, argument);
                }
                
            }
        }

        public static void ulozDoPameti(Int32 cisloPameti, double argument) {
            switch (cisloPameti) {
                case 0:
                    memory0 = argument;
                    break;
                case 1:
                    memory1 = argument;
                    break;
                case 2:
                    memory2 = argument;
                    break;
                case 3:
                    memory3 = argument;
                    break;
                case 4:
                    memory4 = argument;
                    break;
                case 5:
                    memory5 = argument;
                    break;
                case 6:
                    memory6 = argument;
                    break;
                case 7:
                    memory7 = argument;
                    break;
                case 8:
                    memory8 = argument;
                    break;
                case 9:
                    memory9 = argument;
                    break;
                default:
                    break;
            }
        }

        public static double zavolejPamet(Int32 cisloPameti)
        {
            double hodnotaPameti;
            switch (cisloPameti)
            {
                case 0:
                    hodnotaPameti = memory0;
                    break;
                case 1:
                    hodnotaPameti = memory1;
                    break;
                case 2:
                    hodnotaPameti = memory2;
                    break;
                case 3:
                    hodnotaPameti = memory3;
                    break;
                case 4:
                    hodnotaPameti = memory4;
                    break;
                case 5:
                    hodnotaPameti = memory5;
                    break;
                case 6:
                    hodnotaPameti = memory6;
                    break;
                case 7:
                    hodnotaPameti = memory7;
                    break;
                case 8:
                    hodnotaPameti = memory8;
                    break;
                case 9:
                    hodnotaPameti = memory9;
                    break;
                default:
                    hodnotaPameti = 0;
                    break;

            }
            return hodnotaPameti;
        }

        public static double vypocitejPolynom() {

            return 0;
        }

        public static string provedMakro(string makro){
            string priklad = makro;
            string vysledek = "";
            int xIndex;
            double x;
            if (priklad.Contains("F1")) {
                string polynom = polynomString;
                xIndex = Convert.ToInt32(priklad.Substring(priklad.IndexOf("MR(")+3,1));
                x = zavolejPamet(xIndex);
                Polynom.VypoctiPolynom(polynom, 50, 50, 0);
            }
            priklad = priklad.Replace("\n", String.Empty);
            priklad = priklad.Replace("\r", String.Empty);
            priklad = priklad.Replace("\t", String.Empty);
            priklad = priklad.Replace(" ", "");
            priklad = priklad.Replace("MR(1)", Convert.ToString(zavolejPamet(1)));
            priklad = priklad.Replace("MR(2)", Convert.ToString(zavolejPamet(2)));
            priklad = priklad.Replace("MR(3)", Convert.ToString(zavolejPamet(3)));
            priklad = priklad.Replace("MR(4)", Convert.ToString(zavolejPamet(4)));
            priklad = priklad.Replace("MR(5)", Convert.ToString(zavolejPamet(5)));
            priklad = priklad.Replace("MR(6)", Convert.ToString(zavolejPamet(6)));
            priklad = priklad.Replace("MR(7)", Convert.ToString(zavolejPamet(7)));
            priklad = priklad.Replace("MR(8)", Convert.ToString(zavolejPamet(8)));
            priklad = priklad.Replace("MR(9)", Convert.ToString(zavolejPamet(9)));
            priklad = priklad.Replace("MR(0)", Convert.ToString(zavolejPamet(0)));
            char[] equal = new char[1] { '=' };
            string[] priklady = priklad.Split(equal);
            foreach (string zadani in priklady) {
                if (zadani != "")
                {
                    vysledek = Kalkulacka.vypocitej(vysledek + zadani);
                }
            }
            return vysledek;
        }
        public static void provedInstrukce(TextBox textBox)
        {
            int cisloPameti;
            string tempArgument;
            double argument;
            foreach (string line in textBox.Lines)
            {
                if (line.Contains("MS"))
                {
                    cisloPameti = Convert.ToInt32(line.Substring(line.IndexOf("(") + 1, 1));
                    tempArgument = line.Replace(" ", "").Trim();
                    argument = Convert.ToDouble(tempArgument.Substring(0, tempArgument.IndexOf("MS")));              
                    ulozDoPameti(cisloPameti, argument);
                }
                else if (line.Contains("MR"))
                {
                    cisloPameti = Convert.ToInt32(line.Substring(line.IndexOf("(") + 1, 1));
                    zavolejPamet(cisloPameti);

                }

            }
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
                text = "";
            }
            return text;
        }



    }
}
