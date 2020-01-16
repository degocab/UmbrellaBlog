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
    public class TagAPIController : ApiController
    {
        private TagManager tagManager = new TagManager();

        [Authorize(Roles = "Admin,Employee")]
        [Route("api/tags/add")]
        [AcceptVerbs("POST")]
        public void AddTag(Tag tag)
        {
            tagManager.CreateTag(tag);

        }
        [Route("api/tags/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetTag(int id)
        {
            Tag found = tagManager.GetTagById(id).Data;
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }

        [AllowAnonymous]
        [Route("api/tagsByPost/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllPost(int id)
        {
            List<Tag> found = tagManager.GetByPostId(id).Data;
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }


        [Route("api/tags/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllTag()
        {
            List<Tag> found = tagManager.GetAllTags().Data;
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("api/tags/{id}")]
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            tagManager.Delete(id);
        }

        [Authorize(Roles = "Admin,Employee")]
        [Route("api/tags/{id}")]
        [AcceptVerbs("PUT")]
        public void Update(int id, Tag Tag)
        {
            tagManager.Edit(Tag);
        }
    }
}