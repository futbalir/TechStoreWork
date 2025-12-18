using System.Windows.Input;
using TechStore.Core.Interfaces;
using TechStore.Models;

namespace TechStore.Presentation;

public class EditProductViewModel : BaseViewModel
{
    private readonly IProductBusinessService _businessService;
    private ProductDto? _product;

    public EditProductViewModel(IProductBusinessService businessService)
    {
        _businessService = businessService;
    }

    public ProductDto? Product
    {
        get => _product;
        set => SetField(ref _product, value);
    }

    public ICommand SaveCommand { get; private set; } // ← добавь private set
    public ICommand CancelCommand { get; private set; } // ← добавь private set

    public event Action<ProductDto?> OnSave;
    public event Action OnCancel;
    private bool? _dialogResult;
    public bool? DialogResult
    {
        get => _dialogResult;
        set => SetField(ref _dialogResult, value);
    }

    public void Initialize(ProductDto product)
    {
        Product = product;
        SaveCommand = new RelayCommand(_ => Save());
        CancelCommand = new RelayCommand(_ => Cancel());
    }

    private void Save()
    {
        if (Product == null) return;

        var model = Product.ToModel(); // ← метод ToModel должен быть в ProductDto
        _businessService.Update(model);
        OnSave?.Invoke(Product);

        DialogResult = true;
    }

    public void Cancel()
    {
        OnCancel?.Invoke();
        DialogResult = false;
    }
}