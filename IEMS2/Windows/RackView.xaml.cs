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
using WebApi.Models;
using I2MS2.UserControls;
using System.Windows.Controls.Primitives;
using System.Collections;

namespace I2MS2.Windows
{
    /// <summary>
    /// RackView.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public class RackListVM : INotifyPropertyChanged
    {
        #region RackVM 정의
        public int catalog_id { get; set; }
        public int site_id { get; set; }
        public string site_name { get; set; }
        public int building_id { get; set; }
        public string building_name { get; set; }
        public int floor_id { get; set; }
        public string floor_name { get; set; }
        public int room_id { get; set; }
        public string room_name { get; set; }
        public int rack_id { get; set; }
        public string rack_name { get; set; }
        public double total_units { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public double height2 { get; set; }
        public double pixel_4_unit { get; set; }
        public int selected_image { get; set; }

        public List<RackVM> _list_mount_vm = new List<RackVM>();
        public IEnumerable my_source { get; set; }

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

    public partial class RackView : Window
    {
        List<RackListVM> _list_rack_vm = new List<RackListVM>();

        int _location_id = 0;
        double _pixel_4_unit = 16;

        public RackView(int location_id)
        {
            _location_id = location_id;
            InitializeComponent();
            dispRack(location_id);
        }

         private void dispRack(int location_id)
         {
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;

            IEnumerable<location> list = null;
            switch (l.location_level)
            {
                case 3 :
                    int site_id2 = l.site_id ?? 0;
                    list = g.location_list.Where(p => p.site_id == site_id2);
                    break;
                case 4 :
                    int building_id2 = l.building_id ?? 0;
                    list = g.location_list.Where(p => p.building_id == building_id2);
                    break;
                case 5:
                    int floor_id2 = l.floor_id ?? 0;
                    list = g.location_list.Where(p => p.floor_id == floor_id2);
                    break;
                case 6:
                    int room_id2 = l.room_id ?? 0;
                    list = g.location_list.Where(p => p.room_id == room_id2);
                    break;
                case 7:
                    int rack_id2 = l.rack_id ?? 0;
                    list = g.location_list.Where(p => p.rack_id == rack_id2);
                    break;

            }

            _list_rack_vm.Clear();
            _lvRackSet.ItemsSource = null;
            foreach (var node in list)
            {
                int site_id = node.site_id ?? 0;
                int building_id = node.building_id ?? 0;
                int floor_id = node.floor_id ?? 0;
                int room_id = node.room_id ?? 0;
                int rack_id = node.rack_id ?? 0;
                var rr = g.rack_list.Find(p => p.rack_id == rack_id);
                if (rr == null)
                    continue;

                var rc = g.rack_config_list.Find(p => (p.rack_id == rack_id) && (p.slot_no > 0));
                if (rc == null)
                    continue;
                string rack_name = rr.rack_name;
                int catalog_id = rr.rack_catalog_id ?? 0;
                var c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
                if (c == null)
                    continue;
                int tot_units = c.rm_unit_size ?? 0;

                var s1 = g.site_list.Find(p => p.site_id == site_id);
                string site_name = s1 != null ? s1.site_name : "";
                var b1 = g.building_list.Find(p => p.building_id == building_id);
                string building_name = b1 != null ? b1.building_name : "";
                var f1 = g.floor_list.Find(p => p.floor_id == floor_id);
                string floor_name = f1 != null ? f1.floor_name : "";
                var r1 = g.room_list.Find(p => p.room_id == room_id);
                string room_name = r1 != null ? r1.room_name : "";

                RackListVM vm = new RackListVM();
                vm.site_id = site_id;
                vm.site_name = site_name;
                vm.building_id = building_id;
                vm.building_name = building_name;
                vm.floor_id = floor_id;
                vm.floor_name = floor_name;
                vm.room_id = room_id;
                vm.room_name = room_name;
                vm.rack_id = rack_id;
                vm.rack_name = rack_name;
                vm.total_units = tot_units;
                vm.pixel_4_unit = _pixel_4_unit;
                vm.width = _pixel_4_unit * 11 + 20 + 10 + 10 + 20;
                vm.height = _pixel_4_unit * 52 + 20 + 20;
                vm.height2 = _pixel_4_unit * tot_units + 20 + 20;
                vm.selected_image = 440; // _pixel_4_unit >= 30 ? 440 : 220;  romee 2/23 무조건 440 만 사용 처리 

                RackLib rack_lib = new RackLib(vm.rack_id, null, vm._list_mount_vm);
                rack_lib.dispRack();
                vm.my_source = vm._list_mount_vm;

                _list_rack_vm.Add(vm);
            }
            _lvRackSet.ItemsSource = _list_rack_vm;
        }

        private void refreshRack()
        {
            int idx = 0;
            foreach (var item in _lvRackSet.Items)
            {
                RackListVM vm = (RackListVM)item;
                var item2 = (ListViewItem)_lvRackSet.ItemContainerGenerator.ContainerFromIndex(idx++);
                RackControl rc = FindByName("uc", item2) as RackControl;
                if (rc != null)
                {
                    RackLib rack_lib = new RackLib(vm.rack_id, rc, vm._list_mount_vm);
                    rack_lib.dispRack();
                }
            }
        }

        private FrameworkElement FindByName(string name, FrameworkElement root)
        {
            Stack<FrameworkElement> tree = new Stack<FrameworkElement>();
            tree.Push(root);

            while (tree.Count > 0)
            {
                FrameworkElement current = tree.Pop();
                if (current == null)
                    return null;
                if (current.Name == name)
                    return current;

                int count = VisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < count; ++i)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(current, i);
                    if (child is FrameworkElement)
                        tree.Push((FrameworkElement)child);
                }
            }

            return null;
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            _pixel_4_unit += 4; 
            dispRack(_location_id);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (_pixel_4_unit < 10)
                return;
            _pixel_4_unit -= 4;
            dispRack(_location_id);
        }
    }
}
                                                      