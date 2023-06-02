using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I2MS2.Models;
using WebApi.Models;
using System.Windows.Media;

namespace I2MS2.Library
{
    public enum  AssetTreeType{
        World,
        Region1,
        Region2,
        Site,
        Building,
        Floor,
        Room,
        Rack,
        PatchPanel,
        i_PatchPanel,
        Server,
        Cable,
        i_Cable,
        i_Controller,
        Switch,
        SwitchSlot,
        SwitchCard,
        Storage,
        ConsolidationPoint,
        FacePlate,
        MutoaBox,
        EnviromentBox,
        Port,
        UserPort,
        PC,
        EnergeBox,
        Unknown
    };


    public static class CatalogType
    {
        static List<int> _dont_add_asset_list = new List<int>() { 3150,  3210, 3220, 3340, 3460, 3470 };
        static List<int> _pc_list = new List<int>() { 3340 };
        static List<int> _ic_list = new List<int>() { 3110 };
        static List<int> _eb_list = new List<int>() { 3120 };
        static List<int> _ipp_list = new List<int>() { 3130, 3140 };
        static List<int> _ifp_list = new List<int>() { 3140 };     // romee 2016-12-05
        static List<int> _pp_list = new List<int>() { 3130, 3140, 3440, 3450 };
        static List<int> _ica_list = new List<int>() { 3150 };
        static List<int> _ca_patch_list = new List<int>() { 3150, 3460 };
        static List<int> _sv_list = new List<int>() { 3350 };
        static List<int> _ra_list = new List<int>() { 3200, 3210, 3220, 3230 };
        static List<int> _ca_list = new List<int>() { 3150, 3160, 3460, 3470, 3480, 3490 };
        static List<int> _sw_list = new List<int>() { 3310, 3320, 3330 };
        static List<int> _sw_l3_list = new List<int>() { 3330 };
        static List<int> _st_list = new List<int>() { 3370 };
        static List<int> _cp_list = new List<int>() { 3410 };
        static List<int> _fp_list = new List<int>() { 3420 };
        static List<int> _mb_list = new List<int>() { 3430 };
        static List<int> _patch_cord_list = new List<int>() { 3150, 3160, 3460, 3470 };
        static List<int> _mountable_list = new List<int>() { 3110, 3120, 3130, 3140, 3220, 3230, 3310, 3320, 3330, 3350, 3370, 3440, 3450 };
        static List<int> _one_side_list = new List<int>() { 3110, 3120, 3350, 3310, 3320, 3330, 3370 };
        static List<int> _link_diagram_list = new List<int>() { 3110, 3130, 3140, 3440, 3450, 3350, 3310, 3320, 3330, 3370, 3410, 3420, 3430, 3340 };    // LED 3340
        static List<int> _num_of_ports_list = new List<int>() { 3110, 3120, 3130, 3140, 3440, 3450, 3350, 3310, 3320, 3330, 3370, 3410, 3420, 3430 };

        // 자산에 추가할 수 없는 항목의 종류인지... (현재 PC도 수동으로 등록 불가능)
        public static bool is_dont_add_asset(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _dont_add_asset_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 PC 종류인지
        public static bool is_pc(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _pc_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 패치 패널 종류인지
        public static bool is_pp(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach(int n in _pp_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 UTP를 사용할 수 있는 패치 패널 종류인지
        public static bool is_pp_utp(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _pp_list)
            {
                if (_cg.catalog_group_id == n)
                    return _c.pp_media_type == "U";
            }
            return false;
        }

        // 카탈로그가 Fiber를 사용할 수 있는 패치 패널 종류인지
        public static bool is_pp_fiber(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _pp_list)
            {
                if (_cg.catalog_group_id == n)
                    return _c.pp_media_type == "F";
            }
            return false;
        }

        // 카탈로그가 인텔리전트 패치 패널 종류인지
        public static bool is_ipp(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ipp_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 광 인텔리전트 패치 패널 종류인지          // romee 2016-12-05 마곡 간선
        public static bool is_ipp_fp(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ifp_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 XC용 인텔리전트 패치 패널 종류인지
        public static bool is_ipp_xc(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ipp_list)
            {
                if (_cg.catalog_group_id == n)
                    return _c.pp_config_type == "X";
            }
            return false;
        }

        // 카탈로그가 IC용 인텔리전트 패치 패널 종류인지
        public static bool is_ipp_ic(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ipp_list)
            {
                if (_cg.catalog_group_id == n)
                    return _c.pp_config_type == "I";
            }
            return false;
        }

        // 카탈로그가 서버 종류인지
        public static bool is_sv(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _sv_list)
            {
                if (_cg.catalog_group_id == n) 
                    return true;
            }
            return false;
        }

        // 카탈로그가 랙 종류인지(예외로 1단계 추가)
        public static bool is_ra(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ra_list)
            {
                if ((_cg.catalog_group_id == n) || (_cg.level1_catalog_group_id == n) ) 
                    return true;
            }
            return false;
        }

        // 카탈로그가 케이블 종류인지
        public static bool is_ca(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ca_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 UTP를 사용하는 케이블 종류인지
        public static bool is_ca_utp(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ca_list)
            {
                if (_cg.catalog_group_id == n)
                    return _c.ca_media_type == "U";
            }
            return false;
        }

        // 카탈로그가 Fiber를 사용하는 케이블 종류인지
        public static bool is_ca_fiber(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ca_list)
            {
                if (_cg.catalog_group_id == n)
                    return _c.ca_media_type == "F";
            }
            return false;
        }

        // 카탈로그가 인텔리전트 케이블 종류인지
        public static bool is_ica(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ica_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 XC용 인텔리전트 케이블 종류인지
        public static bool is_ica_xc(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ica_list)
            {
                if (_cg.catalog_group_id == n)
                    return _c.ca_install_type == "X";
            }
            return false;
        }

        // 카탈로그가 IC용 인텔리전트 케이블 종류인지
        public static bool is_ica_ic(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ica_list)
            {
                if (_cg.catalog_group_id == n)
                    return _c.ca_install_type == "I";
            }
            return false;
        }

        // 카탈로그가 컨트롤러 종류인지
        public static bool is_ic(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ic_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 스위치 종류인지
        public static bool is_sw(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _sw_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 L3 스위치 종류인지
        public static bool is_sw_l3(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _sw_l3_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 새시형(슬롯형) 스위치 종류인지
        public static bool is_sw_slot(int catalog_id)
        {
            if (catalog_id == 0)
                return false;
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _sw_list)
            {
                if ((_cg.catalog_group_id == n)  && (_c.sw_figure_type == "S"))
                    return true;
            }
            return false;
        }

        // 카탈로그가 새시형(슬롯형) 스위치 종류인지
        public static bool is_sw_card(int catalog_id)
        {
            if (catalog_id == 0)
                return false;
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _sw_list)
            {
                if ((_cg.catalog_group_id == n) && (_c.sw_figure_type == "C"))
                    return true;
            }
            return false;
        }

        // 카탈로그가 스토리지 종류인지
        public static bool is_st(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _st_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 Consolidation Point 종류인지
        public static bool is_cp(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _cp_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 Face Plate 종류인지
        public static bool is_fp(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _fp_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 Mutoa Box 종류인지
        public static bool is_mb(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _mb_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 Environment(Energy) Box 종류인지
        public static bool is_eb(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _eb_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        // 카탈로그가 마운트가 가능한 종류인지
        public static bool is_rack_mountable(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _mountable_list)
            {
                if (_cg.catalog_group_id == n)
                {
                    return _c.rm_is_rack_mount == "Y";
                }
            }
            return false;
        }
        
        // 카탈로그가 링크다이아그램 편집에 사용할 수 있는 종류인지
        public static bool is_link_diagram(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _link_diagram_list)
            {
                if (_cg.catalog_group_id == n)
                {
                    if ((n == 3130) || (n == 3140))  // 인텔리전트 패치 패널
                    {
                        if (_c.pp_config_type == "I")
                            return false;               // 링크다이어그램에 드롭할 때 인터커넥터용 패치는 드롭이 불가능해야 함.
                    }
                    return true;
                }
            }
            return false;
        }

        // 카탈로그가 패치코드 종류인지
        public static bool is_ca_patch(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ca_patch_list)
            {
                if (_cg.catalog_group_id == n)
                {
                    return true;
                }
            }
            return false;
        }

        

        // 카탈로그가 XC type Patch Panel인지...
        public static bool is_xc_ipp(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ipp_list)
            {
                if (_cg.catalog_group_id == n)
                {
                    return _c.pp_config_type == "X";
                }
            }
            return false;
        }

        // 카탈로그가 한쪽방향에만 플러그가 있는 경우
        public static bool is_one_side(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _one_side_list)
            {
                if (_cg.catalog_group_id == n)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool is_i_ipp(int catalog_id)
        {
            catalog _c = null;
            catalog_group _cg = null;
            if (!find_catalog_group(catalog_id, out _c, out _cg))
                return false;
            foreach (int n in _ipp_list)
            {
                if (_cg.catalog_group_id == n)
                {
                    return _c.pp_config_type == "I";
                }
            }
            return false;
        }


        // 카탈로그가 마운트가 가능한 종류인지
        public static bool is_rack_mountable_group(int catalog_group_id)
        {
            foreach (int n in _mountable_list)
            {
                if (catalog_group_id == n)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool is_pp_group(int catalog_group_id)
        {
            foreach (int n in _pp_list)
            {
                if (catalog_group_id == n)
                    return true;
            }
            return false;
        }

        public static bool is_ic_group(int catalog_group_id)
        {
            foreach (int n in _ic_list)
            {
                if (catalog_group_id == n)
                    return true;
            }
            return false;
        }

        public static bool is_sw_group(int catalog_group_id)
        {
            foreach (int n in _sw_list)
            {
                if (catalog_group_id == n)
                    return true;
            }
            return false;
        }

        public static bool is_st_group(int catalog_group_id)
        {
            foreach (int n in _st_list)
            {
                if (catalog_group_id == n)
                    return true;
            }
            return false;
        }

        public static bool is_cp_group(int catalog_group_id)
        {
            foreach (int n in _cp_list)
            {
                if (catalog_group_id == n)
                    return true;
            }
            return false;
        }

        public static bool is_ca_group(int catalog_group_id)
        {
            foreach (int n in _ca_list)
            {
                if (catalog_group_id == n)
                    return true;
            }
            return false;
        }

        public static bool is_num_of_ports_group(int catalog_group_id)
        {
            foreach (int n in _num_of_ports_list)
            {
                if (catalog_group_id == n)
                    return true;
            }
            return false;
        }


        // 랙 유닛 크기
        public static int get_unit_size(int catalog_id)
        {
            int unit_size = 0;
            if (is_rack_mountable(catalog_id))
            {
                var c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
                if (c != null)
                {
                    unit_size = c.rm_unit_size ?? 0;
                }
            }
            return unit_size;
        }


        public static int getRackSize(int rack_id)
        {
            int rack_size = 0;
            var r = g.rack_list.Find(p => p.rack_id == rack_id);
            if (r != null)
            {
                var c = g.catalog_list.Find(p => p.catalog_id == r.rack_catalog_id);
                if (c != null)
                    rack_size = c.rm_unit_size ?? 0;
            }

            return rack_size;
        }

        public static int getEmptySlot(int rack_id)
        {
            int rack_size = getRackSize(rack_id);
            var list = g.rack_config_list.Where(p => (p.rack_id == rack_id) && (p.rack_mount_type == "S"));

            if (list.Count() > 0)
            {
                var min = list.Min(p => p.slot_no);
                var m = list.First(p => p.slot_no == min);

                int catalog_id = m.catalog_id ?? 0;
                int unit_size = get_unit_size(catalog_id);

                int ret = min - unit_size;
                if (ret < 0)
                    ret = 0;
                return ret;
            }
            return rack_size;
        }

        public static AssetTreeType getCatalogType(int catalog_id)
        {
            catalog _c = g.catalog_list.Find(at=> at.catalog_id == catalog_id);


            if (is_ipp(catalog_id))
                return AssetTreeType.i_PatchPanel;

            else if (is_pp(catalog_id))
                return AssetTreeType.PatchPanel;

            else if (is_sv(catalog_id))
                return AssetTreeType.Server;

            else if (is_ica(catalog_id))
                return AssetTreeType.i_Cable;

            else if (is_ca(catalog_id))
                return AssetTreeType.Cable;

            else if (is_ic(catalog_id))
                return AssetTreeType.i_Controller;

            else if (is_sw(catalog_id))
            {
                if (is_sw_card(catalog_id))
                    return AssetTreeType.SwitchCard;
                else if (is_sw_slot(catalog_id))
                    return AssetTreeType.SwitchSlot;
                else
                    return AssetTreeType.Switch;
            }
            else if (is_st(catalog_id))
                return AssetTreeType.Storage;

            else if (is_cp(catalog_id))
                return AssetTreeType.ConsolidationPoint;

            else if (is_mb(catalog_id))
                return AssetTreeType.MutoaBox;

            else if (is_fp(catalog_id))
                return AssetTreeType.FacePlate;

            else if (is_eb(catalog_id))
                return AssetTreeType.EnviromentBox;

            else if (is_pc(catalog_id))
                return AssetTreeType.PC;
            else if (is_eb(catalog_id))
                return AssetTreeType.EnergeBox;

            else
                return AssetTreeType.Unknown;
#if false
            foreach (int n in _ipp_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.i_PatchPanel;
            }
            foreach (int n in _pp_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.PatchPanel;
            }
            foreach (int n in _sv_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.Server;
            }
            foreach (int n in _ica_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.i_Cable;
            }
            foreach (int n in _ca_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.Cable;
            }
            foreach (int n in _ic_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.i_Controller;
            }
            foreach (int n in _sw_list)
            {
                if (_c.catalog_group_id == n)
                {
                    if (_c.sw_figure_type == "C")
                        return AssetTreeType.SwitchCard;
                    else if (_c.sw_figure_type == "S")
                        return AssetTreeType.SwitchSlot;
                    else
                    return AssetTreeType.Switch;
            } 
            }
            foreach (int n in _st_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.Storage;
            } 
            foreach (int n in _cp_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.ConsolidationPoint;
            } 
            foreach (int n in _fp_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.FacePlate;
            } 
            foreach (int n in _mb_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.MutoaBox;
            }

            foreach (int n in _eb_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.EnviromentBox;
            }

            foreach (int n in _pc_list)
            {
                if (_c.catalog_group_id == n)
                    return AssetTreeType.PC;
            } 
#endif
        }

        private static bool find_catalog_group(int catalog_id, out catalog _c, out catalog_group _cg)
        {
            _cg = null;
            _c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
            if (_c == null)
                return false;

            int catalog_group_id = _c.catalog_group_id;
            _cg = g.catalog_group_list.Find(p => p.catalog_group_id == catalog_group_id);
            if (_cg == null)
                return false;

            return true;
        }


        public static string get_rm_image_220(int catalog_id)
        {
            var c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
            int image_id = c != null ? (c.rm_image_220_image_id ?? 0) : 0;
            var im = g.image_list.Find(p => p.image_id == image_id);
            string file_path = im != null ? string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, "rack_220", im.file_name) : "/I2MS2;component/Images/No_Image.png";
            return file_path;
        }

        public static bool is_patch_cord_by_cg(int catalog_group_id)
        {
            catalog_group _cg = g.catalog_group_list.Find(p => p.catalog_group_id == catalog_group_id);
            if (_cg == null)
                return false;

            foreach (int n in _patch_cord_list)
            {
                if (_cg.catalog_group_id == n)
                    return true;
            }
            return false;
        }

        public static Color get_color_rgba(uint color)
        {

            byte color_a = (byte) (color >> 24);
            byte color_r = (byte) ((color << 8) >> 24);
            byte color_g = (byte) ((color << 16) >> 24);
            byte color_b = (byte) ((color << 24) >> 24);

            return Color.FromArgb(color_a, color_r, color_g, color_b);
        }

        // UTP Category Type
        public static string getUtpCableType(string name)
        {
            switch (name)
            {
                case "Category 5":
                    return "5";
                case "Category 5a":
                    return "5a";
                case "Category 6":
                    return "6";
                case "Category 6a":
                    return "6a";
                case "Category 7":
                    return "7";
                case "Category 8":
                    return "8";
                case "-":
                default:
                    return "";
            }
        }

        public static string getUtpCableTypeName(string type)
        {
            switch(type)
            {
                case "5" :
                    return "Category 5";
                case "5a" :
                    return "Category 5a";
                case "6" :
                    return "Category 6";
                case "6a" :
                    return "Category 6a";
                case "7" :
                    return "Category 7";
                case "8" :
                    return "Category 8";
                case "-":
                default:
                    return "";
            }
        }

        // Fiber Cable Type
        public static string getFiberCableType(string name)
        {
            switch (name)
            {
                case "OS(Optical Single mode)":
                    return "S";
                case "OS(Optical Single mode)-1":
                    return "S1";
                case "OS(Optical Single mode)-2":
                    return "S2";
                case "Polarization Maintaining Single mode":
                    return "PM";
                case "OM(Optical Multi mode)":
                    return "M";
                case "OM(Optical Multi mode)-1":
                    return "M1";
                case "OM(Optical Multi mode)-2":
                    return "M2";
                case "OM(Optical Multi mode)-3":
                    return "M3";
                case "OM(Optical Multi mode)-4":
                    return "M4";
                case "-":
                default:
                    return "";
            }
        }

        public static string getFiberCableTypeName(string type)
        {
            switch(type)
            {
                case "S" :
                    return "OS(Optical Single mode)";
                case "S1" :
                    return "OS(Optical Single mode)-1";
                case "S2" :
                    return "OS(Optical Single mode)-2";
                case "PM" :
                    return "Polarization Maintaining Single mode";
                case "M" :
                    return "OM(Optical Multi mode)";
                case "M1" :
                    return "OM(Optical Multi mode)-1";
                case "M2" :
                    return "OM(Optical Multi mode)-2";
                case "M3" :
                    return "OM(Optical Multi mode)-3";
                case "M4" :
                    return "OM(Optical Multi mode)-4";
                case "-":
                default:
                    return "";
            }
        }

        // Fiber Connector Type
        public static string getFiberConnectorType(string name)
        {
            switch (name)
            {
                case "FC":
                    return "FC";
                case "LC":
                    return "LC";
                case "SC":
                    return "SC";
                case "ST":
                    return "ST";
                case "MT-RJ":
                    return "MT";
                case "MPO":
                    return "MP";
                case "-":
                default:
                    return "";
            }
        }

        public static string getFiberConnectorTypeName(string type)
        {
            switch(type)
            {
                case "FC" :
                    return "FC";
                case "LC" :
                    return "LC";
                case "SC" :
                    return "SC";
                case "ST" :
                    return "ST";
                case "MT" :
                    return "MT-RJ";
                case "MP" :
                    return "MPO";
                case "-":
                default:
                    return "";
            }
        }

        // Media Type
        public static string getMediaType(string name)
        {
            switch (name)
            {
                case "UTP":
                    return "U";
                case "Fiber":
                    return "F";
                default:
                    return "";
            }
        }

        public static string getMediaTypeName(string type)
        {
            switch (type)
            {
                case "U":
                    return "UTP";
                case "F":
                    return "Fiber";
                default:
                    return "";
            }
        }

        // Figure Type
        public static string getFigureType(string name)
        {
            switch (name)
            {
                case "Flated":
                    return "F";
                case "Angled":
                    return "A";
                default:
                    return "";
            }
        }

        public static string getFigureTypeName(string type)
        {
            switch (type)
            {
                case "F":
                    return "Flated";
                case "A":
                    return "Angled";
                default:
                    return "";
            }
        }

        // Configure Type
        public static string getConfigType(string name)
        {
            switch (name)
            {
                case "XC":
                    return "X";
                case "IC":
                    return "I";
                default:
                    return "";
            }
        }

        public static string getConfigTypeName(string type)
        {
            switch (type)
            {
                case "X":
                    return "XC";
                case "I":
                    return "IC";
                default:
                    return "";
            }
        }

        // Configure Type (long)
        public static string getConfigTypeLong(string name)
        {
            switch (name)
            {
                case "Cross-Connect":
                    return "X";
                case "Inter-Connect":
                    return "I";
                default:
                    return "";
            }
        }

        public static string getConfigTypeNameLong(string type)
        {
            switch (type)
            {
                case "X":
                    return "Cross-Connect";
                case "I":
                    return "Inter-Connect";
                default:
                    return "";
            }
        }

        // Shield Type
        public static string getShieldType(string name)
        {
            switch (name)
            {
                case "Yes":
                    return "Y";
                case "No":
                    return "N";
                default:
                    return "";
            }
        }

        public static string getShieldTypeName(string type)
        {
            switch (type)
            {
                case "Y":
                    return "Yes";
                case "N":
                case "-":
                    return "No";
                default:
                    return "";
            }
        }

        // Connect Status
        public static string getConnectStatus(string name)
        {
            switch (name)
            {
                case "Yes":
                    return "Y";
                case "No":
                    return "N";
                default:
                    return "";
            }
        }

        public static string getConnectStatusName(string type)
        {
            switch (type)
            {
                case "Y":
                    return "CONNECT";
                case "N":
                case "A":       // 알람
                    return "DISCONNECT";
                default:
                    return "";
            }
        }
    }
}
