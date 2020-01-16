using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Blog.Models;
using Umbrella.Blog.Models.Interfaces;

namespace Umbrella.Blog.BLL
{
    public class TagManager
    {
        private static ITagRepository _tagRepository;

        //public TagManager(ITagRepository tagRepository)
        //{
        //    _tagRepository = tagRepository;
        //}

        public TagManager()
        {
            _tagRepository = RepositoryFactory.CreateTagRepository();
        }

        public Tag CreateTag(Tag tag)
        {
            Response<Tag> response = new Response<Tag>();
            try
            {
                _tagRepository.CreateTag(tag);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return tag;
        }

        public void CreatePostTagLink(int tagId, int postId)
        {
            Response<Tag> response = new Response<Tag>();
            try
            {
                Tag tag = new Tag();
                _tagRepository.CreatePostTagLink(tagId, postId);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public Response<Tag> Delete(int id)
        {
            Response<Tag> response = new Response<Tag>();
            try
            {
                _tagRepository.Delete(id);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }

        public Response<Tag> Edit(Tag tag)
        {
            Response<Tag> response = new Response<Tag>();
            try
            {
                _tagRepository.Edit(tag);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }

        public Response<List<Tag>> GetAllTags()
        {
            Response<List<Tag>> response = new Response<List<Tag>>();
            try
            {
                response.Data = _tagRepository.GetAllTags();
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }

        public Response<List<Tag>> GetByPostId(int id)
        {
            Response<List<Tag>> response = new Response<List<Tag>>();
            try
            {
                response.Data = _tagRepository.GetByPostId(id);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }


        public Response<Tag> GetTagById(int id)
        {
            Response<Tag> response = new Response<Tag>();
            try
            {
                response.Data = _tagRepository.GetTagById(id);
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
