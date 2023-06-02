#define ARRAY_TN
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2MS2.Models
{
    public class tree_node
    {
#if ARRAY_TN
        public tree_node()
        {
            node_id = new int[8];
        }

        //자신의 id를 primary key로 잡고
        //자신 보다 아래의 노드는 -1로 초기화 한다
        public int id { get; set; }
        //public List<int> node_id { get; set; }
        public int[] node_id; 
#else
        public tree_node()
        {
            childlist = new List<tree_node>();
        }

        public int id { get; set; }
        public int level { get; set; }
        public int node_id { get; set; }

        public List<tree_node> childlist { get; set; }
#endif
    }
}
