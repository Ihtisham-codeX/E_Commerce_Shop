using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using E_Commerce_Shop.BL;

namespace E_Commerce_Shop.DL
{
    internal class UserDL
    {
        public static bool AddUser(User user)
        {
            string query = "INSERT INTO users (Username , Email, Password , Role) VALUES (@Username, @Email, @Password, @UserRole)";
            try
            {
                using (var conn = DatabaseHelper.Instance.getConnection())
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", user.GetUsername());
                    cmd.Parameters.AddWithValue("@Email", user.GetEmail());
                    cmd.Parameters.AddWithValue("@Password", user.GetPassword());
                    cmd.Parameters.AddWithValue("@UserRole", user.GetUserRole());
                   
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static User ObjectOfLoggedInUser(User u)
        {
            string password = u.GetPassword();
            string username = u.GetUsername();

            string query = $@"SELECT  Username , Password , Role , Email
                      FROM users
                      WHERE Password = '{password}' AND Username = '{username}'";

            try
            {
                using (var reader = DatabaseHelper.Instance.getData(query))
                {
                    if (reader.Read())
                    {
                        string Username = reader["Username"].ToString();
                        string Password = reader["Password"].ToString();
                        string Role = reader["Role"].ToString();
                        string Email = reader["Email"].ToString();
                        return new UserSignUp(Username, Email, Password, Role);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AuthenticateUser: {ex.Message}");
            }

            return null;
        }
    }
}
