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

namespace E_Commerce_Shop.UI.Admin
{
    public partial class ViewProducts : AdminDasshboard
    {
        public ViewProducts()
        {
            InitializeComponent();
        }

        private void ViewProducts_Load(object sender, EventArgs e)
        {
            LoadProductSalesData();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadProductSalesData()
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Define columns
            dataGridView2.Columns.Add("ProductName", "Product Name");
            dataGridView2.Columns.Add("ShopName", "Shop Name");
            dataGridView2.Columns.Add("TimesSold", "Sold Times");

            string query = @"
            SELECT 
            p.Name AS ProductName,
            s.ShopName,
            SUM(oi.Quantity) AS TimesSold
            FROM orderitems oi
            JOIN products p ON oi.ProductID = p.ProductID
            JOIN shops s ON p.MerchantID = s.MerchantID
            GROUP BY p.ProductID, p.Name, s.ShopName
            ORDER BY TimesSold DESC;";

            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string productName = reader["ProductName"].ToString();
                            string shopName = reader["ShopName"].ToString();
                            int timesSold = Convert.ToInt32(reader["TimesSold"]);

                            dataGridView2.Rows.Add(productName, shopName, timesSold);
                        }
                    }
                }
            }
        }

    }
}
