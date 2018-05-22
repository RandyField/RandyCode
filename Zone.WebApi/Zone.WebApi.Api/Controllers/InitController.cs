using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Zone.WebApi.Api.Controllers
{
    [RoutePrefix("init")]
    public class InitController : ApiController
    {
        [Route("admin")]
        [HttpGet]
        public void init()
        {
            SYS_USER_INFO_BLL.getInstance().InitAdmin();
        }
    }
}
