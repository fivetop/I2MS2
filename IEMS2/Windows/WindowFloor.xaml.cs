using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using MahApps.Metro.Controls;

namespace I2MS2
{
    /// <summary>
    /// Interaction logic for IconsWindow.xaml
    /// </summary>
    public partial class WindowFloor : MetroWindow
    {

        public WindowFloor()
        {
            this.DataContext = this;
            InitializeComponent();
            this.Loaded += this.OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

    }
}
