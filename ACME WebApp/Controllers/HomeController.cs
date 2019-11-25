//___________________________________________________________HomeController is used for authentication(Login, authLogin and logout) and loading the home page____________________________________________

using ACME_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACME_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private ACME_DBEntities db = new ACME_DBEntities();

        public ActionResult Index()
        {
            var categories = db.ProductCategories;
            return View(categories);
        }


        public ActionResult Login(Guid? id)
        {
            if (id != null)
            {
                ViewBag.ID = id;
            }          
            return View();
        }

        [HttpPost]
        public ActionResult AuthoriseLogin(ACME_WebApp.Models.User user, Guid? id)
        {
            //__________Following code sets an onject of user class to the user in model whose details match user input_____________
            var userDetails = db.Users.Where(m => m.user_Name == user.user_Name && m.user_Password == user.user_Password).FirstOrDefault();
            if (userDetails == null)
            {
                user.ErrorMessage = "Incorrect Username and Password combination";
                return View("Login", user);
            }
            else
            {
                //______following code allows only users into application___________________
                    UserSingleton.user = userDetails;
                    Session["user"] = userDetails;
                    Session["userID"] = userDetails.user_ID;
                    Session["userName"] = userDetails.user_Name;

                    if (id != null)
                    {
                        return RedirectToAction("Create", "Purchases", new { id});
                    }
                    return RedirectToAction("Index", "Products", null);
            }
        }
        //______________Method to Logout of Session__________________
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}