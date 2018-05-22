using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSocket server = new WebSocket();
            server.start(81);
        }
    }
}
