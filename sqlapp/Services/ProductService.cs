using Microsoft.Extensions.Diagnostics.HealthChecks;
using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {

        public static string db_source = "sqldbvishal.database.windows.net";
        public static string db_user = "sqladmin";
        public static string db_pwd = "Vishalsahni123#";

        public static string db_database = "sql db";

        private SqlConnection GetConnection()
        {

            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_pwd;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            SqlConnection _conn = GetConnection();
            string statement = "Select * from Products";
            _conn.Open();
            SqlCommand sqlCommand = new SqlCommand(statement, _conn);
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                   Product product = new Product();
                    product.Id = reader.GetInt32(0);
                    product.name = reader.GetString(1);
                    product.Quantity = reader.GetInt32(2);
                    products.Add(product);

                }


            }
            _conn.Close();
            return products;
        }
    }
}
