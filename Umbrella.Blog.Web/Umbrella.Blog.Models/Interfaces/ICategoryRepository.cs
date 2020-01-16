using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella.Blog.Models.Interfaces
{
    public interface ICategoryRepository
    {
        void CreateCategory(Category category);
        Category GetCategoryById(int id);
        void Edit(Category category);
        void Delete(int id);
        List<Category> GetAllCategories();
    }
}
