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
    public partial class ViewCustomers : AdminDasshboard
    {
        public ViewCustomers()
        {
            InitializeComponent();

        }

        private void ViewCustomers_Load(object sender, EventArgs e)
        {
            LoadCustomerSalesData();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void LoadCustomerSalesData()
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2.Columns.Add("CustomerName", "Customer Name");
            dataGridView2.Columns.Add("Email", "Email");
            dataGridView2.Columns.Add("SalesMade", "Number of Sales");
            dataGridView2.Columns.Add("TotalSpent", "Total Spent");

            string query = @"
       SELECT 
    u.Username AS CustomerName,
    u.Email,
    COUNT(DISTINCT o.OrderID) AS SalesMade,
    COALESCE(SUM(oi.Price * oi.Quantity), 0) AS TotalSpent
FROM Users u
LEFT JOIN Orders o ON u.UserID = o.CustomerID
LEFT JOIN OrderItems oi ON o.OrderID = oi.OrderID
WHERE u.Role = 'Customer'
GROUP BY u.UserID, u.Username, u.Email
ORDER BY TotalSpent DESC;
";

            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader["CustomerName"].ToString();
                            string email = reader["Email"].ToString();
                            int sales = Convert.ToInt32(reader["SalesMade"]);
                            decimal total = Convert.ToDecimal(reader["TotalSpent"]);

                            dataGridView2.Rows.Add(name, email, sales, total.ToString("C"));
                        }
                    }
                }
            }
        }

    }
}
