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
using WebApiClient;
using WebApi.Models;

namespace I2MS2.Windows
{
    /// <summary>
    /// RackView.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class RackMountManager : Window
    {
        int _location_id = 0;
        int _rack_id = 0;
        RackLib rack_lib = null;

        Color color_red = (Color)Application.Current.Resources["_colorRed"];

        List<RackVM> _list_mount_vm = new List<RackVM>();

        rack_config _ipm_l = null;
        rack_config _ipm_r = null;

        public RackMountManager(int location_id, int rack_id)
        {
            InitializeComponent();

            _location_id = location_id;
            _rack_id = rack_id;
            _rack._lvSlots.SelectionMode = SelectionMode.Extended;
            rack_lib = new RackLib(_rack_id, _rack, _list_mount_vm);

            initData();
        }

        #region UI 이벤트 처리 로직
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rack_lib.dispRack();
        }

        private async void _btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool r = await saveData();
            if (r)
            {
                try
                {
                    DialogResult = true;
                }
                catch { }
                Close();
            }
        }
        #endregion

        #region 드래그 & 드롭
        private Point startPoint;

        private void _rack_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void _rack_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(diff.X) > 1 ||
                    Math.Abs(diff.Y) > 1))
                {
                // Get the dragged ListViewItem
                ListViewItem listViewItem =
                    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem == null)
                    return;

                // Find the data behind the ListViewItem
                RackVM vm = (RackVM) _rack._lvSlots.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", vm);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }

        private void _rack_DragEnter(object sender, DragEventArgs e)
        {
            if (!(e.Data.GetDataPresent("myFormat") || e.Data.GetDataPresent("myFormat2")) ||
                sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void _rack_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                RackVM source_vm = e.Data.GetData("myFormat") as RackVM;
                if (source_vm == null)
                    return;

                // Drop 한 곳의 위치를 알아온다.
                ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem != null)
                {
                    RackVM dest_vm = (RackVM)_rack._lvSlots.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    int dest_idx = _rack._lvSlots.Items.IndexOf(dest_vm);
                    int source_idx = _rack._lvSlots.Items.IndexOf(source_vm);
                    if ((source_idx >= 0) && (dest_idx >= 0) && (source_idx != dest_idx))
                    {
                        if (rack_lib.moveAsset(dest_vm, source_vm, dest_idx, source_idx))
                        {
                            refreshRack();
                        }
                    }
                }
            }
         }

        // IPM 드롭
        private void _imgLeftIPM_Drop(object sender, DragEventArgs e)
        {
            Debug.WriteLine("Drop");
        }

        // Helper to search up the VisualTree
        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
#endregion

        #region 처리 로직
        private void initData()
        {
            rack_lib.dispRack();
            _rack._lvSlots.ItemsSource = _list_mount_vm;

            var rr = g.rack_list.Find(p => p.rack_id == _rack_id);
            if (rr == null)
                return;

            int rack_catalog_id = rr.rack_catalog_id ?? 0;
            if (rack_catalog_id == 0)
                return;

            var cc = g.catalog_list.Find(p => p.catalog_id == rack_catalog_id);
            if (cc == null)
                return;

            int num_of_slots = cc.rm_unit_size ?? 0;
            _rack.TotalUnit = num_of_slots;

            var l = g.location_list.Find(p => p.location_id == _location_id);
            _rack.RackName = l != null ? l.location_name : "";
            int pixel_4_unit = 16;
            _rack.PixelPerUnit = pixel_4_unit;
            _rack.Width = pixel_4_unit * 11 + 20 + 10 + 10 + 20;
            _rack.Height = num_of_slots * pixel_4_unit + 20 + 20;
            _gridRack1.Height = 52 * pixel_4_unit + 20 + 20;

            _ipm_l = g.rack_config_list.Find(p => (p.slot_no == 0) && (p.rack_id == _rack_id) && (p.rack_mount_type == "L"));
            _ipm_r = g.rack_config_list.Find(p => (p.slot_no == 0) && (p.rack_id == _rack_id) && (p.rack_mount_type == "R"));

            if (_ipm_l != null)
            {
                string file_path = CatalogType.get_rm_image_220(_ipm_l.catalog_id ?? 0);
                _imgLeftIPM.Source = new BitmapImage(new Uri(file_path, UriKind.Absolute));
            }

            if (_ipm_r != null)
            {
                string file_path = CatalogType.get_rm_image_220(_ipm_r.catalog_id ?? 0);
                _imgRightIPM.Source = new BitmapImage(new Uri(file_path, UriKind.Absolute));
            }
            
        }

        private async Task<bool> saveData()
        {
            // 랙마운트 순으로 순번을 바꾼다.

            var list = (from rc in _list_mount_vm
                        where (rc.rack_id == _rack_id) && (rc.asset_id > 0)
                        orderby rc.slot_no descending
                        select new rack_config()
                        {
                            asset_id = rc.asset_id,
                            catalog_id = rc.catalog_id,
                            rack_config_id = rc.rack_config_id,
                            slot_no = rc.slot_no,
                            rack_id = rc.rack_id,
                            rack_mount_type = "S"
                        }).ToList();

            if (_ipm_l != null)
                list.Add(_ipm_l);

            if (_ipm_r != null)
                list.Add(_ipm_r);

            int disp_order = 0;
            foreach (var node in list)
            {
                // 트리에만 존재한 레코드인경우 추가
                if (node.rack_config_id == 0)
                {
                    var r2 = (rack_config)await g.webapi.post("rack_config", node, typeof(rack_config));
                    if (r2 == null)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return false;
                    }

                    g.rack_config_list.Add(r2);
                }
                else
                {
                    var r3 = g.rack_config_list.Find(p => p.rack_config_id == node.rack_config_id);
                    if (r3 != null)
                    {
                        if ((r3.slot_no != node.slot_no) || (r3.rack_mount_type != node.rack_mount_type))
                        {
                            int r4 = await g.webapi.put("rack_config", node.rack_config_id, node, typeof(rack_config));
                            if (r4 != 0)
                            {
                                MessageBox.Show(g.tr_get("C_Error_Server"));
                                return false;
                            }
                            r3.slot_no = node.slot_no;
                            r3.rack_mount_type = node.rack_mount_type;
                        }
                    }
                }

                var at = g.asset_tree_list.Find(p => p.asset_id == node.asset_id);
                if (at != null)
                {
                    at.disp_order = ++disp_order;
                    var r1 = await g.webapi.put("asset_tree", at.asset_tree_id, at, typeof(asset_tree));
                    if (r1 != 0)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return false;
                    }
                }
            }

            return true;
        }


        private void refreshRack()
        {
            _rack._lvSlots.ItemsSource = null;
            _rack._lvSlots.ItemsSource = _list_mount_vm;
        }
        #endregion

        // 스페이스 항목을 추가한다. 추가 한 곳 부터 모든 유닛을 한칸씩 아래로 이동시킨다.
        private void _rack_InsertSpaceClicked(object sender, RoutedEventArgs e)
        {

        }

        // 지정한 스페이스 항목을 삭제하고 삭제한 곳 아래 위치한 모든 유닛을 한칸씩 위로 이동시킨다.
        private void _rack_DeleteSpaceClicked(object sender, RoutedEventArgs e)
        {

        }

        // 지정한 자산을 삭제하고 그 자리를 비운다.
        private void _rack_DeleteAssetClicked(object sender, RoutedEventArgs e)
        {

        }

        private void _rack_SelectionChanged(object sender, RoutedEventArgs e)
        {
            RackVM vm = (RackVM) _rack._lvSlots.SelectedItem;
            if (vm == null)
                return;
            if (vm.asset_id > 0)
            {
                g.prop_data.force_clear = true;
                g.main_window._ctlRightSide.dispAssetProperty(vm.asset_id);
                g.prop_data.force_changed = true;
            }
        }

        private int? getAssetTreeId(int? asset_id)
        {
            if (asset_id == null)
                return null;

            if (asset_id == 0)
                return null;

            var at = g.asset_tree_list.Find(p => p.asset_id == asset_id);
            if (at == null)
                return null;

            return at.asset_tree_id;            
        }

    }
}
                                                      