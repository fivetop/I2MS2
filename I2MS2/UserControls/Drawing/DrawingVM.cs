using I2MS2.Library;
using I2MS2.Library.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2MS2.UserControls.Drawing
{
    public class DrawingVM
    {
        public int room_id { get; set; }
        public int floor_id { get; set; }
        public string room_name { get; set; }
        public Nullable<int> square_x1 { get; set; }
        public Nullable<int> square_y1 { get; set; }
        public Nullable<int> square_x2 { get; set; }
        public Nullable<int> square_y2 { get; set; }

        public int rack_id { get; set; }
        public string rack_name { get; set; }

        public int asset_id { get; set; }
        public int catalog_id { get; set; }
        public int location_id { get; set; }
        public string asset_name { get; set; }
        public string is_layout { get; set; }

        public int user_port_layout_id { get; set; }
        public int port_no { get; set; }

        public int type_id { get; set; }
        public AssetTreeType type { get; set; }

        public Nullable<int> pos_x { get; set; }
        public Nullable<int> pos_y { get; set; }
        public Boolean is_changed { get; set; }

        public int State { get; set; }
    }
}
