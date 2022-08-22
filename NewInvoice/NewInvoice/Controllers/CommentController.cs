using NewInvoice.Models;
using NewInvoice.singlton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewInvoice.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        DbSinglton DbSinglton = new DbSinglton();
        
        [HttpPost]
        public ActionResult AddComment(FormCollection form )
        {
            DbCon db = DbSinglton.GitDB();
            comment comment = new comment();
            string innber = form["in"].ToString();
            comment.invoice = db.invoices.Find(innber);
            comment.content = form["comm"].ToString(); ;
            db.comments.Add(comment);
            db.SaveChanges();
            return Redirect("/my/details?invoicenumber="+innber);
        }
    }
}