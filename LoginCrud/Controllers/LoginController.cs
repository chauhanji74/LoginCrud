using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginCrud.DbCon;
using System.Web.Security;

namespace LoginCrud.Controllers
{
    public class LoginController : Controller
    {
        LoginCrudEntities db = new LoginCrudEntities();
        // GET: Login
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(tbl_user si)
        {
            var data = db.tbl_user.Where(m => m.email == si.email).FirstOrDefault();
            if(data != null)
            {
                if(data.password == si.password)
                {
                    FormsAuthentication.SetAuthCookie(si.email, false);
                    Session["m"] = "hi";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Wrong Password";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid Email";
            }
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(tbl_user su)
        {
            if (ModelState.IsValid == true)
            {
                db.tbl_user.Add(su);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["RMessage"] = "<script>alert('Registered Successfully...')</script>";
                    return RedirectToAction("Signin", "Login");
                }
                else
                {
                    TempData["RMessage"] = "<script>alert('Not Registered...')</script>";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["m"] = "bye";
            return RedirectToAction("Signin", "Login");
        }
    }
}