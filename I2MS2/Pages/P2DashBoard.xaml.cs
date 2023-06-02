using I2MS2.Chart;
using I2MS2.Library;
using I2MS2.Models;
using I2MS2.UserControls;
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

namespace I2MS2.Pages
{
    /// <summary>
    /// P2DashBoard.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class P2DashBoard : Page
    {

        #region Test
        DataViewModel tempmodel1;
        private void s1_Click(object sender, RoutedEventArgs e)
        {
            tempmodel1.TestStartTimer();
        }

        private void s2_Click(object sender, RoutedEventArgs e)
        {
            tempmodel1.TestStopTimer();
        }


        #endregion
        List<int> _dashboard_view_list = new List<int>();           // 해당 판넬의 보이기 처리 1/0
        List<int> _dashboard_position_list = new List<int>();       // 해당 판넬의 위치 처리 1,2,....

        public P2DashBoard()
        {
            g._P2DashBoard = this;

            InitializeComponent();

            initMenu();
            disp_DB_Panel();
        }
         
        // 초기 화면 셋팅 
        private void disp_DB_Panel()
        {
            // DB_Panel[] arr = { _panel1, _panel2, _panel3, _panel4, _panel5, _panel6, _panel7, _panel8, _panel9, _panel10, _panel11, _panel12, _panel13, _panel14, _panel15, _panel16, _panel17 };
            DB_Panel[] arr = { _panel1, _panel2, _panel3, _panel4, _panel5, _panel6, _panel7, _panel8, _panel9 };
            try
            {
                _p_main.Children.Clear();
                
                // 위치 순서 정하기 
                for (int i = 0; i < _dashboard_position_list.Count(); i++)
                {
                    int cnt = _dashboard_position_list[i];
                    _p_main.Children.Add(arr[cnt]);
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
            int cnt = _p_main.Children.Count;  

            for (int i = 0; i < cnt ; i++)
            {
                string s1 = "T" + (i+1).ToString();
                DB_Panel p1 = (DB_Panel) _p_main.Children[i];
                p1.position = i;
                MenuItem m1 = new MenuItem();
                m1.Header = p1.TitleName;
                m1.Tag = s1;
                m1.IsCheckable = true;
                m1.IsChecked = true;
                m1.Click += new RoutedEventHandler(MenuItem_Click);
                _menu1.Items.Add(m1);
                _dashboard_view_list.Add(1);
                _dashboard_position_list.Add(i);
            }
            // GS_DEL
            //_dashboard_view_list = g._dashboard_view;
            //_dashboard_position_list = g._dashboard_position;

//#if GS_DEL
            var l1 = Reg.get_dashboard("dashboard_view");
            var l2 = Reg.get_dashboard("dashboard_position");

            if (l1 == null)
            {
                Reg.save_dashboard(_dashboard_position_list, "dashboard_position");
                Reg.save_dashboard(_dashboard_view_list, "dashboard_view");
                l1 = Reg.get_dashboard("dashboard_view");
                l2 = Reg.get_dashboard("dashboard_position");
            }
            if (l1.Count() != 0)
                _dashboard_view_list = l1.ToList();
            if (l2.Count() != 0)
                _dashboard_position_list = l2.ToList();
//#endif
        }


        // 드래그 처리 
        private Point startPoint;

        private void _p_main_Drop(object sender, DragEventArgs e)
        {
            //Point pt1 = e.GetPosition(sender);
            try
            {
                var t1 = e.Source as DB_Panel; // 올드 
                var t2 = e.Data.GetData(e.Data.GetFormats()[0]) as DB_Panel; // 신규 

                //if (t1 == t2)
                //{
                //    _p_main.Children.Remove(t2);
                //    _p_main.Children.Insert(t2.position, t2);
                //    return;
                //}
                for (int i = 0; i < _p_main.Children.Count; i++)
                {
                    var t3 = _p_main.Children[i] as DB_Panel; // 올드 찾기
                    if (t3 == t1)
                    {
                        // 원래 자리 지우고 해당 위치에 끼워 넣기 
                        _p_main.Children.Remove(t2);
                        _p_main.Children.Insert(i, t2);
                        _dashboard_position_list.Remove(t2.position);
                        _dashboard_position_list.Insert(i, t2.position);
                        break;
                    }
                }
            }
            catch (COMException ex)
            {
                System.Console.WriteLine("{0}", ex.ToString() );
            }
            g._dashboard_position = _dashboard_position_list;
            Reg.save_dashboard(_dashboard_position_list, "dashboard_position");
        }

        private void _p_main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var obj = e.Source as DB_Panel;
            if (obj != null && e.ButtonState == MouseButtonState.Pressed)
            {
                startPoint = e.GetPosition(null);
                Console.WriteLine("{0}, {1}", startPoint.X, startPoint.Y);
                DragDrop.DoDragDrop(obj, obj, DragDropEffects.Move);
            }
        }

        // 클로즈 이벤트 받기 처리 
        private void _panel_CloseEvent(object obj, RoutedEventArgs e, Object o)
        {
            DB_Panel p1 = o as DB_Panel;
            ContextMenu m1 = _menu1;
            string s1 = p1.TitleName;

            int i = 0;
            foreach(MenuItem cmp in m1.Items)
            {
                string s2 = cmp.Header.ToString();
                if(s2 == s1)
                {
                    if(p1.Visibility == System.Windows.Visibility.Collapsed)
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
            g._dashboard_view = _dashboard_view_list; 
            Reg.save_dashboard(_dashboard_view_list, "dashboard_view");
        }
        // 메뉴 클릭시 화면 제어처리 
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DB_Panel[] arr = { _panel1, _panel2, _panel3, _panel4, _panel5, _panel6, _panel7, _panel8, _panel9 };
            MenuItem menu = sender as MenuItem;
            string t1 = menu.Tag.ToString();
            int p1 = int.Parse(t1.Substring(1, t1.Length-1));
            bool t2 = menu.IsChecked;

            _dashboard_view_list[p1 - 1] = (t2 == true) ? 1: 0;
            g._dashboard_view = _dashboard_view_list;
            Reg.save_dashboard(_dashboard_view_list, "dashboard_view");

            arr[p1 - 1].Visibility = t2 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed; 
        }

        #pragma warning disable 4014
        private async Task<bool> Page_Loaded2(object sender, RoutedEventArgs e)
        {
            bool a = await tempmodel1.OnLoaded();
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
            tempmodel1.OnLoaded_Event(code);
        }
        internal void PageLoad()
        {
            tempmodel1 = new DataViewModel();
            this.DataContext = tempmodel1;
            Page_Loaded2(null, null);
        }
    }
}
