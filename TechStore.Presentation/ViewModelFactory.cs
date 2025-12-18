using TechStore.Core.Interfaces;

namespace TechStore.Presentation;

public class ViewModelFactory
{
    private readonly IProductBusinessService _businessService;

    public ViewModelFactory(IProductBusinessService businessService)
    {
        _businessService = businessService;
    }

    public ProductsViewModel CreateProductsViewModel() => new(_businessService);
}