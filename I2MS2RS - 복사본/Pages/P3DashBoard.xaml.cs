using I2MS2.Chart;
using I2MS2.Library;
using I2MS2.Models;
using I2MS2.UserControls;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;
using WebApi.Models;

namespace I2MS2.Pages
{
    /// <summary>
    /// P2DashBoard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class P3DashBoard : Page
    {

        #region Test
        public string sysinfo
        {
            get { return (string)GetValue(sysinfoProperty); }
            set { SetValue(sysinfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for sysinfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty sysinfoProperty =
            DependencyProperty.Register("sysinfo", typeof(string), typeof(P3DashBoard), new PropertyMetadata("System Event Information"));

        
        private void s1_Click(object sender, RoutedEventArgs e)
        {
            g.DVModel.TestStartTimer();
        }

        private void s2_Click(object sender, RoutedEventArgs e)
        {
            g.DVModel.TestStopTimer();
        }

        public void set_location_id(int id)
        {
            g.DVModel.Location_Id = id;
        }

        #endregion
        List<int> _dashboard_view_list = new List<int>();           // 해당 판넬의 보이기 처리 1/0
        List<int> _dashboard_position_list = new List<int>();       // 해당 판넬의 위치 처리 1,2,....
        TextBlock tb = null;

        List<GroupBox> arr = new List<GroupBox>();
        public P3DashBoard()
        {
           g._P3DashBoard = (P3DashBoard) this;

            InitializeComponent();

            initMenu();
            disp_DB_Panel();
            var t = new DispatcherTimer(TimeSpan.FromSeconds(3), DispatcherPriority.Normal, Tick, this.Dispatcher);
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

        // 초기 화면 셋팅 
        private void disp_DB_Panel()
        {

            try
            {
                _p_main.Items.Clear();
                
                // 위치 순서 정하기 
                for (int i = 0; i < _dashboard_position_list.Count(); i++)
                {
                    int cnt = _dashboard_position_list[i];
                    _p_main.Items.Add(arr[cnt]);
                }

                // 보이기 처리 
                for (int i = 0; i < _dashboard_view_list.Count(); i++)
                {
                    int cnt = _dashboard_view_list[i];
                    arr[i].Visibility = cnt == 1 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                }

                // 메뉴 처리 
                foreach (MenuItem menu in _menu1.Items)
                {
                    string t1 = menu.Tag.ToString();
                    int p1 = int.Parse(t1.Substring(1, t1.Length - 1));

                    if (_dashboard_view_list[p1 - 1] == 1)
                    {
                        menu.IsChecked = true;
                    }
                    else
                    {
                        menu.IsChecked = false;
                    }
                }

            }
            catch (COMException ex)
            {
                System.Console.WriteLine("{0}", ex.ToString());
            }

            return;
        }

        // 메뉴 처리 
        private void initMenu()
        {
            int cnt = _p_main.Items.Count;  

            for (int i = 0; i < cnt ; i++)
            {
                string s1 = "T" + (i+1).ToString();
                GroupBox p1 = (GroupBox)_p_main.Items[i];
                p1.Tag = i;
                p1.Header = p1.Header +" : " +s1;
                MenuItem m1 = new MenuItem();
                m1.Header = p1.Header;
                m1.Tag = s1;
                m1.IsCheckable = true;
                m1.IsChecked = true;
                m1.Click += new RoutedEventHandler(MenuItem_Click);
                _menu1.Items.Add(m1);
                _dashboard_view_list.Add(1);
                _dashboard_position_list.Add(i);
                arr.Add(p1);
            }

            var l1 = Reg.get_dashboard("dashboard2_view");
            var l2 = Reg.get_dashboard("dashboard2_position");

            if (l1 == null)
            {
                Reg.save_dashboard(_dashboard_position_list, "dashboard2_position");
                Reg.save_dashboard(_dashboard_view_list, "dashboard2_view");
            }

            if (l1.Count() != 0) 
                _dashboard_view_list = l1.ToList();
            if (l2.Count() != 0) 
                _dashboard_position_list = l2.ToList();
        }


        // 드래그 처리 
        private Point startPoint;

        private void _p_main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var obj = e.Source as GroupBox;
            if (obj != null && e.ButtonState == MouseButtonState.Pressed)
            {
                startPoint = e.GetPosition(null);
                Console.WriteLine("{0}, {1}", startPoint.X, startPoint.Y);
                DragDrop.DoDragDrop(obj, obj, DragDropEffects.Move);
            }
        }


        private void _p_main_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var obj = e.Source as GroupBox;
            if (obj != null && e.ButtonState == MouseButtonState.Pressed)
            {
                startPoint = e.GetPosition(null);
                Console.WriteLine("{0}, {1}", startPoint.X, startPoint.Y);
                DragDrop.DoDragDrop(obj, obj, DragDropEffects.Move);
            }

        }

        private void _p_main_Drop_1(object sender, DragEventArgs e)
        {
            //Point pt1 = e.GetPosition(sender);
            try
            {
                var t1 = e.Source as GroupBox; // 올드 
                var t2 = e.Data.GetData(e.Data.GetFormats()[0]) as GroupBox; // 신규 

                for (int i = 0; i < _p_main.Items.Count; i++)
                {
                    var t3 = _p_main.Items[i] as GroupBox; // 올드 찾기
                    if (t3 == t1)
                    {
                        // 원래 자리 지우고 해당 위치에 끼워 넣기 
                        _p_main.Items.Remove(t2);
                        _p_main.Items.Insert(i, t2);
                        _dashboard_position_list.Remove(int.Parse(t2.Tag.ToString()));
                        _dashboard_position_list.Insert(i, int.Parse(t2.Tag.ToString()));
                        break;
                    }
                }
            }
            catch (COMException ex)
            {
                System.Console.WriteLine("{0}", ex.ToString());
            }
            Reg.save_dashboard(_dashboard_position_list, "dashboard2_position");

        }

        // 클로즈 이벤트 받기 처리 
        private void ExitButton_Clicked(object sender, RoutedEventArgs e)
        {
            Button b1 = sender as Button;

            ContextMenu m1 = _menu1;
            int t1 = int.Parse(b1.Tag.ToString()) - 1;
            GroupBox p1 = arr[t1];
            string s1 = p1.Header.ToString();

            int i = 0;
            foreach (MenuItem cmp in m1.Items)
            {
                string s2 = cmp.Header.ToString();
                if (s2 == s1)
                {
                    if (p1.Visibility == System.Windows.Visibility.Collapsed)
                    {
                        p1.Visibility = System.Windows.Visibility.Visible;
                        cmp.IsChecked = true;
                        _dashboard_view_list[i] = 1;
                    }
                    else
                    {
                        p1.Visibility = System.Windows.Visibility.Collapsed;
                        cmp.IsChecked = false;
                        _dashboard_view_list[i] = 0;
                    }
                }
                i++;
            }
            Reg.save_dashboard(_dashboard_view_list, "dashboard2_view");
            //_p_main.Update(); // memory error 
        }


        // 메뉴 클릭시 화면 제어처리 
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            string t1 = menu.Tag.ToString();
            int p1 = int.Parse(t1.Substring(1, t1.Length-1));
            bool t2 = menu.IsChecked;

            _dashboard_view_list[p1 - 1] = (t2 == true) ? 1: 0;
            Reg.save_dashboard(_dashboard_view_list, "dashboard2_view");

            arr[p1 - 1].Visibility = t2 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            _p_main.Update();
        }

        #pragma warning disable 4014
        private async Task<bool> Page_Loaded2(object sender, RoutedEventArgs e)
        {
            bool a = await g.DVModel.OnLoaded();
            return true;
        }
        // 페이지 로드에서는 처리 없음 
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Page_Loaded2(sender , e);
        }
        // Signal R 처리로직 
        // 장비 시그날을 받으면 페이지를 업데이트 처리 한다.  알람 이벤트 처리
        public void update_dashboard(eEventCode code)
        {
            g.DVModel.OnLoaded_Event(code);
        }
        internal void PageLoad()
        {
            g.DVModel = new DataViewModel();
            this.DataContext = g.DVModel;
            Page_Loaded2(null, null);
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
    }



}
