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

    public class SWMountVM : INotifyPropertyChanged
    {
        #region SWMountVM 정의
        public int asset_id { get; set; }
        public string asset_name { get; set; }
        public int catalog_id { get; set; }
        public string catalog_name { get; set; }
        public int? slot_no { get; set; }
        public Color checked_color { get; set; }
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
        #endregion
    }

    public partial class SWMountManager : Window
    {
        int _location_id = 0;
        int _sw_asset_id = 0;

        Color color_red = (Color)Application.Current.Resources["_colorRed"];

        List<SWMountVM> _list_asset_vm = new List<SWMountVM>();
        List<SWMountVM> _list_mount_vm = new List<SWMountVM>();

        public SWMountManager(int location_id, int sw_asset_id)
        {
            InitializeComponent();
            _location_id = location_id;
            _sw_asset_id = sw_asset_id;
            initData();
        }

        #region UI 이벤트 처리 로직
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
           var item = (sender as FrameworkElement).DataContext;
            int idx = _lvLeft.Items.IndexOf(item);
            _lvLeft.SelectedIndex = idx;

            if (idx >= 0)
            {
                var node = _lvLeft.SelectedItem as SWMountVM;
                if (node == null)
                    return;

                // 이미 눌려진 버튼..
                if (node.checked_color == Colors.Transparent)
                    return;

                var idx2 = _lvRight.SelectedIndex;
                if (idx2 < 0)
                {
                    idx2 = findEmptySlot();
                }
                if (addSlot(node, idx2))
                {
                    _lvRight.SelectedIndex = idx2 + 1;
                    //int pp_no = _list_mount_vm[idx2].pp_no;
                    //offAssetButton(node.asset_id, pp_no);
                }
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int idx = _lvRight.Items.IndexOf(item);
            _lvRight.SelectedIndex = idx;

            if (idx >= 0)
            {
                delSlot(idx);
            }
        }

        private async void _btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool r = await saveData();
            if (r)
                Close();
        }
        #endregion

        #region 드래그 & 드롭
        private Point startPoint;

        private void _lvLeft_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void _lvLeft_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem == null)
                    return;

                // Find the data behind the ListViewItem
                SWMountVM vm = (SWMountVM)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", vm);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }

        private void _lvRight_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") ||
                sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void _lvRight_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                SWMountVM vm = e.Data.GetData("myFormat") as SWMountVM;
                if (vm == null)
                    return;

                // Drop 한 곳의 위치를 알아온다.
                ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem != null)
                {
                    var vm2 = (SWMountVM)_lvRight.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    int idx = _lvRight.Items.IndexOf(vm2);
                    if (idx >= 0)
                    {
                        _lvRight.SelectedIndex = idx;

                        // 이미 눌려진 버튼..
                        if (vm.checked_color != color_red)
                            return;

                        if (addSlot(vm, idx))
                        {
                            //int pp_no = _list_mount_vm[idx].pp_no;
                            //offAssetButton(vm.asset_id, pp_no);
                        }
                    }
                }
            }
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
            initData_lvLeft();
            initData_lvRight();
            //dispRight();

            // update button for left1 list view
            foreach (var node in _list_mount_vm)
            {
                offAssetButton(node.asset_id, node.slot_no);
            }
        }

        private void initData_lvLeft()
        {
            _list_asset_vm = (from a in g.asset_list 
                                join c in g.catalog_list on a.catalog_id equals c.catalog_id
                                where (a.location_id == _location_id) && (CatalogType.is_sw(a.catalog_id)) && (c.sw_figure_type == "C")
                              select new SWMountVM
                       {
                           asset_id = a.asset_id,
                           asset_name = a.asset_name,
                           catalog_id = a.catalog_id,
                           catalog_name = c.catalog_name,
                           checked_color = color_red
                       }).ToList();

            _lvLeft.ItemsSource = _list_asset_vm;
        }

        private void initData_lvRight()
        {
            var list = from sw in g.sw_card_config_list
                    join a in g.asset_list on sw.sw_card_asset_id equals a.asset_id 
                    join c in g.catalog_list on a.catalog_id equals c.catalog_id
                    where sw.sw_asset_id == _sw_asset_id
                    select new SWMountVM
                    {
                        asset_id = a.asset_id,
                        asset_name = a.asset_name,
                        catalog_id = a.catalog_id,
                        catalog_name = c.catalog_name,
                        slot_no = sw.slot_no,
                        checked_color = Colors.White
                    };

            _list_mount_vm = list.ToList();
                   

            // pp 최대 수를 알아온다.
            int max = 1;
            var aa = g.asset_list.Find(p => p.asset_id == _sw_asset_id);
            if (aa != null)
            {
                var c = g.catalog_list.Find(p => p.catalog_id == aa.catalog_id);
                max = c != null ? (c.sw_num_of_slots ?? 1) : 1;
            }

            int slot_no = 1;
            int idx = 0;
            int cnt = _list_mount_vm.Count();
            idx = 0;
            SWMountVM vm2 = null;

            while (slot_no <= max)
            {
                // 리스트에 레코드가 없는 경우 추가.
                if (idx >= cnt)
                {
                    vm2 = newVM(slot_no++);
                    _list_mount_vm.Add(vm2);
                    cnt++;
                }
                else
                {
                    vm2 = _list_mount_vm[idx];
                    if (vm2.slot_no == slot_no)
                        slot_no ++;
                    else
                    {
                        vm2 = newVM(slot_no++);
                        _list_mount_vm.Insert(idx, vm2);
                        cnt++;
                    }
                }
                idx++;
            }

            _lvRight.ItemsSource = _list_mount_vm;
        }

        private SWMountVM newVM(int slot_no)
        {
            SWMountVM vm = new SWMountVM();

            vm.slot_no = slot_no;
            vm.checked_color = Colors.Transparent;
            return vm;
        }

        private async Task<bool> saveData()
        {
            foreach(var vm in _list_mount_vm)
            {
                int slot_no = vm.slot_no ?? 0;
                if (slot_no > 0)
                {
                    var node = g.sw_card_config_list.Find(p => (p.sw_asset_id == _sw_asset_id) && (p.slot_no == slot_no));
                    if (node != null)
                    {
                        node.sw_card_asset_id = vm.asset_id;
                        node.asset = null;

                        var r = await g.webapi.put("sw_card_config", node.sw_card_config_id, node, typeof(sw_card_config));
                        if (r != 0)
                        {
                            MessageBox.Show(g.tr_get("C_Error_Server"));
                            return false;
                        }
                    }

                }
            }
            return true;
        }

        private void onAssetButton(int asset_id)
        {
            // 좌측 IPP 리스트에서 체크 표시 한다.
            try
            {
                var tmp = _list_asset_vm.Find(ee => ee.asset_id == asset_id);
                if (tmp != null)
                {
                    tmp.checked_color = color_red;
                    tmp.slot_no = 0;
                    tmp.force_changed = true;
                }
            }
            catch (Exception) { }
        }

        private void offAssetButton(int asset_id, int? pp_no)
        {
            // 좌측 IPP 리스트에서 체크 표시 한다.
            try
            {
                var tmp = _list_asset_vm.Find(ee => ee.asset_id == asset_id);
                if (tmp != null)
                {
                    tmp.checked_color = Colors.Transparent;
                    tmp.slot_no = pp_no;
                    tmp.force_changed = true;
                }
            }
            catch (Exception) { }
        }

        private int findEmptySlot()
        {
            int idx = 0;
            foreach(var node in _list_mount_vm)
            {
                if (node.asset_id == 0)
                    return idx;
                idx++;
            }
            return -1;
        }

        private bool addSlot(SWMountVM vm, int idx)
        {
            if (_list_mount_vm[idx].asset_id != 0)
                return false;

            SWMountVM dest = _list_mount_vm[idx];
            dest.asset_id = vm.asset_id;
            dest.asset_name = vm.asset_name;
            dest.catalog_id = vm.catalog_id;
            dest.catalog_name = vm.catalog_name;
            dest.checked_color = Colors.White;
            dest.force_changed = true;
            vm.checked_color = Colors.Transparent;
            vm.slot_no = dest.slot_no;
            vm.force_changed = true;
            return true;
        }

        private bool delSlot(int idx)
        {
            if (_list_mount_vm[idx].asset_id == 0)
                return false;

            SWMountVM dest = _list_mount_vm[idx];
            var node = _list_asset_vm.Find(p => p.asset_id == dest.asset_id);
            if (node != null)
            {
                node.checked_color = color_red;
                node.slot_no = null;
                node.force_changed = true;
            }

            dest.asset_id = 0;
            dest.asset_name = "";
            dest.catalog_id = 0;
            dest.catalog_name = "";
            dest.checked_color = Colors.Transparent;
            dest.force_changed = true;
            return true;
        }

        #endregion

    }
}
                                                      