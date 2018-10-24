using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using qi;

namespace wuziqi
{
    public partial class Form1 : Form
    {
        qi1 myqi = new qi1();
        qiq borw = qiq.Black;
        public Form1()
        {
            InitializeComponent();
            Width = 800;
            Height = 780;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fileFToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen blankPen = new Pen(Color.Black);
            SolidBrush brownBrush = new SolidBrush(Color.BurlyWood);
            g.FillRectangle(brownBrush, new Rectangle(30, 50, 600, 600));
            for (int i = 0; i < 15; i++)
            {
                g.DrawLine(blankPen, 50, 70 + i * 40, 560 + 50, 70 + i * 40);
                g.DrawLine(blankPen, 50 + i * 40, 70, 50+i*40, 70+560);
            }
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);

            for (int i = 0; i <15 ; i++)
                for (int j = 0; j < 15; j++)
                {
                    if (myqi.five[i, j] == qiq.Black)
                        g.FillEllipse(blackBrush, new Rectangle(50 + i * 40 - 15, 70 + j * 40 - 15, 30, 30));
                    if (myqi.five[i, j] == qiq.White)
                        g.FillEllipse(whiteBrush, new Rectangle(50 + i * 40 - 15, 70 + j * 40 - 15, 30, 30));
                }
            {

            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left&&e.X>30&&e.X<630&&e.Y
                >50&&e.Y<650)
            {
                int i = (e.X - 50 + 20) / 40;
                int j = (e.Y - 70 + 20) / 40;
                myqi.five[i, j] = borw;
                borw = (borw == qiq.Black ? qiq.White : qiq.Black);
                Invalidate();
            }
        }
    }


}
