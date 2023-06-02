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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace I2MS2.UserControls
{

    
    

    /// <summary>
    /// ToggleButton4.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ToggleButton4 : UserControl
    {
        public string Button1 { get; set; }
        public string Button2 { get; set; }
        public string Button3 { get; set; }
        public string Button4 { get; set; }

        public ToggleButton4()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            bt1.Text = Button1;
            bt2.Text = Button2;
            bt3.Text = Button3;
            bt4.Text = Button4;
        }


    }
}
