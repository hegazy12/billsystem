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
        DbSinglton myconnection = new DbSinglton();

        

        [HttpGet]
        public ActionResult Addbill()
        {
            if (Session["username"] == "null")
            {
                return RedirectToAction("login", "user");
            }

            DbCon db = myconnection.GitDB();
            addbill addbill = new addbill();
            addbill.users = db.users.ToList();
            
            ViewBag.addbill = addbill;
            ViewBag.currencies = db.currencies.ToList();
            ViewBag.vendors = db.vendors.ToList();
            ViewBag.manger = db.users.ToList();
            return View();
        }






        [HttpPost]
        public ActionResult Addbill(FormCollection form, List<string> mangers)
        {
            DbCon db = myconnection.GitDB();

            invoice invoice = new invoice();

            invoice.invoicenumber = form["invoicenumber"].ToString();
            invoice.value = (float)Convert.ToDouble(form["value"]);
            invoice.description = form["description"].ToString();

            int x = Convert.ToInt32(Session["id"]);
            string currencies = form["currencies"].ToString();
            int vendors =Convert.ToInt32(form["vendors"]);

            invoice.creator = db.users.Find(x);
            invoice.creator_key = x;
            invoice.delete_state = 0;
            invoice.currency = db.currencies.Find(currencies);
            invoice.vendor = db.vendors.Find(vendors);
            invoice.state = "pend";
            db.invoices.Add(invoice);
            db.SaveChanges();

            approver approver;
            int p;
            foreach (var ite in mangers)
            {
                approver = new approver();
                approver.invoice = invoice;
                approver.invoice_key = invoice.invoicenumber;
                approver.decision = "pend";
                p = Convert.ToInt32(ite);
                approver.user = db.users.Find(p);
                approver.user_key = p;
                db.approvers.Add(approver);
                db.SaveChanges();
            }
            return RedirectToAction("Addbill");
        }
    }
}