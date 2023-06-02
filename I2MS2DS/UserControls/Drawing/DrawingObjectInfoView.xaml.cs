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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace I2MS2.UserControls.Drawing
{
    // 자산뷰에서 3D 처리 ??
    /// <summary>
    /// DrawingObjectInfoView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawingObjectInfoView : UserControl
    {
        public Point3D point { get; set; } 

        public DrawingObjectInfoView()
        {
            InitializeComponent();
        }
    }
}
