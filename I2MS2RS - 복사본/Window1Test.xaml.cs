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

namespace I2MS2
{
    /// <summary>
    /// Window1Test.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1Test : Window
    {
        public Window1Test()
        {
            InitializeComponent();

        }

        private void _panel_CloseEvent(object obj, RoutedEventArgs e, object o)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            aa2.Update();

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //aa2.Update();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            aa2.Update();

        }
    }
}
