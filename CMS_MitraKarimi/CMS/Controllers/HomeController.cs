using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyCms.Controllers
{
    public class HomeController : Controller
    {
        private IPageRepository pageRepository;

        public HomeController(IPageRepository pageRepository)
        {
            this.pageRepository = pageRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Slider()
        {
            return PartialView(pageRepository.PagesInSlider());
        }


    }
}