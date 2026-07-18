using Microsoft.AspNetCore.Mvc;

namespace AuthMVC.Controllers
{
    public class AccessDeniedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
