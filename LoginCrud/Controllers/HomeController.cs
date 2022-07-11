using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginCrud.DbCon;
using System.Data.Entity;

namespace LoginCrud.Controllers
{
    public class HomeController : Controller
    {
        LoginCrudEntities db = new LoginCrudEntities();
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            var emp = db.tbl_employee.ToList();
            return View(emp);
        }

        [Authorize]
        public ActionResult Create(int id = 0)
        {
            if(id == 0)
            {
                TempData["Title"] = "Add Employee";
            }
            else
            {
                var udata = db.tbl_employee.Where(m => m.id == id).FirstOrDefault();
                TempData["Title"] = "Update";
                return View(udata);
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(tbl_employee emp)
        {
            if (emp.id == 0)
            {
                
                db.tbl_employee.Add(emp);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["AMessage"] = "<script>alert('Added Successfully...')</script>";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["AMessage"] = "<script>alert('Not Added...')</script>";
                }
                
            }
            else
            {
                db.Entry(emp).State = EntityState.Modified;
                int b = db.SaveChanges();
                if (b > 0)
                {
                    TempData["UMessage"] = "<script>alert('Updated Successfully...')</script>";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["UMessage"] = "<script>alert('Not Updated...')</script>";
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var delItem = db.tbl_employee.Where(m => m.id == id).FirstOrDefault();
            db.tbl_employee.Remove(delItem);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}