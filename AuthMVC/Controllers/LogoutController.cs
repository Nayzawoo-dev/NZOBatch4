using Microsoft.AspNetCore.Mvc;

namespace AuthMVC.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
