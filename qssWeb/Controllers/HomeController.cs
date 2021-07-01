using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using qssWeb.Models;

namespace qssWeb.Controllers
{
    public class HomeController : Controller
    {
        private QssDBContext db = new QssDBContext();
        public ActionResult Index()
        {
            Response.Redirect("/User/");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";
            
            var employees = from u in db.Users orderby u.userId select u;

            var users = db.Users;
            
            
            ViewData["users"] = users;


            
            Console.WriteLine(users);
            return View();

            //return View();
        }
    }
}