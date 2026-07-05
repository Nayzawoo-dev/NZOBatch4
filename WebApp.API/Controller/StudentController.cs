using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Database.Models;

namespace WebApp.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            List<Student> lst = await _context.Students.ToListAsync();
            return Ok(lst);
        }

     
        [HttpPost]
        public async Task<ActionResult> CreateStudent([FromBody] Student model)
        {
            if (model == null)
            {
                return BadRequest("Data is null");
            }

            _context.Students.Add(model);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student created successfully!" });
        }

    }
}
