using I2MS2.Models;
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
using I2MS2.Library;
using System.ComponentModel;
using System.Windows.Threading;

namespace I2MS2.Windows
{
    public partial class ICFwUpgrade_VM : INotifyPropertyChanged
    {
        public int ic_asset_id { get; set; }
        public string ic_disp_name { get; set; }
        public string ic_fw_version { get; set; }
        public string ic_fw_status { get; set; }
        public int fw_id { get; set; }
        public string fw_version { get; set; }
        public int user_id { get; set; }
        public DateTime? fw_update_date { get; set; }
        public string remarks { get; set; }
        public bool is_select { get; set; }
        public string ic_ftp_server_ip { get; set; }

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

    /// <summary>
    /// ICFwUpgradeManagerApply.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ICFwUpgradeManagerApply : Window
    {

        public List<ICFwUpgrade_VM> ic_fwup_list = new List<ICFwUpgrade_VM>();
        List<int> ic_fwup_apply_id_list = new List<int>();
        fw_upgrade fw;

        public ICFwUpgradeManagerApply(fw_upgrade fw)
        {
            InitializeComponent();
            this.fw = fw;
            initData();
        }

        private async Task<bool> initData()
        {
            await g.left_tree_handler.reload_ic_connect_status();

            _stackFWInfo.DataContext = fw;
            _txbFwName.Text = fw.fw_name;
            _txbFwVersion.Text = fw.fw_version;
            _txbFwFileName.Text = fw.fw_file_name;

            foreach (var ic in g.ic_connect_status_list)
            {
                int ic_asset_id = ic.ic_asset_id;
                string name = Etc.get_asset_name(ic_asset_id);
                ICFwUpgrade_VM ic_fwup = new ICFwUpgrade_VM();
                ic_fwup.fw_id = fw.fw_id;
                ic_fwup.ic_asset_id = ic_asset_id;
                ic_fwup.ic_disp_name = name;
                ic_fwup.ic_fw_version = ic.fw_version;
                ic_fwup.ic_fw_status = "";
                ic_fwup.ic_ftp_server_ip = ic.ftp_server_ip;
                ic_fwup.fw_update_date = ic.fw_update_date;
                ic_fwup_list.Add(ic_fwup);
            }

            _lvIC.ItemsSource = ic_fwup_list;
            return true;
        }

        private DateTime? get_ic_fw_last_updated(int ic_asset_id)
        {
            var list = g.fw_upgrade_hist_list.Where(p => (p.ic_asset_id == ic_asset_id) && (p.result == "S")).OrderByDescending(p => p.last_updated).ToList();
            if (list.Count == 0)
                return null;
            return list[0].last_updated;
        }

        private void _cbFwApply_Checked(object sender, RoutedEventArgs e)
        {
            var v = e.OriginalSource;
            if (v is CheckBox)
            {
                CheckBox ck = (CheckBox)v;
                var src = ck.DataContext;
                if(src is ICFwUpgrade_VM)
                {
                    ICFwUpgrade_VM icfw = (ICFwUpgrade_VM)src;
                    ic_fwup_apply_id_list.Add(icfw.ic_asset_id);
                }
            }
        }

        private async void _btnApply_Click(object sender, RoutedEventArgs e)
        {
            //ic_fwup_apply_id_list에 있는 ic들의 업그래이드를 진행한다

            int cnt = ic_fwup_list.Count(p => p.is_select);
            if (cnt == 0)
                return;

            bool b1 = MessageBox.Show(g.tr_get("M9_ICFwUpgradeManager_Apply_1"), g.tr_get("C_Firmware_Upgrade"),
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (!b1)
                return;

            foreach(var vm in ic_fwup_list)
            {
                if (vm.is_select)
                {
                    int ic_asset_id = vm.ic_asset_id;
                    //string fw_name = _txbFwName.Text;
                    string fw_version = _txbFwVersion.Text;
                    string fw_file_name = _txbFwFileName.Text;
                    int fw_id = vm.fw_id;
                    vm.ic_fw_status = "Progressing...";
                    vm.force_changed = true;

                    var new_node = new request();
                    new_node.ApplyIcFw(ic_asset_id, fw_id, fw_file_name, fw_version);
                    var r = (request) await g.webapi.post("request", new_node, typeof(request));
                }
            }
            
            // Close();
        }

        public void update_fw_upgrade_window(ICFwUpgrade_VM node, int fw_upgrade_result)
        {
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
            {
                try
                {
                    node.ic_fw_status = fw_upgrade_result == 1 ? "Successed" : "Failed";
                    // 성공하면 체크를 지운다.
                    if (fw_upgrade_result == 1)
                    {
                        node.fw_version = fw.fw_version;
                        node.is_select = false;
                    }
                    node.force_changed = true;
                }
                catch (Exception)
                { }
            }));
        }
    }
}
