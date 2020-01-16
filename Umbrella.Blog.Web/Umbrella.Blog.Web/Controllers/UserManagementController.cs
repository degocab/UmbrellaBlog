using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


using System.Web.Mvc;
using System.Web.Security;
using Umbrella.Blog.Web.Models;

namespace Umbrella.Blog.Web.Controllers
{
    //[CustomAuthorization(Rule = "Admin")]
    public class UserManagementController : Controller
    {
        protected ApplicationDbContext db { get; set; }
        private ApplicationUserManager _userManager;

        public UserManagementController()
        {
            this.db = new ApplicationDbContext();
        }

        public UserManagementController(ApplicationUserManager userManager)
        {
            UserManager = userManager; 
        }
        
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("SetEmployee")]
        public async Task<ActionResult> SetEmployee(string id)
        {
            await SetRoleAsync(id);
            return RedirectToAction("ListUser", "Account");

        }
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("RevokeEmployee")]
        public async Task<ActionResult> RevokeEmployee(string id)
        {
            await RevokeRoleAsync(id);
            return RedirectToAction("ListUser", "Account");

        }

        [System.Web.Http.HttpPost, System.Web.Http.ActionName("DeleteConfirmed")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            //if (UserManager.IsInRole(id, "Admin"))
            //{
            //    ModelState.AddModelError("", "Failed to delete user");
            //    //Session.Abandon();
            //}
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            if (applicationUser == null || UserManager.IsInRole(id,"Admin"))
            {
                ModelState.AddModelError("", "Failed to find User ID for deletion.");
            }
            else
            {
                IdentityResult result = await UserManager.DeleteAsync(applicationUser);
                if (result.Succeeded)
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("ListUser", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to Delete User.");
                    var errors = string.Join(",", result.Errors);
                    ModelState.AddModelError("", errors);
                }
            }
         
          
            return RedirectToAction("ListUser","Account");
        }
        public async Task<ActionResult> SetRoleAsync(string id)
        {
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            if (applicationUser != null)
            {
                UserManager.AddToRole(applicationUser.Id, "Employee");
                return RedirectToAction("ListUser", "Account");
               
            }
         






            return RedirectToAction("ListUser","Account");
        }
        public async Task<ActionResult> RevokeRoleAsync(string id)
        {
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            if (applicationUser != null)
            {
                await UserManager.RemoveFromRoleAsync(applicationUser.Id, "Employee");
                return RedirectToAction("ListUser", "Account");

            }
            return RedirectToAction("ListUser", "Account");
        }
    }
}