using sara_system.Models;
using sara_system.singlton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sara_system.Controllers
{
    public class MyController : Controller
    {
        DbSinglton mystring = new DbSinglton();

        public ActionResult Requst()
        {
            DbCon db = mystring.GitDB();
            List<approver> approvers = new List<approver>();
            int x = Convert.ToInt32(Session["id"]);
            approvers = db.approvers.Where(m => m.user_key == x).ToList();
            return View(approvers);
        }

        [HttpGet]
        public ActionResult accept(int? id)
        {
            DbCon db = mystring.GitDB();
            approver approver = db.approvers.Find(id);
            approver.decision = "accept";
            db.SaveChanges();
            return RedirectToAction("Requst");
        }

        [HttpGet]
        public ActionResult reject(int? id)
        {
            DbCon db = mystring.GitDB();
            approver approver = db.approvers.Find(id);
            approver.decision = "reject";
            db.SaveChanges();
            return RedirectToAction("Requst");
        }

        [HttpGet]
        public ActionResult myinvoice()
        {
            DbCon db = mystring.GitDB();
            int x = Convert.ToInt32(Session["id"]);


            List<invoice> invoices = db.invoices.Where(m => m.creator_key == x).ToList();
              
            foreach(var item in invoices)
            {
                foreach(var i in item.Approver)
                {
                    if (i.decision == "accept") 
                    {
                        item.state = "accept";
                        continue;   
                    }
                    else if(i.decision == "pend") 
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

    }
}