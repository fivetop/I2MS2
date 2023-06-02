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
using I2MS2.Models;
using WebApi.Models;

namespace I2MS2.UserControls
{
    /// <summary>
    /// P1SelectCenter_RegionPopUp.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class regionFlagTextControl : UserControl
    {
        public int id;

        public regionFlagTextControl(int _id, string region_name)
        {
            InitializeComponent();
            id = _id;
            region1_text.Text = string.Format("{0}", region_name);
            
        }
    }
}
