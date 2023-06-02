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

namespace I2MS2.Windows
{

    public partial class FwUpgrade_VM
    {
        public int num { get; set; }
        public int fw_id { get; set; }
        public string fw_name { get; set; }
        public string fw_version { get; set; }
        public string fw_file_name { get; set; }
        public int user_id { get; set; }
        public DateTime last_updated { get; set; }
        public string remarks { get; set; }
    }

    /// <summary>
    /// IcFwUpgradeWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ICFwUpgradeManager : Window
    {
        Boolean _new = true;
        Boolean _del = false;
        Boolean _apply = false;

        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand ApplyCommand = new RoutedCommand();

        List<FwUpgrade_VM> fw_list = new List<FwUpgrade_VM>();
        FwUpgrade_VM select_fw;


        public ICFwUpgradeManager()
        {
            InitializeComponent();
            fwlist_update();
        }

        private void _cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _new;
        }

        private void _cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ICFwUpgradeManagerNew window = new ICFwUpgradeManagerNew();
            window.Owner = this;
            window.ShowDialog();

            //리스트 업데이트
            fwlist_update();
        }

        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _del;
        }

        private void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            deletFw();
        }

        private void _cmdApply_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _apply;
        }

        private void _cmdApply_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (select_fw == null)
                return;

            //업그래이드 적용 윈도우를 실행한다
            fw_upgrade fw = g.fw_upgrade_list.Find(at => at.fw_id == select_fw.fw_id);
            if (fw == null) return;

            g.fw_apply_window = new ICFwUpgradeManagerApply(fw);
            g.fw_apply_window.Owner = this;
            g.fw_apply_window.ShowDialog();
            g.fw_apply_window = null;
        }

        public FwUpgrade_VM makeFwUp_VM(fw_upgrade fw, int number)
        {
            return new FwUpgrade_VM()
            {
                num = number,
                fw_id = fw.fw_id,
                fw_name = fw.fw_name,
                fw_version = fw.fw_version,
                fw_file_name = fw.fw_file_name,
                user_id = fw.user_id,
                last_updated = fw.last_updated,
                remarks = fw.remarks
            };
        }

        private void fwlist_update()
        {
            _lvFirmware.ItemsSource = null;
            fw_list.Clear();
            int i = 0;
            foreach (var fw in g.fw_upgrade_list)
            {
                i++;
                fw_list.Add(makeFwUp_VM(fw, i));
            }

            _lvFirmware.ItemsSource = fw_list;
        }

        private void _lvFirmware_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((_lvFirmware.SelectedItem !=null)&&(_lvFirmware.SelectedItem is FwUpgrade_VM))
            {
                select_fw = (FwUpgrade_VM)_lvFirmware.SelectedItem;
                _apply = true;
                _del = true;
            }
        }


        private async void deletFw()
        {
            FwUpgrade_VM fw_vm = (FwUpgrade_VM)_lvFirmware.SelectedItem;
            if(fw_vm==null)
                return;

            //서버에 파일 삭제  
            Task<int> t1 = g.webapi.deleteFile("firmware", fw_vm.fw_file_name);
            int ret = await t1;
            //if (ret == 0)
              
            //DB에서 삭제
            int ret2 = await g.webapi.delete("fw_upgrade", fw_vm.fw_id);

            //g. 에서 삭제
            fw_upgrade fw = g.fw_upgrade_list.Find(at => at.fw_id == fw_vm.fw_id);
            if (fw == null) return;

            g.fw_upgrade_list.Remove(fw);


            fwlist_update();
        }

        
    }
}
