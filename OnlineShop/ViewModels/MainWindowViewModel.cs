using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OnlineShop.Infrastructure.Commands;
using OnlineShop.ViewModels.Base;
using OnlineShop.Views.Windows;

namespace OnlineShop.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _msSqlInfo;
        private string _msAccessInfo;
        private object _mainWindowSelectedItem;

        private SqlConnection _sqlConnection;
        private SqlDataAdapter _dataAdapter;
        private DataTable _dataTable;
        private DataView _dataView;

        private OleDbConnection _oleDbConnection;
        private OleDbDataAdapter _oleDbDataAdapter;
        private DataTable _oleDbDataTable;
        private DataView _oleDbDataView;

        private DataGridCellInfo _cellInfo;
        public MainWindowViewModel()
        {
            PreparingMsSql();
            PreparingMsAccess();
            AddClientCommand = new RelayCommand(OnAddClientCommandExecuted);
            DeleteClientCommand = new RelayCommand(OnDeleteClientCommandExecuted);
            ClientsProductsCommand = new RelayCommand(OnClientsProductsCommandExecuted);
            ClientProductsCommand = new RelayCommand(OnClientProductsCommandExecuted);
        }

        public DataView DataView { get => _dataView; set => Set(ref _dataView, value); }
        public string MsSqlInfo { get => _msSqlInfo; set => Set(ref _msSqlInfo, value); }
        public string MsAccessInfo { get => _msAccessInfo; set => Set(ref _msAccessInfo, value); }
        public object MainWindowSelectedItem { get => _mainWindowSelectedItem; set => Set(ref _mainWindowSelectedItem, value); }
        public DataGridCellInfo CellInfo
        {
            get => _cellInfo;
            set
            {
                Set(ref _cellInfo, value);
                _dataAdapter.Update(_dataTable);
            }
        }
        public ICommand AddClientCommand { get; }
        public ICommand DeleteClientCommand { get; }
        public ICommand ClientsProductsCommand { get; }
        public ICommand ClientProductsCommand { get; }
        public void PreparingMsSql()
        {
            StartMsSql();

            _dataAdapter = new SqlDataAdapter();
            _dataTable = new DataTable();
            _dataView = new DataView();

            try
            {
                //Вывод всей информации
                var sql = @"SELECT * FROM Clients";
                _dataAdapter.SelectCommand = new SqlCommand(sql, _sqlConnection);

                //Добавить клиента
                sql = @"INSERT INTO Clients (FirstName,  SecondName,  LastName, PhoneNumber, Email) 
                                 VALUES (@FirstName, @SecondName, @LastName, @PhoneNumber, @Email); 
                     SET @ID = @@IDENTITY;";

                _dataAdapter.InsertCommand = new SqlCommand(sql, _sqlConnection);

                _dataAdapter.InsertCommand.Parameters.Add("@ID", SqlDbType.Int, 4, "ID").SourceVersion = DataRowVersion.Original;
                _dataAdapter.InsertCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 128, "FirstName");
                _dataAdapter.InsertCommand.Parameters.Add("@SecondName", SqlDbType.NVarChar, 128, "SecondName");
                _dataAdapter.InsertCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 128, "LastName");
                _dataAdapter.InsertCommand.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 128, "PhoneNumber");
                _dataAdapter.InsertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 128, "Email");

                //Обновить данные
                sql = @"UPDATE Clients SET 
                           FirstName = @FirstName,
                           SecondName = @SecondName, 
                           LastName = @LastName, 
                           PhoneNumber = @PhoneNumber, 
                           Email = @Email
                    WHERE ID = @ID";

                _dataAdapter.UpdateCommand = new SqlCommand(sql, _sqlConnection);
                _dataAdapter.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int, 4, "ID").SourceVersion = DataRowVersion.Original;
                _dataAdapter.UpdateCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 128, "FirstName");
                _dataAdapter.UpdateCommand.Parameters.Add("@SecondName", SqlDbType.NVarChar, 128, "SecondName");
                _dataAdapter.UpdateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 128, "LastName");
                _dataAdapter.UpdateCommand.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 128, "PhoneNumber");
                _dataAdapter.UpdateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 128, "Email");

                //Удалить клиента
                sql = "DELETE FROM Clients WHERE ID = @ID";

                _dataAdapter.DeleteCommand = new SqlCommand(sql, _sqlConnection);
                _dataAdapter.DeleteCommand.Parameters.Add("@ID", SqlDbType.Int, 4, "ID");

                _dataAdapter.Fill(_dataTable);
                _dataView = _dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void PreparingMsAccess()
        {
            StartMsAccess();
            _oleDbDataAdapter = new OleDbDataAdapter();
            _oleDbDataTable = new DataTable();
            _oleDbDataView = new DataView();

            var sql = @"SELECT * FROM Products";
            _oleDbDataAdapter.SelectCommand = new OleDbCommand(sql, _oleDbConnection);

            //sql = @"INSERT INTO Products (Email,  ProductCode,  ProductName) 
            //                     VALUES (@Email, @ProductCode, @ProductName); 
            //         SET @ID = @@IDENTITY;";

            //_oleDbDataAdapter.InsertCommand = new OleDbCommand(sql, _oleDbConnection);

            //_oleDbDataAdapter.InsertCommand.Parameters.AddWithValue("@Email", "Email");
            //_oleDbDataAdapter.InsertCommand.Parameters.AddWithValue("@ProductCode", "ProductCode");
            //_oleDbDataAdapter.InsertCommand.Parameters.AddWithValue("@ProductName", "ProductName");

            _oleDbDataAdapter.Fill(_oleDbDataTable);
            _oleDbDataView = _oleDbDataTable.DefaultView;
        }
        private void StartMsSql()
        {
            SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "MSSQLOnlineShop",
                UserID = "admin",
                Password = "admin",
                IntegratedSecurity = true,
                Pooling = false
            };

            _sqlConnection = new SqlConnection(strCon.ConnectionString);
            //sqlConnection.Open();

            MsSqlInfo = _sqlConnection.State.ToString();
            Debug.WriteLine(_sqlConnection.State);
        }
        private void StartMsAccess()
        {
            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            builder.Provider = @"Microsoft.ACE.OLEDB.12.0";
            builder.DataSource = @"C:\Users\AB\Source\Repos\OnlineShop\OnlineShop\bin\Debug\net7.0-windows\MSACCESSOnlineShop.accdb";

            _oleDbConnection = new OleDbConnection(builder.ConnectionString);
            //oleDbConnection.Open();

            MsAccessInfo = _oleDbConnection.State.ToString();
            Debug.WriteLine(_oleDbConnection.State);
        }
        private void OnAddClientCommandExecuted(object p)
        {
            DataRow row = _dataTable.NewRow();

            AddClient addClient = new AddClient();
            AddClientViewModel addClientViewModel = new AddClientViewModel(addClient, row);
            addClient.DataContext = addClientViewModel;
            addClient.ShowDialog();

            if (addClient.DialogResult.Value)
            {
                _dataTable.Rows.Add(row);
                _dataAdapter.Update(_dataTable);
            }
        }
        private void OnDeleteClientCommandExecuted(object p)
        {
            try
            {
                DataRowView row = (DataRowView)MainWindowSelectedItem;
                row.Row.Delete();
                _dataAdapter.Update(_dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }
        private void OnClientsProductsCommandExecuted(object p)
        {
            ClientsProducts сlientsProducts = new ClientsProducts();
            ClientsProductsViewModel сlientsProductsViewModel = new ClientsProductsViewModel(_oleDbDataView);
            сlientsProducts.DataContext = сlientsProductsViewModel;
            сlientsProducts.ShowDialog();
        }
        private void OnClientProductsCommandExecuted(object p)
        {
            try
            {
                DataRowView row = (DataRowView)MainWindowSelectedItem;
                var email = row.Row.ItemArray[5];

                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
                DataTable oleDbDataTable = new DataTable();
                DataView oleDbDataView = new DataView();

                var sql = @"SELECT * FROM Products WHERE Email = @Email";
                oleDbDataAdapter.SelectCommand = new OleDbCommand(sql, _oleDbConnection);
                oleDbDataAdapter.SelectCommand.Parameters.AddWithValue("@Email", email.ToString());

                oleDbDataAdapter.Fill(oleDbDataTable);
                oleDbDataView = oleDbDataTable.DefaultView;

                ClientProducts сlientProducts = new ClientProducts();
                ClientProductsViewModel сlientProductsViewModel = new ClientProductsViewModel(oleDbDataView);
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
