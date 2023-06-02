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
using System.Windows.Shapes;
using WebApi.Models;
using I2MS2.Models;
using I2MS2.Library;
using WebApiClient;
using System.ComponentModel;
using I2MS2.Library.ColorPicker;
using System.Globalization;

namespace I2MS2.Windows
{
    /// <summary>
    /// CatalogGroupManager.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public class CatalogExtVM : INotifyPropertyChanged
    {
        public int catalog_ext_id { get; set; }
        public int ext_id { get; set; }
        public int catalog_id { get; set; }
        public string ext_name { get; set; }
        public int ext_length { get; set; }
        public string ext_type { get; set; }
        public string remarks { get; set; }
        public bool is_registered { get; set; }
        public bool is_checked { get; set; }
                                        
        public bool force_changed
        {
            get { return true; }
            set { NotifyPropertyChanged(""); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    public partial class CatalogManager : Window
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();

        private bool _new = false;
        private bool _delete = false;
        private bool _edit = false;
        private bool _save = false;
        private bool _cancel = false;
        #endregion

        private List<CatalogGroupTree> _catalog_group_tree = new List<CatalogGroupTree>();
        private CatalogGroupTree _cgt;

        private List<catalog> _catalog_list = new List<catalog>();
        private catalog _c;
        private int _catalog_id = 0;

        private int _disp_image_id = 0;
        private int _disp_220_image_id = 0;
        private int _disp_440_image_id = 0;
        private int _disp_880_image_id = 0;

        private List<CatalogExtVM> _catalog_ext_vm_list = new List<CatalogExtVM>();
        //private CatalogExtVM _cevm;

        #region 초기화
        public CatalogManager()
        {
            InitializeComponent();
            initTree();
            enableControl(false);
            unselectAllExclusiveCatalog();

            cboManufacture.ItemsSource = g.manufacture_list;
            cboManufacture.SelectedIndex = 0;
        }
        #endregion

        #region CRUD 신규,삭제 등 버튼 처리 로직
        private void _cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_cgt == null)
            {
                e.CanExecute = false;
                return;
            }

            if (_cgt.disp_level != 2)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = _new;
        }

        private void _cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _catalog_id = 0;
            clearControl();
            enableControl(true);

            unselectAllExclusiveCatalog();
            int catalog_group_id = _cgt == null ? 0 : _cgt.catalog_group_id;
            selectExclusiveCatalog(catalog_group_id);
            cboManufacture.SelectedIndex = 0;

            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;

            txtCatalogName.Focus();
        }

        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!_delete)
            {
                e.CanExecute = false;
                return;
            }
            if (_c != null)
            {
                e.CanExecute = _c.deletable == "Y";
                return;
            }
            e.CanExecute = true;
        }

        private async void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var a = g.asset_list.Find(p => p.catalog_id == _catalog_id);
            if (a != null)
            {
                MessageBox.Show(g.tr_get("C_Error_Catalog_1")); 
                return;
            }

            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            int idx = _lvCatalog.SelectedIndex;

            if (await deleteCatalog())
            {
                if (idx >= 0)
                {
                    dispDetail(_cgt);
                    if (idx > 0)
                    {
                        _lvCatalog.SelectedIndex = idx - 1;
                        catalog cc = (catalog) _lvCatalog.SelectedItem;
                        if (cc != null)
                            _lvCatalog.ScrollIntoView(cc);
                    }

                    // Command를 무조건 갱신하게 만듦.
                    CommandManager.InvalidateRequerySuggested();
                    return;
                }
            }
        }

        private void _cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!_edit)
            {
                e.CanExecute = false;
                return;
            }
            // Jake, 잠시 막아 놓는다.
            //if (_c != null)
            //{
            //    e.CanExecute = _c.deletable == "Y";
            //    return;
            //}
            e.CanExecute = true;
        }

        private void _cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            enableControl(true);
            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;
        }


        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _save;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            bool new_flag = _catalog_id == 0;

            if (!await saveData())
                return;

            if (new_flag)
            { 
                // 화면을 갱신하기 위해 다시 데이터를 리스트에 로드하고 선택한다.
                dispDetail(_cgt);
                _lvCatalog.SelectedValue = _catalog_id;
                _lvCatalog.ScrollIntoView(_c);
            }

            enableControl(false);

            _new = true;
            _delete = false;
            _edit = false;
            _save = false;
            _cancel = false;

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdCancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _cancel;
        }

        private void _cmdCancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            clearControl();
            enableControl(false);

            _new = true;
            _delete = false;
            _edit = false;
            _save = false;
            _cancel = false;
        }
        #endregion

        #region 초기화 상세 처리 
        // 좌측 트리 창에 내용을 출력한다.
        private void initTree()
        {
            // GS_DEL
            //List<int> _gs_del_list = new List<int>() { 3120,  3310, 3350, 3360, 3370, 3380, 3390, 3500 };
            int id;
            List<catalog_group> list = g.catalog_group_list.OrderBy(p => p.disp_order).ToList();

            foreach(var cg in list)
            {
                id = cg.catalog_group_id;
                CatalogGroupTree cgt = new CatalogGroupTree();

                cgt.catalog_group_id = cg.catalog_group_id;
                cgt.catalog_group_name = cg.catalog_group_name;
                cgt.disp_level = cg.catalog_level ?? 0;
                cgt.level1_catalog_group_id = cg.level1_catalog_group_id ?? 0;
                cgt.level2_catalog_group_id = cg.level2_catalog_group_id ?? 0;
                cgt.is_expander_visible = Visibility.Visible;
                if (cg.deletable == "Y")
                    cgt.fixed_mark = "";
                else
                    cgt.fixed_mark = "*";
                cgt.image_id = ImageIcon.get_icon_id_by_catalog_group_id(cg.catalog_group_id);
                sp_list_image_Result ii = g.sp_image_list.Find(e => e.image_id == cgt.image_id);
                if (ii != null)
                    cgt.image_file_path = string.Format("/I2MS2;component/Icons/{0}", ii.file_name);
                else
                    cgt.image_file_path = string.Format("/I2MS2;component/Icons/{0}", "null_16.png");

                if (cg.catalog_level == 1)
                {
                    // GS_DEL GS 인증을 위한 카탈로그 정리
                    //bool aa = _gs_del_list.Contains(cg.catalog_group_id);
                    //if (!aa)
                        _catalog_group_tree.Add(cgt);
                }

                if (cg.catalog_level == 2)
                {
                    // GS_DEL GS 인증을 위한 카탈로그 정리
                    //bool aa = _gs_del_list.Contains(cg.catalog_group_id);
                    //if(!aa)
                        _catalog_group_tree.Last().node_list.Add(cgt);
                }
            }
            _tree.ItemsSource = _catalog_group_tree;
        }


        // 우측 편집 창에 내용을 출력한다.
        private void dispDetail(CatalogGroupTree _cgt)
        {
            if (_cgt == null)
                return;

            if (_cgt.catalog_group_id == 0)
                return;

            int catalog_group_id = _cgt.catalog_group_id;
            _catalog_list = g.catalog_list.Where(p => p.catalog_group_id == catalog_group_id).ToList();

            _lvCatalog.ItemsSource = _catalog_list;            
        }

        private void dispDetail2(catalog cc)
        {
            showCatalog(cc);
            unselectAllExclusiveCatalog();
            selectExclusiveCatalog(cc.catalog_group_id);
            showExclusiveProperty(cc);
            showRackMountProperty(cc);
            showExtendedProperty(cc);
            showIncludedAssetList(cc.catalog_id);
        }

        private void showCatalog(catalog cc)
        {
            txtCatalogName.Text = cc.catalog_name;
            txtCatalogFullName.Text = cc.catalog_full_name;
            cboManufacture.SelectedValue = cc.manufacture_id;
            txtModel.Text = cc.model_code;
            txtOrderNumber.Text = cc.order_code;
            txtWidth.Text = cc.size_w.ToString();
            txtDepth.Text = cc.size_d.ToString();
            txtHeight.Text = cc.size_h.ToString();
            txtWeight.Text = cc.weight.ToString();
            txtNumberOfPorts.Text = cc.num_of_ports.ToString();
            txtRemarks.Text = cc.remarks;
            _disp_image_id = cc.image_id ?? 0;

            try
            {
                sp_list_image_Result list = g.sp_image_list.Find(p => p.image_id == cc.image_id);
                if (list != null)
                {
                    string ss = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, list.folder_name, list.file_name);
                    ImageSource source = new BitmapImage(new Uri(ss, UriKind.Absolute));
                    imgImage.Source = source;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("Exception error: code={0}, {1}", ee.HResult, ee.Message);
            }
        }

        private void showExclusiveProperty(catalog cc)
        {
            chk_pp_1.IsChecked = cc.pp_use_intelligent == "Y";
            rdo_pp_2_1.IsChecked = cc.pp_config_type == "X";
            rdo_pp_2_2.IsChecked = cc.pp_config_type == "I";
            rdo_pp_3_1.IsChecked = cc.pp_media_type == "U";
            rdo_pp_3_2.IsChecked = cc.pp_media_type == "F";
            rdo_pp_4_1.IsChecked = cc.pp_utp_jack_type != "M";
            rdo_pp_4_2.IsChecked = cc.pp_utp_jack_type == "M";
            rdo_pp_5_1.IsChecked = cc.pp_utp_shield_type != "Y";
            rdo_pp_5_2.IsChecked = cc.pp_utp_shield_type == "Y";
            rdo_pp_6_1.IsChecked = cc.pp_figure_type == "F";
            rdo_pp_6_2.IsChecked = cc.pp_figure_type == "A";

            txt_ic_1.Text = cc.ic_num_of_pp_connectors.ToString();
            chk_ic_2.IsChecked = cc.ic_num_of_power == 2;  // 2016-09-30 전원이중화 비교 1 - > 2로 변경 

            txt_st_1.Text = cc.st_num_of_disk.ToString();

            rdo_sw_1_1.IsChecked = cc.sw_figure_type == "E";
            rdo_sw_1_2.IsChecked = cc.sw_figure_type == "S";
            rdo_sw_1_3.IsChecked = cc.sw_figure_type == "C";
            txt_sw_2.Text = cc.sw_num_of_slots.ToString();
            rdo_sw_3_1.IsChecked = cc.sw_model_type == "C";
            rdo_sw_3_2.IsChecked = cc.sw_model_type == "D";
            rdo_sw_3_3.IsChecked = cc.sw_model_type == "E";
            rdo_sw_3_4.IsChecked = cc.sw_model_type == "A";

            rdo_cp_1_1.IsChecked = cc.cp_plug_side == "N";
            rdo_cp_1_2.IsChecked = cc.cp_plug_side == "R";
            rdo_cp_1_3.IsChecked = cc.cp_plug_side == "B";

            chk_ca_1.IsChecked = cc.ca_use_intelligent == "Y";
            rdo_ca_2_1.IsChecked = cc.ca_install_type == "F";
            rdo_ca_2_2.IsChecked = cc.ca_install_type == "P";
            rdo_ca_2_3.IsChecked = cc.ca_install_type == "I";
            rdo_ca_2_4.IsChecked = cc.ca_install_type == "X";
            rdo_ca_3_1.IsChecked = cc.ca_for_army != "Y";
            rdo_ca_3_2.IsChecked = cc.ca_for_army == "Y";
            rdo_ca_4_1.IsChecked = cc.ca_media_type == "U";
            rdo_ca_4_2.IsChecked = cc.ca_media_type == "F";
            rdo_ca_5_1.IsChecked = cc.ca_is_utp_shield == "N";
            rdo_ca_5_2.IsChecked = cc.ca_is_utp_shield == "Y";
            cbo_ca_6.Text = CatalogType.getUtpCableTypeName(cc.ca_utp_cable_type);
            cbo_ca_7.Text = CatalogType.getFiberCableTypeName(cc.ca_fiber_cable_type);
            cbo_ca_8.Text = CatalogType.getFiberConnectorTypeName(cc.ca_fiber_connector_type);
            uint rgba = (uint) (cc.ca_disp_color_rgb ?? 0x00ffffff); // Tranparent
            Color color = CatalogType.get_color_rgba(rgba);
            rect_ca_9_1.Fill = new SolidColorBrush(color); 
            txt_ca_10.Text = cc.ca_disp_name; 

        }

        private void showRackMountProperty(catalog cc)
        {
            chk_rm_1.IsChecked = cc.rm_is_rack_mount == "Y";
            txt_rm_2.Text = cc.rm_unit_size.ToString();
            _disp_220_image_id = cc.rm_image_220_image_id ?? 0;
            _disp_440_image_id = cc.rm_image_440_image_id ?? 0;
            _disp_880_image_id = cc.rm_image_880_image_id ?? 0;

            dispImage(_disp_220_image_id, _img_rm_3_1);
            dispImage(_disp_440_image_id, _img_rm_3_2);
        }

        private void showExtendedProperty(catalog cc)
        {
            int catalog_id = cc.catalog_id;

            _catalog_ext_vm_list = (from ce in g.ext_property_list
                                    select new CatalogExtVM()
                                    {
                                        ext_id = ce.ext_id,
                                        ext_length = ce.ext_length,
                                        ext_name = ce.ext_name,
                                        ext_type = ce.ext_type,
                                        remarks = ce.remarks
                                    }).ToList();

            foreach(var aa in _catalog_ext_vm_list)
            {
                var ce = g.catalog_ext_list.Find(p => (p.ext_id == aa.ext_id) && (p.catalog_id == catalog_id));
                if (ce != null)
                {
                    aa.catalog_ext_id = ce.catalog_ext_id;
                    aa.is_checked = true;
                    aa.is_registered = true;
                }

            }
            _lvExtended.ItemsSource = null;
            _lvExtended.ItemsSource = _catalog_ext_vm_list;
        }

        private void showIncludedAssetList(int catalog_id)
        {
            // 사이트와 관계없이 모두....출력
            if (_rdoSite1.IsChecked == true)
            {
                var list = Etc.get_asset_tree_vm_ex_list(0, true, catalog_id, false);
                try
                {
                    if (_lvAssetList != null)
                    {
                        _lvAssetList.ItemsSource = null;
                        _lvAssetList.ItemsSource = list;
                    }
                }
                catch (Exception) { }
            }
            else
            {
                var list = Etc.get_asset_tree_vm_ex_list(g.selected_site_id, false, catalog_id, false);
                try
                {
                    if (_lvAssetList != null)
                    {
                        _lvAssetList.ItemsSource = null;
                        _lvAssetList.ItemsSource = list;
                    }
                }
                catch (Exception) { }
            }
        }

        private void _rdoSite1_Checked(object sender, RoutedEventArgs e)
        {
            showIncludedAssetList(_catalog_id);
        }
        private void _rdoSite1_Unchecked(object sender, RoutedEventArgs e)
        {
            showIncludedAssetList(_catalog_id);
        }
        private void _rdoSite2_Checked(object sender, RoutedEventArgs e)
        {
            showIncludedAssetList(_catalog_id);
        }
        private void _rdoSite2_Unchecked(object sender, RoutedEventArgs e)
        {
            showIncludedAssetList(_catalog_id);
        }

        private void unselectAllExclusiveCatalog()
        {
            _grid_pp.IsEnabled = false;
            _grid_pp.Visibility = Visibility.Hidden;

             _grid_ic.IsEnabled = false;
            _grid_ic.Visibility = Visibility.Hidden;

            _grid_st.IsEnabled = false;
            _grid_st.Visibility = Visibility.Hidden;

            _grid_sw.IsEnabled = false;
            _grid_sw.Visibility = Visibility.Hidden;

            _grid_cp.IsEnabled = false;
            _grid_cp.Visibility = Visibility.Hidden;

            _grid_ca.IsEnabled = false;
            _grid_ca.Visibility = Visibility.Hidden;
        }

        private void selectExclusiveCatalog(int catalog_group_id)
        {
            if (CatalogType.is_pp_group(catalog_group_id))
            {
                _grid_pp.IsEnabled = true;
                _grid_pp.Visibility = Visibility.Visible;
                return;
            }

            if (CatalogType.is_ic_group(catalog_group_id))
            {
                _grid_ic.IsEnabled = true;
                _grid_ic.Visibility = Visibility.Visible;
                return;
            }

            if (CatalogType.is_sw_group(catalog_group_id))
            {
                _grid_sw.IsEnabled = true;
                _grid_sw.Visibility = Visibility.Visible;
                return;
            }

            if (CatalogType.is_st_group(catalog_group_id))
            {
                _grid_st.IsEnabled = true;
                _grid_st.Visibility = Visibility.Visible;
                return;
            }

            if (CatalogType.is_cp_group(catalog_group_id))
            {
                _grid_cp.IsEnabled = true;
                _grid_cp.Visibility = Visibility.Visible;
                return;
            }

            if (CatalogType.is_ca_group(catalog_group_id))
            {
                _grid_ca.IsEnabled = true;
                _grid_ca.Visibility = Visibility.Visible;
                return;
            }
        }

        private double get_timestamp(byte[] bytes)
        {
            double result = 0;
            double inter = 0;
            for (int i = 0; i < bytes.Length; i++)
            {

                inter = System.Convert.ToDouble(bytes[i]);
                inter = inter * Math.Pow(2, ((7 - i) * 8));
                result += inter;
            }
            return result;
        }

        private DateTime ConvertFromTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        private double ConvertToTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        #endregion

        #region refresh 로직,  diplay 로직, clear control 로직, 화면 컨트롤 enable
        // 트리 로드
        private void _tree_Loaded(object sender, RoutedEventArgs e)
        {
            int n = 0;
            foreach (CatalogGroupTree cgt in _tree.Items)
            {
                TreeViewItem item = _tree.ItemContainerGenerator.ContainerFromIndex(n++) as TreeViewItem;
                item.IsExpanded = true;
            }
        }
        // 트리 선택 후
        private void _tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem tvi = (TreeViewItem)_tree.ItemContainerGenerator.ContainerFromIndex(_tree.Items.CurrentPosition);

            clearControl();
            _cgt = (CatalogGroupTree)_tree.SelectedItem;

            if (_cgt == null)
                return;

            _new = true;
            dispDetail(_cgt);

            // 무조건 첫 번째것을 선택한다.
            _lvCatalog.SelectedIndex = 0;
        }
        // 카탈로그 리스트 선택시 
        private void _lvCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _c = (catalog)_lvCatalog.SelectedItem;

            if (_c == null)
            {
                _new = true;
                _delete = false;
                _edit = false;
                _save = false;
                _cancel = false;
                return;
            }

            clearControl();
            enableControl(false);
            dispDetail2(_c);

            _catalog_id = _c.catalog_id;

            _new = true;
            _delete = true;
            _edit = true;
            _save = false;
            _cancel = false;
        }
        #endregion

        #region 저장 / 수정 / 삭제 
        private async Task<bool> saveData()
        {
            string name = txtCatalogName.Text.Trim();
            catalog cc = null;

            // 신규 상태에서 저장 버튼을 누른 경우
            if (_catalog_id == 0)
            {
                cc = g.catalog_list.Find(p => p.catalog_name == name);
                if (cc != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Duplicated_Catalog"));
                    return false;
                }

                _c = new catalog() { catalog_id = 0 };
                if (!updateData(true))
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show(g.tr_get("C_Error_Empty_Catalog_Name"));
                    return false;
                }

                // 랙 마운트 장치의 경우 랙 유닛 사이즈가 0이 아니어야 함
                if (CatalogType.is_rack_mountable_group(_cgt.catalog_group_id))
                {
                    if (CatalogType.is_sw_group(_cgt.catalog_group_id) && _c.sw_figure_type == "C")
                    {
                        // 스위치카드는 랙마운트가 되면 안된다.
                        if (_c.rm_is_rack_mount == "Y")
                        {
                            MessageBox.Show(g.tr_get("C_Error_Rack_Mount_2"));
                            return false;
                        }
                    }
                    else
                    {
                        if (_c.rm_is_rack_mount != "Y")
                        {
                            MessageBox.Show(g.tr_get("C_Error_Rack_Mount"));
                            return false;
                        }

                        if (_c.rm_unit_size < 1)
                        {
                            MessageBox.Show(g.tr_get("C_Error_Rack_Unit_Size"));
                            return false;
                        }
                    }
                }

                if (CatalogType.is_sw_group(_cgt.catalog_group_id))
                {
                    if ((_c.sw_figure_type != "E") && (_c.sw_figure_type != "S") && (_c.sw_figure_type != "C"))
                    {
                            MessageBox.Show(g.tr_get("C_Error_Switch_Figure_Type"));
                            return false;
                    }
                }

                // 스위치 샤시의 경우 포트수가 0 이어야 함
                if (CatalogType.is_sw_group(_cgt.catalog_group_id) && (_c.sw_figure_type == "S"))
                {
                    if (_c.num_of_ports > 0)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Ports_Be_0"));
                        return false;
                    }

                    if (_c.sw_num_of_slots < 1)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Needs_Slots"));
                        return false;
                    }
                }

                if (CatalogType.is_sw_group(_cgt.catalog_group_id))
                {
                     if ((_c.sw_figure_type == "E") || (_c.sw_figure_type == "C"))
                     {
                         if (_c.num_of_ports < 1)
                         {
                             MessageBox.Show(g.tr_get("C_Error_Ports_Not_Be_0"));
                             return false;
                         }
                     }
                }

                if (CatalogType.is_num_of_ports_group(_cgt.catalog_group_id)) 
                {
                    if ((_c.num_of_ports < 1) && (_c.sw_figure_type != "S"))
                    {
                        MessageBox.Show(g.tr_get("C_Error_Ports_Not_Be_0"));
                        return false;
                    }
                }

                _c.icon_16_image_id = ImageIcon.get_icon_id_by_catalog_group_id(_c.catalog_group_id);
                _c.link_80_image_id = ImageIcon.get_link80_id_by_catalog_group_id(_c.catalog_group_id);

                cc = (catalog) await g.webapi.post("catalog", _c, typeof(catalog));
                if (cc == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                g.catalog_list.Add(cc);
                _catalog_id = cc.catalog_id;

                await saveCatalogExt(cc);
            }
            else
            {
                if (!updateData(false))
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                _c.catalog_ext = null;
                _c.catalog_group = null;
                _c.manufacture = null;
                int r = await g.webapi.put("catalog", _catalog_id, _c, typeof(catalog));
                if (r != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                                    
                await saveCatalogExt(_c);
            }

            return true;
        }

        private async Task<bool> saveCatalogExt(catalog cc)
        {

            foreach(var cevm in _catalog_ext_vm_list)
            {
                if (cevm.is_registered && !cevm.is_checked)
                {
                    // 삭제
                    int rr1 = await g.webapi.delete("catalog_ext", cevm.catalog_ext_id);
                    if (rr1 != 0)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return false;
                    }

                    catalog_ext ce = g.catalog_ext_list.Find(p => p.catalog_ext_id == cevm.catalog_ext_id);
                    if (ce != null)
                        g.catalog_ext_list.Remove(ce);
                }
                if (!cevm.is_registered && cevm.is_checked)
                {
                    // 추가

                    catalog_ext ce = new catalog_ext() { 
                                 catalog_id = cc.catalog_id,
                                 ext_id = cevm.ext_id
                    };

                    ce = (catalog_ext)await g.webapi.post("catalog_ext", ce, typeof(catalog_ext));
                    if (ce == null)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return false;
                    }
                    g.catalog_ext_list.Add(ce);
                }          
            }
            return true;
        }

        private bool updateData(bool new_flag)
        {
            if (_c == null)
                return false;
            _c.catalog_name = txtCatalogName.Text.Trim();
            _c.catalog_full_name = txtCatalogFullName.Text.Trim();
            _c.manufacture_id = (Nullable<int>) cboManufacture.SelectedValue;
            _c.model_code = txtModel.Text.Trim();
            _c.order_code = txtOrderNumber.Text.Trim();
            int ii = 0;
            int.TryParse(txtWidth.Text, out ii);
            _c.size_w = ii;
            ii = 0;
            int.TryParse(txtDepth.Text, out ii);
            _c.size_d = ii;
            ii = 0;
            int.TryParse(txtHeight.Text, out ii);
            _c.size_h = ii;
            decimal dd = 0;
            decimal.TryParse(txtWeight.Text, out dd);
            _c.weight = dd;
            ii = 0;
            int.TryParse(txtNumberOfPorts.Text, out ii);
            _c.num_of_ports = ii;
            _c.remarks = txtRemarks.Text.Trim();

            if (new_flag)
            {
                _c.catalog_group_id = _cgt.catalog_group_id;
                _c.deletable = "Y";
            }

            _c.pp_use_intelligent = chk_pp_1.IsChecked == true ? "Y" : "N";
            _c.pp_config_type = rdo_pp_2_1.IsChecked == true ? "X" : "-";
            _c.pp_config_type = rdo_pp_2_2.IsChecked == true ? "I" : _c.pp_config_type;
            _c.pp_media_type = rdo_pp_3_1.IsChecked == true ? "U" : "-";
            _c.pp_media_type = rdo_pp_3_2.IsChecked == true ? "F" : _c.pp_media_type;
            _c.pp_utp_jack_type = rdo_pp_4_1.IsChecked == true ? "N" : "-";
            _c.pp_utp_jack_type = rdo_pp_4_2.IsChecked == true ? "M" : _c.pp_utp_jack_type;
            _c.pp_utp_shield_type = rdo_pp_5_1.IsChecked == true ? "N" : "-";
            _c.pp_utp_shield_type = rdo_pp_5_2.IsChecked == true ? "M" : _c.pp_utp_shield_type;
            _c.pp_figure_type = rdo_pp_6_1.IsChecked == true ? "F" : "-";
            _c.pp_figure_type = rdo_pp_6_2.IsChecked == true ? "A" : _c.pp_figure_type;
            ii = 0;     
            int.TryParse(txt_ic_1.Text, out ii);
            _c.ic_num_of_pp_connectors = ii;
            _c.ic_num_of_power = chk_ic_2.IsChecked == true ? 2 : 1;
            ii = 0;
            int.TryParse(txt_st_1.Text, out ii);
            _c.st_num_of_disk = ii;
            _c.sw_figure_type = rdo_sw_1_1.IsChecked == true ? "E" : "-";
            _c.sw_figure_type = rdo_sw_1_2.IsChecked == true ? "S" : _c.sw_figure_type;
            _c.sw_figure_type = rdo_sw_1_3.IsChecked == true ? "C" : _c.sw_figure_type;
            _c.sw_model_type = rdo_sw_3_1.IsChecked == true ? "C" : "-";
            _c.sw_model_type = rdo_sw_3_2.IsChecked == true ? "D" : _c.sw_model_type;
            _c.sw_model_type = rdo_sw_3_3.IsChecked == true ? "E" : _c.sw_model_type;
            _c.sw_model_type = rdo_sw_3_4.IsChecked == true ? "A" : _c.sw_model_type;
            ii = 0;
            int.TryParse(txt_sw_2.Text, out ii);
            _c.sw_num_of_slots = ii;
            _c.cp_plug_side = rdo_cp_1_1.IsChecked == true ? "N" : "-";
            _c.cp_plug_side = rdo_cp_1_2.IsChecked == true ? "R" : _c.cp_plug_side;
            _c.cp_plug_side = rdo_cp_1_3.IsChecked == true ? "B" : _c.cp_plug_side;
            _c.ca_use_intelligent = chk_ca_1.IsChecked == true ? "Y" : "N";
            _c.ca_install_type = rdo_ca_2_1.IsChecked == true ? "F" : "-";
            _c.ca_install_type = rdo_ca_2_2.IsChecked == true ? "P" : _c.ca_install_type;
            _c.ca_install_type = rdo_ca_2_3.IsChecked == true ? "I" : _c.ca_install_type;
            _c.ca_install_type = rdo_ca_2_4.IsChecked == true ? "X" : _c.ca_install_type;
            //_c.ca_install_type = CatalogType.is_patch_cord_by_cg(_cgt.catalog_group_id) == true ? "P" : "F";
            _c.ca_for_army = rdo_ca_3_1.IsChecked == true ? "N" : "-";
            _c.ca_for_army = rdo_ca_3_2.IsChecked == true ? "Y" : _c.ca_for_army;
            _c.ca_media_type = rdo_ca_4_1.IsChecked == true ? "U" : "-";
            _c.ca_media_type = rdo_ca_4_2.IsChecked == true ? "F" : _c.ca_media_type;
            _c.ca_is_utp_shield = rdo_ca_5_1.IsChecked == true ? "N" : "-";
            _c.ca_is_utp_shield = rdo_ca_5_2.IsChecked == true ? "Y" : _c.ca_is_utp_shield;
            _c.ca_utp_cable_type = CatalogType.getUtpCableType(cbo_ca_6.Text);
            _c.ca_fiber_cable_type = CatalogType.getFiberCableType(cbo_ca_7.Text);
            _c.ca_fiber_connector_type = CatalogType.getFiberConnectorType(cbo_ca_8.Text); 
            SolidColorBrush br1 = rect_ca_9_1.Fill as SolidColorBrush;
            Color c1 = br1.Color;
            int c2 = c1.A * 65536 * 256 + c1.R * 65536 + c1.G * 256 + c1.B;
            _c.ca_disp_color_rgb = c2;
            _c.ca_disp_name = txt_ca_10.Text.Trim();

            _c.rm_is_rack_mount = (chk_rm_1.IsChecked ?? false) ? "Y" : "N";

            _c.image_id = _disp_image_id;
            _c.rm_image_220_image_id = _disp_220_image_id;
            _c.rm_image_440_image_id = _disp_440_image_id;
            _c.rm_image_880_image_id = _disp_880_image_id; 
            ii = 0;
            int.TryParse(txt_rm_2.Text, out ii);
            _c.rm_unit_size = ii;
            _c.last_updated = DateTime.Now;

            return true;
        }


        private async Task<bool> deleteCatalog()
        {

            int delete_id = _catalog_id;

            int rr1 = await g.webapi.delete("catalog", delete_id);
            if (rr1 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            catalog cc = g.catalog_list.Find(p => p.catalog_id == delete_id);
            if (cc == null)
                return false;

            g.catalog_list.Remove(cc);
            _catalog_list.Remove(cc);

            return true;
        }
        #endregion

        #region 화면 지원용 
        private void clearControl()
        {
            _disp_image_id = 0;
            _disp_220_image_id = 0;
            _disp_440_image_id = 0;
            _disp_880_image_id = 0;

            txtCatalogName.Clear();
            txtCatalogFullName.Clear();
            cboManufacture.Text = "";
            txtModel.Clear();
            txtOrderNumber.Clear();
            txtWidth.Clear();
            txtDepth.Clear();
            txtHeight.Clear();
            txtWeight.Clear();
            txtNumberOfPorts.Clear();
            txtRemarks.Clear();
            imgImage.Source = null;

            chk_pp_1.IsChecked = false;
            rdo_pp_2_1.IsChecked = false;
            rdo_pp_2_2.IsChecked = false;
            rdo_pp_3_1.IsChecked = false;
            rdo_pp_3_2.IsChecked = false;
            rdo_pp_4_1.IsChecked = false;
            rdo_pp_4_2.IsChecked = false;
            rdo_pp_5_1.IsChecked = false;
            rdo_pp_5_2.IsChecked = false;
            rdo_pp_6_1.IsChecked = false;
            rdo_pp_6_2.IsChecked = false;
            txt_ic_1.Clear();
            chk_ic_2.IsChecked = false;
            txt_st_1.Clear();
            rdo_sw_1_1.IsChecked = false;
            rdo_sw_1_2.IsChecked = false;
            rdo_sw_1_3.IsChecked = false;
            txt_sw_2.Clear();
            rdo_sw_3_1.IsChecked = false;
            rdo_sw_3_2.IsChecked = false;
            rdo_sw_3_3.IsChecked = false;
            rdo_sw_3_4.IsChecked = false;
            rdo_cp_1_1.IsChecked = false;
            rdo_cp_1_2.IsChecked = false;
            rdo_cp_1_3.IsChecked = false;
            chk_ca_1.IsChecked = false;
            rdo_ca_2_1.IsChecked = false;
            rdo_ca_2_2.IsChecked = false;
            rdo_ca_2_3.IsChecked = false;
            rdo_ca_2_4.IsChecked = false;
            rdo_ca_3_1.IsChecked = false;
            rdo_ca_3_2.IsChecked = false;
            rdo_ca_4_1.IsChecked = false;
            rdo_ca_4_2.IsChecked = false;
            rdo_ca_5_1.IsChecked = false;
            rdo_ca_5_2.IsChecked = false;
            cbo_ca_6.Text = "";
            cbo_ca_7.Text = "";
            cbo_ca_8.Text = "";
            rect_ca_9_1.Fill = new SolidColorBrush(Colors.Transparent);
            txt_ca_10.Clear();

            chk_rm_1.IsChecked = false;
            txt_rm_2.Clear();
            _img_rm_3_1.Source = null;
            _img_rm_3_2.Source = null;
            _lvExtended.ItemsSource = null;
            _lvAssetList.ItemsSource = null;
        }
        
        private void enableControl(bool flag)
        {
            txtCatalogName.IsEnabled = flag;
            txtCatalogFullName.IsEnabled = flag;
            cboManufacture.IsEnabled = flag;
            txtModel.IsEnabled = flag;
            txtOrderNumber.IsEnabled = flag;
            txtWidth.IsEnabled = flag;
            txtDepth.IsEnabled = flag;
            txtHeight.IsEnabled = flag;
            txtWeight.IsEnabled = flag;
            
            // 신규일 경우에만..
            if (_catalog_id == 0)
                txtNumberOfPorts.IsEnabled = flag;
            else
                txtNumberOfPorts.IsEnabled = false;

            txtRemarks.IsEnabled = flag;
            imgImage.IsEnabled = flag;
            btnImage.IsEnabled = flag;

            chk_pp_1.IsEnabled = flag;
            rdo_pp_2_1.IsEnabled = flag;
            rdo_pp_2_2.IsEnabled = flag;
            rdo_pp_3_1.IsEnabled = flag;
            rdo_pp_3_2.IsEnabled = flag;
            rdo_pp_4_1.IsEnabled = flag;
            rdo_pp_4_2.IsEnabled = flag;
            rdo_pp_5_1.IsEnabled = flag;
            rdo_pp_5_2.IsEnabled = flag;
            rdo_pp_6_1.IsEnabled = flag;
            rdo_pp_6_2.IsEnabled = flag;
            txt_ic_1.IsEnabled = flag;
            chk_ic_2.IsEnabled = flag;
            txt_st_1.IsEnabled = flag;
            rdo_sw_1_1.IsEnabled = flag;
            rdo_sw_1_2.IsEnabled = flag;
            rdo_sw_1_3.IsEnabled = flag;
            txt_sw_2.IsEnabled = flag;
            rdo_sw_3_1.IsEnabled = flag;
            rdo_sw_3_2.IsEnabled = flag;
            rdo_sw_3_3.IsEnabled = flag;
            rdo_sw_3_4.IsEnabled = flag;
            rdo_cp_1_1.IsEnabled = flag;
            rdo_cp_1_2.IsEnabled = flag;
            rdo_cp_1_3.IsEnabled = flag;
            chk_ca_1.IsEnabled = flag;
            rdo_ca_3_1.IsEnabled = flag;
            rdo_ca_3_2.IsEnabled = flag;
            rdo_ca_4_1.IsEnabled = flag;
            rdo_ca_4_2.IsEnabled = flag;
            rdo_ca_5_1.IsEnabled = flag;
            rdo_ca_5_2.IsEnabled = flag;
            cbo_ca_6.IsEnabled = flag;
            cbo_ca_7.IsEnabled = flag;
            cbo_ca_8.IsEnabled = flag;
            rect_ca_9_1.IsEnabled = flag;
            btn_ca_9_2.IsEnabled = flag;
            txt_ca_10.IsEnabled = flag;

            chk_rm_1.IsEnabled = flag;

            // 신규일 경우에만..
            if (_catalog_id == 0)
                txt_rm_2.IsEnabled = flag;
            else
                txt_rm_2.IsEnabled = false;

            _img_rm_3_1.IsEnabled = flag;
            _img_rm_3_2.IsEnabled = flag;
            _btn_rm_3_1.IsEnabled = flag;
            _btn_rm_3_2.IsEnabled = flag;
        }

        private void btn_ca_9_2_Click(object sender, RoutedEventArgs e)
        {
            ColorPickerDialog cPicker = new ColorPickerDialog();
            Color fill_color = (rect_ca_9_1.Fill as SolidColorBrush).Color;
            cPicker.StartingColor = fill_color == Colors.Transparent ? Colors.Blue : fill_color;
            SolidColorBrush br1 = rect_ca_9_1.Fill as SolidColorBrush;
            cPicker.Owner = this;

            bool? dialogResult = cPicker.ShowDialog();
            if (dialogResult != null && (bool)dialogResult == true)
            {
                rect_ca_9_1.Fill = new SolidColorBrush(cPicker.SelectedColor);
                //fill_color = cPicker.SelectedColor;
            }
        }

        private void _btn_rm_3_1_Click(object sender, RoutedEventArgs e)
        {
            int image_id = _c != null ? (_c.rm_image_220_image_id ?? 0) : 0;

            SelectImage window = new SelectImage(1160006, image_id);
            window.Owner = this;
            bool? b = window.ShowDialog();
            bool b1 = b ?? false;
            if (b1)
            {
                _disp_220_image_id = g.result_image_id;
                dispImage(_disp_220_image_id, _img_rm_3_1);
            }
        }

        private void _btn_rm_3_2_Click(object sender, RoutedEventArgs e)
        {
            int image_id = _c != null ? (_c.rm_image_440_image_id ?? 0) : 0;

            SelectImage window = new SelectImage(1160007, image_id);
            window.Owner = this;
            bool? b = window.ShowDialog();
            bool b1 = b ?? false;
            if (b1)
            {
                _disp_440_image_id = g.result_image_id;
                dispImage(_disp_440_image_id, _img_rm_3_2);
            }

        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            int image_id = _c != null ? (_c.image_id ?? 0) : 0;

            SelectImage window = new SelectImage(1160005, image_id);
            window.Owner = this;
            bool? b = window.ShowDialog();
            bool b1 = b ?? false;
            if (b1)
            {
                _disp_image_id = g.result_image_id;
                dispImage(_disp_image_id, imgImage);
            }
        }

        private void dispImage(int image_id, Image img)
        {
            try
            {
                sp_list_image_Result list = g.sp_image_list.Find(p => p.image_id == image_id);
                if (list != null)
                {
                    string ss = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, list.folder_name, list.file_name);
                    ImageSource source = new BitmapImage(new Uri(ss, UriKind.Absolute));
                    img.Source = source;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("Exception error: code={0}, {1}", ee.HResult, ee.Message);
            }
        }
        #endregion
    }

}
