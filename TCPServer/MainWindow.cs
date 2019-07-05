using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPServer
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            _serverSocket.Close();
        }

        private void Btn_StartMonitor_Click(object sender, EventArgs e)
        {

            if(!IPAddress.TryParse(Txt_IP.Text,out var address))
                return;
            if(!int.TryParse(Txt_Port.Text,out var port)
                || port < 0 || port > 65535)
                return;
            if(_serverSocket!=null
               &&address.Equals(_serverSocket.IpAddress) 
               && port == _serverSocket.Port)
                return;

            listBox_Info.Items.Clear();
            _serverSocket = new TcpServerSocket(new IPEndPoint(address,port));
            _serverSocket.ClientConnect += OnClientConnect;
            _serverSocket.ReceiveMsg += ReceiveMsg;
            _serverSocket.StartListen += StartListen;
            _serverSocket.Start();
        }

       


        private void Btn_Send_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Txt_SendContent.Text))
                return;
            if (EndPointBox.SelectedItem != null)
            {
                _serverSocket.SendMessageToOne(EndPointBox.SelectedItem.ToString(),Txt_SendContent.Text, SendCallBack);
            }
            else
            {
               _serverSocket.SendMessageToAll(Txt_SendContent.Text);
            }

        }
        private void StartListen()
        {
            AddItemToInfoList("开始监听");
        }
        private void SendCallBack(EndPoint obj)
        {
            AddItemToInfoList($"已向{obj}发送");
        }

        private void OnClientConnect(EndPoint endPoint)
        {
            AddItemToInfoList($"{endPoint}\t已连接");
            AddEndPointItem(endPoint.ToString());
        }

        private void ReceiveMsg(string msg)
        {
            AddItemToInfoList(msg);
        }

        private void AddEndPointItem(string endPoint)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(AddEndPointItem), endPoint);
                return;
            }
            EndPointBox.Items.Add(endPoint);
        }

        private void AddItemToInfoList(string info)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(AddItemToInfoList), info);
                return;
            }

            listBox_Info.Items.Add(info);
            if (listBox_Info.Items.Count > 5)
            {
                listBox_Info.TopIndex = listBox_Info.Items.Count - 1;
            }
        }

        private TcpServerSocket _serverSocket;

     
    }
}
