using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {

        delegate void NumberChanger(int n);

        //匿名方法
        static NumberChanger nc = delegate(int x)
        {
            Console.WriteLine("计算结果:{0}", x);
        };

        //匿名方法-lambda表达式
        static NumberChanger nc1 = x => {
            Console.WriteLine("计算结果:{0}", x * x); 
        };


        //类比js  var a= function(){}

        static void Main(string[] args)
        {
            //类的静态方法规定的，类中静态的方法、成员函数只能访问静态的数据成员或者静态的方法。 

            //static void Main(string[] args)
            
            //这使用了关键字static代表是静态方法，如果Main方法里面要调用外面的方法或者函数必须是静态的方法或者是函数。 

            //C#中就连static void Main(string[] args)要访问这个方法外面的变量都得是静态的。 

            //这些都是在类中，对于访问其它类就可以了。 

            //如：Main函数中访问其它类，就不用加Static

            nc(5);

            nc1(5);
            Console.ReadKey();
            
        }
    }
}
