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
using WebApi.Models;
using I2MS2.Models;
using I2MS2.Library;
using WebApiClient;
using System.ComponentModel;

namespace I2MS2.Windows
{
    /// <summary>
    /// ManufactureManager.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    
    public class TerminalVM : asset_terminal, INotifyPropertyChanged
    {
        public string ip_addr { set; get; }
        public string cur_outlet_asset_name { set; get; }
        public int location_id { set; get; }
        public string location_name { set; get; }
        public string location_path { set; get; }

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
    public partial class TerminalView : Window
    {
        List<NetworkVM> _network_list = null;
        NetworkVM _network = new NetworkVM();
        int _selected_net_id = 0;

        //List<TerminalVM> _terminal_list = null;
        TerminalVM _terminal = new TerminalVM();
        //int _selected_terminal_id = 0;

        public TerminalView()
        {
            InitializeComponent();
            initListView();

            _lvNetwork.SelectedItem = 0;
        }

        private void _lvNetwork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NetworkVM vm = (NetworkVM)_lvNetwork.SelectedItem;

            if (vm == null)
            {
                _selected_net_id = 0;
                return;
            }
            _selected_net_id = vm.net_id;
            dispDetail(_selected_net_id);
        }

        // 좌측 창에 내용을 출력한다.
        private void initListView()
        {
            var list = from aa in g.net_scan_list
                       select new NetworkVM()
                       {
                           net_id = aa.net_id,
                           net_name = aa.net_name,
                           net_addr = aa.net_addr,
                           subnet = aa.subnet,
                           start_ipv4 = aa.start_ipv4,
                           end_ipv4 = aa.end_ipv4,
                           user_id = aa.user_id,
                           remarks = aa.remarks,
                       };
            _network_list = list.ToList();
                                    
            _lvNetwork.ItemsSource = _network_list;
        }



        // 음수인경우 마지막을 선택
        private void refreshData(int idx)
        {
            _lvNetwork.ItemsSource = null;
            _lvNetwork.ItemsSource = _network_list;
            int idx2 = idx;
            if (idx < 0)
               idx2 = _lvNetwork.Items.Count - 1;

            _lvNetwork.SelectedIndex = idx2;
        }

        private void dispDetail(int net_id)
        {
            var list = from aa in g.asset_terminal_list.Where(p => p.net_id == net_id)
                       select new TerminalVM()
                       {
                           terminal_id = aa.terminal_id,
                           terminal_asset_id = aa.terminal_asset_id,
                           mac = aa.mac,
                           cur_net_bios_name = aa.cur_net_bios_name,
                           cur_sw_asset_id = aa.cur_sw_asset_id,
                           cur_outlet_asset_id = aa.cur_outlet_asset_id,
                           cur_sw_port_no = aa.cur_sw_port_no,
                           cur_outlet_port_no = aa.cur_outlet_port_no,
                           last_activated = aa.last_activated,
                       };
            var list2 = list.ToList();

            foreach(var node in list2)
            {
                var ati = g.asset_terminal_ip_list.Find(p => p.terminal_id == node.terminal_id);
                if (ati != null)
                {
                    node.ip_addr = ati.ipv4;        // 대표 ip 하나만...
                }

                var a = g.asset_list.Find(p => p.asset_id == node.cur_outlet_asset_id);
                if (a != null)
                {
                    node.cur_outlet_asset_name = a.asset_name;
                    node.location_id = a.location_id;
                    var l = g.location_list.Find(p => p.location_id == a.location_id);
                    if (l != null)
                    {
                        node.location_name = l.location_name;
                        node.location_path = l.location_path;
                    }
                }
            }
            _lvTerminal.ItemsSource = null;
            _lvTerminal.ItemsSource = list2;
        }

        private async void _btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // 수동검색
            NetworkVM vm = (NetworkVM)_lvNetwork.SelectedItem;
            if (vm == null)
                return;
            int net_id = vm.net_id;
            request req = new request();
            req.ManualNetScan(net_id);
            var r = await g.webapi.post("request", req, typeof(request));
            if (r == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Progressing_Auto_Scan"));
                return;
            }
            MessageBox.Show("Regist Network Switch Scan : " + vm.net_name);
        }
    }
}
