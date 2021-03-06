﻿using ProiectBun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProiectBun.Controllers
{
    public class ProductsController : Controller
    {
        private ProiectBun.Models.AppContext db = new Models.AppContext();
        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products;
            ViewBag.Products = products;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }
            return View();
        }



        public ActionResult Show(int id)
        {
            Product product = db.Products.Find(id);
            ViewBag.ProductRating = GetRating(product);
            return View(product);
        }

        public ActionResult New()
        {
            Product product = new Product();
            product.Categ = GetAllCategories();

            return View(product);
        }

        [HttpPost]
        public ActionResult New(Product product)
        {
            product.Categ = GetAllCategories();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    TempData["message"] = "Produsul a fost adaugat cu succes!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(product);
                }
            }
            catch (Exception e)
            {
                return View(product);
            }
        }

        public ActionResult Edit(int id)
        {
            Product product = db.Products.Find(id);
            product.Categ = GetAllCategories();
            return View(product);
        }

        [HttpPut]
        public ActionResult Edit(int id, Product requestProduct)
        {
            requestProduct.Categ = GetAllCategories();
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = db.Products.Find(id);

                    if (TryUpdateModel(product))
                    {
                        product = requestProduct;
                        db.SaveChanges();
                        TempData["message"] = "Produsul a fost modificat cu succes!";
                        return RedirectToAction("Index");
                    }
                    return View(requestProduct);
                }
                else
                {
                    return View(requestProduct);
                }
                
            }
            catch (Exception e)
            {
                return View(requestProduct);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            TempData["message"] = "Produsul a fost sters cu succes!";
            return RedirectToAction("Index");
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllCategories()
        {
            var selectList = new List<SelectListItem>();

            var categories = from cat in db.Categories
                             select cat;

            foreach(var category in categories)
            {
                selectList.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name.ToString()
                });
            }

            return selectList;
        }
        
        [NonAction]
        public double GetRating(Product product)
        {
            var ratings = product.Reviews;
            var selectList = new List<int>();
            foreach (var rating in ratings)
            {
                selectList.Add(rating.Rating);
            }
            var productRating = selectList.Average();
            return productRating;
        }
    }
}