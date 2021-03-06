﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Blog.Models;
using Umbrella.Blog.Models.Interfaces;

namespace Umbrella.Blog.DAL
{
    public class PostMockRepository : IPostRepository
    {
        private static List<Category> _category = new List<Category>()
        {
            new Category
            {CategoryId = 1,
            CategoryName = "Zombies"}

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

        public void approvePost(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateComment(Comment comment)
        {
            comment.CommentId = _comments.Max(m => m.CommentId) + 1;
            _comments.Add(comment);
        }

        public Post CreatePost(Post post)
        {
            post.PostId = _posts.Max(m => m.PostId) + 1;
            _posts.Add(post);
            return post;
        }

        public void Delete(int id)
        {
            _posts.RemoveAll(m => m.PostId == id);
        }

        public void Edit(Post post)
        {
            var found = _posts.FirstOrDefault(m => m.PostId == post.PostId);
            if (found != null)
            {
                found = post;
            }
        }

        public List<Post> GetAllPosts()
        {
            return _posts;
        }

        public List<Post> GetByCategory(int CategoryId, Post post)
        {
            List<Post> posts = new List<Post>();

            while (true)
            {
                var found = _posts.FirstOrDefault(m => m.CategoryId == post.PostId);
                while (found != null)
                {
                    posts.Add(found);
                }
                break;
            }
            return posts;
        }

        public List<Post> GetByTag(int TagId, Post post)
        {
            List<Post> posts = new List<Post>();

            //post.tags should be private list needs fixed
            foreach (Tag t in post.Tags)
            {
                if (t.TagId == TagId)
                {
                    posts.Add(post);
                }
            }
            return posts;

        }

        public Post GetPostById(int id)
        {
            return _posts.FirstOrDefault(m => m.PostId == id);
        }

        public List<Post> GetTop3Posts()
        {
            List<Post> Top3 = new List<Post>();

            Top3.Add(new Post { PostId = 1, Approved = true, CategoryId = 1, PostDate = DateTime.Parse("2/2/2020"), ExpirationDate = null, Comments = _comments, Tags = _tags, UserId = "10000000000000000" });
            Top3.Add(new Post { PostId = 1, Approved = true, CategoryId = 1, PostDate = DateTime.Parse("2/2/2020"), ExpirationDate = null, Comments = _comments, Tags = _tags, UserId = "10000000000000000" });
            Top3.Add(new Post { PostId = 1, Approved = true, CategoryId = 1, PostDate = DateTime.Parse("2/2/2020"), ExpirationDate = null, Comments = _comments, Tags = _tags, UserId = "10000000000000000" });

            return Top3;
        }

        public void RemoveTags(int tagId, int postId)
        {
            throw new NotImplementedException();
        }


        public List<Post> GetPostsbyTag(int auth, string filter)
        {
            throw new NotImplementedException();
        }
    }
}
