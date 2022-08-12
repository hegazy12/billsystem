using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sara_system.Models;
using sara_system.singlton;
using sara_system.viewmodels;

namespace sara_system.Controllers
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
            DbCon db = mystring.GitDB();
            addbill addbill = new addbill();
            addbill.users = db.users.ToList();
            ViewBag.addbill = addbill;
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
            db.invoices.Add(invoice);
            db.SaveChanges();

            return View();
        }

        [HttpGet]
        public ActionResult addmangertobill()
        {
            DbCon db = mystring.GitDB();
            ViewBag.addbill = db.users.ToList();
            return View(db.invoices.ToList());
        }

        
        [HttpPost]
        public ActionResult addmangertobill(string numbill , FormCollection form)
        {
            DbCon db = mystring.GitDB();
            
            approver approver = new approver();
            approver.invoice = numbill;
            approver.user = Convert.ToInt32(form["app"]);
            db.approvers.Add(approver);
            db.SaveChanges();
            return RedirectToAction("addmangertobill");
        }
    }
}