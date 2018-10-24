using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wuziqi
{
    public partial class Form2 : System.Windows.Forms.Form
    {
        public static string ip;     //定义对方IP
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ip = textBox1.Text;           
            Form1 mySecondForm = new Form1(); 
            mySecondForm.Show();                                                                 
        }
       
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostByName(hostname);
            IPAddress ipAddress = localhost.AddressList[0];
            textBox2.Text = ipAddress.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
