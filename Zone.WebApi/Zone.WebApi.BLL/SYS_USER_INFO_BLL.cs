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
    ///SYS_USER_INFO_BLL
    ///Author:ZhangDeng
    ///</summary>
    public class SYS_USER_INFO_BLL
    {
        #region 单例模式
        ///<summary>
        ///create bll instance
        ///</summary>
        private static SYS_USER_INFO_BLL instance;

        /// <summary>
        /// 私有构造函数，该类无法被实例化
        /// </summary>
        private SYS_USER_INFO_BLL() { }


        /// <summary>
        /// 线程锁
        /// </summary>
        private static object asyncLock = new object();

        /// <summary>
        /// 获取一个可用的对象
        /// </summary>
        /// <returns></returns>
        public static SYS_USER_INFO_BLL getInstance()
        {

            if (instance == null)
            {
                instance = new SYS_USER_INFO_BLL();
            }

            return instance;
        }
        #endregion

        private IBaseDAL idal = DbFactory.Create();

        #region 初始化密码

        /// <summary>
        /// 初始化admin密码
        /// </summary>
        public void InitAdmin()
        {
            try
            {
                SYS_USER_INFO model = new SYS_USER_INFO();
                model.UserName = "admin";

                //判断admin是否存在
                DynamicParameters param = new DynamicParameters();

                param.Add("UserName", model.UserName);
                string querysql = @"SELECT * FROM SYS_USER_INFO WHERE UserName=@UserName";

                if (idal.FindOne<SYS_USER_INFO>(querysql, param, false) == null)
                {
                    model.CreateTime = DateTime.Now;
                    model.PassWord = SecureHelper.AESEncrypt("admin");
                    model.State = 1;

                    string insertsql = @"INSERT INTO [SYS_USER_INFO]
                                          (
                                              [UserName]
                                              ,[PassWord]
                                              ,[State]
                                              ,[CreateTime]                                           
                                           )
                                        VALUES
                                           (
                                               @UserName
                                              ,@PassWord
                                              ,@State
                                              ,@CreateTime                                            
                                            )            ";
                    idal.CreateEntity<SYS_USER_INFO>(insertsql, model);
                }
                else
                {
                    model.UpdateTime = DateTime.Now;
                    model.PassWord = SecureHelper.AESEncrypt("admin");
                    model.State = 1;
                    param.Add("PassWord", model.PassWord);
                    param.Add("UpdateTime", model.UpdateTime);
                    param.Add("State", model.State);
                    string updatesql = @"UPDATE [SYS_USER_INFO] SET         
                                            [PassWord]=@PassWord
                                              ,[State]=@State
                                              ,[UpdateTime]=@UpdateTime                                           
                                           
                                        WHERE
                                            UserName='admin'         ";
                    idal.ExcuteNonQuery<SYS_USER_INFO>(updatesql, param, false);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("admin初始化异常，异常信息:{0}", ex.ToString()));
            }
        }

        #endregion

        #region 登录


        /// <summary>
        /// 根据用户名 密码 获取用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SYS_USER_INFO GetLoginModel(string username, string password, ref string msg)
        {
            SYS_USER_INFO model = null;
            try
            {
                //判断admin是否存在
                DynamicParameters param = new DynamicParameters();

                param.Add("UserName", username);
                string querysql = @"SELECT * FROM SYS_USER_INFO WHERE UserName=@UserName";

                SYS_USER_INFO temp = idal.FindOne<SYS_USER_INFO>(querysql, param, false);

                if (temp == null)
                {
                    msg = "用户名不存在";
                }
                else
                {
                    if (SecureHelper.AESDecrypt(temp.PassWord) == password)
                    {
                        msg = "登录成功";
                        model = temp;
                    }
                    else
                    {
                        msg = "密码错误";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("【{0}】登录异常，异常信息:{1}", username, ex.ToString()));
            }
            return model;
        }

        #endregion

        #region 新增账户


        /// <summary>
        /// 新增账户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddUser(SYS_USER_INFO model, ref string msg)
        {
            bool success = false;
            try
            {
                string tempmsg="";

                SYS_USER_INFO temp = QueryUsername(model.UserName, ref tempmsg);

                if (temp == null)
                {
                    string insertsql = @"INSERT INTO [SYS_USER_INFO]
                                          (
                                              [UserName]
                                              ,[PassWord]
                                              ,[RealName]
                                              ,[Phone]
                                              ,[QQ]
                                              ,[State]
                                              ,[CreateUser]
                                              ,[CreateTime]                                         
                                           )
                                        VALUES
                                           (
                                               @UserName
                                              ,@PassWord
                                              ,@RealName
                                              ,@Phone
                                              ,@QQ
                                              ,@State
                                              ,@CreateUser
                                              ,@CreateTime                                              
                                            )            ";
                    success = idal.CreateEntity<SYS_USER_INFO>(insertsql, model);
                }
                else
                {
                    msg = "用户名已存在";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("账户新增异常，异常信息:{0}", ex.ToString()));
            }
            return success;
        }

        #endregion

        #region 编辑账户


        /// <summary>
        /// 编辑账户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditUser(SYS_USER_INFO model)
        {
            bool success = false;
            try
            {
                DynamicParameters param = new DynamicParameters();

                //条件
                param.Add("Id", model.Id);

                param.Add("PassWord", model.PassWord);
                param.Add("RealName", model.RealName);
                param.Add("Phone", model.Phone);
                param.Add("QQ", model.QQ);
                param.Add("State", model.State);
                param.Add("UpdateUser", model.UpdateUser);
                param.Add("UpdateTime", model.UpdateTime);
                string updatesql = @"UPDATE [SYS_USER_INFO] SET         
                                              PassWord= @PassWord   
                                              ,RealName=@RealName   
                                              ,Phone=@Phone 
                                              ,QQ=@QQ   
                                              ,State=@State 
                                              ,UpdateUser=@UpdateUser   
                                              ,UpdateTime=@UpdateTime    
                                        WHERE       
                                            Id=@Id";
                success = idal.ExcuteNonQuery<SYS_USER_INFO>(updatesql, param, false) > 0;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("账户编辑异常，异常信息:{0}", ex.ToString()));
            }
            return success;
        }

        #endregion

        #region 查询账户 id


        /// <summary>
        /// 查询账户根据id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SYS_USER_INFO QueryUserById(string id, ref string msg)
        {
            SYS_USER_INFO model = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Id", id);
                string querysql = @"SELECT * FROM SYS_USER_INFO WHERE Id=@Id";

                model = idal.FindOne<SYS_USER_INFO>(querysql, param, false);

                if (model == null)
                {
                    msg = "用户不不存在";
                }

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("根据id【{0}】查询用户异常，异常信息:{1}", id, ex.ToString()));
            }
            return model;
        }

        #endregion

        #region 查询账户 用户名

        /// <summary>
        /// 根据用户名查询用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SYS_USER_INFO QueryUsername(string username, ref string msg)
        {
            SYS_USER_INFO model = null;
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("UserName", username);

                string querysql = @"SELECT * FROM SYS_USER_INFO WHERE UserName=@UserName";

                model = idal.FindOne<SYS_USER_INFO>(querysql, param, false);

                if (model == null)
                {
                    msg = "用户名不存在";
                }

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("根据用户名【{0}】查询用户异常，异常信息:{1}", username, ex.ToString()));
            }
            return model;
        }

        #endregion
    }
}