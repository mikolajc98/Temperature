using MySQLDb.Properties;
using MySQLDb.Service;
using MySQLDb.Views;
using System;



namespace MySQLDb
{
    class Program
    {
        static void Main(string[] args)
        {   
            Console.WriteLine("Logging into database. Please wait.");

            var initialized = DatabaseService
                .Initialize(
                SystemSettings.Default.DatabaseServerIP,
                SystemSettings.Default.DefaultDatabase,
                SystemSettings.Default.UserLogin,
                SystemSettings.Default.UserPassword);

            if (initialized)
                Console.WriteLine("Logged In");
            else
            {
                Console.WriteLine("Connection not initialized. Closing program.");
                return;
            }

            var mainView = new MainView();
            mainView.Open();
        }
    }
}
