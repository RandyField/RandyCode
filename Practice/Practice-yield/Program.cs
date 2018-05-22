using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_yield
{
    class Program
    {
        static void Main(string[] args)
        {
            Class1 a = new Class1();


            foreach (int s in a)
            {
                Console.WriteLine(s);
            }

            Console.ReadKey();

            
        }
    }


}
