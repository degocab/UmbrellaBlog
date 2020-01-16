using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Blog.DAL;
using Umbrella.Blog.Models;

namespace Umbrella.Blog.Tests
{
    [TestFixture]
    public class PostIntegrationTest
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        PostRepository postRepository = new PostRepository();

        [Test]
        public void CanLoadAllPosts()
        {
            List<Post> posts = postRepository.GetAllPosts();

            Assert.AreEqual(3, posts.Count);

            Assert.AreEqual(1, posts[0].PostId);
            Assert.AreEqual(true, posts[0].Approved);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void CanGetPostById(int id)
        {
            Post post = postRepository.GetPostById(id);

            Assert.AreEqual(id, post.PostId);
        }



        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void CanGetPostByTag(int id)
        {
            Post post = new Post();
            List<Post> posts = postRepository.GetByTag(id, post);

            Assert.AreEqual(1, posts.Count);

            Assert.AreEqual(id, posts[0].PostId);
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void CanGetPostByCategories(int id)
        {
            Post post = new Post();
            List<Post> posts = postRepository.GetByCategory(id, post);

            Assert.AreEqual(1, posts.Count);

            Assert.AreEqual(id, posts[0].PostId);
        }


        [TestCase("We just won the award for having a purpose for the betterment of mankind", true, "2008-08-08 11:11:11.6730000", "2010-08-08 11:11:11.6730000", 1, "00000000-0000-0000-0000-000000000000")]
	    [TestCase("We want to make everyone aware of our latest new", false, "2008-08-08 11:11:11.6730000", "2010-08-08 11:11:11.6730000", 2, "00000000-0000-0000-0000-000000000000")]
	    [TestCase("Please be aware of our latest our break. See hashtags for symptoms", true, "2008-08-08 11:11:11.6730000", "2010-08-08 11:11:11.6730000", 3, "00000000-0000-0000-0000-000000000000")]
        public void CanCreatePost(string text, bool approved, DateTime postDate, DateTime exp, int cat, string userId)
        {
            Post post = new Post()
            {
                PostText = text,
                Approved = approved,
                PostDate = postDate,
                ExpirationDate = exp,
                CategoryId = cat,
                UserId = userId
            };

            post = postRepository.CreatePost(post);

            Assert.AreEqual(text, post.PostText);
            Assert.AreEqual(approved, post.Approved);
            Assert.AreEqual(postDate, post.PostDate);
            Assert.AreEqual(exp, post.ExpirationDate);
            Assert.AreEqual(cat, post.CategoryId);
            Assert.AreEqual(userId, post.UserId);

            Assert.AreEqual(4, post.PostId);
        }




        [TestCase(1, "We just won the award for having a purpose for the betterment of mankind", true, "2008-08-08 11:11:11.6730000", "2010-08-08 11:11:11.6730000", 1, "00000000-0000-0000-0000-000000000000")]
        [TestCase(2, "We want to make everyone aware of our latest new", false, "2008-08-08 11:11:11.6730000", "2010-08-08 11:11:11.6730000", 2, "00000000-0000-0000-0000-000000000000")]
        [TestCase(3, "Please be aware of our latest our break. See hashtags for symptoms", true, "2008-08-08 11:11:11.6730000", "2010-08-08 11:11:11.6730000", 3, "00000000-0000-0000-0000-000000000000")]
        public void CanEditPost(int postId, string text, bool approved, DateTime postDate, DateTime exp, int cat, string userId)
        {
            Post post = new Post()
            {
                PostId = postId,
                PostText = text,
                Approved = approved,
                PostDate = postDate,
                ExpirationDate = exp,
                CategoryId = cat,
                UserId = userId
            };

            postRepository.Edit(post);
            postRepository.GetPostById(post.PostId);

            Assert.AreEqual(text, post.PostText);
            Assert.AreEqual(approved, post.Approved);
            Assert.AreEqual(postDate, post.PostDate);
            Assert.AreEqual(exp, post.ExpirationDate);
            Assert.AreEqual(cat, post.CategoryId);
            Assert.AreEqual(userId, post.UserId);

            Assert.AreEqual(postId, post.PostId);
        }



        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void CanDeletePost(int id)
        {
            Post post = new Post();
            postRepository.Delete(id);

            List<Post> posts = postRepository.GetAllPosts();

            Assert.AreEqual(2, posts.Count);
        }


        [Test]
        public void CanApprovePost()
        {
            Post post = new Post();
            postRepository.approvePost(2);

            post = postRepository.GetPostById(2);

            Assert.AreEqual(true, post.Approved);
        }



        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public void CanRemoveTagsFromPost(int tagId, int postId)
        {
            Post post = new Post();
            postRepository.RemoveTags(tagId, postId);

            List<Tag> tags = postRepository.GetAllPostTags(postId);

            Assert.AreEqual(0, tags.Count);
        }
    }
}
