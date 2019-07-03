using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TCPServer.Base;

namespace TCPServer
{
    internal class SocketClientHandle:Observer,IDisposable
    {


      

        public SocketClientHandle(Socket client)
        {
            this._client = client;
            _isConnect = true;
            _receiveBuffer = new byte[1024 * 1024 * 2];
        }

        #region Method
        /// <summary>
        /// 接受来自客户端发来的数据
        /// </summary>
        public void RecevieData(object state)
        {
           
            while (_isConnect)
            {
                try
                {
                    var len = _client.Receive(_receiveBuffer);
                    var str = this.Encoding.GetString(_receiveBuffer, 0, len);
                    OnReceiveMsg?.Invoke(str);
                }
                catch 
                {
                }
            }
        }

        /// <summary>
        /// 向客户端发送数据
        /// </summary>
        public void SendData(string msg)
        {
            byte[] data = this.Encoding.GetBytes(msg);
            try
            {
                _client.Send(data);
            }
            catch { }
        }

        #endregion


        #region 委托

        public Action<string> OnReceiveMsg { get; set; }

        #endregion


        public override void Update()
        {

        }

        #region 释放
        public void Dispose()
        {
            _isConnect = false;
            if (_client != null)
            {
                _client.Close();
                _client = null;
            }
            GC.SuppressFinalize(this);
        }


        #endregion

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
