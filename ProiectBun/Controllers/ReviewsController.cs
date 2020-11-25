using ProiectBun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProiectBun.Controllers
{
    public class ReviewsController : Controller
    {
        private Models.AppContext db = new Models.AppContext();
        // GET: Reviews
        
        public ActionResult Index(int id)
        {
            return View();
        }
        
        //Get: Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Review rev = db.Reviews.Find(id);
            db.Reviews.Remove(rev);
            return Redirect("/Products/Show/" + rev.ProductId);
        }

        [HttpPost]
        public ActionResult New(Review rev)
        {
            rev.Date = DateTime.Now;
            try
            {
                db.Reviews.Add(rev);
                db.SaveChanges();
                return Redirect("/Products/Show/" + rev.ProductId);
            }

            catch (Exception e)
            {
                return Redirect("/Products/Show/" + rev.ProductId);
            }
        }
        //GET: New
        public ActionResult Edit(int id)
        {
            Review review = db.Reviews.Find(id);
            return View(review);
        }

        [HttpPut]
        public ActionResult Edit(int id, Review requestReview)
        {
            try
            {
                Review review = db.Reviews.Find(id);
                if (TryUpdateModel(review))
                {

                    review = requestReview;
                    review.Date = DateTime.Now;
                    db.SaveChanges();
                }
                return Redirect("/Product/Show/" + review.ProductId);
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }

}