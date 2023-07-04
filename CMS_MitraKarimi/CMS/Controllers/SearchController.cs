using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

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