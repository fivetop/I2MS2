using I2MS2.Library;
using I2MS2.Models;
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
using WebApi.Models;

namespace I2MS2.Windows
{
    // 3D 도면 크기 설정 
    public partial class ItemDataTmp
    {
        public ItemDataTmp()
        {
            RACK_SIZE_HEIGHT = g.RACK_SIZE_HEIGHT;
            RACK_SIZE_WIDTH = g.RACK_SIZE_WIDTH;
            RACK_HEIGHT = g.RACK_HEIGHT;
            ASSET_SIZE_HEIGHT = g.ASSET_SIZE_HEIGHT;
            ASSET_SIZE_WIDTH = g.ASSET_SIZE_WIDTH;
            ASSET_HEIGHT = g.ASSET_HEIGHT;
            USERPORT_RADIUS = g.USERPORT_RADIUS;
            USERPORT_HEIGHT = g.USERPORT_HEIGHT;
        }

        public Double RACK_SIZE_HEIGHT;
        public Double RACK_SIZE_WIDTH;
        public Double RACK_HEIGHT;
        public Double ASSET_SIZE_HEIGHT;
        public Double ASSET_SIZE_WIDTH;
        public Double ASSET_HEIGHT;
        public Double USERPORT_RADIUS;
        public Double USERPORT_HEIGHT;
    }
    /// <summary>
    /// EtcViewOption.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EtcViewOption : Window
    {
        List<drawing_3d_vm> d_vm_list = new List<drawing_3d_vm>();
        //rack

        rack rk;
        asset ast;
        ItemDataTmp data;

        public EtcViewOption()
        {
            InitializeComponent();
            data = new ItemDataTmp();
            dataReady();
            foreach(var d in g.drawing_3d_list)
            {
                drawing_3d_vm d_vm = makeDrawing3DVM(d);
                d_vm_list.Add(d_vm);
            }
            _lvDrawing3DList.ItemsSource = d_vm_list;

            int ratio = (int)((g.ASSET_HEIGHT / g.DEF_ASSET_HEIGHT) * 100);
            switch(ratio)
            {
                case 10:
                    _radio10.IsChecked = true;
                    break;
                case 25:
                    _radio25.IsChecked = true;
                    break;
                case 50:
                    _radio50.IsChecked = true;
                    break;
                case 100:
                    _radio100.IsChecked = true;
                    break;
                default:
                    break;
            }
        
        }

        private void dataReady()
        {
            rk = new rack()
            {
                rack_id = 1,
                rack_catalog_id = 0,
                rack_name = "test_rack",
                rack_no = 1,
                pos_x = 1000,
                pos_y = 1000,
                room_id = 1,
                angle = 30,
                user_id = 1,
                vcm_l_catalog_id = 7,
                vcm_r_catalog_id = 6
            };

            ast = new asset()
            {
                asset_id = 2,
                catalog_id =1,
                location_id =1,
                asset_name = "test asset",
                user_id = 1,
            };
        }

        private void _lvDrawing3DList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_lvDrawing3DList.SelectedItem != null)
            {
                drawing_3d_vm select_drawing_3d_vm = (drawing_3d_vm)_lvDrawing3DList.SelectedItem;
                //_ctlDrawingView2D.openDrawingFile(select_drawing_3d_vm.file_path);
                _ctlDrawingView3D.clearItemAll();

                _ctlDrawingView3D.openDrawingFile(select_drawing_3d_vm.file_path);
            }
        }

        private drawing_3d_vm makeDrawing3DVM(drawing_3d d)
        {
            return new drawing_3d_vm()
            {
                drawing_3d_id = d.drawing_3d_id,
                drawing_3d_name = d.drawing_3d_name,
                file_name = d.file_name,
                remarks = d.remarks,
                file_path = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, d.file_name)
            };
        }

        private void drawRackAsset()
        {
            _ctlDrawingView3D.addRack(rk);
            _ctlDrawingView3D.addAsset(ast.asset_id, Library.AssetTreeType.FacePlate,new Point(2000, 2000));
        }

        private void removeRackAsset()
        {
            _ctlDrawingView3D.clearItemAll();
        }

        private void _radio_Checked(object sender, RoutedEventArgs e)
        {
            removeRackAsset();
            if(sender is RadioButton)
            {
                RadioButton rd = (RadioButton)sender;
                int num = Etc.get_number_from_str(rd.Name);
                Double ratio = (Double)num/100;

                g.RACK_SIZE_HEIGHT = g.DEF_RACK_SIZE_HEIGHT * ratio;
                g.RACK_SIZE_WIDTH = g.DEF_RACK_SIZE_WIDTH * ratio;
                g.RACK_HEIGHT = g.DEF_RACK_HEIGHT * ratio;

                g.ASSET_HEIGHT = g.DEF_ASSET_HEIGHT * ratio;
                g.ASSET_SIZE_HEIGHT = g.DEF_ASSET_SIZE_HEIGHT * ratio;
                g.ASSET_SIZE_WIDTH = g.DEF_ASSET_SIZE_WIDTH * ratio;

                g.USERPORT_HEIGHT = g.DEF_USERPORT_HEIGHT * ratio;
                g.USERPORT_RADIUS = g.DEF_USERPORT_RADIUS * ratio;
            }
            drawRackAsset();
        }



        private void _btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(g.tr_get("C_Error14"), "Save", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }

            // GS_DEL 레지스트리에 저장
            int ratio = (int)(g.DEF_RACK_SIZE_HEIGHT / g.RACK_SIZE_HEIGHT);
            g._ratio = ratio;
            Close();
        }

        private void _btnCancle_Click(object sender, RoutedEventArgs e)
        {
            g.RACK_SIZE_HEIGHT = data.RACK_SIZE_HEIGHT;
            g.RACK_SIZE_WIDTH = data.RACK_SIZE_WIDTH;
            g.RACK_HEIGHT = data.RACK_HEIGHT;

            g.ASSET_HEIGHT = data.ASSET_HEIGHT;
            g.ASSET_SIZE_HEIGHT = data.ASSET_SIZE_HEIGHT;
            g.ASSET_SIZE_WIDTH = data.ASSET_SIZE_WIDTH;

            g.USERPORT_HEIGHT = data.USERPORT_HEIGHT;
            g.USERPORT_RADIUS = data.USERPORT_RADIUS;

            Close();
        }
    }

   
}
