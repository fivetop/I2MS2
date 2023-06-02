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
    // 3D 도면 이름 저장 
    /// <summary>
    /// DrawingNamming.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawingNamming : Window
    {
        public string name;

        public DrawingNamming()
        {
            InitializeComponent();
        }

        private void _btnOK_Click(object sender, RoutedEventArgs e)
        {
            if(_txtDrawingName !=null)
            {
                name = _txtDrawingName.Text;
                if (name.Length > 20)
                    return;
                try
                {
                    this.DialogResult = true;
                }
                catch { }
                Close();
            }
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
