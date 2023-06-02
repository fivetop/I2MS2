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
using System.Windows.Threading;

namespace I2MS2.Windows
{
    /// <summary>
    /// PopUpWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PopUpWindow
    {
        public DispatcherTimer Timer = new DispatcherTimer();


        public PopUpWindow(String str)
        {
            InitializeComponent();
            _txtPopUp.Text = str;


            Timer.Interval = TimeSpan.FromSeconds(3);
            Timer.Tick += new EventHandler(timer_Tick);
            Timer.Start();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
