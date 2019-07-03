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
    internal class TcpSocket:Subject<SocketClientHandle>
    {

        public TcpSocket(int listenPort)
            : this(IPAddress.Any, listenPort, 1024)
        {
        }

        /// <summary>
        /// 同步Socket TCP服务器
        /// </summary>
        /// <param name="localEP">监听的终结点</param>
        public TcpSocket(IPEndPoint localEP)
            : this(localEP.Address, localEP.Port, 1024)
        {
        }

        /// <summary>
        /// 同步Socket TCP服务器
        /// </summary>
        /// <param name="localIPAddress">监听的IP地址</param>
        /// <param name="listenPort">监听的端口</param>
        /// <param name="maxClient">最大客户端数量</param>
        public TcpSocket(IPAddress localIPAddress, int listenPort, int maxClient)
        {
            this.IpAddress = localIPAddress;
            this.Port = listenPort;

            _maxClient = maxClient;
            _serverSocket = new Socket(localIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (IsRunning)
                return;
            ;
            IsRunning = true;
            _serverSocket.Listen(16);
            _serverSocket.Bind(new IPEndPoint(this.IpAddress, this.Port));
            var thread = new Thread(Listen);
            thread.Start();
        }

        private void Listen()
        {
            while (IsRunning)
            {
                var clientSocket = _serverSocket.Accept();
                _clientCount++;
                var handle = new SocketClientHandle(clientSocket)
                {
                    IsConnect = true,
                };
                this.Attach(handle);
                this.Notify();
                ThreadPool.QueueUserWorkItem(new WaitCallback(handle.RecevieData));
            }
        }
        

        public bool IsRunning { get; private set; }
        public IPAddress IpAddress { get; set; }
        public int Port { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        private readonly Socket _serverSocket;
        private int _maxClient;
        private int _clientCount = 0;

    }
}
