using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using I2MS2.Models;

namespace I2MS2.Library
{
    // 통계
    public class Stat
    {
        // --------------------------------------------------------------------
        // 1. Site 별
        // --------------------------------------------------------------------

        // 전체 패치 패널 수
        public static int get_tot_pp_ports_by_site_id(int site_id)
        {
            var list = from aa in g.asset_list.Where(p => CatalogType.is_pp(p.catalog_id))
                       join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                       join cc in g.location_list.Where(p => p.site_id == site_id)  on aa.location_id equals cc.location_id
                       select new { bb.num_of_ports };
            int sum = 0;
            foreach (var node in list)
                sum += node.num_of_ports;
            return sum;
        }

        // 전체 패치 패널 사용 수
        public static int get_used_pp_ports_by_site_id(int site_id)
        {
            var list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0) || (p.rear_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_pp(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.site_id == site_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 사용된 지능형 패치 패널 사용 수
        public static int get_used_ipp_ports_by_site_id(int site_id)
        {
            var list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0) || (p.rear_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.site_id == site_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 전체 사용자포트 수
        public static int get_tot_user_ports_by_site_id(int site_id)
        {
            var list = from aa in g.user_port_layout_list
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.site_id == site_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 배치된 사용자포트 수
        public static int get_used_user_ports_by_site_id(int site_id)
        {
            var list = from aa in g.user_port_layout_list.Where(p => p.is_layout == "Y")
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.site_id == site_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 전체 환경 장치 수 에너지 박스 / 파워스트립 / 온도 / 습도 / 도어 
        public static List<int> get_tot_eb_by_site_id(int site_id, out int sum_e, out int sum_p, out int sum_t, out int sum_h, out int sum_d)
        {
            List<int> list2 = new List<int>();
            var list = from aa in g.eb_port_config_list
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.site_id == site_id) on bb.location_id equals cc.location_id
                       select new { aa.port_type };
            sum_p = list.Count(p => p.port_type == "P");
            sum_t = list.Count(p => p.port_type == "T");
            sum_h = sum_t;
            sum_d = list.Count(p => p.port_type == "D");

            var list1 = from aa in g.asset_list.Where(p => CatalogType.is_eb(p.catalog_id))
                       join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                       join cc in g.location_list.Where(p => p.site_id == site_id) on aa.location_id equals cc.location_id
                       select new { aa.asset_id};
            sum_e = list1.Count();

            list2 = list1.Select(p => p.asset_id).ToList();
            return list2;
        }

        // --------------------------------------------------------------------
        // 2. building 별
        // --------------------------------------------------------------------

        // 전체 패치 패널 수
        public static int get_tot_pp_ports_by_building_id(int building_id)
        {
            var list = from aa in g.asset_list.Where(p => CatalogType.is_pp(p.catalog_id))
                       join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                       join cc in g.location_list.Where(p => p.building_id == building_id) on aa.location_id equals cc.location_id
                       select new { bb.num_of_ports };
            int sum = 0;
            foreach (var node in list)
                sum += node.num_of_ports;
            return sum;
        }

        // 사용된 지능형 패치 패널 수
        public static int get_tot_ipp_ports_by_building_id(int building_id)
        {
            var list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                       join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                       join cc in g.location_list.Where(p => p.building_id == building_id) on aa.location_id equals cc.location_id
                       select new { bb.num_of_ports };
            int sum = 0;
            foreach (var node in list)
                sum += node.num_of_ports;

            return sum;
        }

        // 전체 패치 패널 사용 수
        public static int get_used_pp_ports_by_building_id(int building_id)
        {
            var list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0) || (p.rear_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_pp(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.building_id == building_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 사용된 지능형 패치 패널 사용 수
        public static int get_used_ipp_ports_by_building_id(int building_id)
        {
            var list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0) || (p.rear_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.building_id == building_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 전체 사용자포트 수
        public static int get_tot_user_ports_by_building_id(int building_id)
        {
            var list = from aa in g.user_port_layout_list
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.building_id == building_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 배치된 사용자포트 수
        public static int get_used_user_ports_by_building_id(int building_id)
        {
            var list = from aa in g.user_port_layout_list.Where(p => p.is_layout == "Y")
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.building_id == building_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 전체 환경 장치 수 에너지 박스 / 파워스트립 / 온도 / 습도 / 도어 
        public static List<int> get_tot_eb_by_building_id(int building_id, out int sum_e, out int sum_p, out int sum_t, out int sum_h, out int sum_d)
        {
            List<int> list2 = new List<int>();

            var list = from aa in g.eb_port_config_list
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.building_id == building_id) on bb.location_id equals cc.location_id
                       select new { aa.port_type };
            sum_p = list.Count(p => p.port_type == "P");
            sum_t = list.Count(p => p.port_type == "T");
            sum_h = sum_t;
            sum_d = list.Count(p => p.port_type == "D");

            var list1 = from aa in g.asset_list.Where(p => CatalogType.is_eb(p.catalog_id))
                        join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                        join cc in g.location_list.Where(p => p.building_id == building_id) on aa.location_id equals cc.location_id
                        select new { aa.asset_id };
            sum_e = list1.Count();
            list2 = list1.Select(p => p.asset_id).ToList();
            return list2;
        }

        // --------------------------------------------------------------------
        // 3. floor 별
        // --------------------------------------------------------------------


        // 전체 패치 패널 수
        public static int get_tot_pp_ports_by_floor_id(int floor_id)
        {
            var list = from aa in g.asset_list.Where(p => CatalogType.is_pp(p.catalog_id))
                       join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                       join cc in g.location_list.Where(p => p.floor_id == floor_id) on aa.location_id equals cc.location_id
                       select new { bb.num_of_ports };
            int sum = 0;
            foreach (var node in list)
                sum += node.num_of_ports;
            return sum;
        }

        // 사용된 지능형 패치 패널 수
        public static int get_tot_ipp_ports_by_floor_id(int floor_id)
        {
            var list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                       join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                       join cc in g.location_list.Where(p => p.floor_id == floor_id) on aa.location_id equals cc.location_id
                       select new { bb.num_of_ports };
            int sum = 0;
            foreach (var node in list)
                sum += node.num_of_ports;

            return sum;
        }

        // 전체 패치 패널 사용 수
        public static int get_used_pp_ports_by_floor_id(int floor_id)
        {
            var list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0) || (p.rear_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_pp(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.floor_id == floor_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 사용된 지능형 패치 패널 사용 수
        public static int get_used_ipp_ports_by_floor_id(int floor_id)
        {
            var list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0) || (p.rear_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.floor_id == floor_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 전체 사용자포트 수
        public static int get_tot_user_ports_by_floor_id(int floor_id)
        {
            var list = from aa in g.user_port_layout_list
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.floor_id == floor_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 배치된 사용자포트 수
        public static int get_used_user_ports_by_floor_id(int floor_id)
        {
            var list = from aa in g.user_port_layout_list.Where(p => p.is_layout == "Y")
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.floor_id == floor_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 전체 환경 장치 수 에너지 박스 / 파워스트립 / 온도 / 습도 / 도어 
        public static List<int> get_tot_eb_by_floor_id(int floor_id, out int sum_e, out int sum_p, out int sum_t, out int sum_h, out int sum_d)
        {
            List<int> list2 = new List<int>();
            var list = from aa in g.eb_port_config_list
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.floor_id == floor_id) on bb.location_id equals cc.location_id
                       select new { aa.port_type };
            sum_p = list.Count(p => p.port_type == "P");
            sum_t = list.Count(p => p.port_type == "T");
            sum_h = sum_t;
            sum_d = list.Count(p => p.port_type == "D");

            var list1 = from aa in g.asset_list.Where(p => CatalogType.is_eb(p.catalog_id))
                        join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                        join cc in g.location_list.Where(p => p.floor_id == floor_id) on aa.location_id equals cc.location_id
                        select new { aa.asset_id };
            sum_e = list1.Count();
            list2 = list1.Select(p => p.asset_id).ToList();
            return list2;
        }
        // --------------------------------------------------------------------
        // 4. room 별
        // --------------------------------------------------------------------


        // 전체 패치 패널 수
        public static int get_tot_pp_ports_by_room_id(int room_id)
        {
            var list = from aa in g.asset_list.Where(p => CatalogType.is_pp(p.catalog_id))
                       join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                       join cc in g.location_list.Where(p => p.room_id == room_id) on aa.location_id equals cc.location_id
                       select new { bb.num_of_ports };
            int sum = 0;
            foreach (var node in list)
                sum += node.num_of_ports;
            return sum;
        }

        // 사용된 지능형 패치 패널 수
        public static int get_tot_ipp_ports_by_room_id(int room_id)
        {
            var list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                       join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                       join cc in g.location_list.Where(p => p.room_id == room_id) on aa.location_id equals cc.location_id
                       select new { bb.num_of_ports };
            int sum = 0;
            foreach (var node in list)
                sum += node.num_of_ports;

            return sum;
        }

        // 전체 패치 패널 사용 수
        public static int get_used_pp_ports_by_room_id(int room_id)
        {
            var list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0) || (p.rear_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_pp(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.room_id == room_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 사용된 지능형 패치 패널 사용 수
        public static int get_used_ipp_ports_by_room_id(int room_id)
        {
            var list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0) || (p.rear_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.room_id == room_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 전체 사용자포트 수
        public static int get_tot_user_ports_by_room_id(int room_id)
        {
            var list = from aa in g.user_port_layout_list
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.room_id == room_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 배치된 사용자포트 수
        public static int get_used_user_ports_by_room_id(int room_id)
        {
            var list = from aa in g.user_port_layout_list.Where(p => p.is_layout == "Y")
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.room_id == room_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 전체 환경 장치 수 에너지 박스 / 파워스트립 / 온도 / 습도 / 도어 
        public static List<int> get_tot_eb_by_room_id(int room_id, out int sum_e, out int sum_p, out int sum_t, out int sum_h, out int sum_d)
        {
            List<int> list2 = new List<int>();

            var list = from aa in g.eb_port_config_list
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.room_id == room_id) on bb.location_id equals cc.location_id
                       select new { aa.port_type };
            sum_p = list.Count(p => p.port_type == "P");
            sum_t = list.Count(p => p.port_type == "T");
            sum_h = sum_t;
            sum_d = list.Count(p => p.port_type == "D");

            var list1 = from aa in g.asset_list.Where(p => CatalogType.is_eb(p.catalog_id))
                        join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                        join cc in g.location_list.Where(p => p.room_id == room_id) on aa.location_id equals cc.location_id
                        select new { aa.asset_id };
            sum_e = list1.Count();
            list2 = list1.Select(p => p.asset_id).ToList();
            return list2;
        }
        // --------------------------------------------------------------------
        // 5. rack 별
        // --------------------------------------------------------------------


        // 전체 패치 패널 수
        public static int get_tot_pp_ports_by_rack_id(int rack_id)
        {
            var list = from aa in g.asset_list.Where(p => CatalogType.is_pp(p.catalog_id))
                       join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                       join cc in g.location_list.Where(p => p.rack_id == rack_id) on aa.location_id equals cc.location_id
                       select new { bb.num_of_ports };
            int sum = 0;
            foreach (var node in list)
                sum += node.num_of_ports;
            return sum;
        }

        // 사용된 지능형 패치 패널 수
        public static int get_tot_ipp_ports_by_rack_id(int rack_id)
        {
            var list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                       join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                       join cc in g.location_list.Where(p => p.rack_id == rack_id) on aa.location_id equals cc.location_id
                       select new { bb.num_of_ports };
            int sum = 0;
            foreach (var node in list)
                sum += node.num_of_ports;

            return sum;
        }

        // 전체 패치 패널 사용 수
        public static int get_used_pp_ports_by_rack_id(int rack_id)
        {
            var list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0) || (p.rear_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_pp(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.rack_id == rack_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 사용된 지능형 패치 패널 사용 수
        public static int get_used_ipp_ports_by_rack_id(int rack_id)
        {
            var list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0) || (p.rear_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.rack_id == rack_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 전체 사용자포트 수
        public static int get_tot_user_ports_by_rack_id(int rack_id)
        {
            var list = from aa in g.user_port_layout_list
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.rack_id == rack_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 배치된 사용자포트 수
        public static int get_used_user_ports_by_rack_id(int rack_id)
        {
            var list = from aa in g.user_port_layout_list.Where(p => p.is_layout == "Y")
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.rack_id == rack_id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
            return list.Count();
        }

        // 전체 환경 장치 수 에너지 박스 / 파워스트립 / 온도 / 습도 / 도어 
        public static List<int> get_tot_eb_by_rack_id(int rack_id, out int sum_e, out int sum_p, out int sum_t, out int sum_h, out int sum_d)
        {
            List<int> list2 = new List<int>();

            var list = from aa in g.eb_port_config_list
                       join bb in g.asset_list on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.rack_id == rack_id) on bb.location_id equals cc.location_id
                       select new { aa.port_type };
            sum_p = list.Count(p => p.port_type == "P");
            sum_t = list.Count(p => p.port_type == "T");
            sum_h = sum_t;
            sum_d = list.Count(p => p.port_type == "D");

            var list1 = from aa in g.asset_list.Where(p => CatalogType.is_eb(p.catalog_id))
                        join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                        join cc in g.location_list.Where(p => p.rack_id == rack_id) on aa.location_id equals cc.location_id
                        select new { aa.asset_id };
            sum_e = list1.Count();
            list2 = list1.Select(p => p.asset_id).ToList();
            return list2;
        }

        // --------------------------------------------------------------------
        // 6. 통계 추가 
        // --------------------------------------------------------------------

        // 사용된 지능형 패치 패널 포트에서 연결된 포트의 카운트 (dd.ipp_port_status == "L")  
        public static int get_used_ipp_ports_linked_by_site_id(int id, AssetTreeType type)
        {
            int sum = 0;
            switch (type)
            {
                case AssetTreeType.Site:
                    var list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                               join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.site_id == id) on aa.location_id equals cc.location_id
                               join dd in g.asset_ipp_port_link_list on aa.asset_id equals dd.ipp_asset_id
                               where (dd.ipp_port_status == "L" || dd.ipp_port_status == "P")
                               select new { dd.ipp_port_status };
                    sum = list.Count();
                    break;
                case AssetTreeType.Building:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                               join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.building_id == id) on aa.location_id equals cc.location_id
                               join dd in g.asset_ipp_port_link_list on aa.asset_id equals dd.ipp_asset_id
                           where (dd.ipp_port_status == "L" || dd.ipp_port_status == "P")
                               select new { dd.ipp_port_status };
                    sum = list.Count();
                    break;
                case AssetTreeType.Floor:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                               join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                           join cc in g.location_list.Where(p => p.floor_id == id) on aa.location_id equals cc.location_id
                               join dd in g.asset_ipp_port_link_list on aa.asset_id equals dd.ipp_asset_id
                           where (dd.ipp_port_status == "L" || dd.ipp_port_status == "P")
                               select new { dd.ipp_port_status };
                    sum = list.Count();
                    break;
                case AssetTreeType.Room:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                               join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.room_id == id) on aa.location_id equals cc.location_id
                               join dd in g.asset_ipp_port_link_list on aa.asset_id equals dd.ipp_asset_id
                           where (dd.ipp_port_status == "L" || dd.ipp_port_status == "P")
                               select new { dd.ipp_port_status };
                    sum = list.Count();
                    break;
                case AssetTreeType.Rack:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                               join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.rack_id == id) on aa.location_id equals cc.location_id
                               join dd in g.asset_ipp_port_link_list on aa.asset_id equals dd.ipp_asset_id
                           where (dd.ipp_port_status == "L" || dd.ipp_port_status == "P")
                               select new { dd.ipp_port_status };
                    sum = list.Count();
                    break;
                default:
                    break;
            }
            return sum;
        }

        // 사용된 지능형 패치 패널 포트 수
        public static int get_tot_ipp_ports_by_site_id(int id, AssetTreeType type)
        {
            int sum = 0;
            switch (type)
            {
                case AssetTreeType.Site:
                    var list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                               join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.site_id == id) on aa.location_id equals cc.location_id
                               select new { bb.num_of_ports };
                    foreach (var node in list)
                        sum += node.num_of_ports;
                    break;
                case AssetTreeType.Building:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                               join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.building_id == id) on aa.location_id equals cc.location_id
                               select new { bb.num_of_ports };
                    foreach (var node in list)
                        sum += node.num_of_ports;
                    break;
                case AssetTreeType.Floor:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                               join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.floor_id == id) on aa.location_id equals cc.location_id
                               select new { bb.num_of_ports };
                    foreach (var node in list)
                        sum += node.num_of_ports;
                    break;
                case AssetTreeType.Room:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                               join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.room_id == id) on aa.location_id equals cc.location_id
                               select new { bb.num_of_ports };
                    foreach (var node in list)
                        sum += node.num_of_ports;
                    break;
                case AssetTreeType.Rack:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_ipp(p.catalog_id))
                               join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.rack_id == id) on aa.location_id equals cc.location_id
                               select new { bb.num_of_ports };
                    foreach (var node in list)
                        sum += node.num_of_ports;
                    break;
                default:
                    break;
            }
            return sum;
        }

        // 스위치의 전체 포트 수
        public static int get_tot_sw_ports_by_site_id(int id, AssetTreeType type)
        {
            int sum = 0;
            switch (type)
            {
                case AssetTreeType.Site:
                    var list = from aa in g.asset_list.Where(p => CatalogType.is_sw(p.catalog_id))
                           join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                           join cc in g.location_list.Where(p => p.site_id == id) on aa.location_id equals cc.location_id
                           select new { bb.num_of_ports };
                    foreach (var node in list) sum += node.num_of_ports;
                    break;
                case AssetTreeType.Building:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_sw(p.catalog_id))
                           join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                           join cc in g.location_list.Where(p => p.building_id == id) on aa.location_id equals cc.location_id
                           select new { bb.num_of_ports };
                    foreach (var node in list) sum += node.num_of_ports;
                    break;
                case AssetTreeType.Floor:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_sw(p.catalog_id))
                           join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                           join cc in g.location_list.Where(p => p.floor_id == id) on aa.location_id equals cc.location_id
                           select new { bb.num_of_ports };
                    foreach (var node in list) sum += node.num_of_ports;
                    break;
                case AssetTreeType.Room:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_sw(p.catalog_id))
                           join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                           join cc in g.location_list.Where(p => p.room_id == id) on aa.location_id equals cc.location_id
                           select new { bb.num_of_ports };
                    foreach (var node in list) sum += node.num_of_ports;
                    break;
                case AssetTreeType.Rack:
                    list = from aa in g.asset_list.Where(p => CatalogType.is_sw(p.catalog_id))
                           join bb in g.catalog_list on aa.catalog_id equals bb.catalog_id
                           join cc in g.location_list.Where(p => p.rack_id == id) on aa.location_id equals cc.location_id
                           select new { bb.num_of_ports };
                    foreach (var node in list) sum += node.num_of_ports;
                    break;
                default:
                    break;
            }


            return sum;
        }

        // 사용중인 스위치 포트수 
        public static int get_used_sw_ports_by_site_id(int id, AssetTreeType type)
        {
            int sum = 0;
            switch (type)
            {
                case AssetTreeType.Site:
                    var list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_sw(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.site_id == id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
                    sum = list.Count();
                    break;
                case AssetTreeType.Building:
                    list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_sw(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.building_id == id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
                    sum = list.Count();
                    break;
                case AssetTreeType.Floor:
                    list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_sw(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.floor_id == id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
                    sum = list.Count();
                    break;
                case AssetTreeType.Room:
                    list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_sw(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.room_id == id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
                    sum = list.Count();
                    break;
                case AssetTreeType.Rack:
                    list = from aa in g.asset_port_link_list.Where(p => (p.front_asset_id > 0))
                       join bb in g.asset_list.Where(p => CatalogType.is_sw(p.catalog_id)) on aa.asset_id equals bb.asset_id
                       join cc in g.location_list.Where(p => p.rack_id == id) on bb.location_id equals cc.location_id
                       select new { aa.asset_id, aa.port_no };
                    sum = list.Count();
                    break;
                default:
                    break;
            }
            return sum;
        }

        // rack 의 전체 공간 수
        public static int get_tot_rack_by_site_id(int id, AssetTreeType type)
        {
            int sum = 0;
            switch (type)
            {
                case AssetTreeType.Site:
                    var list = from aa in g.rack_list.Where(p => p.rack_catalog_id > 0)
                               join bb in g.catalog_list on aa.rack_catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.site_id == id) on aa.rack_id equals cc.rack_id
                               select new { bb.rm_unit_size };
                    foreach (var node in list)
                        sum += node.rm_unit_size ?? 0;
                    break;
                case AssetTreeType.Building:
                    list = from aa in g.rack_list.Where(p => p.rack_catalog_id > 0)
                               join bb in g.catalog_list on aa.rack_catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.building_id == id) on aa.rack_id equals cc.rack_id
                               select new { bb.rm_unit_size };
                    foreach (var node in list)
                        sum += node.rm_unit_size ?? 0;
                    break;
                case AssetTreeType.Floor:
                    list = from aa in g.rack_list.Where(p => p.rack_catalog_id > 0)
                               join bb in g.catalog_list on aa.rack_catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.floor_id == id) on aa.rack_id equals cc.rack_id
                               select new { bb.rm_unit_size };
                    foreach (var node in list)
                        sum += node.rm_unit_size ?? 0;
                    break;
                case AssetTreeType.Room:
                    list = from aa in g.rack_list.Where(p => p.rack_catalog_id > 0)
                               join bb in g.catalog_list on aa.rack_catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.room_id == id) on aa.rack_id equals cc.rack_id
                               select new { bb.rm_unit_size };
                    foreach (var node in list)
                        sum += node.rm_unit_size ?? 0;
                    break;
                case AssetTreeType.Rack:
                    list = from aa in g.rack_list.Where(p => p.rack_catalog_id > 0)
                               join bb in g.catalog_list on aa.rack_catalog_id equals bb.catalog_id
                               join cc in g.location_list.Where(p => p.rack_id == id) on aa.rack_id equals cc.rack_id
                               select new { bb.rm_unit_size };
                    foreach (var node in list)
                        sum += node.rm_unit_size ?? 0;
                    break;
                default:
                    break;
            }
            return sum;
        }

        // 사용중인 rack 공간수 
        public static int get_used_rack_by_site_id(int id, AssetTreeType type)
        {
            int sum = 0;
            switch (type)
            {
                case AssetTreeType.Site:
                    var list = from aa in g.rack_list.Where(p => p.rack_catalog_id > 0)
                                join bb in g.catalog_list on aa.rack_catalog_id equals bb.catalog_id
                                join cc in g.location_list.Where(p => p.site_id == id) on aa.rack_id equals cc.rack_id
                                select new { aa.rack_id};
                    foreach (var node in list)
                    {
                        var list2 = g.rack_config_list.Where(p => p.rack_id == node.rack_id);
                        foreach (var node2 in list2)
                        {
                            int s = CatalogType.get_unit_size(node2.catalog_id ?? 0);
                            sum += s;
                        }
                    }
                    break;
                case AssetTreeType.Building:
                    list = from aa in g.rack_list.Where(p => p.rack_catalog_id > 0)
                                join bb in g.catalog_list on aa.rack_catalog_id equals bb.catalog_id
                                join cc in g.location_list.Where(p => p.building_id == id) on aa.rack_id equals cc.rack_id
                                select new { aa.rack_id};
                    foreach (var node in list)
                    {
                        var list2 = g.rack_config_list.Where(p => p.rack_id == node.rack_id);
                        foreach (var node2 in list2)
                        {
                            int s = CatalogType.get_unit_size(node2.catalog_id ?? 0);
                            sum += s;
                        }
                    }
                    break;
                case AssetTreeType.Floor:
                    list = from aa in g.rack_list.Where(p => p.rack_catalog_id > 0)
                                join bb in g.catalog_list on aa.rack_catalog_id equals bb.catalog_id
                                join cc in g.location_list.Where(p => p.floor_id == id) on aa.rack_id equals cc.rack_id
                                select new { aa.rack_id};
                    foreach (var node in list)
                    {
                        var list2 = g.rack_config_list.Where(p => p.rack_id == node.rack_id);
                        foreach (var node2 in list2)
                        {
                            int s = CatalogType.get_unit_size(node2.catalog_id ?? 0);
                            sum += s;
                        }
                    }
                    break;
                case AssetTreeType.Room:
                    list = from aa in g.rack_list.Where(p => p.rack_catalog_id > 0)
                                join bb in g.catalog_list on aa.rack_catalog_id equals bb.catalog_id
                                join cc in g.location_list.Where(p => p.room_id == id) on aa.rack_id equals cc.rack_id
                                select new { aa.rack_id};
                    foreach (var node in list)
                    {
                        var list2 = g.rack_config_list.Where(p => p.rack_id == node.rack_id);
                        foreach (var node2 in list2)
                        {
                            int s = CatalogType.get_unit_size(node2.catalog_id ?? 0);
                            sum += s;
                        }
                    }
                    break;
                case AssetTreeType.Rack:
                    list = from aa in g.rack_list.Where(p => p.rack_catalog_id > 0)
                                join bb in g.catalog_list on aa.rack_catalog_id equals bb.catalog_id
                                join cc in g.location_list.Where(p => p.rack_id == id) on aa.rack_id equals cc.rack_id
                                select new { aa.rack_id};
                    foreach (var node in list)
                    {
                        var list2 = g.rack_config_list.Where(p => p.rack_id == node.rack_id);
                        foreach (var node2 in list2)
                        {
                            int s = CatalogType.get_unit_size(node2.catalog_id ?? 0);
                            sum += s;
                        }
                    }
                    break;
                default:
                    break;
            }
            return sum;
        }

        // 터미날 총수 
        public static async Task<int> get_tot_terminal_by_site_id(int site_id)
        {
            DateTime day = DateTime.Today;

            string filter;
            filter = string.Format("?site_id={0}&year={1}&month={2}&day={3}", site_id, day.Year, day.Month, day.Day);
            var v1 = (List<sp_stat_terminal_data_hour_Result>)await g.webapi.getList("sp_stat_terminal_data_hour", typeof(List<sp_stat_terminal_data_hour_Result>), filter);
            if (v1 == null) return 0;
            if (v1.Count == 0) return 0;

            int a1 = v1.Last().avg_of_act_terminal ?? 0;
            int a2 = v1.Last().avg_of_tot_terminal ?? 0;
            return a2;
        }

        // 사용중인 터미날 수 
        public static async Task<int> get_used_terminal_by_site_id(int site_id)
        {
            DateTime day = DateTime.Today;

            string filter;
            filter = string.Format("?site_id={0}&year={1}&month={2}&day={3}", site_id, day.Year, day.Month, day.Day);
            var v1 = (List<sp_stat_terminal_data_hour_Result>)await g.webapi.getList("sp_stat_terminal_data_hour", typeof(List<sp_stat_terminal_data_hour_Result>), filter);
            if (v1 == null) return 0;
            if (v1.Count == 0) return 0;

            int a1 = v1.Last().avg_of_act_terminal ?? 0;
            int a2 = v1.Last().avg_of_tot_terminal ?? 0;
            return a1;
        }

        // 터미날 접속 수 - 시간대별  
        public static async Task<object> get_terminal_by_site_id(int site_id)
        {
            DateTime day = DateTime.Today;

            string filter;
            filter = string.Format("?site_id={0}&year={1}&month={2}&day={3}", site_id, day.Year, day.Month, day.Day);
            var v1 = (List<sp_stat_terminal_data_hour_Result>)await g.webapi.getList("sp_stat_terminal_data_hour", typeof(List<sp_stat_terminal_data_hour_Result>), filter);
            if (v1 == null) return null;
            //if (v1.Count == 0) return null;

            return v1;
        }
    }
}
