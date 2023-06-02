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
using I2MS2.Models;
using System.ComponentModel;
using System.Diagnostics;
using I2MS2.Library;
using WebApi.Models;
using I2MS2.UserControls;
using System.Windows.Controls.Primitives;
using System.Collections;

namespace I2MS2.Windows
{
    /// <summary>
    /// RackView.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 


    public partial class LinkDiagramView : Window
    {
        int _asset_id = 0;

        public LinkDiagramView(int asset_id)
        {
            _asset_id = asset_id;
            InitializeComponent();
            initData();
        }

        private void initData()
        {
            string asset_name = Etc.get_asset_name(_asset_id);
            _txtAssetName.Text = asset_name;
            int num_of_ports = Etc.get_num_of_ports_by_asset_id(_asset_id);
            _txtPorts.Text = num_of_ports.ToString();
            int i = 0;
            List<LinkDiagramControl> list = new List<LinkDiagramControl>();
            for (i = 0; i < num_of_ports; i++)
            {
                LinkDiagramControl ldc = new LinkDiagramControl();
                ldc.Height = 120;  // 100  romee UP 1  -> 80 -> 100 
                ldc.IsEnabled = false;
                ldc.Show(_asset_id, i + 1);
                list.Add(ldc);
            }
            _lv.ItemsSource = list;
        }
    }
}
                                                      