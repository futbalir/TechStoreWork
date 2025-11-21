using Dapper;
using System.Data.SqlClient;
using TechStore.Core.Interfaces;
using TechStore.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TechStore.DataAccessLayer.Dapper
{
    /// <summary>
    /// Репозиторий Dapper для работы с товарами
    /// </summary>
    public class DapperRepository : IProductRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Инициализирует репозиторий Dapper
        /// </summary>
        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Добавляет новый товар
        /// </summary>
        public void Add(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Products (Name, Price, Category, StockQuantity, Manufacturer) 
                       VALUES (@Name, @Price, @Category, @StockQuantity, @Manufacturer);
                       SELECT CAST(SCOPE_IDENTITY() as int)";

            var newId = connection.QuerySingle<int>(sql, product);
            product.Id = newId;
        }

        /// <summary>
        /// Удаляет товар по идентификатору
        /// </summary>
        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Products WHERE Id = @Id";
            connection.Execute(sql, new { Id = id });
        }

        /// <summary>
        /// Получает все товары
        /// </summary>
        public IList GetAllProducts()
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Products";
            var products = connection.Query<Product>(sql).ToList();

            
            return products;
        }

        /// <summary>
        /// Находит товар по идентификатору
        /// </summary>
        public Product GetProductById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            return connection.QuerySingleOrDefault<Product>(sql, new { Id = id });
        }

        /// <summary>
        /// Обновляет данные товара
        /// </summary>
        public void Update(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"UPDATE Products 
                       SET Name = @Name, Price = @Price, Category = @Category, 
                           StockQuantity = @StockQuantity, Manufacturer = @Manufacturer 
                       WHERE Id = @Id";

            connection.Execute(sql, product);
        }
    }
}