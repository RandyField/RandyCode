using Common.Helper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zone.WebApi.DAL;
using Zone.WebApi.IDAL;
using Zone.WebApi.Model;
namespace BLL
{
    ///<summary>
    ///票据授权表_BLL
    ///Author:ZhangDeng
    ///</summary>
    public class SYS_TICKET_AUTH_BLL
    {
        #region 单例模式
        ///<summary>
        ///create bll instance
        ///</summary>
        private static SYS_TICKET_AUTH_BLL instance;

        /// <summary>
        /// 私有构造函数，该类无法被实例化
        /// </summary>
        private SYS_TICKET_AUTH_BLL() { }


        /// <summary>
        /// 线程锁
        /// </summary>
        private static object asyncLock = new object();

        /// <summary>
        /// 获取一个可用的对象
        /// </summary>
        /// <returns></returns>
        public static SYS_TICKET_AUTH_BLL getInstance()
        {

            if (instance == null)
            {
                instance = new SYS_TICKET_AUTH_BLL();
            }

            return instance;
        }
        #endregion
        private IBaseDAL idal = DbFactory.Create();

        /// <summary>
        /// 保存票据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SavaTicketAuth(SYS_TICKET_AUTH model)
        {
            bool success = false;
            try
            {
                //判断admin是否存在
                DynamicParameters param = new DynamicParameters();


                string insertsql = @"INSERT INTO SYS_TICKET_AUTH    
                                    (
                                        [UserId]
                                      ,[UserName]
                                      ,[Token]
                                      ,[CreateTime]
                                      ,[ExprieTime]
                                     )
                                    VALUES
                                    (
                                         @UserId
                                      ,@UserName
                                      ,@Token
                                      ,@CreateTime
                                      ,@ExprieTime
                                    )";

                success = idal.CreateEntity<SYS_TICKET_AUTH>(insertsql, model);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("【{0}】票据保存异常，异常信息:{1}", model.Token, ex.ToString()));
            }
            return success;
        }


        /// <summary>
        /// 根据token获取票据实体
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public SYS_TICKET_AUTH GetTicketAuthByToken(string token)
        {
            SYS_TICKET_AUTH model = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Token", token);

                string querysql = @"SELECT [Id]
                                          ,[UserId]
                                          ,[UserName]
                                          ,[Token]
                                          ,[CreateTime]
                                          ,[ExprieTime]     
                                FROM [ZoneSite].[dbo].[SYS_TICKET_AUTH]
                                WHERE Token=@Token";

                model = idal.FindOne<SYS_TICKET_AUTH>(querysql, param, false);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("【{0}】获取票据信息异常，异常信息:{1}", token, ex.ToString()));
            }
            return model;
            
        }

        /// <summary>
        /// 根据用户id删除用户票据
        /// </summary>
        /// <param name="userid"></param>
        public void DeleteTicketAuthByUserId(int userid)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("UserId", userid);

                string deletesql = @"DELETE    
                                FROM [ZoneSite].[dbo].[SYS_TICKET_AUTH]
                                WHERE UserId=@UserId";

                idal.ExcuteNonQuery<SYS_TICKET_AUTH>(deletesql, param, false);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("用户id：【{0}】清除票据异常，异常信息:{1}", userid, ex.ToString()));
            }
        }
    }
}