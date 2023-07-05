using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyCms.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private IPageRepository pageRepository;
        private IPageGroupRepository pageGroupRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public PagesController(IPageRepository pageRepository, IPageGroupRepository pageGroupRepository, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            this.pageRepository = pageRepository;
            this.pageGroupRepository = pageGroupRepository;
        }

        // GET: /Admin/Pages/index
        public ActionResult Index()
        {
            return View(pageRepository.GetAllPage());
        }

        // GET: Admin/Pages/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var page = pageRepository.GetPageById(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // GET: Admin/Pages/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(pageGroupRepository.GetAllGroups(), "GroupID", "GroupTitle");
            return View();
        }

        // POST: Admin/Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PageID,GroupID,Title,ShortDescription,Text,Visit,ImageName,ShowInSlider,CreateDate,Tags")] Page page, IFormFile imgUp)
        {
            page.Visit = 0;
            page.CreateDate = DateTime.Now;
            if (imgUp != null)
            {
                var ImageName = pageRepository.SaveImage(imgUp);
                page.ImageName = ImageName;
            }
            pageRepository.InsertPage(page);
            pageRepository.Save();
            return RedirectToAction("Index");
        }


        // GET: Admin/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest(ModelState);
            }
            Page page = pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }
            ViewBag.GroupID = new SelectList(pageGroupRepository.GetAllGroups(), "GroupID", "GroupTitle", page.GroupID);
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(Page page, IFormFile imgUp)
        {

            if (imgUp != null)
            {
                var ImageName = pageRepository.SaveImage(imgUp);
                page.ImageName = ImageName;
            }
            pageRepository.UpdatePage(page);
            pageRepository.Save();
            return RedirectToAction("Index");
        }



        // GET: Admin/Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Page page = pageRepository.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var page = pageRepository.GetPageById(id);
            if (page.ImageName != null)
            {
                pageRepository.DeleteImage(page.ImageName);
            }
            pageRepository.DeletePage(page);
            pageRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pageGroupRepository.Dispose();
                pageRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
