using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I2MS2.Models;
using WebApi.Models;
using System.Windows;
using System.Windows.Threading;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace I2MS2.Library
{

    public class Etc
    {
        // 유틸 
        public class asset_tree_vm_ex : asset_tree
        {
            public int catalog_id { set; get; }
            public string catalog_name { set; get; }
            public int sn { set; get; }
            public string location_path { set; get; }
            public int region1_id { set; get; }
            public string region1_name { set; get; }
            public int region2_id { set; get; }
            public string region2_name { set; get; }
            public int site_id { set; get; }
            public string site_name { set; get; }
            public int building_id { set; get; }
            public string building_name { set; get; }
            public int floor_id { set; get; }
            public string floor_name { set; get; }
            public int floor_no { set; get; }
            public int room_id { set; get; }
            public string room_name { set; get; }
            public int rack_id { set; get; }
            public string rack_name { set; get; }
            public int rack_catalog_id { set; get; }
        }

        // 복사 처리 --> 참조처리 방지용 
        public static bool CopyTo(object S, object T)
        {
            foreach (var pS in S.GetType().GetProperties())
            {
                foreach (var pT in T.GetType().GetProperties())
                {
                    if (pT.Name != pS.Name) continue;
                    (pT.GetSetMethod()).Invoke(T, new object[] { pS.GetGetMethod().Invoke(S, null) });
                }
            }
            return true;
        }

        // 네트워크 스케줄러에서 이미 등록되어 사용중인 스위치를 못지우게 하기 위해...
        public static bool is_use_sw_card_in_network_scheduler(int asset_id)
        {
            var node = g.net_scan_sw_list.Find(p => p.sw_asset_id == asset_id);
            return node != null;
        }

        // link 다이어그램 편집에서 open된 지능형 패치를 삭제하지 못하게 하기 위해...
        public static bool is_open_link_diagram(int asset_id)
        {
            return false;
        }

        public static bool is_use_link_diagram(int asset_id)
        {
            var node = g.asset_port_link_list.Find(p => p.front_asset_id == asset_id);
            if (node != null)
                return true;
            node = g.asset_port_link_list.Find(p => p.rear_asset_id == asset_id);
            return node != null;
        }

        public static catalog get_catalog_by_asset_id(int asset_id)
        {
            var a = get_asset(asset_id);
            if (a == null)
                return null;
            int catalog_id = a.catalog_id;
            var c = get_catalog(catalog_id);
            return c;
        }

        public static catalog get_catalog(int catalog_id)
        {
            var c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
            return c;
        }

        public static asset get_asset(int asset_id)
        {
            var a = g.asset_list.Find(p => p.asset_id == asset_id);
            return a;
        }

        public static List<int> get_asset_id_list(int site_id)
        {
            List<int> list = new List<int>();
            if (site_id == 0)
                return list;
            var li = from aa in g.asset_list
                         join bb in g.location_list  on aa.location_id equals bb.location_id
                         where bb.site_id == site_id
                         select new { aa.asset_id };
            list = li.Select(p => p.asset_id).ToList();
            return list;
        }

        // any_site가 true이면 모든 site에 대해...
        // any_catalog가 true이면 모든 catalog에 대해...
        public static List<asset_tree_vm_ex> get_asset_tree_vm_ex_list(int site_id, bool any_site, int catalog_id, bool any_catalog)
        {
            var list = new List<asset_tree_vm_ex>();

            int sn = 0;
            var region1_list = from re1 in g.region1_list
                                join ll in g.location_list.Where(p => p.location_level == 1) on re1.region1_id equals ll.region1_id
                                orderby ll.disp_order
                                select new { re1.region1_id, re1.region1_name };
            foreach (var g1 in region1_list)
            {
                var region2_list = from re2 in g.region2_list.Where(p => p.region1_id == g1.region1_id)
                                   join ll in g.location_list.Where(p => p.location_level == 2) on re2.region2_id equals ll.region2_id
                                   orderby ll.disp_order
                                   select new { re2.region2_id, re2.region2_name };
                foreach (var g2 in region2_list)
                {
                    var site_list = from ss in g.site_list.Where(p => p.region2_id == g2.region2_id)
                                    join ll in g.location_list.Where(p => p.location_level == 3) on ss.site_id equals ll.site_id
                                    orderby ll.disp_order
                                    where (ss.site_id == site_id) || any_site
                                    select new { ss.site_id, ss.site_name };
                    foreach (var s in site_list)
                    {
                        var building_list = from bb in g.building_list.Where(p => p.site_id == s.site_id)
                                            join ll in g.location_list.Where(p => p.location_level == 4) on bb.building_id equals ll.building_id
                                            orderby ll.disp_order
                                            select new { bb.building_id, bb.building_name };

                        foreach (var b in building_list)
                        {
                            var floor_list = from ff in g.floor_list.Where(p => p.building_id == b.building_id)
                                             join ll in g.location_list.Where(p => p.location_level == 5) on ff.floor_id equals ll.floor_id
                                             orderby ll.disp_order
                                             select new { ff.floor_id, ff.floor_name, ff.floor_no };

                            foreach (var f in floor_list)
                            {
                                var room_list = from rr in g.room_list.Where(p => p.floor_id == f.floor_id)
                                                join ll in g.location_list.Where(p => p.location_level == 6) on rr.room_id equals ll.room_id
                                                orderby ll.disp_order
                                                select new { rr.room_id, rr.room_name, ll.location_id, ll.location_path };

                                foreach (var r in room_list)
                                {
                                    var rack_list = from ra in g.rack_list.Where(p => p.room_id == r.room_id)
                                                    join ll in g.location_list.Where(p => p.location_level == 7) on ra.rack_id equals ll.rack_id
                                                    orderby ll.disp_order
                                                    select new { ra.rack_id, ra.rack_name, ra.rack_catalog_id, ll.location_id, ll.location_path };

                                    foreach (var r2 in rack_list)
                                    {
                                        var asset_list = from at in g.asset_tree_list
                                                         join aa in g.asset_list.Where(p => p.location_id == r2.location_id) on at.asset_id equals aa.asset_id
                                                         join cc in g.catalog_list.Where(p => (p.catalog_id == catalog_id) || any_catalog) on aa.catalog_id equals cc.catalog_id
                                                         orderby at.disp_order
                                                         select new asset_tree_vm_ex()
                                                         {
                                                             asset_id = aa.asset_id,
                                                             disp_name = at.disp_name,
                                                             location_id = at.location_id,
                                                             catalog_id = aa.catalog_id,
                                                             catalog_name = cc.catalog_name,
                                                             disp_level = at.disp_level,
                                                             disp_order = at.disp_order,
                                                             location_path = r2.location_path,
                                                             region1_id = g1.region1_id,
                                                             region1_name = g1.region1_name,
                                                             region2_id = g2.region2_id,
                                                             region2_name = g2.region2_name,
                                                             site_id = s.site_id,
                                                             site_name = s.site_name,
                                                             building_id = b.building_id,
                                                             building_name = b.building_name,
                                                             floor_id = f.floor_id,
                                                             floor_name = f.floor_name,
                                                             floor_no = f.floor_no,
                                                             room_id = r.room_id,
                                                             room_name = r.room_name,
                                                             rack_id = r2.rack_id,
                                                             rack_name = r2.rack_name,
                                                             rack_catalog_id = r2.rack_catalog_id ?? 0
                                                         };
                                        foreach (var a in asset_list)
                                        {
                                            a.sn = ++sn;
                                            list.Add(a);
                                        }
                                    }

                                    var asset_list2 = from at in g.asset_tree_list
                                                      join aa in g.asset_list.Where(p => p.location_id == r.location_id) on at.asset_id equals aa.asset_id
                                                      join cc in g.catalog_list.Where(p => (p.catalog_id == catalog_id) || any_catalog) on aa.catalog_id equals cc.catalog_id
                                                      orderby at.disp_order
                                                      select new asset_tree_vm_ex()
                                                     {
                                                         asset_id = aa.asset_id,
                                                         disp_name = at.disp_name,
                                                         location_id = at.location_id,
                                                         catalog_id = aa.catalog_id,
                                                         catalog_name = cc.catalog_name,
                                                         disp_level = at.disp_level,
                                                         disp_order = at.disp_order,
                                                         location_path = r.location_path,
                                                         region1_id = g1.region1_id,
                                                         region1_name = g1.region1_name,
                                                         region2_id = g2.region2_id,
                                                         region2_name = g2.region2_name,
                                                         site_id = s.site_id,
                                                         site_name = s.site_name,
                                                         building_id = b.building_id,
                                                         building_name = b.building_name,
                                                         floor_id = f.floor_id,
                                                         floor_name = f.floor_name,
                                                         floor_no = f.floor_no,
                                                         room_id = r.room_id,
                                                         room_name = r.room_name,
                                                         rack_id = 0,
                                                         rack_name = "",
                                                         rack_catalog_id = 0
                                                     };
                                    foreach (var a in asset_list2)
                                    {
                                        a.sn = ++sn;
                                        list.Add(a);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        public static ePortStatus get_port_status_from_ipp_asset_port_link(int ipp_asset_id, int port_no)
        {
            if ((ipp_asset_id == 0) || (port_no == 0))
                return ePortStatus.Unknown;
            var node = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == ipp_asset_id) && (p.port_no == port_no));
            if (node == null)
                return ePortStatus.Unknown;
            string status = node.ipp_port_status;
            ePortStatus status_t = Common.get_status_type(status);
            return status_t;
        }

        public static bool has_rack_catalog(int rack_id)
        {
            if (rack_id == 0)
                return false;
            var r = g.rack_list.Find(p => p.rack_id == rack_id);
            if (r != null)
            {
                var c = g.catalog_list.Find(p => p.catalog_id == r.rack_catalog_id);
                return c != null;
            }
            return false;
        }


        // 새시형 스위치에서 다음 장착 가능한 슬롯 번호를 알아온다.
        public static int get_sw_next_slot(int sw_slot_asset_id)
        {
            if (sw_slot_asset_id == 0)
                return 1;
            try
            {
                int max = (int) g.sw_card_config_list.Where(p => (p.sw_asset_id == sw_slot_asset_id) && (p.sw_card_asset_id > 0)).Max(p => p.slot_no);
                return max + 1;
            }
            catch(Exception) {}
            return 1;
        }

        public static int get_rack_max_slot(int rack_id)
        {
            if (rack_id == 0)
                return 0;
            var r = g.rack_list.Find(p => p.rack_id == rack_id);
            if (r == null)
                return 0;
            int catalog_id = r.rack_catalog_id ?? 0;
            if (catalog_id == 0)
                return 0;
            var c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
            return c != null ? c.rm_unit_size ?? 0 : 0;
        }

        public static string get_building_name(int building_id)
        {
            if (building_id == 0)
                return "";
            var b = g.building_list.Find(p => p.building_id == building_id);
            return b != null ? b.building_name : "";
        }

        public static int get_catalog_id(string catalog_name)
        {
            if (catalog_name == string.Empty)
                return 0;
            var c = g.catalog_list.Find(p => p.catalog_name == catalog_name);
            return c != null ? c.catalog_id : 0;
        }


        // 랙명은 빌딩내에서 중복 불가능
        public static int get_rack_id_in_room(string _rack_name, int room_id)
        {
            string rack_name = _rack_name.Trim();
            if (rack_name == string.Empty)
                return 0;
            var r = g.rack_list.Find(p => (p.rack_name == rack_name) && (p.room_id == room_id));
            return r != null ? r.rack_id : 0;
        }

        // 랙명은 빌딩내에서 중복 불가능
        public static int get_rack_id_in_building(string _rack_name, int building_id)
        {
            string rack_name = _rack_name.Trim();
            if (rack_name == string.Empty)
                return 0;
            var r = from aa in g.rack_list.Where(p => p.rack_name == rack_name)
                    join bb in g.location_list on aa.rack_id equals bb.rack_id
                    where bb.building_id == building_id
                    select new { aa.rack_id };
            return r.Count() > 0 ? r.First().rack_id : 0;
        }

        // 룸명은 빌딩내에서 중복 불가능
        public static int get_room_id_in_building(string _room_name, int building_id)
        {
            string room_name = _room_name.Trim();
            if (room_name == string.Empty)
                return 0;
            var r = from aa in g.room_list.Where(p => p.room_name == room_name)
                    join bb in g.location_list on aa.room_id equals bb.room_id
                    where bb.building_id == building_id
                    select new { aa.room_id };
            return r.Count() > 0 ? r.First().room_id : 0;
        }

        // 룸명은 빌딩내에서 중복 불가능
        public static int get_room_id_in_floor(string _room_name, int floor_id)
        {
            string room_name = _room_name.Trim();
            if (room_name == string.Empty)
                return 0;
            var r = g.room_list.Find(p => (p.room_name == room_name) && (p.floor_id == floor_id));
            return r != null ? r.room_id : 0;
        }

        // 층명은 빌딩내에서 중복 불가능
        public static int get_floor_id_in_building(string _floor_name, int building_id)
        {
            string floor_name = _floor_name.Trim();
            if (floor_name == string.Empty)
                return 0;
            var f = g.floor_list.Find(p => (p.floor_name == floor_name) && (p.building_id == building_id));
            return f != null ? f.floor_id : 0;
        }

        // 빌딩명은 사이트내에서 중복 불가능
        public static int get_building_id_in_site(string _building_name, int site_id)
        {
            string building_name = _building_name.Trim();
            if (building_name == string.Empty)
                return 0;
            var b = g.building_list.Find(p => (p.building_name == building_name) && (p.site_id == site_id));
            return b != null ? b.building_id : 0;
        }

        // 자산 ID는 빌딩내에서 중복 불가능
        public static int get_asset_id_in_building(string asset_name, int building_id)
        {
            string asset_name2 = asset_name.Trim();
            if (asset_name == string.Empty)
                return 0;
            var a = from aa in g.asset_list.Where(p => p.asset_name == asset_name2)
                    join bb in g.location_list on aa.location_id equals  bb.location_id
                    join cc in g.building_list.Where(p => p.building_id == building_id) on bb.building_id equals cc.building_id
                    select new { asset_id = aa.asset_id};
            
            if (a.Count() == 0)
                return 0;
            return a.First().asset_id;
        }

        public static string get_asset_name(int asset_id)
        {
            if (asset_id == 0)
                return "";
            var a = g.asset_list.Find(p => p.asset_id == asset_id);
            string asset_name = a != null ? a.asset_name : "";
            return asset_name;
        }

        public static string get_catalog_name(int catalog_id)
        {
            if (catalog_id == 0)
                return "";
            var c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
            string catalog_name = c != null ? c.catalog_name : "";
            return catalog_name;
        }

        public static int get_sw_max_slot(int catalog_id)
        {
            if (catalog_id == 0)
                return 0;
            var c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
            int max_slot = c != null ? c.sw_num_of_slots ?? 0 : 0;
            return max_slot;
        }

        public static bool is_rack(int location_id)
        {
            if (location_id == 0)
                return false;
            var l = g.location_list.Find(p => (p.location_id == location_id) && (p.location_level == 7));
            return l != null;
        }

        public static bool is_room_or_rack(int location_id)
        {
            if (location_id == 0)
                return false;
            var l = g.location_list.Find(p => (p.location_id == location_id) && 
                ((p.location_level == 6) || (p.location_level == 7)));
            return l != null;
        }

        public static bool is_floor_or_room_or_rack(int location_id)
        {
            if (location_id == 0)
                return false;
            var l = g.location_list.Find(p => (p.location_id == location_id) && 
                ((p.location_level == 5) || (p.location_level == 6) || (p.location_level == 7)));
            return l != null;
        }

        public static int get_rack_id_by_location_id(int location_id)
        {
            if (location_id == 0)
                return 0;
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return 0;
            return l.rack_id ?? 0;
        }

        public static int get_location_id_by_asset_id(int asset_id)
        {
            if (asset_id == 0)
                return 0;
            var l = g.asset_list.Find(p => p.asset_id == asset_id);
            if (l == null)
                return 0;
            return l.location_id;
        }

        public static int get_location_id_by_region1_id(int region1_id)
        {
            if (region1_id == 0)
                return 0;
            var node = g.location_list.Find(p => (p.region1_id == region1_id) && (p.location_level == 1));
            if (node == null)
                return 0;

            return node.location_id;
        }

        public static int get_location_id_by_region2_id(int region2_id)
        {
            if (region2_id == 0)
                return 0;
            var node = g.location_list.Find(p => (p.region2_id == region2_id) && (p.location_level == 2));
            if (node == null)
                return 0;

            return node.location_id;
        }

        public static int get_location_id_by_site_id(int site_id)
        {
            if (site_id == 0)
                return 0;
            var node = g.location_list.Find(p => (p.site_id == site_id) && (p.location_level == 3));
            if (node == null)
                return 0;

            return node.location_id;
        }

        public static int get_location_id_by_building_id(int building_id)
        {
            if (building_id == 0)
                return 0;
            var node = g.location_list.Find(p => (p.building_id == building_id) && (p.location_level == 4));
            if (node == null)
                return 0;

            return node.location_id;
        }

        public static int get_location_id_by_floor_id(int floor_id)
        {
            if (floor_id == 0)
                return 0;
            var node = g.location_list.Find(p => (p.floor_id == floor_id) && (p.location_level == 5));
            if (node == null)
                return 0;

            return node.location_id;
        }

        public static int get_location_id_by_room_id(int room_id)
        {
            if (room_id == 0)
                return 0;
            var node = g.location_list.Find(p => (p.room_id == room_id) && (p.location_level == 6));
            if (node == null)
                return 0;

            return node.location_id;
        }

        public static int get_location_id_by_rack_id(int rack_id)
        {
            if (rack_id == 0)
                return 0;
            var node = g.location_list.Find(p => (p.rack_id == rack_id) && (p.location_level == 7));
            if (node == null)
                return 0;

            return node.location_id;
        }

        public static bool is_same_location_level(int dest_location_id, int source_location_id)
        {
            var dest = g.location_list.Find(p => p.location_id == dest_location_id);
            var source = g.location_list.Find(p => p.location_id == source_location_id);

            if ((dest == null) || (source == null))
                return false;

            return (dest.location_level) == source.location_level;
        }

        // 위치 항목은 복사될 때 한 단계 상위 레벨에만 만들어진다.
        public static bool is_copy_location(int dest_location_id, int source_location_id)
        {
            var dest = g.location_list.Find(p => p.location_id == dest_location_id);
            var source = g.location_list.Find(p => p.location_id == source_location_id);

            if ((dest == null) || (source == null))
                return false;

            return (dest.location_level + 1) == source.location_level;
        }

        public static int get_sw_slot_asset_id(int sw_card_asset_id)
        {
            if (sw_card_asset_id == 0)
                return 0;
            var scc = g.sw_card_config_list.Find(p => p.sw_card_asset_id == sw_card_asset_id);
            return scc != null ? scc.sw_asset_id : 0;
        }

        // building부터 가능...
        public static int get_prev_location_id(int location_id)
        {
            var l = g.location_list.Find(p => p.location_id == location_id);
            switch (l.location_level)
            {
                case 4:
                    // building
                    int prev_site_id = l.site_id ?? 0;
                    var ss = g.site_list.Find(p => p.site_id == prev_site_id);
                    if (ss == null)
                        return 0;
                    var l1 = g.location_list.Find(p => (p.site_id == ss.site_id) && (p.location_level == (l.location_level - 1)));
                    return l1 != null ? l1.location_id : 0;
                case 5:
                    // floor
                    int prev_building_id = l.building_id ?? 0;
                    var bb = g.building_list.Find(p => p.building_id == prev_building_id);
                    if (bb == null)
                        return 0;
                    var l2 = g.location_list.Find(p => (p.building_id == bb.building_id) && (p.location_level == (l.location_level - 1)));
                    return l2 != null ? l2.location_id : 0;
                case 6:
                    // room
                    int prev_floor_id = l.floor_id ?? 0;
                    var ff = g.floor_list.Find(p => p.floor_id == prev_floor_id);
                    if (ff == null)
                        return 0;
                    var l3 = g.location_list.Find(p => (p.floor_id == ff.floor_id) && (p.location_level == (l.location_level - 1)));
                    return l3 != null ? l3.location_id : 0;
                case 7:
                    // rack
                    int prev_room_id = l.room_id ?? 0;
                    var rr = g.room_list.Find(p => p.room_id == prev_room_id);
                    if (rr == null)
                        return 0;
                    var l4 = g.location_list.Find(p => (p.room_id == rr.room_id) && (p.location_level == (l.location_level - 1)));
                    return l4 != null ? l4.location_id : 0;
            }
            return 0;
        }

        public static int get_favorite_tree_id(int asset_id)
        {
            var ft = g.favorite_tree_list.Find(p => p.asset_id == asset_id);
            if (ft == null)
                return 0;
            return ft.favorite_tree_id;
        }

        public static int get_asset_tree_id(int asset_id)
        {
            var at = g.asset_tree_list.Find(p => p.asset_id == asset_id);
            if (at == null)
                return 0;
            return at.asset_tree_id;
        }


        public static string get_string(int no, int digit_cnt)
        {
            string a = no.ToString();
            if (a.Length >= digit_cnt)
                return a.Substring(0, digit_cnt);
            while(true)
            {
                if (a.Length >= digit_cnt)
                    break;
                a = "0" + a;
            }
            return a;
        }

        public static int? get_null_int(int value)
        {
            int? value2 = null;
            if (value != 0)
                value2 = value;
            return value2;
        }

        public static string get_null_string(string value)
        {
            string value2 = null;
            if (value != string.Empty)
                value2 = value;
            return value2;
        }

        // 디지트 문자를 숫자 인트로 
        public static int get_int(string text)
        {
            int value = 0;
            try
            {
                int.TryParse(text, out value);
            }
            catch (Exception) { }
            return value;
        }

        // 디지트 문자를 숫자 더블로 
        public static double get_double(string text)
        {
            double value = 0;
            try
            {
                double.TryParse(text, out value);
            }
            catch (Exception) { }
            return value;
        }

        public static int get_slot_no_by_asset_id(int asset_id)
        {
            if (asset_id == 0)
                return 0;
            var rc = g.rack_config_list.Find(p => p.asset_id == asset_id);
            if (rc == null)
                return 0;
            return rc.slot_no;
        }

        public static int get_num_of_ports_by_asset_id(int asset_id)
        {
            if (asset_id == 0)
                return 0;
            int catalog_id = get_catalog_id_by_asset_id(asset_id);
            if (catalog_id == 0)
                return 0;
            int num_of_ports = get_num_of_ports_by_catalog_id(catalog_id);
            return num_of_ports;
        }

        public static int get_catalog_id_by_asset_id(int asset_id)
        {
            if (asset_id == 0)
                return 0;
            var a = g.asset_list.Find(p => p.asset_id == asset_id);
            if (a == null)
                return 0;
            int catalog_id = a.catalog_id;
            return catalog_id;
        }

        public static int get_num_of_ports_by_catalog_id(int catalog_id)
        {
            if (catalog_id == 0)
                return 0;
            var c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
            if (c == null)
                return 0;
            int num_of_ports = c.num_of_ports;
            return num_of_ports;
        }

        public static int get_slot_no(int asset_id)
        {
            if (asset_id == 0)
                return 0;
            var rc = g.rack_config_list.Find(p => p.asset_id == asset_id);
            if (rc == null)
                return 0;
            return rc.slot_no;
        }

        public static int get_pp_id_by_asset_id(int asset_id)
        {
            if (asset_id == 0)
                return 0;
            var iic = g.ic_ipp_config_list.Find(p => p.ipp_asset_id == asset_id);
            if (iic == null)
                return 0;
            return iic.ipp_connect_no;
        }

        public static string get_ic_asset_name_by_ipp_asset_id(int asset_id)
        {
            string name = "";
            if (asset_id == 0)
                return name;
            var iic = g.ic_ipp_config_list.Find(p => p.ipp_asset_id == asset_id);
            if (iic == null)
                return name;

            int ic_asset_id = iic.ic_asset_id;
            var a = g.asset_list.Find(p => p.asset_id == ic_asset_id);
            if (a == null)
                return name;
            return a.asset_name;
        }

        public static int get_sys_id_by_ipp_asset_id(int ipp_asset_id)
        {
            if (ipp_asset_id == 0)
                return 0;
            var iic = g.ic_ipp_config_list.Find(p => p.ipp_asset_id == ipp_asset_id);
            if (iic == null)
                return 0;

            int ic_asset_id = iic.ic_asset_id;
            var au = g.asset_aux_list.Find(p => p.asset_id == ic_asset_id);
            if (au == null)
                return 0;
            return au.ic_con_id ?? 0;
        }      
        

        // 인텔리전트 케이블을 알아온다.
        public static int get_standard_ica(int ipp_asset_id)
        {
            int cable_catalog_id = 415001;      // 대표 케이블
            var a = g.asset_list.Find(p => p.asset_id == ipp_asset_id);
            if (a == null)
                return cable_catalog_id;
            var c = g.catalog_list.Find(p => p.catalog_id == a.catalog_id);
            if (c == null)
                return cable_catalog_id;

            string config_type = c.pp_config_type;      // Ic, Xc
            string media_type = c.pp_media_type;       // Utp, Fiber

            var c2 = g.catalog_list.Find(p => (p.catalog_group_id == 3150) && (p.ca_media_type == media_type) && (p.ca_install_type == config_type));
            if (c2 == null)
                return cable_catalog_id;
            return c2.catalog_id;
        }

        // ic_asset_id 값을 알아온다.
        public static int get_ic_asset_id(int sys_id)
        {
            if (sys_id == 0)
                return 0;
            var aa = g.asset_aux_list.Find(p => p.ic_con_id == sys_id);
            int ic_asset_id = aa != null ? aa.asset_id : 0;
            return ic_asset_id;
        }

        public static int get_ic_asset_id_by_ipp_asset_id(int ipp_asset_id)
        {
            if (ipp_asset_id == 0)
                return 0;
            var iic = g.ic_ipp_config_list.Find(p => p.ipp_asset_id == ipp_asset_id);
            int ic_asset_id = iic != null ? iic.ic_asset_id : 0;
            return ic_asset_id;
        }

        // ipp_asset_id 값을 알아온다.
        public static int get_ipp_asset_id(int sys_id, int pp_id)
        {
            if ((sys_id == 0) || (pp_id == 0))
                return 0;
            int ic_asset_id = get_ic_asset_id(sys_id);
            var iic = g.ic_ipp_config_list.Find(p => (p.ic_asset_id == ic_asset_id) && (p.ipp_connect_no == pp_id));
            int ipp_asset_id = iic != null ? iic.ipp_asset_id ?? 0 : 0;
            return ipp_asset_id;
        }

        public static string get_event_name(string type)
        {
            switch (type)
            {
                case "I":
                    return "Information";
                case "W":
                    return "Warnning";
                case "E":
                    return "Error";
                default:
                    return "None";
            }
        }


        public static string get_status_char(ePortStatus status)
        {
            switch (status)
            {
                case ePortStatus.Plugged:
                    return "P";
                case ePortStatus.Unplugged:
                    return "U";
                case ePortStatus.Linked:
                    return "L";
                default:
                    return "-";
            }
        }

        public static ePortStatus get_status_type(string status)
        {
            switch (status)
            {
                case "P":
                    return  ePortStatus.Plugged;
                case "U":
                    return ePortStatus.Unplugged;
                case "L":
                    return ePortStatus.Linked;
                default:
                    return ePortStatus.Unknown;
            }
        }

        public static string get_vcm_name(string type)
        {
            switch (type)
            {
                case "L":
                    return "Left Side";
                case "R":
                    return "Right Side";
                case "B":
                    return "Both Side";
                case "N":
                default:
                    return "None";
            }
        }

        public static string get_storage_type_name(string type)
        {
            switch (type)
            {
                case "H":
                    return "Host connected";
                case "S":
                default:
                    return "Stand-alone";
            }
        }

        public static AssetTreeVM get_location_ast_vm(int location_id)
        {
            if (g.location_ast_vm_dic.ContainsKey(location_id))
                return g.location_ast_vm_dic[location_id];
            else
                return null;
        }


        public static int get_number_from_str(string str)
        {
            try
            {
                string str1 = Regex.Replace(str, @"\D", "");
                return int.Parse(str1);
            }
            catch (Exception e) { Console.WriteLine("{0}", e.Message); return 0; }
        }
    }

    // 실시간으로 UI를 업데이트하고자 할 때 필요...
    public static class Refresh
    {
        public static void Refresh_Controls(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() => { }));
        }
    }

}
