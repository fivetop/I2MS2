// Asynchronous Server Socket Example
// http://msdn.microsoft.com/en-us/library/fx6588te.aspx

using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WebApi.Models;
using System.Linq;
using System.Collections.Generic;

 
namespace NmsSimulator
{

    // State object for reading client data asynchronously
    public class StateObject {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
    // Received data string.
        public StringBuilder sb = new StringBuilder();  
    }
 
    public class TcpServer {
        // Thread signal.
        public ManualResetEvent allDone = new ManualResetEvent(false);
        //static int _listen_port = 0;

        public StateObject state_object = null;

        public TcpData _tcp_data = null;
 
        public TcpServer() 
        {
        }

        public void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];
 
            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            // running the listener is "host.contoso.com".
            // IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            // IPAddress ipAddress = ipHostInfo.AddressList[3];
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5200);
 
            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp );
 
            // Bind the socket to the local endpoint and listen for incoming connections.
            try {
                listener.Bind(localEndPoint);
                listener.Listen(100);
 
                while (true) {
                    // Set the event to nonsignaled state.
                    allDone.Reset();
 
                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept( 
                        new AsyncCallback(AcceptCallback),
                        listener );
 
                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }
 
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
 
            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        
        }
 
        public void AcceptCallback(IAsyncResult ar) {
            // Signal the main thread to continue.
            allDone.Set();
 
            // Get the socket that handles the client request.
            Socket listener = (Socket) ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
 
            // Create the state object.
            StateObject state = new StateObject();
            state_object = state;
            state.workSocket = handler;
            handler.BeginReceive( state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        private string received_data = "";
        public void ReadCallback(IAsyncResult ar) {
            String content = String.Empty;
        
            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject) ar.AsyncState;
            Socket handler = state.workSocket;

            try
            {
                // Read data from the client socket. 
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Check for end-of-file tag. If it is not there, read 
                    // more data.
                    content = state.sb.ToString();

                    received_data += content;
                    state.sb.Clear();

                    Debug.WriteLine("TCPServer: Received TCP message: " + content);

                    string remained = "";
                    string trimed = "";
                    while (true)
                    {
                        bool b = TcpDataTrim.trim_token(received_data, out trimed, out remained);
                        if (b)
                        {
                            received_data = remained;
                            Debug.WriteLine("Trimed TCP message: " + trimed);
                            _tcp_data = new TcpData();
                            _tcp_data.parse_data(trimed);
                            process_token();
                        }
                        else
                        {
                            // Not all data received. Get more.
                            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                            break;
                        }
                    }
                
                }
            }
            catch (Exception) { }
        }
    
        public void Send(Socket handler, String data) {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);
 
            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }
 
        private void SendCallback(IAsyncResult ar) {
            try {
                // Retrieve the socket from the state object.
                Socket handler = (Socket) ar.AsyncState;
 
                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                // Console.WriteLine("Sent {0} bytes to client.", bytesSent); 
                // handler.Shutdown(SocketShutdown.Both);
                // handler.Close();
 
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        private void sendTcp(string data)
        {
            string send_data = TcpDataTrim.make_token(data);
            // Debug.WriteLine(data);

            if (state_object != null)
            {
                Socket socket = state_object.workSocket;

                Send(socket, send_data);
            }
        }

        private void process_token()
        {
        
            string send_data = "";
            eTcpCommand tcp_cmd = _tcp_data.tcp_cmd;
            TcpData ans_data = new TcpData();
            switch (tcp_cmd)
            {
                case eTcpCommand.eCIR:
                    send_data = ans_data.RET("CIR", 0);
                    sendTcp(send_data);
                    break;
                case eTcpCommand.eCON:
                    send_all_status(_tcp_data.sys_id);
                    send_data = ans_data.RET("CON", 0);
                    sendTcp(send_data);
                    break;
            }
        }

        public static i2ms2Entities _db = new i2ms2Entities();

        // CCR, PPR, POR, COR 이벤트 전송...
        private void send_all_status(int sys_id)
        {
            _ic_ipp_config_list = _db.ic_ipp_config.ToList();
            _asset_ipp_port_link_list = _db.asset_ipp_port_link.ToList();
            _asset_aux_list = _db.asset_aux.ToList();

            TcpData ans_data1 = new TcpData();
            string send_data1 = ans_data1.CCR(sys_id);
            sendTcp(send_data1);
            TcpData ans_data2 = new TcpData();
            string send_data2 = PPR(_db, ans_data2, sys_id);
            sendTcp(send_data2);
            TcpData ans_data3 = new TcpData();
            string send_data3 = POR(_db, ans_data3, sys_id);
            sendTcp(send_data3);
            TcpData ans_data4 = new TcpData();
            string send_data4 = COR(_db, ans_data4, sys_id);
            sendTcp(send_data4);
        }


        public string PPR(i2ms2Entities _db, TcpData d, int _sys_id)
        {
            d.sys_id = _sys_id;
            int ic_asset_id = 0;
            try
            {
                var find = _db.asset.Where(p => p.asset_name == "IC-101").ToList();
                ic_asset_id = find.Count > 0 ? find[0].asset_id : 0;
            }
            catch (Exception e)
            {
                Debug.WriteLine(string.Format("Exception Error: code={0}, msg={1}"), e.HResult, e.Message);
                return "";
            }

            List<PPData> pp_list = new List<PPData>();

            int i = 0;
            for (i = 1; i <= 40; i++)
            {
                PPData pp = new PPData();
                pp.sys_id = d.sys_id;
                pp.pp_id = i;
                pp.total_port = 24;
                pp.pp_mode = ePPMode.eXC;
                pp.active_port = 0;
                pp.deactive_port = 0;
                pp.pp_type = 1;    // 1=basic, 2=extend
                pp_list.Add(pp);
            }

            // 구성 여부를 보내줄 필요는 없음.
            // var list = _db.ic_ipp_config.Where(p => (p.ic_asset_id == ic_asset_id) && (p.ipp_asset_id > 0));
            string send_data = "PPR,";
            int idx = 0;
            foreach (var node in pp_list)
            {
                send_data += string.Format("[{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}]\r\n", ++idx, node.sys_id, node.pp_id, "a", node.pp_type, node.total_port, node.active_port, node.deactive_port, 1, (node.pp_mode == ePPMode.eIC ? 1 : 2));
            }
            return send_data;
        }

        List<ic_ipp_config> _ic_ipp_config_list = null;
        List<asset_ipp_port_link> _asset_ipp_port_link_list = null;
        List<asset_aux> _asset_aux_list = null;

        public string POR(i2ms2Entities _db, TcpData d, int _sys_id)
        {
            d.sys_id = _sys_id;
            var find = _db.asset.Where(p => p.asset_name == "IC-101").ToList();
            int ic_asset_id = find.Count > 0 ? find[0].asset_id : 0;
            int idx = 0;
            int i = 0;
            int j = 0;
            List<PortData> port_data_list = new List<PortData>();
            for (i = 1; i <= 40; i++)
            {

                for (j = 1; j <= 24; j++)
                {
                    PortData port = new PortData();
                    port.sys_id = _sys_id;
                    port.pp_id = i;
                    port.port_no = j;
                    port_data_list.Add(port);
                }
            }

            var list = _ic_ipp_config_list.Where(p => (p.ic_asset_id == ic_asset_id) && (p.ipp_asset_id > 0));
            foreach (var node in list)
            {
                int ipp_asset_id = node.ipp_asset_id ?? 0;
                int pp_id = node.ipp_connect_no;
                var list2 = _asset_ipp_port_link_list.Where(p => p.ipp_asset_id == ipp_asset_id);
                foreach (var node2 in list2)
                {
                    var find2 = port_data_list.Find(p => (p.pp_id == pp_id) && (p.port_no == node2.port_no));
                    if (find2 != null)
                    {
                        string status = node2.ipp_port_status;
                        switch (status)
                        {
                            case "P":
                                find2.port_status = ePortStatus.Plugged;
                                break;
                            case "U":
                                find2.port_status = ePortStatus.Unplugged;
                                break;
                            case "L":
                                find2.port_status = ePortStatus.Linked;
                                break;
                            default:
                                find2.port_status = ePortStatus.Unknown;
                                break;
                        }
                    }
                }
            }

            string send_data = "POR,";
            foreach (var node in port_data_list)
            {
                int status = 0;
                switch (node.port_status)
                {
                    case ePortStatus.Plugged :
                            status = 1;
                            break;
                    case ePortStatus.Unplugged :
                            status = 2;
                            break;
                    case ePortStatus.Linked :
                            status = 3;
                            break;
                }
                send_data += string.Format("[{0},{1},{2},{3},{4},{5},{6}]\r\n", ++idx, node.sys_id, node.pp_id, node.port_no, status, 0, 0);
            }
            return send_data;
        }


        public string COR(i2ms2Entities _db, TcpData d, int _sys_id)
        {

            d.sys_id = _sys_id;
            var find = _db.asset.Where(p => p.asset_name == "IC-101").ToList();
            int ic_asset_id = find.Count > 0 ? find[0].asset_id : 0;
            int idx = 0;
            int i = 0;
            int j = 0;
            List<PortData> port_data_list = new List<PortData>();
            for (i = 1; i <= 40; i++)
            {
                for (j = 1; j <= 24; j++)
                {
                    PortData port = new PortData();
                    port.sys_id = _sys_id;
                    port.pp_id = i;
                    port.port_no = j;
                    port_data_list.Add(port);
                }
            }

            var list = _ic_ipp_config_list.Where(p => (p.ic_asset_id == ic_asset_id) && (p.ipp_asset_id > 0));
            foreach (var node in list)
            {
                int ipp_asset_id = node.ipp_asset_id ?? 0;
                int pp_id = node.ipp_connect_no;
                var list2 = _asset_ipp_port_link_list.Where(p => p.ipp_asset_id == ipp_asset_id);
                foreach (var node2 in list2)
                {
                    var find2 = port_data_list.Find(p => (p.pp_id == pp_id) && (p.port_no == node2.port_no));
                    if (find2 != null)
                    {
                        int remote_ic_asset_id = node2.remote_ic_asset_id ?? 0;
                        var au = _asset_aux_list.Find(p => p.asset_id == remote_ic_asset_id);
                        find2.remote_sys_id = au != null ? (au.ic_con_id ?? 0) : 0;
                        int remote_pp_asset_id = node2.remote_pp_asset_id ?? 0;
                        if (remote_pp_asset_id > 0)
                        {
                            var f = _ic_ipp_config_list.Find(p => p.ipp_asset_id == remote_pp_asset_id);
                            find2.remote_pp_id = f.ipp_connect_no;
                        }
                        else
                            find2.remote_pp_id = 0;
                        find2.remote_port_no = node2.remote_port_no ?? 0;
                    }
                }
            }

            string send_data = "COR,";
            foreach (var node in port_data_list)
            {
                send_data += string.Format("[{0},{1},{2},{3},{4},{5},{6},{7}]\r\n", ++idx, idx, node.sys_id, node.pp_id, node.port_no, node.remote_sys_id, node.remote_pp_id, node.remote_port_no);
            }
            return send_data;
        } 
    }
}