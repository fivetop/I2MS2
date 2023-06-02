using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace I2MS2.Models
{
    public class asset_tree_for_treeview
    {
        public asset_tree_for_treeview()
        {
            this.child_list = new List<asset_tree_for_treeview>();
        }

        public int asset_tree_id { get; set; }
        public string disp_name { get; set; }
        public int disp_level { get; set; }
        public string is_alarm { get; set; }
        public int image_id { get; set; }
        public Nullable<int> asset_id { get; set; }
        public int location_id { get; set; }
        public Nullable<int> prev_asset_tree_id { get; set; }
        public Nullable<int> next_asset_tree_id { get; set; }
        public string image_file_path { get; set; }
        public System.Windows.Visibility is_expander_visible { get; set; }
        public int parent_tree_id { get; set; }
        //public Thickness margin { get; set; }

        public List<asset_tree_for_treeview> child_list { get; set; }
    }
}
