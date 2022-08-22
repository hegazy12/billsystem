using System;
using System.Collections.Generic;
using System.IO;
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
            ViewBag.purchaseorders = db.purchaseorders.ToList();
            ViewBag.projects = db.projects.ToList();

            ViewBag.vendors = db.vendors.ToList();
            ViewBag.manger = db.users.ToList();

            return View();
        }
     

        [HttpPost]
        public ActionResult Addbill(FormCollection form, List<string> mangers, HttpPostedFileBase[] files)
        {
            DbCon db = myconnection.GitDB();

            invoice invoice = new invoice();

            invoice.invoicenumber = form["invoicenumber"].ToString();
            invoice.value = (float)Convert.ToDouble(form["value"]);
            invoice.description = form["description"].ToString();

            int x = Convert.ToInt32(Session["id"]);
            string currencies = form["currencies"].ToString();
            int vendors = Convert.ToInt32(form["vendors"]);
            string orders = form["purchaseorders"].ToString();
            invoice.creator = db.users.Find(x);
            string projects = form["projects"].ToString();
            invoice.creator = db.users.Find(x);
            //invoice.creator_key = x;
            invoice.delete_state = 0;
            invoice.currency = db.currencies.Find(currencies);
            invoice.vendor = db.vendors.Find(vendors);
            invoice.purchaseorder = db.purchaseorders.Find(orders);
            invoice.projectnumber = db.projects.Find(projects);

            invoice.state = "pend";
            db.invoices.Add(invoice);
            db.SaveChanges();

            approver approver;
            int p;
            foreach (var ite in mangers)
            {
                approver = new approver();
                approver.invoice = invoice;
                //approver.invoice_key = invoice.invoicenumber;
                approver.decision = "pend";
                p = Convert.ToInt32(ite);
                approver.user = db.users.Find(p);
                // approver.user_key = p;
                db.approvers.Add(approver);
                db.SaveChanges();
            }
            doc doc;

            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {

                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";

                        doc = new doc();
                        doc.path = "/UploadedFiles/" + InputFileName;
                        doc.invoice = invoice;
                        db.docs.Add(doc);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Addbill");
        }
        

        }
}


    