using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace qi
{
    public enum qiq
    {
        Empty=0,
        Black=1,
        White=2
    }
    public class qi1
    {
        public qiq[,] five = new qiq[15, 15];
        public Stack<Point> stFive = new Stack<Point>();
        public qi1()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    five[i, j] = qiq.Empty;
                }
            }
        }
        public bool Judge()
        {
            return false;
        }
    }
}
