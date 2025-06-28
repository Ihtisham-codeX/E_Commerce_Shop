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
    public partial class UpdateProducts : MerchantDashboard

    {
        protected User user;
        public UpdateProducts() : base()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
            {
                InitializeComponent();
                user = new LoginUser("Designer", "Preview"); // Dummy user for designer
                return;
            }

            throw new InvalidOperationException("UpdateProduct must be created with a User object at runtime.");
        }
        public UpdateProducts(User user) : base(user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void UpdateProducts_Load(object sender, EventArgs e)
        {
            LoadProductsForUpdate();
            dataGridView1.CellContentClick += dgvUpdateProducts_CellContentClick;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadProductsForUpdate()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.RowTemplate.Height = 170;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Columns
            dataGridView1.Columns.Add("ProductID", "ID"); // Hidden, for deletion
            dataGridView1.Columns["ProductID"].Visible = false;

            dataGridView1.Columns.Add("Name", "Name");

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "Image";
            imgCol.HeaderText = "Image";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.Columns.Add(imgCol);

            dataGridView1.Columns.Add("Price", "Price");
            dataGridView1.Columns.Add("Quantity", "Quantity");

            // styling delete button
            dataGridView1.CellFormatting += dgvUpdateProducts_CellFormatting;
            // Add Delete Button Column
            DataGridViewButtonColumn deleteCol = new DataGridViewButtonColumn();
            deleteCol.Name = "Delete";
            deleteCol.HeaderText = "Delete";
            deleteCol.Text = "Delete";
            deleteCol.UseColumnTextForButtonValue = true;
            //styling
            // Set style using DefaultCellStyle
            deleteCol.DefaultCellStyle.BackColor = Color.Red;
            deleteCol.DefaultCellStyle.ForeColor = Color.White;
            deleteCol.DefaultCellStyle.SelectionBackColor = Color.DarkRed;
            deleteCol.DefaultCellStyle.SelectionForeColor = Color.White;
            deleteCol.FlatStyle = FlatStyle.Flat; // Optional: removes 3D look
            //adding
            dataGridView1.Columns.Add(deleteCol);

                
            // Load Data
            string query = "SELECT ProductID, Name, ImagePath, Price, Quantity FROM Products";
            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int productId = Convert.ToInt32(reader["ProductID"]);
                            string name = reader["Name"].ToString();
                            string imagePath = reader["ImagePath"].ToString();
                            string price = reader["Price"].ToString();
                            string quantity = reader["Quantity"].ToString();

                            Image img = new Bitmap(100, 100); // fallback
                            try
                            {
                                if (File.Exists(imagePath))
                                {
                                    using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                                    {
                                        img = Image.FromStream(fs).Clone() as Image;
                                    }
                                }
                            }
                            catch { }

                            dataGridView1.Rows.Add(productId, name, img, price, quantity);
                        }
                    }
                }
            }
        }
        private void dgvUpdateProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if "Delete" column clicked
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete" && e.RowIndex >= 0)
            {
                int productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string deleteQuery = $"DELETE FROM Products WHERE ProductID = {productId}";

                    try
                    {
                        int rowsAffected = DatabaseHelper.Instance.Update(deleteQuery);
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product deleted.");
                            LoadProductsForUpdate(); // Refresh grid
                        }
                        else
                        {
                            MessageBox.Show("Product deletion failed.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting product: " + ex.Message);
                    }
                }
            }
        }

        // styling delete button
        private void dgvUpdateProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete" && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Change the style of the cell to make the button red
                cell.Style.BackColor = Color.Red;
                cell.Style.ForeColor = Color.White;
                cell.Style.SelectionBackColor = Color.DarkRed;
                cell.Style.SelectionForeColor = Color.White;
            }
        }

    }
}
