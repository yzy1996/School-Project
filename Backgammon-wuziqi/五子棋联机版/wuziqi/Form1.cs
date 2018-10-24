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
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Drawing.Drawing2D;

/*
 发消息：
 1000;自己已就绪
 1001:自己为黑色的信息
 Convert.ToString(i + 15 * j):棋子信息
 2000:我赢了
 2001:对方赢了
 3001:发出和棋信号
 3004：同意和棋
 3003：不同意和棋
 4000：请求悔棋
 4001：同意悔棋
 4002：不同意悔棋
*/

namespace wuziqi
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        ///--------------------成员声明---------------------------------------------------------------------------------------------------

        public wuziqiBase myWuZiQi = new wuziqiBase();    //public StatusOfColor borw = StatusOfColor.Empty;

        public int x =0, y = 0;//棋盘左上角交叉点坐标

        public bool gameStatus_Own = false;//自己的状态
        public bool gameStatus_Opponent = false;//对方的状态
        public bool gameStatus = false;

        int timeLeft_Own = 20 * 60;
        int timeLeft_Opponent = 20 * 60;

        public StatusOfColor colorSelected = 0;//用于决定双方的黑白棋,1是自己是黑，2是自己是白
        public StatusOfColor colorSelected_Own = 0;
        public StatusOfColor colorSelected_Opponent = 0;

        public bool speakPeace = false;//和棋变量
        public bool undo = false;//悔棋变量
        public bool myTurn = false;//false是自己不可下子，ture是自己可下子

        public int gameResult = 0;//0是正在比赛，1是自己赢了,2是对方赢了，3是和棋
        
        private Thread th;
        private TcpListener tcpl;
        public bool listenerRun = true;
        Sender se = new Sender();
        ///------------------------------------------------------------------------------------------------------------------------------




        ///--------------------主界面----------------------------------------------------------------------------------------------------

        //初始化窗口&&调整窗口大小
        public Form1()
        {
            InitializeComponent();
            Width = 680+125 ;
            Height = 700;
        }

        //准备主界面
        private void Form1_Load(object sender, EventArgs e)
        {
            Game_panel1_toolbar.Hide();
            Game_lable_timeShow_Own.Hide();
            Game_lable_timeShow_Opponent.Hide();
            th = new Thread(new ThreadStart(Listen));//新建一个用于监听的线程
            th.Start();//打开新线程
        }

        //绘制主界面"Main_panel2_cover"
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //点击"开始游戏"时
        private void Main_Begin_Click(object sender, EventArgs e)
        {
            gamestart();
            judgestart();
            Main_panel2_Cover.Hide();
            Game_panel1_toolbar.Show();     
             Game_lable_timeShow_Own.Show();             
            Game_lable_timeShow_Opponent.Show(); 
            Game_timer1.Start();                     
        }

        //点击"出品人"时
        private void Main_Publish_Click(object sender, EventArgs e)
        {
            MessageBox.Show("出品人 马千里 孙浩原 杨致远 徐思渊");
        }
        ///------------------------------------------------------------------------------------------------------------------------------




        ///--------------------游戏界面-------------------------------------------------------------------------------------------------
 
        //绘制游戏界面
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (gameStatus == true)
            {
                x = 50;
                y = 70;
                Graphics g = e.Graphics;
                Pen blankPen = new Pen(Color.Black);
                // SolidBrush brownBrush = new SolidBrush(Color.BurlyWood);
                //g.FillRectangle(brownBrush, new Rectangle(30, 50, 600, 600));
                TextureBrush textureBrush_Mu = new TextureBrush(new Bitmap("Wenli_Mu.jpg"));
                g.FillRectangle(textureBrush_Mu, new Rectangle(x-20, y-20, 600, 600));
                for (int i = 0; i < 15; i++)
                {
                    g.DrawLine(blankPen, x, y + i * 40, 560 + x, y + i * 40);
                    g.DrawLine(blankPen, x + i * 40, y, x + i * 40, y + 560);
                }
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                g.FillEllipse(blackBrush, new Rectangle(x + 3 * 40 - 3, y + 3 * 40 - 3, 6, 6));
                g.FillEllipse(blackBrush, new Rectangle(x + 3 * 40 - 3, y + 11 * 40 - 3, 6, 6));
                g.FillEllipse(blackBrush, new Rectangle(x + 11 * 40 - 3, y + 3 * 40 - 3, 6, 6));
                g.FillEllipse(blackBrush, new Rectangle(x + 11 * 40 - 3, y + 11 * 40 - 3, 6, 6));
                g.FillEllipse(blackBrush, new Rectangle(x + 7 * 40 - 3, y + 7 * 40 - 3, 6,6));

                for (int i = 0; i < 15; i++)
                    for (int j = 0; j < 15; j++)
                    {
                        if (myWuZiQi.statusOfPoint[i, j] == StatusOfColor.Black)
                        {
                            //g.FillEllipse(blackBrush, new Rectangle(50 + i * 40 - 15, 70 + j * 40 - 15, 30, 30));
                            using (GraphicsPath path = new GraphicsPath())
                            {
                                path.AddEllipse(x + i * 40 - 15, y + j * 40 - 15, 30, 30);
                                //渐变填充GraphicsPath对象
                                using (PathGradientBrush brush = new PathGradientBrush(path))
                                {
                                    g.SmoothingMode = SmoothingMode.HighQuality;
                                    //渐变中心点颜色
                                    brush.CenterColor = Color.White;
                                    //渐变中心点位置
                                    brush.CenterPoint = new PointF(x + i * 40, y + j * 40);
                                    brush.SurroundColors = new Color[] { Color.Black };
                                    g.FillPath(brush, path);
                                }
                            }
                        }
                        if (myWuZiQi.statusOfPoint[i, j] == StatusOfColor.White)
                        {
                            // g.FillEllipse(whiteBrush, new Rectangle(50 + i * 40 - 15, 70 + j * 40 - 15, 30, 30));
                            using (GraphicsPath path = new GraphicsPath())
                            {
                                path.AddEllipse(x + i * 40 - 15, y + j * 40 - 15, 30, 30);
                                //渐变填充GraphicsPath对象
                                using (PathGradientBrush brush = new PathGradientBrush(path))
                                {
                                    g.SmoothingMode = SmoothingMode.HighQuality;
                                    //渐变中心点颜色
                                    brush.CenterColor = Color.White;
                                    //渐变中心点位置
                                    brush.CenterPoint = new PointF(x + i * 40, y + j * 40);
                                    brush.SurroundColors = new Color[] { Color.FromArgb(255, 0xCF, 0xB8, 0xB8) };
                                    g.FillPath(brush, path);
                                }
                            }
                        }
                    }
            }
        }

        //落子&&判断是否五子连珠
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (gameStatus && myTurn)
            {
                if (e.Button == MouseButtons.Left && e.X > 30 && e.X < 630 && e.Y
                    > 50 && e.Y < 650)
                {
                    int i = (e.X - 50 + 20) / 40;
                    int j = (e.Y - 70 + 20) / 40;
                    if (myWuZiQi.statusOfPoint[i, j] == StatusOfColor.Empty)
                    {
                        //myWuZiQi.statusOfPoint[i, j] = borw;
                        myWuZiQi.statusOfPoint[i, j] = colorSelected;
                        myWuZiQi.recorder.Push(new Point(i, j));
                        Invalidate();
                        send(Convert.ToString(i + 15 * j));
                        //Thread.Sleep(1000);
                        if (myWuZiQi.Judge(i, j))
                        {
                            send("2000");
                            gameResult = 1;
                            ChangeResult(1);
                        }
                        Changesituation();
                    }
                }
            }
        }

        //点击"悔棋"时
        private void button1_Click(object sender, EventArgs e)
        {
            send("4000");
            MessageBox.Show("请求已发出");
            while (undo == false)
            {
                Thread.Sleep(100);
            }
            undo = false;
        }

        //点击"认输"时
        private void button2_Click(object sender, EventArgs e)
        {
            send("2001");
            gameResult = 2;
            ChangeResult(2);
        }

        //点击"讲和"时
        private void button3_Click(object sender, EventArgs e)
        {
            send("3000");
            MessageBox.Show("请求已发出");
            while (speakPeace == false)
            {
                Thread.Sleep(100);
            }
            speakPeace = false;
        }
       
        //计时器
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gameStatus&&colorSelected!=0)
            {
                if (myTurn == false)
                {                   
                    timeLeft_Opponent--;
                    Game_lable_timeShow_Own.Text = Convert.ToString(timeLeft_Opponent / 60) + ":" + Convert.ToString(timeLeft_Opponent % 60);
                }
                else if(myTurn==true)
                {                               
                    timeLeft_Own--;
                    Game_lable_timeShow_Opponent.Text = Convert.ToString(timeLeft_Own / 60) + ":" + Convert.ToString(timeLeft_Own % 60);
                }
            }
            if(timeLeft_Own==0)
            {
                gameResult = 2;
                ChangeResult(2);
                send("2001");
            }
            if (gameResult != 0)
            {
                //start1 = false; start2 = false;
                switch (gameResult)
                {
                    case 1:
                        Game_timer1.Enabled = false;
                        MessageBox.Show("YOU WIN!");
                        break;
                    case 2:
                        Game_timer1.Enabled = false;
                        MessageBox.Show("YOU LOSE!");
                        break;
                    case 3:
                        Game_timer1.Enabled = false;
                        MessageBox.Show("DRAW!");
                        break;
                }
                
                gameover();
            }
        }

        //双方都准备好时,游戏开始
        public void judgestart()
        {
            gameStatus_Own = true;
            send("1000");
            while (!gameStatus)
            {
                if (gameStatus_Own && gameStatus_Opponent)
                    gameStatus = true;
                Thread.Sleep(100);
            }
        }

        //确定哪一方执黑先走
        public void gamestart()
        {
            se.Send("1001");
            colorSelected_Own = StatusOfColor.Black;
            if (colorSelected_Opponent == StatusOfColor.Black)
                colorSelected_Own = StatusOfColor.White;
            while (colorSelected_Opponent == 0)
            {
                Thread.Sleep(100);
            };//等待接受
            if (colorSelected_Own == StatusOfColor.Black && colorSelected_Opponent == StatusOfColor.White)
                colorSelected = StatusOfColor.Black;
            else if (colorSelected_Own == StatusOfColor.White && colorSelected_Opponent == StatusOfColor.Black)
                colorSelected = StatusOfColor.White;
            else
                colorSelected = 0;
            if (colorSelected == StatusOfColor.White)
            {
                //borw = StatusOfColor.White;               
                myTurn = false;
                Game_lable_currentPlayer.Text = "•轮到对方•";
            }
            else if(colorSelected== StatusOfColor.Black)
            {
                //borw = StatusOfColor.Black;                
                myTurn = true;
                Game_lable_currentPlayer.Text = "•轮到你•";

            }
        }

        //游戏结束
        public void gameover()
        {
            gameStatus = false;
            gameStatus_Own = false;
            gameStatus_Opponent = false;
            colorSelected = 0;
            colorSelected_Own = 0;
            colorSelected_Opponent = 0;
            myTurn = false;
            myWuZiQi.init();
            listenerRun = false;
            thStop();
            this.Close();//一局游戏结束过后直接关闭窗体
        }

        //胜利or失败界面
        public void ChangeResult(int a)
        {


        }       

        //显示当前轮到哪一方走棋
        public void Changesituation()
        {
            if(myTurn==true)
            {
                myTurn = false;
                Game_lable_currentPlayer.Text = "•轮到对方•";
            }
            else if (myTurn == false)
            {
                myTurn = true;
                Game_lable_currentPlayer.Text = "•轮到你•";
            }

        }

        ///------------------------------------------------------------------------------------------------------------------------------



        ///--------------------消息&监听-----------------------------------------------------------------------------------------------

        //消息发送函数
        public void send(string aa)
        {
            se.Send(aa);
        }

        //开始监听
        private void Listen()
        {
            try
            {
                string hostname = Dns.GetHostName();
                IPHostEntry localhost = Dns.GetHostByName(hostname);
                IPAddress ipAddress = localhost.AddressList[0];
                //IPHostEntry heserver = Dns.GetHostEntry("localhost");
                //IPAddress ipAddress = heserver.AddressList[1];
                //Console.WriteLine(ipAddress.ToString());
                try
                {
                    tcpl = new TcpListener(ipAddress, 5656);
                    
                }
                catch (Exception e)
                {}
                tcpl.Start();
                while (listenerRun)//开始监听
                {
                    Socket s = tcpl.AcceptSocket();
                    //string remote = s.RemoteEndPoint.ToString();
                    Byte[] stream = new Byte[80];
                    int i = s.Receive(stream);//接受连接请求的字节流
                    string msg = System.Text.Encoding.UTF8.GetString(stream);
                    Int32 a0 = Convert.ToInt32(msg);
                    //此处加判断
                    switch (a0)
                    {
                        case 1000:
                            {gameStatus_Opponent=true;   break; }
                        case 1001:
                            {
                                if (colorSelected_Own == 0)
                                    colorSelected_Opponent = StatusOfColor.Black;
                                
                                else
                                    colorSelected_Opponent = StatusOfColor.White;
                                    break;
                            }
                        case 2000:
                            {gameResult = 2;  ChangeResult(2); break;  }
                        case 2001:
                            { gameResult = 1; ChangeResult(1); break; }
                        case 3000:
                            {
                                var gg=MessageBox.Show("是否同意和棋","和棋", MessageBoxButtons.YesNo);
                                if(gg== DialogResult.No)
                                {
                                    send("3003");
                                }
                                else if(gg==DialogResult.Yes)
                                {
                                    send("3004");
                                    gameResult = 3;
                                    ChangeResult(3);
                                }
                                break;
                            }
                        case 3004:
                            {
                                speakPeace = true;
                                MessageBox.Show("对方同意和棋");
                                gameResult = 3;
                                ChangeResult(3);
                                break;
                            }
                        case 3003:
                            {
                                speakPeace = true;
                                MessageBox.Show("对方不同意和棋");
                                break;
                            }
                        case 4000:
                            {
                                var gg = MessageBox.Show("是否同意悔棋", "悔棋", MessageBoxButtons.YesNo);
                                if (gg == DialogResult.No)
                                {
                                    send("4002");
                                }
                                else if(gg==DialogResult.Yes)
                                {
                                    send("4001");
                                    if(myTurn==false)
                                    {
                                        myWuZiQi.statusOfPoint[myWuZiQi.recorder.Peek().X, myWuZiQi.recorder.Peek().Y] = StatusOfColor.Empty;
                                        myWuZiQi.recorder.Pop();
                                        myWuZiQi.statusOfPoint[myWuZiQi.recorder.Peek().X, myWuZiQi.recorder.Peek().Y] = StatusOfColor.Empty;
                                        myWuZiQi.recorder.Pop();
                                        Invalidate();
                                    }
                                    else if(myTurn==true)
                                    {
                                        myWuZiQi.statusOfPoint[myWuZiQi.recorder.Peek().X, myWuZiQi.recorder.Peek().Y] = StatusOfColor.Empty;
                                        myWuZiQi.recorder.Pop();
                                        Invalidate();
                                        Changesituation();
                                    }
                                }
                                break;
                            }
                        case 4002:
                            {
                                 undo= true;
                                MessageBox.Show("对方不同意悔棋");
                                break;
                            }
                        case 4001:
                            {
                                undo = true;
                                MessageBox.Show("对方同意悔棋");
                                if (myTurn == false)
                                {
                                    myWuZiQi.statusOfPoint[myWuZiQi.recorder.Peek().X, myWuZiQi.recorder.Peek().Y] = StatusOfColor.Empty;
                                    myWuZiQi.recorder.Pop();                                 
                                    Invalidate();
                                    Changesituation();
                                }
                                else if (myTurn == true)
                                {
                                    myWuZiQi.statusOfPoint[myWuZiQi.recorder.Peek().X, myWuZiQi.recorder.Peek().Y] = StatusOfColor.Empty;
                                    myWuZiQi.recorder.Pop();
                                    myWuZiQi.statusOfPoint[myWuZiQi.recorder.Peek().X, myWuZiQi.recorder.Peek().Y] = StatusOfColor.Empty;
                                    myWuZiQi.recorder.Pop();
                                    Invalidate();
                                }
                                break;
                            }
                        default:
                            {
                                if(Convert.ToInt32(msg)>=0&& Convert.ToInt32(msg) <300)
                                {
                                    int c = Convert.ToInt32(msg)%15;
                                    int d = Convert.ToInt32(msg)/15;
                                    //myWuZiQi.statusOfPoint[c, d] = 3-borw;
                                    myWuZiQi.statusOfPoint[c, d] = 3 - colorSelected;
                                    myWuZiQi.recorder.Push(new Point(c, d));
                                    Invalidate();
                                    Changesituation();
                                }
                                break;
                            }
                    }
                }
            }
            catch (System.Security.SecurityException)
            {
            }
            catch (Exception)
            {
            }
        }

        //终止线程
        public void thStop()
        {
            tcpl.Stop();
            th.Abort();//终止线程
        }

        //准备关闭游戏界面窗口,所有变量赋为初始状态
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gameResult == 0)
                send("2001");
            gameStatus = false;
            gameStatus_Own = false;
            gameStatus_Opponent = false;
            colorSelected = 0;
            colorSelected_Own = 0;
            colorSelected_Opponent = 0;
            myTurn = false;
            myWuZiQi.init();
            listenerRun = false;
            thStop();          
        }

        //
        private void 标题_Click(object sender, EventArgs e)
        {

        }  

        ///------------------------------------------------------------------------------------------------------------------------------
    }
}