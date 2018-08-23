using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            BoilerEventLog += new BoilerLogHandler(Logger);

            BoilerEventLog("状态错误");

            BoilerEventLog("状态正确");
            Console.ReadKey();
        }

        //定义委托
        public delegate void BoilerLogHandler(string status);

        //基于上面的委托定义事件
        public static event BoilerLogHandler BoilerEventLog;

        static void Logger(string info)
        {
            Console.WriteLine(info);
        }
        //end 

    }
}
