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
using System.Globalization;

namespace tastTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string s = String.Empty;
        double temp, avarage = 0;
        int splitBy = 0;

        void loadFile_Click(object sender, EventArgs e)
        {
            using (StreamReader sr = File.OpenText("WIG20.txt"))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    List<string> values = new List<string>(s.Split(','));
                   
                    try
                    {
                        temp += double.Parse(values[5], CultureInfo.InvariantCulture);
                        avarage = temp / ++splitBy;
                        values.Add(avarage.ToString(CultureInfo.InvariantCulture));
                    }
                    catch (FormatException)
                    {
                      
                    }

                }
            }
        }

        void makeFile(List<string> values)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                string s = "Header";
                sw.WriteLine(s);
                foreach(line in lines)
                {

                }
            }
        }
         void shwResult_Click(object sender, EventArgs e)
        {
           
        }
    }
}
