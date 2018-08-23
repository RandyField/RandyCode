using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var annoyCla = new
            {
                id = 1,
                name = "randy",
                age = 25
            };
            //AnonymousType#1

            Console.Write(annoyCla.name);
            Console.ReadKey();

            //（1）匿名类被编译后会生成一个[泛型类]，<>f__AnonymousType0<<ID>j__TPar, <Name>j__TPar, <Age>j__TPar>就是一个泛型类；

            //（2）匿名类所生成的属性都是只读的，可以看出与其对应的字段也是只读的；

            //（3）可以看出，匿名类还重写了基类的三个方法：Equals,GetHashCode和ToString；我们可以看看它为我们所生成的ToString方法是怎么来实现的：

        }
    }
}
