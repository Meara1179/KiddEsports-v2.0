using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace DataManagement
{
    public static class Helper
    {
        // Gets the string used to connection to the database.
        private static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        
        // Creates a connection with the database.
        public static SqlConnection CreateConnection(string name)
        {
            return new SqlConnection(GetConnectionString(name));
        }
    }
}
