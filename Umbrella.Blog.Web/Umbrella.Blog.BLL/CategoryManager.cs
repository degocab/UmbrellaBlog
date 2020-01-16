using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Blog.Models;
using Umbrella.Blog.Models.Interfaces;

namespace Umbrella.Blog.BLL
{
    public class CategoryManager
    {
        private static ICategoryRepository _categoryRepository;

        //public CategoryManager(ICategoryRepository categoryRepository)
        //{
        //    _categoryRepository = categoryRepository;
        //}

        public CategoryManager()
        {
            _categoryRepository = RepositoryFactory.CreateCategoryRepository();
        }
        public Response<Category> CreateCategory(Category category)
        {
            Response<Category> response = new Response<Category>();
            try
            {
                _categoryRepository.CreateCategory(category);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }

        public Response<Category> Delete(int id)
        {
            Response<Category> response = new Response<Category>();
            try
            {
                _categoryRepository.Delete(id);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return response;
        }

        public Response<Category> Edit(Category category)
        {
            Response<Category> response = new Response<Category>();
            try
            {
                _categoryRepository.Edit(category);
                response.Success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response;
        }

        public Response<List<Category>> GetAllCategories()
        {
            Response<List<Category>> response = new Response<List<Category>>();
            try
            {
                response.Data = _categoryRepository.GetAllCategories().ToList();
                response.Success = true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return response;
        }

        public Response<Category> GetCategoryById(int id)
        {
            Response<Category> response = new Response<Category>();
            try
            {
                response.Data = _categoryRepository.GetCategoryById(id);
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
