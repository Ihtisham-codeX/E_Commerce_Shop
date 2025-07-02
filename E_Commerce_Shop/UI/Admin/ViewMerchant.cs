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
    public partial class ViewMerchant : AdminDasshboard
    {
        public ViewMerchant()
        {
            InitializeComponent();
        }

        private void ViewMerchant_Load(object sender, EventArgs e)
        {
            LoadMerchantSalesData();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadMerchantSalesData()
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Define columns
            dataGridView2.Columns.Add("MerchantName", "Merchant Name");
            dataGridView2.Columns.Add("Email", "Email");
            dataGridView2.Columns.Add("ShopName", "Shop Name");
            dataGridView2.Columns.Add("ShopType", "Shop Type");
            dataGridView2.Columns.Add("OrderCount", "Number of Orders");
            dataGridView2.Columns.Add("TotalEarnings", "Total Earnings");

            string query = @"
       SELECT 
    u.Username AS MerchantName,
    u.Email,
    s.ShopName,
    s.ShopType,
    COUNT(DISTINCT o.OrderID) AS OrderCount,
    COALESCE(SUM(oi.Price * oi.Quantity), 0) AS TotalEarnings
FROM Users u
JOIN Shops s ON u.UserID = s.MerchantID
LEFT JOIN Products p ON s.MerchantID = p.MerchantID
LEFT JOIN OrderItems oi ON p.ProductID = oi.ProductID
LEFT JOIN Orders o ON oi.OrderID = o.OrderID
WHERE u.Role = 'Merchant'
GROUP BY u.UserID, u.Username, u.Email, s.ShopName, s.ShopType
ORDER BY TotalEarnings DESC;
";

            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string merchantName = reader["MerchantName"].ToString();
                            string email = reader["Email"].ToString();
                            string shopName = reader["ShopName"].ToString();
                            string shopType = reader["ShopType"].ToString();
                            int orderCount = Convert.ToInt32(reader["OrderCount"]);
                            decimal earnings = Convert.ToDecimal(reader["TotalEarnings"]);

                            dataGridView2.Rows.Add(
                                merchantName,
                                email,
                                shopName,
                                shopType,
                                orderCount,
                                earnings.ToString("C")
                            );
                        }
                    }
                }
            }
        }

    }
}
