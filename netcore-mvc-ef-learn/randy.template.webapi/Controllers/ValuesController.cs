using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MODEL;

namespace randy.template.webapi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly SchoolDbContext _context;


        //ASP.NET 依赖关系注入将会负责处理传递的一个SchoolContext数据库上下文实例 插入控制器。
        public ValuesController(SchoolDbContext context)
        {
            _context = context;
        }


        // GET api/values
        [HttpGet]
        public Task<List<Student>> Get()
        {
            var Students = _context.Students.ToListAsync();
            return Students;
            // return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
