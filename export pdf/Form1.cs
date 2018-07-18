using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Threading;




namespace Export_PDF
{
    public partial class Form1 : Form
    {
        int result = 0;
        Thread t;
        delegate void DisplayRefreshCallback();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            t = new Thread(threadExport);
            t.Start();

            
        }

        public void threadExport()
        {

            result = 1;


            try
            {


                for (int i = 0; i <= 50; i++)
                {

                    Document doc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    string path = @"C:\Users\faruk.aziz\Desktop\PDF\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);

                    }

                    

                    string name = "09/01/2015_10:50:30";
                    string new_name = name.Replace("/", "-");
                    string final_name = new_name.Replace(":", "-");

                    final_name = final_name + "-" + i;



                    PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(path + final_name + ".pdf", FileMode.Create));

                    doc.Open();

                    invokeForm();


                    using (MemoryStream stream = new MemoryStream())
                    {
                        chart1.SaveImage(stream, ChartImageFormat.Png);
                        iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                        chartImage.ScalePercent(75f);
                        doc.Add(chartImage);
                    }


                    doc.Close();
                    result++;

                   

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }


        }

    

        public void invokeForm()
        {
            if(this.chart1.InvokeRequired)
            {
               DisplayRefreshCallback d = new DisplayRefreshCallback(invokeForm);
               chart1.Invoke(d);
            }
            else
            {


                chart1.Series.Clear();
                
                chart1.Series.Add("series");
                


                chart1.Series[0].Points.AddXY(5, 10);
                chart1.Series[0].Points.AddXY(10, 15);
                chart1.Series[0].Points.AddXY(15, 20);
                chart1.Series[0].Points.AddXY(20, 25);
                chart1.Series[0].Points.AddXY(25, 30);
                
                

            }
           

}

        private void Form1_Load(object sender, EventArgs e)
        {
             
        }

        private void Form1_Unload(object sender, EventArgs e)
        {
            t.Abort();
        }

         

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = result.ToString();

        }


    }
}
