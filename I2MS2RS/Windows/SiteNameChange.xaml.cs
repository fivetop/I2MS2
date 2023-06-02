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
    public partial class SiteNameChange : MetroWindow
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();

        private bool _edit = false;
        private bool _save = false;
        private bool _cancel = false;
        #endregion

        private site item;                                                        // 현재 상태 
        public site _item { get { return item; } set { item = value; } }// 데이터베이스 

        public SiteNameChange()
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
            _item = g.site_list.Find(p => p.site_id == 79300001);
            if (_item == null)
            {
                _item.site_id  = 79300001;
                _item.site_name = "사이트";
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
            string site_name = t2.Text.ToString();
            if (site_name.Length < 1)
                return false;
            _item.site_name = site_name;
            int r = await g.webapi.put("site", _item.site_id, item, typeof(site));
            if (r != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }
            t1.Text = site_name;

            await editLocation(19030003, site_name);
            return true;
        }

        private async Task<bool> editLocation(int location_id, string name)
        {
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return false;

            int len1 = l.location_name.Length;
            int len2 = l.location_path.Length;
            string left = l.location_path.Substring(0, len2 - len1);
            l.location_name = name;
            l.location_path = left + name;

            int r2 = await g.webapi.put("location", location_id, l, typeof(location));
            if (r2 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            var at = g.asset_tree_list.Find(p => p.location_id == location_id);
            if (at == null)
                return false;
            at.disp_name = name;
            int r3 = await g.webapi.put("asset_tree", at.asset_tree_id, at, typeof(asset_tree));
            if (r3 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            //_ctlLeftSide.editLocationToTreeView(location_id);   // 트리명 바꾸기 

            return true;
        }

        #endregion

        #region 컨트롤 처리 
        // enable control 로직
        private void enableControl(bool flag)
        {
            t1.IsEnabled = false;
            t2.IsEnabled = flag;
        }

        #endregion
    }
}
