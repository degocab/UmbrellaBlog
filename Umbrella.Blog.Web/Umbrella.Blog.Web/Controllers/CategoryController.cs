using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using Umbrella.Blog.BLL;
using Umbrella.Blog.Models;
using Umbrella.Blog.Web.Models;

namespace Umbrella.Blog.Web.Controllers
{
    public class CategoryController : Controller
    {

        CategoryManager categoryManager = new CategoryManager();
        [System.Web.Mvc.Authorize(Roles = "Admin,Employee")]
        public ActionResult Manage()
        {
            CategoryVM viewModel = new CategoryVM();
            IEnumerable<Category> s = categoryManager.GetAllCategories().Data; ;
            
            ViewBag.Message = "Category Edit Page";

            return View(s);
        }
        [System.Web.Mvc.Authorize(Roles = "Admin,Employee")]
        [System.Web.Mvc.AcceptVerbs("POST")]
        public ActionResult Edit(Category catmodel)
        {
            CategoryVM viewModel = new CategoryVM();
            IEnumerable<Category> s = categoryManager.GetAllCategories().Data; ;


            return View(s);
        }
    }
}
