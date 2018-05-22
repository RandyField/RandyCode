using BLL;
using Common.Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WebApi.Common;
using Zone.WebApi.Api.Models;
using Zone.WebApi.Model;

namespace Zone.WebApi.Api.Controllers
{
    [RoutePrefix("api/user")]
    public class LoginController : ApiController
    {  
        #region +登录

        [Route("account/login")]
        [HttpPost]
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ResponseResult Login([FromBody]JObject data)
        {
            //返回实体
            ResponseResult result = new ResponseResult();
            try
            {
                string msg = "";

                if (data["username"] != null && data["password"] != null)
                    
                    //&& data["validatecode"] != null && data["pictureId"]!=null)
                {
                    var dtNow = DateTime.Now;
                    string username = data["username"].ToString();
                    string password = data["password"].ToString();

                    //string validatecode = data["validatecode"].ToString();
                    //string pictureId = data["pictureId"].ToString();

                    //if (validatecode != CacheHelper.GetCache(pictureId).ToString())
                    //{
                    //    result.return_code = "FAIL";
                    //    result.return_msg = "验证码错误";
                    //    return result;
                    //}

                    //登录
                    var model = SYS_USER_INFO_BLL.getInstance().GetLoginModel(username, password, ref msg);

                    //账号 密码验证通过
                    if (model != null)
                    {
                        //Common.Enum.LoginStatus.Success;
                        //生成票据
                        string Token = Common.Helper.DESEncrypt.Encrypt(System.Guid.NewGuid().ToString());

                        int userid = model.Id;

                        //直接清除
                        SYS_TICKET_AUTH_BLL.getInstance().DeleteTicketAuthByUserId(userid);


                        #region 将身份信息保存票据表中，验证当前请求是否是有效请求

                        SYS_TICKET_AUTH ticket = new SYS_TICKET_AUTH();
                        ticket.UserId = model.Id;
                        ticket.UserName = model.UserName;
                        ticket.Token = Token;
                        ticket.ExprieTime = dtNow.AddMinutes(30); //30分钟过期
                        ticket.CreateTime = dtNow;

                        //新增ticket
                        SYS_TICKET_AUTH_BLL.getInstance().SavaTicketAuth(ticket);

                        #endregion

                        JObject logininfo = new JObject();
                        logininfo.Add("userid", model.Id);
                        logininfo.Add("loginname", model.UserName);
                        logininfo.Add("token", Token);
                        result.return_code = Common.Enum.RETURN_CODE.SUCCESS.ToString();
                        result.return_info = logininfo;
                    }

                    //账号密码验证错误
                    else
                    {
                        result.return_code = Common.Enum.RETURN_CODE.FAIL.ToString();
                    }
                }
                else
                {
                    result.return_code = Common.Enum.RETURN_CODE.ERROR.ToString() ;
                    result.return_msg = "参数不完整";
                    result.return_info = null;
                }
                result.return_msg = msg;
                
                //var resultObj = JsonConvert.SerializeObject(obj, Formatting.Indented);
                //HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(resultObj, Encoding.GetEncoding("UTF-8"), "application/json") };
                //return result;  
            }
            catch (Exception ex)
            {

                Logger.Error(string.Format("登录异常，异常信息:{0}",  ex.ToString()));
            }
            return result;
        }

        #endregion

        #region +查询Token是否有效
        /// <summary>  
        /// 查询Token是否有效  
        /// </summary>  
        /// <param name="token">token</param>  
        /// <returns></returns>  
        [Route("validatetoken")]
        [HttpGet]
        public ResponseResult ValidateToken(string token)
        {
            //定义  
            ResponseResult result = new ResponseResult();
            bool flag = ValidateTicket(token);
            if (flag)
            {
                //返回信息              
                result.return_code = "SUCCESS";
                result.return_msg = "token有效";
            }
            else
            {
                result.return_code = "FAIL";
                result.return_msg = "token无效";
            }

            return result;
        }
        #endregion

        #region +用户退出登录，清空Token
        /// <summary>  
        /// 用户退出登录，清空Token  
        /// </summary>  
        /// <param name="userId">用户ID</param>  
        /// <returns></returns>  
        [Route("account/loginout")]
        [HttpGet]
        public ResponseResult LoginOut(int userid)
        {
            ResponseResult result = new ResponseResult();
            SYS_TICKET_AUTH_BLL.getInstance().DeleteTicketAuthByUserId(userid);

            //返回信息              
            result.return_code = "SUCCESS";
            result.return_info = "成功退出";
            return result;
        }
        #endregion

        #region +验证票据是否有效
        /// <summary>  
        /// 验证票据是否有效  
        /// </summary>  
        /// <param name="encryptToken">token</param>  
        /// <returns></returns>  
        private bool ValidateTicket(string encryptToken)
        {
            bool flag = false;

            //获取数据库Token  
            SYS_TICKET_AUTH model = SYS_TICKET_AUTH_BLL.getInstance().GetTicketAuthByToken(encryptToken);
            if (model.Token == encryptToken) //存在  
            {
                //未超时  
                flag = (DateTime.Now <= model.ExprieTime) ? true : false;
            }

            return flag;
        }

        #endregion

        #region +新增用户

        [Route("account/new")]
        [HttpPost]
        public ResponseResult Add([FromBody]JObject data)
        {
            //返回实体
            ResponseResult result = new ResponseResult();
            try
            {
                string msg = "";

                if (data["username"] != null && data["password"] != null)
                {
                    var dtNow = DateTime.Now;
                    string username = data["username"].ToString();
                    string password = data["password"].ToString();
                    SYS_USER_INFO model = new SYS_USER_INFO();
                    model.UserName = username;
                    model.PassWord = password;
                    model.QQ = data["QQ"] == null ? null : data["QQ"].ToString();
                    model.Phone = data["Phone"] == null ? null : data["Phone"].ToString();
                    model.RealName = data["RealName"] == null ? null : data["RealName"].ToString();
                    model.State = 0;
                    model.CreateTime = dtNow;
                    //model.CreateUser = userid;  
                    //model.QQ = data["QQ"] == null ? null : data["QQ"].ToString();  
                }
            }
            catch (Exception ex)
            {

                Logger.Error(string.Format("登录异常，异常信息:{0}", ex.ToString()));
            }
            return result;
        }
        #endregion
    }
}
