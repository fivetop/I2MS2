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
using MahApps.Metro.Controls;

namespace I2MS2.Windows
{
    /// <summary>
    /// RackView.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public class IEMountVM : INotifyPropertyChanged
    {
        #region RackVM 정의
        public int asset_id { get; set; }
        public string asset_name { get; set; }
        public int catalog_id { get; set; }
        public int? pp_no { get; set; }
        public string pp_connect_no_string { get; set; }
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

    public partial class IEMountManager : MetroWindow
    {
        int _location_id = 0;
        int _ic_asset_id = 0;

        Color color_red = (Color)Application.Current.Resources["_colorRed"];

        List<IEMountVM> _list_asset_vm = new List<IEMountVM>();
        List<IEMountVM> _list_mount_vm = new List<IEMountVM>();

        public IEMountManager(int location_id, int ic_asset_id)
        {
            InitializeComponent();
            _location_id = location_id;
            _ic_asset_id = ic_asset_id;
            initData();
        }

        #region 초기화 처리 로직
        private void initData()
        {
            initData_lvLeft();
            initData_lvRight();
            //dispRight();

            // update button for left1 list view
            foreach (var node in _list_mount_vm)
            {
                offAssetButton(node.asset_id, node.pp_no);
            }
        }

        private void initData_lvLeft()
        {
            location l = g.location_list.Find(at => at.location_id == _location_id);
            List<int?> list2 = new List<int?>();
//            var l1 = (from aa in g.ic_ipp_config_list.Where(p => p.ipp_asset_id != null && p.ic_asset_id != _ic_asset_id)
//                      select new { aa.ipp_asset_id });

            var l1 = (from aa in g.eb_config_list.Where(p => p.eb_slave_asset_id != null && p.eb_asset_id != _ic_asset_id)
                      select new { aa.eb_slave_asset_id });

            list2 = l1.Select(p => p.eb_slave_asset_id).ToList();

            int?[] t1 = list2.ToArray();

            // 층으로 처리      romee 2/11
            var _list = from aa in g.asset_list.Where(p => CatalogType.is_eb(p.catalog_id) && p.catalog_id == 412003)
                        join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                        join cc in g.location_list.Where(p => p.floor_id == l.floor_id) on aa.location_id equals cc.location_id
                        select new IEMountVM
                        {
                            asset_id = aa.asset_id,
                            asset_name = aa.asset_name,
                            catalog_id = aa.catalog_id,
                            checked_color = color_red
                        };

            // 층으로 처리      romee 2/11
            _list_asset_vm = (from aa in _list
                              where !(t1.Contains(aa.asset_id))
                              select new IEMountVM
                              {
                                  asset_id = aa.asset_id,
                                  asset_name = aa.asset_name,
                                  catalog_id = aa.catalog_id,
                                  checked_color = color_red
                              }).ToList();

            // 해당 랙으로 처리 
            //_list_asset_vm = (from a in g.asset_list
            //                  where (a.location_id == _location_id) && (CatalogType.is_ipp(a.catalog_id))
            //                  select new ICMountVM
            //                  {
            //                      asset_id = a.asset_id,
            //                      asset_name = a.asset_name,
            //                      catalog_id = a.catalog_id,
            //                      checked_color = color_red
            //                  }).ToList();

            _lvLeft.ItemsSource = _list_asset_vm;
        }

        private void initData_lvRight()
        {
            var list = from ic in g.eb_config_list
                       join a in g.asset_list
                       on ic.eb_slave_asset_id equals a.asset_id
                       where ic.eb_asset_id == _ic_asset_id
                       select new IEMountVM
                       {
                           asset_id = a.asset_id,
                           asset_name = a.asset_name,
                           catalog_id = a.catalog_id,
                           pp_no = ic.eb_connect_no,
                           checked_color = Colors.White
                       };

            _list_mount_vm = list.ToList();


            // pp 최대 수를 알아온다.
            int max = 31;
            int pp_no = 1;
            int idx = 0;
            int cnt = _list_mount_vm.Count();
            idx = 0;
            IEMountVM vm2 = null;

            while (pp_no <= max)
            {
                // 리스트에 레코드가 없는 경우 추가.
                if (idx >= cnt)
                {
                    vm2 = newVM(pp_no++);
                    _list_mount_vm.Add(vm2);
                    cnt++;
                }
                else
                {
                    vm2 = _list_mount_vm[idx];
                    if (vm2.pp_no == pp_no)
                    {
                        vm2.pp_connect_no_string = string.Format("{0}-{1}", (pp_no - 1) / 4 + 1, (pp_no - 1) % 4 + 1);
                        pp_no++;
                    }
                    else
                    {
                        vm2 = newVM(pp_no++);
                        _list_mount_vm.Insert(idx, vm2);
                        cnt++;
                    }
                }
                idx++;
            }

            _lvRight.ItemsSource = _list_mount_vm;
        }

        private IEMountVM newVM(int pp_no)
        {
            IEMountVM vm = new IEMountVM();

            vm.pp_no = pp_no;
            vm.pp_connect_no_string = string.Format("{0}-{1}", (pp_no - 1) / 4 + 1, (pp_no - 1) % 4 + 1);
            vm.checked_color = Colors.Transparent;
            return vm;
        }

        
        #endregion

        #region UI 이벤트 처리 로직
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
           var item = (sender as FrameworkElement).DataContext;
            int idx = _lvLeft.Items.IndexOf(item);
            _lvLeft.SelectedIndex = idx;

            if (idx >= 0)
            {
                var node = _lvLeft.SelectedItem as IEMountVM;
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
                if (addPP(node, idx2))
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
                delPP(idx);
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
                IEMountVM vm = (IEMountVM)listView.ItemContainerGenerator.
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
                IEMountVM vm = e.Data.GetData("myFormat") as IEMountVM;
                if (vm == null)
                    return;

                // Drop 한 곳의 위치를 알아온다.
                ListViewItem listViewItem = FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem != null)
                {
                    var vm2 = (IEMountVM)_lvRight.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    int idx = _lvRight.Items.IndexOf(vm2);
                    if (idx >= 0)
                    {
                        _lvRight.SelectedIndex = idx;

                        // 이미 눌려진 버튼..
                        if (vm.checked_color != color_red)
                            return;

                        if (addPP(vm, idx))
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
        private async Task<bool> saveData()
        {
            foreach(var vm in _list_mount_vm)
            {
                int pp_no = vm.pp_no ?? 0;
                if (pp_no > 0)
                {
                    var node = g.eb_config_list.Find(p => (p.eb_asset_id == _ic_asset_id) && (p.eb_connect_no == pp_no));
                    if (node != null)
                    {
                        node.eb_slave_asset_id = vm.asset_id;
                        node.asset = null;

                        var r = await g.webapi.put("eb_config", node.eb_config_id, node, typeof(eb_config));
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
                    tmp.pp_no = 0;
                    tmp.pp_connect_no_string = "";
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
                    tmp.pp_no = pp_no;
                    tmp.pp_connect_no_string = "";
                    tmp.force_changed = true;
                }
            }
            catch (Exception) { }
        }

        //private void refreshRight()
        //{
        //    _lvRight.ItemsSource = null;
        //    _lvRight.ItemsSource = _list_mount_vm;
        //}


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

        private bool addPP(IEMountVM vm, int idx)
        {
            if (_list_mount_vm[idx].asset_id != 0)
                return false;

            IEMountVM dest = _list_mount_vm[idx];
            dest.asset_id = vm.asset_id;
            dest.asset_name = vm.asset_name;
            dest.catalog_id = vm.catalog_id;
            dest.checked_color = Colors.White;
            dest.force_changed = true;
            vm.checked_color = Colors.Transparent;
            vm.pp_no = dest.pp_no;
            vm.force_changed = true;
            return true;
        }

        private bool delPP(int idx)
        {
            if (_list_mount_vm[idx].asset_id == 0)
                return false;

            IEMountVM dest = _list_mount_vm[idx];
            var node = _list_asset_vm.Find(p => p.asset_id == dest.asset_id);
            if (node != null)
            {
                node.checked_color = color_red;
                node.pp_no = null;
                node.force_changed = true;
            }

            dest.asset_id = 0;
            dest.asset_name = "";
            dest.catalog_id = 0;
            dest.checked_color = Colors.Transparent;
            dest.force_changed = true;
            return true;
        }

        #endregion

    }
}
                                                      