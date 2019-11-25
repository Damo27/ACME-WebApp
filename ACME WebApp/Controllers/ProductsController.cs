//______________Controller allows for the creation of new products, as well as access to the viewing of all products_____________

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
    public class ProductsController : Controller
    {
        private ACME_DBEntities db = new ACME_DBEntities();

        // GET: Products
        public ActionResult Index(Guid? id)
        {
            var categories = db.ProductCategories;
            var products = db.Products.Where(p => p.category_ID == db.ProductCategories.FirstOrDefault().category_ID).Include(p => p.ProductCategory);
            if (id != null)
            {
                products = db.Products.Where(p => p.category_ID == id).Include(p => p.ProductCategory);
            }
            
            ViewBag.Category = new SelectList(categories, "category_ID", "cat_name");
            return View(products.ToList());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.category_ID = new SelectList(db.ProductCategories, "category_ID", "cat_name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_ID,prod_name,prod_description,prod_price,prod_imgUrl,category_ID, File")] Product product)
        {
            if (ModelState.IsValid)
            {
                string nameAndLocation = @"~/Content/Images/" + product.File.FileName;
                product.File.SaveAs(Server.MapPath(nameAndLocation));
                product.prod_imgUrl = product.File.FileName;
                product.product_ID = Guid.NewGuid();
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_ID = new SelectList(db.ProductCategories, "category_ID", "cat_name", product.category_ID);
            return View(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //___________PartialView to populate Target div on change of department selection______________________________
        public PartialViewResult ProductsByCategory(Guid id, String search)
        {
            var products = db.Products.Where(p => p.category_ID == id).ToList();

            if (search != null)
            {
                products = products.Where(p => p.prod_name.Contains(search)).ToList();
                if (!products.Any())
                {
                    ProductCategory productCategory = new ProductCategory { category_ID = id };
                    products.Add(new Product { ProductCategory = productCategory });
                }
            }

            return PartialView(products);
        }

        //// GET: Products/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// GET: Products/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.category_ID = new SelectList(db.ProductCategories, "category_ID", "cat_name", product.category_ID);
        //    return View(product);
        //}

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "product_ID,prod_name,prod_description,prod_price,prod_imgUrl,category_ID")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.category_ID = new SelectList(db.ProductCategories, "category_ID", "cat_name", product.category_ID);
        //    return View(product);
        //}

        //// GET: Products/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    Product product = db.Products.Find(id);
        //    db.Products.Remove(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}
