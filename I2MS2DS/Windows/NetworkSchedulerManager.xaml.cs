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
    
    public class NetworkVM : net_scan, INotifyPropertyChanged
    {
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

    // 2015.09.03 romee  
    // 터미날 가져오기 모듈 -> 넷 스캔 등록 과 검색 결과 추가 


    public partial class NetworkSchedulerManager : Window
    {
        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();
        public static RoutedCommand SelectCommand = new RoutedCommand();
        public static RoutedCommand Select2Command = new RoutedCommand();
        public static RoutedCommand SearchCommand = new RoutedCommand();
        public static RoutedCommand GrantCommand = new RoutedCommand();
        public static RoutedCommand SWonCommand = new RoutedCommand();
        public static RoutedCommand SWoffCommand = new RoutedCommand();

        private bool _new = true;
        private bool _edit = false;
        private bool _delete = false;
        private bool _save = false;
        private bool _cancel = false;
        private bool _select = false;
        private bool _select2 = false;
        private bool _search = false;
        private bool _grant = false;
        private bool _swon = false;
        private bool _swoff = false;

        List<NetworkVM> _network_list = null;
        NetworkVM _network = new NetworkVM();
        private List<SwitchSelectorVM> _switch_list = new List<SwitchSelectorVM>();
        private List<SwitchSelectorVM> _switch_list2 = new List<SwitchSelectorVM>();
        int _selected_net_id = 0;
        int _l3_sw_asset_id = 0;

        TerminalVM _terminal = new TerminalVM();


        public NetworkSchedulerManager()
        {
            InitializeComponent();
            initSchedule();
            initListView();

            getSwitch(0);
            _lvNetwork.SelectedItem = 0;
        }

        #region // Command process 
        private void _cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _new;
        }

        private void _cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _new = false;
            _edit = false;
            _save = true;
            _delete = false;
            _cancel = true;
            _select = true;
            _select2 = true;
            _search = false;
            _selected_net_id = 0;

            enableControl(true);

            txtNetName.Clear();
            txtNetAddr.Clear();
            txtSubnet.Clear();
            txtStartIP.Clear();
            txtEndIP.Clear();
            txtSwList.Clear();
            txtGateway.Clear();
            txtRemarks.Clear();

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            bool flag = _save && !_edit;
            enableControl(flag);
            e.CanExecute = _edit;
        }

        private void enableControl(bool flag)
        {
            txtNetName.IsEnabled = flag;
            txtNetAddr.IsEnabled = flag;
            txtSubnet.IsEnabled = flag;
            txtStartIP.IsEnabled = flag;
            txtEndIP.IsEnabled = flag;
            txtRemarks.IsEnabled = flag;
        }

        private void _cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_selected_net_id == 0)
                return;

            _new = false;
            _edit = false;
            _save = true;
            _delete = false;
            _cancel = true;
            _select = true;
            _select2 = true;
            _search = false;

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _delete;
        }

        private async void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_selected_net_id == 0)
                return;

            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            if (!await deleteNetwork())
                return;
            
            _new = true;
            _edit = false;
            _delete = false;
            _save = false;
            _cancel = false;
            _select = false;
            _select2 = false;
            _search = false;

            refreshData(-1);

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _save;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveData())
                return;

            _new = true;
            _save = false;
            _edit = true;
            _delete = true;
            _cancel = false;
            _select = false;
            _select2 = false;
            _search = false;

            refreshData(_lvNetwork.SelectedIndex);

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdCancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _cancel;
        }

        private void _cmdCancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _new = true;
            _save = false;
            _edit = true;
            _delete = true;
            _cancel = false;
            _select = false;
            _select2 = false;
            _search = false;

            refreshData(_lvNetwork.SelectedIndex);

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdSelect_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _select;
        }

        private void _cmdSelect_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 신규 버튼을 누른 경우 리스트를 지우고 들어간다.
            if (_selected_net_id == 0)
            {
                foreach (var node in _switch_list)
                {
                    node.is_select = false;
                }
            }

            SwitchSelector window = new SwitchSelector(_switch_list);
            window.Owner = this;
            bool b = window.ShowDialog() ?? false;

            if (b)
                dispSwitch();

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdSelect2_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _select2;
        }

        private void _cmdSelect2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var list = _switch_list2;
            //            if (_selected_net_id == 0)  // romee 2015.08.13 의미 없음 무조건 클리어 처리
            //           {
            foreach (var node in list)
            {
                node.is_select = false;
            }
            //           }

            SwitchSelector2 window = new SwitchSelector2(list);
            window.Owner = this;
            bool b = window.ShowDialog() ?? false;

            if (b)
            {
                foreach (var node in _switch_list2)
                {
                    if (node.asset_id == window._vm.asset_id)
                        node.is_select = true;  // ROMEE 2017.07.27   스위치 리스트에 저장 
                }
                dispSwitch2();
            }

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdSearch_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _search;
        }

        private async void _cmdSearch_Executed(object sender, ExecutedRoutedEventArgs e)
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
                // 기존 스캔 내용 뿌려주기 
                dispDetail(net_id);
                return;
            }
            MessageBox.Show("Regist Network Switch Scan : " + vm.net_name);
            dispDetail(net_id);

            _new = true;
            _edit = false;
            _delete = false;
            _save = false;
            _cancel = false;
            _select = false;
            _search = false;

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion

        #region // Command process

        private void _cmdGrant_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            TerminalVM vm = (TerminalVM)_lvTerminal.SelectedItem;
            if (vm == null)
                _grant = false;
            else
                _grant = true;

            e.CanExecute = _grant;
        }

        private async void _cmdGrant_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 수동검색
            TerminalVM vm = (TerminalVM)_lvTerminal.SelectedItem;
            if (vm == null)
                return;
            int net_id = vm.net_id;

            bool b = MessageBox.Show(g.tr_get("C_Grant_Ask"), g.tr_get("C_Grant"), MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (!b)
                return;

            var r = await saveGrant();
            _grant = false;
            CommandManager.InvalidateRequerySuggested();
        }


        private void _cmdSWon_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            TerminalVM vm = (TerminalVM)_lvTerminal.SelectedItem;
            if (vm == null)
                _swon = false;
            else
                _swon = true;

            e.CanExecute = _swon;
        }

        private async void _cmdSWon_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TerminalVM vm = (TerminalVM)_lvTerminal.SelectedItem;
            if (vm == null)
                return;
            int net_id = vm.net_id;
            int sw_asset_id = vm.cur_sw_asset_id ?? 0;
            int sw_asset_port = vm.cur_sw_port_no ?? 0;

            if (sw_asset_id == 0) return;
            var re = new request()
            {
                type = eRequestType.eSwitchPort,
                sw_asset_id = sw_asset_id,
                sw_port_no = sw_asset_port,
                on_off = true
            };
            var re1 = (request)await g.webapi.post("request", re, typeof(request));

            _swon = false;
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdSWoff_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            TerminalVM vm = (TerminalVM)_lvTerminal.SelectedItem;
            if (vm == null)
                _swoff = false;
            else
                _swoff = true;

            e.CanExecute = _swoff;
        }

        private async void _cmdSWoff_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // 수동검색
            TerminalVM vm = (TerminalVM)_lvTerminal.SelectedItem;
            if (vm == null)
                return;
            int net_id = vm.net_id;
            int sw_asset_id = vm.cur_sw_asset_id ?? 0;
            int sw_asset_port = vm.cur_sw_port_no ?? 0;

            if (sw_asset_id == 0) return;
            var re = new request()
            {
                type = eRequestType.eSwitchPort,
                sw_asset_id = sw_asset_id,
                sw_port_no = sw_asset_port,
                on_off = false
            };
            var re1 = (request)await g.webapi.post("request", re, typeof(request));

            _swoff = false;
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion

        #region // 리스트 1 스위치 데이터 보이기 처리 
        private void getSwitch(int net_id)
        {
            var sw_list = g.net_scan_sw_list.Where(p => p.net_id == net_id);
            var list = from aa in Etc.get_asset_tree_vm_ex_list(0, true, 0, true).Where(p => CatalogType.is_sw(p.catalog_id))
                       select new SwitchSelectorVM()
                       {
                           asset_id = aa.asset_id,
                           catalog_id = aa.catalog_id,
                           disp_name = aa.disp_name,
                           site_name = aa.site_name,
                           building_name = aa.building_name,
                           floor_name = aa.floor_name,
                           room_name = aa.room_name,
                           rack_name = aa.rack_name,
                           is_select = sw_list.Count(p => p.sw_asset_id == aa.asset_id) > 0
                       };
            _switch_list = list.ToList();

            var ns = g.net_scan_list.Find(p => p.net_id == net_id);
            _l3_sw_asset_id = ns != null ? ns.l3_sw_asset_id ?? 0 : 0;
            var list2 = from aa in Etc.get_asset_tree_vm_ex_list(0, true, 0, true).Where(p => CatalogType.is_sw_l3(p.catalog_id))
                       select new SwitchSelectorVM()
                       {
                           asset_id = aa.asset_id,
                           catalog_id = aa.catalog_id,
                           disp_name = aa.disp_name,
                           site_name = aa.site_name,
                           building_name = aa.building_name,
                           floor_name = aa.floor_name,
                           room_name = aa.room_name,
                           rack_name = aa.rack_name,
                           is_select = _l3_sw_asset_id == aa.asset_id 
                       };

            _switch_list2 = list2.ToList();
        }

        private void dispSwitch()
        {
            // 선택 부분만...
            var list2 = _switch_list.Where(p => p.is_select).ToList();
            string sw_list_string = "";
            int cnt = 0;
            foreach (SwitchSelectorVM node in list2)
            {
                sw_list_string = sw_list_string + node.disp_name;
                if (++cnt != list2.Count)
                    sw_list_string += ", ";
            }
            txtSwList.Text = sw_list_string;
        }

        private void dispSwitch2()
        {
            // 선택 부분만...
            var node = _switch_list2.Find(p => p.is_select);
            string sw_name = "";
            if (node != null)
            {
                sw_name = node.disp_name;
                _l3_sw_asset_id = node.asset_id ?? 0;
            }
            else
                _l3_sw_asset_id = 0;
            txtGateway.Text = sw_name;
        }

        // 좌측 리스트 창에 내용을 출력한다.
        private void initListView()
        {
            var list = from aa in g.net_scan_list
                       select new NetworkVM()
                       {
                           net_id = aa.net_id,
                           net_name = aa.net_name,
                           net_addr = aa.net_addr,
                           subnet = aa.subnet,
                           l3_sw_asset_id = aa.l3_sw_asset_id,
                           start_ipv4 = aa.start_ipv4,
                           end_ipv4 = aa.end_ipv4,
                           user_id = aa.user_id,
                           remarks = aa.remarks,
                       };
            _network_list = list.ToList();

            _lvNetwork.ItemsSource = _network_list;
        }

        #endregion

        #region // 스위치 선택시 처리 

        private void _lvNetwork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _new = true;
            _edit = true;
            _delete = true;
            _save = false;
            _cancel = false;
            _select = false;
            _search = true;

            NetworkVM vm = (NetworkVM)_lvNetwork.SelectedItem;

            if (vm == null)
            {
                _selected_net_id = 0;
                return;
            }

            _selected_net_id = vm.net_id;
            getSwitch(vm.net_id);
            _network = new NetworkVM();
            _network.net_id = vm.net_id;
            _network.net_name = vm.net_name;
            _network.net_addr = vm.net_addr;
            _network.subnet = vm.subnet;
            _network.l3_sw_asset_id = vm.l3_sw_asset_id;
            _network.start_ipv4 = vm.start_ipv4;
            _network.end_ipv4 = vm.end_ipv4;
            _network.remarks = vm.remarks;
            dispDetail();
        }

        
        #endregion

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

            // 터미날 출력 하기 
            dispDetail(net_id);
        }



        #region // 터미날 출력 처리
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
                           last_activated = aa.last_activated
#if I2MS_V21
                           ,terminal_grant = aa.terminal_grant == 1 ? "Authorized" : "Unauthorized"
#endif
                       };
            var list2 = list.ToList();

            foreach (var node in list2)
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
        #endregion


        #region // 스케쥴러 처리 사용안함
        // 스케쥴러 내용을 출력한다.
        private void initSchedule()
        {
            if (g.net_scan_scheduler_list.Count == 0)
                return;
            var nss = g.net_scan_scheduler_list[0];
            txtStartTime.Text = nss.schedule_time;
            txtRepeatMinute.Text = nss.repeat_minute.ToString();
            rdoPattern2.IsChecked = (nss.enabled == "Y") && (nss.repeat_pattern == "E");    // Everyday
            rdoPattern3.IsChecked = (nss.enabled == "Y") && (nss.repeat_pattern == "S");    // Selecteed Day
            rdoPattern1.IsChecked = nss.enabled != "Y";
            chkDay0.IsChecked = nss.repeat_day0 == "Y";
            chkDay1.IsChecked = nss.repeat_day1 == "Y";
            chkDay2.IsChecked = nss.repeat_day2 == "Y";
            chkDay3.IsChecked = nss.repeat_day3 == "Y";
            chkDay4.IsChecked = nss.repeat_day4 == "Y";
            chkDay5.IsChecked = nss.repeat_day5 == "Y";
            chkDay6.IsChecked = nss.repeat_day6 == "Y";
            enableScheduleControl();
        }

        private void enableScheduleControl()
        {
            if (txtRepeatMinute == null)
                return;
            txtRepeatMinute.IsEnabled = rdoPattern1.IsChecked != true;
            txtStartTime.IsEnabled = rdoPattern1.IsChecked != true;
            bool enabled = rdoPattern3.IsChecked == true;
            chkDay0.IsEnabled = enabled;
            chkDay1.IsEnabled = enabled;
            chkDay2.IsEnabled = enabled;
            chkDay3.IsEnabled = enabled;
            chkDay4.IsEnabled = enabled;
            chkDay5.IsEnabled = enabled;
            chkDay6.IsEnabled = enabled;
        }
        #endregion


        #region // add. update, delete
        private async Task<bool> saveData()
        {
            net_scan n = null;
            if (_selected_net_id == 0)
            {
                // 신규일때 저장
                n = new net_scan();
            }
            else
            {
                n = g.net_scan_list.Find(p => p.net_id == _network.net_id);
                if (n == null)
                    return false;
            }

            n.net_name = txtNetName.Text.Trim();
            n.net_addr = txtNetAddr.Text.Trim();
            n.subnet = txtSubnet.Text.Trim();
            n.l3_sw_asset_id = _l3_sw_asset_id;
            n.start_ipv4 = txtStartIP.Text.Trim();
            n.end_ipv4 = txtEndIP.Text.Trim();
            n.user_id = g.login_user_id;
            n.remarks = txtRemarks.Text.Trim();
            n.last_updated = DateTime.Now;
            n.net_scan_sw = null;

            if (_selected_net_id == 0)
            {
                var nn = (net_scan) await g.webapi.post("net_scan", n, typeof(net_scan));
                if (nn == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
//                g.net_scan_list.Add(nn);  // romee 2015.4.27 이중등록 수정  
                NetworkVM new_vm2 = new NetworkVM()
                {
                    net_id = nn.net_id,
                    net_name = nn.net_name,
                    subnet = nn.subnet,
                    l3_sw_asset_id = _l3_sw_asset_id,
                    start_ipv4 = nn.start_ipv4,
                    end_ipv4 = nn.end_ipv4,
                    net_addr = nn.net_addr,
                    last_updated = nn.last_updated,
                    remarks = nn.remarks,
                    user_id = nn.user_id
                };

                _network_list.Add(new_vm2);
                g.net_scan_list.Add(nn);
                bool b = await addSwitch(nn.net_id);
                if (!b)
                    return false;
                _lvNetwork.ItemsSource = null;
                _lvNetwork.ItemsSource = _network_list;
                _lvNetwork.SelectedValue = nn.net_id;
                getSwitch(nn.net_id);
            }
            else
            {
                int r = await g.webapi.put("net_scan", n.net_id, n, typeof(net_scan));
                if (r != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                var nn2 = _network_list.Find(p => p.net_id == n.net_id);
                if (nn2 == null)
                    return false;
                _network = nn2;
                _network.net_name = n.net_name;
                _network.net_addr = n.net_addr;
                _network.subnet = n.subnet;
                _network.l3_sw_asset_id = n.l3_sw_asset_id;
                _network.start_ipv4 = n.start_ipv4;
                _network.end_ipv4 = n.end_ipv4;
                _network.remarks = n.remarks;
                _network.user_id = n.user_id;
                _network.last_updated = n.last_updated;
                bool b2 = await addSwitch(n.net_id);
                if (!b2)
                    return false;
                _network.force_changed = true;
                getSwitch(n.net_id);
            }

            return true;
        }


        private async Task<bool> addSwitch(int net_id)
        {
            // 먼저 관련 스위치를 모두 지우고..
            bool b = await delete_net_scan_sw(net_id);
            if (!b)
                return false;

            // 선택 부분만 추가...
            var list2 = _switch_list.Where(p => p.is_select).ToList();

            foreach(var node in list2)
            {
                net_scan_sw nss = new net_scan_sw()
                {
                    net_id = net_id,
                    sw_asset_id = node.asset_id ?? 0,
                };
                var nn = (net_scan_sw) await g.webapi.post("net_scan_sw", nss, typeof(net_scan_sw));
                if (nn == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                nss.net_scan_sw_id = nn.net_scan_sw_id;
                g.net_scan_sw_list.Add(nss);
            }
            return true;
        }

        private async Task<bool> delete_net_scan_sw(int net_id)
        {
            var list = g.net_scan_sw_list.Where(p => p.net_id == net_id);
            foreach(var node in list)
            {
                int net_scan_sw_id = node.net_scan_sw_id;
                if (net_scan_sw_id > 0)
                {
                    int rr1 = await g.webapi.delete("net_scan_sw", net_scan_sw_id);
                    if (rr1 != 0)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return false;
                    }
                }
            }
            g.net_scan_sw_list.RemoveAll(p => p.net_id == net_id);
            return true;
        }

        private async Task<bool> deleteNetwork()
        {
            int net_id = _selected_net_id;
            bool b = await delete_net_scan_sw(net_id);
            if (!b)
                return false;

            int rr2 = await g.webapi.delete("net_scan", net_id);
            if (rr2 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            g.net_scan_list.RemoveAll(p => p.net_id == net_id);
            _network_list.RemoveAll(p => p.net_id == net_id);

            return true;
        }

        private async Task<bool> saveGrant()
        {
            TerminalVM vm = (TerminalVM)_lvTerminal.SelectedItem;
            if (vm == null)
                return false;
            int net_id = vm.terminal_id;

            asset_terminal ast_t = g.asset_terminal_list.Find(at => at.terminal_id == net_id);
            if (ast_t == null)
                return false;

#if I2MS_V21
            if(ast_t.terminal_grant == 1) ast_t.terminal_grant = 0;
            else ast_t.terminal_grant = 1; 
#endif
            int r = await g.webapi.put("asset_terminal", ast_t.terminal_id, ast_t, typeof(asset_terminal));
            if (r != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }
            dispDetail(ast_t.net_id);

            return true;
        }

        #endregion

        #region // Util
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

        private void dispDetail()
        {
            if (_network == null)
            {
                clearControl();
                return;
            }

            if (_selected_net_id == 0)
            {
                clearControl();
                return;
            }

            txtNetName.Text = _network.net_name;
            txtNetAddr.Text = _network.net_addr;
            txtSubnet.Text = _network.subnet;
            txtGateway.Text = Etc.get_asset_name(_network.l3_sw_asset_id ?? 0);
            txtStartIP.Text = _network.start_ipv4;
            txtEndIP.Text = _network.end_ipv4;
            txtRemarks.Text = _network.remarks;
            dispSwitch();
        }

        private void clearControl()
        {
            txtNetName.Clear();
            txtNetAddr.Clear();
            txtSubnet.Clear();
            txtGateway.Clear();
            txtStartIP.Clear();
            txtEndIP.Clear();
            txtRemarks.Clear();
            txtSwList.Clear();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // 스케쥴 저장
            if (g.net_scan_scheduler_list.Count == 0)
                return;
            var nss = g.net_scan_scheduler_list[0];
            nss.schedule_time = txtStartTime.Text.Trim();
            nss.repeat_minute = Etc.get_int(txtRepeatMinute.Text); 
            nss.enabled = rdoPattern1.IsChecked != true ? "Y" : "N";
            nss.repeat_pattern = "-";
            if (rdoPattern2.IsChecked == true)
                nss.repeat_pattern = "E";       // Everyday
            if (rdoPattern3.IsChecked == true)
                nss.repeat_pattern = "S";       // Selecteed Day
            nss.repeat_day0 = chkDay0.IsChecked == true ? "Y" : "-";
            nss.repeat_day1 = chkDay1.IsChecked == true ? "Y" : "-";
            nss.repeat_day2 = chkDay2.IsChecked == true ? "Y" : "-";
            nss.repeat_day3 = chkDay3.IsChecked == true ? "Y" : "-";
            nss.repeat_day4 = chkDay4.IsChecked == true ? "Y" : "-";
            nss.repeat_day5 = chkDay5.IsChecked == true ? "Y" : "-";
            nss.repeat_day6 = chkDay6.IsChecked == true ? "Y" : "-";
            nss.last_updated = DateTime.Now;
            var r1 = await g.webapi.put("net_scan_scheduler", nss.net_scan_scheduler_id, nss, typeof(net_scan_scheduler));
            if (r1 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return;
            }
            return;
        }

        private void rdoPattern1_Checked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        private void rdoPattern1_Unchecked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        private void rdoPattern2_Checked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        private void rdoPattern2_Unchecked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        private void rdoPattern3_Checked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        private void rdoPattern3_Unchecked(object sender, RoutedEventArgs e)
        {
            enableScheduleControl();
        }
        #endregion
    }
}
