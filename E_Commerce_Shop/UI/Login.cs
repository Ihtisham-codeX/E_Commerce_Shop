using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E_Commerce_Shop.BL;
using E_Commerce_Shop.DL;
using E_Commerce_Shop.UI.Customer;


namespace E_Commerce_Shop.UI
{
    public partial class Login : HomePage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            User temporaryuser = new LoginUser(username, password , " "," ");
            User loggedInUser = UserDL.ObjectOfLoggedInUser(temporaryuser);
            if (loggedInUser != null)
            {
                if (loggedInUser.GetUserRole() == "Merchant")
                {
                    this.Hide();
                    MerchantDashboard merchantDashboard = new MerchantDashboard(loggedInUser);
                    merchantDashboard.Show();
                }
                else if (loggedInUser.GetUserRole() == "Customer")
                {
                    this.Hide();
                    Customerdashboard customerDashboard = new Customerdashboard(loggedInUser);
                    customerDashboard.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
