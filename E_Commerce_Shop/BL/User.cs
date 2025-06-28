using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Shop.DL;

namespace E_Commerce_Shop.BL
{
    public abstract class User
    {
        protected string username;
        protected string password;
        protected string email;
        protected string role;

        //default constructor
        public User() { }

        //signup constructor
        public User(string username, string email, string password, string role)
        {
            this.username = username;
            this.password = password;
            this.email = email;
            this.role = role;
        }
        //login constructor
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        // making the login functions
        public abstract bool Register();
        public static bool RegisterUser(User user)
        {
            return user.Register();
        }
        // making a side aid function to get user id from password and username
        public static int GetUserId(string password, string username)
        {
            int userId = 0;
            string query = $"SELECT UserID FROM users WHERE Password = '{password}' AND Username = '{username}'";
            using (var reader = DatabaseHelper.Instance.getData(query))
            {
                if (reader.Read())
                {
                    userId = Convert.ToInt32(reader["UserID"]);
                }
                return userId;
            }
        }
        // making getters and setters for each as they are protected
        public abstract string GetUsername();
        public abstract void SetUsername(string username);
        public abstract string GetPassword();
        public abstract void SetPassword(string password);
        public abstract string GetEmail();
        public abstract void SetEmail(string email);
        public abstract string GetUserRole();
        public abstract void SetUserRole(string userRole);
    }
}
