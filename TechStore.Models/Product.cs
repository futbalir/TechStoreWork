namespace TechStore.Models
{
    /// <summary>
    /// Модель товара
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int StockQuantity { get; set; }
        public string Manufacturer { get; set; }

        public Product() { }

        public Product(int id, string name, decimal price, string category, int stockQuantity, string manufacturer)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
            StockQuantity = stockQuantity;
            Manufacturer = manufacturer;
        }
        /// <summary>
        /// Переопределение метода для красивого отображения в консоли
        /// </summary>
        public override string ToString()
        {
            return $"{Name} ({Price:N2} руб.) - В наличии: {StockQuantity} шт. - {Manufacturer}";
        }
    }
}