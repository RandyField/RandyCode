using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_delegate
{
    delegate int Calculator(int x);
    /// <summary>
    /// 委托基础
    /// </summary>
    class Program
    {
        //委托-把方法当参数传递的对象，而且还知道怎么调用这个方法，同时也是粒度更小的“接口”（约束了指向方法的签名）


        static int Double(int x) 
        {
            return x * 2;
        }

        static void Main(string[] args)
        {
            //Calculator c = new Calculator(Double);

            //简写

            Calculator c = Double;

            int result = c(2);

            Console.Write(result);
            Console.ReadKey();

        }
    }
}
