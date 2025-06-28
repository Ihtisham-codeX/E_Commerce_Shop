using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Shop.DL;

namespace E_Commerce_Shop.BL
{
    internal class UserSignUp : User
    {
        public UserSignUp(string username, string email, string password, string role) : base(username, email, password, role)
        {
        }
        public override bool Register() => UserDL.AddUser(this);
        public override string GetUsername() => username;
        public override void SetUsername(string username) => this.username = username;
        public override string GetPassword() => password;
        public override void SetPassword(string password) => this.password = password;
        public override string GetEmail() => email;
        public override void SetEmail(string email) => this.email = email;
        public override string GetUserRole() => role;
        public override void SetUserRole(string role) => this.role = role;
    }
}
