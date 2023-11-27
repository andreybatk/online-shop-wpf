using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using OnlineShop.Infrastructure.Commands;
using OnlineShop.ViewModels.Base;
using OnlineShop.Views.Windows;

namespace OnlineShop.ViewModels
{
    internal class AddClientViewModel : ViewModel
    {
        private DataRow _row;
        private AddClient _addClient;
        public AddClientViewModel(AddClient addClient, DataRow row)
        {
            this._addClient = addClient;
            this._row = row;
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
            if(String.IsNullOrWhiteSpace(FirstName) || String.IsNullOrWhiteSpace(SecondName)
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
                _row["FirstName"] = FirstName;
                _row["SecondName"] = SecondName;
                _row["LastName"] = LastName;
                _row["PhoneNumber"] = PhoneNumber;
                _row["Email"] = Email;

                _addClient.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            
        }
    }
}
