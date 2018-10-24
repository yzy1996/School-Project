using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace qi
{
    /// <summary>
    /// 变量说明
    /// statusOfPoint    枚举类型      棋盘上点的状态
    /// wuziqiBase        类                五子棋基类
    /// recorder            栈                记录棋局进程
    /// myStatusOfPoint
    /// </summary>
    public enum StatusOfColor    //棋盘上点的状态，分为黑，白以及没有棋子,共三种
    {
        Empty=0,
        Black=1,
        White=2
    }
    public class wuziqiBase    //五子棋基类
    {
        public Stack<Point> recorder = new Stack<Point>();    //定义栈,记录棋局进程
        public StatusOfColor[,] statusOfPoint = new StatusOfColor[15, 15];    //定义枚举类型的数组,描述棋盘上每个点的状态
        public wuziqiBase()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    statusOfPoint[i, j] = StatusOfColor.Empty;
                }
            }
        }
        public void init()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    statusOfPoint[i, j] = StatusOfColor.Empty;
                }
            }
        }
        /// <summary>
        /// 参量x y 为在刚下完是鼠标所下的那个棋子的坐标
        /// 坐标按照原点在左上角记录 下为y轴正方向 右为x轴正方向
        /// </summary>
        /// <param name="x">指最近一次的棋子的横坐标</param>
        /// <param name="y">指最近一次的棋子的纵坐标</param>
        /// <returns></returns>
        public bool Judge(int x, int y)
        {
            StatusOfColor flag = statusOfPoint[x, y];
            int count_heng = -1;
            int count_shu = -1;
            int count_xie_zuo = -1;
            int count_xie_rou = -1;
            //计算横排的数目
            //先左方的计数
            int X = x;
            int Y = y;
            while (statusOfPoint[X, Y] == flag)
            {
                count_heng++;
                if (X > 0)
                    X--;
                else
                    break;

            }
            X = x;
            while (statusOfPoint[X, Y] == flag)
            {
                count_heng++;
                if (X < 14)
                    X++;
                else
                    break;

            }
            //计算竖排的数目
            X = x;
            Y = y;
            while (statusOfPoint[X, Y] == flag)
            {
                count_shu++;
                if (Y > 0)
                    Y--;
                else
                    break;

            }
            Y = y;
            while (statusOfPoint[X, Y] == flag)
            {
                count_shu++;
                if (Y < 14)
                    Y++;
                else
                    break;

            }
            //计算斜项的数目  \  方向
            X = x;
            Y = y;
            while (statusOfPoint[X, Y] == flag)
            {
                count_xie_zuo++;
                if (Y > 0 && X > 0)
                {
                    Y--;
                    X--;
                }
                else
                    break;

            }
            X = x;
            Y = y;
            while (statusOfPoint[X, Y] == flag)
            {
                count_xie_zuo++;
                if (Y < 14 && X < 14)
                {
                    Y++;
                    X++;
                }
                else
                    break;

            }
            //计算斜向数目  / 方向
            X = x;
            Y = y;
            while (statusOfPoint[X, Y] == flag)
            {
                count_xie_rou++;
                if (Y > 0 && X < 14)
                {
                    Y--;
                    X++;
                }
                else
                    break;

            }
            X = x;
            Y = y;
            while (statusOfPoint[X, Y] == flag)
            {
                count_xie_rou++;
                if (Y < 14 && X > 0)
                {
                    Y++;
                    X--;
                }
                else
                    break;

            }
            if (count_heng >= 5 || count_shu >= 5 || count_xie_rou >= 5 || count_xie_zuo >= 5)
                return true;
            else
                return false;
        }
         
    }
}
