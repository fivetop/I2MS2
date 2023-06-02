using System;
using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;

namespace I2MS2.Models
{
    public class UserTree : INotifyPropertyChanged 
    {
        public UserTree()
        {
            this.node_list = new List<UserTree>();
        }

        public int user_id { get; set; }
        public string user_group { get; set; }
        public string user_name { get; set; }
        public string login_id { get; set; }
        public string login_password { get; set; }
        public string login_password2 { get; set; }
        public string email { get; set; }
        public string use_email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string use_sms { get; set; }
        public int reg_user_id { get; set; }
        public byte[] last_updated { get; set; }
        public string deletable { get; set; }
        public string remarks { get; set; }
        public DateTime last_updated2 { get; set; }

        public List<UserTree> node_list { get; set; }

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
