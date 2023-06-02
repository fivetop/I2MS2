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
    /// ProgressBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProgressBarControl : UserControl
    {
        public double rect_width = 0;
        public ProgressBarControl()
        {
            InitializeComponent();
            stateBar.Width = 0;
        }

        public void setProgressBar(int pe)
        {
            rect_width = (pe  * 343)/100;
            Console.WriteLine("stateBar.Width={0}", rect_width);
        }

        public void increaseBar()
        {
            
            stateBar.Width = rect_width;
            //Console.WriteLine("stateBar.Width={0}", stateBar.Width);
        }
    }
}
