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
using E_Commerce_Shop.UI.Merchant;

namespace E_Commerce_Shop.UI
{
    public partial class Signup : HomePage
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string username = textBox1.Text;
                string password = textBox3.Text;
                string email = textBox2.Text;
                string role = comboBox1.SelectedItem.ToString();
                UserSignUp newUser = new UserSignUp(username, email, password , role);
                if (User.RegisterUser(newUser))
                {
                    MessageBox.Show("User registered successfully.");
                    this.Hide();
                    if(role == "Merchant")
                    {
                        User temporaryuser = new LoginUser(username, password);
                        User loggedInUser = UserDL.ObjectOfLoggedInUser(temporaryuser);
                        User u = loggedInUser as User;
                        CreateShop createShop = new CreateShop(u);
                        createShop.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

