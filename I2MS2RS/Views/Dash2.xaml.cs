using I2MS2.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WebApi.Models;

namespace I2MS2.Views
{
    /// <summary>
    /// Interaction logic for Dash2.xaml
    /// </summary>
    public partial class Dash2 : UserControl
    {
        List<int> _dashboard_view_list = new List<int>();           // 해당 판넬의 보이기 처리 1/0
        List<int> _dashboard_position_list = new List<int>();       // 해당 판넬의 위치 처리 1,2,....

        public Dash2()
        {
            g._dash2 = this;
            InitializeComponent();
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

        private void _p_main_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

    }
}
