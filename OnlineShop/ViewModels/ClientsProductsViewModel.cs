using DataBaseFirst;
using OnlineShop.ViewModels.Base;
using System.ComponentModel;

namespace OnlineShop.ViewModels
{
    internal class ClientsProductsViewModel : ViewModel
    {
        private BindingList<Products> _productsList;

        public ClientsProductsViewModel(BindingList<Products> productsList)
        {
            this.ClientsProducts = productsList;
        }

        public BindingList<Products> ClientsProducts { get => _productsList; set => Set(ref _productsList, value); }
    }
}