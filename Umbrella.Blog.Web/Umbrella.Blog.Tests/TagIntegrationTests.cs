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
    public class TagIntegrationTests
    {
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

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void CanLoadTagById(int id)
        {
            var repo = new TagRepository();

            var tag = repo.GetTagById(id);



            Assert.AreEqual(id, tag.TagId);
        }


        [Test]
        public void CanLoadTags()
        {
            //assuming that no more sample tags are added. There shuold only be 3 in the sample data
            var repo = new TagRepository();
            var tags = repo.GetAllTags();
            Assert.AreEqual(3, tags.Count);
        }

        [Test]
        public void CanAddTag()
        {
            Tag tagtoAdd = new Tag();

            var repo = new TagRepository();

            tagtoAdd.TagText = "#NewTag";

            repo.CreateTag(tagtoAdd);
            //assuming that no more sample tags are added. There shuold only be 3 in the sample data
            Assert.AreEqual(4,tagtoAdd.TagId);

        }


        [Test]
        public void CanDeleteTag()
        {
            Tag tagtoAdd = new Tag();
            tagtoAdd.TagText = "test";

            var repo = new TagRepository();

            repo.CreateTag(tagtoAdd);
            //assuming that no more sample tags are added. There shuold only be 3 in the sample data
            var loaded = repo.GetTagById(4);
            Assert.IsNotNull(loaded);

            //assuming that no more sample tags are added. There shuold only be 3 in the sample data
            repo.Delete(4);
            loaded = repo.GetTagById(4);

            Assert.IsNull(loaded.TagText);
            Assert.AreEqual(0,loaded.TagId);
        }

        [Test]
        public void CanUpdateTag()
        {
            Tag tag = new Tag();
            var repo = new TagRepository();

            tag.TagText = "Test1";

            repo.CreateTag(tag);

            tag.TagText = "TestOne";

            repo.Edit(tag);
            //assuming that no more sample tags are added. There shuold only be 3 in the sample data
            var updatedTag = repo.GetTagById(4);

            Assert.AreEqual("TestOne",updatedTag.TagText);

        }



    }
}
