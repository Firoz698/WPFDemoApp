using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDemoApp.Data
{
    public class AppDbContext
    {
        private readonly string _connectionString;

        public AppDbContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }


    //public class AppDbContext
    //{
    //    private readonly string _connectionString = "Server=DESKTOP-7TCVLOM\\SQLEXPRESS;Database=WPFDataBase;Trusted_Connection=True;TrustServerCertificate=True;";

    //    public IDbConnection CreateConnection()
    //    {
    //        return new SqlConnection(_connectionString);
    //    }
    //}
}
