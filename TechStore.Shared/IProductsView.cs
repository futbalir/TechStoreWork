using System;
using System.Collections;
using TechStore.Models;

namespace TechStore.Shared
{

    /// <summary>
    /// Интерфейс представления в паттерне MVP
    /// Определяет контракт для отображения данных и обработки пользовательского ввода
    /// </summary>
    public interface IProductsView
    {
        event EventHandler RefreshRequested;
        event EventHandler ShowInStockRequested;
        event EventHandler GroupByCategoryRequested;
        event EventHandler AddProductRequested;
        event EventHandler<Product> EditProductRequested;
        event EventHandler<int> DeleteProductRequested;

        /// <summary>
        /// Отображает список товаров
        /// </summary>
        /// <param name="products">Коллекция товаров для отображения</param>
        void DisplayProducts(IList products);

        /// <summary>
        /// Отображает товары, сгруппированные по категориям
        /// </summary>
        /// <param name="groupedProducts">Сгруппированная коллекция товаров</param>
        void DisplayGroupedProducts(IList groupedProducts);

        /// <summary>
        /// Показывает информационное сообщение
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="caption">Заголовок окна</param>
        void ShowMessage(string message, string caption);

        /// <summary>
        /// Показывает сообщение об ошибке
        /// </summary>
        /// <param name="error">Текст ошибки</param>
        void ShowError(string error);

        /// <summary>
        /// Сбрасывает выделение в UI элементах
        /// </summary>
        void ClearSelection();

       
    }
}