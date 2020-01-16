using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Blog.DAL;
using Umbrella.Blog.Models.Interfaces;

namespace Umbrella.Blog.BLL
{
    public class RepositoryFactory
    {
        public static IPostRepository CreatePostRepository()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TEST":
                     IPostRepository mockPostRepository= new PostMockRepository();
                    return mockPostRepository;
                case "LIVE":
                    IPostRepository livePostRepository = new PostRepository();
                    return livePostRepository;
                default:
                    throw new Exception();
            }
        }
        public static IMenuItemRepository CreateMenuItemRepository()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TEST":
                    //IMenuItemRepository mockPostRepository = new PostMockRepository();
                    //return mockPostRepository;
                case "LIVE":
                    IMenuItemRepository liveCatgoryRepository = new MenuItemRepository();
                    return liveCatgoryRepository;
                default:
                    throw new Exception();
            }
        }
        public static ITagRepository CreateTagRepository()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TEST":
                    ITagRepository mockTagRepository = new TagMockRepository();
                    return mockTagRepository;
                case "LIVE":
                    ITagRepository liveTagRepository = new TagRepository();
                    return liveTagRepository;
                default:
                    throw new Exception();
            }
        }

        public static ICategoryRepository CreateCategoryRepository()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TEST":
                    ICategoryRepository mockCategoryRepository = new CategoryMockRepository();
                    return mockCategoryRepository;
                case "LIVE":
                    ICategoryRepository liveCategoryRepository = new CategoryRepository();
                    return liveCategoryRepository;
                default:
                    throw new Exception();
            }
        }

    }
}
