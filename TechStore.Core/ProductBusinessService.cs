using System.Collections.Generic;
using System.Linq;
using TechStore.Core.Interfaces;
using TechStore.Models;

namespace TechStore.Core
{
    /// <summary>
    /// Реализация бизнес-сервиса для работы с товарами
    /// Содержит бизнес-логику и валидацию (принцип SRP)
    /// </summary>
    public class ProductBusinessService : IProductBusinessService
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// Инициализирует бизнес-сервис с репозиторием
        /// </summary>
        /// <param name="repository">Репозиторий для доступа к данным</param>
        public ProductBusinessService(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Создает новый товар с валидацией бизнес-правил
        /// </summary>
        public Product Create(Product product)
        {
            ValidateProduct(product);
            _repository.Add(product);
            return product;
        }

        /// <summary>
        /// Получает все товары из репозитория
        /// </summary>
        public List<Product> GetAll()
        {
            return _repository.GetAllProducts() as List<Product> ?? new List<Product>();
        }

        /// <summary>
        /// Находит товар по идентификатору
        /// </summary>
        public Product GetById(int id)
        {
            return _repository.GetProductById(id);
        }

        /// <summary>
        /// Обновляет данные товара с проверкой существования и валидацией
        /// </summary>
        public void Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing == null)
                throw new System.Exception("Товар не найден");

            ValidateProduct(product);
            _repository.Update(product);
        }

        /// <summary>
        /// Удаляет товар по идентификатору
        /// </summary>
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        /// <summary>
        /// Получает товары, которые есть в наличии (количество > 0)
        /// </summary>
        public List<Product> GetProductsInStock()
        {
            return GetAll()
                .Where(p => p.StockQuantity > 0)
                .ToList();
        }

        /// <summary>
        /// Группирует товары по категориям для аналитики
        /// </summary>
        public Dictionary<string, List<Product>> GroupProductsByCategory()
        {
            return GetAll()
                .GroupBy(p => p.Category)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Валидирует товар согласно бизнес-правилам
        /// </summary>
        /// <param name="product">Товар для валидации</param>
        /// <exception cref="System.Exception">Выбрасывается при нарушении бизнес-правил</exception>
        private void ValidateProduct(Product product)
        {
            if (product.Price < 0)
                throw new System.Exception("Цена не может быть отрицательной");

            if (product.StockQuantity < 0)
                throw new System.Exception("Количество не может быть отрицательным");
        }
    }
}