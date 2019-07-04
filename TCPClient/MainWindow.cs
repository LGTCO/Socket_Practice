using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPClient
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            _clientSocket.Close();
        }

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            if (!IPAddress.TryParse(Txt_IP.Text, out var address))
                return;
            if (!int.TryParse(Txt_Port.Text, out var port)
                || port < 0 || port > 65535)
                return;
            if (_clientSocket != null
                && address.Equals(_clientSocket.Address)
                && port == _clientSocket.Port)
                return;

            _clientSocket = new TCPClientSocket(new IPEndPoint(address, port), SendMsgCallBack, 1024 * 1024 * 2);
            _clientSocket.OnReceiveMsg += OnReceiveMsg;
            _clientSocket.Connect();

        }
        private void Btn_Send_Click(object sender, EventArgs e)
        {
            if(!_clientSocket.IsConnect
               || string.IsNullOrEmpty(Txt_SendContent.Text))
                return;

            var thread = new Thread(_clientSocket.SendData);
            thread.Start(Txt_SendContent.Text);
        }

        private void OnReceiveMsg(string msg)
        {
           Show(msg);
        }

        private void SendMsgCallBack(string msg)
        {
            Show(msg+"\t已发送");
        }

        private void Show(object msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(Show),msg);
                return;
            }

            ListBox_Info.Items.Add(msg);
        }


        private TCPClientSocket _clientSocket;

    }
}
