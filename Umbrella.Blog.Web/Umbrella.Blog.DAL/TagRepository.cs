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
    public class TagRepository : ITagRepository
    {
        public Tag CreateTag(Tag tag)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TagsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                //SqlParameter param = new SqlParameter("@TagId", SqlDbType.Int);

                cmd.Parameters.Add("@TagId", SqlDbType.Int).Direction = ParameterDirection.Output;
                

                cmd.Parameters.AddWithValue("@TagText", tag.TagText); 
                
                cn.Open();

                cmd.ExecuteNonQuery();

                tag.TagId = (int)cmd.Parameters["@TagId"].Value;

                return tag;
            }
        }

        public void CreatePostTagLink(int tagId, int postId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PostsTagsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                //SqlParameter param = new SqlParameter("@TagId", SqlDbType.Int);

                cmd.Parameters.AddWithValue("@TagId", tagId);
                cmd.Parameters.AddWithValue("@PostId", postId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TagsDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TagId", id);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Edit(Tag tag)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TagsUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TagId", tag.TagId);
                cmd.Parameters.AddWithValue("@TagText", tag.TagText);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Tag> GetAllTags()
        {
            List<Tag> tags = new List<Tag>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TagsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Tag rowToAdd = new Tag();
                        rowToAdd.TagId = (int)dr["TagId"];
                        rowToAdd.TagText = dr["TagText"].ToString();                      
                        tags.Add(rowToAdd);
                    }
                }
            }
            return tags;
        }




        public List<Tag> GetAllByPostId(int id)
        {
            List<Tag> tags = new List<Tag>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TagsByPostId", cn);
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
                        tags.Add(rowToAdd);
                    }
                }
            }
            return tags;
        }

        public List<Tag> GetByPostId(int id)
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


        public Tag GetTagById(int id)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("TagsSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TagId", id);
                cn.Open();
                Tag tag = new Tag();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        tag.TagId = (int)dr["TagId"];
                        tag.TagText = dr["TagText"].ToString();
                    }
                }
                return tag;
            }
        }
    }
}
