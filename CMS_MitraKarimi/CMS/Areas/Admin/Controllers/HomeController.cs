using Microsoft.AspNetCore.Mvc;

namespace CMS.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
