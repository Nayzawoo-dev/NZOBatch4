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
        public async Task<IActionResult> StudentIndex()
        {
            List<StudentModel> lst = await studentServices.GetStudentsAsync();
            return View("StudentIndex",lst);
        }

        [ActionName("Create")]
        public IActionResult StudentCreate()
        {
            return View("CreateStudent");
        }


        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> CreateStudent(StudentModel student)
        {
            if (student.RollNo is null || student.Name is null)
            {
                return View("CreateStudent");
            }

            
            bool isSaved = await studentServices.CreateStudentAsync(student);

            if (isSaved)
            {
                return RedirectToAction("Index");
            }

            return View("CreateStudent");
        }
    }
    }

