using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using I2MS2.Models;
using WebApi.Models;
using System.Windows;

namespace I2MS2.Library
{
    // 링크 다이어 그램 화면 처리 관리용 
    public class LinkDiagram
    {
        #region 쉬트에 자산 및 케이블 배치 관련
        // 쉬트 열기 
        public void openAsset(int asset_id, int start_port_no, int stop_port_no, List<WorkCell> cell_list2)
        {
            int port_no;
            int rows = stop_port_no - start_port_no + 1;
            int i;
            int idx;

            // cell_list2 를 초기화 한다.
            // 그리드 모양의 쉬트를 초기화하는데 세로로 시작번호 부터 끝번호까지 지정할 수 있다.
            init_cell_list(start_port_no, stop_port_no, cell_list2);

            // cell_list에 데이터를 담는다.
            for (i = 0; i < rows; i++)
            {
                port_no = start_port_no + i;
                idx = i * g.MAX_COL;

                // 디버그용
                if ((i == 18) && (port_no == 19))
                {
                }

                // 해당 가로줄에 자산을 배치한다. (세로축 출력할 포트를 지정한다.)
                init_asset_row(asset_id, start_port_no, port_no, cell_list2);

                // 자산이 배치된 후에 케이블을 연결한다.
                init_asset_row_cable(asset_id, idx, cell_list2, false);

                // 새롭게 비인가플러그가 되어 추가로 그려야 할 지능형 패치 및 그 이하
                update_asset_row_for_linked_status(asset_id, start_port_no, port_no, cell_list2);

                // 케이블이 배치된 후 지능형 IPP에 대한 상태를 읽어 표시한다.
                init_asset_row_status(asset_id, start_port_no, port_no, cell_list2, false); 

                // 우측 지능형 패치의 좌측부분(플러그 및 알람등) 및 연결된 지능형 패치코드의 우측상태를 표시한다.
                update_asset_row_status_right_pp(asset_id, start_port_no, port_no, cell_list2);
                // 좌측 지능형 패치의 우측부분(플러그 및 알람등)  및 연결된 지능형 패치코드의 좌측상태를 표시한다.
                update_asset_row_status_left_pp(asset_id, start_port_no, port_no, cell_list2);
            }
        }

        // 사용할 cell_list2 공간에 대한 초기화 작업
        public void init_cell_list(int start_port_no, int stop_port_no, List<WorkCell> cell_list2)
        {
            int i, j;
            int rows = stop_port_no - start_port_no + 1;
            cell_list2.Clear();
            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < g.MAX_COL; j++)
                {
                    WorkCell cell = new WorkCell()
                    {
                        idx = i * g.MAX_COL + j,
                        row_no = i,
                        col_no = j,
                        template_type = "empty",
                        port_no = start_port_no + i,
                        left_alarm_visible = Visibility.Hidden,
                        right_alarm_visible = Visibility.Hidden,
                        left_wo_visible = Visibility.Hidden,
                        right_wo_visible = Visibility.Hidden,
                    };
                    cell_list2.Add(cell);
                }
            }
        }

        // cell_list2 공간에 자산을 배치한다. (초기화시만 사용)
        public void init_asset_row(int asset_id, int start_port_no, int port_no, List<WorkCell> cell_list2)
        {
            int find_asset_id = 0;
            int find_port_no = 0;
            string find_plug_side = "F";
            int y = port_no - start_port_no;
            int rel_pos = 0;
            int lg_prj = 0;
            bool is_right_to_left = false;
            WorkCell center = cell_list2[y * g.MAX_COL + g.CENTER_COL];

            // 중앙에 기준 자산을 드로잉한다. 우측에 Front를 그리고자 하면 "R"을 대입
            updateCell(asset_id, port_no, center, true, false, "R", false);

            get_left_asset(asset_id, port_no, ref find_asset_id, ref find_port_no, ref find_plug_side, ref rel_pos, ref lg_prj); // 2016-12-09 마곡지구

            // 해당 포트가 존재하지 않는 경우
            if (find_asset_id == 0)
                return;

            int catalog_id = 0;
            string catalog_name;
                asset a = g.asset_list.Find(e => e.asset_id == find_asset_id);
            if (a != null)
            {
                catalog c = g.catalog_list.First(e => e.catalog_id == a.catalog_id);
                if (c != null)
                {
                    catalog_id = a.catalog_id;
                    catalog_name = c.catalog_name;

                    // 카탈로그ID가 434001이면 허브이며 허브인 경우 좌측부터 표시가 불가능해서 우측부터 검색 필요
                    is_right_to_left = (catalog_id == g.VIRTUAL_HUB_CATALOG_ID) || (catalog_name == "Virtual HUB");

                    if (is_right_to_left)
                    {
                        rel_pos = 0;
                        get_right_asset(asset_id, port_no, ref find_asset_id, ref find_port_no, ref find_plug_side, ref rel_pos, ref lg_prj); // 2016-12-09 마곡지구
                    }
                }

                //init_asset_row_sub(find_asset_id, find_port_no, y, rel_pos, is_right_to_left, find_plug_side, cell_list2, false);
                init_asset_row_sub_lg(find_asset_id, find_port_no, y, rel_pos, is_right_to_left, find_plug_side, cell_list2, false, lg_prj);
            }
        }

        // find_asset_id, 
        // find_port_no :       그리고자 하는 처음 자산 ID와 포트 번호
        // rel_pos :            중앙을 기준으로 처음 그리고자 하는 상대 위치 (예:중앙이 10이고 rel_pos가 -2 이면 8의 위치부터 drawing한다.)
        // is_right_to_left :   우측에서 좌측으로 그려야 하는 경우(처음 그려야 하는 자산이 허브인 경우 우측에서 좌측으로 그려야함.)
        // find_plug_side :     마지막 검색한 자산의 앞뒤중 하나가 좌측 방향에 표시됨. : F = Front가 좌측, R = Rear가 좌측
        //                      가장 좌측이 스위치로부터 시작된 경우 R부터 시작...

        public void init_asset_row_sub_lg(int find_asset_id, int find_port_no, int y, int rel_pos, bool is_right_to_left, string find_plug_side, List<WorkCell> cell_list2, bool is_ins_mark, int lg_prj)
        {
            int idx;
            int x = 0;

            while (true)
            {
                x = g.CENTER_COL + rel_pos;
                idx = y * g.MAX_COL + x;

                // 좌우 그릴 방향에 따라 두 칸씩 이동
                if (is_right_to_left)
                    rel_pos -= 2;
                else
                    rel_pos += 2;

                bool is_center_cell = x == g.CENTER_COL;
                WorkCell cell = cell_list2[idx];
                if (lg_prj == 0)
                {
                    bool b = updateCell(find_asset_id, find_port_no, cell, is_center_cell, is_right_to_left, find_plug_side, is_ins_mark);
                    if (!b)
                        break;
                }
                else if (lg_prj == 1 && (cell.col_no > 7 && cell.col_no < 15))
                {
                    bool b = updateCell(find_asset_id, find_port_no, cell, is_center_cell, is_right_to_left, find_plug_side, is_ins_mark);
                    if (!b)
                        break;
                }

                if (cell.col_no == 14 && CatalogType.is_ipp_fp(cell.catalog_id))     // 2016-12-12 마곡 
                    break;
                if (cell.col_no < 7 && CatalogType.is_pp(cell.catalog_id))
                    break;
                if (cell.col_no > 15 && CatalogType.is_pp(cell.catalog_id))
                    break;
                var apl = g.asset_port_link_list.Find(e => e.asset_id == find_asset_id && e.port_no == find_port_no);
                int front_asset_id = apl.front_asset_id ?? 0;
                int front_port_no = apl.front_port_no ?? 0;
                string front_plug_side = apl.front_plug_side;
                int rear_asset_id = apl.rear_asset_id ?? 0;
                int rear_port_no = apl.rear_port_no ?? 0;
                string rear_plug_side = apl.rear_plug_side;

                // 좌측이 Front이면?
                if (find_plug_side == "F")
                {
                    if (rear_asset_id == 0)
                        break;
                    find_asset_id = rear_asset_id;
                    find_port_no = rear_port_no;
                    find_plug_side = rear_plug_side;
                }
                else
                {
                    if (front_asset_id == 0)
                        break;
                    find_asset_id = front_asset_id;
                    find_port_no = front_port_no;
                    find_plug_side = front_plug_side;
                }
            }
        }

        // cell_list2 공간에 자산을 배치한다. (자산을 추가한 경우...사용)
        public void init_asset_row2(int asset_id, int port_no, int idx, List<WorkCell> cell_list2, bool is_ins_mark)
        {
            int find_asset_id = asset_id;
            int find_port_no = port_no;
            WorkCell cell = cell_list2[idx];
            int y = cell.row_no;
            // rel_pos은 중앙을 기준으로 +- 값이 담김. (예로 -2는 8번째칸을 의미)
            int rel_pos = cell.col_no - g.CENTER_COL;
            // 중앙보다 좌측을 검색한 경우 우측에서 좌측으로 그리기 위함(용도가 있을지 모름)
            bool is_right_to_left = rel_pos < 0;
            var apl = g.asset_port_link_list.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no));
            if (apl == null)
                return;

            // 양쪽에 연결이 있는 경우 곤란....
            //if (((apl.rear_asset_id ?? 0) != 0) && ((apl.front_asset_id ?? 0) != 0))
            //    return;

            // romee 2016.01.27 사용자 입력 부분 오류 점검     
            // I2MS_V21 : 배선뷰화면에서 아울렛 연결 방지 처리 
            if (is_ins_mark)   // 사용자 입력이고 
            {
                // 이전셀 점검 2016.01.27 romee
                // 컬럼 6번 이하 와 컬럼 12번 이상에서 전단이 스위치인 경우 스위치나 아울렛은 못옴 
                int p_catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
                int c_catalog_id = 0;

                if (cell.col_no < 8 || cell.col_no > 14)
                {
                    if (cell.col_no < 8)
                    {
                        WorkCell tcell = cell_list2[idx + 2];
                        c_catalog_id = Etc.get_catalog_id_by_asset_id(tcell.asset_id);
                    }
                    else if (cell.col_no > 14)
                    {
                        WorkCell tcell = cell_list2[idx - 2];
                        c_catalog_id = Etc.get_catalog_id_by_asset_id(tcell.asset_id);
                    }
                    // 스위치이고 아울렛이면 
                    if (CatalogType.is_sw(c_catalog_id) && CatalogType.is_fp(p_catalog_id))
                    {
                        return;
                    } // 전단에 아무것도 없는데 아울렛만 삽입하는 경우 안됨 -> 이후에 스위치가 올지 모름  
                    else if (c_catalog_id == 0 && CatalogType.is_fp(p_catalog_id))
                    {
                        return;
                    }
                }
            }

            string find_plug_side;
            int lg_prj = 0;

            // IPP 이면서 우측(12번째칸)에 PP가 있는 경우 우측이 rear가 되어야 함. (크로스커넥트)
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            if (CatalogType.is_ipp(catalog_id) && (cell.col_no == (g.CENTER_COL + 2)))
                find_plug_side = "F";   // 반대를 표시..
            else if (CatalogType.is_ipp_fp(catalog_id) && (cell.col_no == 14))   // 마곡 간선
            {
                find_plug_side = "R";   // 반대를 표시..
                lg_prj = 1;
            }
            else if (CatalogType.is_ipp_fp(catalog_id) && (cell.col_no == 8))   // 마곡 간선
            {
                find_plug_side = "R";   // 반대를 표시..
                lg_prj = 1;
            }
            // iFDF 이면서 7이하면 이전 거 처리 리턴  2017.07.06 romee
            else if (CatalogType.is_ipp_fp(catalog_id) && (cell.col_no < 8 || cell.col_no > 14))   // 마곡 간선
            {
                return;
            }
            // 인터커넥트 인 경우
            else if (CatalogType.is_sw(catalog_id) && (cell.col_no == (g.CENTER_COL + 2)))
                find_plug_side = "F";   // 반대를 표시..  IC 연결 방식인경우
            // 아웃렛인경우 real 이 항상 지능형으로 와야함, 무조건 반대로 그리기 romee 2016.02.16 I2MS_V21
            else if (CatalogType.is_cp(catalog_id) || CatalogType.is_fp(catalog_id) || CatalogType.is_mb(catalog_id))
            {
//                find_plug_side = (apl.front_asset_id ?? 0) == 0 ? "R" : "F";
                find_plug_side = "R";
            }    
            else
                find_plug_side = (apl.front_asset_id ?? 0) == 0 ? "F" : "R";

            // 실제 배치를 여기서 함
            //init_asset_row_sub(find_asset_id, find_port_no, y, rel_pos, is_right_to_left, find_plug_side, cell_list2, is_ins_mark);
            init_asset_row_sub_lg(find_asset_id, find_port_no, y, rel_pos, is_right_to_left, find_plug_side, cell_list2, is_ins_mark, lg_prj);
        }

        // find_asset_id, 
        // find_port_no :       그리고자 하는 처음 자산 ID와 포트 번호
        // rel_pos :            중앙을 기준으로 처음 그리고자 하는 상대 위치 (예:중앙이 10이고 rel_pos가 -2 이면 8의 위치부터 drawing한다.)
        // is_right_to_left :   우측에서 좌측으로 그려야 하는 경우(처음 그려야 하는 자산이 허브인 경우 우측에서 좌측으로 그려야함.)
        // find_plug_side :     마지막 검색한 자산의 앞뒤중 하나가 좌측 방향에 표시됨. : F = Front가 좌측, R = Rear가 좌측
        //                      가장 좌측이 스위치로부터 시작된 경우 R부터 시작...

        public void init_asset_row_sub(int find_asset_id, int find_port_no, int y, int rel_pos, bool is_right_to_left, string find_plug_side, List<WorkCell> cell_list2, bool is_ins_mark)
        {
            int idx;
            int x = 0;

            while (true)
            {
                x = g.CENTER_COL + rel_pos;
                idx = y * g.MAX_COL + x;

                // 좌우 그릴 방향에 따라 두 칸씩 이동
                if (is_right_to_left)
                    rel_pos -= 2;
                else
                    rel_pos += 2;

                bool is_center_cell = x == g.CENTER_COL;
                WorkCell cell = cell_list2[idx];
                bool b = updateCell(find_asset_id, find_port_no, cell, is_center_cell, is_right_to_left, find_plug_side, is_ins_mark);
                if (!b)
                    break;

                var apl = g.asset_port_link_list.Find(e => e.asset_id == find_asset_id && e.port_no == find_port_no);
                int front_asset_id = apl.front_asset_id ?? 0;
                int front_port_no = apl.front_port_no ?? 0;
                string front_plug_side = apl.front_plug_side;
                int rear_asset_id = apl.rear_asset_id ?? 0;
                int rear_port_no = apl.rear_port_no ?? 0;
                string rear_plug_side = apl.rear_plug_side;

                // 좌측이 Front이면?
                if (find_plug_side == "F")
                {
                    if (rear_asset_id == 0)
                        break;
                    find_asset_id = rear_asset_id;
                    find_port_no = rear_port_no;
                    find_plug_side = rear_plug_side;
                }
                else
                {
                    if (front_asset_id == 0)
                        break;
                    find_asset_id = front_asset_id;
                    find_port_no = front_port_no;
                    find_plug_side = front_plug_side;
                }
            }
        }


        // cell_list2 공간에 케이블을 배치한다.
        public void init_asset_row_cable(int asset_id, int idx, List<WorkCell> cell_list2, bool is_ins_mark)
        {
            int x;
            int idx2;
            WorkCell cell = cell_list2[idx];
            int start_no = 0;

            if (cell.col_no > g.CENTER_COL)
                start_no = cell.col_no;

            // 케이블 연결

            for(x = start_no; x < g.MAX_COL; x += 2)
            {
                idx2 = idx + x - cell.col_no ;

                makeConnection(cell_list2, idx2, is_ins_mark);
            }
        }


        // x=col_no    : 좌측에 있는 자산을 기준으로 우측 자산과 연결한다. 
        public void makeConnection(List<WorkCell> cell_list2, int idx, bool is_ins_mark)
        {
            int cable_catalog_id = 0;
            int x = cell_list2[idx].col_no;
            if (x >= (g.MAX_COL - 2))
                return;

            WorkCell left = cell_list2[idx];
            WorkCell cable = cell_list2[idx + 1];
            WorkCell right = cell_list2[idx + 2];

            // front 쪽이 우측과 연결된 경우 : Front --> 
            if ((left.front_asset_id == right.asset_id) && (left.front_asset_id != 0))
            {
                left.is_left_front = false;
                cable_catalog_id = left.front_cable_catalog_id;
                if (cable_catalog_id == 0)
                {
                    cable_catalog_id = 415001; // FDF 가 카달로그에서 케이블을 못 가져오면 기본 케이블 정보로 처리 romee 2/16
                }
            }
            // rear 쪽이 우측과 연결된 경우 : Rear --> 
            if ((left.rear_asset_id == right.asset_id) && (left.rear_asset_id != 0))
            {
                cell_list2[idx].is_left_front = true;
                cable_catalog_id = left.rear_cable_catalog_id;
            }
            // 카탈로그ID가 434001이면 허브이며, 이러한 경우 좌측부터 표시가 불가능해서 우측에 있는 자산의 프론트 케이블 정보를 가져온다.
            if ((left.catalog_id == 434001) || (left.catalog_name == "Virtual HUB"))
            {
                left.is_left_front = false; // 좌측셀이 허브인경우 무조건 우측이 front
                cable_catalog_id = right.front_cable_catalog_id;        // 허브와 연결되는 장치의 방향은 항상 프론트?  Face Plate나 Mutoa Box의 경우는 프론트임.
            }
            if ((right.catalog_id == 434001) || (right.catalog_name == "Virtual HUB"))
            {
                right.is_left_front = true; // 우측셀이 허브인경우 무조건 좌측이 front
                cable_catalog_id = left.front_cable_catalog_id;
            }
            if (cable.catalog_id != cable_catalog_id)
                cable.is_ins_mark = is_ins_mark;

            drawConnection(cell_list2, idx, cable_catalog_id);
        }



        // x=col_no    : 좌측에 있는 자산을 기준으로 우측 자산과 연결 상태를 표시한다.
        public void drawConnection(List<WorkCell> cell_list2, int idx, int cable_catalog_id)
        {
            int x = cell_list2[idx].col_no;
            if (x >= (g.MAX_COL - 2))
                return;

            WorkCell left = cell_list2[idx];
            WorkCell cable = cell_list2[idx + 1];
            WorkCell right = cell_list2[idx + 2];

            if (cable_catalog_id > 0)
            {
                catalog c = g.catalog_list.Find(e => e.catalog_id == cable_catalog_id);
                string catalog_name = c != null ? c.catalog_name : "";
                string ca_disp_name = c != null ? c.ca_disp_name : "";
                Color color = c != null ? CatalogType.get_color_rgba((uint)(c.ca_disp_color_rgb ?? 0x00ffffff)) : Colors.Transparent;

                left.right_plug_status = ePortStatus.Plugged;
                left.right_ca_disp_color_rgb = color;
                if (left.is_left_front)
                {
                    left.rear_plug_status = ePortStatus.Plugged;        // depre
                    left.rear_ca_disp_color_rgb = color;                // depre
                }
                else
                {
                    left.front_plug_status = ePortStatus.Plugged;       // depre
                    left.front_ca_disp_color_rgb = color;               // depre
                }

                cable.catalog_id = cable_catalog_id;
                cable.template_type = "cable";
                cable.catalog_name = catalog_name;
                cable.ca_disp_name = ca_disp_name;
                cable.ca_disp_color_rgb = color;
                cable.left_plug_status = ePortStatus.Plugged;
                cable.right_plug_status = ePortStatus.Plugged;

                right.left_plug_status = ePortStatus.Plugged;
                right.left_ca_disp_color_rgb = color;
                if (right.is_left_front)
                {
                    right.front_plug_status = ePortStatus.Plugged;       // depre
                    right.front_ca_disp_color_rgb = color;               // depre
                }
                else
                {
                    right.rear_plug_status = ePortStatus.Plugged;        // depre
                    right.rear_ca_disp_color_rgb = color;                // depre
                }
            }
            else
            {
                left.right_plug_status = ePortStatus.Unplugged;
                left.right_ca_disp_color_rgb = Colors.Transparent;

                cable.catalog_id = 0;
                cable.template_type = "empty";
                cable.catalog_name = "";
                cable.ca_disp_name = "";
                cable.left_plug_status = ePortStatus.Unplugged;
                cable.right_plug_status = ePortStatus.Unplugged;
                cable.left_ca_disp_color_rgb = Colors.Transparent;
                cable.right_ca_disp_color_rgb = Colors.Transparent;

                right.left_plug_status = ePortStatus.Unplugged;
                right.left_ca_disp_color_rgb = Colors.Transparent;
            }
            left.force_changed = true;
            cable.force_changed = true;
            right.force_changed = true;
        }

        // 새롭게 비인가플러그가 되어 추가로 그려야 할 지능형 패치 및 그 이하
        // 인수 중 asset_id는 지능형 패치 패널만 인입
        public void update_asset_row_for_linked_status(int asset_id, int start_port_no, int port_no, List<WorkCell> cell_list2)
        {
            var left = cell_list2.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no));
            if (left == null)
                return;
            if (left.col_no != g.CENTER_COL)
                return;
            var cable = cell_list2[left.idx + 1];

            var aipl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == asset_id) && (p.port_no == port_no));
            if (aipl == null)
                return;
            // DB에 고정연결은 없는 대신 PP 스캐닝된 포트 상태가 연결되어 있는 상태라면....
            if ((aipl.remote_ic_asset_id > 0) && (cable.template_type != "cable"))
            {
                int remote_ipp_asset_id = aipl.remote_pp_asset_id ?? 0;
                int remote_port_no = aipl.remote_port_no ?? 0;
                add_connected_asset_to_screen_ex(asset_id, port_no, remote_ipp_asset_id, remote_port_no, ePortStatus.Linked, cell_list2, true, false);
            }
            /*
                        // 한번 더 리모트에서 검색...
                        var aipl2 = g.asset_ipp_port_link_list.Find(p => (p.remote_pp_asset_id == asset_id) && (p.remote_port_no == port_no));
                        if (aipl == null)
                            return;
                        // DB에 고정연결은 없는 대신 PP 스캐닝된 포트 상태가 연결되어 있는 상태라면....
                        if (cable.template_type != "cable")
                        {
                            int remote_ipp_asset_id = aipl.remote_pp_asset_id ?? 0;
                            int remote_port_no = aipl.remote_port_no ?? 0;
                            // 리모트와 위치만 바꿔서 호출...
                            add_connected_asset_to_screen(remote_ipp_asset_id, remote_port_no, asset_id, port_no, ePortStatus.Linked, cell_list2);
                        }
            */
        }

        // 지능형 패치의 상태를 가져와서 자산 셀에 저장
        // user_add 는 사용자가 추가한 경우에만 true
        public void init_asset_row_status(int asset_id, int start_port_no, int port_no, List<WorkCell> cell_list2, bool user_add)
        {
            asset_ipp_port_link aipl;
            int idx;

            // Step 1. 다이어그램에서 최초 검색된 PP에 대해 .....
            var node = find_ipp_cell(cell_list2, asset_id, port_no);
            if (node == null)
                return;

            idx = node.idx;
            aipl = g.asset_ipp_port_link_list.Find(e => e.ipp_asset_id == node.asset_id && e.port_no == node.port_no);
            // 정보 처리 

            // 좌우에 있는 패치 정보를 가져온다.
            int idx2 = get_near_ipp_idx2(cell_list2, node);   // 2016-12-09 마곡지구 
            if (idx2 == -1)
            {
                // PP가 하나만 있는 경우
                if (aipl != null)
                {
                    // 리모트 연결정보는 없으므로 생략...
                    // 아래 상태는 aipl에 있는 정보를 그대로 대입
                    node.plug_status = get_port_status(aipl.ipp_port_status);
                    node.alarm_status = get_alarm_status(aipl.alarm_status);
                    node.wo_status = get_wo_status(aipl.wo_status);
                    node.trace_status = get_trace_status(aipl.is_port_trace);
                }
            }
            else
            {
                // PP가 근처에 또 있는 경우
                var node2 = cell_list2[idx2];

                node.remote_ic_asset_id = Etc.get_ic_asset_id_by_ipp_asset_id(node2.asset_id);
                node.remote_pp_asset_id = node2.asset_id;
                node.remote_port_no = node2.port_no;
                if (user_add)
                    node.plug_status = ePortStatus.Linked;
                else
                {
                    node.plug_status = get_port_status(aipl.ipp_port_status);
                    node.alarm_status = get_alarm_status(aipl.alarm_status);
                    node.wo_status = get_wo_status(aipl.wo_status);
                    node.trace_status = get_trace_status(aipl.is_port_trace);
                }

                // Step 2. 근처에 있는 패치의 정보를 갱신
                node2.remote_ic_asset_id = Etc.get_ic_asset_id_by_ipp_asset_id(node.asset_id);
                node2.remote_pp_asset_id = node.asset_id;
                node2.remote_port_no = node.port_no;
                if (user_add)
                    node2.plug_status = ePortStatus.Linked;
                else
                {
                    var aipl2 = g.asset_ipp_port_link_list.Find(e => e.ipp_asset_id == node2.asset_id && e.port_no == node2.port_no);
                    if (aipl2 != null)
                    {
                        node2.plug_status = get_port_status(aipl2.ipp_port_status);
                        node2.alarm_status = get_alarm_status(aipl2.alarm_status);
                        node2.wo_status = get_wo_status(aipl2.wo_status);
                        node2.trace_status = get_trace_status(aipl2.is_port_trace);
                    }
                }
            }
        }


        // 자산을 검색하여 표시하는 것이 아니라... 해당 자산의 몇 번째 줄을 찾아 지능형 패치 상태를 바꾸게 만든다.
        // asset_id는 지능형 패치만 인수로 받아온다.            
        // 이 함수는 배선뷰(링크다이어그램 편집화면) 에서만 호출된다.
        public void update_asset_row_status(int asset_id, int start_port_no, int port_no, List<List<WorkCell>> cell_list3)
        {
            int y = port_no - start_port_no;
            int x = g.CENTER_COL;
            int idx = y * g.MAX_COL + x;

            // 센터를 기준으로 검색하되 port_no가 줄번호가 된다.
            asset_ipp_port_link aipl = g.asset_ipp_port_link_list.Find(ee => ee.ipp_asset_id == asset_id && ee.port_no == port_no);
            if (aipl == null)
                return;

            foreach(var cell_list2 in cell_list3)
            {
                if (cell_list2 != null)
                {
                    WorkCell cell = cell_list2.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no));
                    if (cell != null)
                    {
                        idx = cell.idx;
                        WorkCell left = null;
                        WorkCell cable = null;
                        WorkCell right = null;

                        cell.plug_status = get_port_status(aipl.ipp_port_status);
                        cell.alarm_status = get_alarm_status(aipl.alarm_status);
                        cell.wo_status = get_wo_status(aipl.wo_status);

                        Console.WriteLine("asset_id={0}, plug_status={1}, alarm_status={2}, wo_status={3}", asset_id, cell.plug_status, cell.alarm_status, cell.wo_status);

                        if (cell.col_no == g.CENTER_COL) // 중심 셀 변경 처리 
                        {
                            left = cell_list2[idx];
                            cable = cell_list2[idx+1];
                            right = cell_list2[idx + 2];
                            update_asset_row_status_left_pp(asset_id, start_port_no, port_no, cell_list2);
                            ePortStatus port_status = Etc.get_status_type(aipl.ipp_port_status);
                            delete_row_when_unplugged(left, cable, right, port_status,  cell_list2);
                        }
                        else if (cell.col_no == g.CENTER_COL + 2) // 12셀 변경 처리 
                        {
                            left = cell_list2[idx - 2];
                            cable = cell_list2[idx - 1];
                            right = cell_list2[idx];
                            update_asset_row_status_right_pp(asset_id, start_port_no, port_no, cell_list2);
                        }

                        left.force_changed = true;
                        cable.force_changed = true;
                        right.force_changed = true;
                    }
                }
            }
        }


  

        // 10번셀의 IPP 관련한 우측케이블 색상과 11번 패치코드와 관련된 좌측케이블 색상 설정을 ...
        public void update_asset_row_status_left_pp(int asset_id, int start_port_no, int port_no, List<WorkCell> cell_list2)
        {
            // 좌측 패치를 찾아온다.
            WorkCell left = get_left_ipp_cell(cell_list2, asset_id, port_no);
            if (left == null)
                return;
            int idx = left.idx;
            var cable = cell_list2[idx + 1];

            // 인텔리전트 PP 인경우에만 상태를 읽어온다.
            if (CatalogType.is_ipp(left.catalog_id))
            {
                ePortStatus plug_status = left.plug_status;
                left.right_plug_status = plug_status;
                cable.left_plug_status = plug_status;
                // 우측 케이블
                if ((plug_status == ePortStatus.Plugged) || (plug_status == ePortStatus.Linked))
                {
                    Color tmp_color = cable.ca_disp_color_rgb;
                    left.right_ca_disp_color_rgb = tmp_color;
                    cable.left_ca_disp_color_rgb = Colors.Transparent;
                }
                else
                {
                    Color tmp_color = cable.ca_disp_color_rgb;
                    left.right_ca_disp_color_rgb = Colors.Transparent;
                    cable.left_ca_disp_color_rgb = tmp_color;
                }

                // 우측 알람
                if (left.alarm_status != eAlarmStatus.None)
                    left.right_alarm_visible = Visibility.Visible;
                else
                    left.right_alarm_visible = Visibility.Hidden;

                // 우측 워크오더
                if (left.wo_status != eWorkStatus.None)
                    cable.left_wo_visible = Visibility.Visible;
                else
                    cable.left_wo_visible = Visibility.Hidden;
            }
        }

        // 12번셀의 IPP 관련한 좌측케이블 색상과 11번 패치코드와 관련된 우측케이블 색상 설정을 ...
        public void update_asset_row_status_right_pp(int asset_id, int start_port_no, int port_no, List<WorkCell> cell_list2)
        {
            WorkCell right = get_right_ipp_cell(cell_list2, asset_id, port_no);
            if (right == null)
                return;
            int idx = right.idx;
            var cable = cell_list2[idx - 1];

            // 상대방에 연결된 자산이 인텔리전트 PP 인경우에만 상태를 읽어온다.
            if (CatalogType.is_ipp(right.catalog_id))
            {
                ePortStatus plug_status = right.plug_status;
                right.left_plug_status = plug_status;
                cable.right_plug_status = plug_status;
                // 우측 IPP의 좌측 케이블 컬러와 중앙에 위치한 패치 케이블의 우측 부분의 컬러를 결정한다.
                if ((plug_status == ePortStatus.Plugged) || (plug_status == ePortStatus.Linked))
                {
                    Color tmp_color = cable.ca_disp_color_rgb;
                    right.left_ca_disp_color_rgb = tmp_color;
                    cable.right_ca_disp_color_rgb = Colors.Transparent;
                }
                else
                {
                    Color tmp_color = cable.ca_disp_color_rgb;
                    right.left_ca_disp_color_rgb = Colors.Transparent;
                    cable.right_ca_disp_color_rgb = tmp_color;
                }

                // 좌측 알람
                if (right.alarm_status != eAlarmStatus.None)
                    right.left_alarm_visible = Visibility.Visible;
                else
                    right.left_alarm_visible = Visibility.Hidden;

                // 좌측 워크오더
                if (right.wo_status != eWorkStatus.None)
                    cable.right_wo_visible = Visibility.Visible;
                else
                    cable.right_wo_visible = Visibility.Hidden;
            }
        }
        #endregion

        #region 자산 추가  (Ctrl-V, Drop, SignalR)

        // Ctrl-V
        // 배선관리에서 인입
        // 붙여넣기 명령
        // d: dest, asset_id, port_no: source
        public void cloneAssetCell(WorkCell d, int asset_id, int port_no, List<List<WorkCell>> cell_list3, List<WorkCell> cell_list2)
        {
            //d.port_no = port_no;
            //d.is_ins_mark = true;
            //d.is_del_mark = false;
            //cloneCell(d, s);
            //d.force_changed = true;
            addAsset2WorkCell(d, asset_id, port_no, cell_list3, cell_list2, true);
        }


        // 사용자가 자산을 추가한 경우
        public bool addAsset2WorkCell(WorkCell d, int asset_id, int source_port_no, List<List<WorkCell>> cell_list3, List<WorkCell> cell_list2, bool add_user)
        {
            return addAsset2WorkCellEx(d, asset_id, source_port_no, cell_list3, cell_list2, true, add_user);
        }

        // signalR을 통해 오면 ins_mark 는 false
        public bool addAsset2WorkCellEx(WorkCell d, int asset_id, int source_port_no, List<List<WorkCell>> cell_list3, List<WorkCell> cell_list2, bool ins_mark, bool add_user)
        {
            int port_no = source_port_no;

            if (cell_list3 != null)
            {
                if (port_no < 1)
                {
                    port_no = get_clone_port_no(asset_id, 0, cell_list3);
                    if (port_no < 1)
                        return false;
                }
            }

            // 이미 자산이 놓여있는 경우 실패
            //if ((d.asset_id > 0) || (d.catalog_id > 0))
            //    return false;
            // 짝수에만...
            if ((d.col_no % 2) == 1)
                return false;
            var a = g.asset_list.Find(p => p.asset_id == asset_id);
            if (a == null)
                return false;
            var c = g.catalog_list.Find(p => p.catalog_id == a.catalog_id);
            if (c == null)
                return false;

            int catalog_id = c.catalog_id;
            if (!CatalogType.is_link_diagram(catalog_id))
                return false;

            // 사용된 포트는 안됨 <-- 요거 시스템이 그리는 경우에 한해 허용.....
            // if (is_used_port(asset_id, port_no, cell_list3))
            // if (is_used_port(asset_id, port_no, cell_list2))
            //        return false;

            // 인터커넥트인 경우 자산 표시가 거꾸로 되는 문제....
            init_asset_row2(asset_id, port_no, d.idx, cell_list2, ins_mark);
            // 자산이 배치된 후에 케이블을 연결한다.
            init_asset_row_cable(asset_id, d.idx, cell_list2, ins_mark);

            // 케이블이 배치된 후 IPP 상태를 읽어 표시한다.
            init_asset_row_status(asset_id, 1, port_no, cell_list2, add_user);

            // 기준이 되는 자산과 연결된 좌측 상태를 표시한다.
            update_asset_row_status_right_pp(asset_id, 1, port_no, cell_list2);
            // 기준이 되는 자산과 연결된 우측 상태를 표시한다.
            update_asset_row_status_left_pp(asset_id, 1, port_no, cell_list2);

            d.is_ins_mark = ins_mark;
            d.force_changed = true;

            // bool b = updateCell(asset_id, port_no, d, false, false, "F");

            return true;
        }

        // CTRL-V로 자산 및 케이블을 복사하기 위해 포트번호를 증가하여 부여
        public int get_clone_port_no(int asset_id, int source_port_no, List<List<WorkCell>> cell_list3)
        {
            // 대상포트가 사용중이면 다음 사용되지 않는 포트번호를 부여한다.
            int max = 0;
            try
            {
                max = (g.asset_port_link_list.Where(p => p.asset_id == asset_id)).Max(p => p.port_no);
            }
            catch (Exception) { }

            int i = 0;
            for (i = source_port_no; i < max; i++)
            {
                int port_no = i + 1;
                var apl = g.asset_port_link_list.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no));
                if (apl == null)
                    break;

                // 프론트 또는 리어 연결이 하나만 되어 있거나 연결이 되어 있지 않은 경우에 대해 사용가능 여부 체크
                if ((apl.front_asset_id == null) || (apl.rear_asset_id == null))
                {
                    // 혹여 DB연결은 없더라도 실제 드로잉을 하여 사용중이면 해당 포트는 배재하기 위한 루틴
                    bool find = false;
                    foreach (var list in cell_list3)
                    {
                        if (list != null)
                        {
                            var f = list.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no));
                            if (f != null)
                            {
                                find = true;
                                break;
                            }
                        }
                    }
                    // 사용된 흔적이 발견되지 않으면 이 포트를 사용할 수 있다.
                    if (!find)
                        return port_no;
                }
            }

            // 사용할 수 있는 포트가 없는 경우
            return 0;
        }

        // 배선DB에서 프론트와 리어가 동시에 연결이 되어 있거나 배선관리에서 중앙을 제외한 자산이 입력이 된 경우 사용중이라 판단.
        public bool is_used_port(int asset_id, int port_no, List<WorkCell> cell_list2)
        {
            var apl = g.asset_port_link_list.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no) && (p.front_asset_id > 0) && (p.rear_asset_id > 0));
            if (apl != null)
                return true;

            var f = cell_list2.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no) && (p.col_no != g.CENTER_COL));
            return f != null;
        }

        #endregion

        #region 케이블 추가 (사용자 및 시스템)

        // 사용자 조작으로 케이블 추가 연결
        public bool insertCableCell(List<WorkCell> list, int dest_idx, int cable_catalog_id)
        {
            return insertCableCellEx(list, dest_idx, cable_catalog_id, true);
        }

        // signalR을 통해 들어오면 ins_mark 가 false
        // dest_idx: cable의 인덱스
        public bool insertCableCellEx(List<WorkCell> list, int dest_idx, int cable_catalog_id, bool ins_mark)
        {
            WorkCell d = list[dest_idx];

            WorkCell dest_left = list[dest_idx - 1];
            WorkCell dest_right = list[dest_idx + 1];

            // 케이블 좌우에는 반드시 자산이 있어야 한다.
            if ((dest_left.asset_id == 0) || (dest_right.asset_id == 0))
                return false;

            // 향후 연결이 되면 안되는 조건들을 추가해야 함. 예를 들어 아울렛 등의 방향과 관련하여 프론트가 아닌 곳과 케이블이 연결되지 않도록...
            bool dest_left_is_sw = CatalogType.is_sw(dest_left.catalog_id);
            bool dest_right_is_sw = CatalogType.is_sw(dest_right.catalog_id);
            bool dest_left_is_ipp = CatalogType.is_ipp(dest_left.catalog_id);
            bool dest_right_is_ipp = CatalogType.is_ipp(dest_right.catalog_id);
            bool dest_left_is_xc_ipp = CatalogType.is_xc_ipp(dest_left.catalog_id);
            bool dest_right_is_xc_ipp = CatalogType.is_xc_ipp(dest_right.catalog_id);
            bool dest_left_is_one_side = CatalogType.is_one_side(dest_left.catalog_id);
            bool dest_right_is_one_side = CatalogType.is_one_side(dest_right.catalog_id);

            // 좌측이 스위치이며 Front가 좌측인경우 안됨.
            if (dest_left_is_one_side && dest_left.is_left_front)
                return false;

            // 우측이 스위치이며 Front가 우측인경우 안됨.
            if (dest_right_is_one_side && !dest_right.is_left_front)
                return false;

            // 복사할 케이블이 인텔리전트 타입인경우 좌우에 IPP나 SW만 가능하다.
            if (CatalogType.is_ica(cable_catalog_id))
            {
                // 좌측이 IPP 또는 SW가 아니면 안됨.
                if (!dest_left_is_ipp && !dest_left_is_sw)
                    return false;

                // 우측이 IPP 또는 SW가 아니면 안됨.
                if (!dest_right_is_ipp && !dest_right_is_sw)
                    return false;

                // 좌우측이 IPP인경우 Front 방향이 다르면 안됨.
                if (dest_left_is_ipp && dest_right_is_ipp)
                {
                    if (dest_left.is_left_front)
                        return false;
                    if (!dest_right.is_left_front)
                        return false;
                }
                else
                {
                    // 한쪽만 IPP인경우 크로스 커넥트형식이면 안됨.
                    if (dest_left_is_xc_ipp || dest_right_is_xc_ipp)
                        return false;
                }

            }
            else
            {
                // 좌측 자산이 IPP이면서 우측이 앞면인 경우 일반 케이블 사용 불가능
                if (dest_left_is_ipp && !dest_left.is_left_front)
                    return false;
                // 우측 자산이 IPP이면서 좌측이 앞면인 경우 일반 케이블 사용 불가능
                if (dest_right_is_ipp && dest_right.is_left_front)
                    return false;
            }

            if (CatalogType.is_ica(cable_catalog_id))
                d.is_wo_mark = ins_mark;

            d.port_no = 0;
            d.is_ins_mark = ins_mark;
            d.is_del_mark = false;

            // 기본 정보 복제
            // cloneCell(d, s);
            makeCableCell(d, cable_catalog_id);

            // 좌측자산의 연결정보 대입
            if (!dest_left.is_left_front)
            {
                dest_left.front_asset_id  = dest_right.asset_id;
                dest_left.front_port_no = dest_right.port_no;
                dest_left.front_cable_catalog_id = cable_catalog_id;
                dest_left.front_plug_side = dest_right.is_left_front ? "F" : "R";
            }
            else
            {
                dest_left.rear_asset_id = dest_right.asset_id;
                dest_left.rear_port_no = dest_right.port_no;
                dest_left.rear_cable_catalog_id = cable_catalog_id;
                dest_left.rear_plug_side = dest_right.is_left_front ? "F" : "R";
            }

            // 우측자산의 연결정보 대입
            if (dest_right.is_left_front)
            {
                dest_right.front_asset_id = dest_left.asset_id;
                dest_right.front_port_no = dest_left.port_no;
                dest_right.front_cable_catalog_id = cable_catalog_id;
                dest_right.front_plug_side = dest_left.is_left_front ? "R" : "F";
            }
            else
            {
                dest_right.rear_asset_id = dest_left.asset_id;
                dest_right.rear_port_no = dest_left.port_no;
                dest_right.rear_cable_catalog_id = cable_catalog_id;
                dest_right.rear_plug_side = dest_left.is_left_front ? "R" : "F";
            }

            // 추가 복제할 정보
            makeConnection(list, dest_idx - 1, ins_mark);

            return true;

        }
#endregion
           
        #region 시스템(SignalR)에서 호출되어 작동하는 루틴

        // signalR을 통해 언플러그된 지능형 패치로 인해 셀에서 자산들을 삭제할 때 사용
        private void delete_row_when_unplugged(WorkCell left, WorkCell cable, WorkCell right, ePortStatus port_status, List<WorkCell> cell_list2)
        {
            if ((port_status == ePortStatus.Unplugged) && right.is_ins_mark)
            {
                del_connected_asset(left.asset_id, left.port_no, ePortStatus.Unplugged, cell_list2);
            }
        }

        // signalR에서 포트 상태가 변경된 경우 호출 (당연히 left_tree_handler 통해서..)
        public void update_port_status(int ipp_asset_id, int port_no, ePortStatus status, List<List<WorkCell>> cell_list3)
        {
            foreach (var cell_list2 in cell_list3)
            {
                var cell = cell_list2.Find(p => (p.asset_id == ipp_asset_id) && (p.port_no == port_no));
                if (cell != null)
                {

                    ePortStatus old_status = cell.plug_status;

                    // 아래 일부 스위치케이스까지 미사용
                    switch (status)
                    {
                        case ePortStatus.Plugged :
                            switch (old_status)
                            {
                                case ePortStatus.Unplugged:
                                    // 자신쪽만 연결되어 있는 것 처럼 보임.
                                    break;
                                case ePortStatus.Linked:
                                    // 연결되어 있던 것이 상대방 포트가 끊어져 보이는 것처럼 ...
                                    break;
                            }
                            break;
                        case ePortStatus.Unplugged :
                            switch (old_status)
                            {
                                case ePortStatus.Plugged:
                                    // 케이블이 아예 안보임.
                                    break;
                                case ePortStatus.Linked:
                                    // 연결되어 있던 것이 끊어진 것 처럼 보여야 함.
                                    break;
                            }
                            break;
                        case ePortStatus.Linked :
                            // 양쪽이 연결된 것처럼 보여야 함.
                            break;
                    }


                    cell.plug_status = status;
                    update_asset_row_status(ipp_asset_id, 1, port_no, cell_list3);
                }
            }
        }

        // 수동 스캔(크로스커넥트 및 인터커넥트)하여 연결을 한다. INS 되어 있으면 삭제됨.
        // 인터커넥트 연결일 경우 remote_asset_id 에 sw_asset_id 가 대응됨
        public void add_link_info_asset_to_multi_screen(int ipp_asset_id, int port_no, int remote_asset_id, int remote_port_no, ePortStatus status, List<List<WorkCell>> cell_list3)
        {
            // 그리기 전에 먼저 지우고....  
            del_link_info_asset_to_multi_screen(ipp_asset_id, port_no, status, cell_list3);
            add_connected_asset_to_multi_screen_ex(ipp_asset_id, port_no, remote_asset_id, remote_port_no, status, cell_list3, false, false);
        }

        // 멀티스크린의 뜻은 여러페이지라는 의미
        // 시스템(SingalR)만 호출함.
        public void del_link_info_asset_to_multi_screen(int ipp_asset_id, int port_no, ePortStatus status, List<List<WorkCell>> cell_list3)
        {
            foreach (var cell_list2 in cell_list3)
            {
                var cell2 = cell_list2.Find(p => (p.asset_id == ipp_asset_id) && (p.port_no == port_no) && (p.col_no == g.CENTER_COL));
                if (cell2 != null)
                {
                    // 오른쪽에 IPP를 삭제 처리
                    if ((cell2.col_no + 1) < g.MAX_COL)
                    {
                        int idx = cell2.idx + 2;
                        while (true)
                        {
                            WorkCell d = cell_list2[idx];
                            if (d.template_type != "empty") 
                            {
                                deleteAssetCell(cell_list2, idx);
                            }
                            else
                                break;
                            if ((d.col_no + 1) >= g.MAX_COL)
                                break;
                            idx++;
                            idx++;
                        }
                    }
                }
            }
        }

        // 플러그가 연결되었을 경우 자산 연결 추가...(INS)
        public void add_connected_asset_to_multi_screen(int ipp_asset_id, int port_no, int remote_ipp_asset_id, int remote_port_no, ePortStatus status, List<List<WorkCell>> cell_list3, bool add_user)
        {
            foreach (var cell_list2 in cell_list3)
            {
                add_connected_asset_to_screen_ex(ipp_asset_id, port_no, remote_ipp_asset_id, remote_port_no, status, cell_list2, true, add_user);
                add_connected_asset_to_screen_ex(remote_ipp_asset_id, remote_port_no, ipp_asset_id, port_no, status, cell_list2, true, add_user);
            }
        }

        public void add_connected_asset_to_multi_screen_ex(int ipp_asset_id, int port_no, int remote_ipp_asset_id, int remote_port_no, ePortStatus status, List<List<WorkCell>> cell_list3, bool ins_mark, bool add_user)
        {
            foreach (var cell_list2 in cell_list3)
            {
                add_connected_asset_to_screen_ex(ipp_asset_id, port_no, remote_ipp_asset_id, remote_port_no, status, cell_list2, ins_mark, add_user);
                add_connected_asset_to_screen_ex(remote_ipp_asset_id, remote_port_no, ipp_asset_id, port_no, status, cell_list2, ins_mark, add_user);
            }
        }

        // 인터커넥트 일경우에는 한쪽이 스위치로 변신...
        public void add_connected_asset_to_screen_ex(int ipp_asset_id, int port_no, int remote_asset_id, int remote_port_no, ePortStatus status, List<WorkCell> cell_list2, bool ins_mark, bool add_user)
        {
            var cell2 = cell_list2.Find(p => (p.asset_id == ipp_asset_id) && (p.port_no == port_no));
            if (cell2 != null)
            {
                int cable_catalog_id = Etc.get_standard_ica(ipp_asset_id);
                if (cell2.is_left_front)
                {
                    // 왼쪽에 IPP를 출력
                    if (cell2.col_no > 1)
                    {
                        WorkCell d = cell_list2[cell2.idx - 2];
                        if (d.template_type == "empty")
                        {
                            addAsset2WorkCellEx(d, remote_asset_id, remote_port_no, null, cell_list2, ins_mark, false);
                            insertCableCell(cell_list2, cell2.idx - 1, cable_catalog_id);
                        }
                    }
                }
                else
                {
                    // 오른쪽에 IPP를 출력
                    if ((cell2.col_no + 1) < g.MAX_COL)
                    {
                        WorkCell d = cell_list2[cell2.idx + 2];
                        if (d.template_type == "empty")
                        {
                            addAsset2WorkCellEx(d, remote_asset_id, remote_port_no, null, cell_list2, ins_mark, false);
                            insertCableCellEx(cell_list2, cell2.idx + 1, cable_catalog_id, ins_mark);
                        }
                    }
                }
            }
        }

        // 삭제...
        public void del_connected_asset_to_multi_screen(int ipp_asset_id, int port_no, ePortStatus status, List<List<WorkCell>> cell_list3)
        {
            foreach (var cell_list2 in cell_list3)
            {
                del_connected_asset(ipp_asset_id, port_no, status, cell_list2);
            }
        }


        // 호출된 자산의 우측(+2)셀부터 끝까지 삭제...
        public void del_connected_asset(int ipp_asset_id, int port_no, ePortStatus status, List<WorkCell> cell_list2)
        {
            var cell2 = cell_list2.Find(p => (p.asset_id == ipp_asset_id) && (p.port_no == port_no) && (p.col_no == g.CENTER_COL));
            if (cell2 != null)
            {
                // 오른쪽에 IPP를 삭제 처리
                if ((cell2.col_no + 1) < g.MAX_COL)
                {
                    //WorkCell cable = cell_list2[cell2.idx + 1];
                    //cable.is_ins_mark = false;
                    //cable.is_wo_mark = false;
                    //cable.is_del_mark = false;
                    //clearCellOne(cell_list2, idx);

                    int idx = cell2.idx + 2;
                    while (true)
                    {
                        WorkCell d = cell_list2[idx];
                        if ((d.template_type != "empty") && d.is_ins_mark)
                        {
                            deleteAssetCell(cell_list2, idx);
                        }
                        else
                            break;
                        if ((d.col_no + 1) >= g.MAX_COL)
                            break;
                        idx++;
                        idx++;
                    }
                }
            }
        }

        // signalR에서 호출됨.
        public void update_alarm_status(int ipp_asset_id, int port_no, string status, List<List<WorkCell>> cell_list3)
        {
            foreach(var cell_list2 in cell_list3)
            {
                var cell = cell_list2.Find(p => (p.asset_id == ipp_asset_id) && (p.port_no == port_no));
                if (cell != null)
                {
                    eAlarmStatus old_status = cell.alarm_status;
                    eAlarmStatus new_status = Common.get_alarm_status_type(status);
                    if (old_status != new_status)
                    {
                        cell.alarm_status = new_status;
                        cell.force_changed = true;
                    }
                }
            }
            update_asset_row_status(ipp_asset_id, 1, port_no, cell_list3);
        }

        // 워크오더 중인 포트인지?
        public bool is_wo_port(int ipp_asset_id, int port_no, List<List<WorkCell>> cell_list3)
        {
            foreach(var cell_list2 in cell_list3)
            {
                var find = cell_list2.Find(p => (p.asset_id == ipp_asset_id) && (p.port_no == port_no) && (p.col_no == g.CENTER_COL));
                if (find != null)
                    return cell_list2[find.idx + 1].is_wo_mark;
                else
                    return false;
            }
            return false;
        }


        public void update_wo_status(int ipp_asset_id, int port_no, string status, List<List<WorkCell>> cell_list3)
        {
            foreach(var cell_list2 in cell_list3)
            {
                var cell = cell_list2.Find(p => (p.asset_id == ipp_asset_id) && (p.port_no == port_no));
                if (cell != null)
                {
                    eWorkStatus old_status = cell.wo_status;
                    eWorkStatus new_status = Common.get_wo_status_type(status);
                    if (old_status != new_status)
                    {
                        cell.wo_status = new_status;
                    }

                    WorkCell cable = null;
                    WorkCell left = null;
                    WorkCell right = null;
                    if (cell.col_no == g.CENTER_COL)
                    {
                        left = cell_list2[cell.idx];
                        cable = cell_list2[cell.idx + 1];
                        right = cell_list2[cell.idx + 2];
                    }
                    if (cell.col_no == g.CENTER_COL + 2)
                    {
                        left = cell_list2[cell.idx - 2];
                        cable = cell_list2[cell.idx - 1];
                        right = cell_list2[cell.idx];
                    }

                    ePortStatus status_type = Common.get_status_type(status);
                    if (cable != null)
                    {
                        left.alarm_status = eAlarmStatus.None;
                        cable.alarm_status = eAlarmStatus.None;
                        right.alarm_status = eAlarmStatus.None;
                        left.is_ins_mark = false;
                        cable.is_ins_mark = false;
                        right.is_ins_mark = false;
                        left.is_del_mark = false;
                        cable.is_del_mark = false;
                        right.is_del_mark = false;
                        left.is_wo_mark = false;
                        cable.is_wo_mark = false;
                        right.is_wo_mark = false;
                        left.force_changed = true;
                        cable.force_changed = true;
                        right.force_changed = true;                                
                        if (cable.is_ins_mark)
                        {
                            // 뒤에 주욱...ins 마크를 삭제한다.
                            int idx = cable.idx;
                            int col_no = cable.col_no;
                            while (true)
                            {
                                idx++;
                                col_no++;
                                if ((col_no + 1) >= g.MAX_COL)
                                    break;
                                WorkCell wc = cell_list2[idx];
                                wc.is_ins_mark = false;
                                wc.force_changed = true;
                            }
                        }
                        if (cable.is_del_mark)
                        {
                            // 뒤에 셀들을 모두 지운다.
                            int idx = cable.idx;
                            int col_no = cable.col_no;
                            while (true)
                            {
                                clearCellOne(cell_list2, idx);
                                WorkCell wc = cell_list2[idx];
                                wc.force_changed = true;                                
                                idx++;
                                col_no++;
                                if ((col_no + 1) >= g.MAX_COL)
                                    break;
                            }
                        }
                    }
                    update_asset_row_status(ipp_asset_id, 1, port_no, cell_list3);
                }
            }
        }

        // 포트트레이스...
        public void update_trace_status(int ipp_asset_id, int port_no, string status, List<List<WorkCell>> cell_list3)
        {
            foreach(var cell_list2 in cell_list3)
            {
                var cell = cell_list2.Find(p => (p.asset_id == ipp_asset_id) && (p.port_no == port_no));
                if (cell != null)
                {
                    eTraceStatus old_status = cell.trace_status;
                    eTraceStatus new_status = Common.get_trace_status_type(status);
                    if (old_status != new_status)
                    {
                        cell.trace_status = new_status;
                        update_asset_row_status(ipp_asset_id, 1, port_no, cell_list3);
                    }
                }
            }
        }
        #endregion

        #region // ROMEE 추가 처리 로직   - 시험 필요 
        // 2015.11.03 romee 화면 터미날 처리 
        // 로우셀의 업데이트 디비에서 읽어와서 -> 웤크셀에 저장 화면 갱신 처리 
        // 터미날 처리 로직 
        // 1. 스위치에서 터미날이 다운 되면 처리 없음 -> 사용자 PC의 파워가 내려간 경우 -> 다음날 다시 킨다
        // 2. 스위치에서 업이 되도 처리 없음 -> 사용자 PC 켜짐
        // 3. 스위치 스캔후 해당 PC 가 아닌경우 혹은 없다가 생긴경우 관리 대상 
        // 4. PC 가 옮겨지거나 새로 생긴 경우임 
        // 5. 지우기 처리 -> 새로 생성하기 
        public void update_work_cell(int outlet_asset_id, int outlet_port_no, int terminal_asset_id, int key, List<List<WorkCell>> cell_list3)
        {
            if (key == 0) // 터미날 삭제하기 
            {
                foreach (var cell_list2 in cell_list3)
                {
                    var cell2 = cell_list2.Find(p => (p.asset_id == terminal_asset_id));
                    if (cell2 != null)
                    {
                        if ((cell2.col_no + 1) < g.MAX_COL)
                        {
                            int idx = cell2.idx;
                            WorkCell d = cell_list2[idx];
                            if (d.template_type != "empty")
                            {
                                deleteAssetCell(cell_list2, idx);
                            }
                        }
                    }
                }
            }
            else // 터미날 추가하기  -> 아울렛을 새로 그리게 되면 하부에 딸린 터미날은 자동으로 디비에서 가져와서 새로 그림 
            {
                foreach (var cell_list2 in cell_list3)
                {
                    var cell = cell_list2.Find(p => (p.asset_id == outlet_asset_id) && (p.port_no == outlet_port_no));
                    if (cell != null)
                    {
                        int cable_catalog_id = 490001; // 케이블은 항상 동일 - 일반 패치코드 
                        if (cell.is_left_front)
                        {
                            // 왼쪽에 IPP를 출력
                            if ((cell.col_no - 2) > 1)
                            {
                                WorkCell d = cell_list2[cell.idx]; // 자신거 가지고 들어가면 새로 그려짐 
                                addAsset2WorkCellEx(d, outlet_asset_id, outlet_port_no, null, cell_list2, false, false);
                                insertCableCellEx(cell_list2, cell.idx - 1, cable_catalog_id, false);
                            }
                        }
                        else
                        {
                            // 오른쪽에 IPP를 출력
                            if ((cell.col_no + 2) < g.MAX_COL)
                            {
                                WorkCell d = cell_list2[cell.idx];
                                addAsset2WorkCellEx(d, outlet_asset_id, outlet_port_no, null, cell_list2, false, false);
                                insertCableCellEx(cell_list2, cell.idx + 1, cable_catalog_id, false);
                            }
                        }

                    }
                }
            }
        }
        #endregion


        #region 셀 관련
        // 자산셀 하나 그린다.
        // is_center_cell : 중앙에서만 true
        // is_right_to_left : 우측에서 좌측으로 그려야 할 경우 true (HUB 연결된...)
        // find_plug_side : 다음 자산을 연결된 사이드의 반대쪽을 대입한다. (Front or Rear)
        // is_ins_mark : ins 마크를 표시해야 하는 경우 (사용자가 추가한 경우)
        private bool updateCell(int find_asset_id, int find_port_no, WorkCell cell, bool is_center_cell, bool is_right_to_left, string find_plug_side, bool is_ins_mark)
        {
            int front_asset_id = 0;
            int front_port_no = 0;
            string front_plug_side = "F";
            int rear_asset_id = 0;
            int rear_port_no = 0;
            string rear_plug_side = "F";

            int link_80_image_id = 0;

            asset a;
            catalog c;
            location l;
            asset_port_link apl;
            sp_list_image_Result im;

            try
            {
                a = g.asset_list.First(e => e.asset_id == find_asset_id);
                c = g.catalog_list.First(e => e.catalog_id == a.catalog_id);
                l = g.location_list.First(e => e.location_id == a.location_id);
                apl = g.asset_port_link_list.First(e => e.asset_id == find_asset_id && e.port_no == find_port_no);

                front_asset_id = apl.front_asset_id ?? 0;
                front_port_no = apl.front_port_no ?? 0;
                front_plug_side = apl.front_plug_side;
                rear_asset_id = apl.rear_asset_id ?? 0;
                rear_port_no = apl.rear_port_no ?? 0;
                rear_plug_side = apl.rear_plug_side;
                link_80_image_id = c.link_80_image_id ?? 0;
                //Console.WriteLine("asset_id={0}, port_no={1}, plug_side={2}", find_asset_id, find_port_no, find_plug_side);

                cell.template_type = "asset";

                cell.asset_port_link_id = apl.asset_port_link_id;
                cell.asset_id = a.asset_id;
                cell.port_no = apl.port_no;
                cell.catalog_id = c.catalog_id;
                cell.catalog_group_id = c.catalog_group_id;
                cell.asset_name = a.asset_name;
                cell.catalog_name = c.catalog_name;
                cell.front_asset_id = front_asset_id;
                cell.front_port_no = front_port_no;
                cell.front_plug_side = front_plug_side;
                cell.front_cable_catalog_id = apl.front_cable_catalog_id ?? 0;
                cell.rear_asset_id = rear_asset_id;
                cell.rear_port_no = rear_port_no;
                cell.rear_plug_side = rear_plug_side;
                cell.rear_cable_catalog_id = apl.rear_cable_catalog_id ?? 0;
                cell.location_id = l.location_id;
                cell.location_name = l.location_name;
                cell.is_ins_mark = is_ins_mark;
                if (is_right_to_left)
                {
                    cell.is_left_front = find_plug_side == "R" ? true : false;
                    //cell.is_right_front = find_plug_side == "F" ? true : false;
                }
                else
                {
                    //cell.is_right_front = find_plug_side == "R" ? true : false;
                    cell.is_left_front = find_plug_side == "F" ? true : false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception error. code={0}, message={1}", e.HResult, e.Message);
                return false;
            }

            // 링크 다이어그램용 이미지명을 알아온다.
            cell.link_80_image_name = "Icons/link/etc_80.png";
            cell.link_80_image_id = link_80_image_id;
            if (link_80_image_id > 0)
            {
                im = g.sp_image_list.Find(e => e.image_id == link_80_image_id);
                if (im != null)
                    cell.link_80_image_name = string.Format("/I2MS2;component/Icons/{0}/{1}", im.folder_name, im.file_name);
            }


            cell.force_changed = true;

            return true;
        }


        // 해당 셀을 케이블셀로 초기화
        private bool makeCableCell(WorkCell d, int cable_catalog_id)
        {
            var c = g.catalog_list.Find(p => p.catalog_id == cable_catalog_id);
            if (c == null)
                return false;
            // 복제
            d.catalog_id = cable_catalog_id;
            d.catalog_group_id = c.catalog_group_id;
            d.catalog_name = c.catalog_name;
            d.ca_disp_name = c.ca_disp_name;
            d.link_80_image_id = c.link_80_image_id ?? 0;


            // 링크 다이어그램용 이미지명을 알아온다.
            d.link_80_image_name = "/I2MS2;component/Icons/link/etc_80.png";
            if (d.link_80_image_id > 0)
            {
                var im = g.sp_image_list.Find(e => e.image_id == d.link_80_image_id);
                if (im != null)
                    d.link_80_image_name = string.Format("/I2MS2;component/Icons/{0}/{1}", im.folder_name, im.file_name);
            }

            // 초기값
            d.asset_port_link_id = 0;
            d.front_asset_id = 0;
            d.front_port_no = 0;
            d.front_plug_side = "F";
            d.front_cable_catalog_id = 0;
            d.rear_asset_id = 0;
            d.rear_port_no = 0;
            d.rear_plug_side = "F";
            d.rear_cable_catalog_id = 0;

            d.template_type = "cable";
            d.asset_id = 0;
            d.asset_name = "";
            d.location_id = 0;
            d.location_name = "";
            d.is_left_front = false;
            return true;
        }

        // 자산셀(짝수)이면 true 
        public bool is_asset_cell(WorkCell vm)
        {
            return (vm.col_no % 2) == 0;
        }

        //센터셀이면 true
        public bool is_center_cell(WorkCell vm)
        {
            return (vm.col_no == g.CENTER_COL);
        }

        // 자산 또는 케이블 셀 삭제 (사용자만 호출)
        public void deleteCell(List<WorkCell> list, int idx)
        {
            WorkCell vm = list[idx];
            WorkCell left = list[idx - 1];
            WorkCell right = list[idx + 1];

            // 중앙셀은 삭제 불가능
            if (is_center_cell(vm))
                return;

            // ins 마크가 있다는 것은 빈 공간에 사용자가 추가한 정보(자산 또는 케이블을)임
            if (vm.is_ins_mark)
            {
                if (is_asset_cell(vm))
                {
                    // 자산셀 삭제 (자산셀의 좌우 케이블도 삭제)
                    deleteAssetCell(list, idx);
                    return;
                }
                else
                {
                    // 케이블이면 케이블 셀 삭제
                    deleteCableCell(list, idx);
                    return;
                }
            }
            // 이미 화면에 그려져 있는 자산이나 케이블을 삭제 요구한 경우 del_mark 가 있고, 다시 한 번 삭제요구가 있으면 del_mark 만 지운다.
            else if (vm.is_del_mark)
            {
                // 좌우측에 자산이 있으면서 del 마크가 되어 있는경우 케이블의 del 마크를 없앨 수 없다.
                if ((left.template_type == "asset") && (left.is_del_mark))
                    return;
                if ((right.template_type == "asset") && (right.is_del_mark))
                    return;
                vm.is_del_mark = false;
                vm.is_wo_mark = false;
                if ((left.template_type == "cable") && (left.is_del_mark))
                {
                    left.is_del_mark = false;
                }
                if ((right.template_type == "cable") && (right.is_del_mark))
                {
                    right.is_del_mark = false;
                }
            }
            // ins도 없고 del 마크도 없는 경우 del_mark 를 새로 그린다.
            else
            {
                vm.is_del_mark = true;
                // 인텔리전트 케이블이면서 좌우 자산이 del 마크가 없는 경우 워크 오더 발행
                if ((vm.template_type == "cable") &&
                    (CatalogType.is_ica(vm.catalog_id)) &&
                    !left.is_del_mark && !right.is_del_mark)
                {
                    vm.is_wo_mark = true;
                }
                if ((left.template_type == "cable") && (!left.is_del_mark))
                {
                    left.is_del_mark = true;
                }
                if ((right.template_type == "cable") && (!right.is_del_mark))
                {
                    right.is_del_mark = true;
                }
            }
            vm.force_changed = true;
            left.force_changed = true;
            right.force_changed = true;
        }

        // 자산셀삭제.. 주변 케이블까지..
        public void deleteAssetCell(List<WorkCell> list, int idx)
        {
            int x = list[idx].col_no;
            if (x >= 2)
            {
                list[idx - 2].right_plug_status = ePortStatus.Unplugged;
                list[idx - 2].right_ca_disp_color_rgb = Colors.Transparent;
                clearCellOne(list, idx - 1);
            }
            clearCellOne(list, idx);
            if (x < (g.MAX_COL - 2))
            {
                clearCellOne(list, idx + 1);
                list[idx + 2].left_plug_status = ePortStatus.Unplugged;
                list[idx + 2].left_ca_disp_color_rgb = Colors.Transparent;
            }
            drawConnection(list, idx - 2, 0);
            drawConnection(list, idx, 0);
        }

        // 하나의 셀을 삭제
        public void clearCellOne(List<WorkCell> list, int idx)
        {
            WorkCell d = list[idx];

            d.port_no = 0;
            d.is_ins_mark = false;
            d.is_del_mark = false;
            d.is_wo_mark = false;

            // 복제
            d.template_type = "empty";
            d.asset_id = 0;
            d.catalog_id = 0;
            d.catalog_group_id = 0;
            d.asset_name = "";
            d.catalog_name = "";
            d.location_id = 0;
            d.location_name = "";
            d.link_80_image_id = 0;
            d.link_80_image_name = "";

            // 초기값
            d.asset_port_link_id = 0;
            d.front_asset_id = 0;
            d.front_port_no = 0;
            d.front_plug_side = "F";
            d.front_cable_catalog_id = 0;
            d.rear_asset_id = 0;
            d.rear_port_no = 0;
            d.rear_plug_side = "F";
            d.rear_cable_catalog_id = 0;
            d.ca_disp_color_rgb = Colors.Transparent;
            d.ca_disp_name = "";
            d.alarm_status = eAlarmStatus.None;                 // 아래 2줄  포함 romee 1.30
            d.left_alarm_visible = Visibility.Hidden;
            d.right_alarm_visible = Visibility.Hidden;

            d.left_plug_status = ePortStatus.Unplugged;
            d.right_plug_status = ePortStatus.Unplugged;
            d.left_ca_disp_color_rgb = Colors.Transparent;
            d.right_ca_disp_color_rgb = Colors.Transparent;
        }

        // 케이블셀삭제.. 중앙에 있는 케이블은 삭제하고 좌우 자산의 플러그 상태를 unplugged로 변경
        public void deleteCableCell(List<WorkCell> list, int idx)
        {
            int x = list[idx].col_no;
            if (x >= 1)
            {
                WorkCell left = list[idx - 1];
                left.right_plug_status = ePortStatus.Unplugged;
                left.right_ca_disp_color_rgb = Colors.Transparent;
            }
            clearCellOne(list, idx);
            if (x < (g.MAX_COL - 1))
            {
                WorkCell right = list[idx + 1];
                right.left_plug_status = ePortStatus.Unplugged;
                right.left_ca_disp_color_rgb = Colors.Transparent;
            }
            drawConnection(list, idx - 1, 0);
        }

        // 좌우 회전
        public bool turn_asset(List<WorkCell> cell_list2, int idx)
        {
            WorkCell d = cell_list2[idx];
            d.is_left_front = !d.is_left_front;
            drawConnection(cell_list2, idx, 0);

            return true;
        }

        #endregion
               
        #region 유틸리티
        // 좌우에 패치가 있는 경우 인덱스를 리턴, 만일 없으면 -1을 리턴한다.
        private int get_near_ipp_idx(List<WorkCell> cell_list2, WorkCell cell)
        {
            WorkCell left = null;
            WorkCell right = null;
            if (cell == null)
                return -1;
            int idx = cell.idx;
            if (cell.col_no >= 2)
            {
                left = cell_list2[idx - 2];
                if (CatalogType.is_ipp(left.catalog_id))
                    return left.idx;
            }
            if (cell.col_no < (g.MAX_COL - 2))
            {
                right = cell_list2[idx + 2];
                if (CatalogType.is_ipp(right.catalog_id))
                    return right.idx;
            }
            return -1;
        }

        // 좌우에 패치가 있는 경우 인덱스를 리턴, 만일 없으면 -1을 리턴한다.
        // 2016-12-07 마곡지구 지능형 간선 추가 문제 
        private int get_near_ipp_idx2(List<WorkCell> cell_list2, WorkCell cell)
        {
            WorkCell left = null;
            WorkCell right = null;
            if (cell == null)
                return -1;
            int idx = cell.idx;
            if (cell.col_no == 12)  // 마곡
            {
                left = cell_list2[idx - 2];
                if (CatalogType.is_ipp(left.catalog_id))
                    return left.idx;
            }
            if (cell.col_no == 10)  // 마곡
            {
                right = cell_list2[idx + 2];
                if (CatalogType.is_ipp(right.catalog_id))
                    return right.idx;
            }
            return -1;
        }


        // 해당 자산을 찾아서 그 라인에 있는 첫 번째 패치를 발견해온다.
        private WorkCell find_ipp_cell(List<WorkCell> cell_list2, int asset_id, int port_no)
        {
            // 먼저 해당 자산을 쉬트에서 검색한다.
            var cell = cell_list2.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no));
            if (cell == null)
                return null;
            // 어떠한 줄에 위치하는지 알아온다.
            int row_no = cell.row_no;
            // 발견한 줄에서 패치를 검색한다.
            cell = cell_list2.Find(p => (p.row_no == row_no) && CatalogType.is_ipp(p.catalog_id) && p.col_no > 8);  // 2016.12.07 지능형 광 간선이 있는 경우 마곡지구 
            return cell;
        }

        // 자산이 PP이면서 asset_id는 좌측이든 우측이든 검색을 먼저하고, 그 두 개중 좌측PP인 셀을 리턴
        private WorkCell get_left_ipp_cell(List<WorkCell> cell_list2, int asset_id, int port_no)
        {
            var cell = find_ipp_cell(cell_list2, asset_id, port_no);
            if (cell == null)
                return null;

            // front가 우측이면 PP는 좌측임..
            if (!cell.is_left_front)
                return cell;
            int idx2 = get_near_ipp_idx2(cell_list2, cell);
            if (idx2 != -1)
            {
                var cell2 = cell_list2[idx2];
                if (!cell2.is_left_front)
                    return cell2;
            }
            return null;
        }

        // 자산이 PP이면서 asset_id는 좌측이든 우측이든 검색을 먼저하고, 그 두 개중 우측PP인 셀을 리턴
        private WorkCell get_right_ipp_cell(List<WorkCell> cell_list2, int asset_id, int port_no)
        {
            var cell = find_ipp_cell(cell_list2, asset_id, port_no);
            if (cell == null)
                return null;

            // front가 좌측이면 PP는 우측임..
            if (cell.is_left_front)
                return cell;
            int idx2 = get_near_ipp_idx2(cell_list2, cell);  // 2016-12-07 마곡간선  get_near_ipp_idx 
            if (idx2 != -1)
            {
                var cell2 = cell_list2[idx2];
                if (cell2.is_left_front)
                    return cell2;
            }
            return null;
        }
        #endregion

        #region 유틸리티2
        // 화면에 보여줄 가장 좌측에 출력할 자산이 검색됨
        // ----------------------------------------------------------------------------------------------------
        // 자산 연결 예) SW-101 --> IPP-101 --> IPP-102 --> CP-101 --> FP-101 --> PC
        // 검색한 자산의 Front가 우측 기준으로 검색하므로 자산 검색 위치에 따라 스위치가 검색이 될 수도 있고,
        // 마지막에 있는 PC가 처음 자산으로 검색될 수도 있다.
        // IPP-101로 검색하면 스위치가 리턴되고, IPP-102로 검색하면 스테이션이 리턴됨
        // rel_pos=검색 기준이 되는 자산으로부터 좌우 위치를 
        // find_plug_side : 마지막 검색한 자산의 앞뒤중 하나가 좌측 방향에 표시됨. : F = Front가 좌측, R = Rear가 좌측

        public void get_left_asset(int asset_id, int port_no, ref int find_asset_id, ref int find_port_no, ref string find_plug_side, ref int rel_pos, ref int lg_prj)
        {
            get_last_asset(asset_id, port_no, true, ref find_asset_id, ref find_port_no, ref find_plug_side, ref rel_pos, ref lg_prj);
        }

        // 화면에 보여줄 가장 우측에 출력할 자산이 검색됨(좌측에 HUB가 있을 경우 사용)
        // ----------------------------------------------------------------------------------------------------
        public void get_right_asset(int asset_id, int port_no, ref int find_asset_id, ref int find_port_no, ref string find_plug_side, ref int rel_pos, ref int lg_prj)
        {
            get_last_asset(asset_id, port_no, false, ref find_asset_id, ref find_port_no, ref find_plug_side, ref rel_pos, ref lg_prj);
        }

        // 가장 마지막 자산을 알아온다.
        public void get_last_asset(int asset_id, int port_no, bool is_left, ref int find_asset_id, ref int find_port_no, ref string find_plug_side, ref int rel_pos, ref int lg_prj)
        {
            int find_asset_id2 = asset_id;
            int find_port_no2 = port_no;
            string find_plug_side2;
            if (is_left)
                find_plug_side2 = "F";
            else
                find_plug_side2 = "R";

            find_asset_id = 0;
            find_port_no = 0;
            find_plug_side = "F";

            while (true)
            {
                asset_port_link data = null;

                data = g.asset_port_link_list.Find(e => find_asset_id2 == e.asset_id && find_port_no2 == e.port_no);
                if (data == null)
                    break;

                // 우측이 Front인 경우
                if (find_plug_side2 == "F")
                {
                    if (data.rear_asset_id == null)
                    {
                        find_plug_side2 = "R";
                        break;
                    }
                    find_asset_id2 = data.rear_asset_id ?? 0;
                    find_port_no2 = data.rear_port_no ?? 0;
                    find_plug_side2 = data.rear_plug_side;
                }
                else
                {
                    if (data.front_asset_id == null)
                    {
                        find_plug_side2 = "F";
                        break;
                    }
                    find_asset_id2 = data.front_asset_id ?? 0;
                    find_port_no2 = data.front_port_no ?? 0;
                    find_plug_side2 = data.front_plug_side;
                }
                if (rel_pos == 2 || rel_pos == -2)
                {
                    if (CatalogType.is_ipp_fp(Etc.get_asset(asset_id).catalog_id))
                        lg_prj = 1;
                }

                if (is_left)
                    rel_pos -= 2;
                else
                    rel_pos += 2;
            }

            find_asset_id = find_asset_id2;
            find_port_no = find_port_no2;
            find_plug_side = find_plug_side2;
            //Console.WriteLine("find first. asset_id={0}, port_no={1}, plug_side={2}", find_asset_id, find_port_no, find_plug_side);
        }

        #endregion
               
        #region 상태 스트링을 관리용 상태으로 변경

        public ePortStatus get_port_status(string status_string)
        {
            switch(status_string)
            {
                case "U" :
                    return ePortStatus.Unplugged;
                case "P":
                    return ePortStatus.Plugged;
                case "L":
                    return ePortStatus.Linked;
                default :
                    return ePortStatus.Unknown;
            }
        }

        public eAlarmStatus get_alarm_status(string status_string)
        {
            switch(status_string)
            {
                case "U" :
                    return eAlarmStatus.UnpluggedAlarm;
                case "P":
                    return eAlarmStatus.PluggedAlarm;
                default :
                    return eAlarmStatus.None;
            }
        }

        public eWorkStatus get_wo_status(string status_string)
        {
            switch(status_string)
            {
                case "U" :
                    return eWorkStatus.UnpluggedWork;
                case "P":
                    return eWorkStatus.PluggedWork;
                default :
                    return eWorkStatus.None;
            }
        }

        public eTraceStatus get_trace_status(string status_string)
        {
            switch(status_string)
            {
                case "Y" :
                case "T" :
                case "1" :
                    return eTraceStatus.Enabled;
                default :
                    return eTraceStatus.Disabled;
            }
        }
        #endregion


    }

}
