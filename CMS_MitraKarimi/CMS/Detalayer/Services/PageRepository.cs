using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PageRepository : IPageRepository
    {
        private MyCmsContext db;

        public PageRepository(MyCmsContext context)
        {
            this.db = context;
        }
        public IEnumerable<Page> GetAllPage()
        {
            return db.Pages;
        }

        public Page GetPageById(int pageId)
        {
            return db.Pages.Find(pageId);
        }

        public bool InsertPage(Page page)
        {
            try
            {
                db.Pages.Add(page);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool UpdatePage(Page page)
        {
            try
            {
                db.Entry(page).State=EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool DeletePage(Page page)
        {
            try
            {
                db.Entry(page).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool DeletePage(int pageId)
        {
            try
            {
                var page = GetPageById(pageId);
                DeletePage(page);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Page> TopNews(int take = 4)
        {
            return db.Pages.OrderByDescending(p => p.Visit).Take(take);
        }

        public IEnumerable<Page> PagesInSlider()
        {
            return db.Pages.Where(p => p.ShowInSlider == true);
        }

        public IEnumerable<Page> LastNews(int take = 4)
        {
            return db.Pages.OrderByDescending(p => p.CreateDate).Take(take);
        }

        public IEnumerable<Page> ShowPageByGroupId(int groupId)
        {
            return db.Pages.Where(p => p.GroupID == groupId);
        }

        public IEnumerable<Page> SearchPage(string search)
        {
            return
                db.Pages.Where(
                    p =>
                        p.Title.Contains(search) || p.ShortDescription.Contains(search) || p.Tags.Contains(search) ||
                        p.Text.Contains(search)).Distinct();
        }


        public void Dispose()
        {
          db.Dispose();
        }

        public string GenerateSavePath(string fileName)
        {
            string appRootPath = AppDomain.CurrentDomain.BaseDirectory;
            string savePath = Path.Combine(appRootPath, "test", Guid.NewGuid().ToString() + Path.GetExtension(fileName));
            return savePath;
        }


        public void SaveImage(FileStream image)
        {
            string savePath = GenerateSavePath(image.Name); 
            using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

        }
    }
}
