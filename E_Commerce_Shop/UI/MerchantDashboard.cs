using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E_Commerce_Shop.UI.Merchant;
using E_Commerce_Shop.BL;
using E_Commerce_Shop.DL;

namespace E_Commerce_Shop.UI
{
    public partial class MerchantDashboard : Form
    {
        protected User user;

        // Design-time constructor
        public MerchantDashboard()
        {
            // This constructor is ONLY for design-time
            if (!IsRuntime())
            {
                InitializeComponent();
                user = new LoginUser("Designer", "Preview");
                return;
            }

            // At runtime, force using the proper constructor
            throw new InvalidOperationException("MerchantDashboard must be created with a User object at runtime.");
        }

        // Runtime constructor
        public MerchantDashboard(User user)
        {
            InitializeComponent();
            this.user = user ?? throw new ArgumentNullException(nameof(user));
        }

        // Helper method to detect runtime vs design-time
        private static bool IsRuntime()
        {
            return LicenseManager.UsageMode != LicenseUsageMode.Designtime &&
                   !(System.Reflection.Assembly.GetExecutingAssembly().Location.Contains("VisualStudio"));
        }


        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            BestSelling bestSelling = new BestSelling(user);
            bestSelling.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void MerchantDashboard_Load(object sender, EventArgs e)
        {
            int id = User.GetUserId(user.GetPassword(), user.GetUsername());
            // TOTAL PRODUCTS
            int totalProducts = GetTotalProductsByMerchant(id);
            label3.Text = totalProducts.ToString();
            // TOTAL ORDERS
            int totalOrders = GetTotalOrdersByMerchant(id);
            label8.Text = totalOrders.ToString();
            // TOTAL EARNINGS
            decimal totalEarnings = GetTotalEarningsByMerchant(id);
            label4.Text = totalEarnings.ToString("C2");
            // Shop Name
            string shopName = GetShopNameByMerchant(id);
            label6.Text = shopName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddProducts addProducts = new AddProducts(user);
            addProducts.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProductTypes productTypes = new ProductTypes(user);
            productTypes.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewProducts viewProducts = new ViewProducts(user);
            viewProducts.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateProducts updateProducts = new UpdateProducts(user);
            updateProducts.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewOrders viewOrders = new ViewOrders(user);
            viewOrders.Show();
        }
        private int GetTotalProductsByMerchant(int merchantId)
        {
            string query = $@"SELECT 
                             MerchantID,
                             SUM(Quantity) AS TotalQuantityInStock
                             FROM 
                             Products
                             WHERE 
                             MerchantID = '{merchantId}'
                             GROUP BY 
                             MerchantID;";

            using (var reader = DatabaseHelper.Instance.getData(query))
            {
                if (reader.Read())
                {
                    int totalProducts = Convert.ToInt32(reader["TotalQuantityInStock"]);

                    return totalProducts;
                }
            }
            return 00;
        }
        private int GetTotalOrdersByMerchant(int merchantId)
        {
            string query = $@"SELECT COUNT(DISTINCT oi.OrderID) AS TotalOrdersForMerchant
                            FROM OrderItems oi
                            JOIN Products p ON oi.ProductID = p.ProductID
                            WHERE p.MerchantID = '{merchantId}';";

            using (var reader = DatabaseHelper.Instance.getData(query))
            {
                if (reader.Read())
                {
                    int totalOrders = Convert.ToInt32(reader["TotalOrdersForMerchant"]);

                    return totalOrders;
                }
            }
            return 00;
        }
        private decimal GetTotalEarningsByMerchant(int merchantId)
        {
            string query = $@"SELECT 
                            p.MerchantID,
                            SUM(oi.Price * oi.Quantity) AS TotalEarnings
                            FROM 
                            OrderItems oi
                            JOIN 
                            Products p ON oi.ProductID = p.ProductID
                            WHERE 
                            p.MerchantID = '{merchantId}'
                            GROUP BY 
                            p.MerchantID;";

            using (var reader = DatabaseHelper.Instance.getData(query))
            {
                if (reader.Read())
                {
                    int totalEarnings = Convert.ToInt32(reader["TotalEarnings"]);

                    return totalEarnings;
                }
            }
            return 00;
        }
        private string GetShopNameByMerchant(int merchantId)
        {
            string query = $@"SELECT 
                            ShopName
                            FROM 
                            Shops
                            WHERE 
                            MerchantID = '{merchantId}'";

            using (var reader = DatabaseHelper.Instance.getData(query))
            {
                if (reader.Read())
                {
                    string name = reader["ShopName"].ToString();

                    return name;
                }
            }
            return "No Name";

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            HomePage homePage = new HomePage();
            homePage.Show();
        }
    }
}
