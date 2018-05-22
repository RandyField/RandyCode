using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Zone.WebApi.Model;

namespace Zone.WebApi.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        public string LoginedUserName = "";
        public int LoginedUserId = 0;

        //var content = ActionContext.Request.Properties["MS_HttpContext"] as HttpContextBase;

        //////get
        ////var token = content.Request.Headers["Token"];
        //var token = content.Request.Form["Token"];
        //SYS_TICKET_AUTH model = SYS_TICKET_AUTH_BLL.getInstance().GetTicketAuthByToken(token);
        //LoginedUserName = model.UserName;
        //LoginedUserId = model.UserId;

        /// <summary>
        /// 获取用户授权信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public void GetByToken(string token)
        {
            LoginedUserName = SYS_TICKET_AUTH_BLL.getInstance().GetTicketAuthByToken(token).UserName;
        }
    }
}
