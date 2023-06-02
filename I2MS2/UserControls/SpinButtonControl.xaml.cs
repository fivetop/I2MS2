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
    /// SpinButtonControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SpinButtonControl : UserControl
    {

        public static readonly DependencyProperty SpinValueProperty = 
            DependencyProperty.Register("SpinValue", typeof(int), typeof(SpinButtonControl));

        public int SpinValue
        {
            get { return (int)GetValue(SpinValueProperty); }
            set { SetValue(SpinValueProperty, value); }
        }
        
        public SpinButtonControl()
        {
            InitializeComponent();
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            int num = 0;
            if (int.TryParse(_txtLabel.Text, out num))
                num++;

            _txtLabel.Text = num.ToString();
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            int num = 0;
            if (int.TryParse(_txtLabel.Text, out num))
                num--;

            _txtLabel.Text = num.ToString();
        }
    }
}
