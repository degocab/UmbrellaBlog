using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbrella.Blog.BLL;
using Umbrella.Blog.Models;
using Umbrella.Blog.Web.Models;

namespace Umbrella.Blog.Web.Controllers
{
    public class PostAPIController : ApiController
    {
        private PostManager postManager = new PostManager();


        [Authorize(Roles = "Admin,Employee")]
        [Route("api/posts/add")]
        [AcceptVerbs("POST")]
        public void AddPost(Post post)
        {           
            postManager.CreatePost(post);
        }
        [Authorize(Roles = "Admin")]
        [Route("api/posts/approve/{id}")]
        [AcceptVerbs("POST")]
        public void approvePost(int id)
        {
            postManager.approvePost(id);
        }
        [AllowAnonymous]
        [Route("api/posts/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetPost(int id)
        {
            Post found = postManager.GetPostById(id).Data;
            if (found==null)
            {
                return NotFound();
            }
            return Ok(found);
        }

        [AllowAnonymous]
        [Route("api/posts")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllPost(UserVM user)
        {
            List<Post> found = postManager.GetAllPosts().Data;
            if (found == null)
            {
                return NotFound();
            }

            if(!User.IsInRole("Admin") && !User.IsInRole("Employee"))
            {
                found.RemoveAll(m => m.Approved == false);
                found.RemoveAll(m => m.ExpirationDate < DateTime.Today.AddDays(1));
                return Ok(found);
            }

            return Ok(found);
        }

        [AllowAnonymous]
        [Route("api/posts/filter/{filter}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllPostByFilter(UserVM user,string filter)
        {
            int auth = 0;

            if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
            {
                auth = 0;
               
            }
            else if (User.IsInRole("Admin") && User.IsInRole("Employee"))
            {
                auth = 1;
            }
            List<Post> found = postManager.GetPostsbyTag(auth,filter).Data;
            if (found == null)
            {
                return NotFound();
            }

          

            return Ok(found);
        }

        [AllowAnonymous]
        [Route("api/posts/top")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetTop3Posts()
        {
            List<Post> found = postManager.GetTop3Posts().Data;
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }
        [Authorize(Roles = "Admin,Employee")]
        [Route("api/posts/{id}")]
        [AcceptVerbs("DELETE")]
        public void Delete(int id)
        {
            postManager.Delete(id);
        }

        [Authorize(Roles = "Admin")]
        [Route("api/posts/{id}")]
        [AcceptVerbs("PUT")]
        public void Update(int id, Post post)
        {
            postManager.Edit(post);
        }

    }
}
