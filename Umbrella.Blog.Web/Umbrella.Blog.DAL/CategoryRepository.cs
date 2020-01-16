using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Blog.Models;
using Umbrella.Blog.Models.Interfaces;

namespace Umbrella.Blog.DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        public void CreateCategory(Category category)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CategoriesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@CategoryId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                cn.Open();

                cmd.ExecuteNonQuery();
                category.CategoryId = (int)param.Value;
            }
        }

        public void Delete(int id)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CategoriesDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryId", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Edit(Category category)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CategoriesUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CategoriesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Category rowToAdd = new Category();
                        rowToAdd.CategoryId = (int)dr["CategoryId"];
                        rowToAdd.CategoryName = dr["CategoryName"].ToString();
                        categories.Add(rowToAdd);
                    }
                }
            }
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CategoriesSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", id);
                cn.Open();
                Category category = new Category();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        category.CategoryId = (int)dr["CategoryId"];
                        category.CategoryName = dr["CategoryName"].ToString();
                    }
                }
                return category;
            }
        }
    }
}
