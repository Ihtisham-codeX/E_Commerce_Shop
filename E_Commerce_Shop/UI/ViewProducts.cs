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
            string query = @$"SELECT Name, ImagePath, Price, Quantity FROM Products Where MerchantID = '{id}'";

            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        //Clear existing rows and columns
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();

                        dataGridView1.AutoGenerateColumns = false; // Do not auto-generate columns
                        dataGridView1.RowTemplate.Height = 170; // Set row height for images
                        dataGridView1.AllowUserToAddRows = false; // Disable adding new rows by user
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Set columns to fill the grid width

                        // Add the coulumns
                        dataGridView1.Columns.Add("Name", "Name");

                        DataGridViewImageColumn imgCol = new DataGridViewImageColumn // Image column
                        {
                            Name = "Image",
                            HeaderText = "Image",
                            ImageLayout = DataGridViewImageCellLayout.Stretch
                        };
                        dataGridView1.Columns.Add(imgCol);

                        dataGridView1.Columns.Add("Price", "Price");
                        dataGridView1.Columns.Add("Quantity", "Quantity");

                        // Load rows
                        while (reader.Read())
                        {
                            string name = reader["Name"].ToString();
                            string imagePath = reader["ImagePath"].ToString();
                            string price = reader["Price"].ToString();
                            string quantity = reader["Quantity"].ToString();

                            Image img = new Bitmap(100, 100); // Create a blank image of 100x100 pixels
                            try
                            {
                                if (File.Exists(imagePath))
                                {
                                    // Load the image from the file stream and clone it to avoid locking the file
                                    // This allows the file to be deleted or modified later if needed
                                    using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read)) 
                                    {
                                        img = Image.FromStream(fs).Clone() as Image;
                                    }
                                }
                            }
                            catch
                            {
                                // fallback image already assigned
                            }

                            dataGridView1.Rows.Add(name, img, price, quantity);
                        }
                    }
                }
            }
        }

    }
}
