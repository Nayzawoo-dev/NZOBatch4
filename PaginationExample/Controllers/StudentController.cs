using DatabaseLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PaginationExample.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _db;

        public StudentController(AppDbContext db)
        {
            _db = db;
        }P

        public async Task<IActionResult> StudentList(string name, int PageNo = 1, int pageSize = 10)
        {
            StudentResponseModel model = new StudentResponseModel();
            //var studentlst = await _db.Students.ToListAsync();

            var query = _db.Students.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            var rowCount = await query.CountAsync();
            var pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }
            var lst = await query
                .Skip((PageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            model.Data = lst;
            model.PageCount = pageCount;
            model.PageSize = pageSize;
            model.PageNo = PageNo;

            return View(model);
        }
    }

    public class StudentResponseModel
    {
        public int PageCount { get; set; }

        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public List<Student> Data { get; set; }
    }
}
