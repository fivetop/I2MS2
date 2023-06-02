using I2MS2.Chart;
using I2MS2.Library;
using I2MS2.UserControls;
using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebApi.Models;
using System.Windows.Threading;
using I2MS2.Windows;
using System.Threading;
using I2MS2;

namespace MetroDemo.ExampleViews
{
    /// <summary>
    /// Interaction logic for Dash2.xaml
    /// </summary>
    public partial class Dash2 : UserControl
    {
        List<int> _dashboard_view_list = new List<int>();           // 해당 판넬의 보이기 처리 1/0
        List<int> _dashboard_position_list = new List<int>();       // 해당 판넬의 위치 처리 1,2,....
        double oldWidth;

        public string sysinfo
        {
            get { return (string)GetValue(sysinfoProperty); }
            set { SetValue(sysinfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for sysinfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty sysinfoProperty =
            DependencyProperty.Register("sysinfo", typeof(string), typeof(Dash2), new PropertyMetadata("System Event Information"));

        TextBlock tb = null;

        List<GroupBox> arr = new List<GroupBox>();
        List<listHeader> _listHeader = new List<listHeader>();
        List<EventPrintList> _print_list = new List<EventPrintList>();

        public Dash2()
        {
            g._dash2 = this;
            InitializeComponent();
            initMenu();
            disp_DB_Panel();
            initListView();
            EventAlarm();
            var t = new DispatcherTimer(TimeSpan.FromSeconds(5), DispatcherPriority.Normal, Tick, this.Dispatcher);
        }

        void Tick(object sender, EventArgs e)
        {
            var dateTime = DateTime.Now;

            if (tb == null)
                tb = new TextBlock { Text = dateTime + " " + sysinfo, SnapsToDevicePixels = true };
            else
                tb.Text = dateTime + " " + sysinfo;

            transitioning.Content = null;
            transitioning.Content = tb;
        }


        private void initListView()
        {
            // 리소스 스트링을 디비화 할 필요 있음 
            _listHeader.Clear();
                    _listHeader.Add(new listHeader { h_width = 0, h_title = "ID", h_bind = "event_hist_id" });
                    _listHeader.Add(new listHeader { h_width = 60, h_title = "C_No", h_bind = "RowNumber" });
                    _listHeader.Add(new listHeader { h_width = 130, h_title = "C_Date", h_bind = "write_time" });
                    _listHeader.Add(new listHeader { h_width = 100, h_title = "M_Prop2_Group", h_bind = "event_type" });
                    _listHeader.Add(new listHeader { h_width = 450, h_title = "C_Brief", h_bind = "event_text" });
                    _listHeader.Add(new listHeader { h_width = 380, h_title = "C_Location_Name", h_bind = "location_id" });
            _lvGridView.Columns.Clear();
            // 동적 생성 
            for (int i = 0; i < _listHeader.Count; i++)
            {
                listHeader l1 = _listHeader[i];
                TextBlock t1 = new TextBlock();
                t1.Text = I2MSR.Properties.Resources.ResourceManager.GetString(l1.h_title);
                t1.Style = Application.Current.Resources["I2MS_ListViewColHeaderText"] as Style;
                Border b2 = new Border();
                b2.BorderThickness = new Thickness(0);
                b2.Child = t1;

                GridViewColumn g1 = new GridViewColumn();
                g1.DisplayMemberBinding = new Binding(l1.h_bind);
                if (i == 1)
                {
                    GridViewExtensions.SetIsContentCentered(g1, true);
                }
                g1.Header = b2;
                g1.Width = l1.h_width;

                if (i == 3)
                {
                    g1.DisplayMemberBinding = null;
                    var bind1 = new Binding(l1.h_bind);
                    var bind2 = new Binding(l1.h_bind) { Converter = new MyConverter() };
                    var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
                    textBlockFactory.SetBinding(TextBlock.TextProperty, bind1);
                    textBlockFactory.SetBinding(TextBlock.ForegroundProperty, bind2);
                    var cellTemplate = new DataTemplate { VisualTree = textBlockFactory };
                    g1.CellTemplate = cellTemplate;
                }


                _lvGridView.Columns.Add(g1);
            }
              
             
        }

        public void EventAlarm()
        {
            // 데이터 취합 
            int i = 1;
            var tdb1 = from a in g.event_hist_list
                       join b in g.event_list on a.event_id equals b.event_id
                       orderby a.event_hist_id descending

                       select new EventPrintList()
                       {
                           RowNumber = i++,

                           event_hist_id = a.event_hist_id,
                           event_type = getevent_type(a.event_type),
                           event_id = a.event_id,
                           event_text = a.event_text,
                           asset = a.asset_id,
                           location = a.location_id,
                           location_id = a.location_id == null ? "" : getLocationName(a.location_id),
                           catalog_group_id = a.catalog_group_id,
                           catalog_id = a.catalog_id,
                           asset_id = a.asset_id == null ? "" : getAssetName(a.asset_id, a.port_no),
                           port_no = a.port_no,
                           terminal_asset_id = a.terminal_asset_id == null ? "" : getAssetName(a.asset_id, a.port_no),
                           mac = a.mac,
                           ipv4 = a.ipv4,
                           user_id = a.user_id,
                           is_confirm = a.is_confirm,
                           confirm_user_id = a.confirm_user_id,
                           write_time = a.write_time == null ? "" : string.Format("{0}", a.write_time.ToString("yyyy-MM-dd HH:mm:ss")),
                           wo_id = a.wo_id,

                           popup_screen = b.popup_screen,
                           send_email = b.send_email,
                           send_sms = b.send_sms,
                       };

            int j = 1;
            var tdb2 = from a in tdb1.Take(40)
                       select new EventPrintList()
                       {
                           RowNumber = j++,
                           event_hist_id = a.event_hist_id,
                           event_type = a.event_type,
                           event_id = a.event_id,
                           event_text = a.event_text,
                           asset = a.asset,
                           location = a.location,
                           location_id = a.location_id,
                           catalog_group_id = a.catalog_group_id,
                           catalog_id = a.catalog_id,
                           asset_id = a.asset_id,
                           port_no = a.port_no,
                           terminal_asset_id = a.terminal_asset_id,
                           mac = a.mac,
                           ipv4 = a.ipv4,
                           user_id = a.user_id,
                           is_confirm = a.is_confirm,
                           confirm_user_id = a.confirm_user_id,
                           write_time = a.write_time,
                           wo_id = a.wo_id,

                           popup_screen = a.popup_screen,
                           send_email = a.send_email,
                           send_sms = a.send_sms,
                           event_desc = a.event_desc,
                       };
            _print_list.Clear();
            _print_list = tdb2.ToList();

            // Row Num 삽입
            for (int j1 = 0; j1 < _print_list.Count(); j1++)
            {
                EventPrintList t1 = _print_list[j1];
                t1.RowNumber = j1 + 1;
            }

//            initListView();
            _lvManufacture.ItemsSource = _print_list; // _manufacture_list;
        }

        private string getLocationName(int? id)
        {
            int lid = id ?? 0;
            string ret = "";

            if (lid == 0) return ret;
            var a1 = g.location_list.Find(p => p.location_id == lid);
            if (a1 != null)
            {
                ret = a1.location_path;
            }
            return ret;
        }

        private string getevent_type(string id)
        {
            string lid = id.Trim();
            string ret = "Information";

            switch (lid)
            {
                case "I": ret = "Information"; break;
                case "W": ret = "Warning"; break;
                case "E": ret = "Error"; break;
            }
            return ret;
        }

        private string getBuildingName(int? id)
        {
            int lid = id ?? 0;
            string ret = "";

            if (lid == 0) return ret;
            var a1 = g.building_list.Find(p => p.building_id == lid);
            if (a1 != null)
            {
                ret = a1.building_name;
            }
            return ret;
        }

        private string getFloorName(int? id)
        {
            int lid = id ?? 0;
            string ret = "";

            if (lid == 0) return ret;
            var a1 = g.floor_list.Find(p => p.floor_id == lid);
            if (a1 != null)
            {
                ret = a1.floor_name;
            }
            return ret;
        }

        private string getAssetName(int? id, int? port)
        {
            int lid = id ?? 0;
            int lport = port ?? 0;
            string ret = "";

            if (lid == 0 || lport == 0)
                return ret;
            var a1 = g.asset_list.Find(p => p.asset_id == lid);
            if (a1 != null)
            {
                ret = string.Format("{0}/{1}", a1.asset_name, lport);
            }
            return ret;
        }

        public T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }


        // 초기 화면 셋팅 
        private void disp_DB_Panel()
        {
            return;
        }

        // 메뉴 처리 
        private void initMenu()
        {
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void _p_main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void _p_main_Drop(object sender, DragEventArgs e)
        {

        }

        private void _panel_CloseEvent(object obj, RoutedEventArgs e, object o)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _p_main.Update();
            //Page_Loaded2(null, null);
        }

        private async Task<bool> Page_Loaded2(object sender, RoutedEventArgs e)
        {
            bool a = await g.DVModel.OnLoaded();
            return true;
        }
        // Signal R 처리로직 
        // 장비 시그날을 받으면 페이지를 업데이트 처리 한다.  알람 이벤트 처리
        public void update_dashboard(eEventCode code)
        {
            g.DVModel.OnLoaded_Event(code);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        public void set_location_id(int id)
        {
            g.DVModel.Location_Id = id;
        }

        private void ExitButton_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void _p_main_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void _p_main_Drop_1(object sender, DragEventArgs e)
        {

        }

        public bool PieValue(double value)
        {
            double v1;

            v1 = value / 2;

            _pie2.Setvalueorg = v1;
            _pie2.DoubleAnimation_Start();
            return true;
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _pie2.Setvalueorg = 40;
            _pie2.DoubleAnimation_Start();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.InvalidateVisual();
            Console.WriteLine("UserControl_SizeChanged1 {0}", _p_main.ActualWidth);
            _p_main.InvalidateVisual();
        }

        private void _p_main_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var t = new DispatcherTimer(TimeSpan.FromSeconds(5), DispatcherPriority.Normal, Tick1, this.Dispatcher);
            Console.WriteLine("UserControl_SizeChanged3 {0}", _p_main.ActualWidth);
        }

        void Tick1(object sender, EventArgs e)
        {
//            if (Application.Current.MainWindow.WindowState  == WindowState.Normal)
//                g.main_window.Width = g.main_window.Width  - 1;
            if (oldWidth == _p_main.ActualWidth) return;
            oldWidth = _p_main.ActualWidth;
            _p_main.Update();
            Console.WriteLine("UserControl_SizeChanged4 {0}", _p_main.ActualWidth);
        }

        private void _lvManufacture_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EventPrintList vm = (EventPrintList)_lvManufacture.SelectedItem;
            if (vm == null) return;
            if (vm.event_hist_id == 0) return;

            int p;
            p = (int)vm.location == null ? 0 : (int)vm.location;
            if (p > 0)
                g.left_tree_handler._ctlLeftSide.searchlocation(p);
/*
                    int p1;
                    p1 = (int)vm.asset == null ? 0 : (int)vm.asset;
                    if (p1 > 0)
                        g.left_tree_handler._ctlLeftSide.searchasset(p);
*/

            var lo = g.location_list.Find(l => l.location_id == p);
            if (lo == null)
                return;

            int floor_id = lo.floor_id ?? 0;
            var f1 = g.floor_list.Find(f => f.floor_id == floor_id);
            if (f1 == null)
                return;

            if (g.window.alive == 0)
            {
                g.window = new IPMFloorView(floor_id);
                g.window.Owner = Application.Current.MainWindow;
            }
            g.window.SelectRack(lo.rack_id ?? 0);
            g.window.Show();

        }



    }
}
