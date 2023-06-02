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
using System.Threading;
using System.Windows.Threading;

namespace I2MS2.Windows
{
    /// <summary>
    /// RackView.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public class PPViewVM : INotifyPropertyChanged
    {
        #region PPViewVM 정의
        public int ipp_asset_id { get; set; }
        public int port_no { get; set; }
        public bool is_rear_cable { get; set; }
        public string alarm_status { get; set; }
        public string wo_status { get; set; }
        public string is_port_trace { get; set; }
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
        public int rear_asset_id { get; set; }
        public int rear_port_no { get; set; }
        public string rear_location_path { get; set; }
        public string rear_disp_name { get; set; }

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

    public partial class PPView : Window
    {
        List<PPViewVM> _list_pp_vm = new List<PPViewVM>();
        int _ipp_asset_id = 0;
        int _port_no = 0;

        public PPView(int ipp_asset_id)
        {
            _ipp_asset_id = ipp_asset_id;
            InitializeComponent();

            _btnPortTrace.DataContext = new TopMenuButtonData() { name = "center", img = "/I2MS2;component/Icons/TopMenuIcon7_1.png", pressed_img = "/I2MS2;component/Icons/TopMenuIcon7_2.png" };

            dispFirst();
            initData();
        }

        private void initData()
        {   
            var list = from pp in g.asset_ipp_port_link_list.Where(p => p.ipp_asset_id == _ipp_asset_id) 
                     join a in g.asset_list on pp.ipp_asset_id equals a.asset_id
                     join l in g.location_list on a.location_id equals l.location_id
                     select new PPViewVM{
                          ipp_asset_id = _ipp_asset_id,
                          port_no = pp.port_no,
                          alarm_status = pp.alarm_status,
                          wo_status = pp.wo_status,
                          is_port_trace = pp.is_port_trace,
                          port_status = pp.ipp_port_status,
                          cur_location_path = l.location_path,
                          cur_disp_name = string.Format("{0}/{1}", a.asset_name, pp.port_no),
                     };

            _list_pp_vm = list.ToList();

            foreach (var node in _list_pp_vm)
            {
                var cur = g.asset_port_link_list.Find(p => (p.asset_id == node.ipp_asset_id) && (p.port_no == node.port_no));
                if (cur != null)
                {
                    var front = g.asset_port_link_list.Find(p => (p.asset_id == cur.front_asset_id) && (p.port_no == cur.front_port_no));
                    if (front != null)
                    {
                        node.front_asset_id = front.asset_id;
                        node.front_port_no = front.port_no;
                        var a1 = g.asset_list.Find(p => p.asset_id == front.asset_id);
                        if (a1 != null)
                        {
                            node.front_disp_name = string.Format("{0}/{1}", a1.asset_name, front.port_no);
                            var l1 = g.location_list.Find(p => p.location_id == a1.location_id);
                            if (l1 != null)
                                node.front_location_path = l1.location_path;
                        }
                    }
                    var rear = g.asset_port_link_list.Find(p => (p.asset_id == cur.rear_asset_id) && (p.port_no == cur.rear_port_no));
                    if (rear != null)
                    {
                        node.rear_asset_id = rear.asset_id;
                        node.rear_port_no = rear.port_no;
                        node.is_rear_cable = true;
                        var a2 = g.asset_list.Find(p => p.asset_id == rear.asset_id);
                        if (a2 != null)
                        {
                            node.rear_disp_name = string.Format("{0}/{1}", a2.asset_name, rear.port_no);
                            var l2 = g.location_list.Find(p => p.location_id == a2.location_id);
                            if (l2 != null)
                                node.rear_location_path = l2.location_path;
                        }
                    }
                    node.ip_addr = getinfo(node.rear_asset_id , node.rear_port_no, 2);
                    node.mac_addr = getinfo(node.rear_asset_id, node.rear_port_no, 1);
                    node.host_name = getinfo(node.rear_asset_id, node.rear_port_no, 3);
                }

                // Jake, ip 정보를 채워야 함.
            }

            ucIPP880.MyItemsSource = _list_pp_vm;
        }

        private string getinfo(int _sw_asset_id, int p1, int p2)
        {
            string ret = "";
            var a = g.asset_terminal_list.Find(p => p.cur_sw_asset_id == _sw_asset_id && p.cur_sw_port_no == p1);
            if (a == null)
                return ret;

            switch (p2)
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
            var a = g.asset_list.Find(p => p.asset_id == _ipp_asset_id);
            if (a == null)
                return;

            var c = g.catalog_list.Find(p => p.catalog_id == a.catalog_id);
            if (c == null)
                return;

            _txtAssetName.Text = a.asset_name;
            _txtCatalogName.Text = c.catalog_name;
        }

        static readonly object _locker = new object();
        private async void ucIPP880_MySelectedChangeEvent(object obj)
        {
            int idx = (int)obj;
            if (idx >= 0)
            {
                PPViewVM vm = _list_pp_vm[idx];
                _gridDetail.DataContext = vm;

                if (_port_no != vm.port_no)
                {
                    _port_no = vm.port_no;
                    // 링크다이어그램을 표시...
                    _ucLink.Show(vm.ipp_asset_id, _port_no);

                    // 포트 트레이스 중인 경우 타이머에 의해 아래 포트번호가 포트트레이스가 동작된다.
                    if (port_trace_flag)
                        _new_port_trace_no = _port_no;
                }
            }
        }

        // 이벤트가 빠른 시간내에 여러개가 들어올 경우 마지막 이벤트만 처리하되 한 번에 하나씩 처리하기 위해 타이머를 사용
        private DispatcherTimer _timer = new DispatcherTimer();
        private bool port_trace_flag = false;   // 포트 트레이스가 시작되는 경우 true
        private int _new_port_trace_no = 0;     // 요청이 발생하는 경우의 포트 번호
        private int _cur_port_trace_no = 0;     // 현재 포트 트레이스가 되는 경우 포트 번호

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_port_no > 0)
            {
                if (port_trace_flag)
                {
                    // 타이머 정지
                    port_trace_flag = false;
                    _timer.Stop();

                    await port_trace(_ipp_asset_id, _cur_port_trace_no, false);

                    var aipl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == _ipp_asset_id) && (p.port_no == _cur_port_trace_no));
                    int remote_asset_id = aipl != null ? aipl.remote_pp_asset_id ?? 0 : 0;
                    int remote_port_no = aipl != null ? aipl.remote_port_no ?? 0 : 0;
                    if (remote_asset_id > 0)
                        await port_trace(remote_asset_id, remote_port_no, false);

                    _cur_port_trace_no = 0;
                    _new_port_trace_no = 0;
                }
                else
                {
                    // 타이머 시작
                    port_trace_flag = true;
                    _new_port_trace_no = _port_no;
                    _timer.Interval = System.TimeSpan.FromMilliseconds(100);
                    _timer.Tick += new EventHandler(TimerEvent);
                    _timer.Start();
                }
            }
        }

        private async void TimerEvent(object sender, System.EventArgs e)
        {
            // 일단 이벤트 틱이 더 이상 발생하지 않도록 정지...
            _timer.Stop();
            if ((_new_port_trace_no == 0) || (_new_port_trace_no == _cur_port_trace_no))
            {
                // 타이머 재가동?
                if (port_trace_flag)
                    _timer.Start();
                return;
            }

            int new_port_trace_no = _new_port_trace_no;
            int cur_port_trace_no = _cur_port_trace_no;
            //Debug.WriteLine(string.Format("start tick. cur={0}, new={1}", cur_port_trace_no, new_port_trace_no));

            if (cur_port_trace_no > 0)
            {
                _cur_port_trace_no = new_port_trace_no;
                await port_trace(_ipp_asset_id, cur_port_trace_no, false);
                var aipl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == _ipp_asset_id) && (p.port_no == cur_port_trace_no));
                int remote_asset_id = aipl != null ? aipl.remote_pp_asset_id ?? 0 : 0;
                int remote_port_no = aipl != null ? aipl.remote_port_no ?? 0 : 0;
                if (remote_asset_id > 0)
                    await port_trace(remote_asset_id, remote_port_no, false);
            }
            if (new_port_trace_no > 0)
            {
                _cur_port_trace_no = new_port_trace_no;
                await port_trace(_ipp_asset_id, new_port_trace_no, true);
                var aipl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == _ipp_asset_id) && (p.port_no == new_port_trace_no));
                int remote_asset_id = aipl != null ? aipl.remote_pp_asset_id ?? 0 : 0;
                int remote_port_no = aipl != null ? aipl.remote_port_no ?? 0 : 0;
                if (remote_asset_id > 0)
                    await port_trace(remote_asset_id, remote_port_no, true);
            }
            //Debug.WriteLine(string.Format("stop tick. cur={0}, new={1}", cur_port_trace_no, new_port_trace_no));
            // 타이머 재가동?
            if (port_trace_flag)
                _timer.Start();
        }

        private async Task<bool>port_trace(int ipp_asset_id, int port_no, bool on_off)
        {
            var re = new request()
            {
                type = eRequestType.ePortTrace,
                pp_asset_id = ipp_asset_id,
                port_no = port_no,
                on_off = on_off
            };
            var re1 = (request)await g.webapi.post("request", re, typeof(request));
            return re1 != null;
        }

        // 윈도우 종료 시 port trace를 off 시킨다.
        private async void _window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_cur_port_trace_no > 0)
            {
                await port_trace(_ipp_asset_id, _cur_port_trace_no, false);
                var aipl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == _ipp_asset_id) && (p.port_no == _cur_port_trace_no));
                int remote_asset_id = aipl != null ? aipl.remote_pp_asset_id ?? 0 : 0;
                int remote_port_no = aipl != null ? aipl.remote_port_no ?? 0 : 0;
                if (remote_asset_id > 0)
                    await port_trace(remote_asset_id, remote_port_no, false);
            }
        }
    }
}
                                                      