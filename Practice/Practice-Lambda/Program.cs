using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            // 创建商品集合
            IEnumerable<Product> products = new List<Product> { 
                new Product {Name = "西瓜", Category = "水果", Price = 2.3M}, 
                new Product {Name = "苹果", Category = "水果", Price = 4.9M}, 
                new Product {Name = "ASP.NET MCV 入门", Category = "书籍", Price = 19.5M}, 
                new Product {Name = "ASP.NET MCV 提高", Category = "书籍", Price = 34.9M} 
            };

            //delegate int Calculator(int x);

            //用匿名函数定义一个具体的查询需求
            //委托和匿名函数
            //原始Func<Product, bool> fruitFilter = delegate(Product prod)
            //{
            //    return prod.Category == "水果";
            //};

            ////演化1
            //Func<Product, bool> fruitFilter = a => a.Category == "水果";

            ////调用Filter，查询分类为“水果”的商品
            //IEnumerable<Product> filteredProducts = products.Filter(fruitFilter);

            //演化2
            IEnumerable<Product> filteredProducts = products.Filter(p=>p.Category=="水果");

            //打印结果
            foreach (Product prod in filteredProducts)
            {
                Console.WriteLine("商品名称: {0}, 单价: {1:c}", prod.Name, prod.Price);
            }
            Console.ReadKey();



            List<Product> s = new List<Product> { 
                new Product {Name = "西瓜", Category = "水果", Price = 2.3M}, 
                new Product {Name = "苹果", Category = "水果", Price = 4.9M}, 
                new Product {Name = "ASP.NET MCV 入门", Category = "书籍", Price = 19.5M}, 
                new Product {Name = "ASP.NET MCV 提高", Category = "书籍", Price = 34.9M} 
            };
         
        }
    }
}
