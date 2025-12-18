namespace TechStore.Presentation;

public class ProductDto : BaseViewModel
{
    private int _id;
    private string _name = string.Empty;
    private decimal _price;
    private string _category;
    private int _stockQuantity;
    private string _manufacturer = string.Empty;

    public int Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public decimal Price
    {
        get => _price;
        set => SetField(ref _price, value);
    }

    public string Category
    {
        get => _category;
        set => SetField(ref _category, value);
    }

    public int StockQuantity
    {
        get => _stockQuantity;
        set => SetField(ref _stockQuantity, value);
    }

    public string Manufacturer
    {
        get => _manufacturer;
        set => SetField(ref _manufacturer, value);
    }

    // Метод для маппинга из сущности
    public static ProductDto FromModel(TechStore.Models.Product product) =>
        new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Category = product.Category,
            StockQuantity = product.StockQuantity,
            Manufacturer = product.Manufacturer
        };
    public TechStore.Models.Product ToModel() =>
    new()
    {
        Id = Id,
        Name = Name,
        Price = Price,
        StockQuantity = StockQuantity,
        Category = Category,
        Manufacturer = Manufacturer
    };

    public ProductDto Clone()
    {
        return new ProductDto
        {
            Id = Id,
            Name = Name,
            Price = Price,
            Category = Category,
            StockQuantity = StockQuantity,
            Manufacturer = Manufacturer
        };
    }
}