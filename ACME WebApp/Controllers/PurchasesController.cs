//______________Controller allows for the creation of new Purchases, as well as access to the viewing of all purchases, for employees, and_____________
//______________user specific purchases by each user_______________________________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ACME_WebApp.Models;

namespace ACME_WebApp.Controllers
{
    public class PurchasesController : Controller
    {
        private ACME_DBEntities db = new ACME_DBEntities();

        // GET: Purchases
        public ActionResult Index(Guid? id)
        {
            var purchases = db.Purchases.Include(p => p.Product).Include(p => p.User);
            if (UserSingleton.user != null && !UserSingleton.user.isEmployee)
            {
                purchases = purchases.Where(x => x.user_ID == id);
            }
            
            return View(purchases.ToList());
        }


        // GET: Purchases/Create
        public ActionResult Create(Guid id)
        {
            var prod = db.Products.Where(p => p.product_ID == id).FirstOrDefault();
            Purchase purchase = new Purchase { Product = prod, product_ID = prod.product_ID, User = UserSingleton.user, user_ID = UserSingleton.user.user_ID};
            
            return View(purchase);
        }

        // POST: Purchases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "purchase_ID,user_ID,product_ID,date")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                purchase.purchase_ID = Guid.NewGuid();
                purchase.date = DateTime.Now;
                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_ID = new SelectList(db.Products, "product_ID", "prod_name", purchase.product_ID);
            ViewBag.user_ID = new SelectList(db.Users, "user_ID", "user_Name", purchase.user_ID);
            return View(purchase);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //// GET: Purchases/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Purchase purchase = db.Purchases.Find(id);
        //    if (purchase == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(purchase);
        //}

        //// GET: Purchases/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Purchase purchase = db.Purchases.Find(id);
        //    if (purchase == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.product_ID = new SelectList(db.Products, "product_ID", "prod_name", purchase.product_ID);
        //    ViewBag.user_ID = new SelectList(db.Users, "user_ID", "user_Name", purchase.user_ID);
        //    return View(purchase);
        //}

        //// POST: Purchases/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "purchase_ID,user_ID,product_ID")] Purchase purchase)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(purchase).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.product_ID = new SelectList(db.Products, "product_ID", "prod_name", purchase.product_ID);
        //    ViewBag.user_ID = new SelectList(db.Users, "user_ID", "user_Name", purchase.user_ID);
        //    return View(purchase);
        //}

        //// GET: Purchases/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Purchase purchase = db.Purchases.Find(id);
        //    if (purchase == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(purchase);
        //}

        //// POST: Purchases/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Purchase purchase = db.Purchases.Find(id);
        //    db.Purchases.Remove(purchase);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
