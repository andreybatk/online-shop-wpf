using DataBaseFirst;
using OnlineShop.ViewModels.Base;
using System.Collections.Generic;

namespace OnlineShop.ViewModels
{
    internal class ClientProductsViewModel : ViewModel
    {
        private List<Products> _productsList;

        public ClientProductsViewModel(List<Products> productsList)
        {
            this._productsList = productsList;
        }

        public List<Products> ClientProducts { get => _productsList; set => Set(ref _productsList, value); }
    }
}
