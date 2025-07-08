using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace E_Commerce_Shop.DL
{

    public class DatabaseHelper
    {
        //private String serverName = "127.0.0.1";
        //private String port = "3306";
        //private String databaseName = "ecommercestore";
        //private String databaseUser = "root";
        //private String databasePassword = "root";

        private String serverName = "sql12.freesqldatabase.com";
        private String port = "3306";
        private String databaseName = "sql12788888";
        private String databaseUser = "sql12788888";
        private String databasePassword = "Z2BEjZ4Nxw";



        public DatabaseHelper() { }

        private static DatabaseHelper _instance;
        public static DatabaseHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DatabaseHelper();
                return _instance;
            }
        }
        public MySqlConnection getConnection()
        {
            string connectionString = $"server={serverName};port={port};user={databaseUser};database ={databaseName}; password ={databasePassword}; SslMode = None; ";
            var connection = new
            MySqlConnection(connectionString);
            connection.Open();



            return connection;
        }

        //public MySqlDataReader getData(string query)
        //{
        //    using (var connection = getConnection())
        //    {
        //        using (var command = new MySqlCommand(query, getConnection()))
        //        {
        //            return command.ExecuteReader();
        //        }
        //    }

        //}
        public MySqlDataReader getData(string query)
        {
            var connection = getConnection(); // ✅ open once
            var command = new MySqlCommand(query, connection);
            return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection); // ✅ auto-close
        }


        //public int Update(string query)
        //{
        //    using (var connection = getConnection())
        //    {
        //        using (var command = new MySqlCommand(query, getConnection()))
        //        {
        //            return command.ExecuteNonQuery();
        //        }
        //    }

        //}
        public int Update(string query)
        {
            using (var connection = getConnection())
            {
                using (var command = new MySqlCommand(query, connection)) // ✅ use the existing connection
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }
    }
}
