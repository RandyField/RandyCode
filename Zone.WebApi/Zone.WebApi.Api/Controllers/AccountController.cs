using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Zone.WebApi.Api.Models;
using Zone.WebApi.Api.Providers;
using Zone.WebApi.Api.Results;
using WebApi.Common;
using Newtonsoft.Json.Linq;
using Zone.WebApi.Api.Filter;
using Zone.WebApi.Model;
using BLL;
using Common.Enum;

namespace Zone.WebApi.Api.Controllers
{
    [AuthFilterOutside(Roles = "test")]
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        #region +账户新增

        /// <summary>
        /// 账户新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("add")]
        [HttpPost]
        public ResponseResult SaveUser([FromBody]JObject data)
        {
            //定义  
            ResponseResult result = new ResponseResult();

            string msg = "";

            if (data["username"] != null && data["password"] != null)
            {
                SYS_USER_INFO model = new SYS_USER_INFO();
                var dtNow = DateTime.Now;
                string username = data["username"].ToString();
                string password = data["password"].ToString();

                //获取操作用户
                GetByToken(data["Token"].ToString());

                model.UserName = username;
                model.PassWord = Common.Helper.SecureHelper.AESEncrypt(password);
                model.QQ = data["qq"] == null ? null : data["qq"].ToString();
                model.Phone = data["phone"] == null ? null : data["phone"].ToString();
                model.RealName = data["realname"] == null ? null : data["realname"].ToString();
                model.State = 0;
                model.CreateTime = dtNow;
                model.CreateUser = LoginedUserName;

                //保存
                if (SYS_USER_INFO_BLL.getInstance().AddUser(model, ref msg))
                {
                    result.return_code = RETURN_CODE.SUCCESS.ToString();
                    result.return_msg = "新增成功";
                }
                else
                {
                    result.return_code = RETURN_CODE.FAIL.ToString();
                    result.return_msg = msg;
                }

            }
            else
            {
                msg = "用户名或密码不能为空";
            }


            return result;
        }
        #endregion

        /// <summary>
        /// 账户删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public ResponseResult DeleteUser([FromBody]JObject data)
        {
            //定义  
            ResponseResult result = new ResponseResult();


            return result;
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public ResponseResult EditUser([FromBody]JObject data)
        {
            //定义  
            ResponseResult result = new ResponseResult();


            return result;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Route("Search")]
        [HttpPost]
        public ResponseResult SearchUser([FromBody]JObject data)
        {
            //定义  
            ResponseResult result = new ResponseResult();


            return result;
        }

    }
}
