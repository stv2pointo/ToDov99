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
    public class ListViewController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: ListView
        public ActionResult Index()
        {
            return View(db.Lists.ToList());
        }

        // GET: ListView/Details/5
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

        // GET: ListView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListView/Create
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

        // GET: ListView/Edit/5
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
            return View(list);
        }

        // POST: ListView/Edit/5
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

        // GET: ListView/Delete/5
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

        // POST: ListView/Delete/5
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
