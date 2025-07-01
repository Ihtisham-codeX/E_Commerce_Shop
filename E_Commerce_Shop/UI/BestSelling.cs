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

namespace E_Commerce_Shop.UI.Merchant
{
    public partial class BestSelling : MerchantDashboard
    {
        protected User user;
        public BestSelling(User user) : base(user)
        {
            InitializeComponent();
            this.user = user;
        }
        public BestSelling() : base()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
            {
                InitializeComponent();
                user = new LoginUser("Designer", "Preview"); // Dummy for designer
                return;
            }

            throw new InvalidOperationException("BestSelling must be created with a User object at runtime.");
        }




        private void BestSelling_Load(object sender, EventArgs e)
        {
            LoadBestSellingProducts();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadBestSellingProducts()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Add("MerchantName", "Merchant");
            dataGridView1.Columns.Add("ShopName", "Shop");
            dataGridView1.Columns.Add("ProductName", "Product");
            dataGridView1.Columns.Add("TotalSold", "Total Sold");

            string query = @"
       SELECT 
       u.Username AS MerchantName,
       s.ShopName,
       p.Name AS ProductName,
       SUM(oi.Quantity) AS TotalSold
       FROM OrderItems oi
      JOIN Products p ON oi.ProductID = p.ProductID
      JOIN Shops s ON p.MerchantID = s.MerchantID
      JOIN Users u ON p.MerchantID = u.UserID
      WHERE u.UserID = @merchantId
      GROUP BY p.ProductID, u.Username, s.ShopName, p.Name
      ORDER BY TotalSold DESC;";


            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    int merchantId = User.GetUserId(user.GetPassword(), user.GetUsername());
                    cmd.Parameters.AddWithValue("@merchantId", merchantId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string merchant = reader["MerchantName"].ToString();
                            string shop = reader["ShopName"].ToString();
                            string product = reader["ProductName"].ToString();
                            int sold = Convert.ToInt32(reader["TotalSold"]);


                            dataGridView1.Rows.Add(merchant, shop, product, sold);
                        }
                    }
                }
            }
        }

    }
}
