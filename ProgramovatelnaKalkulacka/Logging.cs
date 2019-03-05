using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramovatelnaKalkulacka
{
    class Logging
    {
        
        public static void WriteLog(string inputText, string filePath) {
            StringBuilder sb = new StringBuilder();
            sb.Append(inputText);
            File.AppendAllText(filePath + "\\log.txt", DateTime.Now.ToString() + "\t" + sb.ToString() + Environment.NewLine);
            sb.Clear();
        }
        
        
    }
}
