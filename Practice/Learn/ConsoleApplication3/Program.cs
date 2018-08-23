using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 5;
            int b = 6;



            Swap<int>(ref a, ref b);

            Console.Write("a={0},b={1}", a, b);
            Console.ReadKey();
        }


        /// <summary>
        /// 泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }

    //T：结构（类型参数必须是值类型。可以指定除 Nullable 以外的任何值类型）
    //T：类 （类型参数必须是引用类型，包括任何类、接口、委托或数组类型）
    //T：new() （类型参数必须具有无参数的公共构造函数。当与其他约束一起使用时new() 约束必须最后指定）
    //T：< 基类名> 类型参数必须是指定的基类或派生自指定的基类
    //T：< 接口名称> 类型参数必须是指定的接口或实现指定的接口。可以指定多个接口约束。约束接口也可以是泛型的。
    //T：U 

    //在封装公共组件的时候，很多时候我们的类/方法不需要关注调用者传递的实体是"什么"，这个时候就可以使用泛型。
    public class CacheHelperL<T> where T : new()
    {
        /// <summary>
        /// 获取缓存实体
        /// </summary>
        /// <param name="chche"></param>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static T Get(Cache chche, string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return (T)objCache[cacheKey];
        }

        /// <summary>
        /// 插入缓存
        /// </summary>
        /// <param name="tEntity"></param>
        /// <param name="cacheKey"></param>
        public static void Set(T tEntity, string cacheKey)
        {
        }
    }
}
