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

namespace I2MS2.Windows
{
    /// <summary>
    /// ManufactureManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EventViewer : Window
    {
        public EventPrintList _ep { get; set; }
        public int _ret;

        public EventViewer(EventVM vm)
        {
            InitializeComponent();
            if (vm == null) return;
            _ep = new EventPrintList();
            _ep.event_hist_id = vm.event_hist_id;
            _ep.event_type = getevent_type(vm.event_type);
            _ep.event_text = vm.event_text;
            _ep.location_id = vm.location_id == null ? "" : getLocationName(vm.location_id);
            _ep.asset_id = vm.asset_id == null ? "" : getAssetName(vm.asset_id, vm.port_no);
            _ep.port_no = vm.port_no;
            _ep.mac = vm.mac_addr == null ? g.tr_get("C_None") : vm.mac_addr;
            _ep.ipv4 = vm.ip_addr == null ? g.tr_get("C_None") : vm.ip_addr;
            _ep.write_time = vm.write_time == null ? "" : string.Format("{0}", vm.write_time.ToString("yyyy-MM-dd HH:mm:ss"));

            this.DataContext = _ep;
            _ret = 0;
            if (vm.location_id == 0 || vm.location_id == null)
            {
                _btnlocation.IsEnabled = false;
            }
            if (vm.asset_id == 0 || vm.asset_id == null)
            {
                _btnasset.IsEnabled = false;
            }
        }

        public EventViewer(int id)
        {
            InitializeComponent();
            if (id == 0) return;

            var e = g.event_hist_list.Find(p => p.event_hist_id == id);

            _ep = new EventPrintList();
            _ep.event_hist_id = e.event_hist_id;
            _ep.event_type = getevent_type(Common.get_event_type(e.event_type));
            _ep.event_text = e.event_text;
            _ep.location_id = e.location_id == null ? "" : getLocationName(e.location_id);
            _ep.asset_id = e.asset_id == null ? "" : getAssetName(e.asset_id, e.port_no);
            _ep.port_no = e.port_no;
            _ep.mac = e.mac == null ? g.tr_get("C_None") : e.mac;
            _ep.ipv4 = e.ipv4 == null ? g.tr_get("C_None") : e.ipv4;
            _ep.write_time = e.write_time == null ? "" : string.Format("{0}", e.write_time.ToString("yyyy-MM-dd HH:mm:ss"));

            this.DataContext = _ep;
            _ret = 0;
            if (e.location_id == 0 || e.location_id == null)
            {
                _btnlocation.IsEnabled = false;
            }
            if (e.asset_id == 0 || e.asset_id == null)
            {
                _btnasset.IsEnabled = false;
            }
        }


        private string getevent_type(eEventType id)
        {
            eEventType lid = id;
            string ret = "Information";

            switch (lid)
            {
                case eEventType.eInfo : ret = "Information"; break;
                case eEventType.eWarnning : ret = "Warning"; break;
                case eEventType.eError: ret = "Error"; break;
            }
            return ret;
        }
        
        private string getLocationName(int? id)
        {
            int lid = id ?? 0;
            string ret = "";

            if (lid == 0) return ret;
            var a1 = g.location_list.Find(p => p.location_id == lid);
            if (a1 != null)
            {
                ret = a1.location_path;
            }
            return ret;
        }

        private string getAssetName(int? id, int? port)
        {
            int lid = id ?? 0;
            int lport = port ?? 0;
            string ret = "";

            var a1 = g.asset_list.Find(p => p.asset_id == lid);
            if (a1 != null)
            {
                ret = string.Format("{0}", a1.asset_name);
            }
            return ret;
        }

        private void _gridTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            _ret = 0;
            this.DialogResult = false;
            Close();
        }

        private void _btnlocation_Click(object sender, RoutedEventArgs e)
        {
            _ret = 1;
            try
            {
                this.DialogResult = true;
            }
            catch { }
            Close();
        }

        private void _btnasset_Click(object sender, RoutedEventArgs e)
        {
            _ret = 2;
            this.DialogResult = true;
            Close();
        }
    }
}
