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
        
        [HttpGet]
        public ActionResult Requst()
        {
            if (Session["username"] == "null")
            {
                return RedirectToAction("login", "user");
            }
           
            DbCon db = myconnection.GitDB();

            int x = Convert.ToInt32(Session["id"]);
            user u = db.users.Find(x);

            List<approver> approvers = db.approvers.Where(m => m.user.id == u.id && m.decision == "pend" && m.invoice.state != "reject").ToList();
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


        [HttpGet]
        public ActionResult Editmyinvoice(string numinvoice)
        {
            DbCon db = myconnection.GitDB();

            invoice u = db.invoices.Find(numinvoice);
            ViewBag.currencies = db.currencies.ToList();
            ViewBag.purchaseorders = db.purchaseorders.ToList();
            ViewBag.projects = db.projects.ToList();

            ViewBag.vendors = db.vendors.ToList();
            ViewBag.manger = db.users.ToList();
            return View(u);
        }

        [HttpGet]
        public ActionResult deleteapprover(int id)
        {
            DbCon db = myconnection.GitDB();
            var approver = db.approvers.Find(id);
            string str = approver.invoice.invoicenumber;
            db.approvers.Remove(approver);
            db.SaveChanges();
            return Redirect("~/my/Editmyinvoice?numinvoice=" + str);
        }

        [HttpPost]
        public ActionResult Editmyinvoice(FormCollection form, List<string> mangers, HttpPostedFileBase[] files)
        {
            DbCon db = myconnection.GitDB();
            invoice invoice = db.invoices.Find(form["invoicenumber"].ToString());
            
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
            return RedirectToAction("myinvoice");
        }

        public ActionResult deletedoc(int? id)
        {
            DbCon db   =  myconnection.GitDB();
            var doc    =  db.docs.Find(id);
            string str =  doc.invoice.invoicenumber;
            db.docs.Remove(doc);
            db.SaveChanges();
            return Redirect("~/my/Editmyinvoice?numinvoice="+str);
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