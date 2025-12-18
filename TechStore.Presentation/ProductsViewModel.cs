using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TechStore.Core.Interfaces;
using TechStore.Models;

namespace TechStore.Presentation;

/// <summary>
/// ViewModel для управления списком товаров и операциями над ними.
/// Реализует логику отображения, фильтрации и редактирования товаров.
/// </summary>
public class ProductsViewModel : BaseViewModel
{
    #region Поля

    private readonly IProductBusinessService _businessService;
    private ProductDto? _selectedProduct;
    private bool _isGrouped;

    #endregion

    #region Конструктор

    /// <summary>
    /// Инициализирует новый экземпляр ProductsViewModel с указанным бизнес-сервисом.
    /// </summary>
    /// <param name="businessService">Сервис для работы с бизнес-логикой товаров.</param>
    public ProductsViewModel(IProductBusinessService businessService)
    {
        _businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));

        Products = new ObservableCollection<ProductDto>();
        SelectedProduct = null;
        IsGrouped = false;

        InitializeCommands();
        LoadProducts();
    }

    #endregion

    #region Свойства

    /// <summary>
    /// Коллекция товаров для отображения в UI.
    /// </summary>
    public ObservableCollection<ProductDto> Products { get; }

    /// <summary>
    /// Выбранный в списке товар. Null, если товар не выбран.
    /// </summary>
    public ProductDto? SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            if (SetField(ref _selectedProduct, value))
            {
                
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
                ((RelayCommand)EditCommand).RaiseCanExecuteChanged();
            }
        }
    }

    /// <summary>
    /// Флаг, указывающий включена ли группировка товаров по категориям.
    /// </summary>
    public bool IsGrouped
    {
        get => _isGrouped;
        private set => SetField(ref _isGrouped, value);
    }

    /// <summary>
    /// Текст для кнопки группировки, меняется в зависимости от состояния.
    /// </summary>
    public string GroupButtonText => IsGrouped ? "Показать все" : "Сгруппировать";

    /// <summary>
    /// Менеджер навигации для управления открытием окон.
    /// </summary>
    public ViewModelManager? ViewModelManager { get; set; }

    #endregion

    #region Команды

    /// <summary>
    /// Команда для загрузки списка товаров.
    /// </summary>
    public ICommand LoadCommand { get; private set; }

    /// <summary>
    /// Команда для добавления нового товара.
    /// </summary>
    public ICommand AddCommand { get; private set; }

    /// <summary>
    /// Команда для удаления выбранного товара.
    /// </summary>
    public ICommand DeleteCommand { get; private set; }

    /// <summary>
    /// Команда для редактирования выбранного товара.
    /// </summary>
    public ICommand EditCommand { get; private set; }

    /// <summary>
    /// Команда для переключения режима группировки товаров по категориям.
    /// </summary>
    public ICommand GroupCommand { get; private set; }

    #endregion

    #region Приватные методы

    /// <summary>
    /// Инициализирует команды ViewModel.
    /// </summary>
    private void InitializeCommands()
    {
        LoadCommand = new RelayCommand(_ => LoadProducts());
        AddCommand = new RelayCommand(_ => AddProduct());
        DeleteCommand = new RelayCommand(
            execute: _ => DeleteProduct(),
            canExecute: _ => SelectedProduct != null
        );
        EditCommand = new RelayCommand(
            execute: _ => EditProduct(),
            canExecute: _ => SelectedProduct != null
        );
        GroupCommand = new RelayCommand(_ => ToggleGrouping());
    }

    /// <summary>
    /// Загружает список товаров из бизнес-слоя.
    /// </summary>
    private void LoadProducts()
    {
        var products = _businessService.GetAll();
        var sortedProducts = IsGrouped
            ? products.OrderBy(p => p.Category)  
            : products.OrderBy(p => p.Id);       

        Products.Clear();
        foreach (var product in sortedProducts)
        {
            Products.Add(ProductDto.FromModel(product));
        }
    }

    /// <summary>
    /// Добавляет новый товар с параметрами по умолчанию.
    /// </summary>
    private void AddProduct()
    {
        var newProduct = new Product
        {
            Name = $"Новый товар {Products.Count + 1}",
            Price = 999.99m,
            StockQuantity = 10,
            Category = "Общее",
            Manufacturer = "Неизвестно"
        };

        _businessService.Create(newProduct);
        LoadProducts(); 
    }

    /// <summary>
    /// Удаляет выбранный товар.
    /// </summary>
    private void DeleteProduct()
    {
        if (SelectedProduct == null) return;

        _businessService.Delete(SelectedProduct.Id);
        LoadProducts(); // Обновляем список после удаления
    }

    /// <summary>
    /// Открывает окно редактирования для выбранного товара.
    /// </summary>
    private void EditProduct()
    {
        if (SelectedProduct == null || ViewModelManager == null) return;

        var editVm = new EditProductViewModel(_businessService);
        editVm.Initialize(SelectedProduct.Clone());
        editVm.OnSave += _ => LoadProducts(); 

        ViewModelManager.ShowEditProductDialog(editVm);
    }

    /// <summary>
    /// Переключает режим группировки товаров по категориям.
    /// </summary>
    private void ToggleGrouping()
    {
        IsGrouped = !IsGrouped;
        OnPropertyChanged(nameof(GroupButtonText)); 
        LoadProducts(); 
    }

    #endregion

    #region События (оставлены для обратной совместимости)

    /// <summary>
    /// Событие, возникающее при запросе открытия диалога редактирования.
    /// Используется для обратной совместимости.
    /// </summary>
    public event Action<EditProductViewModel>? RequestEditDialog;

    #endregion
}