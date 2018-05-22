using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketServer
{
    class Program
    {
        static List<Socket> connectionSocketList = new List<Socket>();
        static byte[] receivedDataBuffer;

        static void Main(string[] args)
        {
            Socket Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            //绑定地址  端口
            Listener.Bind(new IPEndPoint(getLocalmachineIPAddress(), 81));

            //监听的最大长度
            Listener.Listen(10);

            Logger.Log(string.Format("聊天服务器启动。监听地址：{0}, 端口：{1}", getLocalmachineIPAddress(), 81));
            Logger.Log(string.Format("WebSocket服务器地址: ws://{0}:{1}/chat", getLocalmachineIPAddress(), 81));
            while (true)
            {
                Socket sc = Listener.Accept();////监听获取客户端tcp请求 阻塞
                if (sc != null)
                {
                    System.Threading.Thread.Sleep(100);
                    Socket client = sc;

                    connectionSocketList.Add(sc);

                    //广播消息
                    string message = "欢迎【{0}】来到聊天室！";

                    Send(message);


                    //client.BeginReceive(receivedDataBuffer,
                    //                                         0, receivedDataBuffer.Length,
                    //                                         0, new AsyncCallback(ManageHandshake),
                    //                                        client.Available);
                    //
                }
            }
        }

        public static void Send(string message)
        {
            foreach (Socket item in connectionSocketList)
            {
                if (!item.Connected) return;
                try
                {
                    //item.Send(FirstByte);
                    item.Send(Encoding.UTF8.GetBytes(message));
                    //item.Send(LastByte);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex.Message);
                }
            }
        }




        public static IPAddress getLocalmachineIPAddress()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            foreach (IPAddress ip in ipEntry.AddressList)
            {
                //IPV4
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip;
            }

            return ipEntry.AddressList[0];
        }

        /// <summary>
        /// 日志类
        /// </summary>
        public static class Logger
        {
            public static void Log(string Text)
            {
                Console.WriteLine(Text);
            }
        }
    }
}
