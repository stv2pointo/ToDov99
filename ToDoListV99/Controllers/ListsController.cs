using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoListV99.Models;

namespace ToDoListV99.Controllers
{
    public class ListsController : Controller
    {

        private MyDbContext db = new MyDbContext();
        protected static string CurrentListID { get; set; }


        // GET: Lists
        public ActionResult Index()
        {
            return View(db.Lists.ToList());
        }

        public ActionResult BuildListTable()
        {
            return PartialView("_ListTable",db.Lists.ToList());
        }

        public ActionResult BuildItemTable()
        {
            return PartialView("_ItemTable", GetMyItems());
        }

        public IEnumerable<Item> GetMyItems()
        {
            List currentList = db.Lists.FirstOrDefault
                (x => x.ListId.ToString() == CurrentListID);
            return db.Items.ToList().Where(x => x.List.ListId == currentList.ListId);

        }

        // GET: Lists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = db.Lists.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }
            //return View(list);
            var Results = from c in db.Categories
                          select new
                          {
                              c.CategoryId,
                              c.CategoryName,
                              Checked = ((from ab in db.ListsToCategories
                                          where (ab.ListId == id) & (ab.CategoryId == c.CategoryId)
                                          select ab).Count() > 0)
                          };

            var MyViewModel = new ListsViewModel();

            MyViewModel.ListId = id.Value;
            MyViewModel.ListName = list.ListName;

            var MyCheckBoxList = new List<CheckBoxViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.CategoryId, Name = item.CategoryName, Checked = item.Checked });
            }

            MyViewModel.Categories = MyCheckBoxList;


            return View(MyViewModel);
        }

        // GET: Lists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ListId,ListName")] List list)
        {
            if (ModelState.IsValid)
            {
                db.Lists.Add(list);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(list);
        }



        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreateItem([Bind(Include = "ItemID,Description")] Item item)
        {
            if (ModelState.IsValid)
            {

                List currentList = db.Lists.FirstOrDefault
                    (x => x.ListId.ToString() == CurrentListID);
                item.List = currentList;
                item.IsComplete = false;
                db.Items.Add(item);
                db.SaveChanges();
            }

            return PartialView("_ItemTable", GetMyItems());
        }


        // GET: Lists/Edit/5
        public ActionResult Edit(int? id)
        {
            CurrentListID = id.ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = db.Lists.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }


            

            return View(list);

           /* var Results = from c in db.Categories
                          select new
                          {
                              c.CategoryId,
                              c.CategoryName,
                              Checked = ((from ab in db.ListsToCategories
                                          where (ab.ListId == id) & (ab.CategoryId == c.CategoryId)
                                          select ab).Count() > 0)
                          };

            var MyViewModel = new ListsViewModel();

            MyViewModel.ListId = id.Value;
            MyViewModel.ListName = list.ListName;

            var MyCheckBoxList = new List<CheckBoxViewModel>();

            foreach (var item in Results)
            {
                MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.CategoryId, Name = item.CategoryName, Checked = item.Checked });
            }

            MyViewModel.Categories = MyCheckBoxList;


            return View(MyViewModel);*/

        }

        // POST: Lists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ListsViewModel list)
        {
            if (ModelState.IsValid)
            {
                var MyList = db.Lists.Find(list.ListId);

                MyList.ListName = list.ListName;

                foreach (var item in db.ListsToCategories)
                {
                    if (item.ListId == list.ListId)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (var item in list.Categories)
                {
                    if (item.Checked)
                    {
                        db.ListsToCategories.Add(new ListToCategory() { ListId = list.ListId, CategoryId = item.Id });
                    }
                }
                //db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(list);
        }
   

        // POST: Lists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult AJAXEditItem(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                item.IsComplete = value;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_ItemTable", GetMyItems());
            }
        }
       


        // GET: Lists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = db.Lists.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }
            return View(list);
        }

        // POST: Lists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List list = db.Lists.Find(id);
            db.Lists.Remove(list);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /************* add categories to a list **********************/

        // GET:
        public ActionResult AddCategoriesToAList(int? id)
        {
            CurrentListID = id.ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = db.Lists.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }

            var Results = from c in db.Categories
                           select new
                           {
                               c.CategoryId,
                               c.CategoryName,
                               Checked = ((from ab in db.ListsToCategories
                                           where (ab.ListId == id) & (ab.CategoryId == c.CategoryId)
                                           select ab).Count() > 0)
                           };

             var MyViewModel = new ListsViewModel();

             MyViewModel.ListId = id.Value;
             MyViewModel.ListName = list.ListName;

             var MyCheckBoxList = new List<CheckBoxViewModel>();

             foreach (var item in Results)
             {
                 MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.CategoryId, Name = item.CategoryName, Checked = item.Checked });
             }

             MyViewModel.Categories = MyCheckBoxList;


             return View(MyViewModel);

        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategoriesToAList(ListsViewModel list)
        {
            if (ModelState.IsValid)
            {
                var MyList = db.Lists.Find(list.ListId);

                MyList.ListName = list.ListName;

                foreach (var item in db.ListsToCategories)
                {
                    if (item.ListId == list.ListId)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (var item in list.Categories)
                {
                    if (item.Checked)
                    {
                        db.ListsToCategories.Add(new ListToCategory() { ListId = list.ListId, CategoryId = item.Id });
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(list);
        }
        /*********** end add categories to a list ********************/
    }
}
