using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E_Commerce_Shop.BL;
using E_Commerce_Shop.DL;
using MySql.Data.MySqlClient;

namespace E_Commerce_Shop.UI
{
    public partial class AddProducts : MerchantDashboard
    {
        protected User user; // This is a non-nullable field
        public AddProducts(User user) : base(user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void AddProducts_Load(object sender, EventArgs e)
        {
            LoadProductTypes();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            button7.PerformClick();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an Image";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBox4.Text = ofd.FileName;
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            int a = int.Parse(textBox3.Text) + 1;
            textBox3.Text = a.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox3.Text) > 0)
            {
                int a = int.Parse(textBox3.Text) - 1;
                textBox3.Text = a.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            int price = int.Parse(textBox5.Text);
            string description = textBox2.Text;
            string ProductType = comboBox1.SelectedItem.ToString();
            int quantity = int.Parse(textBox3.Text);
            string imagePath = textBox4.Text;

            int MerchantID = User.GetUserId(user.GetPassword(), user.GetUsername());
            int CategoryId = CategoryID(ProductType);

            string query = @"INSERT INTO products (Name, Price, Quantity, ImagePath, CategoryID, MerchantID) 
                            VALUES (@name, @price, @quantity, @imagePath, @categoryId, @merchantId)";
            try
            {
                using (var conn = DatabaseHelper.Instance.getConnection())
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@imagePath", imagePath);
                    cmd.Parameters.AddWithValue("@categoryId", CategoryId);
                    cmd.Parameters.AddWithValue("@merchantId", MerchantID);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product added successfully.");
                        this.Hide();
                        ViewProducts viewProducts = new ViewProducts(user);
                        viewProducts.Show();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add product. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product: " + ex.Message);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadProductTypes()
        {
            int id = User.GetUserId(user.GetPassword(), user.GetUsername());
            string query = $@"SELECT pc.CategoryName
                            FROM shops s
                            JOIN shop_categories sc ON s.ShopType = sc.CategoryName
                            JOIN product_categories pc ON pc.Shop_Category_ID = sc.ShopCategoryID
                            WHERE s.MerchantID = '{id}' AND s.ShopID = pc.ShopID;";

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
        private int CategoryID(string categoryName)
        {
            int id = 0;
            string query = $"SELECT Product_Category_ID FROM product_categories WHERE CategoryName = '{categoryName}'";
            using (var reader = DatabaseHelper.Instance.getData(query))
            {
                if (reader.Read())
                {
                    id = Convert.ToInt32(reader["Product_Category_ID"]);
                }
            }
            return id;
        }
    }
}