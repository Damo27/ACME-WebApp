//___________________________________________________________________Controller allows for the creation of new ProductCategories_______________________________________________________________

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
    public class ProductCategoriesController : Controller
    {
        private ACME_DBEntities db = new ACME_DBEntities();

        // GET: ProductCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "category_ID,cat_name,cat_description,cat_imgUrl,File")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                string nameAndLocation = @"~/Content/Images/" + productCategory.File.FileName;
                productCategory.File.SaveAs(Server.MapPath(nameAndLocation));
                productCategory.cat_imgUrl = productCategory.File.FileName;
                productCategory.category_ID = Guid.NewGuid();
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);
            }

            return View(productCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: ProductCategories
        //public ActionResult Index()
        //{
        //    return View(db.ProductCategories.ToList());
        //}

        // GET: ProductCategories/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProductCategory productCategory = db.ProductCategories.Find(id);
        //    if (productCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(productCategory);
        //}


        // GET: ProductCategories/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProductCategory productCategory = db.ProductCategories.Find(id);
        //    if (productCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(productCategory);
        //}

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "category_ID,cat_name,cat_description,cat_imgUrl")] ProductCategory productCategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(productCategory).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(productCategory);
        //}

        // GET: ProductCategories/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProductCategory productCategory = db.ProductCategories.Find(id);
        //    if (productCategory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(productCategory);
        //}

        //// POST: ProductCategories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    ProductCategory productCategory = db.ProductCategories.Find(id);
        //    db.ProductCategories.Remove(productCategory);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}
