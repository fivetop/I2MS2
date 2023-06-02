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

namespace I2MS2.Windows
{
    // 3D 도면 선택 관리자 
    /// <summary>
    /// _3dDrawingManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawingManager : Window
    {
        public DrawingManager()
        {
            InitializeComponent();
        }

        private void drawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void drawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void drawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void _btnEdit_Click(object sender, RoutedEventArgs e)
        {
            DrawingEditer window = new DrawingEditer();
            window.ShowDialog();
        }
        // 취소 선택
        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
