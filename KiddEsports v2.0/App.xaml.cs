using DataManagement;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KiddEsports_v2._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Creates a new DB_Builder object.
            DB_Builder builder = new DB_Builder();

            // Creates the database if it doesn't already exit.
            builder.CreateDatabase();

            // Creates the database tables if they don't already exist
            if (builder.DoTablesExist() == false)
            {
                builder.BuildDatabaseTables();
            }
        }
    }
}
