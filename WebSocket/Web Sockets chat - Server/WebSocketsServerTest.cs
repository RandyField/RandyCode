using System;
using System.IO;
using System.Timers;
namespace WebSocketServer
{
    class WebSocketServerTest : IDisposable
    {
        /// <summary>
        /// websocket服务端
        /// </summary>
        private WebSocketServer WSServer;
        public WebSocketServerTest()
        {
            //使用默认的设置
            WSServer = new WebSocketServer();  
        }

        public void Dispose()
        {
            Close();
        }

        private void Close()
        {
            WSServer.Dispose();
            GC.SuppressFinalize(this);
        }

        ~WebSocketServerTest()
        {
            Close();
        }

        /// <summary>
        /// 服务器启动
        /// </summary>
        public void Start()
        {
            //连接事件
            WSServer.NewConnection += new NewConnectionEventHandler(WSServer_NewConnection);

            //连接断开事件
            WSServer.Disconnected += new DisconnectedEventHandler(WSServer_Disconnected);

            //启动
            WSServer.StartServer();
        }

        //连接断开
        void WSServer_Disconnected(Object sender, EventArgs e)
        {
        }

        //连接方法
        void WSServer_NewConnection(string loginName, EventArgs e)
        {
        }
}
}
