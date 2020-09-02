using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AutoLevelDbHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            DbProviderFactories.RegisterFactory("provider", SqlClientFactory.Instance);
            var providerFactory = DbProviderFactories.GetFactory("provider");

            DataSet dataSet = new DataSet("OnlineShop");
            var dataAdapter = providerFactory.CreateDataAdapter();

            var connection = providerFactory.CreateConnection();
            connection.ConnectionString = "Server = localhost; Database = OnlineShopDB; Trusted_Connection = True";
            
            var selectCommand = providerFactory.CreateCommand();
            selectCommand.CommandText = "select * from User";
            selectCommand.Connection = connection;

            dataAdapter.SelectCommand = selectCommand;

            dataAdapter.Fill(dataSet);

            var usersTable = dataSet.Tables[0];

            AddRowToUserTable(usersTable, "Sanya", "XXX_Sanya_XXX", "sapnu_puas");
            AddRowToUserTable(usersTable, "admin", "admin", "admin");
            AddRowToUserTable(usersTable, "Dimas", "NewUser", "parol");
            DeleteRowFromUserTable(usersTable, 2);

            var productTable = dataSet.Tables[1];

            dataAdapter.Update(dataSet);
            
            dataSet.AcceptChanges();
        }

        static void AddRowToUserTable(DataTable table, string name, string login, string password)
        {
            table.Rows.Add(Guid.NewGuid(), name, login, password);
        }

        static void DeleteRowFromUserTable(DataTable table, int index)
        {
            var row = table.Rows[index];
            row.Delete();
        }

        static void EditRowFromUserTable(DataTable table, int rowIndex, string columnName, string value)
        {
            var row = table.Rows[rowIndex];
            row.BeginEdit();

            row[columnName] = value;

            row.EndEdit();
        }

        static void AddRowToProductTable(DataTable table, string name, string title, int price)
        {
            table.Rows.Add(Guid.NewGuid(), name, title, price);
        }

        static void DeleteRowFromProductTable(DataTable table, int index)
        {
            var row = table.Rows[index];
            row.Delete();
        }

        static void EditRowFromProductTable(DataTable table, int rowIndex, string columnName, string value)
        {
            var row = table.Rows[rowIndex];
            row.BeginEdit();

            row[columnName] = value;

            row.EndEdit();
        }

        static void EditRowFromProductTable(DataTable table, int rowIndex, string columnName, int value)
        {
            var row = table.Rows[rowIndex];
            row.BeginEdit();

            row[columnName] = value;

            row.EndEdit();
        }
    }
}
