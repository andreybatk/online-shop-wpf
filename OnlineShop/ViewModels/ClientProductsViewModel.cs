using System.Data;
using OnlineShop.ViewModels.Base;

namespace OnlineShop.ViewModels
{
    internal class ClientProductsViewModel : ViewModel
    {
        private DataView _dataView;

        public ClientProductsViewModel(DataView dataView)
        {
            this._dataView = dataView;
        }

        public DataView ClientProductsDataView { get => _dataView; set => Set(ref _dataView, value); }
    }
}
