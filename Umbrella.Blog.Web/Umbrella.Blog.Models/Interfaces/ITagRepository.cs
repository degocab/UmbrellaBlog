using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella.Blog.Models.Interfaces
{
    public interface ITagRepository
    {
        Tag CreateTag(Tag tag);
        Tag GetTagById(int id);
        void Edit(Tag tag);
        void Delete(int id);
        List<Tag> GetAllTags();
        void CreatePostTagLink(int tagId, int postId);
        List<Tag> GetByPostId(int id);
    }
}
