using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbrella.Blog.BLL;
using Umbrella.Blog.Web.Models;

namespace Umbrella.Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Blog()
        {
            ViewBag.Message = "Your Blog page.";

            return View();
        }
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult UsersPage()
        {         
            ViewBag.Message = "Manage Users";
            return View();
        }
        public ActionResult BlogPost(int id)
        {

            return View();
        }
 
    }
}