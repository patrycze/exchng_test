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
using System.Windows.Forms.DataVisualization.Charting;

namespace tastTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Data
        {
            public string Name;
            public string Date;
            public string Open;
            public string High;
            public string Low;
            public string Close;
            public string Volume;
            public string Average;
            
           public Data(string name, string date, string open, string high, string low, 
                string close, string volume)
            {
                Name = name;
                Date = date;
                Open = open;
                High = high;
                Low = low;
                Close = close;
                Volume = volume; 
            }



        }
        
         void shwResult_Click(object sender, EventArgs e)
        {
            // blokada przycisku
           // shwResult.Enabled = false;
            string s = String.Empty;
            double temp = 0;
            double avarage = 0;
            int splitBy = 0;
            List<Data> values = new List<Data>();
            string path = System.Windows.Forms.Application.StartupPath + "../../../data/WIG20.txt";
            //System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "../../../data/WIG20.txt";
            //Environment.GetFolderPath(Environment.SpecialFolder.Windows);

            using (StreamReader sr = File.OpenText(path))
                {
                
                    while ((s = sr.ReadLine()) != null)
                    {
                    List<string> value = new List<string>(s.Split(','));
                    Data objekt = new Data(value[0], value[1], value[2], value[3], value[4], value[5], value[6]);
                    values.Add(objekt);        
                        try
                        {
                            if (splitBy > 15)
                            {
                                temp = 0;
                                splitBy = 0;
                            }
                                
                        temp += double.Parse(value[5], CultureInfo.InvariantCulture);
                        avarage = temp / ++splitBy;
                        avarage = Math.Round(avarage, 2);
                        objekt.Average = avarage.ToString(CultureInfo.InvariantCulture);
                    }
                        catch (FormatException)
                        {

                        }
                    }
                    // uwaga na temat ścieżki
                    
                    int i = 0;
                    path = System.Windows.Forms.Application.StartupPath + "/WIG_" + i + ".txt";

                    while (File.Exists(path))
                    {
                        i++;
                        path = "WIG_" + i + ".txt";
                    }
                    using (StreamWriter sw = new StreamWriter(path))
                    {

                       
                        foreach (var Value in values)
                        {
                        sw.Write("{0} {1} {2} {3} {4} {5} {6} {7}",Value.Name, Value.Date, Value.Open, Value.High, Value.Low, Value.Close, Value.Volume, Value.Average);
                        
                        //Value.ForEach(x => sw.Write("{0}\t", x));
                            sw.WriteLine("");
                        }
                    }
                chart1.Series["SMA"].Points.Clear();
                chart1.Series["average"].Points.Clear();
                

                foreach (var Value in values)
                    {

                    int n = Value.Date.IndexOf("2016");
                        
                    

                    if (n != -1)
                        {
                        this.chart1.Series["SMA"].Points.AddXY(Value.Date, Value.Average);
                        this.chart1.Series["average"].Points.AddXY(Value.Date, Value.Close);
                        }
                    
                }
                
            }
                
        }

       
    }
}
