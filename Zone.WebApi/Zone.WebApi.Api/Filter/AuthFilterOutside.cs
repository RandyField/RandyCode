using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Zone.WebApi.Model;

namespace Zone.WebApi.Api.Filter
{
    public class AuthFilterOutside : AuthorizeAttribute
    {
       

        //重写基类的验证方式，加入我们自定义的Ticket验证  
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            
            //url获取token  
            var content = actionContext.Request.Properties["MS_HttpContext"] as HttpContextBase;

            ////get
            //var token = content.Request.Headers["Token"];
            var token = content.Request.Form["Token"];
            if (!string.IsNullOrEmpty(token))
            {
                //解密用户ticket,并校验用户名密码是否匹配  
                if (ValidateTicket(token))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401  
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();

                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);

                if (isAnonymous)
                {
                    base.OnAuthorization(actionContext);
                }

                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
        }

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
            if (model!=null)
            {
                if (model.Token == encryptToken) //存在  
                {
                    //未超时  
                    flag = (DateTime.Now <= model.ExprieTime) ? true : false;
                }
            }
            //Roles = model.UserName;

            return flag;
        }

        #endregion
    }
}