using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace I2MS2.Models
{
    public class PropertyData : INotifyPropertyChanged
    {
        // 2. 위치 속성

        public int location_id { get; set; }
        public string location_path { get; set; }
        public bool location_is_building { get; set; }
        public bool location_is_floor { get; set; }
        public bool location_is_room { get; set; }
        public bool location_is_rack { get; set; }
        public string location_name { get; set; }
        public string location_remarks { get; set; }
        public string location_rack_name { get; set; }
        public int location_rack_radian { get; set; }
        public string location_rack_vcm_l { get; set; }
        public string location_rack_vcm_r { get; set; }

        // 3 카달로그 속성

        public string catalog_group { get; set; }
        public int catalog_id { get; set; }
        public string catalog_name { get; set; }
        public string catalog_full_name { get; set; }
        public string catalog_order_code { get; set; }
        public string catalog_model_code { get; set; }
        public string catalog_manufacture { get; set; }
        public int catalog_width { get; set; }
        public int catalog_height { get; set; }
        public int catalog_depth { get; set; }
        public int catalog_number_of_port { get; set; }
        public string catalog_remarks { get; set; }
        public string catalog_image { get; set; }
        public DateTime? catalog_last_updated { get; set; }
        public string rm_is_rack_mount { get; set; }
        public bool rm_is_rack_mount_1 { get; set; }
        public int rm_unit_size { get; set; }
        public int rm_image_220_image_id { get; set; }
        public int rm_image_440_image_id { get; set; }
        
        public int ic_num_of_pp { get; set; }
        public int ic_num_of_power { get; set; }
        public bool ic_num_of_power_1 { get; set; }
        public string pp_use_intelligent { get; set; }
        public bool pp_use_intelligent_1 { get; set; }
        public bool catalog_config_type_is_xc { get; set; }
        public bool catalog_config_type_is_ic { get; set; }
        public bool catalog_media_type_is_utp { get; set; }
        public bool catalog_media_type_is_fiber { get; set; }
        public bool catalog_utp_jack_type_is_fixed { get; set; }
        public bool catalog_utp_jack_type_is_modula { get; set; }
        public bool catalog_utp_jack_shild_is_none { get; set; }
        public bool catalog_utp_jack_shild_is_shild { get; set; }
        public bool catalog_figure_type_1 { get; set; }
        public bool catalog_figure_type_2 { get; set; }
        public int st_num_of_disk { get; set; }
        public string sw_figure_type { get; set; }
        public bool sw_figure_type_1 { get; set; }
        public bool sw_figure_type_2 { get; set; }
        public bool sw_figure_type_3 { get; set; }
        public int sw_num_of_slots { get; set; }
        public string sw_model_type { get; set; }
        public bool sw_model_type_1 { get; set; }
        public bool sw_model_type_2 { get; set; }
        public string cp_plug_side { get; set; }
        public bool cp_plug_side_1 { get; set; }
        public bool cp_plug_side_2 { get; set; }
        public bool cp_plug_side_3 { get; set; }
        public string ca_use_intelligent { get; set; }
        public bool ca_use_intelligent_1 { get; set; }
        public string ca_install_type { get; set; }
        public bool ca_install_type_1 { get; set; }
        public bool ca_install_type_2 { get; set; }
        public bool ca_install_type_3 { get; set; }
        public bool ca_install_type_4 { get; set; }
        public string ca_for_army { get; set; }
        public bool ca_for_army_1 { get; set; }
        public bool ca_for_army_2 { get; set; }
        public string ca_media_type { get; set; }
        public bool ca_media_type_1 { get; set; }
        public bool ca_media_type_2 { get; set; }
        public string ca_is_utp_shield { get; set; }
        public bool ca_is_utp_shield_1 { get; set; }
        public string ca_utp_cable_type { get; set; }
        public string ca_utp_cable_type_name { get; set; }
        public string ca_fiber_cable_type { get; set; }
        public string ca_fiber_cable_type_name { get; set; }
        public string ca_fiber_connector_type { get; set; }
        public string ca_fiber_connector_type_name { get; set; }
        public int ca_disp_color_rgb { get; set; }
        public Color cable_color { get; set; }
        public Color cable_color2 { get; set; }
        public string cable_disp_name { get; set; }

        // 4자산속성

        public int asset_id { get; set; }
        public string asset_name { get; set; }
        public string remarks { get; set; }
        public string serial_no { get; set; }
        public string install_user_name { get; set; }
        public DateTime? install_date { get; set; }
        public DateTime? last_updated { get; set; }
        public int slot_no_in_rack { get; set; }
        public string ipv4 { get; set; }
        public int user_id { get; set; }

        public int ic_con_id { get; set; }              // pp 에서도 사용
        public string ic_asset_name { get; set; }       // pp 에서도 사용
        public int pp_pp_id { get; set; }
        public string sv_kind_of_os { get; set; }
        public string sv_os_ver { get; set; }
        public string sv_host_name { get; set; }
        public int sv_num_of_nic { get; set; }
        public int sv_tot_disk_amount { get; set; }
        public int sv_num_of_disks { get; set; }
        public string ra_vcm_type { get; set; }
        public int ra_slot_no { get; set; }
        public int ra_vcm_depth { get; set; }
        public int st_cur_num_of_disks { get; set; }
        public int st_cur_disk_amount { get; set; }
        public string st_type { get; set; }
        public bool st_type_1 { get; set; }
        public bool st_type_2 { get; set; }
        public string sw_gateway { get; set; }
        public string sw_vlan { get; set; }
        public string sw_alias { get; set; }

        public string as_management_div { get; set; }
        public string as_management_user_name { get; set; }
        public DateTime? as_free_start_date { get; set; }
        public int as_free_duration { get; set; } //개월
        public DateTime? as_free_end_date { get; set; }
        public DateTime? as_start_date { get; set; }
        public int as_duration { get; set; }//개월
        public DateTime? as_end_date { get; set; }
        public int as_price { get; set; }
        public string as_company { get; set; }

        public string bu_purchase_date { get; set; }
        public string bu_purchase_user_name { get; set; }
        public int bu_depreciation_start_year { get; set; }
        public int bu_depreciation_duration { get; set; }
        public int bu_depreciation_end_year { get; set; }

        public string snmp_get_community { get; set; }
        public string snmp_set_community { get; set; }
        public string snmp_version { get; set; }
        public bool snmp_version1 { get; set; }
        public bool snmp_version2 { get; set; }
        public bool snmp_version3 { get; set; }
        public string snmp_trap_svr_ip { get; set; }
        public string snmp_user { get; set; }
        public string snmp_password { get; set; }

        public string netbios_name { get; set; }
        public string mac { get; set; }

        public bool force_changed
        {
            get { return true; }
            set { NotifyPropertyChanged(""); }
        }

        public bool force_clear
        {
            get { return true; }
            set
            {
                //위치

                location_id = 0;
                location_path = null;
                location_is_building = false;
                location_is_floor = false;
                location_is_room = false;
                location_is_rack = false;
                location_name = null;
                location_remarks = null;
                location_rack_name = null;
                location_rack_radian = 0;
                location_rack_vcm_l = null;
                location_rack_vcm_r = null;

                // 카탈로그

                catalog_id = 0;
                catalog_group = null;
                catalog_name = null;
                catalog_full_name = null;
                catalog_manufacture = null;
                catalog_model_code = null;
                catalog_order_code = null;
                catalog_width = 0;
                catalog_height = 0;
                catalog_depth = 0;
                catalog_number_of_port = 0;
                catalog_remarks = null;
                catalog_last_updated = null;
                catalog_image = g.NULL_FILE_PATH;
                rm_unit_size = 0;
                rm_is_rack_mount = null;
                rm_is_rack_mount_1 = false;
                rm_image_220_image_id = 0;
                rm_image_440_image_id = 0;

                st_num_of_disk = 0;
                cp_plug_side = null;
                cp_plug_side_1 = false;
                cp_plug_side_2 = false;
                cp_plug_side_3 = false;
                pp_use_intelligent = null;
                pp_use_intelligent_1 = false;
                catalog_config_type_is_xc = false;
                catalog_config_type_is_ic = false;
                catalog_media_type_is_utp = false;
                catalog_media_type_is_fiber = false;
                catalog_utp_jack_type_is_fixed = false;
                catalog_utp_jack_type_is_modula = false;
                catalog_utp_jack_shild_is_none = false;
                catalog_utp_jack_shild_is_shild = false;
                catalog_figure_type_1 = false;
                catalog_figure_type_2 = false;
                ic_num_of_pp = 0;
                ic_num_of_power = 0;
                ic_num_of_power_1 = false;
                sw_num_of_slots = 0;
                sw_figure_type = null;
                sw_figure_type_1 = false;
                sw_figure_type_2 = false;
                sw_figure_type_3 = false;
                ca_use_intelligent = null;
                ca_use_intelligent_1 = false;
                ca_install_type = null;
                ca_install_type_1 = false;
                ca_install_type_2 = false;
                ca_install_type_3 = false;
                ca_install_type_4 = false;
                ca_for_army = null;
                ca_for_army_1 = false;
                ca_media_type = null;
                ca_media_type_1 = false;
                ca_media_type_2 = false;
                ca_is_utp_shield = null;
                ca_is_utp_shield_1 = false;
                ca_utp_cable_type = null;
                ca_utp_cable_type_name = null;
                ca_fiber_cable_type = null;
                ca_fiber_cable_type_name = null;
                ca_fiber_connector_type = null;
                ca_fiber_connector_type_name = null;
                ca_disp_color_rgb = 0;
                cable_color = Colors.Transparent;
                cable_color2 = Colors.Transparent;
                cable_disp_name = null;

                // 자산

                asset_id = 0;
                asset_name = null;
                remarks = null;
                serial_no = null;
                install_user_name = null;
                install_date = null;
                last_updated = null;
                ipv4 = null;
                slot_no_in_rack = 0;
                
                ic_con_id = 0;
                ic_asset_name = null;
                pp_pp_id = 0;
                sv_kind_of_os = null;
                sv_os_ver = null;
                sv_host_name = null;
                sv_num_of_nic = 0;
                sv_num_of_disks = 0;
                sv_tot_disk_amount = 0;
                st_cur_num_of_disks = 0;
                st_cur_disk_amount = 0;
                st_type = null;
                st_type_1 = false;
                st_type_2 = false;
                ra_slot_no = 0;
                ra_vcm_type = null;
                ra_vcm_depth = 0;
                sw_gateway = null;
                sw_vlan = null;
                //sw_max_slots = 0;

                as_management_div = null;
                as_management_user_name = null;
                as_free_start_date = null;
                as_free_duration = 0;
                as_free_end_date = null;
                as_start_date = null;
                as_duration = 0;
                as_end_date = null;
                as_price = 0;
                as_company = null;

                bu_purchase_date = null;
                bu_purchase_user_name = null;
                bu_depreciation_start_year = 0;
                bu_depreciation_duration = 0;
                bu_depreciation_end_year = 0;

                snmp_get_community = null;
                snmp_set_community = null;
                snmp_version = null;
                snmp_version1 = false;
                snmp_version2 = false;
                snmp_version3 = false;
                snmp_user = null;
                snmp_password = null;
                snmp_trap_svr_ip = null;

                netbios_name = null;
                mac = null;

            }
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
}
