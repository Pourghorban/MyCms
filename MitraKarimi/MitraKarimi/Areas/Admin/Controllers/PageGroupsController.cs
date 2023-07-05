using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace MyCms.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PageGroupsController : Controller
    {

        private IPageGroupRepository pageGroupRepository;
        public PageGroupsController(IPageGroupRepository pageGroupRepository)
        {
            this.pageGroupRepository = pageGroupRepository;
        }

        // GET: /Admin/PageGroups/index
        [HttpGet]
        public ActionResult Index()
        {
            return View(pageGroupRepository.GetAllGroups());
        }


        // GET: admin/pagegroups/details/1
        // [HttpGet("details/{id}")]
        public ActionResult Details(int id)

        {
            if (id == null)
            {
                return BadRequest();
            }
            PageGroup pageGroup = pageGroupRepository.GetGroupById(id);
            if (pageGroup == null)
            {
                return NotFound();
            }
            return View(pageGroup);
        }

        // GET: admin/pagegroups/create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: admin/pagegroups/create
        [HttpPost]

        public ActionResult Create([Bind("GroupID,GroupTitle")] PageGroup pageGroup)
        {
            pageGroupRepository.InsertGroup(pageGroup);
            pageGroupRepository.Save();
            return RedirectToAction("Index");
        }

        // GET: admin/pagegroups/edit/5

        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var pageGroup = pageGroupRepository.GetGroupById(id);
            if (pageGroup == null)
            {
                return NotFound();
            }
            return PartialView(pageGroup);
        }
        // POST: admin/pagegroups/edit/5
        [HttpPost]
        public ActionResult Edit([Bind("GroupID,GroupTitle")] PageGroup pageGroup)
        {
            pageGroupRepository.UpdateGroup(pageGroup);
            pageGroupRepository.Save();
            return RedirectToAction("Index");
        }

        // GET: admin/pagegroups/delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            
            var pageGroup = pageGroupRepository.GetGroupById(id.Value);

            if (pageGroup == null)
                return NotFound();
            
            return PartialView(pageGroup);
        }

        // POST: admin/pagegroups/delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            pageGroupRepository.DeleteGroup(id);
            pageGroupRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pageGroupRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
