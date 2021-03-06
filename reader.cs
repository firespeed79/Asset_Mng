using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Asset_Mng
{
    public partial class reader : Form
    {
        public reader()
        {
            InitializeComponent();
        }

        public static string file1 = @"C:\Users\kiM\Documents\Visual Studio 2005\Projects\Asset_Mng\Asset_Mng\bin\Debug\DCT\SendCompleted.txt";
        public static string file2 = @"C:\Users\kiM\Documents\Visual Studio 2005\Projects\Asset_Mng\Asset_Mng\bin\Debug\DCT\SendUncompleted.txt";
        public static string file3 = @"C:\Users\kiM\Documents\Visual Studio 2005\Projects\Asset_Mng\Asset_Mng\bin\Debug\DCT\ReceiveCompleted.txt";
        public static string file4 = @"C:\Users\kiM\Documents\Visual Studio 2005\Projects\Asset_Mng\Asset_Mng\bin\Debug\DCT\ReceiveUncompleted.txt";
        public static int counter;
        public static string check = "0";
        public static Process proc = new Process();
        private void button1_Click(object sender, EventArgs e)
        {
            //calling the process
            ProcessStartInfo startinfo = new ProcessStartInfo(@"C:\Users\kiM\Documents\Visual Studio 2005\Projects\Asset_Mng\Asset_Mng\bin\Debug\DCT\PajaExec_Send - Shortcut");
          //  proc.CreateObjRef(
            proc.StartInfo = startinfo;
            proc.Start();
            check = "1";
            //deleting the files
            File.Delete(file1);
            File.Delete(file2);
            //timer for 1 min
            counter = 60;
            timer1.Interval = 1000;
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = counter.ToString();


            if (counter == 0)
                timer1.Stop();
            if (File.Exists(file1))
                label2.Text = "found";
            if (File.Exists(file2))
                label3.Text = "found";


            counter--;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //deleteing the database
            SqlConnection connection = Main.connection;
            string delete = "Delete FROM DCT_Asset";
            connection.Open();
            SqlCommand command = new SqlCommand(delete, connection);
            command.ExecuteNonQuery();
            connection.Close();

            //calling the process
            ProcessStartInfo startinfo = new ProcessStartInfo(@"C:\Users\kiM\Documents\Visual Studio 2005\Projects\Asset_Mng\Asset_Mng\bin\Debug\DCT\PajaExec_Receive - Shortcut");
            proc.StartInfo = startinfo;
            proc.Start();
            check = "1";
            //checking the files
            File.Delete(file3);
            File.Delete(file4);
            counter = 60;
            timer2.Interval = 1000;
            timer2.Start();
            
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label1.Text = counter.ToString();


            if (counter == 0)
                timer2.Stop();


            if (File.Exists(file3))
                label2.Text = "found";
            if (File.Exists(file4))
                label3.Text = "found";


            counter--;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (check == "1")
            {
                proc.Kill();
                timer1.Stop();
                timer2.Stop();
            }

            // startinfo.
            check = "0";
            MessageBox.Show("عملیات متوقف شد.", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}