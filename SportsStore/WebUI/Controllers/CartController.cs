﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                //Cart = GetCart(),
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }

        //public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        //{
        //    Product product = repository.Products
        //        .FirstOrDefault(p => p.ProductID == productId);
        //    if (product != null)
        //    {
        //        //GetCart().AddItem(product, 1);
        //        cart.AddItem(product, 1);
        //    }
        //    return RedirectToAction("Index", new {returnUrl});
        //}
        public JsonResult AddToCart(Cart cart, int productId)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                //GetCart().AddItem(product, 1);
                cart.AddItem(product, 1);
            }
            return Json(cart.GetTotalCount());
        }
        //public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        //{
        //    Product product = repository.Products
        //         .FirstOrDefault(p => p.ProductID == productId);
        //    if (product != null)
        //    {
        //        //GetCart().RemoveLine(product);
        //        cart.RemoveLine(product);
        //    }
        //    return RedirectToAction("Index", new { returnUrl });
        //}

        public JsonResult RemoveFromCart(Cart cart, int productId)
        {
            Product product = repository.Products
                 .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                //GetCart().RemoveLine(product);
                cart.RemoveLine(product);
            }
            return Json(cart.GetTotalCount());
        }

        private Cart GetCart()
        {
            Cart cart =  (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private int GetCartCount(Cart cart)
        {
            return cart.GetTotalCount();
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public PartialViewResult CartSummary(Cart cart)
        {
    
            return PartialView(new CartIndexViewModel
            {
                //Cart = GetCart(),
                ReturnUrl = "",
                Cart = cart
            });
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}