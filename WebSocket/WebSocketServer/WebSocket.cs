using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebSocketServer
{
    /// <summary>
    /// websocket
    /// </summary>
    public class WebSocket
    {
        /// <summary>
        /// session池
        /// </summary>
        private Dictionary<string, Session> SessionPool = new Dictionary<string, Session>();

        /// <summary>
        /// 消息池
        /// </summary>
        private Dictionary<string, string> MsgPool = new Dictionary<string, string>();

        #region 启动websocket服务
        /// <summary>
        /// 启动websocket 服务器
        /// </summary>
        /// <param name="port"></param>
        public void start(int port)
        {
            //IPv4的地址
            //支持可靠、双向、基于连接的字节流，而不重复数据，也不保留边界。 
            //此类型的 Socket 与单个对方主机通信，并且在通信开始之前需要建立远程主机连接。
            //tcp
            Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //绑定ip与端口
            socketServer.Bind(new IPEndPoint(IPAddress.Any, port));

            //允许连接队列的最大长度
            socketServer.Listen(2);

            //异步监听客户端的tcp连接
            socketServer.BeginAccept(new AsyncCallback(Accept), socketServer);

            //Socket sc = socketServer.Accept();////监听获取客户端tcp请求 阻塞
            Console.WriteLine("服务已启动");
            Console.WriteLine("按任意键关闭服务");
            Console.ReadLine();
        }
        #endregion

        #region 处理客户端连接请求
        /// <summary>
        /// 处理客户端连接请求
        /// </summary>
        /// <param name="socket"></param>
        private void Accept(IAsyncResult socket)
        {
            //还原传入的原始套接字
            Socket socketserver = (Socket)socket.AsyncState;

            //在原始套接字上调用EndAccept方法，放回新的客户端套接字
            Socket socketclient = socketserver.EndAccept(socket);

            //缓存
            byte[] buffer = new byte[4096];

            try
            {
                //异步接收客户端的数据
                socketclient.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(Recieve), socketclient);

                //保存登录的客户端
                Session session = new Session();
                session._socketclient = socketclient;
                session._ip = socketclient.RemoteEndPoint.ToString();
                session._buffer = buffer;
                lock (SessionPool)
                {
                    if (SessionPool.ContainsKey(session._ip))
                    {
                        this.SessionPool.Remove(session._ip);
                    }
                    this.SessionPool.Add(session._ip, session);
                }

                ////异步接收客户端的数据
                //socketclient.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(Recieve), socketclient);

                //准备接受下一个客户端
                socketserver.BeginAccept(new AsyncCallback(Accept), socketserver);

                //打包消息
                byte[] msgBuffer = PackageServerData("start");

                //遍历session池
                foreach (Session se in SessionPool.Values)
                {
                    //广播
                    se._socketclient.Send(msgBuffer, msgBuffer.Length, SocketFlags.None);
                }

                Console.WriteLine(string.Format("Client {0} connected", socketclient.RemoteEndPoint));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.ToString());
            }
        }
        #endregion

        #region 处理接收的数据

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        private void Recieve(IAsyncResult socket)
        {
            Socket socketclient = (Socket)socket.AsyncState;

            //获取客户端ip地址
            string Ip = socketclient.RemoteEndPoint.ToString();

            if (socketclient == null || !SessionPool.ContainsKey(Ip))
            {
                return;
            }
            try
            {
                int length = socketclient.EndReceive(socket);

                //ip为key，session为value
                byte[] buffer = SessionPool[Ip]._buffer;

                socketclient.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(Recieve), socketclient);

                //utf-8解码 为字符串
                string msg = Encoding.UTF8.GetString(buffer, 0, length);

                //websocket建立连接的时候，除了TCP连接的三次握手，websocket协议中客户端与服务器想建立连接需要一次额外的握手动作
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    //判断是否是web
                    if (msg.Contains("Sec-WebSocket-Key"))
                    {
                        //发送给客户端握手信息  除了TCP连接的三次握手，websocket协议中客户端与服务器想建立连接需要一次额外的握手动作
                        socketclient.Send(PackageHandShakeData(buffer, length));
                        SessionPool[Ip]._isweb = true;
                        return;
                    }

                    //处理数据
                    if (SessionPool[Ip]._isweb)
                    {
                        //解析客户端数据
                        msg = AnalyzeClientData(buffer, length);
                        Console.WriteLine(string.Format("Client {0} say:{1}", socketclient.RemoteEndPoint, msg));
                    }

                    //打包消息
                    byte[] msgBuffer = PackageServerData(msg);

                    //遍历session池
                    foreach (Session se in SessionPool.Values)
                    {
                        //广播
                        se._socketclient.Send(msgBuffer, msgBuffer.Length, SocketFlags.None);
                    }
                }
            }
            catch
            {
                socketclient.Disconnect(true);
                Console.WriteLine("客户端 {0} 断开连接", Ip);
                SessionPool.Remove(Ip);
            }
        }

        #endregion

        #region 客户端和服务端的响应
        /*
         * 客户端向服务器发送请求
         * 
         * GET / HTTP/1.1
         * Origin: http://localhost:81
         * Sec-WebSocket-Key: vDyPp55hT1PphRU5OAe2Wg==
         * Connection: Upgrade
         * Upgrade: Websocket
         *Sec-WebSocket-Version: 13
         * User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko
         * Host: localhost:8064
         * DNT: 1
         * Cache-Control: no-cache
         * Cookie: DTRememberName=admin
         * 
         * 服务器给出响应
         * 
         * HTTP/1.1 101 Switching Protocols
         * Upgrade: websocket
         * Connection: Upgrade
         * Sec-WebSocket-Accept: xsOSgr30aKL2GNZKNHKmeT1qYjA=
         * 
         * 在请求中的“Sec-WebSocket-Key”是随机的，服务器端会用这些数据来构造出一个SHA-1的信息摘要。把“Sec-WebSocket-Key”加上一个魔幻字符串
         * “258EAFA5-E914-47DA-95CA-C5AB0DC85B11”。使用 SHA-1 加密，之后进行 BASE-64编码，将结果做为 “Sec-WebSocket-Accept” 头的值，返回给客户端
         */
        #endregion

        #region 打包请求连接数据

        /// <summary>
        /// 打包请求连接数据(WebSocket握手包)
        /// </summary>
        /// <param name="handShakeBytes"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private byte[] PackageHandShakeData(byte[] handShakeBytes, int length)
        {
            string handShakeText = Encoding.UTF8.GetString(handShakeBytes, 0, length);
            string key = string.Empty;
            Regex reg = new Regex(@"Sec\-WebSocket\-Key:(.*?)\r\n");
            Match m = reg.Match(handShakeText);
            if (m.Value != "")
            {
                key = Regex.Replace(m.Value, @"Sec\-WebSocket\-Key:(.*?)\r\n", "$1").Trim();
            }
            byte[] secKeyBytes = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(key + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"));
            string secKey = Convert.ToBase64String(secKeyBytes);
            var responseBuilder = new StringBuilder();
            responseBuilder.Append("HTTP/1.1 101 Switching Protocols" + "\r\n");
            responseBuilder.Append("Upgrade: websocket" + "\r\n");
            responseBuilder.Append("Connection: Upgrade" + "\r\n");
            responseBuilder.Append("Sec-WebSocket-Accept: " + secKey + "\r\n\r\n");
            return Encoding.UTF8.GetBytes(responseBuilder.ToString());
        }
        #endregion

        #region 处理接收的数据
        /// <summary>
        /// 处理接收的数据
        /// 参考 http://www.cnblogs.com/smark/archive/2012/11/26/2789812.html
        /// </summary>
        /// <param name="recBytes"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string AnalyzeClientData(byte[] recBytes, int length)
        {
            int start = 0;

            // 如果有数据则至少包括3位
            if (length < 2)
            {
                return "";
            }

            // 判断是否为结束针
            bool IsEof = (recBytes[start] >> 7) > 0;

            // 暂不处理超过一帧的数据
            if (!IsEof)
            {
                return "";
            }
            start++;

            // 是否包含掩码
            bool hasMask = (recBytes[start] >> 7) > 0;

            // 不包含掩码的暂不处理
            if (!hasMask)
            {
                return "";
            }

            // 获取数据长度
            UInt64 mPackageLength = (UInt64)recBytes[start] & 0x7F;

            start++;

            // 存储4位掩码值
            byte[] Masking_key = new byte[4];

            // 存储数据
            byte[] mDataPackage;

            if (mPackageLength == 126)
            {
                // 等于126 随后的两个字节16位表示数据长度
                mPackageLength = (UInt64)(recBytes[start] << 8 | recBytes[start + 1]);
                start += 2;
            }

            if (mPackageLength == 127)
            {
                // 等于127 随后的八个字节64位表示数据长度
                mPackageLength = (UInt64)(recBytes[start] << (8 * 7) | recBytes[start] << (8 * 6) | recBytes[start] << (8 * 5) | recBytes[start] << (8 * 4) | recBytes[start] << (8 * 3) | recBytes[start] << (8 * 2) | recBytes[start] << 8 | recBytes[start + 1]);
                start += 8;
            }

            mDataPackage = new byte[mPackageLength];
            for (UInt64 i = 0; i < mPackageLength; i++)
            {
                mDataPackage[i] = recBytes[i + (UInt64)start + 4];
            }

            Buffer.BlockCopy(recBytes, start, Masking_key, 0, 4);

            for (UInt64 i = 0; i < mPackageLength; i++)
            {
                mDataPackage[i] = (byte)(mDataPackage[i] ^ Masking_key[i % 4]);
            }

            return Encoding.UTF8.GetString(mDataPackage);
        }
        #endregion

        #region 发送数据
        /// <summary>
        /// 把发送给客户端消息打包处理（拼接上谁什么时候发的什么消息）
        /// </summary>
        /// <returns>The data.</returns>
        /// <param name="message">Message.</param>
        private byte[] PackageServerData(string msg)
        {
            byte[] content = null;
            byte[] temp = Encoding.UTF8.GetBytes(msg);
            if (temp.Length < 126)
            {
                content = new byte[temp.Length + 2];
                content[0] = 0x81;
                content[1] = (byte)temp.Length;
                Buffer.BlockCopy(temp, 0, content, 2, temp.Length);
            }
            else if (temp.Length < 0xFFFF)
            {
                content = new byte[temp.Length + 4];
                content[0] = 0x81;
                content[1] = 126;
                content[2] = (byte)(temp.Length & 0xFF);
                content[3] = (byte)(temp.Length >> 8 & 0xFF);
                Buffer.BlockCopy(temp, 0, content, 4, temp.Length);
            }
            return content;
        }
        #endregion
    }
}
