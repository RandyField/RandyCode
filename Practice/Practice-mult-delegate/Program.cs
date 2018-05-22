using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_mult_delegate
{
    class Program
    {
        /// <summary>
        /// 声明一个委托
        /// </summary>
        /// <param name="precentComplete"></param>
        public delegate void ProgressReporter(int precentComplete);

        static void Main(string[] args)
        {
            ProgressReporter p = WriteProgressToConsole;
            p += WriteProgressToFile;// “+=” 用来添加，同理“-=”用来移除。
            Utility.Match(p);
            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        /// <summary>
        /// 控制台写进度的方法
        /// </summary>
        /// <param name="percentComplete"></param>
        static void WriteProgressToConsole(int percentComplete)
        {
            Console.WriteLine(percentComplete + "%");
        }

        /// <summary>
        /// txt文件写进度的方法
        /// </summary>
        /// <param name="percentComplete"></param>
        static void WriteProgressToFile(int percentComplete)
        {
            System.IO.File.AppendAllText("progress.txt", percentComplete + "%");
        }

        public class Utility
        {
            /// <summary>
            /// 调用委托的方法
            /// </summary>
            /// <param name="p"></param>
            public static void Match(ProgressReporter p)
            {
                if (p != null)
                {
                    for (int i = 0; i <= 10; i++)
                    {
                        p(i * 10);
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
        }
    }
}
