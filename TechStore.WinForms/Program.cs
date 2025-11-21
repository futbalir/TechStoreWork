// TechStore.WinForms/Program.cs
using System.Windows.Forms;
using Ninject;
using TechStore.Core;
using TechStore.Shared;

namespace TechStore.WinForms
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

            // 3. РЕГИСТРИРУЕМ ФОРМУ КАК СИНГЛТОН
            kernel.Bind<Form1>().ToSelf().InSingletonScope();
            kernel.Bind<IProductsView>().ToMethod(ctx => ctx.Kernel.Get<Form1>());

            // 4. НЕ РЕГИСТРИРУЕМ ProductsPresenter здесь!
            // kernel.Bind<ProductsPresenter>().ToSelf().InSingletonScope(); ← УБРАТЬ!

            // 5. Инициализируем сидер
            kernel.Get<DatabaseSeeder>();

            // 6. СОЗДАЁМ ТОЛЬКО ФОРМУ - презентер создастся автоматически в конструкторе Form1
            var mainForm = kernel.Get<Form1>();
            Application.Run(mainForm);
        }
    }
}