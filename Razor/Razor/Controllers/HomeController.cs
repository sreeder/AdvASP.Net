﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Razor.Models;
using System.Web.Mvc;

namespace Razor.Controllers
{
    public class HomeController : Controller
    {
        private Product myProduct = new Product
        {
            ProductID = 1,
            Name = "Kayak",
            Description = "A boat for one person",
            Category= "Watersports",
            Price = 275M

        };


    // GET: Home
        public ActionResult Index()
        {
            return View(myProduct);
        }

        public ActionResult NameAndPrice()
        {
            return View(myProduct);
        }

        public ActionResult DemoExpression()
        {
            ViewBag.ProductCount = 1;
            ViewBag.ExpressShip = true;
            ViewBag.ApplyDiscount = false;
            ViewBag.Supplier = null;

            return View(myProduct);
        }

        public ActionResult DemoArray()
        {
            Product[] array =
            {
                new Product {Name = "Kayak", Price = 275M },
                new Product {Name = "LifeJacket", Price= 48.95M },
                new Product {Name = "Soccor ball", Price= 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M }


            };
            return View(array);

        }
    }
}