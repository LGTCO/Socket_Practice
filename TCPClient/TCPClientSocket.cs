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
    internal class TCPClientSocket:IDisposable
    {

        public TCPClientSocket(IPEndPoint iPEndPoint, Action<string> callBack, uint bufferLen)
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
            _clientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            _clientSocket.Connect(this.Address,this.Port);

            if (!_clientSocket.Connected)
            {
                ConnectError?.Invoke();
                return;
            }
                

            IsConnect = true;
            OnConnected?.Invoke();

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
                if (msg.Equals("#PLEASECLOSE"))
                {
                    this.Close();
                }
                OnReceiveMsg?.Invoke($"{Address}:{Port}：{msg}");
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
            SendData("#CLOSE-CLIENT"); 
            _clientSocket.Disconnect(true);
            _clientSocket.Close();
            IsConnect = false;
            Dispose();
        }

        public void Dispose()
        {
            _clientSocket?.Dispose();
        }

        public Action OnConnected { get; set; }
        public Action ConnectError { get; set; }
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
