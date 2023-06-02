using System;
using System.Collections;
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
using System.Globalization;

namespace I2MS2.Windows
{
    /// <summary>
    /// CatalogGroupManager.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public class SwitchSelectorVM : Etc.asset_tree_vm_ex
    {
        public bool is_select { get; set; }
    }

    public partial class SwitchSelector : Window
    {
        public static RoutedCommand OkCommand = new RoutedCommand();

        private bool _ok = true;
        public SwitchSelectorVM _vm = null;

        public List<SwitchSelectorVM> _switch_list = new List<SwitchSelectorVM>();

        public SwitchSelector(List<SwitchSelectorVM> list)
        {
            _switch_list = list;
            InitializeComponent();
            initData();
        }

        private void _cmdOk_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _ok;
        }

        private void _cmdOk_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SwitchSelectorVM vm = (SwitchSelectorVM)_lvSwitch.SelectedItem;
            if (vm == null)
                return;

            _vm = vm;
            DialogResult = true;
            Close();
        }


        private void initData()
        {
            _lvSwitch.ItemsSource = _switch_list;
            _lvSwitch.SelectedIndex = 0;
        }

    }

  
}
