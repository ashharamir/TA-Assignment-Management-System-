using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_PROJECT_DEMO
{
    public static class DBConnection
    {
        private static string connectionString = "Data Source=LAPTOP-N25QV6PN\\SQLEXPRESS;Initial Catalog=db_proj_final;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open(); // Open the connection
            Console.WriteLine($"Connection started: {connection.State}");
            return connection;
        }
    }
}
