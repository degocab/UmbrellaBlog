using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Blog.Models;
using Umbrella.Blog.Models.Interfaces;

namespace Umbrella.Blog.BLL
{
    public class PostManager
    {
        private static IPostRepository _postRepository;

        //public PostManager(IPostRepository postRepository)
        //{
        //    _postRepository = postRepository;
        //}

        public PostManager()
        {
            _postRepository = RepositoryFactory.CreatePostRepository();
        }

        public Post CreatePost(Post post)
        {
            Response<Post> response = new Response<Post>();
            try
            {
                _postRepository.CreatePost(post);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return post;
        }
        public Response<Post> GetPostById(int id)
        {
            Response<Post> response = new Response<Post>();
            try
            {
                response.Data = _postRepository.GetPostById(id);               
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }
        public Response<Post> Edit(Post post)
        {
            Response<Post> response = new Response<Post>();
            try
            {
                _postRepository.Edit(post);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }
        public Response<Post> Delete(int id)
        {
            Response<Post> response = new Response<Post>();
            try
            {
                _postRepository.Delete(id);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }
        public Response<Post> approvePost(int id)
        {
            Response<Post> response = new Response<Post>();
            try
            {
                _postRepository.approvePost(id);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }
        public Response<List<Post>> GetByTag(int TagId, Post post)
        {
            Response<List<Post>> response = new Response<List<Post>>();
            try
            {
                response.Data = _postRepository.GetByTag(TagId, post);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }
        public Response<List<Post>> GetByCategory(int CategoryId,Post post)
        {
            Response<List<Post>> response = new Response<List<Post>>();
            try
            {
                response.Data = _postRepository.GetByCategory(CategoryId, post);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }
        public Response<List<Post>> GetAllPosts()
        {
            Response<List<Post>> response = new Response<List<Post>>();
            try
            {
                response.Data = _postRepository.GetAllPosts();
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }
        public Response<List<Post>> GetPostsbyTag(int auth,string filtertext)
        {
            Response<List<Post>> response = new Response<List<Post>>();
            try
            {
                response.Data = _postRepository.GetPostsbyTag(auth,filtertext);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }
        public Response<List<Post>> GetTop3Posts()
        {
            Response<List<Post>> response = new Response<List<Post>>();
            try
            {
                response.Data = _postRepository.GetTop3Posts();
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }


        public Response<Post> RemoveTags(int tagId, int postId)
        {
            Response<Post> response = new Response<Post>();
            try
            {
                _postRepository.RemoveTags(tagId, postId);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }

    }
}
