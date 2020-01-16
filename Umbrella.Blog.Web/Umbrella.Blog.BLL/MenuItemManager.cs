using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbrella.Blog.Models;
using Umbrella.Blog.Models.Interfaces;

namespace Umbrella.Blog.BLL
{
    public class MenuItemManager
    {
        private static IMenuItemRepository _menuItemRepository;
        public MenuItemManager()
        {
            _menuItemRepository = RepositoryFactory.CreateMenuItemRepository();
        }
       

       
        public Response<List<MenuItem>> GetAllMenuItems()
        {
            Response<List<MenuItem>> response = new Response<List<MenuItem>>();
            try
            {
                response.Data = _menuItemRepository.GetAllMenuItems();
                response.Success = true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return response;
        }

        public Response CreateMenuItem(MenuItem newMenuItem)
        {
            Response response = new Response();
            try
            {
                _menuItemRepository.CreateMenuItem(newMenuItem);
                response.Success = true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return response;
        }
        public Response DeleteMenuItem(int ID)
        {
            Response response = new Response();
            try
            {
                _menuItemRepository.DeleteMenuItem(ID);
                response.Success = true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return response;
        }
        public Response UpdateMenuItem(MenuItem MenuItem)
        {
            Response response = new Response();
            try
            {
                _menuItemRepository.UpdateMenuItem(MenuItem);
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
