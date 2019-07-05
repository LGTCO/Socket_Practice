using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCPServer.Base;

namespace TCPServer
{
    internal class SocketClientHandle:Observer,IDisposable
    {

        public SocketClientHandle(Socket client,Action<string> receiveData)
        {
            this._client = client;
            IsConnect = true;
            _receiveBuffer = new byte[1024 * 1024 * 2];
            _receiveData = receiveData;
        }

     
        /// <summary>
        /// 接受来自客户端发来的数据
        /// </summary>
        public void ReceiveData(object state)
        {
           
            while (IsConnect)
            {
                try
                {
                    var len = _client.Receive(_receiveBuffer);
                    if (len <= 0)
                        break;
                    
                    var str = this.Encoding.GetString(_receiveBuffer, 0, len);
                    _receiveData?.Invoke($"接收来自{TheEndPoint}的消息\t：{str}");
                }
                catch 
                {
                }
            }
        }

        /// <summary>
        /// 向客户端发送数据
        /// </summary>
        public void SendData(object msg)
        {
            var data = this.Encoding.GetBytes((string)msg);
            try
            {
                _client.Send(data);
                SendCallBack?.Invoke(TheEndPoint);
            }
            catch { }
        }
    
        public override void Update(string endPoint)
        {
            ThreadPool.UnsafeQueueUserWorkItem(SendData, $"{endPoint} 已连接至服务器");
        }

        public void Dispose()
        {
            _client?.Disconnect(false);
            _client?.Dispose();
        }

        ~SocketClientHandle()
        {
            Dispose();
        }
        /// <summary>
        /// 接收到消息后执行
        /// </summary>
        private Action<string> _receiveData;
        /// <summary>
        /// 发送消息后执行
        /// </summary>
        public Action<EndPoint> SendCallBack { get; set; }
        

        public EndPoint TheEndPoint { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 与客户端相关联的socket
        /// </summary>
        private Socket _client;
        public bool IsConnect { get; set; }

        /// <summary>
        /// 数据接受缓冲区
        /// </summary>
        private readonly byte[] _receiveBuffer;
        
    }
}
