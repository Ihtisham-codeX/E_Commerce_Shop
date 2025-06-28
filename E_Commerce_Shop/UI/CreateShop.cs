using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E_Commerce_Shop.DL;
using MySql.Data.MySqlClient;
using E_Commerce_Shop.BL;

namespace E_Commerce_Shop.UI.Merchant
{
    public partial class CreateShop : Form
    {
        protected User user;
        public CreateShop(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void CreateShop_Load(object sender, EventArgs e)
        {
            LoadShopTypes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox4.Text;
            string shoptype = comboBox1.SelectedItem.ToString();
            int id = User.GetUserId(user.GetPassword(), user.GetUsername());
            string query = "INSERT INTO shops (ShopName , MerchantID, ShopType) VALUES (@name, @id, @type)";
            try
            {
                using (var conn = DatabaseHelper.Instance.getConnection())
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@type", shoptype);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Shop created successfully.");
                        this.Hide();
                        HomePage homePage = new HomePage();
                        homePage.Show();
                    }
                    else
                    {
                        MessageBox.Show("Failed to create shop. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadShopTypes()
        {
            string query = @"SELECT CategoryName FROM ecommercestore.shop_categories;";
            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Category = reader.GetString("CategoryName");
                            comboBox1.Items.Add(Category);
                        }
                    }
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
