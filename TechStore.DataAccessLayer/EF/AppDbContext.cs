using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore.DataAccessLayer.EF
{
    /// <summary>
    /// Контекст базы данных Entity Framework
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Инициализирует контекст с указанной строкой подключения
        /// </summary>
        /// <param name="connectionString">Строка подключения к SQL Server</param>
        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Коллекция товаров в базе данных
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Настраивает подключение к базе данных
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}