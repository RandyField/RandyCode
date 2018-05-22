using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketServer
{
    public class Session
    {
        /// <summary>
        /// 客户端
        /// </summary>
        public Socket _socketclient { get; set; }

        public byte[] _buffer { get; set; }

        /// <summary>
        /// 客户端ip地址
        /// </summary>
        public string _ip { get; set; }

        /// <summary>
        /// 是否是web的标识
        /// </summary>
        public bool _isweb{get;set;}
        

    }
}
