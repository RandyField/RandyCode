using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(m =>
                {
                    //Console.WriteLine(Thread.CurrentThread.ManageThreadId.ToString());
                });

                System.Timers.Timer timer = new System.Timers.Timer ()
            }
        }
    }
}
