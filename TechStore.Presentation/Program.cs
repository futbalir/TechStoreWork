// TechStore.Presentation/Program.cs
using System.Windows.Forms;
using Ninject;
using TechStore.Core;
using TechStore.Shared;
using TechStore.WinForms;

namespace TechStore.Presentation
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Выбор технологии
            var choice = MessageBox.Show(
                "Использовать Entity Framework?",
                "Выбор технологии",
                MessageBoxButtons.YesNo
            );
            bool useEF = (choice == DialogResult.Yes);

            // 2. Создаём DI-контейнер
            IKernel kernel = new StandardKernel(new NinjectConfig(useEF));

            // 3. РЕГИСТРИРУЕМ ВСЕ КОМПОНЕНТЫ MVP
            kernel.Bind<Form1>().ToSelf().InSingletonScope();
            kernel.Bind<IProductsView>().ToMethod(ctx => ctx.Kernel.Get<Form1>());
            kernel.Bind<ProductsPresenter>().ToSelf().InSingletonScope(); 

            // 4. Инициализируем сидер
            kernel.Get<DatabaseSeeder>();

            // 5. СОЗДАЁМ И СВЯЗЫВАЕМ MVP-КОМПОНЕНТЫ
            var view = kernel.Get<IProductsView>();
            var presenter = kernel.Get<ProductsPresenter>(); 
            // 6. Запускаем приложение
            Application.Run((Form1)view);
        }
    }
}