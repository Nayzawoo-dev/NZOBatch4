using Microsoft.AspNetCore.Mvc;

namespace Middleware.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginRequestModel model)
        {
            // db => condition => return view
            //HttpContext.Response.Cookies.Append("UserEmail", model.Email);

            var opt = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(7),
                SameSite = SameSiteMode.Lax,
                Secure = true
            };

            HttpContext.Response.Cookies.Append("UserEmail", model.Email, opt);

            return RedirectToAction("Index", "Home");
        }
    }

    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
