using Microsoft.AspNetCore.Mvc;

namespace Middleware.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
