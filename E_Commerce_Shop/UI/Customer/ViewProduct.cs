using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E_Commerce_Shop.DL;
using MySql.Data.MySqlClient;
using E_Commerce_Shop.BL;

namespace E_Commerce_Shop.UI.Customer
{
    public partial class ViewProduct : Customerdashboard
    {
        protected User user;

        public ViewProduct(User user) : base(user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void ViewProduct_Load(object sender, EventArgs e)
        {
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadProducts();
        }

        private void LoadProducts()
        {
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();
            dataGridView4.AutoGenerateColumns = false;
            dataGridView4.RowTemplate.Height = 170;
            dataGridView4.AllowUserToAddRows = false;

            dataGridView4.Columns.Add("ProductID", "ID");
            dataGridView4.Columns["ProductID"].Visible = false;

            dataGridView4.Columns.Add("Name", "Product Name");

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "Image";
            imgCol.HeaderText = "Image";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView4.Columns.Add(imgCol);

            dataGridView4.Columns.Add("ShopName", "Shop");
            dataGridView4.Columns.Add("Price", "Price");
            dataGridView4.Columns.Add("Status", "Status");

            DataGridViewButtonColumn addCol = new DataGridViewButtonColumn();
            addCol.Name = "Add";
            addCol.HeaderText = "Add";
            addCol.Text = "Add to Cart";
            addCol.UseColumnTextForButtonValue = true;
            addCol.DefaultCellStyle.BackColor = Color.Green;
            addCol.DefaultCellStyle.ForeColor = Color.White;
            dataGridView4.Columns.Add(addCol);

            string query = @"
                SELECT p.ProductID, p.Name, p.ImagePath, p.Price, p.Quantity, s.ShopName
                FROM products p
                JOIN shops s ON p.MerchantID = s.MerchantID";

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
                            string shopName = reader["ShopName"].ToString();
                            decimal price = Convert.ToDecimal(reader["Price"]);
                            int quantity = Convert.ToInt32(reader["Quantity"]);
                            string imagePath = reader["ImagePath"].ToString();

                            string status = quantity > 0 ? "In Stock" : "Out of Stock";

                            Image img = new Bitmap(1, 1); // fallback blank image
                            try
                            {
                                using (var wc = new System.Net.WebClient())
                                using (var stream = wc.OpenRead(imagePath))
                                {
                                    img = Image.FromStream(stream);
                                }
                            }
                            catch
                            {
                                // Optional: Load fallback image from resources if needed
                                // img = Properties.Resources.DefaultImage;
                            }

                            dataGridView4.Rows.Add(productId, name, img, shopName, price.ToString("C"), status);
                        }
                    }
                }
            }
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.Columns[e.ColumnIndex].Name == "Add" && e.RowIndex >= 0)
            {
                int productId = Convert.ToInt32(dataGridView4.Rows[e.RowIndex].Cells["ProductID"].Value);
                string status = dataGridView4.Rows[e.RowIndex].Cells["Status"].Value.ToString();

                if (status == "Out of Stock")
                {
                    MessageBox.Show("This product is out of stock.");
                    return;
                }

                int id = User.GetUserId(user.GetPassword(), user.GetUsername());
                AddToCart(id, productId);
            }
        }

        private void AddToCart(int customerId, int productId)
        {
            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                // Check product stock first
                MySqlCommand checkStockCmd = new MySqlCommand("SELECT Quantity FROM products WHERE ProductID = @productId", conn);
                checkStockCmd.Parameters.AddWithValue("@productId", productId);
                object stockResult = checkStockCmd.ExecuteScalar();

                if (stockResult == null || Convert.ToInt32(stockResult) <= 0)
                {
                    MessageBox.Show("This product is out of stock.");
                    return;
                }

                // Ensure cart exists
                MySqlCommand getCartCmd = new MySqlCommand("SELECT CartID FROM carts WHERE CustomerID = @id", conn);
                getCartCmd.Parameters.AddWithValue("@id", customerId);
                object result = getCartCmd.ExecuteScalar();

                int cartId;
                if (result == null)
                {
                    MySqlCommand createCartCmd = new MySqlCommand("INSERT INTO carts(CustomerID) VALUES (@id)", conn);
                    createCartCmd.Parameters.AddWithValue("@id", customerId);
                    createCartCmd.ExecuteNonQuery();
                    cartId = (int)createCartCmd.LastInsertedId;
                }
                else
                {
                    cartId = Convert.ToInt32(result);
                }

                // Add to cart or update quantity
                MySqlCommand addItem = new MySqlCommand(@"
            INSERT INTO cartitems (CartID, ProductID, Quantity)
            VALUES (@cartId, @productId, 1)
            ON DUPLICATE KEY UPDATE Quantity = Quantity + 1", conn);
                addItem.Parameters.AddWithValue("@cartId", cartId);
                addItem.Parameters.AddWithValue("@productId", productId);
                addItem.ExecuteNonQuery();

                // Reduce stock in Products table
                MySqlCommand reduceStock = new MySqlCommand("UPDATE products SET Quantity = Quantity - 1 WHERE ProductID = @productId", conn);
                reduceStock.Parameters.AddWithValue("@productId", productId);
                reduceStock.ExecuteNonQuery();

                // Refresh product view
                LoadProducts();

                MessageBox.Show("Product added to cart!");
            }
        }

    }
}
