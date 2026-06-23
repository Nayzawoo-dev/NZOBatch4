using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AjaxApp.MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly SqlConnectionStringBuilder connectionstring;

        public StudentController(IConfiguration configuration)
        {
            connectionstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("DbConnection"));
        }

        [ActionName("Index")]
        public IActionResult StudentIndex()
        {
            return View("StudentIndex");
        }
    }
}
