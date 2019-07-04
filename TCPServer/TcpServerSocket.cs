using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TCPServer.Base; 

namespace TCPServer
{
    internal class TcpServerSocket:Subject<SocketClientHandle>
    {

        public TcpServerSocket(int listenPort)
            : this(IPAddress.Any, listenPort, 1024)
        {
        }

        /// <summary>
        /// 同步Socket TCP服务器
        /// </summary>
        /// <param name="localEP">监听的终结点</param>
        public TcpServerSocket(IPEndPoint localEP)
            : this(localEP.Address, localEP.Port, 1024)
        {
        }

        /// <summary>
        /// 同步Socket TCP服务器
        /// </summary>
        /// <param name="localIPAddress">监听的IP地址</param>
        /// <param name="listenPort">监听的端口</param>
        /// <param name="maxClient">最大客户端数量</param>
        public TcpServerSocket(IPAddress localIPAddress, int listenPort, int maxClient)
        {
            this.IpAddress = localIPAddress;
            this.Port = listenPort;

            ObserverList = _observers;
            _maxClient = maxClient;
            _serverSocket = new Socket(localIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public SocketClientHandle GetClientHandle(string endPoint)
        {
            return ObserverList.SingleOrDefault(c => c.TheEndPoint.ToString() == endPoint);
        }
        

        public void Start()
        {
            if (IsRunning)
                return;
            
            IsRunning = true;
            
            _serverSocket.Bind(new IPEndPoint(this.IpAddress, this.Port));
            var thread = new Thread(Listen);
            thread.Start();
        }

        private void Listen()
        {
            _serverSocket.Listen(16);
            while (IsRunning)
            {
                var clientSocket = _serverSocket.Accept();
                _clientCount++;
                var handle = new SocketClientHandle(clientSocket, OnReceiveData, RemoveClient)
                {
                    IsConnect = true,
                    TheEndPoint = clientSocket.RemoteEndPoint,
                };
                this.Attach(handle);
                this.Notify(handle.TheEndPoint.ToString());
                this.ClientConnect?.Invoke(clientSocket.RemoteEndPoint);
                ThreadPool.QueueUserWorkItem(new WaitCallback(handle.ReceiveData));
            }
        }

        private void OnReceiveData(string msg)
        {
            ReceiveMsg?.Invoke(msg);
        }


        public override void Dettach(SocketClientHandle t)
        {
            
            t.SendData("#PLEASECLOSE");
            base.Dettach(t);
            t.Dispose();
        }

        public Action<EndPoint> RemoveClient { get; set; }
        public Action<EndPoint> ClientConnect { get; set; }
        public Action<string> ReceiveMsg { get; set; }

        public List<SocketClientHandle> ObserverList { get; }
        public bool IsRunning { get; private set; }
        public IPAddress IpAddress { get; set; }
        public int Port { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        private readonly Socket _serverSocket;
        private int _maxClient;
        private int _clientCount = 0;

    }
}
