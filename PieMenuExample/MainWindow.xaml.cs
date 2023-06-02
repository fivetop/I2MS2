using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PieMenuExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PieMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //System.Console.WriteLine("{0} clicked", (sender as PieGItem).Header);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
           _Menu1.DoubleAnimation_Start();

           _Menu1.BorderBrush2 = Brushes.Beige; // Colors.AliceBlue;
        }

    }
}
