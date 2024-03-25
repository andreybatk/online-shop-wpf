using DataBaseFirst;
using OnlineShop.Infrastructure.Commands;
using OnlineShop.ViewModels.Base;
using OnlineShop.Views.Windows;
using System;
using System.Windows;
using System.Windows.Input;

namespace OnlineShop.ViewModels
{
    internal class AddClientViewModel : ViewModel
    {
        private AddClient _addClient;
        private Clients _client;

        public AddClientViewModel(AddClient addClient, Clients client)
        {
            this._client = client;
            this._addClient = addClient;
            OkCommand = new RelayCommand(OnOkCommandExecuted, CanOkCommandExecute);
        }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICommand OkCommand { get; }

        private bool CanOkCommandExecute(object p)
        {
            if (String.IsNullOrWhiteSpace(FirstName) || String.IsNullOrWhiteSpace(SecondName)
                || String.IsNullOrWhiteSpace(LastName) || String.IsNullOrWhiteSpace(Email))
            {
                return false;
            }
            return true;
        }
        private void OnOkCommandExecuted(object p)
        {
            try
            {
                _client.FirstName = FirstName;
                _client.SecondName = SecondName;
                _client.LastName = LastName;
                _client.PhoneNumber = PhoneNumber;
                _client.Email = Email;

                _addClient.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
    }
}