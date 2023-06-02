using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;

namespace I2MS2.Models
{
    public class CatalogGroupTree : INotifyPropertyChanged 
    {
        public CatalogGroupTree()
        {
            this.node_list = new List<CatalogGroupTree>();
        }

        public int catalog_group_id { get; set; }
        public string catalog_group_name { get; set; }
        public int disp_level { get; set; }
        public int image_id { get; set; }
        public string image_file_path { get; set; }
        public Visibility is_expander_visible { get; set; }
        public int level1_catalog_group_id { get; set; }
        public int level2_catalog_group_id { get; set; }
        public string fixed_mark { get; set; }
        public bool is_expanded { get; set; }

        public List<CatalogGroupTree> node_list { get; set; }

        public bool force_changed
        {
            get { return true; }
            set { NotifyPropertyChanged(""); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
