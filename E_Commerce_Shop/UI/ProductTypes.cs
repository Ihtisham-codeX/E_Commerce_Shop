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
using MySql.Data.MySqlClient;

namespace E_Commerce_Shop.UI
{
    public partial class ProductTypes : MerchantDashboard
    {
        protected User user;
        public ProductTypes(User user) : base(user)
        {
            InitializeComponent();
            this.user = user;
        }
        public ProductTypes() : base()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
            {
                InitializeComponent();
                user = new LoginUser("Designer", "Preview"); // Dummy user for designer
                return;
            }

            throw new InvalidOperationException("ProducType must be created with a User object at runtime.");
        }
        private void ProductTypes_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadMerchantProductTypes();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadMerchantProductTypes();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string type = textBox1.Text.Trim(); 
            string q = $@"INSERT INTO product_categories(CategoryName, Shop_Category_ID)
              VALUES ('{type}', {ShopCategoryID()})";

            try
            {
                int rowsAffected = DatabaseHelper.Instance.Update(q); 

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Product type added successfully.");
                    this.Hide();
                    ProductTypes productTypes = new ProductTypes(user);
                    productTypes.Show();
                }
                else
                {
                    MessageBox.Show("Failed to add product type. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product type: " + ex.Message);
            }


        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private int ShopCategoryID()
        {
            int id = User.GetUserId(user.GetPassword(), user.GetUsername());
            string query = $@"SELECT sc.ShopCategoryID
                            FROM Shops s
                            JOIN Shop_Categories sc ON s.ShopType = sc.CategoryName
                            WHERE s.MerchantID = '{id}';";
            using (var reader = DatabaseHelper.Instance.getData(query))
            {
                if (reader.Read())
                {
                    id = Convert.ToInt32(reader["ShopCategoryID"]);
                }
            }
            return id;
        }
        private void LoadMerchantProductTypes()
        {
            int merchantId = User.GetUserId(user.GetPassword(), user.GetUsername());
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Only one column: Product Type
            dataGridView1.Columns.Add("ProductType", "Product Type");

            string query = $@"
                            SELECT DISTINCT pc.CategoryName AS ProductType
                            FROM product_categories pc
                            JOIN shop_categories sc
                            ON sc.ShopCategoryID = pc.Shop_Category_ID
                            JOIN shops s
                            ON s.ShopType = sc.CategoryName
                            where s.MerchantID = '{merchantId}'";

            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string productType = reader["ProductType"].ToString();
                            dataGridView1.Rows.Add(productType);
                        }
                    }
                }
            }
        }


    }
}
