using NewInvoice.Models;
using NewInvoice.singlton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewInvoice.Controllers;
using System.IO;

namespace NewInvoice.Controllers
{
    public class MyController : Controller
    {
        DbSinglton myconnection = new DbSinglton();
        
        
        public ActionResult Requst(invoice invoice)
        {
            if (Session["username"] == "null")
            {
                return RedirectToAction("login", "user");
            }
           
            DbCon db = myconnection.GitDB();
            List<approver> approvers = new List<approver>();
            int x = Convert.ToInt32(Session["id"]);
            user u = db.users.Find(x);
           
            approvers = db.approvers.Where(m => m.user.id == u.id && m.decision == "pend" && m.invoice.state != "reject").ToList();
            return View(approvers);
        }
       

        [HttpGet]
        public ActionResult accept(int? id)
        {

            DbCon db = myconnection.GitDB();
            approver approver = db.approvers.Find(id);
            approver.decision = "accept";

            db.SaveChanges();
            return RedirectToAction("Requst");
        }

        [HttpGet]
        public ActionResult reject(int? id)
        {
            DbCon db = myconnection.GitDB();
            approver approver = db.approvers.Find(id);
            approver.decision = "reject";
            approver.invoice.state = "reject";
            db.SaveChanges();
            return RedirectToAction("Requst");
        }

        [HttpGet]
        public ActionResult myinvoice()
        {
            if (Session["username"] == "null")
            {
                return RedirectToAction("login", "user");
            }
            DbCon db = myconnection.GitDB();
            int x = Convert.ToInt32(Session["id"]);

            user u = db.users.Find(x);
            List<invoice> invoices = db.invoices.Where(m => m.creator.id == u.id).ToList();

            foreach (var item in invoices)
            {
                foreach (var i in item.Approver)
                {
                    if (i.decision == "accept")
                    {
                        item.state = "accept";
                        continue;
                    }
                    else if (i.decision == "pend")
                    {
                        item.state = "pend";
                        break;
                    }
                    else if (i.decision == "reject")
                    {
                        item.state = "reject";
                        break;
                    }
                }

                db.SaveChanges();
            }

            return View(invoices);
        }



        [HttpGet]
        public ActionResult Details(string invoicenumber)
        {
            DbCon db = myconnection.GitDB();
            invoice u = db.invoices.Find(invoicenumber);

            return View(u);
        }



        public ActionResult OpenPDF(int? id)
        {
            DbCon db = myconnection.GitDB();
            doc doc = new doc(); 
            doc= db.docs.Find(id);
            string filepath = Server.MapPath(Path.Combine("~"+doc.path));
            return File(filepath, "application/pdf");
        }
    } 
}