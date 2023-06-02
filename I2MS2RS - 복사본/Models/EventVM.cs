using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace I2MS2.Models
{
    public class EventVM : INotifyPropertyChanged
    {
        public string template { get; set; }    // button, data
        public string base_text { get; set; }   // Event (1)   <-- 건수 표시
        public int event_hist_id { get; set; }
        public DateTime write_time { get; set; }
        public eEventType event_type { get; set; }
        public string event_text { get; set; }
        public int location_id { get; set; }
        public int asset_id { get; set; }
        public int port_no { get; set; }
        public int catalog_id { get; set; }
        public int terminal_asset_id { get; set; }
        public string mac_addr { get; set; }
        public string ip_addr { get; set; }
        public int wo_id { get; set; }
        public bool is_confirm { get; set; }
        public int confirm_user_id { get; set; }

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
