﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sara_system.singlton;
using sara_system.Models;
using System.Security.Cryptography;

namespace sara_system.Controllers
{
    public class UserController : Controller
    {

        private DbSinglton DbSinglton = new DbSinglton();

        [HttpGet]
        public ActionResult login()
        {
            Session["username"] = "null";
            return View();
        }

        [HttpPost]
        public ActionResult login(string username, string password)
        {
            DbCon db = DbSinglton.GitDB();
            user user  = db.users.Where(
                m => m.email == username && m.password == password
                ).FirstOrDefault();
            
            if(user== null)
            {
                Session["username"] = "null";
                return View();
            }

            Session["username"] = user.email;
            Session["id"] = user.id;

            return View();
        }



        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signup(user user)
        {
            DbCon db = DbSinglton.GitDB();

            if (db.users.Where(m=> m.email == user.email).FirstOrDefault() != null )
            {
                ViewBag.mss = "your email exists"; 
                return View();
            }
            db.users.Add(user);
            db.SaveChangesAsync();
            return View();
        }
    }
}