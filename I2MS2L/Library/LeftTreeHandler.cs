
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WebApi.Models;
using I2MS2.Models;
using System.Collections.ObjectModel;
using System.Windows;
using I2MS2.UserControls;
using I2MS2.Windows;
using System.Windows.Threading;

// LED work order 없이 저장하고 기능 삭제 처리 
//
//

namespace I2MS2.Library
{
    // 시그날 알을 통해 들어온 메시지 처리 -> 트리와 배선뷰등을 업데이트 처리  
    public class LeftTreeHandler
    {
        public LeftSideControl _ctlLeftSide;

        #region signalR로 부터 수신된 메시지 처리

        // fw_upgrade_result: 1=성공, 0=실패
        public async Task<bool> process_fw_upgrade_result(int ic_asset_id, int fw_upgrade_result)
        {
            await reload_ic_connect_status();
            await reload_fw_upgrade_hist();

            // 펌웨어 적용 화면이 동작중이면....
            try
            {
                if (g.fw_apply_window != null)
                {
                    var list = g.fw_apply_window.ic_fwup_list;
                    var node = list.Find(p => p.ic_asset_id == ic_asset_id);
                    if (node != null)
                    {
                        g.fw_apply_window.update_fw_upgrade_window(node, fw_upgrade_result);
                    }
                }

            }
            catch (Exception)
            { }

            return true;
        }
        // 포트 상태 변경 -> 시그날알 수신후 변경 처리 
        public bool change_port_status(int sys_id, int pp_id, int port_no, int remote_sys_id, int remote_pp_id, int remote_port_no, ePortStatus status)
        {
            int ipp_asset_id = Etc.get_ipp_asset_id(sys_id, pp_id);
            if (ipp_asset_id == 0)
            {
                Console.WriteLine("SignalR Received --> change_port_status -> " + ipp_asset_id);
                return false;
            }
            int remote_ic_asset_id = Etc.get_ic_asset_id(remote_sys_id);
            int remote_ipp_asset_id = Etc.get_ipp_asset_id(remote_sys_id, remote_pp_id);

            var aippl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == ipp_asset_id) && (p.port_no == port_no));
            if (aippl == null)
            {
                Console.WriteLine("SignalR Received --> change_port_status -> aippl" + ipp_asset_id);
                return false;
            }

            ePortStatus old_status = Etc.get_status_type(aippl.ipp_port_status);

            if (status != old_status)
            {
                // 포트 상태 갱신 : 공통 모듈
                update_port_status(ipp_asset_id, port_no, remote_ipp_asset_id, remote_port_no, status, aippl);
            }
            return true;
        }

        // 서버가 signalr을 통해 보낸다.
        public bool change_link_info(int sys_id, int pp_id, int port_no, int remote_sys_id, int remote_pp_id, int remote_port_no, ePortStatus status)
        {
            int ipp_asset_id = Etc.get_ipp_asset_id(sys_id, pp_id);
            if (ipp_asset_id == 0)
                return false;
            int remote_ic_asset_id = Etc.get_ic_asset_id(remote_sys_id);
            int remote_ipp_asset_id = Etc.get_ipp_asset_id(remote_sys_id, remote_pp_id);

            var apl = g.asset_port_link_list.Find(p => (p.asset_id == ipp_asset_id) && (p.port_no == port_no));
            if (apl == null)
                return false;

            update_link_info(ipp_asset_id, port_no, remote_ipp_asset_id, remote_port_no, status, apl);

            var apl2 = g.asset_port_link_list.Find(p => (p.asset_id == remote_ipp_asset_id) && (p.port_no == remote_port_no));
            if (apl2 == null)
                return false;

            update_link_info(remote_ipp_asset_id, remote_port_no, ipp_asset_id, port_no, status, apl2);

            return true;
        }

        // 서버가 signalr을 통해 보낸다.
        public bool change_link_info_ic(int sys_id, int pp_id, int port_no, int sw_asset_id, int sw_port_no, ePortStatus status)
        {
            int ipp_asset_id = Etc.get_ipp_asset_id(sys_id, pp_id);
            if (ipp_asset_id == 0)
                return false;

            update_link_info_ic(ipp_asset_id, port_no, sw_asset_id, sw_port_no, status);
            return true;
        }

        // 링크 인포 변경 처리 
        public bool change_simple_link_info(int asset_id, int port_no, string plug_side, int asset_id2, int port_no2, string plug_side2, int cable_catalog_id, bool connect_flag)
        {
            bool node2_edit = true;
            var node = g.asset_port_link_list.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no));
            if (node == null) 
                return false;
            var node2 = g.asset_port_link_list.Find(p => (p.asset_id == asset_id2) && (p.port_no == port_no2));
            if (node2 == null)
                node2_edit = false;
            if (asset_id2 == g.VIRTUAL_HUB_CATALOG_ID)
                node2_edit = false;

            if (connect_flag)
            {
                if (plug_side == "F")
                {
                    node.front_asset_id = asset_id2;
                    node.front_port_no = port_no2;
                    node.front_plug_side = plug_side2;
                    node.front_cable_catalog_id = cable_catalog_id;                    
                }
                else
                {
                    node.rear_asset_id = asset_id2;
                    node.rear_port_no = port_no2;
                    node.rear_plug_side = plug_side2;
                    node.rear_cable_catalog_id = cable_catalog_id;
                }
                if (asset_id2 != g.VIRTUAL_HUB_CATALOG_ID)
                {
                    if (plug_side2 == "F")
                    {
                        node2.front_asset_id = asset_id;
                        node2.front_port_no = port_no;
                        node2.front_plug_side = plug_side;
                        node2.front_cable_catalog_id = cable_catalog_id;
                    }
                    else
                    {
                        node2.rear_asset_id = asset_id;
                        node2.rear_port_no = port_no;
                        node2.rear_plug_side = plug_side;
                        node2.rear_cable_catalog_id = cable_catalog_id;
                    }
                }
            }
            else
            {
                if (plug_side == "F")
                {
                    node.front_asset_id = null;
                    node.front_port_no = null;
                    node.front_plug_side = null;
                    node.front_cable_catalog_id = null;
                }
                else
                {
                    node.rear_asset_id = null;
                    node.rear_port_no = null;
                    node.rear_plug_side = null;
                    node.rear_cable_catalog_id = null;
                }
                if (node2_edit)
                {
                    if (plug_side2 == "F")
                    {
                        node2.front_asset_id = null;
                        node2.front_port_no = null;
                        node2.front_plug_side = null;
                        node2.front_cable_catalog_id = null;
                    }
                    else
                    {
                        node2.rear_asset_id = null;
                        node2.rear_port_no = null;
                        node2.rear_plug_side = null;
                        node2.rear_cable_catalog_id = null;
                      
                    }
                }
            }

            // 화면 갱신... 아래는 샘플
            // 여기서.... 트리쪽 갱신하라고 호출할 수 있음
            _ctlLeftSide.updateIcswStatus();
            return true;

        }

        // 컨트롤러 상태 변경 
        public bool change_ic_status(int sys_id, bool up_down)
        {
            int ic_asset_id = Etc.get_ic_asset_id(sys_id);
            if (ic_asset_id == 0)
                return false;

            var ics = g.ic_connect_status_list.Find(p => p.ic_asset_id == ic_asset_id);
            if (ics == null)
                return false;

            // 메모리 DB 내용을 갱신
            ics.ic_connect_status1 = up_down ? "Y" : "-";

            // 좌측 트리 갱신 (자산트리, 지능형트리, 즐겨찾기)
            _ctlLeftSide.updateIcAlarmStatus(ic_asset_id, up_down);
            return true;
        }
        // 패치패널 상태 변경 
        public bool change_ipp_status(int sys_id, int pp_id, bool up_down)
        {
            int ipp_asset_id = Etc.get_ipp_asset_id(sys_id, pp_id);
            if (ipp_asset_id == 0)
                return false;

            var ics = g.ipp_connect_status_list.Find(p => p.ipp_asset_id == ipp_asset_id);
            if (ics == null)
                return false;

            // 메모리 DB 내용을 갱신
            ics.connect_status = up_down ? "Y" : "-";

            // 좌측 트리 갱신 (자산트리, 지능형트리, 즐겨찾기)
            //_ctlLeftSide.updateIcAlarmStatus(ipp_asset_id, up_down);
            _ctlLeftSide.updateIppAlarmStatus(ipp_asset_id, up_down);
            return true;
        }

        public bool change_sw_status(int sw_asset_id, int sw_port_no, ePortStatus status)
        {
            // 스위치 포트 상태 갱신
            update_sw_port_status(sw_asset_id, sw_port_no, status);
            return true;
        }
        // ??? romee
        public bool clear_flag_with_port_status(int sys_id, int pp_id, int port_no, ePortStatus status)
        {
            string alarm_status = "-";
            string wo_status = "-";
            string trace_status = "-";
            int ipp_asset_id = Etc.get_ipp_asset_id(sys_id, pp_id);
            if (ipp_asset_id == 0)
                return false;

            var aippl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == ipp_asset_id) && (p.port_no == port_no));
            if (aippl == null)
                return false;

            ePortStatus old_status = Etc.get_status_type(aippl.ipp_port_status);
            string old_alarm_status = aippl.alarm_status;
            string old_wo_status = aippl.wo_status;
            string old_trace_status = aippl.is_port_trace;
            int remote_ipp_asset_id = aippl.remote_pp_asset_id ?? 0;
            int remote_port_no = aippl.remote_port_no ?? 0;

            if (status != old_status)
                // 포트 상태 갱신
                update_port_status(ipp_asset_id, port_no, remote_ipp_asset_id, remote_port_no, status, aippl);
            if (Common.get_alarm_status_type(alarm_status) != Common.get_alarm_status_type(old_alarm_status))
                // 알람/워크오더/포트트레이스 상태 갱신
                update_alarm_status(ipp_asset_id, port_no, alarm_status, aippl);
            if (Common.get_wo_status_type(wo_status) != Common.get_wo_status_type(old_wo_status))
                // 포트 상태 갱신
                update_wo_status(ipp_asset_id, port_no, wo_status, aippl);
            if (Common.get_trace_status_type(trace_status) != Common.get_trace_status_type(old_trace_status))
                // 포트 상태 갱신
                update_trace_status(ipp_asset_id, port_no, trace_status, aippl);
            return true;
        }

        public bool change_alarm_status(int sys_id, int pp_id, int port_no, string alarm_status)
        {
            int ipp_asset_id = Etc.get_ipp_asset_id(sys_id, pp_id);
            if (ipp_asset_id == 0)
                return false;

            var aippl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == ipp_asset_id) && (p.port_no == port_no));
            if (aippl == null)
                return false;

            string old_alarm_status = aippl.alarm_status;

            if (alarm_status != old_alarm_status) 
                // 알람 상태 갱신
                update_alarm_status(ipp_asset_id, port_no, alarm_status, aippl);
            return true;
        }

        public bool change_wo_status(int sys_id, int pp_id, int port_no, string wo_status)
        {
            int ipp_asset_id = Etc.get_ipp_asset_id(sys_id, pp_id);
            if (ipp_asset_id == 0)
                return false;

            var aippl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == ipp_asset_id) && (p.port_no == port_no));
            if (aippl == null)
                return false;

            string old_wo_status = aippl.wo_status;

//            if (wo_status != old_wo_status)
                // 워크오더 상태 갱신
            update_wo_status(ipp_asset_id, port_no, wo_status, aippl);
            return true;
        }

        public bool change_trace_status(int sys_id, int pp_id, int port_no, string trace_status)
        {
            int ipp_asset_id = Etc.get_ipp_asset_id(sys_id, pp_id);
            if (ipp_asset_id == 0)
                return false;

            var aippl = g.asset_ipp_port_link_list.Find(p => (p.ipp_asset_id == ipp_asset_id) && (p.port_no == port_no));
            if (aippl == null)
                return false;

            string old_trace_status = aippl.is_port_trace;

            if (trace_status != old_trace_status)
                // 포트트레이스 상태 갱신
                update_trace_status(ipp_asset_id, port_no, trace_status, aippl);
            return true;
        }

        public void process_general_event(eEventCode code)
        {
            switch(code)
            {
                case eEventCode.eStartWorkOrder :
                    g.work_order_progressing = true;
                    g._P5LineManager.update_button_status();
                    break;
                case eEventCode.eStopWorkOrder:
                    g.work_order_progressing = false;
                    g._P5LineManager.update_button_status();
                    break;
                case eEventCode.eCancelWorkOrder:
                    g.work_order_progressing = false;
                    g._P5LineManager.update_button_status();
                    break;
                case eEventCode.eDoorOpen:
                case eEventCode.eDoorClose:
                case eEventCode.eHighCurrentError:
                case eEventCode.eHighPowerError:
                case eEventCode.eHighPowerHourError:
                case eEventCode.eHighTempError:
                case eEventCode.eHighHumiError:
                case eEventCode.eHighVoltageError:
                    // 대시보드 갱신
                    g._P2DashBoard.update_dashboard(code);
                    break;
                case eEventCode.eUnauthorizedPlug:
                case eEventCode.eUnauthorizedUnplug:
                case eEventCode.eRestorePlug:
                case eEventCode.eRestoreUnplug:
                    // 대시보드 갱신
                    g._P2DashBoard.update_dashboard(code);        // romee 2/23 댓쉬보드, 자산뷰 , 포트사용률 업데이트용 
                    g._P4AssetView.update_chart(code);
                    break;
            }
        }

        // 터미날 상태 변경 
        public void process_change_terminal_status(int outlet_asset_id, int outlet_port_no, int terminal_asset_id, bool on_off)
        {
            // 메모리 DB 내용을 갱신
            update_terminal_status(terminal_asset_id, on_off);

            // 좌측 트리 갱신 (자산트리)
            _ctlLeftSide.changePCStatus(outlet_asset_id, outlet_port_no, terminal_asset_id, on_off);

            // 자산 관리에서 링크 다이어그램 갱신

            // 배선 관리에서 링크 다이어그램 갱신
        }
                    
        public async Task<bool> process_add_remove_terminal(int old_outlet_asset_id, int old_outlet_port_no, 
            int outlet_asset_id, int outlet_port_no, int terminal_asset_id, eTerminalAction terminal_action)
        {
            // 메모리 DB 내용을 갱신
            var r = await update_terminal_action(outlet_asset_id, outlet_port_no, terminal_asset_id, terminal_action);

            // 좌측 트리 갱신 (자산트리, 미확인단말)
            switch(terminal_action)
            {
                case eTerminalAction.eAddTerminal:       
                    // 자산트리에 추가
                    _ctlLeftSide.addPC(outlet_asset_id, outlet_port_no, terminal_asset_id, true);
                    break;
                case eTerminalAction.eRemoveTerminal:
                    // 자산트리 및 Unlocated에서 삭제
                    _ctlLeftSide.removePC(outlet_asset_id, outlet_port_no, terminal_asset_id);
                    break;
                case eTerminalAction.eMoveToUnlocated:
                    // 자산트리에서 삭제 후 Unlocated로 생성
                    _ctlLeftSide.movePCToUnlocated(outlet_asset_id, outlet_port_no, terminal_asset_id);
                    break;
                case eTerminalAction.eMoveToAssetTree:
                    // 자산트리내에서 이동
                    _ctlLeftSide.movePCToAssetTree(old_outlet_asset_id, old_outlet_port_no, outlet_asset_id, outlet_port_no, terminal_asset_id);
                    break;
            }


            // 자산 관리에서 링크 다이어그램 갱신
            

            // I2MS_V21 romee 2016.02.15 
            // 배선 관리에서 링크 다이어그램 갱신   romee 2015.11.03 접수  - 터미널 갱신 로직   
            // 일부 오류 발생 시험 필요 romee 2015.11.30 터미날 갱신시 역으로 생성되는 문제 
            g._P5LineManager.update_terminal_linemanager(old_outlet_asset_id, old_outlet_port_no, outlet_asset_id, outlet_port_no, terminal_asset_id);

            return true;
        }


        // 한 사용자가 자산을 변경시켰을 때 타 접속 사용자도 갱신을 해야 한다.
        public async Task<bool> process_add_remove_asset(int asset_id, eAction action)
        {
            switch(action)
            {
                case eAction.eAdd :
                    return await add_asset_from_db(asset_id);
                case eAction.eModify:
                    return await modify_asset_from_db(asset_id);
                case eAction.eRemove:
                    await deleteAsset(asset_id);
                    return true;
            }
            return false;
        }

        // 한 사용자가 위치를 변경시켰을 때 타 접속 사용자도 갱신을 해야 한다.
        public async Task<bool> process_add_remove_location(int location_id, eAction action)
        {
            switch (action)
            {
                case eAction.eAdd:                    
                    return await add_location_from_db(location_id);
                case eAction.eModify:
                    return await modify_location_from_db(location_id);
                case eAction.eRemove:
                    await deleteLocation(location_id);
                    return true;
            }
            return false;
        }

        public async Task<bool> add_location_from_db(int location_id)
        {
            await reload_location(location_id);
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return false;
            switch(l.location_level)
            {
                case 1:
                    await reload_region1(l.region1_id ?? 0);
                    break;
                case 2:
                    await reload_region2(l.region2_id ?? 0);
                    break;
                case 3:
                    await reload_site(l.site_id ?? 0);
                    await reload_site_environment();
                    break;
                case 4:
                    await reload_building(l.building_id ?? 0);
                    break;
                case 5:
                    await reload_floor(l.floor_id ?? 0);
                    break;
                case 6:
                    await reload_room(l.room_id ?? 0);
                    break;
                case 7: 
                    await reload_rack(l.rack_id ?? 0);
                    break;
            }
            await addLocation(location_id);
            return false;
        }

        public async Task<bool> modify_location_from_db(int location_id)
        {
            await reload_location(location_id);
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return false;
            string location_name = l.location_name;
            switch (l.location_level)
            {
                case 1:
                    await reload_region1(l.region1_id ?? 0);
                    break;
                case 2:
                    await reload_region2(l.region2_id ?? 0);
                    break;
                case 3:
                    await reload_site(l.site_id ?? 0);
                    break;
                case 4:
                    await reload_building(l.building_id ?? 0);
                    break;
                case 5:
                    await reload_floor(l.floor_id ?? 0);
                    break;
                case 6:
                    await reload_room(l.room_id ?? 0);
                    break;
                case 7:
                    await reload_rack(l.rack_id ?? 0);
                    break;
            }
            await reload_asset_tree();

            _ctlLeftSide.editLocationToTreeView(location_id);  
            return true;
        }

        private async Task<bool> add_asset_from_db(int asset_id)
        {
            // Step 1. asset
            // Step 2. asset_aux
            // Step 3. asset_ext
            // Step 4. rack_config
            // Step 5. sw_card_config

            await reload_asset(asset_id);
            await reload_asset_aux(asset_id);
            await reload_asset_ext(asset_id);
            await reload_rack_config(asset_id);
            await reload_sw_card_config(asset_id);

            // 나머지 테이블은 자산관리에서 호출하는 아래의 루틴 사용.

            return await addAsset(asset_id);
        }

        private async Task<bool> modify_asset_from_db(int asset_id)
        {
            await reload_asset(asset_id);
            await reload_asset_aux(asset_id);
            await reload_asset_ext(asset_id);
            await reload_asset_tree(asset_id);
            await reload_favorite_tree(asset_id);

            _ctlLeftSide.editAssetToTreeView(asset_id);
            return true;
        }

        private async Task<bool> update_terminal_action(int outlet_asset_id, int outlet_port_no, int terminal_asset_id, eTerminalAction terminal_action)
        {
            int outlet_catalog_id = Etc.get_catalog_id_by_asset_id(outlet_asset_id);
            int outlet_num_of_ports = Etc.get_num_of_ports_by_catalog_id(outlet_catalog_id);
            switch (terminal_action)
            {
                case eTerminalAction.eAddTerminal:
                    {
                        await reload_asset_port_link(outlet_asset_id, outlet_port_no);
                        await reload_asset_port_link(terminal_asset_id, 1);
                        await add_asset_tree_by_asset_id(terminal_asset_id);
                        await reload_asset(terminal_asset_id);
                        await reload_asset_aux(terminal_asset_id);
                        await reload_asset_terminal(terminal_asset_id);
                        var at = g.asset_terminal_list.Find(p => p.terminal_asset_id == terminal_asset_id);
                        int terminal_id = at != null ? at.terminal_id : 0;
                        await reload_asset_terminal_ip(terminal_id);
                    }
                    break;
                case eTerminalAction.eMoveToUnlocated:
                    {
                        await reload_asset_port_link(outlet_asset_id, outlet_port_no);
                        await reload_asset_port_link(terminal_asset_id, 1);
                        var a = g.asset_list.Find(p => p.asset_id == terminal_asset_id);
                        if (a != null)
                            a.location_id = 0;
                        await reload_asset_terminal(terminal_asset_id);
                        var at = g.asset_terminal_list.Find(p => p.terminal_asset_id == terminal_asset_id);
                        int terminal_id = at != null ? at.terminal_id : 0;
                        await reload_asset_terminal_ip(terminal_id);
                    }
                    break;
                case eTerminalAction.eRemoveTerminal:
                    {
                        g.asset_list.RemoveAll(p => p.asset_id == terminal_asset_id);
                        g.asset_aux_list.RemoveAll(p => p.asset_id == terminal_asset_id);
                        g.asset_tree_list.RemoveAll(p => p.asset_id == terminal_asset_id);
                        var at2 = g.asset_terminal_list.Find(p => p.terminal_asset_id == terminal_asset_id);
                        if (at2 != null)
                        {
                            g.asset_terminal_ip_list.RemoveAll(p => p.terminal_id == at2.terminal_id);
                            g.asset_terminal_list.Remove(at2);
                        }
                        g.asset_ext_list.RemoveAll(p => p.asset_id == terminal_asset_id);
                        delete_asset_port_link(terminal_asset_id);
                    }
                    break;
            }
            return true;
        }



        private void update_terminal_status(int terminal_asset_id, bool on_off)
        {
            var at = g.asset_terminal_list.Find(p => p.terminal_asset_id == terminal_asset_id);
            if (at != null)
                at.terminal_status = on_off ? "Y" : "N";
        }

        public async Task<bool> reload_asset_port_link(int asset_id, int port_no)
        {
            var apl = g.asset_port_link_list.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no));

            string filter = string.Format("?asset_id={0}", asset_id);
            var list = (List<asset_port_link>)await g.webapi.getList("asset_port_link", typeof(List<asset_port_link>), filter);
            if (list.Count != 0)
            {
                if (apl != null)
                {
                    // 대체
                    var apl2 = list.Find(p => p.port_no == port_no);
                    if (apl2 != null)
                        apl = apl2;
                }
                else
                {
                    // 추가
                    var apl2 = list.Find(p => p.port_no == port_no);
                    if (apl2 != null)
                        g.asset_port_link_list.Add(apl2);
                }
            }
            else
            {
                // 삭제
                g.asset_port_link_list.RemoveAll(p => (p.asset_id == asset_id) && (p.port_no == port_no));
            }
            return true;
        }


        public async Task<bool> reload_asset_port_link()
        {
            var list = (List<asset_port_link>)await g.webapi.getList("asset_port_link", typeof(List<asset_port_link>));
            if (list.Count != 0)
            {
                g.asset_port_link_list = list;
                return true;
            }
            return false;
        }

        private bool update_alarm_status(int ipp_asset_id, int port_no, string alarm_status, asset_ipp_port_link aippl)
        {
            // 메모리 DB 내용을 갱신
            aippl.alarm_status = alarm_status;

            // 좌측 트리 갱신 (자산트리, 지능형트리, 즐겨찾기)
            _ctlLeftSide.updatePortAlarmAllTree(ipp_asset_id, port_no, alarm_status);

            // 자산 관리에서 링크 다이어그램 갱신
            eAlarmStatus status = Common.get_alarm_status_type(alarm_status);
            g._P4AssetView.update_alarm_status(ipp_asset_id, port_no, status);

            // 배선 관리에서 링크 다이어그램 갱신
            g._P5LineManager.update_alarm_status(ipp_asset_id, port_no, alarm_status);
            return true;
        }

        private bool update_wo_status(int ipp_asset_id, int port_no, string wo_status, asset_ipp_port_link aippl)
        {
            // 메모리 DB 내용을 갱신
            aippl.wo_status = wo_status;

            // 배선 관리에서 링크 다이어그램 갱신
            g._P5LineManager.update_wo_status(ipp_asset_id, port_no, wo_status);
            return true;
        }

        private bool update_trace_status(int ipp_asset_id, int port_no, string trace_status, asset_ipp_port_link aippl)
        {
            // 메모리 DB 내용을 갱신
            aippl.is_port_trace = trace_status;

            // 배선 관리에서 링크 다이어그램 갱신
            g._P5LineManager.update_trace_status(ipp_asset_id, port_no, trace_status);
            return true;
        }

        // 포트 상태 변경 처리 
        private bool update_port_status(int ipp_asset_id, int port_no, 
            int remote_ipp_asset_id, int remote_port_no, ePortStatus status, asset_ipp_port_link aippl)
        {
            // 메모리 DB 내용을 갱신
            aippl.ipp_port_status = Etc.get_status_char(status);
            aippl.remote_ic_asset_id = Etc.get_ic_asset_id_by_ipp_asset_id(remote_ipp_asset_id); // romee/moon 리모트 아이디를 저장하는 부분이 없어서 만듬  
            aippl.remote_pp_asset_id = remote_ipp_asset_id;
            aippl.remote_port_no = remote_port_no;

            // 좌측 트리 갱신 (자산트리, 지능형트리, 즐겨찾기)
            _ctlLeftSide.updatePortStatusAllTree(ipp_asset_id, port_no, aippl.ipp_port_status);

            // 자산 관리에서 링크 다이어그램 갱신
            g._P4AssetView.update_port_status(ipp_asset_id, port_no, status);

            // 배선 관리에서 링크 다이어그램 갱신
            g._P5LineManager.update_port_status(ipp_asset_id, port_no, remote_ipp_asset_id, remote_port_no, status);

            return true;
        }

        private bool update_link_info(int ipp_asset_id, int port_no,
            int remote_ipp_asset_id, int remote_port_no, ePortStatus status, asset_port_link apl)
        {
            // 메모리 DB 내용을 갱신
            if ((status == ePortStatus.Linked) || ((status == ePortStatus.Plugged) && (remote_ipp_asset_id > 0)))
            {
                apl.front_asset_id = remote_ipp_asset_id;
                apl.front_port_no = remote_port_no;
                apl.front_plug_side = "F";
                apl.front_cable_catalog_id = Etc.get_standard_ica(ipp_asset_id);
            }
            else
            {
                apl.front_asset_id = null;
                apl.front_port_no = null;
                apl.front_plug_side = null;
                apl.front_cable_catalog_id = null;
            }

            // 좌측 트리 갱신 (자산트리, 지능형트리, 즐겨찾기)

            // 자산 관리에서 링크 다이어그램 갱신
            g._P4AssetView.update_link_info(ipp_asset_id, port_no);

            // 배선 관리에서 링크 다이어그램 갱신
            g._P5LineManager.update_link_info(ipp_asset_id, port_no, remote_ipp_asset_id, remote_port_no, status);

            return true;
        }

        // import 후에 화면 갱신...
        public bool update_link_info_screen(int asset_id, int port_no,
            int asset_id2, int port_no2, ePortStatus status)
        {
            // 좌측 트리 갱신 (자산트리, 지능형트리, 즐겨찾기)

            // 랙뷰 갱신

            // 자산 관리에서 링크 다이어그램 갱신
            g._P4AssetView.update_link_info(asset_id, port_no);

            // 배선 관리에서 링크 다이어그램 갱신
            g._P5LineManager.update_link_info(asset_id, port_no, asset_id2, port_no2, status);

            return true;
        }


        private bool update_link_info_ic(int ipp_asset_id, int port_no,
            int sw_asset_id, int sw_port_no, ePortStatus status)
        {
            var apl = g.asset_port_link_list.Find(p => (p.asset_id == ipp_asset_id) && (p.port_no == port_no));
            if (apl == null)
                return false;

            var apl2 = g.asset_port_link_list.Find(p => (p.asset_id == sw_asset_id) && (p.port_no == sw_port_no));
            if (apl2 == null)
                return false;

            // 메모리 DB 내용을 갱신
            if (status == ePortStatus.Linked) 
            {
                apl.front_asset_id = sw_asset_id;
                apl.front_port_no = sw_port_no;
                apl.front_plug_side = "F";
                apl.front_cable_catalog_id = Etc.get_standard_ica(ipp_asset_id);

                apl2.front_asset_id = ipp_asset_id;
                apl2.front_port_no = port_no;
                apl2.front_plug_side = "F";
                apl2.front_cable_catalog_id = Etc.get_standard_ica(ipp_asset_id);

            }
            else
            {
                apl.front_asset_id = null;
                apl.front_port_no = null;
                apl.front_plug_side = null;
                apl.front_cable_catalog_id = null;

                apl2.front_asset_id = null;
                apl2.front_port_no = null;
                apl2.front_plug_side = null;
                apl2.front_cable_catalog_id = null;
            }

            // 좌측 트리 갱신 (자산트리, 지능형트리, 즐겨찾기)   스위치 상태 갱신 romee 1/15
            _ctlLeftSide.updateIcswStatus();

            // 자산 관리에서 링크 다이어그램 갱신
            g._P4AssetView.update_link_info(ipp_asset_id, port_no);

            // 배선 관리에서 링크 다이어그램 갱신
            g._P5LineManager.update_link_info_ic(ipp_asset_id, port_no, sw_asset_id, sw_port_no, status);

            return true;
        }

        private bool update_sw_port_status(int sw_asset_id, int sw_port_no, ePortStatus status)
        {
            // 메모리 DB 내용을 갱신
            // aippl.ipp_port_status = Etc.get_status_char(status);   ??? 스위치의 경우 어떻게?

            // 좌측 트리 갱신 (자산트리, 지능형트리, 즐겨찾기)   스위치 상태 갱신 romee 1/15
            _ctlLeftSide.updateIcswStatus();

            // 자산 관리에서 링크 다이어그램 갱신
            g._P4AssetView.update_port_status(sw_asset_id, sw_port_no, status);

            // 배선 관리에서 링크 다이어그램 갱신
            g._P5LineManager.update_sw_port_status(sw_asset_id, sw_port_no, status);

            return true;
        }


        #endregion

        #region 수정 루틴
        public async Task<bool> editRegion1(int region1_id, string name)
        {
            var l = g.location_list.Find(p => (p.region1_id == region1_id) && (p.location_level == 1));
            if (l == null)
                return false;

            return await editLocation(l.location_id, name);
        }

        public async Task<bool> editRegion2(int region2_id, string name)
        {
            var l = g.location_list.Find(p => (p.region2_id == region2_id) && (p.location_level == 2));
            if (l == null)
                return false;

            return await editLocation(l.location_id, name);
        }

        public async Task<bool> editSite(int site_id, string name)
        {
            var l = g.location_list.Find(p => (p.site_id == site_id) && (p.location_level == 3));
            if (l == null)
                return false;

            return await editLocation(l.location_id, name);
        }

        public async Task<bool> editBuilding(int building_id, string name)
        {
            var l = g.location_list.Find(p => (p.building_id == building_id) && (p.location_level == 4));
            if (l == null)
                return false;

            return await editLocation(l.location_id, name);
        }

        public async Task<bool> editFloor(int floor_id, string name)
        {
            var l = g.location_list.Find(p => (p.floor_id == floor_id) && (p.location_level == 5));
            if (l == null)
                return false;

            return await editLocation(l.location_id, name);
        }

        public async Task<bool> editRoom(int room_id, string name)
        {
            var l = g.location_list.Find(p => (p.room_id == room_id) && (p.location_level == 6));
            if (l == null)
                return false;

            return await editLocation(l.location_id, name);
        }

        public async Task<bool> editRack(int rack_id, string name)
        {
            var l = g.location_list.Find(p => (p.rack_id == rack_id) && (p.location_level == 7));
            if (l == null)
                return false;

            return await editLocation(l.location_id, name);
        }

        private async Task<bool> editLocation(int location_id, string name)
        {
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return false;

            if (l.location_path == null)
                return false;
            int len1 = l.location_name.Length;
            int len2 = l.location_path.Length;
            string left = l.location_path.Substring(0, len2 - len1);
            l.location_name = name;
            l.location_path = left + name;

            int r2 = await g.webapi.put("location", location_id, l, typeof(location));
            if (r2 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            var at = g.asset_tree_list.Find(p => p.location_id == location_id);
            if (at == null)
                return false;
            at.disp_name = name;
            int r3 = await g.webapi.put("asset_tree", at.asset_tree_id, at, typeof(asset_tree));
            if (r3 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            var ft = g.favorite_tree_list.Find(p => p.location_id == location_id);
            if (ft != null)
            {
                ft.disp_name = name;
                int r4 = await g.webapi.put("favorite_tree", ft.favorite_tree_id, ft, typeof(favorite_tree));
                if (r4 != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
            }

            _ctlLeftSide.editLocationToTreeView(location_id);  

            return true;
        }

        public async Task<bool> editAsset(int asset_id)
        {
            // 이미 기타 DB쪽은 다 고쳐진걸로 이해하고...

            // Step 1. 자산 트리 메모리 수정 및 DB 수정

            var a = g.asset_list.Find(p => p.asset_id == asset_id);
            if (a == null)
                return false;
            var at = g.asset_tree_list.Find(p => p.asset_id == asset_id);
            if (at != null)
            {
                at.disp_name = a.asset_name;

                int r3 = await g.webapi.put("asset_tree", at.asset_tree_id, at, typeof(asset_tree));
                if (r3 != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
            }

            // Step 2. 인텔리전트 트리 메모리 수정 및 DB 수정

            // Step 3. 즐겨 찾기 트리 메모리 수정 및 DB 수정

            var ft = g.favorite_tree_list.Find(p => p.asset_id == asset_id);
            if (ft != null)
            {
                ft.disp_name = a.asset_name;
                int r4 = await g.webapi.put("favorite_tree", ft.favorite_tree_id, ft, typeof(favorite_tree));
                if (r4 != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
            }

            // Step 4. 화면 갱신(자산트리, 인텔리전트트리, 즐겨찾기트리)
            _ctlLeftSide.editAssetToTreeView(asset_id);
            // 여기서 인텔리전트트리 화면 갱신 루틴 호출 (정현석)
            // 여기서 즐겨찾기트리 화면 갱신 루틴 호출 (정현석)

            // 배선뷰에서 수정
            g._P5LineManager.edit_ipp_asset(asset_id, a.asset_name);

            return true;
        }
        #endregion

        #region 추가 루틴
        public async Task<bool> addRegion1(int region1_id)
        {
            g.location_list = (List<location>)await g.webapi.getList("location", typeof(List<location>));
            var l = g.location_list.Find(p => p.region1_id == region1_id);
            if (l == null)
                return false;
            await addLocationNoTreeView(l.location_id);
            return true;
        }

        public async Task<bool> addRegion2(int region2_id)
        {
            g.location_list = (List<location>)await g.webapi.getList("location", typeof(List<location>));
            var l = g.location_list.Find(p => p.region2_id == region2_id);
            if (l == null)
                return false;
            await addLocationNoTreeView(l.location_id);
            return true;
        }

        public async Task<bool> addSite(int site_id)
        {
            g.location_list = (List<location>)await g.webapi.getList("location", typeof(List<location>));
            var l = g.location_list.Find(p => p.site_id == site_id);
            if (l == null)
                return false;
            await addLocationNoTreeView(l.location_id);
            return true;
        }


        public async Task<bool> addBuilding(int building_id)
        {
            g.location_list = (List<location>)await g.webapi.getList("location", typeof(List<location>));
            var l = g.location_list.Find(p => p.building_id == building_id);
            if (l == null)
                return false;
            await addLocation(l.location_id);
            return true;
        }

        public async Task<bool> addFloor(int floor_id)
        {
            g.location_list = (List<location>)await g.webapi.getList("location", typeof(List<location>));
            var l = g.location_list.Find(p => p.floor_id == floor_id);
            if (l == null)
                return false;
            await addLocation(l.location_id);
            return true;
        }

        public async Task<bool> addRoom(int room_id)
        {
            g.location_list = (List<location>)await g.webapi.getList("location", typeof(List<location>));
            var l = g.location_list.Find(p => p.room_id == room_id);
            if (l == null)
                return false;
            await addLocation(l.location_id);
            return true;
        }

        public async Task<bool> addRack(int rack_id)
        {
            g.location_list = (List<location>)await g.webapi.getList("location", typeof(List<location>));
            var l = g.location_list.Find(p => p.rack_id == rack_id);
            if (l == null)
                return false;
            await addLocation(l.location_id);
            return true;
        }

        private async Task addLocation(int location_id)
        {
            await add_asset_tree_by_location_id(location_id);
            // await add_favorite_tree();

            _ctlLeftSide.addLocationToTreeView(location_id);
        }

        private async Task addLocationNoTreeView(int location_id)
        {
            await add_asset_tree_by_location_id(location_id);
            // await add_favorite_tree();
        }


        
        public async Task<bool> addAsset(int asset_id)
        {
            int num_of_ports = 0;
            int catalog_id = 0;

            // 다이알로그 내부에서 이미 다음 5 가지를 수행 하였음
            //--------------------------------------------------------
            // Step 1. asset
            // Step 2. asset_aux
            // Step 3. asset_ext
            // Step 4. rack_config
            // Step 5. sw_config
            //--------------------------------------------------------
            // 다음 루틴에서 아래 9 가지를 수행하여야 함.
            //--------------------------------------------------------
            // Step 5. asset_port_link
            // Step 6. ipp_connect_status
            // Step 7. asset_ipp_port_link          
            // Step 8. link_diagram                  
            // Step 9. sw_card_config              
            // Step 10. ic_ipp_config            
            // Step 11. user_port_layout                  
            // Step 12. eb_port_config
            // Step 13. eb_port_data_cur
            // Step 14. asset_tree          

            var a = g.asset_list.Find(p => p.asset_id == asset_id);
            if (a == null)
                return false;
            int location_id = a.location_id;

            catalog_id = a.catalog_id;
            var c = g.catalog_list.Find(p => p.catalog_id == catalog_id);
            if (c == null)
                return false;
            num_of_ports = c.num_of_ports;

            await add_asset_port_link(asset_id, catalog_id, num_of_ports);
            await add_ipp_connect_status(asset_id, catalog_id);
            await add_asset_ipp_port_link(asset_id, catalog_id, num_of_ports);        // depre
            //await add_link_diagram(asset_id, catalog_id, num_of_ports);
            await add_sw_card_config(catalog_id);
            await add_ic_ipp_config(catalog_id);
            await add_user_port_layout(asset_id, catalog_id, num_of_ports);
            await add_eb_port_config(catalog_id);
            await add_eb_port_cur(catalog_id);
            await add_asset_tree_by_asset_id(asset_id);
            
            // 화면 갱신
            //------------------------------------------------------
            // Step 1. 좌측 자산 트리
            // Step 2. 좌측 지능형 트리
            // Step 3. 좌측 즐겨 찾기
            _ctlLeftSide.addAssetToTreeView(asset_id);
            // 여기에서 인텔리전트 트리 추가하는 루틴 호출(화면만 추가하면 됨....정현석)

            // 배선관리에 추가
            g._P5LineManager.add_ipp_asset(asset_id);

            return true;
        }


        private async Task add_asset_port_link(int asset_id, int catalog_id, int num_of_ports)
        {     
            int i;
            string filter = string.Format("?asset_id={0}", asset_id);
            var list = (List<asset_port_link>)await g.webapi.getList("asset_port_link", typeof(List<asset_port_link>), filter);
            if (list == null)
                return;

            for (i = 0; i < num_of_ports; i++)
            {
                var item = list.Find(p => (p.asset_id == asset_id) && (p.port_no == i+1));
                if (item != null)
                {
                    g.asset_port_link_list.Add(item);
                }
            }
        }

        private async Task add_ipp_connect_status(int asset_id, int catalog_id)
        {
            if (!CatalogType.is_ipp(catalog_id))
                return;

            string filter = string.Format("?ipp_asset_id={0}", asset_id);
            var list = (List<ipp_connect_status>)await g.webapi.getList("ipp_connect_status", typeof(List<ipp_connect_status>), filter);

            var item = list.Find(p => p.ipp_asset_id == asset_id);
            if (item != null)
            {
                g.ipp_connect_status_list.Add(item);
            }
        }

        private async Task add_asset_ipp_port_link(int asset_id, int catalog_id, int num_of_ports)
        {
            if (!CatalogType.is_ipp(catalog_id))
                return;

            int i;
            string filter = string.Format("?ipp_asset_id={0}", asset_id);
            var list = (List<asset_ipp_port_link>)await g.webapi.getList("asset_ipp_port_link", typeof(List<asset_ipp_port_link>), filter);

            for (i = 0; i < num_of_ports; i++)
            {
                var item = list.Find(p => (p.ipp_asset_id == asset_id) && (p.port_no == i+1));
                if (item != null)
                {
                    g.asset_ipp_port_link_list.Add(item);
                }
            }
        }

        public async Task add_sw_card_config(int catalog_id)
        {
            if (!CatalogType.is_sw(catalog_id))
                return;

            g.sw_card_config_list = (List<sw_card_config>)await g.webapi.getList("sw_card_config", typeof(List<sw_card_config>));
        }

        public async Task add_ic_ipp_config(int catalog_id)
        {
            if (!CatalogType.is_ic(catalog_id))
                return;

            g.ic_ipp_config_list = (List<ic_ipp_config>)await g.webapi.getList("ic_ipp_config", typeof(List<ic_ipp_config>));
        }

        public async Task add_user_port_layout(int asset_id, int catalog_id, int num_of_ports)
        {
            if (!CatalogType.is_fp(catalog_id) && !CatalogType.is_mb(catalog_id))
                return;

            int i;
            string filter = string.Format("?asset_id={0}", asset_id);
            var list = (List<user_port_layout>)await g.webapi.getList("user_port_layout", typeof(List<user_port_layout>), filter);

            for (i = 0; i < num_of_ports; i++)
            {
                var item = list.Find(p => (p.asset_id == asset_id) && (p.port_no == i+1));
                if (item != null)
                {
                    g.user_port_layout_list.Add(item);
                }
            }
        }

        private async Task add_eb_port_config(int catalog_id)
        {
            if (!CatalogType.is_eb(catalog_id))
                return;

            g.eb_port_config_list = (List<eb_port_config>)await g.webapi.getList("eb_port_config", typeof(List<eb_port_config>));
        }

        private async Task add_eb_port_cur(int catalog_id)
        {
            if (!CatalogType.is_eb(catalog_id))
                return;

            g.eb_port_data_cur_list = (List<eb_port_data_cur>)await g.webapi.getList("eb_port_data_cur", typeof(List<eb_port_data_cur>));
        }

        public async Task<bool> reload_ic_connect_status()
        {
            var list = (List<ic_connect_status>)await g.webapi.getList("ic_connect_status", typeof(List<ic_connect_status>));
            if (list == null)
                return false;
            g.ic_connect_status_list = list;
            return true;
        }

        private async Task<bool> reload_fw_upgrade_hist()
        {
            var list = (List<fw_upgrade_hist>)await g.webapi.getList("fw_upgrade_hist", typeof(List<fw_upgrade_hist>));
            if (list == null)
                return false;
            g.fw_upgrade_hist_list = list;
            return true;
        }

        private async Task<bool> reload_asset(int asset_id)
        {
            var a = (asset) await g.webapi.get("asset", asset_id, typeof(asset));
            if (a == null)
                return false;

            g.asset_list.RemoveAll(p => p.asset_id == asset_id);
            g.asset_list.Add(a);
            return true;
        }

        private async Task<bool> reload_region1(int region1_id)
        {
            var node = (region1)await g.webapi.get("region1", region1_id, typeof(region1));
            if (node == null)
                return false;

            g.region1_list.RemoveAll(p => p.region1_id == region1_id);
            g.region1_list.Add(node);
            return true;
        }

        private async Task<bool> reload_region2(int region2_id)
        {
            var node = (region2)await g.webapi.get("region2", region2_id, typeof(region2));
            if (node == null)
                return false;

            g.region2_list.RemoveAll(p => p.region2_id == region2_id);
            g.region2_list.Add(node);
            return true;
        }

        private async Task<bool> reload_site(int site_id)
        {
            var node = (site)await g.webapi.get("site", site_id, typeof(site));
            if (node == null)
                return false;

            g.site_list.RemoveAll(p => p.site_id == site_id);
            g.site_list.Add(node);
            return true;
        }

        private async Task<bool> reload_site_environment()
        {
            var list = (List<site_environment>)await g.webapi.getList("site_environment", typeof(List<site_environment>));
            if (list == null)
                return false;

            g.site_environment_list = list;
            return true;
        }

        private async Task<bool> reload_building(int building_id)
        {
            var node = (building)await g.webapi.get("building", building_id, typeof(building));
            if (node == null)
                return false;

            g.building_list.RemoveAll(p => p.building_id == building_id);
            g.building_list.Add(node);
            return true;
        }

        private async Task<bool> reload_floor(int floor_id)
        {
            var node = (floor)await g.webapi.get("floor", floor_id, typeof(floor));
            if (node == null)
                return false;

            g.floor_list.RemoveAll(p => p.floor_id == floor_id);
            g.floor_list.Add(node);
            return true;
        }

        private async Task<bool> reload_room(int room_id)
        {
            var node = (room)await g.webapi.get("room", room_id, typeof(room));
            if (node == null)
                return false;

            g.room_list.RemoveAll(p => p.room_id == room_id);
            g.room_list.Add(node);
            return true;
        }

        private async Task<bool> reload_rack(int rack_id)
        {
            var node = (rack)await g.webapi.get("rack", rack_id, typeof(rack));
            if (node == null)
                return false;

            g.rack_list.RemoveAll(p => p.rack_id == rack_id);
            g.rack_list.Add(node);
            return true;
        }

        private async Task<bool> reload_location(int location_id)
        {
            var l = (location)await g.webapi.get("location", location_id, typeof(location));
            if (l == null)
                return false;

            g.location_list.RemoveAll(p => p.location_id == location_id);
            g.location_list.Add(l);
            return true;
        }

        private async Task<bool> reload_asset_tree()
        {
            var list = (List<asset_tree>)await g.webapi.getList("asset_tree", typeof(List<asset_tree>));
            if (list == null)
                return false;
            if (list.Count == 0)
                return false;
            g.asset_tree_list = list;
            return true;
        }

        private async Task<bool> reload_asset_tree(int asset_id)
        {
            string filter = string.Format("?asset_id={0}", asset_id);
            var list = (List<asset_tree>)await g.webapi.getList("asset_tree", typeof(List<asset_tree>), filter);
            if (list == null)
                return false;
            if (list.Count == 0)
                return false;
            g.asset_tree_list.RemoveAll(p => p.asset_id == asset_id);
            g.asset_tree_list.Add(list.ElementAt(0));
            return true;
        }

        private async Task<bool> reload_favorite_tree(int asset_id)
        {
            string filter = string.Format("?asset_id={0}", asset_id);
            var list = (List<favorite_tree>)await g.webapi.getList("favorite_tree", typeof(List<favorite_tree>), filter);
            if (list == null)
                return false;
            if (list.Count == 0)
                return false;
            g.favorite_tree_list.RemoveAll(p => p.asset_id == asset_id);
            g.favorite_tree_list.Add(list.ElementAt(0));
            return true;
        }

        private async Task reload_asset_ext(int asset_id)
        {
            string filter = string.Format("?asset_id={0}", asset_id);
            var list = (List<asset_ext>)await g.webapi.getList("asset_ext", typeof(List<asset_ext>), filter);
            g.asset_ext_list.RemoveAll(p => p.asset_id == asset_id);
            foreach (var node in list)
            {
                var ae = list.Find(p => p.asset_ext_id == node.asset_ext_id);
                if (ae != null)
                    g.asset_ext_list.Add(node);
            }
        }

        // 실제 슬롯형 스위치는 수량이 많지 않으므로 모두 리로드..
        private async Task<bool> reload_sw_card_config(int asset_id)
        {
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            if (!CatalogType.is_sw_slot(catalog_id))
                return false;
            var list = (List<sw_card_config>)await g.webapi.getList("sw_card_config", typeof(List<sw_card_config>));
            if (list == null)
                return false;
            g.sw_card_config_list = list;
            return true;
        }

        private async Task<bool> reload_rack_config(int asset_id)
        {
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            if (!CatalogType.is_rack_mountable(catalog_id))
                return false;
            int location_id = Etc.get_location_id_by_asset_id(asset_id);
            if (location_id == 0)
                return false;
            int rack_id = Etc.get_rack_id_by_location_id(location_id);
            if (rack_id == 0)
                return false;
            string filter = string.Format("?rack_id={0}", rack_id);
            var list = (List<rack_config>)await g.webapi.getList("rack_config", typeof(List<rack_config>), filter);
            if (list == null)
                return false;
            if (list.Count == 0)
                return false;
            g.rack_config_list.RemoveAll(p => p.rack_id == rack_id);
            foreach(var node in list)
            {
                g.rack_config_list.Add(node);
            }
            return true;
        }

        private async Task<bool> reload_asset_aux(int asset_id)
        {
            var aa = (asset_aux)await g.webapi.get("asset_aux", asset_id, typeof(asset_aux));
            if (aa == null)
                return false;

            g.asset_aux_list.Add(aa);
            return true;
        }

        private async Task<bool> reload_asset_terminal(int asset_id)
        {
            string filter = string.Format("?terminal_asset_id={0}", asset_id);
            var cur_list = (List<asset_terminal>)await g.webapi.getList("asset_terminal", typeof(List<asset_terminal>), filter);
            if (cur_list.Count == 0)
                return false;

            var cur_at = cur_list.Find(p => p.terminal_asset_id == asset_id);
            if (cur_at == null)
                return false;

            var old = g.asset_terminal_list.Find(p => p.terminal_asset_id == asset_id);
            if (old == null)
                g.asset_terminal_list.Add(cur_at);
            else
                old = cur_at;
            return true;
        }

        private async Task<bool> reload_asset_terminal_ip(int terminal_id)
        {
            string filter = string.Format("?terminal_id={0}", terminal_id);
            var cur_list = (List<asset_terminal_ip>)await g.webapi.getList("asset_terminal_ip", typeof(List<asset_terminal_ip>), filter);
            if (cur_list == null)
                return false;

            foreach(var node in cur_list)
            {
                var ati = g.asset_terminal_ip_list.Find(p => p.terminal_id == node.terminal_id);
                if (ati == null)
                    g.asset_terminal_ip_list.Add(node);
                else
                    ati = node;
            }

            return false;
        }


        private async Task add_asset_tree_by_asset_id(int asset_id)
        {
            string filter = string.Format("?asset_id={0}", asset_id);
            var cur_list = (List<asset_tree>)await g.webapi.getList("asset_tree", typeof(List<asset_tree>), filter);
            if (cur_list == null)
                return;

            asset_tree cur_at = cur_list.Find(p => p.asset_id == asset_id);
            if (cur_at == null)
                return;

            g.asset_tree_list.Add(cur_at);
        }

        private async Task add_asset_tree_by_location_id(int location_id)
        {
            string filter = string.Format("?location_id={0}", location_id);
            var cur_list = (List<asset_tree>)await g.webapi.getList("asset_tree", typeof(List<asset_tree>), filter);
            if (cur_list == null)
                return;

            asset_tree cur_at = cur_list.Find(p => p.location_id == location_id);
            if (cur_at == null)
                return;

            g.asset_tree_list.Add(cur_at);
        }

        private async Task add_favorite_tree()
        {
            g.favorite_tree_list = (List<favorite_tree>)await g.webapi.getList("favorite_tree", typeof(List<favorite_tree>));
        }
        #endregion

        #region 삭제 루틴
        public async Task deleteLocation(int location_id)
        {
            var l = g.location_list.Find(p => p.location_id == location_id);
            if (l == null)
                return;
            int disp_level = l.location_level;


            // Step 2. DB 삭제 
            int ret = 0;
            switch (disp_level)
            {
                case 1:
                    int region1_id = l.region1_id ?? 0;
                    ret = await g.webapi.delete("region1", region1_id);
                    g.region1_list.RemoveAll(p => p.region1_id == region1_id);
                    break;
                case 2:
                    int region2_id = l.region2_id ?? 0;
                    ret = await g.webapi.delete("region2", region2_id);
                    g.region2_list.RemoveAll(p => p.region2_id == region2_id);
                    break;
                case 3:
                    int site_id = l.site_id ?? 0;
                    ret = await g.webapi.delete("site", site_id);
                    g.site_list.RemoveAll(p => p.site_id == site_id);
                    g.site_environment_list.RemoveAll(p => p.site_id == site_id);
                    break;
                case 4:
                    int building_id = l.building_id ?? 0;
                    ret = await g.webapi.delete("building", building_id);
                    g.building_list.RemoveAll(p => p.building_id == building_id);
                    break;
                case 5:
                    int floor_id = l.floor_id ?? 0;
                    ret = await g.webapi.delete("floor", floor_id);
                    g.floor_list.RemoveAll(p => p.floor_id == floor_id);
                    break;
                case 6:
                    int room_id = l.room_id ?? 0;
                    ret = await g.webapi.delete("room", room_id);
                    g.room_list.RemoveAll(p => p.room_id == room_id);
                    break;
                case 7:
                    int rack_id = l.rack_id ?? 0;
                    ret = await g.webapi.delete("rack", rack_id);
                    g.rack_list.RemoveAll(p => p.rack_id == rack_id);
                    g.rack_config_list.RemoveAll(p => p.rack_id == rack_id);
                    break;
            }
            if (ret != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_1"));
                return;
            }

            // 화면 갱신
            //------------------------------------------------------
            // Step 1. 좌측 자산 트리
            // Step 2. 좌측 지능형 트리
            // Step 3. 좌측 즐겨 찾기
            _ctlLeftSide.delLocationToTreeView(location_id);


            // Step 3. 노드를 연결하고 삭제 
            //if (prev_asset_tree_id > 0)
            //{
            //    var find = g.asset_tree_list.Find(p => p.asset_tree_id == prev_asset_tree_id);
            //    if (find != null)
            //        find.next_asset_tree_id = next_asset_tree_id;
            //}
            //if (next_asset_tree_id > 0)
            //{
            //    var find = g.asset_tree_list.Find(p => p.asset_tree_id == next_asset_tree_id);
            //    if (find != null)
            //        find.prev_asset_tree_id = prev_asset_tree_id;
            //}
            //if (at != null)
            //    g.asset_tree_list.Remove(at);

            //if (prev_favorite_tree_id > 0)
            //{
            //    var find = g.favorite_tree_list.Find(p => p.favorite_tree_id == prev_favorite_tree_id);
            //    if (find != null)
            //        find.next_favorite_tree_id = next_favorite_tree_id;
            //}
            //if (next_favorite_tree_id > 0)
            //{
            //    var find = g.favorite_tree_list.Find(p => p.favorite_tree_id == next_favorite_tree_id);
            //    if (find != null)
            //        find.prev_favorite_tree_id = prev_favorite_tree_id;
            //}
            //if (ft != null)
            //    g.favorite_tree_list.Remove(ft);

            //if (prev_location_id > 0)
            //{
            //    var find = g.location_list.Find(p => p.location_id == prev_location_id);
            //    if (find != null)
            //        find.next_location_id = next_location_id;
            //}
            //if (next_asset_tree_id > 0)
            //{
            //    var find = g.location_list.Find(p => p.location_id == next_location_id);
            //    if (find != null)
            //        find.prev_location_id = prev_location_id;
            //}
            g.location_list.Remove(l);
        }

        public async Task<bool> addFavorite(int location_id, int asset_id)
        {
            var a = g.asset_list.Find(p => p.asset_id == asset_id);
            if (a == null)
                return false;

            // Step 1. 메모리에 추가

            // 즐겨찾기에 이미 있는 경우 조용히 빠져나감.
            var ft = g.favorite_tree_list.Find(p => p.asset_id == asset_id);
            //var ft = g.favorite_tree_list.Find(p => (p.asset_id == asset_id) && (p.reg_user_id == g.login_user_id));
            if (ft != null)
                return true;

            var new_node = new favorite_tree()
            {
                asset_id = a.asset_id,
                disp_level = 1,
                disp_name = a.asset_name,
                location_id = location_id,
                reg_user_id = g.login_user_id,
                is_alarm = "-"
            };

            var c = g.catalog_list.Find(p => p.catalog_id == a.catalog_id);
            int catalog_group_id = c != null ? c.catalog_group_id : 0;
            int image_id = ImageIcon.get_icon_id_by_catalog_group_id(catalog_group_id);
            new_node.image_id = image_id;

            // Step 2. DB에 추가

            var new_node2 = (favorite_tree) await g.webapi.post("favorite_tree", new_node, typeof(favorite_tree));
            if (new_node2 == null)
                return false;
            g.favorite_tree_list.Add(new_node2);

            // Step 3. 즐겨찾기 화면에 추가
            _ctlLeftSide.addFtreeAssetTreeVM(asset_id);

            return true;
        }



        public async Task deleteAsset(int asset_id)
        {
            int favorite_tree_id = Etc.get_favorite_tree_id(asset_id);
            // 화면 갱신
            //------------------------------------------------------
            // Step 1. 좌측 자산 트리
            // Step 2. 좌측 지능형 트리
            // Step 3. 좌측 즐겨 찾기

            _ctlLeftSide.delAssetToTreeView(asset_id);
            // 여기에서 인텔리전트 트리 갱신하는 루틴 호출(화면만 삭제하면 됨....정현석)

            if (favorite_tree_id != 0)
            {
                await deleteAssetFromFavoriteTree(asset_id, favorite_tree_id);       // 즐겨찾기에대한 화면, 메모리, DB 3가지 수정
            }

            int r = await g.webapi.delete("asset", asset_id);
            if (r != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_1"));
                return;
            }

            // 삭제 루틴 추가
            // 다음 루틴에서 아래 14 가지를 수행하여야 함.
            //--------------------------------------------------------
            // Step 1. favorite_tree
            // Step 2. asset_tree          
            // Step 3. eb_port_data_cur
            // Step 4. eb_port_config
            // Step 5. user_port_layout                  
            // Step 6. ic_ipp_config            
            // Step 7. sw_card_config              
            // Step 8. link_diagram                  
            // Step 9. asset_ipp_port_link          
            // Step 10. ipp_connect_status
            // Step 11. asset_port_link
            // Step 12. asset_ext
            // Step 13. asset_aux
            // Step 14. asset

            del_favorite_tree(asset_id);                          
            del_asset_tree(asset_id);
            g.rack_config_list.RemoveAll(p => p.asset_id == asset_id);
            if (g.eb_port_data_cur_list !=null)
                g.eb_port_data_cur_list.RemoveAll(p => p.asset_id == asset_id);
            if(g.eb_port_config_list !=null)
                g.eb_port_config_list.RemoveAll(p => p.asset_id == asset_id);
            g.user_port_layout_list.RemoveAll(p => p.asset_id == asset_id);

            var iic = g.ic_ipp_config_list.Find(p => p.ipp_asset_id == asset_id);           // 컨트롤러에 연결된 패치패널이 삭제된 경우
            if (iic != null)
                iic.ipp_asset_id = null;
            g.ic_ipp_config_list.RemoveAll(p => p.ic_asset_id == asset_id);                 // 컨트롤러가 삭제된 경우

            var scc = g.sw_card_config_list.Find(p => p.sw_card_config_id == asset_id);     // 새시형 스위치에 연결된 카드 스위치가 삭제된 경우
            if (scc != null)
                scc.sw_card_asset_id = null;
            g.sw_card_config_list.RemoveAll(p => p.sw_asset_id == asset_id);                // 새시형 스위치가 삭제된 경우

            // g.link_diagram_list.RemoveAll(p => p.pp_asset_id == asset_id);
            g.asset_ipp_port_link_list.RemoveAll(p => p.ipp_asset_id == asset_id);      // depre
            g.ipp_connect_status_list.RemoveAll(p => p.ipp_asset_id == asset_id);
            delete_asset_port_link(asset_id);
            g.asset_ext_list.RemoveAll(p => p.asset_id == asset_id);
            g.asset_aux_list.RemoveAll(p => p.asset_id == asset_id);
            g.asset_list.RemoveAll(p => p.asset_id == asset_id);

            // 배선관리에 추가
            g._P5LineManager.remove_ipp_asset(asset_id);
        }

        public async Task deleteTerminal(int outlet_asset_id, int outlet_port_no, int terminal_asset_id)
        {
            _ctlLeftSide.astMgrLeftSide.removePc(terminal_asset_id);
            _ctlLeftSide.ftreeMgr.removePc(outlet_asset_id, outlet_port_no, terminal_asset_id);
            _ctlLeftSide.ctreeMgr.removePC(terminal_asset_id);
            g.asset_ast_vm_dic.Remove(terminal_asset_id);           

            var at = g.asset_terminal_list.Find(p => p.terminal_asset_id == terminal_asset_id);
            if (at == null)
                return;
            at.terminal_status = "E";       // expired
            int r = await g.webapi.put("asset_terminal", at.terminal_id, at, typeof(asset_terminal));
            if (r != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_1"));
                return;
            }

            // 연결을 끊게 한다.
            var node2 = g.asset_port_link_list.Find(p => (p.asset_id == terminal_asset_id) && (p.port_no == 1));
            if (node2 == null)
                return;
            node2.front_asset_id = null;
            node2.front_port_no = null;
            node2.front_plug_side = null;
            node2.front_cable_catalog_id = null;
            var r1 = await g.webapi.put("asset_port_link", node2.asset_port_link_id, node2, typeof(asset_port_link));
            // 다른 client에도 변경 사항을 보낸다.
            g.signalr.send_simple_link_info_to_signalr(terminal_asset_id, 1, "F", 0, 0, "F", 0, false);

            var node = g.asset_port_link_list.Find(p => (p.asset_id == outlet_asset_id) && (p.port_no == outlet_port_no));
            if (node != null)
            {
                int catalog_id = Etc.get_catalog_id_by_asset_id(node.front_asset_id ?? 0);
                if (catalog_id != g.VIRTUAL_HUB_CATALOG_ID)
                {
                    node.front_asset_id = null;
                    node.front_port_no = null;
                    node.front_plug_side = null;
                    node.front_cable_catalog_id = null;
                    var r2 = await g.webapi.put("asset_port_link", node.asset_port_link_id, node, typeof(asset_port_link));
                    // 다른 client에도 변경 사항을 보낸다.
                    g.signalr.send_simple_link_info_to_signalr(outlet_asset_id, outlet_port_no, "F", 0, 0, "F", 0, false);
                }
            }
        }

        private void delete_asset_port_link(int asset_id)
        {
            var f_list = g.asset_port_link_list.Where(p => p.front_asset_id == asset_id);
            foreach(var node in f_list)
            {
                node.front_asset_id = null;
                node.front_port_no = null;
                node.front_plug_side = null;
                node.front_cable_catalog_id = null;
            }
            var r_list = g.asset_port_link_list.Where(p => p.rear_asset_id == asset_id);
            foreach (var node in r_list)
            {
                node.rear_asset_id = null;
                node.rear_port_no = null;
                node.rear_plug_side = null;
                node.rear_cable_catalog_id = null;
            }
            g.asset_port_link_list.RemoveAll(p => p.asset_id == asset_id);
        }

        public async Task deleteAssetFromFavoriteTree(int asset_id, int favorite_tree_id)
        {
            if (favorite_tree_id == 0)
                return;
            
            // 화면 갱신
            //------------------------------------------------------
            _ctlLeftSide.removeFtreeAssetTreeVM(asset_id);

            // DB 갱신
            //------------------------------------------------------
            int r = await g.webapi.delete("favorite_tree", favorite_tree_id);
            if (r != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_6"));
                return;
            }

            // 메모리 갱신
            //------------------------------------------------------
            del_favorite_tree(asset_id);
        }

        private void del_favorite_tree(int asset_id)
        {
            var cur_at = g.favorite_tree_list.Find(p => p.asset_id == asset_id);
            if (cur_at == null)
                return;

            int cur_asset_tree_id = cur_at.favorite_tree_id;
            g.favorite_tree_list.Remove(cur_at);
        }

        private void del_asset_tree(int asset_id)
        {
            var cur_at = g.asset_tree_list.Find(p => p.asset_id == asset_id);
            if (cur_at == null)
                return;

            int cur_asset_tree_id = cur_at.asset_tree_id;

            g.asset_tree_list.Remove(cur_at);
        }

        #endregion

        #region 이동 루틴
        public async Task<bool> moveAssetTreeItem(AssetTreeVM source_vm, AssetTreeVM source_parent_vm, AssetTreeVM dest_vm, AssetTreeVM dest_parent_vm)
        {
            if ((source_vm == null) || (dest_vm == null))
                return false;
            if (source_vm.asset_tree_id == dest_vm.asset_tree_id)
                return false;
            // 같은 위치에서 움직인 경우
            if (source_parent_vm == dest_parent_vm)
            {
                if ((source_vm.disp_level >= 4) && (source_vm.disp_level <= 7))
                {
                    var list = dest_parent_vm.child_list;
                    await update_disp_order_for_asset_tree(list);
                }
            }
            else if (source_vm.disp_level == (dest_vm.disp_level + 1))
            {
                if ((source_vm.disp_level >= 4) && (source_vm.disp_level <= 7))
                {
                    // 다른 위치의 마지막으로 옮기려고 할 때
                    var list = source_parent_vm.child_list;
                    await update_disp_order_for_asset_tree(list);

                    int source_asset_id = source_vm.asset_id ?? 0;
                    await update_new_location(source_asset_id, dest_vm.location_id);

                    // var list2 = dest_vm.child_list;
                    var node = dest_parent_vm.child_list.Find(p => p.asset_tree_id == dest_vm.asset_tree_id);
                    if (node != null)
                    {
                        var list2 = node.child_list;
                        await update_disp_order_for_asset_tree(list2, dest_vm.location_id);
                    }
                    //source_vm.parant_ast_vm = dest_vm;
                }
            }
            else if (source_vm.disp_level == dest_vm.disp_level) 
            {
                if ((source_vm.disp_level >= 4) && (source_vm.disp_level <= 7))
                {
                    // 다른 위치로 옮기려고 할 때
                    var list = source_parent_vm.child_list;
                    await update_disp_order_for_asset_tree(list);

                    // 자산쪽 location_id 변경
                    int source_asset_id = source_vm.asset_id ?? 0;
                    await update_new_location(source_asset_id, dest_vm.location_id);

                    var list2 = dest_parent_vm.child_list;
                    await update_disp_order_for_asset_tree(list2, dest_vm.location_id);
                    //source_vm.parant_ast_vm = dest_vm.parant_ast_vm;
                }
            }

            return true;
        }

        private async Task<bool> update_new_location(int asset_id, int new_location_id)
        {
            var a = g.asset_list.Find(p => p.asset_id == asset_id);
            if (a != null)
            {
                a.location_id = new_location_id;
                var r = await g.webapi.put("asset", asset_id, a, typeof(asset));
                return r == 0;
            }
            return false;
        }

        // source 측 order 변경
        private async Task<bool> update_disp_order_for_asset_tree(List<AssetTreeVM> list)
        {
            int idx = 0;
            foreach (var node in list)
            {
                idx++;
                int asset_tree_id = node.asset_tree_id;
                if (asset_tree_id > 0)
                {
                    var at = g.asset_tree_list.Find(p => p.asset_tree_id == asset_tree_id);
                    if (at != null)
                    {
                        if (at.disp_order != idx)
                        {
                            at.disp_order = idx;
                            var r = await g.webapi.put("asset_tree", asset_tree_id, at, typeof(asset_tree));
                        }
                    }
                }
            }
            return true;
        }

        // dest 측 order 변경
        private async Task<bool> update_disp_order_for_asset_tree(List<AssetTreeVM> list, int location_id)
        {
            int idx = 0;
            foreach (var node in list)
            {
                idx++;
                int asset_tree_id = node.asset_tree_id;
                if (asset_tree_id > 0)
                {
                    var at = g.asset_tree_list.Find(p => p.asset_tree_id == asset_tree_id);
                    if (at != null)
                    {
                        if ((at.disp_order != idx) || (at.location_id != location_id))
                        {
                            at.disp_order = idx;
                            // 자산들만 location은 통일
                            if ((at.asset_id ?? 0) != 0)
                                at.location_id = location_id;
                            var r = await g.webapi.put("asset_tree", asset_tree_id, at, typeof(asset_tree));

                            // 장소가 다른 장소로 이동.
                            if ((at.asset_id ?? 0) == 0)
                            {
                                switch (at.disp_level)
                                {
                                    case 4 :    // 빌딩
                                        break;
                                    case 5:     // 층
                                        var aaa = g.location_list.Find(p => p.location_id == at.location_id);
                                        var bbb = g.location_list.Find(p => p.location_id == location_id);
                                        if ((aaa != null) && (bbb != null))
                                        {
                                            aaa.building_id = bbb.building_id;
                                            var r1 = await g.webapi.put("location", aaa.location_id, aaa, typeof(location));
                                        }
                                        break;
                                    case 6:     // 룸
                                        break;
                                    case 7 :    // 랙
                                        break;

                                }
                            }
                        }
                    }


                }
            }
            return true;
        }

        public async Task<bool> moveFavoriteTreeItem(AssetTreeVM source_vm, AssetTreeVM dest_vm)
        {
            if ((source_vm == null) || (dest_vm == null))
                return false;
            // 같은 방에서 움직인 경우
            if (source_vm.favorite_tree_id != dest_vm.favorite_tree_id)
            {
                var list = (List<AssetTreeVM>) _ctlLeftSide._tvFavoriteTree.ItemsSource;
                await update_disp_order_for_favorite_tree(list);
            }
            return true;
        }

        private async Task<bool> update_disp_order_for_favorite_tree(List<AssetTreeVM> list)
        {
            int idx = 0;
            foreach (var node in list)
            {
                idx++;
                int favorite_tree_id = node.favorite_tree_id ?? 0;
                if (favorite_tree_id > 0)
                {
                    var ft = g.favorite_tree_list.Find(p => p.favorite_tree_id == favorite_tree_id);
                    if (ft != null)
                    {
                        if (ft.disp_order != idx)
                        {
                            ft.disp_order = idx;
                            var r = await g.webapi.put("favorite_tree", favorite_tree_id, ft, typeof(favorite_tree));
                        }
                    }
                }
            }
            return true;
        }
#endregion

        #region // util
        // 랙마운트의 경우 슬롯이 여유가 있는지 확인한다. 
        public bool check_rack_slot(int dest_location_id, int catalog_id, out string rack_mount_type, out int slot_no2)
        {
            rack_mount_type = "";       // Slot, Left, Right
            slot_no2 = 0;

            if ((dest_location_id == 0) || (catalog_id == 0))
                return false;

            // 랙마운트의 경우 슬롯이 여유가 있는지 확인한다.
            int unit_size = 0;
            var l = g.location_list.Find(p => p.location_id == dest_location_id);
            if (l == null)
                return false;

            int rack_id = l.rack_id ?? 0;
            // 룸에 자산을 추가하는 경우....
            if (rack_id == 0)
                return true;

            if (!Etc.has_rack_catalog(rack_id))
            {
                MessageBox.Show(g.tr_get("C_Error_Rack_Config_1"));
                return true;
            }

            if (CatalogType.is_rack_mountable(catalog_id))
            {
                rack_mount_type = "S";
                slot_no2 = CatalogType.getEmptySlot(rack_id);
                unit_size = CatalogType.get_unit_size(catalog_id);
                if (CatalogType.is_eb(catalog_id))
                {

                    if (unit_size == 0)
                    {
                        // rack_config에 

                        var ipm_l = g.rack_config_list.Find(p => (p.slot_no == 0) && (p.rack_id == rack_id) && (p.rack_mount_type == "L"));
                        var ipm_r = g.rack_config_list.Find(p => (p.slot_no == 0) && (p.rack_id == rack_id) && (p.rack_mount_type == "R"));
                        slot_no2 = 0;

                        if (ipm_l == null)
                            rack_mount_type = "L";
                        else if (ipm_r == null)
                            rack_mount_type = "R";
                        else
                        {
                            MessageBox.Show(g.tr_get("C_Error_Rack_Config_2"));
                            return false;
                        }
                    }
                    return true;
                }

                // 슬롯 여유 체크
                if ((unit_size > 0) && (slot_no2 < 1))
                {
                    MessageBox.Show(g.tr_get("C_Error_Rack_Config_3"));
                    return false;
                }
            }
            else
            {
                MessageBox.Show(g.tr_get("C_Error_Rack_Config_4"));
                return false;
            }
            return true;
        }
        // 랙 저장 
        public async Task<bool> add_to_rack_config(int rack_id, string rack_mount_type, int slot_no2, int asset_id, int catalog_id)
        {
            // rack_config 를 여기서...
            if (rack_mount_type != "")
            {
                var rc = new rack_config()
                {
                    rack_id = rack_id,
                    rack_mount_type = rack_mount_type,
                    slot_no = slot_no2,
                    asset_id = asset_id,
                    catalog_id = catalog_id
                };

                var rc2 = (rack_config)await g.webapi.post("rack_config", rc, typeof(rack_config));

                if (rc2 == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                g.rack_config_list.Add(rc2);
            }
            return true;
        }

        // 랙 
        private async Task<bool> add_to_rack(int asset_id)
        {
            int dest_location_id = Etc.get_location_id_by_asset_id(asset_id);
            if (dest_location_id == 0)
                return false;
            int catalog_id = Etc.get_catalog_id_by_asset_id(asset_id);
            if (catalog_id == 0)
                return false;

            // 랙마운트의 경우 슬롯이 여유가 있는지 확인한다.

            string rack_mount_type = "";       // Slot, Left, Right
            int slot_no2 = 0;
            int unit_size = 0;
            var l = g.location_list.Find(p => p.location_id == dest_location_id);
            if (l == null)
                return false;

            int rack_id = l.rack_id ?? 0;
            // 랙에 자산을 추가하는 경우....
            if (rack_id == 0)
                return false;

            if (CatalogType.is_rack_mountable(catalog_id))
            {
                rack_mount_type = "S";
                slot_no2 = CatalogType.getEmptySlot(rack_id);
                if (CatalogType.is_eb(catalog_id))
                {
                    unit_size = CatalogType.get_unit_size(catalog_id);

                    if (unit_size == 0)
                    {
                        // rack_config에 

                        var ipm_l = g.rack_config_list.Find(p => (p.slot_no == 0) && (p.rack_id == rack_id) && (p.rack_mount_type == "L"));
                        var ipm_r = g.rack_config_list.Find(p => (p.slot_no == 0) && (p.rack_id == rack_id) && (p.rack_mount_type == "R"));
                        slot_no2 = 0;

                        if (ipm_l == null)
                            rack_mount_type = "L";
                        else if (ipm_r == null)
                            rack_mount_type = "R";
                        else
                        {
                            MessageBox.Show(g.tr_get("C_Error_Rack_Config_2"));
                            return false;
                        }
                    }
                }

                // 슬롯 여유 체크
                if ((unit_size > 0) && (slot_no2 < 1))
                {
                    MessageBox.Show(g.tr_get("C_Error_Rack_Config_3"));
                    return false;
                }
            }
            else
            {
                MessageBox.Show(g.tr_get("C_Error_Rack_Config_4"));
                return false;
            }

            // rack_config 를 여기서...
            if (rack_mount_type != "")
            {
                var rc = new rack_config()
                {
                    rack_id = rack_id,
                    rack_mount_type = rack_mount_type,
                    slot_no = slot_no2,
                    asset_id = asset_id,
                    catalog_id = catalog_id
                };

                var rc2 = (rack_config)await g.webapi.post("rack_config", rc, typeof(rack_config));

                if (rc2 == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                g.rack_config_list.Add(rc2);
            }
            return true;
        }

        // cell=cable로 cable을 기준으로... 연결....
        public async Task<bool> connect_asset_port_link(WorkCell left, WorkCell right, WorkCell cell)
        {
            int left_asset_id = left.asset_id;
            int right_asset_id = right.asset_id;
            int left_port_no = left.port_no;
            int right_port_no = right.port_no;
            string left_plug_side = left.is_left_front ? "R" : "F";
            string right_plug_side = right.is_left_front ? "F" : "R";
            int cable_catalog_id = cell.catalog_id;
            if ((left_asset_id <= 0) || (right_asset_id <= 0) || (left_port_no <= 0) || (right_port_no <= 0))
                return false;

            var left_apl = g.asset_port_link_list.Find(p => (p.asset_id == left_asset_id) && (p.port_no == left_port_no));
            var right_apl = g.asset_port_link_list.Find(p => (p.asset_id == right_asset_id) && (p.port_no == right_port_no));
            if ((left_apl == null) || (right_apl == null))
                return false;

            if (left.is_left_front)
            {
                left_apl.rear_asset_id = right_asset_id;
                left_apl.rear_port_no = right_port_no;
                left_apl.rear_cable_catalog_id = cell.catalog_id;
                left_apl.rear_plug_side = right.is_left_front ? "F" : "R";
            }
            else
            {
                left_apl.front_asset_id = right_asset_id;
                left_apl.front_port_no = right_port_no;
                left_apl.front_cable_catalog_id = cell.catalog_id;
                left_apl.front_plug_side = right.is_left_front ? "F" : "R";
            }

            if (right.is_left_front)
            {
                right_apl.front_asset_id = left_asset_id;
                right_apl.front_port_no = left_port_no;
                right_apl.front_cable_catalog_id = cell.catalog_id;
                right_apl.front_plug_side = left.is_left_front ? "R" : "F";
            }
            else
            {
                right_apl.rear_asset_id = left_asset_id;
                right_apl.rear_port_no = left_port_no;
                right_apl.rear_cable_catalog_id = cell.catalog_id;
                right_apl.rear_plug_side = left.is_left_front ? "R" : "F";
            }

            var r1 = await g.webapi.put("asset_port_link", left_apl.asset_port_link_id, left_apl, typeof(asset_port_link));
            var r2 = await g.webapi.put("asset_port_link", right_apl.asset_port_link_id, right_apl, typeof(asset_port_link));

            if ((r1 != 0) || (r2 != 0))
            {
                if (left.is_left_front)
                {
                    left_apl.rear_asset_id = null;
                    left_apl.rear_port_no = null;
                    left_apl.rear_cable_catalog_id = null;
                    left_apl.rear_plug_side = null;
                }
                else
                {
                    left_apl.front_asset_id = null;
                    left_apl.front_port_no = null;
                    left_apl.front_cable_catalog_id = null;
                    left_apl.front_plug_side = null;
                }

                if (right.is_left_front)
                {
                    right_apl.front_asset_id = null;
                    right_apl.front_port_no = null;
                    right_apl.front_cable_catalog_id = null;
                    right_apl.front_plug_side = null;
                }
                else
                {
                    right_apl.rear_asset_id = null;
                    right_apl.rear_port_no = null;
                    right_apl.rear_cable_catalog_id = null;
                    right_apl.rear_plug_side = null;
                }
                return false;
            }

            // 잘 저장하였으면 다른 클라이언트에게도 이 사실을 알려 업데이트 하게 함.
            g.signalr.send_simple_link_info_to_signalr(left_asset_id, left_port_no, left_plug_side, right_asset_id, right_port_no, right_plug_side, cable_catalog_id, true);
            return true;
        }


        // cell=cable로 cable을 기준으로... 연결....
        public async Task<bool> disconnect_asset_port_link(WorkCell left, WorkCell right, WorkCell cell)
        {
            int left_asset_id = left.asset_id;
            int right_asset_id = right.asset_id;
            int left_port_no = left.port_no;
            int right_port_no = right.port_no;
            string left_plug_side = left.is_left_front ? "R" : "F";
            string right_plug_side = right.is_left_front ? "F" : "R";
            int cable_catalog_id = cell.catalog_id;
            if ((left_asset_id <= 0) || (right_asset_id <= 0) || (left_port_no <= 0) || (right_port_no <= 0))
                return false;

            var left_apl = g.asset_port_link_list.Find(p => (p.asset_id == left_asset_id) && (p.port_no == left_port_no));
            var right_apl = g.asset_port_link_list.Find(p => (p.asset_id == right_asset_id) && (p.port_no == right_port_no));
            if ((left_apl == null) || (right_apl == null))
                return false;

            if (left.is_left_front)
            {
                left_apl.rear_asset_id = null;
                left_apl.rear_port_no = null;
                left_apl.rear_cable_catalog_id = null;
                left_apl.rear_plug_side = null;
            }
            else
            {
                left_apl.front_asset_id = null;
                left_apl.front_port_no = null;
                left_apl.front_cable_catalog_id = null;
                left_apl.front_plug_side = null;
            }

            if (right.is_left_front)
            {
                right_apl.front_asset_id = null;
                right_apl.front_port_no = null;
                right_apl.front_cable_catalog_id = null;
                right_apl.front_plug_side = null;
            }
            else
            {
                right_apl.rear_asset_id = null;
                right_apl.rear_port_no = null;
                right_apl.rear_cable_catalog_id = null;
                right_apl.rear_plug_side = null;
            }

            var r1 = await g.webapi.put("asset_port_link", left_apl.asset_port_link_id, left_apl, typeof(asset_port_link));
            var r2 = await g.webapi.put("asset_port_link", right_apl.asset_port_link_id, right_apl, typeof(asset_port_link));

            if ((r1 != 0) || (r2 != 0))
            {
                if (left.is_left_front)
                {
                    left_apl.rear_asset_id = right_asset_id;
                    left_apl.rear_port_no = right_port_no;
                    left_apl.rear_cable_catalog_id = cell.catalog_id;
                    left_apl.rear_plug_side = right.is_left_front ? "F" : "R";
                }
                else
                {
                    left_apl.front_asset_id = right_asset_id;
                    left_apl.front_port_no = right_port_no;
                    left_apl.front_cable_catalog_id = cell.catalog_id;
                    left_apl.front_plug_side = right.is_left_front ? "F" : "R";
                }

                if (right.is_left_front)
                {
                    right_apl.front_asset_id = left_asset_id;
                    right_apl.front_port_no = left_port_no;
                    right_apl.front_cable_catalog_id = cell.catalog_id;
                    right_apl.front_plug_side = left.is_left_front ? "R" : "F";
                }
                else
                {
                    right_apl.rear_asset_id = left_asset_id;
                    right_apl.rear_port_no = left_port_no;
                    right_apl.rear_cable_catalog_id = cell.catalog_id;
                    right_apl.rear_plug_side = left.is_left_front ? "R" : "F";
                }
                return false;
            }
            // 잘 저장하였으면 다른 클라이언트에게도 이 사실을 알려 업데이트 하게 함.
            g.signalr.send_simple_link_info_to_signalr(left_asset_id, left_port_no, left_plug_side, right_asset_id, right_port_no, right_plug_side, cable_catalog_id, false);
            return true;
        }

        public async Task<bool> delete_all_asset(int site_id)
        {
            var list = Etc.get_asset_id_list(site_id);

            foreach(int asset_id in list)
            {
                await deleteAsset(asset_id);
            }

            return true;
        }
        #endregion
    }
}
