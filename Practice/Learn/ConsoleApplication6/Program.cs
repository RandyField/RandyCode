using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            //定义后台线程
            Thread t = new Thread(Run)
            {
                IsBackground = true
            };

            //线程启动
            t.Start();

            Thread.Sleep(1500);
            Console.ReadKey();

        }

        public static void Run()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Run后台线程正在运行中...");
            }
           
        }
    }
}
