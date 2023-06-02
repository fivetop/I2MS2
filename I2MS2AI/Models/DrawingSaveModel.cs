using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2MS2.Models
{
    [Serializable]
    public class DrawingSaveModel
    {
        public DrawingSaveModel()
        {
            for(int i =0;i<4;i++)
            {
                w_list[i] = new List<WallDraw>();
                wc_list[i] = new List<WallCornerDraw>(); 
            }
        }

        public List<WallDraw>[] w_list = new List<WallDraw>[4];
        public List<WallCornerDraw>[] wc_list = new List<WallCornerDraw>[4];
    }
}
