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
        void shwResult_Click(object sender, EventArgs e)
        {
            string s = String.Empty;
            double temp = 0;
            double avarage = 0;
            int splitBy = 0;
            List<List<string>> values = new List<List<string>>();

            using (StreamReader sr = File.OpenText("WIG20.txt"))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    List<string> value = new List<string>(s.Split(','));

                    try
                    {
                        if(splitBy > 15)
                        {
                            temp = 0;
                            splitBy = 0;
                        }
                        temp += double.Parse(value[5], CultureInfo.InvariantCulture);
                        avarage = temp / ++splitBy;
                        value.Add(avarage.ToString(CultureInfo.InvariantCulture));
                        values.Add(value);
                    }
                    catch (FormatException)
                    {
                    
                    }
                }

                    int i = 0;
                    string path = "WIG_" + i + ".txt";

                while(File.Exists(path))
                    {
                    i++;
                    path = "WIG_" + i + ".txt";
                    }
                using (StreamWriter sw = new StreamWriter(path))
                    {
                        foreach (List<string> Value in values)
                        {
                        Value.ForEach(x => sw.Write("{0}\t", x));
                        sw.WriteLine("");
                        }
                    }
               
                 foreach (List<string> Value in values)
                    {
                        int n = Value[1].IndexOf("2016");

                        if (n != -1)
                        {
                            this.chart1.Series["SMA"].Points.AddXY(Value[0], Value[7]);
                        }
                }
            }
        }

    }
}
