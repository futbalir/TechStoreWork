using TechStore.Core.Interfaces;
using TechStore.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TechStore.DataAccessLayer.EF
{
    /// <summary>
    /// Репозиторий Entity Framework для работы с товарами
    /// </summary>
    public class EntityRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Создает новый экземпляр репозитория с внедренным контекстом
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public EntityRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет новый товар
        /// </summary>
        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        /// <summary>
        /// Удаляет товар по идентификатору
        /// </summary>
        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Получает все товары
        /// </summary>
        public IList GetAllProducts()
        {
            return _context.Products.AsNoTracking().ToList();
        }

        /// <summary>
        /// Находит товар по идентификатору
        /// </summary>
        public Product GetProductById(int id)
        {
            return _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Обновляет данные товара
        /// </summary>
        public void Update(Product product)
        {
            
            var existing = _context.Products.Find(product.Id);
            if (existing == null)
                throw new Exception("Товар не найден");

            
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Category = product.Category;
            existing.StockQuantity = product.StockQuantity;
            existing.Manufacturer = product.Manufacturer;

            _context.SaveChanges();
        }
    }
}