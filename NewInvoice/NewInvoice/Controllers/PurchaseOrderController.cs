using NewInvoice.singlton;
using NewInvoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewInvoice.Controllers
{
    public class PurchaseOrderController : Controller
    {
        // GET: PurchaseOrder
        DbSinglton myconnection = new DbSinglton();
        [HttpGet]
        public ActionResult PurchaseOrder()
        {

            return View("PurchaseOrder");

        }
        [HttpPost]
        public ActionResult PurchaseOrder(purchaseorder purchaseorder)
        {
            DbCon db = myconnection.GitDB();
            db.purchaseorders.Add(purchaseorder);
            db.SaveChanges();
            return View("PurchaseOrder");

        }
        public ActionResult GetAllPurchaseOrders()
        {
            DbCon db = myconnection.GitDB();
            List<purchaseorder> orders = db.purchaseorders.ToList();


            return View(orders);

        }
    }

}
