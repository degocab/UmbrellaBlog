﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Blog.Models;
using Umbrella.Blog.Models.Interfaces;

namespace Umbrella.Blog.DAL
{
    public class CategoryMockRepository : ICategoryRepository
    {
        private static List<Category> _category = new List<Category>()
        {
            new Category
            {CategoryId = 1,
            CategoryName = "Zombies" }
            
        };
        private static List<Comment> _comments = new List<Comment>()
        {
            new Comment
            { CommentId = 1, PostId = 1,CommentDate = DateTime.Parse("2/2/2020"),CommentText="Nice"}
        };
        private static List<Post> _posts = new List<Post>()
        {
            new Post
            {PostId = 1,Approved= true,CategoryId = 1, PostDate = DateTime.Parse("2/2/2020"),ExpirationDate=null,Comments= _comments,Tags= _tags,UserId = "10000000000000000" },
            new Post
            {PostId = 2,Approved= true,CategoryId = 1, PostDate = DateTime.Parse("2/2/2020"),ExpirationDate=null,Comments= _comments,Tags= _tags,UserId = "10000000000000000" }

        };

        private static List<Tag> _tags = new List<Tag>()
        {
            new Tag
            { TagId = 1, TagText="#Zombies",Posts = _posts}
        };




        public void CreateCategory(Category category)
        {
            category.CategoryId = _category.Max(c => c.CategoryId) + 1;
            _category.Add(category);
        }

        public void Delete(int id)
        {
            _category.RemoveAll(c => c.CategoryId==id);
        }

        public void Edit(Category category)
        {
            var found = _category.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (found != null)
            {
                found = category;
            }
        }

        public List<Category> GetAllCategories()
        {
            return _category;
        }

        public Category GetCategoryById(int id)
        {
            return _category.FirstOrDefault(c=>c.CategoryId==id);
        }
    }
}
