using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            buybook();

            BookAction1();

            BookAction2("C#从入门到放弃", "新华书店");

            Console.WriteLine(RetBook1());

            Console.WriteLine(RetBook2("Python从入门到放弃"));

            if (wantbook("English"))
            {
                Console.WriteLine("就要这个版本");
            }

            Console.ReadKey();
        }

        #region 正式的委托-方法-委托与方法建立联系
        

        //定义委托----买书
        private delegate void BuyBook();

        //定义方法-----卖书
        public static void Book()
        {
            Console.WriteLine("我提供书籍");
        }

        //实例化委托-----建立买卖关系
        static BuyBook buybook = new BuyBook(Book);

        #endregion

        #region 语法糖之Action 无返回值

        static Action BookAction1 = new Action(Book);

        #endregion

        #region 语法糖之Aciton 多参数

       static Action<string, string> BookAction2 = new Action<string, string>(Book);

        public static void Book(string BookName, string ChangJia)
        {
            Console.WriteLine("我是买书的是:{0},来自{1}", BookName, ChangJia);
        }

        #endregion

        #region 语法糖之Func  无参有返回值

        static Func<string> RetBook1 = new Func<string>(FuncBook);

        public static string FuncBook()
        {
            return "送书来了";
        }
        
        #endregion

        #region 语法糖之Func 有参有返回值

        static Func<string, string> RetBook2 = new Func<string, string>(FuncBook);

        public static string FuncBook(string BookName)
        {
            return BookName;
        }

        #endregion

        #region 语法糖之predicate

        static Predicate<string> wantbook = new Predicate<string>(DoyouWantBook);

        public static bool DoyouWantBook(string BookName)
        {
            if (BookName=="English")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        #endregion

    }
}
