using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ProgramovatelnaKalkulacka
{
    class HlasovaciPrava
    {
        public static XmlDocument nactiXML(string filePath) {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            
            return doc;    
        }

        public static double plochaVsechJednotek(XmlDocument doc) {
            XmlNodeList nodeList = doc.SelectNodes("/bytovy_dum/byt");
            double plochaCelkem = 0;
            foreach (XmlNode node in nodeList)
            {
                try
                {
                    double plocha = Double.Parse(node["plocha"].InnerText);
                    plochaCelkem += plocha;
                }
                catch
                {
                    MessageBox.Show("Chyba ve cteni XML souboru", "xmlError", MessageBoxButtons.OK);
                }
            }
            return plochaCelkem;
        }

        public static List<string> nactiJednotky(string jednotky) {
            jednotky = jednotky.Replace(" ", "");
            List<string> bytoveJednotky = jednotky.Split(',').ToList();
            return bytoveJednotky;
        }

        public static double vypoctiPlochuZadanychJednotek(List<string> jednotky, XmlDocument doc) {
            double celkovaPlochaZadanychJednotek = 0;
            XmlNodeList nodeList = doc.SelectNodes("/bytovy_dum/byt");
            foreach (string id in jednotky) {
                double plocha = 0;
                celkovaPlochaZadanychJednotek += plocha;
            }
            return celkovaPlochaZadanychJednotek;
        }

        public static bool jeUsnasenischopna(double plochaCelkem, double plochaJednotky) {
            if (plochaJednotky / plochaCelkem > 0.5)
            {
                return true;
            }
            else return false;
        }
    }
}
