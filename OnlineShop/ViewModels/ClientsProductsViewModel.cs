using System.Data;
using OnlineShop.ViewModels.Base;

namespace OnlineShop.ViewModels
{
    internal class ClientsProductsViewModel : ViewModel
    {
        private DataView _dataView;

        public ClientsProductsViewModel(DataView dataView)
        {
            this._dataView = dataView;
        }

        public DataView ClientsProductsDataView { get => _dataView; set => Set(ref _dataView, value); }
    }
}
