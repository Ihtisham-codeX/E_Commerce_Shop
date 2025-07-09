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
using System.Net; // Required for WebClient


namespace E_Commerce_Shop.UI
{
    public partial class ViewProducts : MerchantDashboard
    {
        protected User user;
        public ViewProducts(User user) : base(user)
        {
            InitializeComponent();
            this.user = user;
        }
        public ViewProducts() : base()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
            {
                InitializeComponent();
                user = new LoginUser("Designer", "Preview"); // Dummy user for designer
                return;
            }

            throw new InvalidOperationException("ViewProduct must be created with a User object at runtime.");
        }

        private void ViewProducts_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            LoadProducts();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadProducts()
        {
            int id = User.GetUserId(user.GetPassword(), user.GetUsername());
            string query = @$"SELECT Name, ImagePath, Price, Quantity FROM products WHERE MerchantID = '{id}'";

            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();

                        dataGridView1.AutoGenerateColumns = false;
                        dataGridView1.RowTemplate.Height = 170;
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Add columns
                        dataGridView1.Columns.Add("Name", "Name");

                        var imgCol = new DataGridViewImageColumn
                        {
                            Name = "Image",
                            HeaderText = "Image",
                            ImageLayout = DataGridViewImageCellLayout.Zoom
                        };
                        dataGridView1.Columns.Add(imgCol);

                        dataGridView1.Columns.Add("Price", "Price");
                        dataGridView1.Columns.Add("Quantity", "Quantity");

                        while (reader.Read())
                        {
                            string name = reader["Name"].ToString();
                            string imageUrl = reader["ImagePath"].ToString();
                            string price = reader["Price"].ToString();
                            string quantity = reader["Quantity"].ToString();

                            Image img = new Bitmap(1, 1); // fallback image
                            try
                            {
                                using (var wc = new System.Net.WebClient())
                                using (var stream = wc.OpenRead(imageUrl))
                                {
                                    img = Image.FromStream(stream);
                                }
                            }
                            catch
                            {
                                // If image fails to load, blank image remains
                            }

                            dataGridView1.Rows.Add(name, img, price, quantity);
                        }
                    }
                }
            }
        }

    }
}
