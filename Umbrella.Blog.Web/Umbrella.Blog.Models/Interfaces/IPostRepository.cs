using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella.Blog.Models.Interfaces
{
    public interface IPostRepository
    {
        Post CreatePost(Post post);
        Post GetPostById(int id);
        void Edit(Post post);
        void Delete(int id);
        List<Post> GetByTag(int TagId, Post post);
        List<Post> GetByCategory(int CategoryId, Post post);
        List<Post> GetAllPosts();
        List<Post> GetPostsbyTag(int auth, string filter);

        List<Post> GetTop3Posts();
        void RemoveTags(int tagId, int postId);
        void approvePost(int id);

    }
}
