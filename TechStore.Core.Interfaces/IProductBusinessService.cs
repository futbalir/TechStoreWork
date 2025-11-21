using System.Collections.Generic;
using TechStore.Models;

namespace TechStore.Core.Interfaces
{
    /// <summary>
    /// Интерфейс бизнес-сервиса для работы с товарами
    /// Объединяет CRUD операции и бизнес-логику (принцип ISP)
    /// </summary>
    public interface IProductBusinessService
    {
        /// <summary>
        /// Создает новый товар с валидацией
        /// </summary>
        /// <param name="product">Данные товара</param>
        /// <returns>Созданный товар с ID</returns>
        Product Create(Product product);

        /// <summary>
        /// Получает товар по идентификатору
        /// </summary>
        /// <param name="id">ID товара</param>
        /// <returns>Найденный товар или null</returns>
        Product GetById(int id);

        /// <summary>
        /// Получает все товары
        /// </summary>
        /// <returns>Список всех товаров</returns>
        List<Product> GetAll();

        /// <summary>
        /// Обновляет данные товара с валидацией
        /// </summary>
        /// <param name="product">Обновленные данные товара</param>
        void Update(Product product);

        /// <summary>
        /// Удаляет товар по идентификатору
        /// </summary>
        /// <param name="id">ID товара для удаления</param>
        void Delete(int id);

        /// <summary>
        /// Получает товары, которые есть в наличии
        /// </summary>
        /// <returns>Список товаров с количеством > 0</returns>
        List<Product> GetProductsInStock();

        /// <summary>
        /// Группирует товары по категориям
        /// </summary>
        /// <returns>Словарь категория → список товаров</returns>
        Dictionary<string, List<Product>> GroupProductsByCategory();
    }
}