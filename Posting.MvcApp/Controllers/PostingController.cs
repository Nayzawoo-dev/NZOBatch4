using Microsoft.AspNetCore.Mvc;
using Posting.MvcApp.FeaturesServices.UserServices;

namespace Posting.MvcApp.Controllers
{
    public class PostingController : Controller
    {
        private readonly UserFeaturesServices _services;

        public PostingController(UserFeaturesServices services)
        {
            _services = services;
        }

        public async Task<IActionResult> Index()
        {
            List<ResponseUserModel> lst = await _services.ReadUserAsync();
            return View(lst);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(RequestUserModel model)
        {
            var res = await _services.CreateUserAsync(model);
            return RedirectToAction("Index");
        }
    }
}
