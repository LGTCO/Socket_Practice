using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPClient
{
    internal class TcpClientSocket:IDisposable
    {

        public TcpClientSocket(IPEndPoint iPEndPoint, Action<string> callBack, uint bufferLen)
        {
            this.Address = iPEndPoint.Address;
            this.Port = iPEndPoint.Port;
            _sendMsgCallBack = callBack;
            this.BufferLen = bufferLen;
        }

        public void Connect()
        {
            if(IsConnect)
                return;
            try
            {
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _clientSocket.Connect(this.Address, this.Port);

            }
            catch 
            {
                ConnectError?.Invoke("连接异常");
                return;
            }
            if (!_clientSocket.Connected)
            {
                ConnectError?.Invoke("未能连接成功");
                return;
            }
                

            IsConnect = true;
           // OnConnected?.Invoke();

            var thread = new Thread(ReceiveData);
            thread.Start();

        }

        public void ReceiveData()
        {
            while (IsConnect)
            {
                var buffer = new byte[BufferLen];
                var len = _clientSocket.Receive(buffer);

                if (len <= 0) break;

                var msg = this.Encoding.GetString(buffer, 0, len);
                OnReceiveMsg?.Invoke(msg);
            }
        }

        public void SendData(object msg)
        {
            if (!(msg is string strMsg)) return;

            var buffer = this.Encoding.GetBytes(strMsg);
            _clientSocket.Send(buffer);
            _sendMsgCallBack?.Invoke(strMsg);
        }

        public void Close()
        {
            _clientSocket.Close();
            _clientSocket.Dispose();
        }

        public void Dispose()
        {
            _clientSocket?.Dispose();
        }

        //public Action OnConnected { get; set; }
        public Action<string> ConnectError { get; set; }
        public Action<string> OnReceiveMsg { get; set; }
        public Action CloseSocket { get; set; }
        private Action<string> _sendMsgCallBack { get; set; }

        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public bool IsConnect { get; set; }

        public uint BufferLen
        {
            get
            {
                return _bufferLen;
            }
            set
            {
                if (value > 1024 * 1024 * 5)
                {
                    value = 1024 * 1024 * 5;
                }

                _bufferLen = value;
            }
        }

        private uint _bufferLen;
        private Socket _clientSocket;

       
    }
}
