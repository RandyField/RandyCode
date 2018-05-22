using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zone.WebApi.IDAL;

namespace Zone.WebApi.DAL
{
    public class DbFactory
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="strconnection"></param>
        /// <returns></returns>
        public static IBaseDAL Create(string strConnection=null)
        {
            return new BaseDAL(strConnection);
        }
    }
}
