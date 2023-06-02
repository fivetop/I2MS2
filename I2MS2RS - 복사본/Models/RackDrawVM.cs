using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace I2MS2.Models
{
    public class RackDrawVM: INotifyPropertyChanged
    {
        public Point point { get; set; }
        public Size size { get; set; }
        public Double height { get; set; }

        public Color color { get; set; }
        public Double alpha { get; set; }
        public Double Z { get; set; }

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
