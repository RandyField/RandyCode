using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_delegate_plug_in
{

     delegate int Calculator(int x);

    /// <summary>
    /// 委托实现插件式编程
    /// </summary>
    class Program
    {
        //plug-in
        static int Double(int x)
        {
            return x * 2;
        }

        static void Main(string[] args)
        {
            int[] values = { 1, 2, 5, 4 };

            //传值参数，data+function
            Utility.Calculate(values, Double);      
            foreach (var item in values)
            {
                
            }
        }
    }

    class Utility
    {
        public static void Calculate(int[] values, Calculator c)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = c(values[i]);
            }
        }

    }
}
