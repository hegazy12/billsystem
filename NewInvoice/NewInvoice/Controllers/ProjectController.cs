using NewInvoice.singlton;
using NewInvoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewInvoice.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        DbSinglton myconnection = new DbSinglton();
        [HttpGet]
        public ActionResult Project()
        {
            DbCon db = myconnection.GitDB();

            ViewBag.users = db.users.ToList();

            return View("Project");
            
        }
        [HttpPost]
        public ActionResult Project(project project, FormCollection form)
        {
            DbCon db = myconnection.GitDB();
            string users = form["user"].ToString();
            project.projectmang = db.users.Where(m => m.name == users).FirstOrDefault();
            db.projects.Add(project);
            db.SaveChanges();
            return RedirectToAction("Project");

        }
        [HttpGet]
        public ActionResult GetAllProjects()
        {
            DbCon db = myconnection.GitDB();
            List<project> projects = db.projects.ToList();


            return View(projects);

        }
    
}
}