using System;
using System.Collections.Generic;
using System.Windows;
using TechStore.Presentation;

namespace TechStore.WpfApp
{
    /// <summary>
    /// Реализация ViewManager для WPF приложения. Управляет созданием и отображением окон.
    /// </summary>
    public class WpfViewManager : ViewManager
    {
        private readonly Dictionary<Type, Func<Window>> _windowFactories = new();

        /// <summary>
        /// Регистрирует фабричный метод для создания окна, соответствующего указанному ViewModel.
        /// </summary>
       
        public void Register<TViewModel, TWindow>(Func<TWindow> factory)
            where TWindow : Window
            where TViewModel : BaseViewModel
        {
            _windowFactories[typeof(TViewModel)] = factory;
        }

        /// <summary>
        /// Создает окно для указанного типа ViewModel, используя зарегистрированный фабричный метод.
        /// </summary>
        
        private Window GetWindow(Type viewModelType)
        {
            if (_windowFactories.TryGetValue(viewModelType, out var factory))
            {
                return factory();
            }
            throw new InvalidOperationException($"No window registered for ViewModel: {viewModelType.Name}");
        }

        /// <summary>
        /// Отображает главное окно приложения со списком товаров.
        /// </summary>
          
        public override void ShowProducts(ProductsViewModel viewModel)
        {
            var window = GetWindow(typeof(ProductsViewModel));
            window.DataContext = viewModel;
            window.Show();
        }

        /// <summary>
        /// Отображает диалоговое окно редактирования товара.
        /// </summary>
       
        public override bool? ShowEditProduct(EditProductViewModel viewModel)
        {
            var window = GetWindow(typeof(EditProductViewModel));
            window.DataContext = viewModel;

            
            viewModel.OnSave += (product) =>
            {
                if (window.IsLoaded)
                {
                    window.DialogResult = true;
                }
            };

            viewModel.OnCancel += () =>
            {
                if (window.IsLoaded)
                {
                    window.DialogResult = false;
                }
            };

            
            window.Closing += (s, e) =>
            {
                if (window.DialogResult == null)
                {
                    viewModel.Cancel();
                }
            };

            return window.ShowDialog();
        }
    }
}