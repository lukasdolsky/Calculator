using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CalculatorApp
{
    public partial class Calculator : Form
    {
        Double result = 0;
        String operationPerformed = "";
        bool isOperationPerformed = false;




        public Calculator()
        {
            InitializeComponent();
        }

        private void LogMsg() {
            StreamWriter sw = new StreamWriter("log.txt", true);
            sw.WriteLine($"User Message:(Result:{result.ToString()}\tResult1:{result}\tResult:{result.ToString()})");//log message
            sw.Close();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button_click(object sender, EventArgs e)
        {
            if ((textBoxResult.Text == "0")|| (isOperationPerformed))
            {
                textBoxResult.Clear();
            }
            isOperationPerformed = false;
            Button button = (Button)sender;
            if (button.Text == ".")
            {
                if (!textBoxResult.Text.Contains("."))
                    textBoxResult.Text = textBoxResult.Text + button.Text;
                

            }
            else
                textBoxResult.Text = textBoxResult.Text + button.Text;
            
        }

        private void importXMLButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openInputFileDialog = new OpenFileDialog();
            if (openInputFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string strfilename = openInputFileDialog.FileName;
                MessageBox.Show(strfilename);
            }
        }

        private void operatorClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (result != 0)
            {
                // !!!CHYBA!!! Pri kliknuti na plus po ziskani vysledku se provede resultClick metoda 
                buttonResult.PerformClick();
                operationPerformed = button.Text;
                labelCurrentOperation.Text = result + " " + operationPerformed;
                isOperationPerformed = true;
            }
            else
            {
                operationPerformed = button.Text;
                result = Double.Parse(textBoxResult.Text);
                labelCurrentOperation.Text = result + " " + operationPerformed;
                isOperationPerformed = true;
            }
            
        }

        private void resultClick(object sender, EventArgs e)
        {
            switch (operationPerformed)
            {
                case "+":
                    textBoxResult.Text = (result + Double.Parse(textBoxResult.Text)).ToString();
                    LogMsg();
                    break;
                case "-":
                    textBoxResult.Text = (result - Double.Parse(textBoxResult.Text)).ToString();
                    LogMsg();
                    break;
                case "*":
                    textBoxResult.Text = (result * Double.Parse(textBoxResult.Text)).ToString();
                    LogMsg();
                    break;
                case "/":
                    textBoxResult.Text = (result / Double.Parse(textBoxResult.Text)).ToString();
                    LogMsg();
                    break;
                default:

                    break;
                    
            }
            result = Double.Parse(textBoxResult.Text);
            labelCurrentOperation.Text = "";
        }
    }
}
