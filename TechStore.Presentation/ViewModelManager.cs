// TechStore.Presentation/ViewModelManager.cs
using System;

namespace TechStore.Presentation
{
    /// <summary>
    /// Менеджер навигации для ViewModel. Инкапсулирует логику открытия окон.
    /// </summary>
    public class ViewModelManager
    {
        private readonly ViewManager _viewManager;

        /// <summary>
        /// Инициализирует новый экземпляр ViewModelManager.
        /// </summary>
        /// <param name="viewManager">Абстрактный ViewManager для работы с UI-слоем.</param>
        public ViewModelManager(ViewManager viewManager)
        {
            _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));
        }

        /// <summary>
        /// Показывает главное окно со списком товаров.
        /// </summary>
        /// <param name="viewModel">ViewModel для главного окна.</param>
        public void ShowProducts(ProductsViewModel viewModel)
        {
            // Передаем менеджера в ViewModel
            viewModel.ViewModelManager = this; // Используем this, а не ViewManager!
            _viewManager.ShowProducts(viewModel);
        }

        /// <summary>
        /// Показывает диалоговое окно редактирования товара.
        /// </summary>
        /// <param name="viewModel">ViewModel для окна редактирования.</param>
        /// <returns>Результат диалога (true - сохранено, false - отмена, null - закрыто).</returns>
        public bool? ShowEditProductDialog(EditProductViewModel viewModel)
        {
            return _viewManager.ShowEditProduct(viewModel);
        }
    }
}