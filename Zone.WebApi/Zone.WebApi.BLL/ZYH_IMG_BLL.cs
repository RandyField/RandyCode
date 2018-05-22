
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace BLL  
{
	///<summary>
	 	///ZYH_IMG_BLL
		///Author:ZhangDeng
	///</summary>
	public class ZYH_IMG_BLL
	{	
	    #region 单例模式
		///<summary>
		///create bll instance
		///</summary>
		private static ZYH_IMG_BLL instance;
		
		/// <summary>
        /// 私有构造函数，该类无法被实例化
        /// </summary>
        private ZYH_IMG_BLL() { }


        /// <summary>
        /// 线程锁
        /// </summary>
        private static object asyncLock = new object();

        /// <summary>
        /// 获取一个可用的对象
        /// </summary>
        /// <returns></returns>
        public static ZYH_IMG_BLL getInstance()
        {

            if (instance == null)
            {
                instance = new ZYH_IMG_BLL();
            }

            return instance;
        }
        #endregion      
	}
}