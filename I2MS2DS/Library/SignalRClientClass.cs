﻿using System;
using WebApi.Models;

namespace I2MS2.Library
{
    using I2MS2.Models;
    using Microsoft.AspNet.SignalR.Client;
    using Microsoft.Expression.Media.Effects;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using System.Windows.Threading;
    using WebApi;

    // 시그날 알 
    public class SignalRClientClass2
    {
        public IHubProxy _chat;
        private HubConnection _connection;
        // List<SignalRMsg> _signalr_list = new List<SignalRMsg>();

        private Color _red = Colors.Transparent;
        private Color _green = Colors.Transparent;
        private Color _blue = Colors.Transparent;
        private Color _yellow = Colors.Transparent;

        public async Task<bool> connect(string uri)
        {

            _red = (Color)App.Current.Resources["_colorRed"];
            _green = (Color)App.Current.Resources["_colorGreen"];
            _blue = (Color)App.Current.Resources["_colorBlue"];
            _yellow = (Color)App.Current.Resources["_colorYellow"];

            set_connect_color(Colors.Gray);
            _connection = new HubConnection(uri, true);

            // 아래 코드는 디버그가 필요한 경우 사용  -- romee/jake
            _connection.TraceLevel = TraceLevels.All;
            _connection.TraceWriter = Console.Out;

            _chat = _connection.CreateHubProxy("jakeHub");

            _chat.On("broadcastMessage", (SignalRMsg msg) =>
            {
                // _signalr_list.Add(msg);

                App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
                {
                    var b = process_msg(msg);
                    Console.WriteLine("SignalR Received --> " + msg.type + ": " + msg.message);
                    //Thread.Sleep(100);
                }));
                //Thread.Sleep(100);
            });

            //_chat.Received += _hubConnection_Received;

            _connection.Reconnected += Connection_Reconnected;
            _connection.Reconnecting += Connection_Reconnecting;
            _connection.StateChanged += Connection_StateChanged;
            _connection.Error += connection_Error;
            _connection.ConnectionSlow += connection_ConnectionSlow;

            _connection.Closed += Connection_Closed;

            try
            {
                //_connection.Start().Wait();
                await _connection.Start();
                Console.WriteLine("Connected to SignalR Server.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
                return false;
            }
            return true;
        }

        void connection_ConnectionSlow()
        {
            Console.WriteLine(string.Format("connection_ConnectionSlow New State:" + _connection.State + " " + _connection.ConnectionId));
        }

        void connection_Error(Exception obj)
        {
            Console.WriteLine(string.Format("connection_Error New State:" + _connection.State + " " + _connection.ConnectionId));
        }

        public event Action<bool> ConnectionEvent;
        public ConnectionState State
        {
            get { return _connection.State; }
        }
 

        // 서버 연결 상태가 변경되면 컬러 상태를 바꾸어 준다. -> WPF 에서 이벤트로 처리 
        void Connection_StateChanged(StateChange obj)
        {
            switch (this.State)
            {
                case ConnectionState.Connecting:
                    set_connect_color(_red);
                    break;
                case ConnectionState.Connected:
                    set_connect_color(_green);
                    break;
                case ConnectionState.Reconnecting:
                    set_connect_color(_red);
                    break;
                case ConnectionState.Disconnected:
                    set_connect_color(_red);
                    break;
            }

            if (this.State == ConnectionState.Connected)
            {
                if (ConnectionEvent != null) 
                    ConnectionEvent.Invoke(true);
            }
            else
            {
                if (ConnectionEvent != null) 
                    ConnectionEvent.Invoke(false);
            }
            Console.WriteLine(string.Format("connection_StateChanged New State:" + _connection.State + " " + _connection.ConnectionId));
            // romee 2015/05/07 서버가 끈어지면 타이머를 돌려 1분마다 재시도 처리 필요 // 부산 주택금융공사 
            // 여기서 처리 요망 

        }
        // 커넥션이 다시 되는 경우 
        void Connection_Reconnecting()
        {
            Console.WriteLine(string.Format("connection_Reconnecting New State:" + _connection.State + " " + _connection.ConnectionId));
        }

        void Connection_Reconnected()
        {
            Console.WriteLine(string.Format("connection_Reconnected New State:" + _connection.State + " " + _connection.ConnectionId));
        }
 
        // 커넥션이 종료된 경우 -> 디버깅시 발생 
        private void Connection_Closed()
        {
            Console.WriteLine("SignalR Client Disconnected.");
            Thread.Sleep(30000);
            connect(g.signalr_uri_string);
        }

        // 서버와 연결/해제 되면 해당 색상이 바뀜
        private void set_connect_color(Color color)
        {
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
            {
                g.main_window._colorConnect.DarkColor = color;
                g.main_window._colorConnect.LightColor = color;
            }));

        }

        public void send(SignalRMsg msg)
        {
            try
            {
                _chat.Invoke("Send", new object[] { msg });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error={0}, Message={1}", e.HResult, e.Message);
            }
        }
        // 수신 메시지 처리 
        private async Task<bool> process_msg(SignalRMsg msg)
        {
            eSignalRMsgType type = msg.type;
            switch (type)
            {
                case eSignalRMsgType.eICStatus :
                    // 컨트롤러 연결 상태 갱신
                    g.left_tree_handler.change_ic_status(msg.sys_id, msg.connected_flag);
                    break;
                case eSignalRMsgType.ePPStatus:
                    // 패치 패널 연결 상태 갱신
                    g.left_tree_handler.change_ipp_status(msg.sys_id, msg.pp_id, msg.connected_flag);
                    break;
                case eSignalRMsgType.ePortStatus:
                    // 포트 상태 갱신
                    g.left_tree_handler.change_port_status(msg.sys_id, msg.pp_id, msg.port_no, 
                        msg.remote_sys_id, msg.remote_pp_id, msg.remote_port_no, msg.port_status);
                    break;
                case eSignalRMsgType.eLinkInfo:
                    // 연결 정보 갱신(XC용)
                    g.left_tree_handler.change_link_info(msg.sys_id, msg.pp_id, msg.port_no,
                        msg.remote_sys_id, msg.remote_pp_id, msg.remote_port_no, msg.port_status);
                    break;
                case eSignalRMsgType.eLinkInfoIC:
                    // 연결 정보 갱신(IC용)
                    g.left_tree_handler.change_link_info_ic(msg.sys_id, msg.pp_id, msg.port_no,
                        msg.sw_asset_id, msg.sw_port_no, msg.port_status);
                    break;
                case eSignalRMsgType.eSimpleLinkInfo:
                    // 단순 연결 정보 갱신용(배선 관리)
                    g.left_tree_handler.change_simple_link_info(msg.asset_id, msg.port_no, msg.plug_side, msg.asset_id2, msg.port_no2, msg.plug_side2, msg.cable_catalog_id, msg.on_off);
                    break;
                case eSignalRMsgType.eSwitchPortStatus:
                    // 스위치 포트 상태 갱신
                    g.left_tree_handler.change_sw_status(msg.sw_asset_id, msg.sw_port_no, msg.port_status);
                    break;
                case eSignalRMsgType.eClearFlagWithPortStatus:
                    // 상태 정보 클리어...
                    g.left_tree_handler.clear_flag_with_port_status(msg.sys_id, msg.pp_id, msg.port_no, msg.port_status);
                    break;
                case eSignalRMsgType.eAlarmStatus:
                    // 알람 상태 갱신
                    g.left_tree_handler.change_alarm_status(msg.sys_id, msg.pp_id, msg.port_no, msg.alarm_status);
                    break;
                case eSignalRMsgType.eWorkOrderStatus:
                    // 작업 지시 상태 갱신
                    g.left_tree_handler.change_wo_status(msg.sys_id, msg.pp_id, msg.port_no, msg.wo_status);
                    break;
                case eSignalRMsgType.eTraceStatus:
                    // 포트 트레이스 갱신
                    g.left_tree_handler.change_trace_status(msg.sys_id, msg.pp_id, msg.port_no, msg.trace_status);
                    break;
                case eSignalRMsgType.eEvent:
                    // 이벤트 갱신
                    //await g.main_window.updateEvent(msg.event_hist_id);
                    g.left_tree_handler.process_general_event(msg.event_code);
                    break;
                case eSignalRMsgType.eMessage:
                    // 기타 메시지 -> 네트웍 스위치 스캔 , 사용자 로그인 등 일반 메시지 처리 방법 필요 romee 2015.11.04 
                    Console.WriteLine("General Message, Message={0}", eSignalRMsgType.eMessage);
                    break;
                case eSignalRMsgType.eTerminalStatus:
                    // 터미널(PC) 전원 On/Off 상태
                    g.left_tree_handler.process_change_terminal_status(msg.outlet_asset_id, msg.outlet_port_no, msg.terminal_asset_id, msg.on_off);
                    break;
                case eSignalRMsgType.eAddRemoveTerminal:
                    // 터미널 추가/이동/삭제
                    await g.left_tree_handler.process_add_remove_terminal(msg.old_outlet_asset_id, msg.old_outlet_port_no, 
                        msg.outlet_asset_id, msg.outlet_port_no, msg.terminal_asset_id, msg.terminal_action);
                    break;
                case eSignalRMsgType.eAsset:
                    // 클라이언트 사용자가 자산을 추가한 경우
                    if (msg.sender_pc_name != System.Environment.MachineName)
                        await g.left_tree_handler.process_add_remove_asset(msg.asset_id, msg.action);
                    break;
                case eSignalRMsgType.eLocation:
                    // 클라이언트 사용자가 자산을 추가한 경우
                    if (msg.sender_pc_name != System.Environment.MachineName)
                        await g.left_tree_handler.process_add_remove_location(msg.location_id, msg.action);
                    break;
                case eSignalRMsgType.eIcFwUpgradeResult:
                    if (msg.sender_pc_name != System.Environment.MachineName)
                        await g.left_tree_handler.process_fw_upgrade_result(msg.ic_asset_id, msg.fw_upgrade_result);
                    break;
            }
            return true;
        }
        // 자산 변경시 서버로 시그날 전송 
        public void send_asset_to_signalr(int asset_id, eAction action)
        {
            SignalRMsg msg = new SignalRMsg()
            {
                type = eSignalRMsgType.eAsset,
                sender_pc_name = System.Environment.MachineName,
                asset_id = asset_id,
                action = action,
                message = string.Format("asset_id={0}, action={1}", asset_id, action)
            };
            if (_chat == null)
                return;
            send(msg);
        }
        // 로케이션 변경시 서버로 시그날 전송  
        public void send_location_to_signalr(int location_id, eAction action)
        {
            SignalRMsg msg = new SignalRMsg()
            {
                type = eSignalRMsgType.eLocation,
                sender_pc_name = System.Environment.MachineName,
                location_id = location_id,
                action = action,
                message = string.Format("location_id={0}, action={1}", location_id, action)
            };
            if (_chat == null)
                return;
            send(msg);
        }
        // 지능형 장치 상태 변경시 서버로 시그날 전송 
        public void send_simple_link_info_to_signalr(int asset_id, int port_no, string plug_side, int asset_id2, int port_no2, string plug_side2, int cable_catalog_id, bool connect_flag)
        {
            SignalRMsg msg = new SignalRMsg()
            {
                type = eSignalRMsgType.eSimpleLinkInfo,
                sender_pc_name = System.Environment.MachineName,
                asset_id = asset_id,
                port_no = port_no,
                plug_side = plug_side,
                asset_id2 = asset_id2,
                port_no2 = port_no2,
                plug_side2 = plug_side2,
                cable_catalog_id = cable_catalog_id,
                on_off = connect_flag,
                message = string.Format("asset_id={0}, port_no={1}, plug_side={2}, asset_id2={3}, port_no2={4}, plug_side2={5}, cable_catalog_id={6}, connect_flag={7}", 
                    asset_id, port_no, plug_side, asset_id2, port_no2, plug_side2, cable_catalog_id, connect_flag)
            };
            if (_chat == null)
                return;
            send(msg);
        }

    }
}