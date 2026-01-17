using Blog.MvcApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.MvcApp.Controllers
{
    public class MonthsController : Controller
    {
        private readonly IMonthServices _monthservices;

        public MonthsController(IMonthServices monthservices)
        {
            _monthservices = monthservices;
        }

        //[ActionName("Index")]
        public IActionResult MonthsList()
        {
            var model = _monthservices.MonthsList();
            return View("MonthsList",model);
        }
    }
}
