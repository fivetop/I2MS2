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
    public partial class SpinButtonControl2 : UserControl
    {

        public static readonly DependencyProperty SpinValueProperty =
            DependencyProperty.Register("SpinValue", typeof(string), typeof(SpinButtonControl2));

        public string SpinValue
        {
            get { return (string)GetValue(SpinValueProperty); }
            set { SetValue(SpinValueProperty, value); }
        }
        
        public SpinButtonControl2()
        {
            InitializeComponent();
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            string name = _txtLabel.Text.Trim();
            if (name == "")
            {   
                _txtLabel.Text = "A";
                return;
            }

            if (name == "None")
            { 
                _txtLabel.Text = "A";
                return;
            }
            if (name == "Z")
                return;
            char asc = Convert.ToChar(name.Substring(0, 1));
            asc++;
            if (asc < 65)
                asc = (char) 65;
            if (asc > 65 + 25)
                asc = (char) (65 + 25);
            _txtLabel.Text = asc.ToString();
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            string name = _txtLabel.Text.Trim();
            if (name == "")
            {
                _txtLabel.Text = "None";
                return;
            }
            if (name == "None")
                return;
            if (name == "A")
            {
                _txtLabel.Text = "None";
                return;
            }
            char asc = Convert.ToChar(name.Substring(0, 1));
            asc--;
            if (asc < 65)
                asc = (char)65;
            if (asc > 65 + 25)
                asc = (char)(65 + 25);
            _txtLabel.Text = asc.ToString();
        }
    }
}
