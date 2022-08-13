using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewInvoice.Models;
using NewInvoice.singlton;
using NewInvoice.viewmodels;

namespace NewInvoice.Controllers
{
    public class billController : Controller
    {
        DbSinglton mystring = new DbSinglton();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Addbill()
        {
            if (Session["username"] == "null")
            {
                return RedirectToAction("login", "user");
            }

            DbCon db = mystring.GitDB();
            addbill addbill = new addbill();
            addbill.users = db.users.ToList();
            ViewBag.addbill = addbill;
            ViewBag.currencies = db.currencies.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Addbill(FormCollection form)
        {
            DbCon db = mystring.GitDB();

            invoice invoice = new invoice();

            invoice.invoicenumber = form["invoicenumber"].ToString();
            invoice.value = (float)Convert.ToDouble(form["value"]);
            invoice.description = form["description"].ToString();

            int x = Convert.ToInt32(Session["id"]);
            string currencies = form["currencies"].ToString();

            invoice.creator = db.users.Find(x);
            invoice.creator_key = x;
            invoice.delete_state = 0;
            invoice.currency = db.currencies.Find(currencies);
            invoice.state = "pend";

            db.invoices.Add(invoice);
            db.SaveChanges();

            return View();
        }

        [HttpGet]
        public ActionResult addmangertobill()
        {
            if (Session["username"] == "null")
            {
                return RedirectToAction("login", "user");
            }
            DbCon db = mystring.GitDB();
            ViewBag.addbill = db.users.ToList();
            int x = Convert.ToInt32(Session["id"]);
            return View(db.invoices.Where(m=>m.creator_key==x && m.delete_state == 0).ToList());
        }


        [HttpPost]
        public ActionResult addmangertobill(string numbill, FormCollection form)
        {

            DbCon db = mystring.GitDB();
            int x = Convert.ToInt32(form["app"]);

            var apps = db.approvers.Where(m => m.invoice_key == numbill && m.user_key == x).ToList();
            if (apps == null)
            {
                return RedirectToAction("addmangertobill");
            }

            approver approver = new approver();
            approver.invoice = db.invoices.Find(numbill);
            approver.invoice_key = numbill.ToString();
            approver.decision = "pend";

            approver.user = db.users.Find(Convert.ToInt32(form["app"]));
            approver.user_key = Convert.ToInt32(form["app"]);

            db.approvers.Add(approver);
            db.SaveChanges();
            return RedirectToAction("addmangertobill");
        }

        public ActionResult done(string numbill)
        {
            DbCon db = mystring.GitDB();
            invoice invoice = db.invoices.Find(numbill);

            invoice.delete_state = 1;
            db.SaveChanges();

            return RedirectToAction("addmangertobill");
        }

    }
}