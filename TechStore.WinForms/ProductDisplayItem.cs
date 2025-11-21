public class ProductDisplayItem
{
    
    public string Category { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Manufacturer { get; set; }

    
    public bool IsCategoryHeader { get; set; }
    public string CategoryHeader { get; set; }
    public int Id { get; set; }
    public string Description { get; set; }
}