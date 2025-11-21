using System.Windows.Forms;
using TechStore.Models;

namespace TechStore.WinForms
{
    /// <summary>
    /// Форма для редактирования данных товара
    /// </summary>
    public partial class EditProductForm : Form
    {
        /// <summary>
        /// Получает отредактированный товар после закрытия формы
        /// </summary>
        public Product EditedProduct { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр формы редактирования товара
        /// </summary>
        /// <param name="productToEdit">Товар для редактирования</param>
        public EditProductForm(Product productToEdit)
        {
            InitializeComponent();
            EditedProduct = productToEdit;

            textBoxName.Text = productToEdit.Name;
            textBoxPrice.Text = productToEdit.Price.ToString();
            textBoxCategory.Text = productToEdit.Category;
            textBoxStock.Text = productToEdit.StockQuantity.ToString();
            textBoxManufacturer.Text = productToEdit.Manufacturer;
        }

        /// <summary>
        /// Обработчик нажатия кнопки сохранения изменений
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                EditedProduct.Name = textBoxName.Text;
                EditedProduct.Price = decimal.Parse(textBoxPrice.Text);
                EditedProduct.Category = textBoxCategory.Text;
                EditedProduct.StockQuantity = int.Parse(textBoxStock.Text);
                EditedProduct.Manufacturer = textBoxManufacturer.Text;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки отмены редактирования
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}