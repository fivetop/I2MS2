using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WebApi.Models;
using I2MS2.Models;
using I2MS2.Library;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;

namespace I2MS2.Windows
{

    /// <summary>
    /// UserManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EnviromentCorrection : MetroWindow
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();

        private bool _edit = false;
        private bool _save = false;
        private bool _cancel = false;
        #endregion

        private xreserved item;                                                        // 현재 상태 
        public xreserved _item { get { return item; } set { item = value; } }// 데이터베이스 

        List<xreserved> _list;     // 현재

        public EnviromentCorrection()
        {
            InitializeComponent();
            InitData1();
            _edit = true;
            enableControl(false);
            this.DataContext = _item;
        }

        private async void InitData1()
        {
            await InitData();
        }

        private async Task<bool> InitData()
        {
            try { 
                _list = (List<xreserved>) await g.webapi.getList("xreserved", typeof(List<xreserved>));
            }
            catch(Exception e1)
            {
            }

            _item = _list.Find(p => p.id == 1);
            if (_item == null)
            {
                _item.r1 = 1;
                _item.r2 = 1;
                _item.r3 = 1;
            }
            this.DataContext = _item;
            return true;
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
            t1.Focus();
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(t1.Text))
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

        #region add, edit save 로직, delete 로직
        private async Task<bool> saveLeft()
        {
            int r = await g.webapi.put("xreserved", _item.id, item, typeof(xreserved));
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
            t1.IsEnabled = flag;
            t2.IsEnabled = flag;
            t3.IsEnabled = flag;
        }

        #endregion
    }
}
