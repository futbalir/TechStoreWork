using Ninject;
using System;
using System.Windows;
using TechStore.Core;
using TechStore.Presentation;
using TechStore.WpfApp;

namespace TechStore.WpfApp
{
    /// <summary>
    /// Главный класс приложения WPF. Настраивает зависимости и запускает приложение.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Инициализирует приложение при запуске.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                
                var kernel = new StandardKernel(new NinjectConfig(useEntityFramework: true));
                var factory = kernel.Get<ViewModelFactory>();

                
                var wpfViewManager = new WpfViewManager();

                
                wpfViewManager.Register<ProductsViewModel, MainWindow>(() => new MainWindow());
                wpfViewManager.Register<EditProductViewModel, EditProductWindow>(() => new EditProductWindow());

                
                var viewModelManager = new ViewModelManager(wpfViewManager);

                
                var productsVm = factory.CreateProductsViewModel();

               
                productsVm.ViewModelManager = viewModelManager;

                
                viewModelManager.ShowProducts(productsVm);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Ошибка запуска приложения: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }
    }
}