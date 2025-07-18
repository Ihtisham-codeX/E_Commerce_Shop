﻿using System;
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
using MySql.Data.MySqlClient;

namespace E_Commerce_Shop.UI.Customer
{
    public partial class Customerdashboard : Form
    {
        protected User user;
        public Customerdashboard()
        {
            if (!IsRuntime())
            {
                InitializeComponent();
                user = new LoginUser("Designer", "Preview");
                return;
            }
            throw new InvalidOperationException("MerchantDashboard must be created with a User object at runtime.");
        }
        public Customerdashboard(User user)
        {
            InitializeComponent();
            this.user = user ?? throw new ArgumentNullException(nameof(user));
        }
        private static bool IsRuntime()
        {
            return LicenseManager.UsageMode != LicenseUsageMode.Designtime &&
                   !(System.Reflection.Assembly.GetExecutingAssembly().Location.Contains("VisualStudio"));
        }
        private void Customerdashboard_Load(object sender, EventArgs e)
        {
            Color myColor = Color.FromArgb(192, 255, 192);
            button1.FlatAppearance.BorderColor = myColor;
            button2.FlatAppearance.BorderColor = myColor;
            int id = User.GetUserId(user.GetPassword(), user.GetUsername());
            int a = GetPurchaseCountByCustomer(id);
            textBox2.Text = a.ToString();
            textBox1.Text = user.GetUsername();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewProduct viewProduct = new ViewProduct(user);
            viewProduct.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewCart viewCart = new ViewCart(user);
            viewCart.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage homePage = new HomePage();
            homePage.Show();
        }
        public int GetPurchaseCountByCustomer(int customerId)
        {
            string query = $"SELECT COUNT(*) FROM orders WHERE CustomerID = {customerId}";

            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    try
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int count))
                        {
                            return count;
                        }
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error getting purchase count: " + ex.Message);
                        return 0;
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}