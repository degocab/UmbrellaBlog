using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella.Blog.Models.Interfaces
{
   public interface IMenuItemRepository
    {
        List<MenuItem> GetAllMenuItems();
        void CreateMenuItem(MenuItem NewMenuItem);
        void DeleteMenuItem(int ID);
        void UpdateMenuItem(MenuItem MenuItem);



    }
}
