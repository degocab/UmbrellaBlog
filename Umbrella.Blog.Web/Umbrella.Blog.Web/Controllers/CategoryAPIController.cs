﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbrella.Blog.BLL;
using Umbrella.Blog.Models;

namespace Umbrella.Blog.Web.Controllers
{
    public class CategoryAPIController : ApiController
    {
        private CategoryManager categoryManager = new CategoryManager();
        [Authorize(Roles = "Admin,Employee")]
        [Route("api/categories/")]
        [AcceptVerbs("POST")]
        public void AddCategory(Category category)
        {
            categoryManager.CreateCategory(category);
        }
        [Route("api/categories/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetPost(int id)
        {
            Category found = categoryManager.GetCategoryById(id).Data;
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }
        [Route("api/categories")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllCategories()
        {
            List<Category> found = categoryManager.GetAllCategories().Data;
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }
        [Authorize(Roles = "Admin,Employee")]
        [Route("api/categories/{id}")]
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            if(id != 1)
            {
                categoryManager.Delete(id);
            }
          
        }
        [Authorize(Roles = "Admin,Employee")]
        [Route("api/categories/{id}")]
        [AcceptVerbs("PUT")]
        public void Update(Category category)
        {
            categoryManager.Edit(category);
        }
    }



}