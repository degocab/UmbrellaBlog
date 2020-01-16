using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella.Blog.Models
{
    public class Settings
    {
        private static string _connectionstring;

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionstring))

                _connectionstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return _connectionstring;

        }
    }
}
