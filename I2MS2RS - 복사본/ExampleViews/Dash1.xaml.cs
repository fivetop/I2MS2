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

namespace MetroDemo.ExampleViews
{
    /// <summary>
    /// Interaction logic for Dash1.xaml
    /// </summary>
    public partial class Dash1 : UserControl
    {
        List<int> _dashboard_view_list = new List<int>();           // 해당 판넬의 보이기 처리 1/0
        List<int> _dashboard_position_list = new List<int>();       // 해당 판넬의 위치 처리 1,2,....

        TextBlock tb = null;

        public Dash1()
        {
            g._dash1 = this;
            InitializeComponent();
            initMenu();
            disp_DB_Panel();

            var t = new DispatcherTimer(TimeSpan.FromSeconds(5), DispatcherPriority.Normal, Tick, this.Dispatcher);

        }

        void Tick(object sender, EventArgs e)
        {
            var dateTime = DateTime.Now;

            if (g.DVModel == null)
                return;
            if (tb == null)
                tb = new TextBlock { Text = dateTime + "  역률 : " + g.DVModel.CurEnv.ivalue3.ToString() + "%", SnapsToDevicePixels = true };
            else
                tb.Text = dateTime + "  역률 : " + g.DVModel.CurEnv.ivalue3.ToString() + "%";

            transitioning.Content = null;
            transitioning.Content = tb;
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

        public bool PieValue(double value)
        {
            double v1;

            v1 = value / 2;

            _pie2.Setvalueorg = v1;
            _pie2.DoubleAnimation_Start();
            return true;
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

        private void ExitButton_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void _p_main_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void _p_main_Drop_1(object sender, DragEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _p_main.Update();
/*
 * if (g.DVModel != null)
            {
                g.DVModel.CurEnv.ivalue = 0;
                g.DVModel.CurEnv.ivalue1 = 0;
                g.DVModel.CurEnv.ivalue2 = 0;
                g.DVModel.CurEnv.ivalue3 = 0;
                g.DVModel.CurEnv.ivalue4 = 0;
                g.DVModel.CurEnv.ivalue5 = 0;
                g.DVModel.CurEnv.ivalue6 = 0;
                g.DVModel.CurEnv.ivalue7 = 0;
                g.DVModel.CurEnv.ivalue8 = 0;
                g.DVModel.OnLoaded();   
            }
 */
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            _p_main.Update();
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _p_main.Update();
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
