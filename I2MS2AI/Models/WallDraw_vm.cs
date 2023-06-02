using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace I2MS2.Models
{
    public class WallDraw : INotifyPropertyChanged
    {
        public int layer { get; set; }
        public int number { get; set; }
        public Point st_p { get; set; }
        public Point end_p { get; set; }
        public Double thickness { get; set; }
        public Double height { get; set; }
        public Point select_p { get; set; }
       
        public byte colorA { get; set; }
        public byte colorR { get; set; }
        public byte colorG { get; set; }
        public byte colorB { get; set; }

        public Double alpha { get; set; }
        public Double Z { get; set; }

        public Point st_pA { get; set; }
        public Point st_pB { get; set; }
        public Point end_pA { get; set; }
        public Point end_pB { get; set; }

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
