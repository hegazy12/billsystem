﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewInvoice.Models;
using NewInvoice.singlton;
using NewInvoice.viewmodels;


namespace NewInvoice.Controllers
{
    public class CurrencyController : Controller
    {
        // GET: Curency
        DbSinglton mystring = new DbSinglton();
        

        [HttpGet]
        public ActionResult Currency()
            
        {
            return View();
        }
        [HttpPost]
        public ActionResult Currency(currencies currency)
        {
            DbCon db = mystring.GitDB();
            db.currencies.Add(currency);
            db.SaveChanges();
            UserController userController = new UserController();
            return View();
        }
        public ActionResult GetAllCurrencies()
        {
            DbCon db = mystring.GitDB();
            List<currencies> currencies = db.currencies.ToList();
            return View(currencies);
        }
    }
}