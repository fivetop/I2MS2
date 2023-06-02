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
    public partial class Dash3 : UserControl
    {
        List<int> _dashboard_view_list = new List<int>();           // 해당 판넬의 보이기 처리 1/0
        List<int> _dashboard_position_list = new List<int>();       // 해당 판넬의 위치 처리 1,2,....

        public Dash3()
        {
            g._dash3 = this;
            InitializeComponent();
            initMenu();
            disp_DB_Panel();
        }

        void Tick(object sender, EventArgs e)
        {
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

    }
}
