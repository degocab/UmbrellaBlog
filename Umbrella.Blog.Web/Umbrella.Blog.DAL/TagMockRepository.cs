using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Blog.Models;
using Umbrella.Blog.Models.Interfaces;

namespace Umbrella.Blog.DAL
{
    public class TagMockRepository : ITagRepository
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

        public void CreatePostTagLink(int tagId, int postId)
        {
            throw new NotImplementedException();
        }

        public Tag CreateTag(Tag tag)
        {
            tag.TagId = _tags.Max(t => t.TagId) + 1;
            _tags.Add(tag);
            return tag;
        }

        public void Delete(int id)
        {
            _tags.RemoveAll(m => m.TagId == id);
        }

        public void Edit(Tag tag)
        {
            var found = _tags.FirstOrDefault(m => m.TagId == tag.TagId);

            if (found != null)
                found = tag;
        }

        public List<Tag> GetAllTags()
        {
            return _tags;
        }

        public List<Tag> GetByPostId(int id)
        {
            throw new NotImplementedException();
        }

        public Tag GetTagById(int id)
        {
            return _tags.FirstOrDefault(m => m.TagId == id);
        }
    }
}
