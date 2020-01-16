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
    public class MenuItemRepository : IMenuItemRepository
    {
        public void CreateMenuItem(MenuItem NewMenuItem)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MenuItemInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MenuText", NewMenuItem.MenuText);
                cmd.Parameters.AddWithValue("@MenuLink", NewMenuItem.MenuLink);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMenuItem(int ID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MenuItemDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MenuItemID", ID);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<MenuItem> GetAllMenuItems()
        {
            List<MenuItem> MenuItemList = new List<MenuItem>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MenuItemGetALL", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        MenuItem rowToAdd = new MenuItem();
                        rowToAdd.MenuItemID = (int)dr["MenuItemID"];
                        rowToAdd.MenuText = (string)dr["MenuText"];
                        rowToAdd.MenuLink = (string)dr["MenuLink"];

                        MenuItemList.Add(rowToAdd);
                    }
                }
            }
            return MenuItemList;
        }

        public void UpdateMenuItem(MenuItem MenuItem)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MenuItemUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MenuItemID", MenuItem.MenuItemID);
                cmd.Parameters.AddWithValue("@MenuItemText", MenuItem.MenuText);
                cmd.Parameters.AddWithValue("@MenuItemLink", MenuItem.MenuLink);
              

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
