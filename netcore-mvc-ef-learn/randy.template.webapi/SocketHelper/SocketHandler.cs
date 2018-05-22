using System.Collections.Concurrent;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting.Server;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace SocketHelper
{
    public class SocketHandler
    {
        private static List<WebSocket> _sockets = new List<WebSocket>();

        public const int BufferSize = 4096;

        public static object objLock = new object();

        public static void Map(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(Acceptor);
        }

        static async Task Acceptor(HttpContext httpContext, Func<Task> n)
        {
            //非websocket请求
            if (!httpContext.WebSockets.IsWebSocketRequest)
                return;

            //建立一个WebSocket连接请求
            var socket = await httpContext.WebSockets.AcceptWebSocketAsync();

            //判断最大连接数
            if (_sockets.Count >= 1000)
            {
                await socket.CloseAsync(WebSocketCloseStatus.PolicyViolation,
                                        "连接超过最大限制，请稍候请求 ...",
                                        CancellationToken.None);
                return;
            }

            lock (objLock)
            {
                _sockets.Add(socket);//存储websocket请求
            }

            var buffer = new byte[BufferSize];

            //根据请求头获取 计算机名
            string computername = httpContext.Request.Query["computername"].ToString();

            //获取弹幕信息
            string dmstr="json字符串";

            //群发弹幕
            await SendToWebSocketsAsync(_sockets, dmstr);
        }

        /// <summary>
        /// 发送消息到所有人
        /// </summary>
        /// <param name="sockets"></param>
        /// <param name="arraySegment"></param>
        /// <returns></returns>
        public async static Task SendToWebSocketsAsync(List<WebSocket> sockets, string data)
        {
            // SaveHistoricalMessg(data);//保存历史消息
            var dmstr = JsonConvert.SerializeObject(data);
            var buffer = Encoding.UTF8.GetBytes(dmstr);

            ArraySegment<byte> arraySegment = new ArraySegment<byte>(buffer);

            //循环发送消息
            for (int i = 0; i < sockets.Count; i++)
            {
                var tempsocket = sockets[i];

                //那些还存在的连接
                if (tempsocket.State == WebSocketState.Open)
                {
                    //发送消息
                    await tempsocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
