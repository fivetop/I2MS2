using System;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Controls;
using System.Collections.Generic;


namespace I2MS2.Models
{
    public class ipp_list : INotifyPropertyChanged
    {
        public int asset_id { get; set; }
        public string asset_name { get; set; }
        public int catalog_id { get; set; }
        public int catalog_group_id { get; set; }
        public string floor_name { get; set; }
        public string room_name { get; set; }
        public string location_path { get; set; }
        public Color checked_color { get; set; }
        public TabItem ti { get; set; }
        public ListView lv { get; set; }
        public List<WorkCell> list { get; set; }
        public int num_of_ports { get; set; }
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