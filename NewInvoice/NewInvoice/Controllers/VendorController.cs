using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewInvoice.singlton;
using NewInvoice.Models;

namespace NewInvoice.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        DbSinglton mystring = new DbSinglton();
        [HttpGet]
        public ActionResult Vendor()
        {
            
            return View("Vendor");
        }
        [HttpPost]
        public ActionResult Vendor(vendor vendor)
        {
            DbCon db = mystring.GitDB();

            db.vendors.Add(vendor);
            db.SaveChanges();
            return View("Vendor");
        }
        public ActionResult GetAllVendors()
        {
            DbCon db = mystring.GitDB();
            List<vendor> vendors = db.vendors.ToList();
            return View(vendors);
        }
    }
}