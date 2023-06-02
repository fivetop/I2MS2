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
using System.Runtime.CompilerServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace I2MS2.Windows
{

    /// <summary>
    /// UserManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EnviromentTarget : MetroWindow
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();

        private bool _edit = false;
        private bool _save = false;
        private bool _cancel = false;
        #endregion

        public ObservableCollection<eb_target> _eb_target { get; set; }      // 바인딩 처리용
        eb_target _item = null;                                              // 데이터베이스 
        eb_target item = new eb_target();                                    // 화면 버퍼

        public EnviromentTarget()
        {
            _eb_target = new ObservableCollection<eb_target>();
            InitializeComponent();
            InitData();
            _edit = true;
            enableControl(false);
            this.DataContext = _eb_target;
        }

        private void InitData()
        {
            DateTime d1 = DateTime.Now;

            _asset_id.Text = d1.Year.ToString();

            _item = g.eb_target_list.Find(p => p.eb_year == d1.Year && p.eb_type == 0);
            if (_item == null)
            {
                item.eb_type = 0;
                item.eb_year = d1.Year;
                // return;
            }
            if(_item != null)
                Etc.CopyTo(_item, item);
            _eb_target.Add(item);
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
            t0.Focus();
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(t0.Text))
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
            Etc.CopyTo(_item, item);
            _eb_target[0] = item;
            enableControl(false);
            _edit = true;
            _save = false;
            _cancel = false;
        }
        #endregion

        #region add, edit save 로직, delete 로직
        private async Task<bool> saveLeft()
        {
            item = _eb_target.First();
            if (item == null)
                return false;

            if ((item.eb_t0 == 0))
            {
                MessageBox.Show("Check setting value!!");
                return false;
            }

            if (_item != null)
                Etc.CopyTo(item, _item);
            else
            {
                _item = (eb_target) await g.webapi.post("eb_target", item, typeof(eb_target));
                if (_item == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                g.eb_target_list.Add(_item);
                return true;
            }

            int r = await g.webapi.put("eb_target", _item.eb_target_id, item, typeof(eb_target));
            if (r != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }
            return true;
        }
        #endregion

        #region 컨트롤 처리 
        // enable control 로직
        private void enableControl(bool flag)
        {
            t0.IsEnabled = flag;
            t1.IsEnabled = flag;
            t2.IsEnabled = flag;
            t3.IsEnabled = flag;
            t4.IsEnabled = flag;
            t5.IsEnabled = flag;
            t6.IsEnabled = flag;
            t7.IsEnabled = flag;
            t8.IsEnabled = flag;
            t9.IsEnabled = flag;
            t10.IsEnabled = flag;
            t11.IsEnabled = flag;
            t12.IsEnabled = flag;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (t0.Text == "" || t0.Text == "0") 
                return;
            int i1 = int.Parse(t0.Text);
            if (i1 < 12) return;
            int d2 = i1 / 12;

            item.eb_t1 = d2;
            item.eb_t2 = d2;
            item.eb_t3 = d2;
            item.eb_t4 = d2;
            item.eb_t5 = d2;
            item.eb_t6 = d2;
            item.eb_t7 = d2;
            item.eb_t8 = d2;
            item.eb_t9 = d2;
            item.eb_t10 = d2;
            item.eb_t11 = d2;
            item.eb_t12 = d2;

            _eb_target[0] = item;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int i1 = item.eb_t1 + item.eb_t2 + item.eb_t3 + item.eb_t4 + item.eb_t5 + item.eb_t6 + item.eb_t7 + item.eb_t8 + item.eb_t9 + item.eb_t10 + item.eb_t11 + item.eb_t12 ?? 0;
            t0_1.Text = i1.ToString();

        }
        #endregion
    }
}
