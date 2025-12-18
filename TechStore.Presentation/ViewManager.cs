// TechStore.Presentation/ViewManager.cs
using System;

namespace TechStore.Presentation
{
    public abstract class ViewManager
    {
        
        public abstract void ShowProducts(ProductsViewModel viewModel);
        public abstract bool? ShowEditProduct(EditProductViewModel viewModel);
    }
}