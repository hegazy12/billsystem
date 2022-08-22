using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewInvoice.Models;

namespace NewInvoice.Controllers
{
    public class CurrenciesController : Controller
    {
        private DbCon db = new DbCon();

        // GET: Currencies
        public ActionResult Index()
        {
            return View(db.currencies.ToList());
        }

        // GET: Currencies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            currencies currencies = db.currencies.Find(id);
            if (currencies == null)
            {
                return HttpNotFound();
            }
            return View(currencies);
        }

        // GET: Currencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Currencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "symbol,name")] currencies currencies)
        {
            if (ModelState.IsValid)
            {
                db.currencies.Add(currencies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currencies);
        }

        // GET: Currencies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            currencies currencies = db.currencies.Find(id);
            if (currencies == null)
            {
                return HttpNotFound();
            }
            return View(currencies);
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "symbol,name")] currencies currencies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currencies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currencies);
        }

        // GET: Currencies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            currencies currencies = db.currencies.Find(id);
            if (currencies == null)
            {
                return HttpNotFound();
            }
            return View(currencies);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            currencies currencies = db.currencies.Find(id);
            db.currencies.Remove(currencies);
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
