using Ninject;
using TechStore.Core;
using TechStore.Core.Interfaces;
using TechStore.Models;

namespace TechStore.ConsoleApp
{
    internal class Program
    {
        private static IProductBusinessService _businessService; // ← ТЕПЕРЬ ТОЛЬКО ОДНА ЗАВИСИМОСТЬ!

        static void Main(string[] args)
        {
            // 1. Инициализация DI
            var kernel = new StandardKernel(new NinjectConfig());
            _businessService = kernel.Get<IProductBusinessService>(); // ← ТЕПЕРЬ ТОЛЬКО ОДИН СЕРВИС!

            // 2. Инициализация данных
            var seeder = kernel.Get<DatabaseSeeder>();

            while (true)
            {
                Console.WriteLine("=== МАГАЗИН ТЕХНИКИ ===");
                Console.WriteLine("1. Показать все товары");
                Console.WriteLine("2. Показать товары в наличии");
                Console.WriteLine("3. Сгруппировать по категориям");
                Console.WriteLine("4. Добавить новый товар");
                Console.WriteLine("5. Удалить товар");
                Console.WriteLine("6. Изменить товар");
                Console.WriteLine("7. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllProducts();
                        break;
                    case "2":
                        ShowProductsInStock();
                        break;
                    case "3":
                        GroupProductsByCategory();
                        break;
                    case "4":
                        AddNewProduct();
                        break;
                    case "5":
                        DeleteProduct();
                        break;
                    case "6":
                        UpdateProduct();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void ShowAllProducts()
        {
            var products = _businessService.GetAll(); // ← ТЕПЕРЬ ИЗ БИЗНЕС-СЕРВИСА!
            Console.WriteLine("\n=== ВСЕ ТОВАРЫ ===");
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        private static void ShowProductsInStock()
        {
            var products = _businessService.GetProductsInStock();
            Console.WriteLine("\n=== ТОВАРЫ В НАЛИЧИИ ===");
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        private static void GroupProductsByCategory()
        {
            var groupedProducts = _businessService.GroupProductsByCategory();
            Console.WriteLine("\n=== ГРУППИРОВКА ПО КАТЕГОРИЯМ ===");

            foreach (var group in groupedProducts)
            {
                Console.WriteLine($"\n--- {group.Key.ToUpper()} ---");
                foreach (var product in group.Value)
                {
                    Console.WriteLine($"  {product.Name} ({product.Price:N2} руб.)");
                }
            }
        }

        private static void AddNewProduct()
        {
            Console.WriteLine("\n=== ДОБАВЛЕНИЕ НОВОГО ТОВАРА ===");

            try
            {
                Console.Write("Введите название: ");
                string name = Console.ReadLine();

                Console.Write("Введите цену: ");
                decimal price = decimal.Parse(Console.ReadLine());

                Console.Write("Введите категорию: ");
                string category = Console.ReadLine();

                Console.Write("Введите количество на складе: ");
                int stock = int.Parse(Console.ReadLine());

                Console.Write("Введите производителя: ");
                string manufacturer = Console.ReadLine();

                var newProduct = new Product(0, name, price, category, stock, manufacturer);
                _businessService.Create(newProduct); // ← ТЕПЕРЬ ИЗ БИЗНЕС-СЕРВИСА!

                Console.WriteLine("Товар успешно добавлен!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private static void DeleteProduct()
        {
            Console.WriteLine("\n=== УДАЛЕНИЕ ТОВАРА ===");
            ShowAllProducts();

            Console.Write("Введите ID товара для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _businessService.Delete(id); // ← ТЕПЕРЬ ИЗ БИЗНЕС-СЕРВИСА!
                Console.WriteLine("Товар удален!");
            }
            else
            {
                Console.WriteLine("Неверный формат ID!");
            }
        }

        private static void UpdateProduct()
        {
            Console.WriteLine("\n=== ИЗМЕНЕНИЕ ТОВАРА ===");
            ShowAllProducts();

            Console.Write("Введите ID товара для изменения: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный формат ID!");
                return;
            }

            var existingProduct = _businessService.GetById(id); // ← ТЕПЕРЬ ИЗ БИЗНЕС-СЕРВИСА!
            if (existingProduct == null)
            {
                Console.WriteLine("Товар не найден!");
                return;
            }

            try
            {
                Console.Write("Введите новое название: ");
                string name = Console.ReadLine();

                Console.Write("Введите новую цену: ");
                decimal price = decimal.Parse(Console.ReadLine());

                Console.Write("Введите новую категорию: ");
                string category = Console.ReadLine();

                Console.Write("Введите новое количество: ");
                int stock = int.Parse(Console.ReadLine());

                Console.Write("Введите нового производителя: ");
                string manufacturer = Console.ReadLine();

                var updatedProduct = new Product(id, name, price, category, stock, manufacturer);
                _businessService.Update(updatedProduct); // ← ТЕПЕРЬ ИЗ БИЗНЕС-СЕРВИСА!

                Console.WriteLine("Товар успешно изменен!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}