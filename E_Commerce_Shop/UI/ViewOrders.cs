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
    public partial class ViewOrders : MerchantDashboard
    {
        protected User user;
        public ViewOrders(User user) : base(user)
        {
            InitializeComponent();
            this.user = user;
        }
        public ViewOrders() : base()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
            {
                InitializeComponent();
                user = new LoginUser("Designer", "Preview"); // Dummy user for designer
                return;
            }

            throw new InvalidOperationException("ViewOrder must be created with a User object at runtime.");
        }

        private void ViewOrders_Load(object sender, EventArgs e)
        {
            int merchantId = User.GetUserId(user.GetPassword(), user.GetUsername());
            LoadMerchantSales(merchantId);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadMerchantSales(int merchantId)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns.Add("ProductName", "Product");
            dataGridView1.Columns.Add("Quantity", "Quantity");
            dataGridView1.Columns.Add("UnitPrice", "Unit Price");
            dataGridView1.Columns.Add("TotalPrice", "Total Price");
            dataGridView1.Columns.Add("CustomerName", "Customer");
            dataGridView1.Columns.Add("CustomerEmail", "Email");
            dataGridView1.Columns.Add("OrderDate", "Date");

            string query = @"
            SELECT 
            p.Name AS ProductName,
            oi.Quantity,
            oi.Price AS UnitPrice,
            (oi.Quantity * oi.Price) AS TotalPrice,
            cu.Username AS CustomerName,
            cu.Email AS CustomerEmail,
            o.OrderDate
            FROM OrderItems oi
            JOIN Products p ON oi.ProductID = p.ProductID
            JOIN Orders o ON oi.OrderID = o.OrderID
            JOIN Users cu ON o.CustomerID = cu.UserID
            WHERE p.MerchantID = @merchantId
            ORDER BY o.OrderDate DESC;";
                                        

            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@merchantId", merchantId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string product = reader["ProductName"].ToString();
                            int quantity = Convert.ToInt32(reader["Quantity"]);
                            decimal unitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                            decimal totalPrice = Convert.ToDecimal(reader["TotalPrice"]);
                            string customerName = reader["CustomerName"].ToString();
                            string customerEmail = reader["CustomerEmail"].ToString();
                            string date = Convert.ToDateTime(reader["OrderDate"]).ToString("g");

                            dataGridView1.Rows.Add(product, quantity, unitPrice.ToString("C"), totalPrice.ToString("C"), customerName, customerEmail, date);
                        }
                    }
                }
            }
        }

    }
}
