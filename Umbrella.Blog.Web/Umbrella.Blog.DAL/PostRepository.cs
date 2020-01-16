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
    public class PostRepository : IPostRepository
    {
        //public void CreateComment(Comment comment)
        //{
        //    using (var cn = new SqlConnection(Settings.GetConnectionString()))
        //    {
        //        SqlCommand cmd = new SqlCommand("CommentsInsert", cn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlParameter param = new SqlParameter("@CommentId", SqlDbType.Int);
        //        param.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(param);

        //        cmd.Parameters.AddWithValue("@PostId", comment.PostId);
        //        cmd.Parameters.AddWithValue("@CommentDate", comment.CommentDate);
        //        cmd.Parameters.AddWithValue("@CommentText", comment.CommentText);
                
        //        cn.Open();

        //        cmd.ExecuteNonQuery();
        //        comment.CommentId = (int)param.Value;
        //    }
        //}

        public Post CreatePost(Post post)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PostsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@postId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@PostTitle", post.PostTitle);
                cmd.Parameters.AddWithValue("@Approved", post.Approved);
                cmd.Parameters.AddWithValue("@PostText", post.PostText);
                cmd.Parameters.AddWithValue("@CategoryId", post.CategoryId);
                cmd.Parameters.AddWithValue("@PostDate", post.PostDate);
                cmd.Parameters.AddWithValue("@ExpirationDate", post.ExpirationDate);
                cmd.Parameters.AddWithValue("@UserId", post.UserId);

                cn.Open();

                cmd.ExecuteNonQuery();

                post.PostId = (int)param.Value;
                return post;
            }
        }

        public void Delete(int id)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PostsDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostId", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void approvePost(int id)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PostsApprove", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostId", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Edit(Post post)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PostsUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PostId", post.PostId);
                cmd.Parameters.AddWithValue("@PostTitle", post.PostTitle);
                cmd.Parameters.AddWithValue("@Approved", post.Approved);
                cmd.Parameters.AddWithValue("@PostText", post.PostText);
                cmd.Parameters.AddWithValue("@UserId", post.UserId);
                cmd.Parameters.AddWithValue("@CategoryId", post.CategoryId);
                cmd.Parameters.AddWithValue("@PostDate", post.PostDate);
                cmd.Parameters.AddWithValue("@ExpirationDate", post.ExpirationDate);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Tag> GetAllPostTags(int id)
        {
            List<Tag> PostsTagsList = new List<Tag>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetPostsTags", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.Parameters.AddWithValue("@PostId", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Tag rowToAdd = new Tag();
                        rowToAdd.TagId = (int)dr["TagId"];
                        rowToAdd.TagText = dr["TagText"].ToString();
                        PostsTagsList.Add(rowToAdd);
                    }
                }
            }
            return PostsTagsList;
        }

        public List<Post> GetAllPosts()
        {
            List<Post> posts = new List<Post>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PostsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Post rowToAdd = new Post();
                        rowToAdd.PostId = (int)dr["PostId"];
                        rowToAdd.Approved = (bool)dr["Approved"];
                        rowToAdd.PostText = dr["PostText"].ToString();
                        rowToAdd.PostDate = (DateTime)dr["PostDate"];
                        rowToAdd.UserId = dr["UserId"].ToString();
                        rowToAdd.Tags = GetAllPostTags(rowToAdd.PostId);


                        if (dr["PostTitle"] != DBNull.Value)
                            rowToAdd.PostTitle = dr["PostTitle"].ToString();
                        
                        if (dr["CategoryId"] != DBNull.Value)
                            rowToAdd.CategoryId = (int)dr["CategoryId"];                       

                        if (dr["ExpirationDate"] != DBNull.Value)
                            rowToAdd.ExpirationDate = (DateTime)dr["ExpirationDate"];
                        Category category = new Category();
                        CategoryRepository categoryRepository = new CategoryRepository();
                        category = categoryRepository.GetCategoryById(rowToAdd.CategoryId);
                        rowToAdd.CategoryName = category.CategoryName;


                        posts.Add(rowToAdd);
                    }
                }
            }
            return posts;
        }

        public List<Post> GetByCategory(int CategoryId, Post Post)
        {
            List<Post> posts = new List<Post>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PostsSelectByCategory", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Post rowToAdd = new Post();
                        rowToAdd.PostId = (int)dr["PostId"];
                        rowToAdd.Approved = (bool)dr["Approved"];
                        rowToAdd.PostText = dr["PostText"].ToString();
                        rowToAdd.PostDate = (DateTime)dr["PostDate"];                      
                        rowToAdd.UserId = dr["UserId"].ToString();

                        rowToAdd.Tags = GetAllPostTags(rowToAdd.PostId);

                        if (dr["PostTitle"] != DBNull.Value)
                            rowToAdd.PostTitle = dr["PostTitle"].ToString();


                        if (dr["CategoryId"] != DBNull.Value)
                            rowToAdd.CategoryId = (int)dr["CategoryId"];

                        if (dr["ExpirationDate"] != DBNull.Value)
                            rowToAdd.ExpirationDate = (DateTime)dr["ExpirationDate"];

                        posts.Add(rowToAdd);
                    }
                }
            }
            return posts;
        }

        public List<Post> GetByTag(int TagId, Post post)
        {
            List<Post> posts = new List<Post>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PostsSelectByTag", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TagId", TagId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Post rowToAdd = new Post();
                        rowToAdd.PostId = (int)dr["PostId"];
                        rowToAdd.Approved = (bool)dr["Approved"];
                        rowToAdd.PostText = dr["PostText"].ToString();
                        rowToAdd.PostDate = (DateTime)dr["PostDate"];
                        rowToAdd.UserId = dr["UserId"].ToString();

                        rowToAdd.Tags = GetAllPostTags(rowToAdd.PostId);


                        if (dr["PostTitle"] != DBNull.Value)
                            rowToAdd.PostTitle = dr["PostTitle"].ToString();


                        if (dr["CategoryId"] != DBNull.Value)
                            rowToAdd.CategoryId = (int)dr["CategoryId"];

                        if (dr["ExpirationDate"] != DBNull.Value)
                            rowToAdd.ExpirationDate = (DateTime)dr["ExpirationDate"];


                        posts.Add(rowToAdd);
                    }
                }
            }
            return posts;
        }



        public Post GetPostById(int id)
        {
            Post post = new Post();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PostsSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PostId", id);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        post.PostId = (int)dr["PostId"];
                        post.Approved = (bool)dr["Approved"];
                        post.PostText = dr["PostText"].ToString();
                        post.PostDate = (DateTime)dr["PostDate"];                       
                        post.UserId = dr["UserId"].ToString();

                        post.Tags = GetAllPostTags(post.PostId);

                        if (dr["PostTitle"] != DBNull.Value)
                            post.PostTitle = dr["PostTitle"].ToString();


                        if (dr["CategoryId"] != DBNull.Value)
                            post.CategoryId = (int)dr["CategoryId"];

                        if (dr["ExpirationDate"] != DBNull.Value)
                            post.ExpirationDate = (DateTime)dr["ExpirationDate"];

                        post.Tags = GetAllPostTags(post.PostId);


                        Category category = new Category();
                        CategoryRepository categoryRepository = new CategoryRepository();
                        category = categoryRepository.GetCategoryById(post.CategoryId);
                        post.CategoryName = category.CategoryName;
                    }
                }
            }
            return post;
        }

        public List<Post> GetTop3Posts()
        {
            List<Post> posts = new List<Post>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetTopPosts", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Post rowToAdd = new Post();

                        if (dr["PostTitle"] != DBNull.Value)
                        rowToAdd.PostTitle = dr["PostTitle"].ToString();

                        rowToAdd.Tags = GetAllPostTags(rowToAdd.PostId);

                        rowToAdd.PostText = dr["PostText"].ToString();
                        rowToAdd.PostDate = (DateTime)dr["PostDate"];


                        posts.Add(rowToAdd);
                    }
                }
            }
            return posts;
        }

        public void RemoveTags(int tagId, int postId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PostsTagsDelete", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PostId", postId);
                cmd.Parameters.AddWithValue("@TagId", tagId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Post> GetPostsbyTag(int auth, string filter)
        {
            string approved =
                "SELECT DISTINCT Posts.PostId FROM Posts " +
                " INNER JOIN PostsTags ON Posts.PostId = PostsTags.PostId" +
                " INNER JOIN Tags ON PostsTags.TagId = Tags.TagId" +
                " INNER JOIN Categories ON Posts.CategoryId = Categories.CategoryId" +
                " WHERE(Tags.TagText LIKE '%'+@text+'%') AND(Posts.Approved = 1) " +
                "or" +
                " CategoryName LIKE '%'+@text+'%' and(Posts.Approved = 1);";

            string unapproved =
                 "SELECT DISTINCT Posts.PostId FROM Posts " +
                " INNER JOIN PostsTags ON Posts.PostId = PostsTags.PostId" +
                " INNER JOIN Tags ON PostsTags.TagId = Tags.TagId" +
                " INNER JOIN Categories ON Posts.CategoryId = Categories.CategoryId" +
                " WHERE(Tags.TagText LIKE '%'+@text+'%') and(Posts.Approved = 0) " +
                "or" +
                " (CategoryName LIKE '%'+@text+'%') and(Posts.Approved = 0)";


            string SQL = "";


            if(auth == 0)
            {
                SQL = approved;
            }
            else
            {
                SQL = unapproved;
            }
            List<Post> posts = new List<Post>();



            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                List<int> postId = new List<int>();
                SqlCommand cmd = new SqlCommand(SQL, cn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@text", filter);
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        postId.Add((int)dr["PostId"]);
                    }
                }

                foreach(var postid in postId)
                {
                   posts.Add(GetPostById(postid));
                }
            }
            return posts;
        }
    }
}
