using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Umbrella.Blog.Models;
using Umbrella.Blog.BLL;
using System;
using Umbrella.Blog.Web.Attributes;

namespace Umbrella.Blog.Web.Models {

    public class TinyMCEModel {
        public int Id { get; set; }

        
        public string Title { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        public string TagString { get; set; }
        public bool Approved { get; set; }

        [FutureDate(ErrorMessage = "Expiration date must be in the future")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }
    
        public List<Tag> Tags { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "This field is required.")]
        public string HtmlContent { get; set; }

        
        public List<SelectListItem> CategoryDropDownItems { get; set; }

        public TinyMCEModel()
        {
            CategoryDropDownItems = new List<SelectListItem>();
            Tags = new List<Tag>();
        }

        public void SetCategoryDropDownItems(List<Category> categories)
        {
            foreach (var c in categories)
            {
                CategoryDropDownItems.Add(new SelectListItem()
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                });
            }
        }
    }
}