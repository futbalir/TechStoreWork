using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechStore.Core.Interfaces;
using TechStore.Models;
using TechStore.Shared;
using System.Windows.Forms;
using TechStore.WinForms;

namespace TechStore.Presentation
{
    /// <summary>
    /// Presenter в паттерне MVP - координатор между View и бизнес-логикой
    /// Обрабатывает события от View и делегирует выполнение бизнес-сервисам
    /// </summary>
    public class ProductsPresenter
    {
        private readonly IProductsView _view;
        private readonly IProductBusinessService _businessService;

        /// <summary>
        /// Инициализирует презентер с зависимостями
        /// </summary>
        /// <param name="view">Представление для отображения данных</param>
        /// <param name="businessService">Бизнес-сервис для операций с товарами</param>
        public ProductsPresenter(
            IProductsView view,
            IProductBusinessService businessService
        )
        {
            _view = view;
            _businessService = businessService;

            
            _view.RefreshRequested += OnRefreshRequested;
            _view.ShowInStockRequested += OnShowInStockRequested;
            _view.GroupByCategoryRequested += OnGroupByCategoryRequested;
            _view.AddProductRequested += OnAddProductRequested;
            _view.EditProductRequested += OnEditProductRequested;
            _view.DeleteProductRequested += OnDeleteProductRequested;

            
            LoadProducts();
        }

        /// <summary>
        /// Загружает все товары и отображает их в View
        /// </summary>
        private void LoadProducts()
        {
            try
            {
                var products = _businessService.GetAll(); 
                _view.DisplayProducts(new ArrayList(products));
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик запроса обновления данных
        /// </summary>
        private void OnRefreshRequested(object sender, EventArgs e)
        {
            LoadProducts();
            _view.ShowMessage("Данные обновлены", "Информация");
        }


        /// <summary>
        /// Обработчик запроса показа товаров в наличии
        /// </summary>
        private void OnShowInStockRequested(object sender, EventArgs e)
        {
            try
            {
                var inStockProducts = _businessService.GetProductsInStock();
                _view.DisplayProducts(new ArrayList(inStockProducts));
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик запроса группировки товаров по категориям
        /// </summary>
        private void OnGroupByCategoryRequested(object sender, EventArgs e)
        {
            try
            {
                var groupedProducts = _businessService.GroupProductsByCategory();
                var displayList = new ArrayList();

                foreach (var category in groupedProducts.Keys)
                {
                    
                    displayList.Add(new ProductDisplayItem
                    {
                        IsCategoryHeader = true,
                        CategoryHeader = $"--- {category.ToUpper()} ---",
                        Category = category
                    });

                    foreach (var product in groupedProducts[category])
                    {
                        
                        displayList.Add(new ProductDisplayItem
                        {
                            IsCategoryHeader = false,
                            Id = product.Id,
                            Name = product.Name,
                            Price = product.Price,
                            StockQuantity = product.StockQuantity,
                            Manufacturer = product.Manufacturer,
                            Category = product.Category
                        });
                    }
                }

                _view.DisplayGroupedProducts(displayList);
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка группировки: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик запроса добавления нового товара
        /// </summary>
        private void OnAddProductRequested(object sender, EventArgs e)
        {
            try
            {
                var random = new Random();
                var sampleProducts = new[]
                {
                    new Product(0, "PlayStation 5", 49999m, "Игровая консоль", random.Next(1, 10), "Sony"),
                    new Product(0, "Xbox Series X", 45999m, "Игровая консоль", random.Next(1, 10), "Microsoft"),
                    new Product(0, "MacBook Air", 99999m, "Ноутбук", random.Next(1, 10), "Apple")
                };

                var randomProduct = sampleProducts[random.Next(sampleProducts.Length)];

                
                var createdProduct = _businessService.Create(randomProduct);

                _view.ShowMessage($"Товар '{createdProduct.Name}' добавлен!", "Успех");
                LoadProducts();
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка добавления: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик запроса редактирования товара
        /// </summary>
        /// <param name="product">Товар для редактирования</param>
        private void OnEditProductRequested(object sender, Product product)
        {
            Console.WriteLine($"=== EDIT STARTED ===");
            Console.WriteLine($"Time: {DateTime.Now:HH:mm:ss.fff}");
            Console.WriteLine($"Product: {product.Name} (ID: {product.Id})");
            Console.WriteLine($"Stack trace: {Environment.StackTrace}");

            try
            {
                var productCopy = new Product(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Category,
                    product.StockQuantity,
                    product.Manufacturer
                );

                using (var editForm = new EditProductForm(productCopy))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        Console.WriteLine($"=== SAVING PRODUCT ===");
                        _businessService.Update(editForm.EditedProduct);
                        Console.WriteLine($"=== PRODUCT SAVED ===");

                        _view.ShowMessage("Товар успешно обновлен!", "Успех");
                        LoadProducts();
                    }
                    else
                    {
                        Console.WriteLine($"=== EDIT CANCELLED ===");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== ERROR: {ex.Message} ===");
                _view.ShowError($"Ошибка обновления: {ex.Message}");
            }

            Console.WriteLine($"=== EDIT FINISHED ===\n");
        }

        /// <summary>
        /// Обработчик запроса удаления товара
        /// </summary>
        /// <param name="productId">ID товара для удаления</param>
        private void OnDeleteProductRequested(object sender, int productId)
        {
            try
            {
                
                var product = _businessService.GetById(productId);
                if (product == null)
                {
                    _view.ShowError("Товар не найден");
                    return;
                }

                
                _businessService.Delete(productId);

                _view.ShowMessage("Товар удален!", "Успех");
                LoadProducts();
            }
            catch (Exception ex)
            {
                _view.ShowError($"Ошибка удаления: {ex.Message}");
            }
        }
    }
}