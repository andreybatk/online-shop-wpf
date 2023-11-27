using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using OnlineShop.ViewModels.Base;
using OnlineShop.Views.Windows;

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
