using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Diagnostics;
using System.Net.Sockets;
using WebApi.Models;
using WebApi;

namespace NmsSimulator
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool a4 = false;
        private bool b4 = false;
        TcpServer _tcp_server = new TcpServer();
        public MainWindow()
        {
            // TcpServer.StartListening();
            InitializeComponent();

            //-----------------------------------------------------------------------------------------
            // Step 1 : TCP Server Thread
            //-----------------------------------------------------------------------------------------

            Thread t1 = new Thread(new ThreadStart(_tcp_server.StartListening));
            t1.Start();
        }

        private void _btnStatus1_Click(object sender, RoutedEventArgs e)
        {
            if (!a4)
            {
                a4 = true;
                if (b4)
                {
                    TcpData trap_data = new TcpData();
                    string send_data = trap_data.PLT(1, 1, 101, 1, 4, 101, 2, 4);
                    sendTcp(send_data);
                    send_data = trap_data.LIT(1, 1, 101, 1, 4, 101, 2, 4);
                    sendTcp(send_data);
                    send_data = trap_data.LIT(1, 1, 101, 2, 4, 101, 1, 4);
                    sendTcp(send_data);
                }
                else
                {
                    TcpData trap_data = new TcpData();
                    string send_data = trap_data.PLT(1, 1, 101, 1, 4, 0, 0, 0);
                    sendTcp(send_data);
                }
            }
        }
        private void _btnStatus2_Click(object sender, RoutedEventArgs e)
        {
            if (a4)
            {
                a4 = false;
                TcpData trap_data = new TcpData();
                string send_data = trap_data.UPL(1, 1, 101, 1, 4, 0, 0, 0);
                sendTcp(send_data);
                if (b4)
                {
                    send_data = trap_data.ULT(1, 1, 101, 1, 4, 0, 0, 0);
                    sendTcp(send_data);
                    send_data = trap_data.ULT(1, 1, 101, 2, 4, 0, 0, 0);
                    sendTcp(send_data);
                }
            }
        }
        private void _btnStatus3_Click(object sender, RoutedEventArgs e)
        {
            if (!b4)
            {
                b4 = true;
                if (a4)
                {
                    TcpData trap_data = new TcpData();
                    string send_data = trap_data.PLT(1, 1, 101, 2, 4, 101, 1, 4);
                    sendTcp(send_data);
                    send_data = trap_data.LIT(1, 1, 101, 2, 4, 101, 1, 4);
                    sendTcp(send_data);
                    send_data = trap_data.LIT(1, 1, 101, 1, 4, 101, 2, 4);
                    sendTcp(send_data);
                }
                else
                {
                    TcpData trap_data = new TcpData();
                    string send_data = trap_data.PLT(1, 1, 101, 2, 4, 0, 0, 0);
                    sendTcp(send_data);
                }
            }
        }
        private void _btnStatus4_Click(object sender, RoutedEventArgs e)
        {
            if (b4)
            {
                b4 = false;
                TcpData trap_data = new TcpData();
                string send_data = trap_data.UPL(1, 1, 101, 2, 4, 0, 0, 0);
                sendTcp(send_data);
                if (a4)
                {
                    send_data = trap_data.ULT(1, 1, 101, 2, 4, 0, 0, 0);
                    sendTcp(send_data);
                    send_data = trap_data.ULT(1, 1, 101, 1, 4, 0, 0, 0);
                    sendTcp(send_data);
                }
            }
        }

        public void sendTcp(string data)
        {
            Log.info("TCPClient: Send TCP message: " + data);
            string send_data = TcpDataTrim.make_token(data);

            if (_tcp_server.state_object != null)
            {
                Socket socket = _tcp_server.state_object.workSocket;

                _tcp_server.Send(socket, send_data);
            }
        }

        private void _chkA_Click(object sender, RoutedEventArgs e)
        {
            a4 = _chkA.IsChecked.Value;
        }
        private void _chkB_Click(object sender, RoutedEventArgs e)
        {
            b4 = _chkB.IsChecked.Value;
        }
    }
}
