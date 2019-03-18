using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgramovatelnaKalkulacka
{
    public class Polynom
    {
        // Regularni vyraz ziskava jednotlive cleny mnohoclenu vcetne znamenka jednotlivych clenu.
        private static Regex PolynomSplitterExpression = new Regex(@"y\s*=(?<clen>\s*(?<znamenko>(\+|\-|))(?<cast>[^\+\-]+))+");

        // Regularni vyraz ziskava exponenty jednotlivych clenu, aby mohly byt dale vyuzity
        private static Regex TermSplitterExpression = new Regex(@"^(?<k_n>[0-9\.]*)\s*(/)?\s*(?<k_d>[0-9]*)\s*(?<x>x{0,1})\s*(\^\s*(?<exp_n>[0-9\.]+)\s*(/)?\s*(?<exp_d>[0-9]*))?$");

        /// <summary>
        /// Vypocitava zavislou promennou y pro jednotlive nezavisle promenne x 
        /// </summary>
        /// <param name="tbPolynom">Polynom ve tvaru: y = ax^n + bx^n-1 + ... + m .</param>
        /// <param name="tbPocatek">Pocatecni hodnota X.</param>
        /// <param name="tbKonec">Posledni hodnota X.</param>
        /// <param name="tbPocet">Pocet hodnot, ktere maji byt vypocitany.</param>
        /// <returns>Datatable s hodnotami X a Y.</returns>

        public static DataTable VypoctiPolynom(string tbPolynom, double tbPocatek, double tbKonec, int tbPocet)
        {
            // Rozdeleni polynomu na cleny
            Match m = PolynomSplitterExpression.Match(tbPolynom);
            if (!m.Success)
                throw new Exception(String.Format("Separace '{0}' na jednotlive cleny nebyla uspesna.", tbPolynom));

            // Vytvoreni Datatable s vysledky
            DataTable dtVysledek = new DataTable("Vysledky");
            dtVysledek.Columns.Add("X", typeof(double));
            dtVysledek.Columns.Add("Y", typeof(double));

            // Vytvoreni Datatable s jednotlivymi cleny
            DataTable dtCleni = new DataTable("Cleni");
            dtCleni.Columns.Add("k", typeof(double));
            dtCleni.Columns.Add("exp", typeof(double));

            double koeficient = 0.0;
            double exponent = 0.0;
            int znamenko = 1;
            int ziskanyIndex = 0; // pro ziskani spravneho znamenka
            
            #region Vypocet koeficientu a exponentu pro kazdy clen mnohoclenu
            foreach (Capture ziskanyClen in m.Groups["cast"].Captures) {
                Match mClen = TermSplitterExpression.Match(ziskanyClen.Value.Trim());
                if (!mClen.Success)
                    throw new Exception(String.Format("Clen '{0}' neni podporovan.", ziskanyClen.Value.Trim()));

                string s = m.Groups["znamenko"].Captures[ziskanyIndex++].Value.Trim();
                if ((s == "+") || (s == "")) // Pokud je prazdne, predpoklada se kladne znamenko.
                    koeficient = 1;
                else if (s == "-")
                    koeficient = -1;

                // Ziskani hodnoty koeficientu
                koeficient = znamenko * VypoctiMezivysledek(mClen.Groups["k_n"].Value, mClen.Groups["k_d"].Value);

                // Ziskani hodnoty exponentu
                if (String.IsNullOrEmpty(mClen.Groups["x"].Value))
                    exponent = 0.0;
                else
                    exponent = VypoctiMezivysledek(mClen.Groups["exp_n"].Value, mClen.Groups["exp_d"].Value);

                // Add the values to the data table
                dtCleni.Rows.Add(koeficient, exponent);
            }
            #endregion

            #region Vypocet hodnoty Y
            // Vypocet velikosti kroku podle poctu X
            double velikostKroku = (tbKonec - tbPocatek) / (double)tbPocet;
            double y = 0.0;
            for (double x = tbPocatek; x <= tbKonec; x += velikostKroku) {
                y = 0.0;
                foreach (DataRow drClen in dtCleni.Rows) {
                    koeficient = (double)(drClen)["k"];
                    exponent = (double)(drClen)["exp"];

                    if (exponent == 0.0)
                        y += koeficient;
                    else if (exponent == 1)
                        y += (koeficient * x);
                    else
                        y += (koeficient * Math.Pow(x, exponent));

                }
                dtVysledek.Rows.Add(x, y);

            }
            return dtVysledek;
            #endregion

        }

        public static double VypoctiMezivysledek(string _citatel, string _jmenovatel)
        {
           // Nastaveni vychozich hodnot
            double citatel = 1.0;
            double jmenovatel = 1.0;

            // Ziskej citatele, pokud neni prazdny
            if (!String.IsNullOrEmpty(_citatel))
            {
                citatel = Convert.ToDouble(_citatel);

                // Ziskej jmenovatele, pokud neni prazdny
                if (!String.IsNullOrEmpty(_jmenovatel))
                    jmenovatel = Convert.ToDouble(_jmenovatel);
            }

            // Calculate the fraction and return the value
            return (citatel / jmenovatel);
        }

    }

    }
