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
using I2MS2.Models;
using I2MS2.Library;
using WebApiClient;
using System.Globalization;
using System.ComponentModel;
using System.Diagnostics;
using MahApps.Metro.Controls;

namespace I2MS2.Windows
{
    // 자산 등록 / 수정 / 삭제 처리 
    /// <summary>
    /// AssetManager.xaml에 대한 상호 작용 논리
    /// </summary>

    public partial class AssetManager : MetroWindow
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CatalogCommand = new RoutedCommand();

        private bool _new_flag = false;
        #endregion

        public catalog selected_catalog = null;
        private int _new_asset_id = 0;
        private int _new_location_id = 0;
        private PropertyData _asset_vm = new PropertyData();
        bool save_flag = false; // 다중 저장 방지용 romee 2016.05.26

        public AssetManager(int asset_id, int location_id, bool new_flag)
        {
            g.result_asset_id = 0;
            _new_flag = new_flag;
            _new_asset_id = asset_id;
            _new_location_id = location_id;
            //if (!new_flag)
                selected_catalog = Etc.get_catalog(412002);
            InitializeComponent();
            initData(asset_id);         
        }

        #region 신규,삭제 등 버튼 처리 로직
        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_asset_vm.asset_name))
            {
                e.CanExecute = false;
                return;
            }

            // 설치일자가 반드시 입력되어야 함
            if (_asset_vm.install_date == null)
            {
                e.CanExecute = false;
                return;
            }

            if (selected_catalog == null)
            {
                e.CanExecute = false;
                return;
            }

            if (save_flag) e.CanExecute = false;
            e.CanExecute = true;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (save_flag) return;
            save_flag = true;
            if (!await saveData())
            {
                save_flag = false;
                return;
            }
            try
            {
                DialogResult = true;
            }
            catch { }
            g.result_asset_id = _asset_vm.asset_id;
            Close();
            save_flag = false;
        }

        private void _cmdCatalog_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void _cmdCatalog_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (selected_catalog.catalog_id == 412002)
            {
                selected_catalog = Etc.get_catalog(412003);
            }
            else
            {
                selected_catalog = Etc.get_catalog(412002);
            }
            if (selected_catalog != null)
            {
                int catalog_id = selected_catalog.catalog_id;
                _asset_vm.catalog_id = catalog_id;
                _asset_vm.catalog_name = selected_catalog.catalog_name;
                _asset_vm.force_changed = true;

                dispExt(_new_asset_id, catalog_id);
                enableExclusive(catalog_id);
            }
        }


        #endregion

        // 창에 내용을 출력한다.
        private void initData(int asset_id)
        {
            PropertyData v = _asset_vm;
            this.DataContext = _asset_vm;

            if (_new_flag)
            {
                v.install_date = DateTime.Now;
                v.sw_vlan = "1";
                return;
            }

            var a = g.asset_list.Find(p => p.asset_id == asset_id);
            if (a == null)
            {
                v.install_date = DateTime.Now;
                return;
            }

            var c = g.catalog_list.Find(p => p.catalog_id == a.catalog_id);
            if (c == null)
                return;

            var au = g.asset_aux_list.Find(p => p.asset_id == asset_id);
            if (au == null)
                return;

            dispExt(asset_id, c.catalog_id);
            enableExclusive(c.catalog_id);
            convertAsset2VM(_asset_vm, a, au, c);
            _asset_vm.force_changed = true;
        }

        private void enableExclusive(int catalog_id)
        {
            _gridController.Visibility = Visibility.Hidden;
            _gridServer.Visibility = Visibility.Hidden;
            _gridRack.Visibility = Visibility.Hidden;
            _gridStorage.Visibility = Visibility.Hidden;
            _gridSwitch.Visibility = Visibility.Hidden;

            if (CatalogType.is_ic(catalog_id))
                _gridController.Visibility = Visibility.Visible;

            if (CatalogType.is_sv(catalog_id))
                _gridServer.Visibility = Visibility.Visible;

            if (CatalogType.is_ra(catalog_id))
                _gridRack.Visibility = Visibility.Visible;

            if (CatalogType.is_st(catalog_id))
                _gridStorage.Visibility = Visibility.Visible;

            if (CatalogType.is_sw(catalog_id))
                _gridSwitch.Visibility = Visibility.Visible;
        }

        private void dispExt(int asset_id, int catalog_id)
        {
            var ce_list = g.catalog_ext_list.Where(p => p.catalog_id == catalog_id);
            if (ce_list == null)
                return;

            _tabExt.IsEnabled = true;
            _gridExt.Children.Clear();

            int y = 0;
            foreach(var ce in ce_list)
            {
                var ep = g.ext_property_list.Find(p => p.ext_id == ce.ext_id);
                if (ep != null)
                {
                    // ae 값이 없는 경우는 자산쪽에 데이터가 없는 것을 의미.... 없어도됨.
                    var ae = g.asset_ext_list.Find(p => (p.ext_id == ce.ext_id) && (p.asset_id == asset_id));
                    addTextBlock(ep.ext_name, ref y);

                    switch(ep.ext_type)
                    {
                        case "T" :
                            addTextBox(ae, ref y, ep.ext_length);
                            break;
                        case "N":
                            addNumericBox(ae, ref y, ep.ext_length);
                            break;
                        case "D":
                            addDatePicker(ae, ref y);
                            break;
                        case "R":
                            addRadioButton(ae, ep.ext_id, ref y);
                            break;
                        case "L":
                            addComboBox(ae, ep.ext_id, ref y);
                            break;
                        case "C":
                            addCheckBox(ae, ep.ext_id, ref y);
                            break;
                        default:
                            y += _height2;
                            break;
                    }
                }
            }
        }

        private const int _height = 23;
        private const int _height2 = _height + 5;
        private const int _width = 100;

        private void addTextBlock(string ext_name, ref int y)
        {
            TextBlock item = new TextBlock();
            item.Text = ext_name;
            item.HorizontalAlignment = HorizontalAlignment.Stretch;
            item.VerticalAlignment = VerticalAlignment.Top;
            item.Margin = new Thickness(0, y, 0, 0);
            item.Style = App.Current.Resources["I2MS_TextBlockStyle"] as Style;
            Grid.SetColumn(item, 0);
            _gridExt.Children.Add(item);
        }

        private void addTextBox(asset_ext ae, ref int y, int len)
        {
            TextBox item = new TextBox();
            if (ae != null)
                item.Text = ae.ans_string ?? "";
            item.Height = _height - 3;
            item.Margin = new Thickness(0, y, 0, 0);
            item.HorizontalAlignment = HorizontalAlignment.Stretch;
            item.VerticalAlignment = VerticalAlignment.Top;
            item.Style = App.Current.Resources["I2MS_TextBoxStyle"] as Style;
            item.MaxLength = len;
            Grid.SetColumn(item, 1);
            _gridExt.Children.Add(item);
            y += _height2;
        }

        private void addNumericBox(asset_ext ae, ref int y, int len)
        {
            TextBox item = new TextBox();
            if (ae != null)
                item.Text = ae.ans_numeric == null ? "" : ae.ans_numeric.ToString();
            item.HorizontalAlignment = HorizontalAlignment.Stretch;
            item.VerticalAlignment = VerticalAlignment.Top;
            item.Height = _height - 3;
            item.Margin = new Thickness(0, y, 0, 0);
            item.Style = App.Current.Resources["I2MS_TextBoxStyle"] as Style;
            //item.PreviewKeyDown += new KeyEventHandler(NTextBox_PreviewKeyDown);
            item.MaxLength = len;
            item.PreviewTextInput += new TextCompositionEventHandler(NTextBox_PreviewTextInput);
            Grid.SetColumn(item, 1);
            _gridExt.Children.Add(item);
            y += _height2;
        }

        private void NTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int checkVal;
            //눌려진 값의 숫자 여부를 판단한다.
            if (!int.TryParse(e.Text, out checkVal))
            {
                e.Handled = true;
            }
        }

        private void NTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((Key.D0 >= e.Key && Key.D9 <= e.Key) || (Key.NumPad0 >= e.Key && Key.NumPad9 <= e.Key) || Key.Back == e.Key)
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;
            return;
        }

        private void addDatePicker(asset_ext ae, ref int y)
        {
            DatePicker item = new DatePicker();
            if (ae != null)
                item.SelectedDate = ae.ans_date;
            item.HorizontalAlignment = HorizontalAlignment.Stretch;
            item.VerticalAlignment = VerticalAlignment.Top;
            item.Height = _height;
            item.Margin = new Thickness(0, y, 0, 0);
            Grid.SetColumn(item, 1);
            _gridExt.Children.Add(item);
            y += _height2;
        }

        // ans_numeric 필드 사용
        private void addRadioButton(asset_ext ae, int ext_id, ref int y)
        {
            int x = 0;
            var epa_list = g.ext_property_ans_list.Where(p => p.ext_id == ext_id);
            int choice = 0;
            if (ae != null)
                choice = ae.ans_numeric ?? 0;

            if (epa_list != null)
            {
                int cnt = 0;
                foreach (var epa in epa_list)
                {
                    RadioButton item = new RadioButton();
                    item.HorizontalAlignment = HorizontalAlignment.Left;
                    item.VerticalAlignment = VerticalAlignment.Top;
                    item.Height = _height;
                    item.Margin = new Thickness(x, y, 0, 0);
                    item.Style = App.Current.Resources["I2MS_RadioButtonStyle"] as Style;
                    item.Content = epa.ans_name;
                    Grid.SetColumn(item, 1);
                    if ((cnt+1) == choice)
                        item.IsChecked = true;
                    _gridExt.Children.Add(item);
                    if (++cnt % 3 == 0)
                    {
                        x = 0;
                        y += _height2;
                    }
                    else
                        x += _width;
                }
                if (cnt % 3 != 0)
                    y += _height2;     
            }
            else
                y += _height2;
        }

        // ans_numeric 필드 사용
        private void addComboBox(asset_ext ae, int ext_id, ref int y)
        {
            ComboBox item = new ComboBox();
            item.HorizontalAlignment = HorizontalAlignment.Stretch;
            item.VerticalAlignment = VerticalAlignment.Top;
            item.Height = _height;
            item.Margin = new Thickness(0, y, 0, 0);
            Grid.SetColumn(item, 1);
            ComboBoxItem it = new ComboBoxItem();
            it.Style = App.Current.Resources["I2MS_ComboboxItemStyle"] as Style;
            it.Content = "-- Select --";
            item.Items.Add(it);
            int choice = 0;
            if (ae != null)
                choice = ae.ans_numeric ?? 0;

            var epa_list = g.ext_property_ans_list.Where(p => p.ext_id == ext_id);
            if (epa_list != null)
            {
                foreach (var epa in epa_list)
                {
                    ComboBoxItem item2 = new ComboBoxItem();
                    item2.Style = App.Current.Resources["I2MS_ComboboxItemStyle"] as Style;
                    item2.Content = epa.ans_name;
                    item.Items.Add(item2);
                }
                _gridExt.Children.Add(item);
            }

            if (ae != null)
                item.SelectedIndex = choice;
            else
                item.SelectedIndex = 0;

            y += _height2;
        }

        // ans_string 필드 사용   10111 <===    true, false, true, true, true
        private void addCheckBox(asset_ext ae, int ext_id, ref int y)
        {
            int x = 0;
            string choice = "";
            if (ae != null)
                choice = ae.ans_string;
            if (choice == null)
                choice = "";
            var epa_list = g.ext_property_ans_list.Where(p => p.ext_id == ext_id);
            if (epa_list != null)
            {
                int cnt = 0;
                foreach (var epa in epa_list)
                {
                    CheckBox item = new CheckBox();
                    if (choice.Length > cnt)
                    {
                        if (choice.Substring(cnt, 1) == "1")
                            item.IsChecked = true;
                    }
                    item.HorizontalAlignment = HorizontalAlignment.Left;
                    item.VerticalAlignment = VerticalAlignment.Top;
                    item.Height = _height;
                    item.Margin = new Thickness(x, y, 0, 0);
                    item.Style = App.Current.Resources["I2MS_CheckBoxStyle"] as Style;
                    item.Content = epa.ans_name;
                    Grid.SetColumn(item, 1);
                    _gridExt.Children.Add(item);
                    if (++cnt % 3 == 0)
                    {
                        x = 0;
                        y += _height2;
                    }
                    else
                        x += _width;
                }
                if (cnt % 3 != 0)
                    y += _height2;
            }
            else
                y += _height2;
        }

        private void convertAsset2VM(PropertyData v, asset a, asset_aux au, catalog c)
        {
            v.asset_id = a.asset_id;
            v.asset_name= a.asset_name;
            v.install_date = a.install_date;
            v.remarks = a.remarks;
            v.serial_no = a.serial_no;
            v.install_user_name = a.install_user_name;
            v.user_id = a.user_id;
            v.ipv4 = a.ipv4;

            v.ic_con_id = au.ic_con_id ?? 0;
            v.sv_kind_of_os = au.sv_kind_of_os;
            v.sv_os_ver = au.sv_os_ver;
            v.sv_num_of_disks = au.sv_num_of_disks ?? 0;
            v.sv_tot_disk_amount = au.sv_tot_disk_amount ?? 0;
            v.sv_num_of_disks = au.sv_num_of_disks ?? 0;
            v.ra_vcm_depth = au.ra_vcm_depth ?? 0;
            v.ra_vcm_type = au.ra_vcm_type;
            v.st_cur_num_of_disks = au.st_cur_num_of_disks ?? 0;
            v.st_cur_disk_amount = au.st_cur_disk_amount ?? 0;
            v.st_type = au.st_type;
            v.st_type_1 = au.st_type == "S";
            v.st_type_2 = au.st_type == "H";
            v.sw_vlan = au.sw_vlan;
            v.sw_alias = au.sw_alias;

            v.as_management_div = au.as_management_div;
            v.as_management_user_name = au.as_management_user_name;
            v.as_free_start_date = au.as_free_start_date;
            v.as_free_duration = au.as_free_duration == null ? 0 : au.as_free_duration.Value; 
            v.as_free_end_date = au.as_free_end_date;
            v.as_start_date = au.as_start_date;
            v.as_duration =  au.as_duration == null ? 0 : au.as_duration.Value; 
            v.as_end_date = au.as_end_date;
            v.as_price = au.as_price ?? 0;
            v.as_company = au.as_company;
            v.bu_purchase_date = au.bu_purchase_date;
            v.bu_purchase_user_name = au.bu_purchase_user_name;
            v.bu_depreciation_start_year = au.bu_depreciation_start_year == null ? 0 : au.bu_depreciation_start_year.Value; 
            v.bu_depreciation_duration = au.bu_depreciation_duration == null ? 0 : au.bu_depreciation_duration.Value; 
            v.bu_depreciation_end_year = au.bu_depreciation_end_year == null ? 0 : au.bu_depreciation_end_year.Value; 
            v.snmp_get_community = au.snmp_get_community;
            v.snmp_set_community = au.snmp_set_community;
            v.snmp_version = au.snmp_version;
            v.snmp_version1 = au.snmp_version == "1";
            v.snmp_version2 = au.snmp_version == "2";
            v.snmp_version3 = au.snmp_version == "3";
            v.snmp_user = au.snmp_v3_user;
            v.snmp_password = au.snmp_v3_password;
            v.snmp_trap_svr_ip = au.snmp_trap_svr_ip;
            v.catalog_id = c.catalog_id;
            v.catalog_name = c.catalog_name;
            v.location_id = _new_location_id;
        }



        private void convertVM2Asset(asset a, asset_aux au, PropertyData v)
        {
            a.asset_id = v.asset_id;
            a.asset_name = v.asset_name;
            a.install_date = v.install_date;
            a.remarks = v.remarks;
            a.serial_no = v.serial_no;
            a.install_user_name = v.install_user_name;
            a.user_id = v.user_id;
            a.ipv4 = v.ipv4;
            a.catalog_id = selected_catalog.catalog_id;
            a.location_id = v.location_id;
            au.asset_id = v.asset_id;

            au.ic_con_id = v.ic_con_id;
            au.sv_kind_of_os = v.sv_kind_of_os;
            au.sv_os_ver = v.sv_os_ver;
            au.sv_host_name = v.sv_host_name;            
            au.sv_num_of_nic = v.sv_num_of_nic;
            au.sv_num_of_disks = v.sv_num_of_disks;
            au.sv_tot_disk_amount = v.sv_tot_disk_amount;
            au.ra_vcm_depth = v.ra_vcm_depth;
            au.ra_vcm_type = v.ra_vcm_type;
            au.st_cur_num_of_disks = v.st_cur_num_of_disks;
            au.st_cur_disk_amount = v.st_cur_disk_amount;
            au.st_type = v.st_type_1 ? "S" : "H";
            au.sw_vlan = v.sw_vlan;
            au.sw_alias = v.sw_alias;

            au.as_management_div = v.as_management_div;
            au.as_management_user_name = v.as_management_user_name;
            au.as_free_start_date = v.as_free_start_date;
            au.as_free_duration = v.as_free_duration;
            au.as_free_end_date = v.as_free_end_date;
            au.as_start_date = v.as_start_date;
            au.as_duration = v.as_duration;
            au.as_end_date = v.as_end_date;
            au.as_price = v.as_price;
            au.as_company = v.as_company;
            au.bu_purchase_date = v.bu_purchase_date;
            au.bu_purchase_user_name = v.bu_purchase_user_name;
            au.bu_depreciation_start_year = v.bu_depreciation_start_year;
            au.bu_depreciation_duration = v.bu_depreciation_duration;
            au.bu_depreciation_end_year = v.bu_depreciation_end_year;
            au.snmp_get_community = v.snmp_get_community;
            au.snmp_set_community = v.snmp_set_community;
            if (v.snmp_version1)
                au.snmp_version = "1";
            if (v.snmp_version2)
                au.snmp_version = "2";
            if (v.snmp_version3)
                au.snmp_version = "3";
            au.snmp_v3_user = v.snmp_user;
            au.snmp_v3_password = v.snmp_password;
            au.snmp_trap_svr_ip = v.snmp_trap_svr_ip;
        }

        // except_asset_id는 제외하고 검사한다. (수정 시에 반드시 필요...)
        // location_id로 검색시 빌딩에 속해 있어야 한다.
        private bool is_duplicated(int location_id, int except_asset_id, string asset_name)
        {
            if (string.IsNullOrEmpty(asset_name))
                return true;
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return true;
            int building_id = l.building_id ?? 0;
            if (building_id == 0)
                return true;

            var list = from aa in g.asset_list.Where(p => (p.asset_id != except_asset_id) && (p.asset_name == asset_name))
                       join bb in g.location_list.Where(p => p.building_id == building_id) on aa.location_id equals bb.location_id
                       select new { aa.asset_name };

            if (list.Count() > 0)
                return true;

            // 같은 빌딩내에서 중복된것이 없으면...
            return false;
        }

        private async Task<bool> saveData()
        {
            int asset_id = _asset_vm.asset_id;
            int temp_catalog_id = Etc.get_catalog_id_by_asset_id(_new_asset_id);
            int catalog_id = selected_catalog != null ? selected_catalog.catalog_id : 0;
            bool sw_card_flag = CatalogType.is_sw_card(catalog_id);

            // 새시(슬롯)형스위치에 자산을 추가하는 경우 카드타입만 가능하다.
            if (_new_flag && CatalogType.is_sw_slot(temp_catalog_id) && !sw_card_flag)
            {
                MessageBox.Show(g.tr_get("C_Error_Switch_Card_1"));
                return false;
            }

            // 자산 중복 체크
            bool dup = is_duplicated(_new_location_id, asset_id, _asset_vm.asset_name);
            if (dup && _new_flag)   // 수정모드가 아닌데 중복이면 리턴 romee 2/4 
            {
                MessageBox.Show(g.tr_get("C_Error_Duplicated_Asset"));
                return false;
            }

            // 컨트롤러 ID 입력 확인
            if (CatalogType.is_ic(catalog_id))
            {
                if (_asset_vm.ic_con_id == 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Controller_ID"));
                    return false;
                }

                if (_asset_vm.ic_con_id > 999)
                {
                    // GS_DEL
                    MessageBox.Show(g.tr_get("C_Info55"));
                    return false;
                }

                var au9 = g.asset_aux_list.Find(p => (p.ic_con_id == _asset_vm.ic_con_id) && (p.asset_id != _asset_vm.asset_id));
                if (au9 != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Duplicated_Controller"));
                    return false;
                }
            }

            asset a1 = new asset();
            asset_aux au1 = new asset_aux();

            if (_new_flag)
            {
                // 랙에 랙마운트가 안되는 제품을 저장하려고 할때.... 스위치카드 포함...
                // 슬롯형스위치에 자산을 추가하려고 할 때 카드형만 가능하게...

                _asset_vm.location_id = _new_location_id;
                _asset_vm.user_id = g.login_user_id;
                convertVM2Asset(a1, au1, _asset_vm);

                try { 
                    string ip1 = a1.ipv4.Trim();
                    string ip2 = au1.snmp_trap_svr_ip.Trim();

                    if (!g.IsValidvalue(ip1, "IP"))
                    {
                        MessageBox.Show(g.tr_get("C_Info39"));
                        return false;
                    }
                    if (!g.IsValidvalue(ip2, "IP"))
                    {
                        MessageBox.Show(g.tr_get("C_Info39"));
                        return false;
                    }
                }
                catch(Exception e1)
                {
                }

                string rack_mount_type = "";       // Slot, Left, Right
                int slot_no2 = 0;
                if (!sw_card_flag)
                {
                    // 랙마운트의 경우 슬롯이 여유가 있는지 확인한다.
                    var b3 = g.left_tree_handler.check_rack_slot(_new_location_id, catalog_id, out rack_mount_type, out slot_no2);
                    if (!b3)
                    {
                        // GS_DEL
                        MessageBox.Show(g.tr_get("C_Info54"));
                        return false;
                    }
                }
                a1.is_layout = "N";
                a1.last_updated = DateTime.Now;
                var aa = (asset)await g.webapi.post("asset", a1, typeof(asset));
                if (aa == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                au1.asset_id = aa.asset_id;
                _asset_vm.asset_id = aa.asset_id;
                asset_id = aa.asset_id;
                g.asset_list.Add(aa);

                if (sw_card_flag)
                {
                    // int sw_slot_asset = 0; // 요기에 슬롯형 스위치를 ID를 얻어와야 함.
                    var list = g.sw_card_config_list.Where(p => (p.sw_asset_id == _new_asset_id) && !(p.sw_card_asset_id > 0)).OrderBy(p => p.slot_no).ToList();
                    // 스위치슬롯갯수를 초과한 경우 무시...
                    if (list.Count() != 0)
                    {
                        // 왜 처음 것을 수정할까?
                        var scc = list[0];
                        scc.sw_card_asset_id = asset_id;
                        var r = await g.webapi.put("sw_card_config", scc.sw_card_config_id, scc, typeof(sw_card_config));
                        if (r != 0)
                        {
                            MessageBox.Show(g.tr_get("C_Error_Server"));
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(g.tr_get("C_Error_Empty_Switch_Slot"));
                        return false;                    
                    }
                }
                else
                {
                    // rack_config 를 여기서 추가...
                    int rack_id = Etc.get_rack_id_by_location_id(_new_location_id);
                    if (rack_id > 0)
                    {
                        bool b4 = await g.left_tree_handler.add_to_rack_config(rack_id, rack_mount_type, slot_no2, asset_id, catalog_id);
                        if (!b4)
                            return false;
                    }
                }
            }
            else
            {
                _asset_vm.user_id = g.login_user_id;
                a1 = g.asset_list.Find(p => p.asset_id == asset_id);
                if (a1 == null)
                    return false;

                au1 = g.asset_aux_list.Find(p => p.asset_id == asset_id);
                if (au1 == null)
                    return false;

                convertVM2Asset(a1, au1, _asset_vm);

                try
                {
                    string ip1 = a1.ipv4.Trim();
                    string ip2 = au1.snmp_trap_svr_ip.Trim();

                    if (!g.IsValidvalue(ip1, "IP"))
                    {
                        MessageBox.Show(g.tr_get("C_Info39"));
                        return false;
                    }
                    if (!g.IsValidvalue(ip2, "IP"))
                    {
                        MessageBox.Show(g.tr_get("C_Info39"));
                        return false;
                    }
                }
                catch (Exception e1)
                {
                }

                a1.asset_aux = null;
                a1.ipp_connect_status = null;
                a1.ic_connect_status = null;
                a1.asset_ext.Clear();
                a1.asset_port_link.Clear();
                a1.ic_ipp_config.Clear();
                int r = await g.webapi.put("asset", a1.asset_id, a1, typeof(asset));
                if (r != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                bool b = await g.left_tree_handler.editAsset(asset_id);
                if (!b)
                    return false;

                // 다른사용자에게 알린다.
                g.signalr.send_asset_to_signalr(asset_id, eAction.eModify);
            }
                                
            au1.asset = null;
            int r2 = await g.webapi.put("asset_aux", asset_id, au1, typeof(asset_aux));
            if (r2 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            if (_new_flag)
                g.asset_aux_list.Add(au1);

            // 마지막으로 asset_ext 를 수정한다.

            if (!(await saveExtProperty(au1.asset_id, selected_catalog.catalog_id)))
            {
                // MessageBox.Show(g.tr_get("C_Error_Server"));
                return true; //  false; // 2015.04.27 ext table error 무시 
            }

            return true;
        }

        private void _btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private asset_ext get_asset_ext(int asset_id, int ext_id)
        {
            if (asset_id == 0)
                return null;
            if (ext_id == 0)
                return null;
            var ae = g.asset_ext_list.Find(p => (p.asset_id == asset_id) && (p.ext_id == ext_id));
            return ae;
        }

        private async Task<bool> saveExtProperty(int asset_id, int catalog_id)
        {
            TextBox tb2;
            DatePicker dp;
            RadioButton rb;
            CheckBox check_box;
            ComboBox combo_box;
            string ans_text = "";
            int ans_int = 0;
            DateTime? ans_date;
            int idx = 0;
            int col = 0;
            asset_ext ae = null;
            ext_property ep = null;
            int ext_id;
            string type = "";
            int ans_cnt = 0;
            int ans_idx = 0;
            int r1;

            g.catalog_ext_list = (List<catalog_ext>)await g.webapi.getList("catalog_ext", typeof(List<catalog_ext>));
            var ce_list = g.catalog_ext_list.Where(p => p.catalog_id == catalog_id).ToList();

            try { 
                foreach(var node in _gridExt.Children)
                {
                    col++;
                    //if (col == 1)  // 연속된 레디오나 체크박스는 안걸러짐 romee 2015.09.03 
                    if (col == 1 && node.GetType() == typeof(TextBlock))
                    {
                        TextBlock tb = (TextBlock) node;
                        Debug.WriteLine("Ext Property: cnt={0}, q={1}", idx, tb.Text);
                        ext_id = ce_list[idx].ext_id;
                        var ae2 = get_asset_ext(asset_id, ext_id);
                        if (ae2 == null)
                        {
                            // 자산에 asset_ext가 없는 경우 만들어 준다.
                            ae2 = new asset_ext()
                            {
                                asset_id = asset_id,
                                ext_id = ext_id,
                                catalog_id = catalog_id,
                                user_id = g.login_user_id
                            };

                            ae = (asset_ext) await g.webapi.post("asset_ext", ae2, typeof(asset_ext));
                            if (ae == null)
                                return false;
                            g.asset_ext_list.Add(ae);
                        }
                        else
                        {
                            ae = ae2;
                        }

                    }
                    else
                    {
                        if (col == 2)
                        {
                            ext_id = ae.ext_id;
                            ep = g.ext_property_list.Find(p => p.ext_id == ext_id);
                            if (ep == null)
                            {
                                col = 0;
                                idx++;
                                continue;
                            }
                            type = ep.ext_type;
                            ans_cnt = ep.num_of_ans;
                            ans_idx = 0;
                            ans_int = 0;
                            ans_text = "";
                        }
                        switch (type)
                        {
                            case "T" :
                                // 삭제 항목 부터 처리 2015.09.03 romee
                                // 기존 변경이 에러 있으면 누적됨 텍스트는 하나 이므로 모두 지우고 추가 로직으로 바꿈 
                                var node_list = g.asset_ext_list.Where(p => p.ext_id == ae.ext_id).ToList();
                                foreach (var node1 in node_list)
                                {
                                    int rr1 = await g.webapi.delete("asset_ext", node1.asset_ext_id);
                                    if (rr1 != 0)
                                    {
                                        MessageBox.Show(g.tr_get("C_Error_Server"));
                                        return false;
                                    }
                                    g.asset_ext_list.Remove(node1);
                                }

                                tb2 = (TextBox) node;
                                ans_text = tb2.Text;
                                ae.ans_string = ans_text.Substring(0, ans_text.Length > ep.ext_length ? ep.ext_length : ans_text.Length);
                                Debug.WriteLine("Ext Property: cnt={0}, type={1}, ans={2}", idx, type, ans_text);
                                //r1 = await g.webapi.put("asset_ext", ae.asset_ext_id, ae, typeof(asset_ext));
                                //if (r1 != 0)
                                //    return false;

                                var out_node = (asset_ext)await g.webapi.post("asset_ext", ae, typeof(asset_ext));
                                if (out_node == null)
                                {
                                    MessageBox.Show(g.tr_get("C_Error_Server"));
                                    return false;
                                }
                                g.asset_ext_list.Add(out_node);

                                col = 0;
                                idx++;
                                break;
                            case "N" :
                                tb2 = (TextBox) node;
                                ans_int = Etc.get_int(tb2.Text);
                                ae.ans_numeric = ans_int;
                                Debug.WriteLine("Ext Property: cnt={0}, type={1}, ans={2}", idx, type, ans_int);
                                r1 = await g.webapi.put("asset_ext", ae.asset_ext_id, ae, typeof(asset_ext));
                                if (r1 != 0)
                                    return false;
                                col = 0;
                                idx++;
                                break;
                            case "D" :
                                dp = (DatePicker) node;
                                ans_date = dp.SelectedDate;
                                ae.ans_date = ans_date;
                                Debug.WriteLine("Ext Property: cnt={0}, type={1}, ans={2}", idx, type, ans_date);
                                r1 = await g.webapi.put("asset_ext", ae.asset_ext_id, ae, typeof(asset_ext));
                                if (r1 != 0)
                                    return false;
                                col = 0;
                                idx++;
                                break;
                            case "R" :
                                rb = (RadioButton) node;
                                if (rb.IsChecked.Value)
                                    ans_int = ans_idx + 1;
                                if (++ans_idx >= ans_cnt)
                                {
                                    ae.ans_numeric = ans_int;
                                    Debug.WriteLine("Ext Property: cnt={0}, type={1}, ans={2}", idx, type, ans_int);
                                    r1 = await g.webapi.put("asset_ext", ae.asset_ext_id, ae, typeof(asset_ext));
                                    if (r1 != 0)
                                        return false;
                                    col = 0;
                                    if(ans_idx == 1)
                                        idx++;
                                }
                                break;
                            case "L" :
                                combo_box = (ComboBox) node;
                                ans_int = combo_box.SelectedIndex;
                                if (ans_int < 0)
                                    ans_int = 0;
                                ae.ans_numeric = ans_int;
                                Debug.WriteLine("Ext Property: cnt={0}, type={1}, ans={2}", idx, type, ans_int);
                                r1 = await g.webapi.put("asset_ext", ae.asset_ext_id, ae, typeof(asset_ext));
                                if (r1 != 0)
                                    return false;
                                col = 0;
                                idx++;
                                break;
                            case "C" :
                                check_box = (CheckBox) node;
                                ans_text = ans_text + (check_box.IsChecked.Value ? "1" : "0");
                                if (++ans_idx >= ans_cnt)
                                {
                                    ae.ans_string = ans_text;
                                    Debug.WriteLine("Ext Property: cnt={0}, type={1}, ans={2}", idx, type, ans_text);
                                    r1 = await g.webapi.put("asset_ext", ae.asset_ext_id, ae, typeof(asset_ext));
                                    if (r1 != 0)
                                        return false;
                                    col = 0;
                                    if (ans_idx == 1)
                                        idx++;
                                }
                                break; 
                        }
                    }
                }
            }   // try
            catch(Exception e)
            {
                Debug.WriteLine("Exception error: code={0}, msg={1}", e.HResult, e.Message);
                return false;
            }

            return true;

        }

        private void _window_Activated(object sender, EventArgs e)
        {
                int catalog_id = 412002;
                if (_asset_vm.catalog_id != 0)
                {
                }
                else
                {
                    _asset_vm.catalog_id = catalog_id;
                    _asset_vm.catalog_name = selected_catalog.catalog_name;
                }
                _asset_vm.force_changed = true;

                dispExt(_new_asset_id, catalog_id);
                enableExclusive(catalog_id);
        }
    }
}
