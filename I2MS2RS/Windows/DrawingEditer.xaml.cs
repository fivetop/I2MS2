using Microsoft.Win32;
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

using I2MS2.Animation;
using I2MS2.Models;
using I2MS2.Library.Drawing;
using MahApps.Metro.Controls;

namespace I2MS2.Windows
{
    enum DrawingMode
    {
        NONE,
        WALL_DRAWING,
        ROOM_DRAWING,
        RACK_DRAWING,
        PORT_DRAWING,
        DELETE
    };

    /// <summary>
    /// _3dDrawingEdit.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DrawingEditer : MetroWindow
    {


        public DrawingEditer()
        {
            InitializeComponent();
        }

     
    }
}
