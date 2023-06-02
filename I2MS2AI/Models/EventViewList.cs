using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media;

namespace I2MS2.Models
{
    public class EventViewList : INotifyPropertyChanged 
    {
        public EventViewList()
        {
            this.node_list = new List<EventViewList>();
        }
        public List<EventViewList> node_list { get; set; }

        public int event_hist_id { get; set; }
        public DateTime write_time { get; set; }
        //private byte[] _write_time;
        //public byte[] write_time{
        //    get
        //    {
        //        return _write_time;
        //    }
        //    set
        //    {
        //        _write_time = value;
        //        write_time_s = ConvertFromTimestamp(get_timestamp(_write_time)).ToString();
        //    }
        //}

        private string _event_type;
        public string event_type
        {
            get { return _event_type; }
            set 
            {
                _event_type = value;
                switch(_event_type)
                {
                    case "I":
                    {
                        event_type_c = "Transparent";
                        event_type_s = "Information"; break;
                    }
                    case "E": 
                    {
                        event_type_c = "LightYellow";
                        event_type_s = "Error"; break;
                    }
                    case "W": 
                    {
                        event_type_c = "Transparent";
                        event_type_s = "Warning"; break;
                    }
                }
            } 
        }
        public string event_type_c { get; set; }
        public string event_type_s { get; set; }
        public int event_id { get; set; }
        public string event_text { get; set; }
        public Nullable<int> location_id { get; set; }
        public Nullable<int> catalog_group_id { get; set; }
        public Nullable<int> catalog_id { get; set; }
        public Nullable<int> asset_id { get; set; }
        public Nullable<int> port_no { get; set; }
        public Nullable<int> terminal_asset_id { get; set; }
        public string mac { get; set; }
        public string ipv4 { get; set; }
        public Nullable<int> user_id { get; set; }
        public string is_confirm { get; set; }
        public Nullable<int> confirm_user_id { get; set; }
        public Color checked_color { get; set; }
        public string write_time_s { get; set; }

        private double get_timestamp(byte[] bytes)
        {
            double result = 0;
            double inter = 0;
            for (int i = 0; i < bytes.Length; i++)
            {

                inter = System.Convert.ToDouble(bytes[i]);
                inter = inter * Math.Pow(2, ((7 - i) * 8));
                result += inter;
            }
            return result;
        }

        private DateTime ConvertFromTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


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
