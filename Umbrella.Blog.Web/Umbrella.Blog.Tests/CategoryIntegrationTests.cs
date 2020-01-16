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
    public class CategoryIntegrationTests
    {
        //Tests
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
        [Test]
        public void CanCreateCategory()
        {
            var repo = new CategoryRepository();

            var cat = new Category();

            cat.CategoryName = "TestCategory";

            repo.CreateCategory(cat);
            var catreturn = repo.GetCategoryById(cat.CategoryId);
            Assert.IsNotNull(catreturn);
        }
        [Test]
        public void CanDeleteCategory()
        {
            var repo = new CategoryRepository();
            var cat = new Category();

            cat.CategoryName = "TestCategory";
            var catreturn = repo.GetCategoryById(cat.CategoryId);
            Assert.IsNotNull(catreturn);

            repo.Delete(catreturn.CategoryId);

            Assert.AreEqual(0, catreturn.CategoryId);
            Assert.IsNull(catreturn.CategoryName);
        }

        [Test]
        public void CanEditCategory()
        {
            var repo = new CategoryRepository();
            var cat = new Category();
            cat.CategoryName = "TestCategory";
            repo.CreateCategory(cat);


            Assert.IsNotNull(repo.GetCategoryById(cat.CategoryId).CategoryName);

            cat.CategoryName = "Category";

            repo.Edit(cat);
            Assert.AreEqual("Category", cat.CategoryName);


        }

        [Test]
        public void CanGetAllCategories()
        {
            var repo = new CategoryRepository();
            var list = new List<Category>();
            list = repo.GetAllCategories();
            //Assuming the sample data is still at only 4 sample categories
            Assert.AreEqual(4, list.Count);

        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void CanGetCategoryById(int id)
        {
            var repo = new CategoryRepository();
            var returned = repo.GetCategoryById(id);
            Assert.AreEqual(id, returned.CategoryId);

        }

    }
}
