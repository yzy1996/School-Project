using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using qi;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace wuziqi
{
   
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }
    }

    public class Sender
    {
        public void Send(string aInput)
        {
            string stream = aInput;
            try
            {
                TcpClient tcpc = new TcpClient(Form2.ip, 5656);
                //此处必须改为对方的ip地址
                NetworkStream tcpStream = tcpc.GetStream();
                StreamWriter reqStreamW = new StreamWriter(tcpStream);
                reqStreamW.Write(stream);
                reqStreamW.Flush();//发送信息
                tcpStream.Close();
                tcpc.Close();
            }
            catch (Exception e)
            {
            }
        }
    }
}
