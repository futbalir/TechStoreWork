using System;
using System.Collections;
using System.Windows.Forms;
using TechStore.Core.Interfaces;
using TechStore.Models;
using TechStore.Shared;

namespace TechStore.WinForms
{
    /// <summary>
    /// Главная форма приложения - реализация View в паттерне MVP
    /// Отвечает только за отображение UI и обработку пользовательского ввода
    /// </summary>
    public partial class Form1 : Form, IProductsView
    {
        
        public event EventHandler RefreshRequested;
        public event EventHandler ShowInStockRequested;
        public event EventHandler GroupByCategoryRequested;
        public event EventHandler AddProductRequested;
        public event EventHandler<Product> EditProductRequested;
        public event EventHandler<int> DeleteProductRequested;
        

        private ProductsPresenter _presenter;

        
        private bool _isDataLoading = false;
        private bool _isEditing = false;
        

        /// <summary>
        /// Инициализирует главную форму приложения
        /// </summary>
        /// <param name="businessService">Бизнес-сервис для работы с товарами</param>

        public Form1(IProductBusinessService businessService)
        {
            InitializeComponent();

            Console.WriteLine($"=== FORM1 CONSTRUCTOR ===");
            Console.WriteLine($"BusinessService hash: {businessService.GetHashCode()}");

            _presenter = new ProductsPresenter(this, businessService);
            Console.WriteLine($"=== PRESENTER CREATED ===\n");

            dataGridViewProducts.DataError += DataGridViewProducts_DataError;
            dataGridViewProducts.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Отображает список товаров в DataGridView
        /// </summary>
        /// <param name="products">Коллекция товаров для отображения</param>
        public void DisplayProducts(IList products)
        {
            if (_isEditing) return;

            try
            {
                _isDataLoading = true;

                dataGridViewProducts.DataSource = null;
                dataGridViewProducts.DataSource = products;

                int totalCount = products.Count;
                int inStockCount = 0;
                foreach (Product product in products)
                {
                    if (product.StockQuantity > 0) inStockCount++;
                }
                statusLabelInfo.Text = $"Товаров: {totalCount} | В наличии: {inStockCount}";

                dataGridViewProducts.ClearSelection();
                dataGridViewProducts.CurrentCell = null;
            }
            finally
            {
                _isDataLoading = false;
                
            }
        }

        /// <summary>
        /// Отображает товары, сгруппированные по категориям
        /// </summary>
        /// <param name="groupedProducts">Сгруппированная коллекция товаров</param>
        public void DisplayGroupedProducts(IList groupedProducts)
        {
            dataGridViewProducts.DataSource = null;
            dataGridViewProducts.DataSource = groupedProducts;
            ConfigureGroupedGridView();
        }

        /// <summary>
        /// Показывает информационное сообщение пользователю
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="caption">Заголовок окна сообщения</param>
        public void ShowMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Показывает сообщение об ошибке пользователю
        /// </summary>
        /// <param name="error">Текст ошибки</param>
        public void ShowError(string error)
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Сбрасывает выделение в DataGridView
        /// </summary>
        public void ClearSelection()
        {
            if (dataGridViewProducts.InvokeRequired)
            {
                dataGridViewProducts.Invoke(new MethodInvoker(() => {
                    dataGridViewProducts.ClearSelection();
                    dataGridViewProducts.CurrentCell = null;
                }));
            }
            else
            {
                dataGridViewProducts.ClearSelection();
                dataGridViewProducts.CurrentCell = null;
            }
        }

        
        private void buttonRefresh_Click(object sender, EventArgs e) => RefreshRequested?.Invoke(this, EventArgs.Empty);
        private void buttonInStock_Click(object sender, EventArgs e) => ShowInStockRequested?.Invoke(this, EventArgs.Empty);
        private void buttonGroup_Click(object sender, EventArgs e) => GroupByCategoryRequested?.Invoke(this, EventArgs.Empty);
        private void buttonAddProduct_Click(object sender, EventArgs e) => AddProductRequested?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Обработчик нажатия кнопки редактирования товара
        /// </summary>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (_isDataLoading || _isEditing) return;

            if (dataGridViewProducts.CurrentRow == null)
            {
                ShowError("Выберите товар для изменения!");
                return;
            }

            var selectedItem = dataGridViewProducts.CurrentRow.DataBoundItem;
            Product selectedProduct = null;

            if (selectedItem is Product product)
            {
                selectedProduct = product;
            }
            else if (selectedItem is ProductDisplayItem displayItem && !displayItem.IsCategoryHeader)
            {
                selectedProduct = new Product
                {
                    Id = displayItem.Id,
                    Name = displayItem.Name,
                    Price = displayItem.Price,
                    Category = displayItem.Category,
                    StockQuantity = displayItem.StockQuantity,
                    Manufacturer = displayItem.Manufacturer
                };
            }

            if (selectedProduct == null)
            {
                ShowError("Выберите товар для изменения (не заголовок категории)!");
                return;
            }

            _isEditing = true;
            try
            {
                EditProductRequested?.Invoke(this, selectedProduct);
            }
            finally
            {
                _isEditing = false;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки удаления товара
        /// </summary>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_isDataLoading || _isEditing) return;

            if (dataGridViewProducts.CurrentRow == null)
            {
                ShowError("Выберите товар для удаления!");
                return;
            }

            var selectedItem = dataGridViewProducts.CurrentRow.DataBoundItem;
            int productId = -1;

            if (selectedItem is Product product)
            {
                productId = product.Id;
            }
            else if (selectedItem is ProductDisplayItem displayItem && !displayItem.IsCategoryHeader)
            {
                productId = displayItem.Id;
            }

            if (productId == -1)
            {
                ShowError("Выберите товар для удаления (не заголовок категории)!");
                return;
            }

            if (MessageBox.Show($"Вы уверены, что хотите удалить товар?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _isEditing = true;
                DeleteProductRequested?.Invoke(this, productId);
                _isEditing = false;
            }
        }


        /// <summary>
        /// Обработчик изменения выделения в DataGridView
        /// </summary>
        private void dataGridViewProducts_SelectionChanged(object sender, EventArgs e)
        {
           
            if (_isDataLoading || _isEditing || dataGridViewProducts.CurrentRow == null)
            {
                return;
            }

            var selectedItem = dataGridViewProducts.CurrentRow.DataBoundItem;
            if (selectedItem is ProductDisplayItem displayItem && displayItem.IsCategoryHeader)
            {
                return;
            }
        }

        private DateTime _lastDataUpdate = DateTime.MinValue;


        

        /// <summary>
        /// Настраивает DataGridView для отображения сгруппированных данных
        /// </summary>
        private void ConfigureGroupedGridView()
        {
            dataGridViewProducts.AutoGenerateColumns = false;
            dataGridViewProducts.Columns.Clear();

            dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Category",
                HeaderText = "Категория",
                DataPropertyName = "Category",
                Width = 120
            });

            dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Name",
                HeaderText = "Название товара",
                DataPropertyName = "Name",
                Width = 150
            });

            dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Price",
                HeaderText = "Цена",
                DataPropertyName = "Price",
                Width = 80
            });

            dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "StockQuantity",
                HeaderText = "В наличии",
                DataPropertyName = "StockQuantity",
                Width = 70
            });

            dataGridViewProducts.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Manufacturer",
                HeaderText = "Производитель",
                DataPropertyName = "Manufacturer",
                Width = 100
            });

            dataGridViewProducts.CellFormatting += DataGridViewProducts_CellFormatting;
        }

        /// <summary>
        /// Форматирование ячеек DataGridView для сгруппированного отображения
        /// </summary>
        private void DataGridViewProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewProducts.Rows.Count) return;

            var row = dataGridViewProducts.Rows[e.RowIndex];
            if (row.DataBoundItem is ProductDisplayItem item)
            {
                if (item.IsCategoryHeader)
                {
                    e.CellStyle.BackColor = Color.LightBlue;
                    e.CellStyle.Font = new Font(dataGridViewProducts.Font, FontStyle.Bold);
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    if (e.ColumnIndex == 0)
                    {
                        e.Value = $"--- {item.Category?.ToUpper()} ---";
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
                else
                {
                    e.CellStyle.BackColor = Color.White;
                    e.CellStyle.Font = dataGridViewProducts.Font;
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
            }
        }

        /// <summary>
        /// Обработчик ошибок DataGridView
        /// </summary>
        private void DataGridViewProducts_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}