using I2MS2.Library;
using I2MS2.Models;
using System;
using System.Collections;
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
    /// LinkDiagramControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LinkDiagramControl : UserControl
    {
        public LinkDiagramControl()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            _lvLink.ItemsSource = null;
        }

        public void Show(int asset_id, int port_no)
        {
            LinkDiagram ld = new LinkDiagram();
            List<WorkCell> _cell_list2 = new List<WorkCell>();
            ld.openAsset(asset_id, port_no, port_no, _cell_list2);
            _cell_list2.RemoveAll(p => p.template_type == "empty");
            _lvLink.ItemsSource= null;
            _lvLink.ItemsSource = _cell_list2;
        }
    }
}
