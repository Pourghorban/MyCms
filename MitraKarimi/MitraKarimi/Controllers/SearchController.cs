using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace MyCms.Controllers
{
    public class SearchController : Controller
    {
        private IPageRepository pageRepository;

        public SearchController(IPageRepository pageRepository)
        {
            this.pageRepository = pageRepository;
        }
        // GET: Search
        public ActionResult Index(string q)
        {
            ViewBag.Name = q;
            return View(pageRepository.SearchPage(q));
        }
    }
}