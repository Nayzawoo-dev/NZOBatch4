using Microsoft.AspNetCore.Mvc;
using MVC.WebApp.Models;
using MVC.WebApp.Services;

namespace MVC.WebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentServices studentServices;

        public StudentController(IStudentServices studentServices)
        {
            this.studentServices = studentServices;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            List<StudentModel> lst = await studentServices.GetWalletsAsync();
            return View("StudentIndex",lst);
        }
    }
}
