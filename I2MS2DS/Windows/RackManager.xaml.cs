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
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using I2MS2.Library;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;

namespace I2MS2.Windows
{
    /// <summary>
    /// DrawingsManager.xaml에 대한 상호 작용 논리
    /// </summary>
    ///

    public partial class RackManager : Window
    {
        public static RoutedCommand SaveCommand = new RoutedCommand();

        private int _room_id = 0;
        private int _rack_id = 0;
        private rack _r = null;

        public RackManager(int room_id, int rack_id)
        {
            _room_id = room_id;
            _rack_id = rack_id;
            InitializeComponent();
            initData();
            if (_rack_id > 0)
                dispData();
        }

        private void _window_Loaded(object sender, RoutedEventArgs e)
        {
        }


        #region Command & Event

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_txtName.Text))
            {
                e.CanExecute = false;
                return;
            }

            int s1 = _cboRack.SelectedIndex;
            if (s1 < 1)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = true;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveData())
                return;

            DialogResult = true;
            Close();
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        #endregion

        #region 그 외 메소드

        private void initData()
        {
            // 신규의 경우 _r == null
            _r = g.rack_list.Find(p => p.rack_id == _rack_id);

            var list2 = g.catalog_list.Where(p => p.catalog_group_id == 3210).ToList();
            list2.Insert(0, new catalog { catalog_id = 0, catalog_name = "--Select--" });
            _cboRack.ItemsSource = list2;
            _cboRack.SelectedIndex = 0;

            var list3 = g.catalog_list.Where(p => p.catalog_group_id == 3220).ToList();
            list3.Insert(0, new catalog { catalog_id = 0, catalog_name = "--Select--" });
            _cboVcmL.ItemsSource = list3;
            _cboVcmL.SelectedIndex = 0;
            _cboVcmR.ItemsSource = list3;
            _cboVcmR.SelectedIndex = 0;
        }

        private void dispData()
        {
            if (_r == null)
                return;

            _txtName.Text = _r.rack_name;
            _txtRemarks.Text = _r.remarks;

            int rack_catalog_id = _r.rack_catalog_id ?? 0;
            if (rack_catalog_id == 0)
                _cboRack.SelectedIndex = 0;
            else
                _cboRack.SelectedValue = rack_catalog_id;

            int vcm_l_catalog_id = _r.vcm_l_catalog_id ?? 0;
            if (vcm_l_catalog_id == 0)
                _cboVcmL.SelectedIndex = 0;
            else
                _cboVcmL.SelectedValue = vcm_l_catalog_id;

            int vcm_r_catalog_id = _r.vcm_r_catalog_id ?? 0;
            if (vcm_r_catalog_id == 0)
                _cboVcmR.SelectedIndex = 0;
            else
                _cboVcmR.SelectedValue = vcm_r_catalog_id;
        }


        private async Task<bool> saveData()
        {
            string name = _txtName.Text.Trim();

            // 신규이면?
            if (_rack_id == 0)
            {
                // 같은 방 내에서 랙명이 같으면 안된다.
                var at = g.location_list.Find(p => (p.location_name == name) && (p.room_id == _room_id));
                if (at != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_5"));
                    return false;
                }

                _r = new rack();
                getData();

                var rr = (rack)await g.webapi.post("rack", _r, typeof(rack));
                if (rr == null)
                    return false;

                g.rack_list.Add(rr);
                _rack_id = rr.rack_id;

                var rr2 = await g.webapi.put("rack", _rack_id, rr, typeof(rack));
                if (rr2 != 0)
                    return false;

                bool b = await g.left_tree_handler.addRack(_rack_id);
                if (!b)
                    return false;
                int location_id = Etc.get_location_id_by_rack_id(_rack_id);
                g.signalr.send_location_to_signalr(location_id, eAction.eAdd);
            }
            else
            {
                // 같은 빌딩 내에서 층명이 같으면 안된다.
                var at = g.location_list.Find(p => (p.location_name == name) && (p.rack_id != _rack_id) && (p.room_id == _room_id));
                if (at != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_5"));
                    return false;
                }

                getData();
                var rr = await g.webapi.put("rack", _rack_id, _r, typeof(rack));
                if (rr != 0)
                    return false;

                bool b = await g.left_tree_handler.editRack(_rack_id, _r.rack_name);
                if (!b)
                    return false;
                int location_id = Etc.get_location_id_by_rack_id(_rack_id);
                g.signalr.send_location_to_signalr(location_id, eAction.eModify);
            }

            return true;
        }

        private void getData()
        {
            _r.rack_name = _txtName.Text.Trim();
            _r.remarks = _txtRemarks.Text.Trim();
            _r.room_id = _room_id;
            _r.user_id = g.login_user_id;
            var s1 = (catalog) _cboRack.SelectedItem;
            var s2 = (catalog)_cboVcmL.SelectedItem;
            var s3 = (catalog)_cboVcmR.SelectedItem;
            _r.rack_catalog_id = s1 == null ? 0 : s1.catalog_id;
            _r.vcm_l_catalog_id = s2 == null ? 0 : s2.catalog_id;
            _r.vcm_r_catalog_id = s3 == null ? 0 : s3.catalog_id;
        }
        
        #endregion
    }
}
