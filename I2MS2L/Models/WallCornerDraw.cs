using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace I2MS2.Models
{
    [Serializable]
    public class WallCornerDraw 
    {
        public WallCornerDraw()
        {
            p_list = new List<Point>();
        }

        public int layer { get; set; }
        public int id { get; set; }

        public Double height { get; set; }
        public Double angle { get; set; }

        public List<Point> p_list { get; set; }

        public byte colorA { get; set; }
        public byte colorR { get; set; }
        public byte colorG { get; set; }
        public byte colorB { get; set; }

        public Double alpha { get; set; }
        public Double Z { get; set; }

        public int w1_id { get; set; } // 연결 벽의 아이디 2개 
        public int w2_id { get; set; }
    }
}
