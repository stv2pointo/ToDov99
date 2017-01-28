﻿using System;
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

        // GET: 
        public ActionResult ViewItems(int? id)
        {
            CurrentListID = id.ToString();
            return View();
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
            return View(list);
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List list = db.Lists.Find(id);
            if (list == null)
            {
                return HttpNotFound();
            }
            /*****************************************************
            add a category
            *****************************************************/
            //var Results = from c in db.Categories
            //              select new
            //              {
            //                  c.CategoryId,
            //                  c.CategoryName
            //              };
            //var MyCategories = new List<string>;

            return View(list);
        }

        // POST: Lists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ListId,ListName")] List list)
        {
            if (ModelState.IsValid)
            {
                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(list);
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
    }
}
