using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MODEL;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly SchoolContext _context;

        public TodoController(SchoolContext context)
        {
            _context = context;

            // if (_context..Count() == 0)
            // {
            //     _context.TodoItems.Add(new TodoItem { Name = "Item1" });
            //     _context.SaveChanges();
            // }
        }    
        // GET api/values  
        [HttpGet]
        public IEnumerable<Student> GetAll()
        {
            return _context.Students.ToList();
        }
    }
}