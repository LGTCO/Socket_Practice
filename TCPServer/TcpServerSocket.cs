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
    internal class TcpServerSocket : Subject<SocketClientHandle>
    {

        /// <summary>
        /// 同步Socket TCP服务器
        /// </summary>
        /// <param name="listenPort">监听的端口</param>
        public TcpServerSocket(int listenPort)
            : this(IPAddress.Any, listenPort)
        {
        }

        /// <summary>
        /// 同步Socket TCP服务器
        /// </summary>
        /// <param name="localEP">监听的终结点</param>
        public TcpServerSocket(IPEndPoint localEP)
            : this(localEP.Address, localEP.Port)
        {
        }

        /// <summary>
        /// 同步Socket TCP服务器
        /// </summary>
        /// <param name="localIPAddress">监听的IP地址</param>
        /// <param name="listenPort">监听的端口</param>
        public TcpServerSocket(IPAddress localIPAddress, int listenPort)
        {
            this.IpAddress = localIPAddress;
            this.Port = listenPort;

            ObserverList = _observers;
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
            StartListen?.Invoke();
            _serverSocket.Listen(16);
            while (IsRunning)
            {
                var clientSocket = _serverSocket.Accept();
                
                var handle = new SocketClientHandle(clientSocket, OnReceiveData)
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
        /// <summary>
        /// 向某个客户端发送消息
        /// </summary>
        /// <param name="endPoint">终端号</param>
        /// <param name="message">发送消息的内容</param>
        /// <param name="callBack">消息发送完成后的回调函数</param>
        public void SendMessageToOne(string endPoint,string message,Action<EndPoint> callBack)
        {
            var handle = _observers.SingleOrDefault(o => o.TheEndPoint.ToString() == endPoint);
            if(handle == null)
                return;
            if (handle.SendCallBack == null)
            {
                handle.SendCallBack = callBack;
            }

            var thread = new Thread(handle.SendData);
            thread.Start($"{IpAddress}:{Port}：{message}");
        }
        /// <summary>
        /// 向所有连接的客户端发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        public void SendMessageToAll(string message)
        {
            _observers.ForEach(o =>
            {
                ThreadPool.UnsafeQueueUserWorkItem(o.SendData,$"{IpAddress}:{Port}：{message}");
            });
        }

        public void Close()
        {
            _serverSocket.Close();
            _serverSocket.Dispose();
        }
        //public override void Dettach(SocketClientHandle t)
        //{
        //    base.Dettach(t);
        //    t.Dispose();
        //}
        
        /// <summary>
        /// 服务器开始监听时执行
        /// </summary>
        public Action StartListen { get; set; }
        /// <summary>
        /// 有客户端连接后执行
        /// </summary>
        public Action<EndPoint> ClientConnect { get; set; }
        /// <summary>
        /// 接收消息后执行
        /// </summary>
        public Action<string> ReceiveMsg { get; set; }

        public List<SocketClientHandle> ObserverList { get; }
        public bool IsRunning { get; private set; }
        public IPAddress IpAddress { get; set; }
        public int Port { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        private readonly Socket _serverSocket;
    }
}
