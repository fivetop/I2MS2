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
using System.Globalization;
using System.ComponentModel;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;

namespace I2MS2.Windows
{

    /// <summary>
    /// UserManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EnviromentManagerSet : MetroWindow
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();

        private bool _edit = false;
        private bool _save = false;
        private bool _cancel = false;
        #endregion

        eb_port_config _item = null;
        int asset_id;
        int add = 0;

        public EnviromentManagerSet(int tasset_id)
        {
            asset_id = tasset_id ;
            InitializeComponent();
            InitData();
            _edit = true;
            enableControl(false);
        }

        private void InitData()
        {
            _asset_id.Text = "Asset ID : " + asset_id;
            double high_current_th, high_power_th, high_powerh_th;
            _item = g.eb_port_config_list.Find(p => p.asset_id == asset_id);
            if (_item == null)
                return;

            high_current_th = (double)((_item.high_current_th ?? 0) );
            high_power_th = (double)((_item.high_power_th ?? 0) );
            high_powerh_th = (double)((_item.high_powerh_th ?? 0));

            txthigh_current_th.Text = high_current_th.ToString("N1");
            txthigh_power_th.Text = high_power_th.ToString("N1");
            txthigh_powerh_th.Text = high_powerh_th.ToString("N1");
            txtlow_temp_th.Text = (_item.low_temp_th ).ToString();
            txthigh_temp_th.Text = (_item.high_temp_th ).ToString();
            txtlow_humi_th.Text = (_item.low_humi_th ).ToString();
            txthigh_humi_th.Text = (_item.high_humi_th ).ToString();

            _ckbx1.IsChecked = _item.high_current_th == null ? false : true;
            _ckbx2.IsChecked = _item.high_temp_th == null ? false : true;
            _ckbx3.IsChecked = _item.high_humi_th == null ? false : true;
        }

        #region CRUD 신규,삭제 등 버튼 처리 로직


        private void _cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _edit;
        }

        private void _cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            enableControl(true);
            _edit = false;
            _save = true;
            _cancel = true;
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txthigh_current_th.Text))
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = _save;
        }
        // 저장 처리 
        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveLeft())
                return;
            _edit = true;
            _save = false;
            _cancel = false;
            enableControl(false);
            CommandManager.InvalidateRequerySuggested();
        }
        private void _cmdCancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _cancel;
        }

        private void _cmdCancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            enableControl(false);
            _edit = true;
            _save = false;
            _cancel = false;
        }
        #endregion


        // enable control 로직
        private void enableControl(bool flag)
        {
            _ckbx1.IsEnabled = flag;
            _ckbx2.IsEnabled = flag;
            _ckbx3.IsEnabled = flag;

            if (_ckbx1.IsChecked == true)
            {
                txthigh_current_th.IsEnabled = flag;
                txthigh_power_th.IsEnabled = flag;
                txthigh_powerh_th.IsEnabled = flag;
            }
            else
            {
                txthigh_current_th.IsEnabled = false;
                txthigh_power_th.IsEnabled = false;
                txthigh_powerh_th.IsEnabled = false;
            }
            if (_ckbx2.IsChecked == true)
            {
                txtlow_temp_th.IsEnabled = flag;
                txthigh_temp_th.IsEnabled = flag;
            }
            else
            {
                txtlow_temp_th.IsEnabled = false;
                txthigh_temp_th.IsEnabled = false;
            }
            if (_ckbx3.IsChecked == true)
            {
                txtlow_humi_th.IsEnabled = flag;
                txthigh_humi_th.IsEnabled = flag;
            }
            else
            {
                txtlow_humi_th.IsEnabled = false;
                txthigh_humi_th.IsEnabled = false;
            }
        }

        private void TextTempChangedDigit(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;

            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c))
                {
                    newText += c;
                }
            }
            int r1 = Etc.get_int(newText);
            if (r1 >= 0 && r1 <= 100)
                textBox.Text = newText;
            else
                textBox.Text = "0";
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
        }

        private void TextHumiChangedDigit(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text.Length < 2) return;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;

            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c))
                {
                    newText += c;
                }
            }
            int r1 = Etc.get_int(newText);
            if (r1 >= 15 && r1 <= 100)
                textBox.Text = newText;
            else
                textBox.Text = "15";
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
        }

        #region add, edit save 로직, delete 로직
        private async Task<bool> saveLeft()
        {
            if (_item == null)
                return false;

            if (_ckbx1.IsChecked == true)
            {
                _item.high_current_th = Convert.ToInt32(Etc.get_double(txthigh_current_th.Text) );
                _item.high_power_th = Convert.ToInt32(Etc.get_double(txthigh_power_th.Text) );
                _item.high_powerh_th = Convert.ToInt32(Etc.get_double(txthigh_powerh_th.Text) );
            }
            else
            {
                _item.high_current_th = null;
                _item.high_power_th = null;
                _item.high_powerh_th = null;
            }
            if (_ckbx2.IsChecked == true)
            {
                _item.low_temp_th = Etc.get_int(txtlow_temp_th.Text) ;
                _item.high_temp_th = Etc.get_int(txthigh_temp_th.Text) ;
            }
            else
            {
                _item.low_temp_th = null;
                _item.high_temp_th = null;
            }
            if (_ckbx3.IsChecked == true)
            {
                _item.low_humi_th = Etc.get_int(txtlow_humi_th.Text) ;
                _item.high_humi_th = Etc.get_int(txthigh_humi_th.Text) ;
            }
            else
            {
                _item.low_humi_th = null;
                _item.high_humi_th = null;
            }

            if (_item.asset_id != asset_id)
            {
            }
            else 
            {
                int r = await g.webapi.put("eb_port_config", _item.asset_id, _item, typeof(eb_port_config));
                if (r != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
            }

            return true;
        }
        #endregion

        private void _ckbx1_Checked(object sender, RoutedEventArgs e)
        {
            txthigh_current_th.IsEnabled = true;
            txthigh_power_th.IsEnabled = true;
            txthigh_powerh_th.IsEnabled = true;
        }

        private void _ckbx1_Unchecked(object sender, RoutedEventArgs e)
        {
            txthigh_current_th.IsEnabled = false;
            txthigh_power_th.IsEnabled = false;
            txthigh_powerh_th.IsEnabled = false;
        }

        private void _ckbx2_Checked(object sender, RoutedEventArgs e)
        {
            txtlow_temp_th.IsEnabled = true;
            txthigh_temp_th.IsEnabled = true;
        }

        private void _ckbx2_Unchecked(object sender, RoutedEventArgs e)
        {
            txtlow_temp_th.IsEnabled = false;
            txthigh_temp_th.IsEnabled = false;
        }

        private void _ckbx3_Checked(object sender, RoutedEventArgs e)
        {
            txtlow_humi_th.IsEnabled = true;
            txthigh_humi_th.IsEnabled = true;
        }

        private void _ckbx3_Unchecked(object sender, RoutedEventArgs e)
        {
            txtlow_humi_th.IsEnabled = false;
            txthigh_humi_th.IsEnabled = false;
        }
    }
}
