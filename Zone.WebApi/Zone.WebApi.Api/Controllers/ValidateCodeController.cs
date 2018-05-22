using Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace Zone.WebApi.Api.Controllers
{
     [RoutePrefix("api/validate")]
    public class ValidateCodeController : BaseApiController

    {
         public ValidateCodeController(System.Web.Http.Controllers.HttpActionContext actionContext)
         {
 
         }

         [Route("code")]
         public HttpResponseMessage GetValidateCode()
         {
             ValidateCode vCode = new ValidateCode();

             //获取验证码
             string code = vCode.CreateValidateCode(5);

             ////存如session
             //HttpContext.Current.Session["validatecode"] = code;

             //Session["ValidCode"]="Session Test"

             //获取验证码图片
             byte[] bytes = vCode.CreateValidateGraphic(code);

             //设置响应
             var resp = new HttpResponseMessage(HttpStatusCode.OK)
             {
                 Content = new ByteArrayContent(bytes)
                 //或者  
                 //Content = new StreamContent(stream)  
             };
             resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

             string guid=Guid.NewGuid().ToString();
             resp.Headers.Add("pictureId", guid);
             CacheHelper.SetCache(guid, code, TimeSpan.FromSeconds(30));
             return resp;
         }
    }
}
