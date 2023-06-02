using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using I2MS2.Models;
using System.ComponentModel;
using System.Diagnostics;
using I2MS2.Library;
using WebApi.Models;
using I2MS2.UserControls;
using System.Windows.Controls.Primitives;
using System.Collections;

namespace I2MS2.Windows
{
    /// <summary>
    /// RackView.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public class SWViewVM : INotifyPropertyChanged
    {
        #region SWViewVM 정의
        public int sw_asset_id { get; set; }
        public int port_no { get; set; }
        public string port_status { get; set; }
        public string cur_location_path { get; set; }
        public string cur_disp_name { get; set; }
        public string host_name { get; set; }
        public string ip_addr { get; set; }
        public string mac_addr { get; set; }
        public int front_asset_id { get; set; }
        public int front_port_no { get; set; }
        public string front_location_path { get; set; }
        public string front_disp_name { get; set; }

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
        #endregion
    }

    public partial class SWView : Window
    {
        List<SWViewVM> _list_sw_vm = new List<SWViewVM>();

        int _sw_asset_id = 0;
        int _num_of_ports = 0;
        int _sw_port_no = 0;

        public SWView(int sw_asset_id)
        {
            _sw_asset_id = sw_asset_id;
            InitializeComponent();
            dispFirst();
            initData();

            if (_num_of_ports >= 96)
            {
                ucSW144_880.MyItemsSource = _list_sw_vm;
                ucSW144_880.IsEnabled = true;
                ucSW144_880.Visibility = Visibility.Visible;

                //ucSW96_880.MyItemsSource = _list_sw_vm;
                //ucSW96_880.IsEnabled = true;
                //ucSW96_880.Visibility = Visibility.Visible;
            }
            else if (_num_of_ports >= 144)
            {
                ucSW144_880.MyItemsSource = _list_sw_vm;
                ucSW144_880.IsEnabled = true;
                ucSW144_880.Visibility = Visibility.Visible;
            }
            else if (_num_of_ports >= 48)
            {
                ucSW48_880.MyItemsSource = _list_sw_vm;
                ucSW48_880.IsEnabled = true;
                ucSW48_880.Visibility = Visibility.Visible;
            }
            else if (_num_of_ports >= 24)
            {
                ucSW24_880.MyItemsSource = _list_sw_vm;
                ucSW24_880.IsEnabled = true;
                ucSW24_880.Visibility = Visibility.Visible;
            }
        }

        private void initData()
        {
            var list = from sw in g.asset_port_link_list.Where(p => p.asset_id == _sw_asset_id) 
                     join a in g.asset_list on sw.asset_id equals a.asset_id
                     join l in g.location_list on a.location_id equals l.location_id
                     select new SWViewVM{
                          sw_asset_id = _sw_asset_id,
                          port_no = sw.port_no,
                          port_status = ((sw.front_asset_id ?? 0) != 0) ? "P" : "-",
                          cur_location_path = l.location_path,
                          cur_disp_name = string.Format("{0}/{1}", a.asset_name, sw.port_no),
                          front_asset_id = sw.front_asset_id ?? 0,
                          front_port_no = sw.front_port_no ?? 0,
                          mac_addr = getinfo(_sw_asset_id, sw.port_no, 1),
                          ip_addr = getinfo(_sw_asset_id, sw.port_no, 2),
                          host_name = getinfo(_sw_asset_id, sw.port_no, 3)
                     };

            if (list == null)
                return;

            _list_sw_vm = list.ToList();

            foreach (var node in _list_sw_vm)
            {
                var a1 = g.asset_list.Find(p => p.asset_id == node.front_asset_id);
                if (a1 != null)
                {
                    node.front_disp_name = string.Format("{0}/{1}", a1.asset_name, node.front_port_no);
                    var l1 = g.location_list.Find(p => p.location_id == a1.location_id);
                    if (l1 != null)
                        node.front_location_path = l1.location_path;
                }
                // Jake, ip 정보를 채워야 함.
            }
        }

        private string getinfo(int _sw_asset_id, int p1, int p2)
        {
            string ret ="";
            var a = g.asset_terminal_list.Find(p => p.cur_sw_asset_id == _sw_asset_id && p.cur_sw_port_no == p1);
            if (a == null)
                return ret;

            switch(p2)
            {
                case 1:
                    ret = a.mac;
                    break;
                case 2: 
                    var ati = g.asset_terminal_ip_list.Find(p => p.terminal_id == a.terminal_id);
                    if (ati != null)
                    {
                        ret = ati.ipv4;        // 대표 ip 하나만...
                    }
                    break;
                case 3: 
                    ret = a.cur_net_bios_name;
                    break;
            }
            return ret;
        }

        private void dispFirst()
        {
            var a = g.asset_list.Find(p => p.asset_id == _sw_asset_id);
            if (a == null)
                return;

            var c = g.catalog_list.Find(p => p.catalog_id == a.catalog_id);
            if (c == null)
                return;

            _txtAssetName.Text = a.asset_name;
            _txtCatalogName.Text = c.catalog_name;
            _num_of_ports = c.num_of_ports;
        }

        private void ucSW24_880_MySelectedChangeEvent(object obj)
        {
            int idx = (int)obj;

            if (idx >= 0)
            {
                SWViewVM vm = _list_sw_vm[idx];
                _gridDetail.DataContext = vm;

                // 링크다이어그램을 표시...
                _ucLink.Show(vm.sw_asset_id, vm.port_no);
                _sw_port_no = vm.port_no;
            }
        }

        private void ucSW48_880_MySelectedChangeEvent(object obj)
        {
            int idx = (int)obj;

            if (idx >= 0)
            {
                SWViewVM vm = _list_sw_vm[idx];
                _gridDetail.DataContext = vm;

                // 링크다이어그램을 표시...
                _ucLink.Show(vm.sw_asset_id, vm.port_no);
                _sw_port_no = vm.port_no;
            }
        }

        private void ucSW96_880_MySelectedChangeEvent(object obj)
        {
            int idx = (int)obj;

            if (idx >= 0)
            {
                SWViewVM vm = _list_sw_vm[idx];
                _gridDetail.DataContext = vm;

                // 링크다이어그램을 표시...
                _ucLink.Show(vm.sw_asset_id, vm.port_no);
                _sw_port_no = vm.port_no;
            }
        }

        private void ucSW144_880_MySelectedChangeEvent(object obj)
        {
            int idx = (int)obj;

            if (idx >= 0)
            {
                SWViewVM vm = _list_sw_vm[idx];
                _gridDetail.DataContext = vm;

                // 링크다이어그램을 표시...
                _ucLink.Show(vm.sw_asset_id, vm.port_no);
                _sw_port_no = vm.port_no;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var re = new request()
            {
                 type = eRequestType.eSwitchPort,
                 sw_asset_id = _sw_asset_id,
                 sw_port_no = _sw_port_no,
                on_off = true
            };
            var re1 = (request) await g.webapi.post("request", re, typeof(request));
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var re = new request()
            {
                type = eRequestType.eSwitchPort,
                sw_asset_id = _sw_asset_id,
                sw_port_no = _sw_port_no,
                on_off = false
            };
            var re1 = (request)await g.webapi.post("request", re, typeof(request));
        }
    }
}
                                                      