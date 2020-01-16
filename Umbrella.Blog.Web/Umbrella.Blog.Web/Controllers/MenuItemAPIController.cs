using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbrella.Blog.BLL;
using Umbrella.Blog.Models;

namespace Umbrella.Blog.Web.Controllers
{
    public class MenuItemAPIController : ApiController
    {
        private MenuItemManager _menuManager = new MenuItemManager();


        [Route("api/menuitems/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult getAllMenuItems()
        {

            List<MenuItem> found = _menuManager.GetAllMenuItems().Data;
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }
        [Authorize(Roles = "Admin,Employee")]
        [Route("api/menuitems/add")]
        [AcceptVerbs("POST")]
        public void AddTag(MenuItem newitem)
        {
            _menuManager.CreateMenuItem(newitem);

        }
        [Authorize(Roles = "Admin,Employee")]
        [Route("api/menuitems/delete/{id}")]
        [AcceptVerbs("DELETE")]
        public void DeleteMenuItem(int id)
        {
            
            _menuManager.DeleteMenuItem(id);

        }
        [Authorize(Roles = "Admin,Employee")]
        [Route("api/menuitems/edit/{id}")]
        [AcceptVerbs("PUT")]
        public void UpdateMenuItem(int id,MenuItem updateditem)
        {

            _menuManager.UpdateMenuItem(updateditem);

        }
    }
}
