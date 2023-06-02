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

namespace I2MS2.Views
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
                tb = new TextBlock { Text = dateTime + "  당월 전력 사용률 : " + g.DVModel.CurEnv.ivalue13.ToString() + "%", SnapsToDevicePixels = true };
            else
                tb.Text = dateTime + "  당월 전력 사용률 : " + g.DVModel.CurEnv.ivalue13.ToString() + "%";

            transitioning.Content = null;
            transitioning.Content = tb;
            PieValue(g.DVModel.CurEnv.ivalue13);
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
            MenuItem mnu = (MenuItem)sender;
            string txTag = mnu.Tag.ToString();


            foreach (MenuItem item in _menu1.Items)
            {
                item.IsChecked = false;
            }

            mnu.IsCheckable = true;
            mnu.IsChecked = true;

            if (txTag.Equals("0")) // 타이틀1 클릭
                g._dash1_view_option = 0;
            else if (txTag.Equals("1")) //타이틀2 클릭
                g._dash1_view_option = 1;
            else if (txTag.Equals("2")) //타이틀3 클릭
                g._dash1_view_option = 2;

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

        private void _lvManufacture_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
