using DataBaseFirst;
using OnlineShop.Infrastructure.Commands;
using OnlineShop.ViewModels.Base;
using OnlineShop.Views.Windows;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace OnlineShop.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private BindingList<Clients> _clientsList;
        private Clients _currentClient;
        private MsSqlOnlineShopEntities dbContext;

        public MainWindowViewModel()
        {
            dbContext = new MsSqlOnlineShopEntities();

            Preparing();
            AddClientCommand = new RelayCommand(OnAddClientCommandExecuted);
            DeleteClientCommand = new RelayCommand(OnDeleteClientCommandExecuted);
            ClientsProductsCommand = new RelayCommand(OnClientsProductsCommandExecuted);
            ClientProductsCommand = new RelayCommand(OnClientProductsCommandExecuted);
        }

        public BindingList<Clients> ClientsList { get => _clientsList; set => Set(ref _clientsList, value); }
        public Clients ClientsListSelectedItem { get => _currentClient; set => Set(ref _currentClient, value); }
        public ICommand AddClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand ClientsProductsCommand { get; }
        public ICommand ClientProductsCommand { get; }

        private void Preparing()
        {
            dbContext.Clients.Load();
            ClientsList = dbContext.Clients.Local.ToBindingList<Clients>();
        }
        private void OnAddClientCommandExecuted(object p)
        {
            Clients client = new Clients();

            AddClient addClient = new AddClient();
            AddClientViewModel addClientViewModel = new AddClientViewModel(addClient, client);
            addClient.DataContext = addClientViewModel;
            addClient.ShowDialog();

            if (addClient.DialogResult.Value)
            {
                dbContext.Clients.Add(client);
                dbContext.SaveChanges();
            }
        }
        private void OnDeleteClientCommandExecuted(object p)
        {
            try
            {
                dbContext.Clients.Remove(ClientsListSelectedItem);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
        private void OnClientsProductsCommandExecuted(object p)
        {
            try
            {
                dbContext.Products.Load();
                var products = dbContext.Products.Local.ToBindingList<Products>();
                ClientsProducts сlientsProducts = new ClientsProducts();
                ClientsProductsViewModel сlientsProductsViewModel = new ClientsProductsViewModel(products);
                сlientsProducts.DataContext = сlientsProductsViewModel;
                сlientsProducts.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
        private void OnClientProductsCommandExecuted(object p)
        {
            try
            {
                Clients client = ClientsListSelectedItem;
                var email = client.Email;

                dbContext.Products.Load();
                var clientProducts = dbContext.Products.Where(x => x.Email == email).ToList();

                ClientProducts сlientProducts = new ClientProducts();
                ClientProductsViewModel сlientProductsViewModel = new ClientProductsViewModel(clientProducts);
                сlientProducts.DataContext = сlientProductsViewModel;
                сlientProducts.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
    }
}