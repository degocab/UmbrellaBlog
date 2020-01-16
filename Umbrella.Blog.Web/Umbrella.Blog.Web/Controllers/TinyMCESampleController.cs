using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbrella.Blog.BLL;
using Umbrella.Blog.Models;
using Umbrella.Blog.Web.Models;

namespace Umbrella.Blog.Web.Controllers {

    public class TinyMCESampleController : Controller {

        //
        // GET: /TinyMCESample/
        PostManager postManager = new PostManager();
        CategoryManager categoryManager = new CategoryManager();

        [HttpGet]
        public ActionResult TinyMCECreatePost() {

            TinyMCEModel tinyMCEModel = new TinyMCEModel();
            tinyMCEModel.SetCategoryDropDownItems(categoryManager.GetAllCategories().Data);

            return View(tinyMCEModel);

        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public ActionResult TinyMCECreatePost(TinyMCEModel model)
        {

            if (!model.HtmlContent.Contains("<p>") && !model.HtmlContent.Contains("</p>") && !model.HtmlContent.Contains("<p") && !model.HtmlContent.Contains("<img"))
            {
                ModelState.AddModelError("HtmlContent",
                    "Please enter text for your post!");
            }
            Post post = new Post();
            if (ModelState.IsValid)
            {
            TagManager tagManager = new TagManager();
           
            post.PostTitle = model.Title;
            post.PostText = model.HtmlContent;
            post.PostDate = DateTime.Now;
            post.UserId = "00000000-0000-0000-0000-000000000000";
            post.ExpirationDate = model.ExpirationDate;
            post.CategoryId = model.CategoryId;
            post = postManager.CreatePost(post);

            if (!string.IsNullOrWhiteSpace(model.TagString))
            {
                string[] tagInputFromView = model.TagString.Trim().Split(',');
                Tag tag = new Tag();
                foreach (var t in tagInputFromView)
                {
                    tag.TagText = t.Trim();
                    tag = tagManager.CreateTag(tag);
                    tagManager.CreatePostTagLink(tag.TagId, post.PostId);
                }
            }
            return RedirectToAction("Edit", new { postId = post.PostId });
            }
            else
            {
                model.SetCategoryDropDownItems(categoryManager.GetAllCategories().Data);
                return View(model);  
            }
            
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]        
        public ActionResult Edit(int postId)
        {
            var post = postManager.GetPostById(postId).Data;
            
            TinyMCEModel editVM = new TinyMCEModel()
            {
                Id = post.PostId,
                Title = post.PostTitle,
                CategoryId = post.CategoryId,
                HtmlContent = post.PostText,
                Approved = post.Approved,
                ExpirationDate = post.ExpirationDate ?? DateTime.MaxValue
            };


            if (post.Tags.Count != 0)
            {
                foreach (var t in post.Tags)
                {
                    editVM.Tags.Add(t);
                }
            }

            
            editVM.SetCategoryDropDownItems(categoryManager.GetAllCategories().Data);

            return View(editVM);
        }

        [Authorize(Roles = "Admin,Employee")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(TinyMCEModel model)
        {

            if (!model.HtmlContent.Contains("<p>") || !model.HtmlContent.Contains("</p>"))
            {
                ModelState.AddModelError("HtmlContent",
                    "Please enter text for your post!");
            }
            TagManager tagManager = new TagManager();
            PostManager postManager = new PostManager();
            Post post = new Post();

            if (ModelState.IsValid)
            {

                post.PostId = model.Id;
                post.PostTitle = model.Title;
                post.PostText = model.HtmlContent;
                post.Approved = model.Approved;
                post.PostDate = DateTime.Now;
                post.UserId = "00000000-0000-0000-0000-000000000000";
                post.CategoryId = model.CategoryId;
                post.ExpirationDate = model.ExpirationDate;
                post = postManager.Edit(post).Data;

                if (!string.IsNullOrWhiteSpace(model.TagString))
                {
                    string[] tagInputFromView = model.TagString.Trim().Split(',');
                    Tag tag = new Tag();
                    foreach (var t in tagInputFromView)
                    {
                        tag.TagText = t.Trim();
                        tag = tagManager.CreateTag(tag);
                        tagManager.CreatePostTagLink(tag.TagId, model.Id);
                    }
                }
                return RedirectToAction("BlogPost", "Home", new { model.Id } );
            }
            post = postManager.GetPostById(model.Id).Data;
            model.Tags = post.Tags;
            model.SetCategoryDropDownItems(categoryManager.GetAllCategories().Data);
            return View(model);
            //else
            //{
            //    return View("Edit", new { postId = model.Id });
            //}
        }


        [Authorize(Roles = "Admin,Employee")]
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult DeleteTag(int postId, int tagId)
        {
            postManager.RemoveTags(tagId, postId);

            return RedirectToAction("Edit", new { postId = postId });
        }
    }
    
}