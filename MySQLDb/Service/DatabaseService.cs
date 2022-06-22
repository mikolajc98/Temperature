using Dapper;
using MySql.Data.MySqlClient;
using System;

namespace MySQLDb.Service
{
    public class DatabaseService
    {
        private static DatabaseService _instance = null;

        private MySqlConnection _connection;

        public MySqlConnection Connection => _connection;

        public static bool Initialize(string serverName, string databaseName, string userName, string password)
        {
            _instance = new DatabaseService();

            var connectionString = $"server='{serverName}';database='{databaseName}';uid='{userName}';pwd='{password}'";
            Instance._connection = new MySqlConnection(connectionString);
            return Instance.CheckConnection();
        }

        public static DatabaseService Instance
        {
            get
            {
                if (_instance is null)
                    throw new NullReferenceException($"{nameof(DatabaseService)} not initialized. Use {nameof(Initialize)} method before first use!");
                return _instance;
            }
        }

        public bool CheckConnection()
        {
            try
            {
                var result = Instance._connection.QueryFirst<int>("SELECT 1");
                return result == 1;  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
