using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace DapperCRUDAPI.Model
{
   
    public class ProductRepository
    {
        private string connectionString;
        public ProductRepository()
        {
            connectionString = @"Data Source=DESKTOP-H0J38MK\MSSQLSERVER01;Initial Catalog=DAPPERDB;Integrated Security=True";
        }
        

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public void Add(Product prod)
        {   
            using (IDbConnection dbConnection = Connection)
            {   
                string sQuery= @"INSERT INTO Products (Name,Quantity,Price) VALUES(@Name,@Quantity,@Price)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, prod);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using(IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM Products";
                dbConnection.Open();
                return dbConnection.Query<Product>(sql);
            }
        }

        public Product GetByID(int id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string sql = @"SELECT * FROM Products WHERE ProductId = @Id";
                dbConnection.Open();
                return dbConnection.Query<Product>(sql, new {Id = id }).FirstOrDefault(); 

            }
        }

        public void Delete(int id)
        {
            using(IDbConnection dbConnection = Connection)
            {
                string sql = @"DELETE FROM Products WHERE ProductId = @Id";
                dbConnection.Open();
                dbConnection.Execute(sql, new { Id = id });
            }
        }

        public void Update(Product prod)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sql = @"UPDATE Products SET Name=@Name, Quantity=@Quantity, Price=@Price WHERE ProductID=@ProductId";
                dbConnection.Open();
                dbConnection.Execute(sql, prod);
            }
        }
    }
}
