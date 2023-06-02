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
using I2MS2.Library;
using System.Globalization;

namespace I2MS2.UserControls
{
    /// <summary>
    /// RightSideControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RightSideControl : UserControl
    {
        public delegate void PutAndPullEventHandler(object obj);
        public event PutAndPullEventHandler putAndFullEvent;

        public RightSideControl()
        {
            InitializeComponent();
        }


        private void _tgbPutIn_Checked(object sender, RoutedEventArgs e)
        {
            putAndFullEvent("r_put");
        }
        private void _tgbPutIn_Unchecked(object sender, RoutedEventArgs e)
        {
            putAndFullEvent("r_pull");
        }

        public void dispLocationProperty(int location_id)
        {
            if (location_id == 0)
                return;
            location lo = g.location_list.Find(at => at.location_id == location_id);
            if (lo == null)
                return;
            dispLocationProperty(location_id, lo.location_level);
        }
        // 우측 속성 데이터 채우기 ?? romee
        public void dispLocationProperty(int location_id, int disp_level)
        {
            location lo = g.location_list.Find(at => at.location_id == location_id);
            if (lo == null)
                return;

            if (lo != null)
            {
                g.prop_data.location_id = location_id;
                if (lo.region1_id != null)
                    g.select_region1 = g.region1_list.Find(at => at.region1_id == lo.region1_id);

                if (lo.region2_id != null)
                    g.select_region2 = g.region2_list.Find(at => at.region2_id == lo.region2_id);

                if (lo.site_id != null)
                    g.select_site = g.site_list.Find(at => at.site_id == lo.site_id);

                if (lo.building_id != null)
                    g.select_building = g.building_list.Find(at => at.building_id == lo.building_id);

                if (lo.floor_id != null)
                    g.select_floor = g.floor_list.Find(at => at.floor_id == lo.floor_id);

                if (lo.room_id != null)
                    g.select_room = g.room_list.Find(at => at.room_id == lo.room_id);

                if (lo.rack_id != null)
                {
                    g.select_rack = g.rack_list.Find(at => at.rack_id == lo.rack_id);
                    g.prop_data.location_rack_radian = g.select_rack != null ? g.select_rack.angle : 0;

                    catalog ct_l = g.catalog_list.Find(at => at.catalog_id == g.select_rack.vcm_l_catalog_id);
                    g.prop_data.location_rack_vcm_l = (ct_l != null) ? ct_l.catalog_name : "";

                    catalog ct_r = g.catalog_list.Find(at => at.catalog_id == g.select_rack.vcm_r_catalog_id);
                    g.prop_data.location_rack_vcm_r = (ct_r != null) ? ct_r.catalog_name : "";

                    catalog ct = g.catalog_list.Find(at => at.catalog_id == g.select_rack.rack_catalog_id);
                    g.prop_data.location_rack_name = (ct_r != null) ? ct.catalog_name : "";
                }

                g.prop_data.location_path = lo.location_path;
                g.prop_data.location_name = lo.location_name;
                g.prop_data.location_remarks = lo.remarks;
                //g.prop_data.location = lo.location_path;

                //bb = aa ?? 0;
                switch (disp_level)
                {
                    //region1
                    case 1:
                        break;

                    //region2 
                    case 2:
                        break;

                    //site
                    case 3:
                        break;

                    //building
                    case 4:
                        g.prop_data.location_is_building = true;
                        break;
                    //floor
                    case 5:
                        g.prop_data.location_is_floor = true;
                        break;
                    //room
                    case 6:
                        g.prop_data.location_is_room = true;
                        break;
                    //rack
                    case 7:
                        g.prop_data.location_is_rack = true;
                        break;
                    //asset
                    case 8:
                        break;


                    default:
                        return;
                }
                g.prop_data.force_changed = true;
            }
        }

        public void dispAssetProperty(int asset_id)
        {
            asset ass = g.asset_list.Find(at => at.asset_id == asset_id);
            if (ass == null)
                return;
            g.prop_data.asset_id = ass.asset_id;
            g.prop_data.asset_name = ass.asset_name;
            g.prop_data.remarks = ass.remarks;
            g.prop_data.serial_no = ass.serial_no;
            g.prop_data.install_user_name = ass.install_user_name;
            g.prop_data.install_date = ass.install_date;
            g.prop_data.last_updated = ass.last_updated;
            g.prop_data.ipv4 = ass.ipv4;
            g.prop_data.slot_no_in_rack = Etc.get_slot_no_by_asset_id(asset_id);

            // 전용 속성 표시
            asset_aux au = g.asset_aux_list.Find(p => p.asset_id == asset_id);
            if (au == null)
                return;
            g.prop_data.ic_con_id = au.ic_con_id ?? 0;
            if (CatalogType.is_ipp(ass.catalog_id))
                g.prop_data.ic_con_id = Etc.get_sys_id_by_ipp_asset_id(asset_id);

            g.prop_data.ic_asset_name = Etc.get_ic_asset_name_by_ipp_asset_id(asset_id);
            g.prop_data.pp_pp_id = Etc.get_pp_id_by_asset_id(asset_id);
            g.prop_data.sv_host_name =
            g.prop_data.sv_kind_of_os = au.sv_kind_of_os;
            g.prop_data.sv_num_of_disks = au.sv_num_of_disks ?? 0;
            g.prop_data.sv_num_of_nic = au.sv_num_of_nic ?? 0;
            g.prop_data.sv_os_ver = au.sv_os_ver;
            g.prop_data.sv_tot_disk_amount = au.sv_tot_disk_amount ?? 0;
            g.prop_data.ra_slot_no = Etc.get_slot_no(asset_id);
            g.prop_data.ra_vcm_depth = au.ra_vcm_depth ?? 0;
            g.prop_data.ra_vcm_type = Etc.get_vcm_name(au.ra_vcm_type);
            g.prop_data.st_cur_disk_amount = au.st_cur_disk_amount ?? 0;
            g.prop_data.st_cur_num_of_disks = au.st_cur_num_of_disks ?? 0;
            g.prop_data.st_type = Etc.get_storage_type_name(au.st_type);
            g.prop_data.st_type_1 = au.st_type == "S";   // Stand-alone
            g.prop_data.st_type_2 = au.st_type == "H";   // Host-connected
            //g.prop_data.sw_max_slots = au.sw_max_slots ?? 0;
            g.prop_data.sw_vlan = au.sw_vlan;

            g.prop_data.as_company = au.as_company;
            g.prop_data.as_duration = au.as_duration ?? 0;
            g.prop_data.as_end_date = au.as_end_date;
            g.prop_data.as_free_duration = au.as_free_duration ?? 0;
            g.prop_data.as_free_end_date = au.as_free_end_date;
            g.prop_data.as_free_start_date = au.as_free_start_date;
            g.prop_data.as_management_div = au.as_management_div;
            g.prop_data.as_management_user_name = au.as_management_user_name;
            g.prop_data.as_price = au.as_price ?? 0;
            g.prop_data.as_start_date = au.as_start_date;

            g.prop_data.bu_depreciation_duration = au.bu_depreciation_duration ?? 0;
            g.prop_data.bu_depreciation_end_year = au.bu_depreciation_end_year ?? 0;
            g.prop_data.bu_depreciation_start_year = au.bu_depreciation_start_year ?? 0;
            g.prop_data.bu_purchase_date = au.bu_purchase_date;
            g.prop_data.bu_purchase_user_name = au.bu_purchase_user_name;

            g.prop_data.snmp_get_community = au.snmp_get_community;
            g.prop_data.snmp_set_community = au.snmp_set_community;
            g.prop_data.snmp_version = au.snmp_version;
            g.prop_data.snmp_version1 = au.snmp_version == "1";
            g.prop_data.snmp_version2 = au.snmp_version == "2";
            g.prop_data.snmp_version3 = au.snmp_version == "3";
            g.prop_data.snmp_user = au.snmp_v3_user;
            g.prop_data.snmp_password = au.snmp_v3_password;
            g.prop_data.snmp_trap_svr_ip= au.snmp_trap_svr_ip;

            if (CatalogType.is_pc(ass.catalog_id))
                dispTerminalProperty(asset_id);

            dispExtProperty(asset_id);

            // 확장 속성 출력

            dispCatalogProperty(ass.catalog_id);
        }


        private void dispTerminalProperty(int asset_id)
        {
            var node = g.asset_terminal_list.Find(p => p.terminal_asset_id == asset_id);
            if (node != null)
            {
                g.prop_data.netbios_name = node.cur_net_bios_name;
                g.prop_data.mac = node.mac;
            }
        }
        private void dispExtProperty(int asset_id)
        {

            _spExtProp.Children.Clear();
            string ans = "";

            var ae_list = g.asset_ext_list.Where(p => p.asset_id == asset_id);

            foreach(var node in ae_list)
            {
                int ext_id = node.ext_id;
                ext_property ep = g.ext_property_list.Find(p => p.ext_id == ext_id);
                if (ep == null)
                    continue;
                string ext_type = ep.ext_type;
                int ans_no = 0;
                ext_property_ans epa = null;
                //string ans_name = "";
                switch(ext_type)
                {
                    case "T" :
                        // 텍스트
                        ans = node.ans_string;
                        break;
                    case "N":
                        // 숫자
                        ans = (node.ans_numeric ?? 0).ToString();
                        break;
                    case "D":
                        // 날짜
                        ans = node.ans_date != null ? node.ans_date.Value.ToShortDateString() : "";
                        break;
                    case "R":
                        // Radio Button
                        ans_no = node.ans_numeric ?? 0;
                        epa = g.ext_property_ans_list.Find(p => (p.ext_id == ext_id) && (p.ans_no == ans_no));
                        ans = epa != null ? epa.ans_name : "";
                        break;
                    case "L":
                        // List Box
                        ans_no = node.ans_numeric ?? 0;
                        epa = g.ext_property_ans_list.Find(p => (p.ext_id == ext_id) && (p.ans_no == ans_no));
                        ans = epa != null ? epa.ans_name : "";
                        break;
                    case "C":
                        // Check Box
                        int i = 0;
                        //int cnt = ep.num_of_ans;   디비에서 볼 필요 없음 romee 2105.09.03
                        int cnt = node.ans_string.Length;
                        string choice = node.ans_string ?? "";
                        string str1 = "";

                        for (i = 0; i < cnt; i++ )
                        {
                            if (choice.Substring(i, 1) == "1")
                            {
                                epa = g.ext_property_ans_list.Find(p => (p.ext_id == ext_id) && (p.ans_no == (i+1)));
                                if (epa != null)
                                    str1 = str1 + epa.ans_name + " ";
                            }
                        }
                        ans = str1;
                        break;
                }

                TextBox tb1 = new TextBox();
                tb1.Text = ep.ext_name;
                tb1.HorizontalAlignment = HorizontalAlignment.Stretch;
                tb1.VerticalAlignment = VerticalAlignment.Top;
                tb1.Margin = new Thickness(0, 0, 0, 0);
                tb1.Style = App.Current.Resources["I2MS_PropTextBoxStyle1"] as Style;

                TextBox tb2 = new TextBox();
                tb2.Text = ans;
                tb2.HorizontalAlignment = HorizontalAlignment.Stretch;
                tb2.VerticalAlignment = VerticalAlignment.Top;
                tb2.Margin = new Thickness(0, 0, 0, 0);
                tb2.Style = App.Current.Resources["I2MS_PropTextBoxStyle2"] as Style;
                //Grid.SetColumn(tb2, 1);

                DockPanel dp = new DockPanel();
                //DockPanel.SetDock(tb1, Dock.Top);
                //DockPanel.SetDock(tb2, Dock.Top);
                dp.Children.Add(tb1);
                dp.Children.Add(tb2);

                _spExtProp.Children.Add(dp);
            }

        }

        public void dispCatalogProperty(int catalog_id)
        {
            if (catalog_id == 0)
                return;

            catalog ct = g.catalog_list.Find(at => at.catalog_id == catalog_id);
            if (ct != null)
            {
                catalog_group cg = g.catalog_group_list.Find(at => at.catalog_group_id == ct.catalog_group_id);
                if (cg == null)
                    return;
                g.prop_data.catalog_group = cg.catalog_group_name;

                g.prop_data.catalog_id = ct.catalog_id;
                g.prop_data.catalog_name = ct.catalog_name;
                g.prop_data.catalog_model_code = ct.model_code;
                g.prop_data.catalog_full_name = ct.catalog_full_name;
                g.prop_data.rm_unit_size = ct.rm_unit_size ?? 0;
                g.prop_data.rm_is_rack_mount = ct.rm_is_rack_mount;
                g.prop_data.rm_is_rack_mount_1 = ct.rm_is_rack_mount == "Y";
                g.prop_data.rm_image_220_image_id = ct.rm_image_220_image_id ?? 0;
                g.prop_data.rm_image_440_image_id = ct.rm_image_440_image_id ?? 0;
                g.prop_data.catalog_order_code = ct.order_code;
                g.prop_data.catalog_width = ct.size_w ?? 0;
                g.prop_data.catalog_height = ct.size_h ?? 0;
                g.prop_data.catalog_depth = ct.size_d ?? 0;
                g.prop_data.catalog_number_of_port = ct.num_of_ports;
                g.prop_data.catalog_remarks = ct.remarks;
                g.prop_data.catalog_last_updated = ct.last_updated;
                sp_list_image_Result sp_img = g.sp_image_list.Find(at => at.image_id == ct.image_id);
                if (sp_img != null)
                    g.prop_data.catalog_image = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, sp_img.folder_name, sp_img.file_name);
                manufacture mu = g.manufacture_list.Find(at => at.manufacture_id == ct.manufacture_id);
                if (mu != null)
                    g.prop_data.catalog_manufacture = mu.manufacture_name;

                g.prop_data.st_num_of_disk = ct.st_num_of_disk ?? 0;
                g.prop_data.cp_plug_side = ct.cp_plug_side;
                g.prop_data.cp_plug_side_1 = ct.cp_plug_side == "N";
                g.prop_data.cp_plug_side_2 = ct.cp_plug_side == "R";
                g.prop_data.cp_plug_side_3 = ct.cp_plug_side == "B";

                g.prop_data.ic_num_of_pp = ct.ic_num_of_pp_connectors ?? 0;
                g.prop_data.ic_num_of_power = ct.ic_num_of_power ?? 0;
                g.prop_data.ic_num_of_power_1 = ct.ic_num_of_power == 2;
                g.prop_data.sw_num_of_slots = ct.sw_num_of_slots ?? 0;
                g.prop_data.sw_figure_type = ct.sw_figure_type;
                g.prop_data.sw_figure_type_1 = ct.sw_figure_type == "E";    // Embedded
                g.prop_data.sw_figure_type_2 = ct.sw_figure_type == "S";    // Sashi
                g.prop_data.sw_figure_type_3 = ct.sw_figure_type == "C";    // Card
                g.prop_data.sw_model_type_1 = ct.sw_model_type == "C";
                g.prop_data.sw_model_type_2 = ct.sw_model_type == "D";
                g.prop_data.sw_model_type_3 = ct.sw_model_type == "E";

                g.prop_data.pp_use_intelligent = ct.pp_use_intelligent;
                g.prop_data.pp_use_intelligent_1 = ct.pp_use_intelligent == "Y";
                g.prop_data.catalog_config_type_is_ic = ct.pp_config_type == "I";
                g.prop_data.catalog_config_type_is_xc = ct.pp_config_type == "X";
                g.prop_data.catalog_media_type_is_utp = ct.pp_media_type == "U";
                g.prop_data.catalog_media_type_is_fiber = ct.pp_media_type == "F";
                g.prop_data.catalog_utp_jack_type_is_modula = ct.pp_utp_jack_type == "M";
                g.prop_data.catalog_utp_jack_type_is_fixed = ct.pp_utp_jack_type == "N" || ct.pp_utp_jack_type == "-";
                g.prop_data.catalog_utp_jack_shild_is_shild = ct.pp_utp_shield_type == "Y";
                g.prop_data.catalog_utp_jack_shild_is_none = ct.pp_utp_shield_type == "N" || ct.pp_utp_shield_type == "-";
                g.prop_data.catalog_figure_type_1 = ct.pp_figure_type == "F";        // Flat
                g.prop_data.catalog_figure_type_2 = ct.pp_figure_type == "A";      // Angled
                g.prop_data.st_num_of_disk = ct.st_num_of_disk ?? 0;

                g.prop_data.ca_use_intelligent = ct.ca_use_intelligent;
                g.prop_data.ca_use_intelligent_1 = ct.ca_use_intelligent == "Y";
                g.prop_data.ca_install_type = ct.ca_install_type;
                g.prop_data.ca_install_type_1 = ct.ca_install_type == "F";  // Fixed Link
                g.prop_data.ca_install_type_2 = ct.ca_install_type == "P";  // 일반 Patch Cord
                g.prop_data.ca_install_type_3 = ct.ca_install_type == "I";  // IC용 Patch Cord
                g.prop_data.ca_install_type_4 = ct.ca_install_type == "X";  // XC용 Patch Cord
                g.prop_data.ca_for_army = ct.ca_for_army;
                g.prop_data.ca_for_army_1 = ct.ca_for_army == "N" || ct.ca_for_army == "-";
                g.prop_data.ca_for_army_2 = ct.ca_for_army == "Y";
                g.prop_data.ca_media_type = ct.ca_media_type;
                g.prop_data.ca_media_type_1 = ct.ca_media_type == "U";
                g.prop_data.ca_media_type_2 = ct.ca_media_type == "F";
                g.prop_data.ca_is_utp_shield = ct.ca_is_utp_shield;
                g.prop_data.ca_is_utp_shield_1 = ct.ca_is_utp_shield == "Y";
                g.prop_data.ca_utp_cable_type = ct.ca_utp_cable_type;
                g.prop_data.ca_utp_cable_type_name = CatalogType.getUtpCableTypeName(ct.ca_utp_cable_type);
                g.prop_data.ca_fiber_cable_type = ct.ca_fiber_cable_type;
                g.prop_data.ca_fiber_cable_type_name = CatalogType.getFiberCableTypeName(ct.ca_fiber_cable_type);
                g.prop_data.ca_fiber_connector_type = ct.ca_fiber_connector_type;
                g.prop_data.ca_fiber_connector_type_name = CatalogType.getFiberConnectorTypeName(ct.ca_fiber_connector_type);
                g.prop_data.ca_disp_color_rgb = ct.ca_disp_color_rgb ?? 0;
                g.prop_data.cable_disp_name = ct.ca_disp_name;
                g.prop_data.cable_color = CatalogType.get_color_rgba((uint)(g.prop_data.ca_disp_color_rgb));
                g.prop_data.cable_color = g.prop_data.ca_disp_color_rgb > 0 ? Colors.Black : Colors.Transparent;
            }
        }
    }

    public class GetPropertyHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            double result = 0;
            if (values == null)
                return result;

            string type = (string)values[0];
            int catalog_id = (int)values[1];
            bool flag = false;

            switch(type)
            {
                case "ICIPP":
                    flag = CatalogType.is_ic(catalog_id) || CatalogType.is_ipp(catalog_id);
                    break;
                case "IC":
                    flag = CatalogType.is_ic(catalog_id);
                    break;
                case "IPP":
                    flag = CatalogType.is_ipp(catalog_id);
                    break;
                case "PP":
                    flag = CatalogType.is_pp(catalog_id);
                    break;
                case "ST":
                    flag = CatalogType.is_st(catalog_id);
                    break;
                case "SV":
                    flag = CatalogType.is_sv(catalog_id);
                    break;
                case "RA":
                    flag = CatalogType.is_ra(catalog_id);
                    break;
                case "SW":
                    flag = CatalogType.is_sw(catalog_id);
                    break;
                case "ICA":
                    flag = CatalogType.is_ica(catalog_id);
                    break;
                case "CA":
                    flag = CatalogType.is_ca(catalog_id);
                    break;
                case "PC":
                    flag = CatalogType.is_pc(catalog_id);
                    break;
                case "RACKMOUNTABLE":
                    flag = CatalogType.is_rack_mountable(catalog_id);
                    break;
            }
            if (flag)
                result = 20;
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class GetPropertyVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (values == null)
                return Visibility.Hidden;

            string type = (string)values[0];
            int catalog_id = (int)values[1];
            bool flag = false;

            switch (type)
            {
                case "ICIPP":
                    flag = CatalogType.is_ic(catalog_id) || CatalogType.is_ipp(catalog_id);
                    break;
                case "IC":
                    flag = CatalogType.is_ic(catalog_id);
                    break;
                case "IPP":
                    flag = CatalogType.is_ipp(catalog_id);
                    break;
                case "PP":
                    flag = CatalogType.is_pp(catalog_id);
                    break;
                case "ST":
                    flag = CatalogType.is_st(catalog_id);
                    break;
                case "SV":
                    flag = CatalogType.is_sv(catalog_id);
                    break;
                case "RA":
                    flag = CatalogType.is_ra(catalog_id);
                    break;
                case "SW":
                    flag = CatalogType.is_sw(catalog_id);
                    break;
                case "ICA":
                    flag = CatalogType.is_ica(catalog_id);
                    break;
                case "CA":
                    flag = CatalogType.is_ca(catalog_id);
                    break;
                case "PC":
                    flag = CatalogType.is_pc(catalog_id);
                    break;
                case "RACKMOUNTABLE":
                    flag = CatalogType.is_rack_mountable(catalog_id);
                    break;
            }
            if (flag)
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GetPropertyHeightConverter2 : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType,
            object parameter, CultureInfo culture)
        {
            double result = 0;
            if (values == null)
                return result;

            string type = (string)values[0];
            int catalog_id = (int)values[1];
            bool flag = false;

            switch (type)
            {
                case "CA":
                    flag = CatalogType.is_ca(catalog_id);
                    break;
            }
            if (flag)
                result = 42;
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
