using System.Collections;
using TechStore.Models;

namespace TechStore.Core.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с товарами
    /// </summary>
    public interface  IRepository 
    {
        /// <summary>
        /// Добавляет новый товар
        /// </summary>
        void Add(Product product);

        /// <summary>
        /// Удаляет товар по идентификатору
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// Получает все товары
        /// </summary>
        IList GetAllProducts();

        /// <summary>
        /// Находит товар по идентификатору
        /// </summary>
        Product GetProductById(int id);

        /// <summary>
        /// Обновляет данные товара
        /// </summary>
        void Update(Product product);
    }
}