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

        public SocketClientHandle(Socket client,Action<string> receiveData,Action<EndPoint> RemoveSelf)
        {
            this._client = client;
            _isConnect = true;
            _receiveBuffer = new byte[1024 * 1024 * 2];
            _receiveData = receiveData;
            _removeSelf = RemoveSelf;
        }

        #region Method
        /// <summary>
        /// 接受来自客户端发来的数据
        /// </summary>
        public void ReceiveData(object state)
        {
           
            while (_isConnect)
            {
                try
                {
                    var len = _client.Receive(_receiveBuffer);
                    if (len <= 0)
                        break;
                    
                    var str = this.Encoding.GetString(_receiveBuffer, 0, len);
                    if (str.Equals("#CLOSECLIENT"))
                    {
                        _removeSelf?.Invoke(TheEndPoint);
                    }
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

        #endregion


        #region 委托
        private Action<EndPoint> _removeSelf;
        private Action<string> _receiveData;
        public Action<EndPoint> SendCallBack { get; set; }
        
        #endregion


        public override void Update(string endPoint)
        {
            ThreadPool.UnsafeQueueUserWorkItem(SendData, $"{endPoint} 已连接至服务器");
        }

        #region 释放
        public void Dispose()
        {
            _client?.Disconnect(false);
            _client?.Dispose();
        }

        ~SocketClientHandle()
        {
            Dispose();
        }
        #endregion

        public EndPoint TheEndPoint { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 与客户端相关联的socket
        /// </summary>
        private Socket _client;

        /// <summary>
        /// 标识是否与客户端相连接
        /// </summary>
        private bool _isConnect;
        public bool IsConnect
        {
            get { return _isConnect; }
            set { _isConnect = value; }
        }

        /// <summary>
        /// 数据接受缓冲区
        /// </summary>
        private readonly byte[] _receiveBuffer;
        
    }
}
