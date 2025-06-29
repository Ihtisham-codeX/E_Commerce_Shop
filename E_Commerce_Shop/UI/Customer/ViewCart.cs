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

namespace E_Commerce_Shop.UI.Customer
{
    public partial class ViewCart : Customerdashboard
    {
        protected User user;
        public ViewCart(User user) : base(user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void ViewCart_Load(object sender, EventArgs e)
        {
            LoadCartItems();
        }
        private void LoadCartItems()
        {
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Clear();
            dataGridView4.AutoGenerateColumns = false;
            dataGridView4.AllowUserToAddRows = false;
            dataGridView4.RowTemplate.Height = 170;
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView4.Columns.Add("ProductID", "ID");
            dataGridView4.Columns["ProductID"].Visible = false;

            dataGridView4.Columns.Add("ProductName", "Product");

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol.Name = "Image";
            imgCol.HeaderText = "Image";
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView4.Columns.Add(imgCol);

            dataGridView4.Columns.Add("ShopName", "Shop");
            dataGridView4.Columns.Add("Price", "Unit Price");
            dataGridView4.Columns.Add("Quantity", "Quantity");
            dataGridView4.Columns.Add("Total", "Total");

            DataGridViewButtonColumn removeCol = new DataGridViewButtonColumn();
            removeCol.Name = "Remove";
            removeCol.HeaderText = "Remove";
            removeCol.Text = "Remove";
            removeCol.UseColumnTextForButtonValue = true;
            removeCol.DefaultCellStyle.BackColor = Color.Red;
            removeCol.DefaultCellStyle.ForeColor = Color.White;
            dataGridView4.Columns.Add(removeCol);

            string query = @"
        SELECT p.ProductID, p.Name AS ProductName, p.ImagePath, s.ShopName, p.Price, SUM(ci.Quantity) AS Quantity
        FROM CartItems ci
        JOIN Carts c ON ci.CartID = c.CartID
        JOIN Products p ON ci.ProductID = p.ProductID
        JOIN Shops s ON p.MerchantID = s.MerchantID
        WHERE c.CustomerID = @id
        GROUP BY p.ProductID, p.Name, p.ImagePath, s.ShopName, p.Price";

            using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    int id = User.GetUserId(user.GetPassword(), user.GetUsername());
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int productId = Convert.ToInt32(reader["ProductID"]);
                            string productName = reader["ProductName"].ToString();
                            string imagePath = reader["ImagePath"].ToString();
                            string shopName = reader["ShopName"].ToString();
                            decimal price = Convert.ToDecimal(reader["Price"]);
                            int quantity = Convert.ToInt32(reader["Quantity"]);
                            decimal total = price * quantity;

                            Image img = new Bitmap(100, 100);
                            try
                            {
                                if (File.Exists(imagePath))
                                {
                                    using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                                    {
                                        img = Image.FromStream(fs);
                                    }
                                }
                            }
                            catch { }

                            dataGridView4.Rows.Add(productId, productName, img, shopName, price.ToString("C"), quantity, total.ToString("C"));
                        }
                    }
                }
            }

            dataGridView4.CellContentClick -= dataGridView4_CellContentClick;
            dataGridView4.CellContentClick += dataGridView4_CellContentClick;
        }



        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.Columns[e.ColumnIndex].Name == "Remove" && e.RowIndex >= 0)
            {
                int productId = Convert.ToInt32(dataGridView4.Rows[e.RowIndex].Cells["ProductID"].Value);

                DialogResult confirm = MessageBox.Show("Are you sure you want to remove this item?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm != DialogResult.Yes)
                    return;

                using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
                {
                    int customerId = User.GetUserId(user.GetPassword(), user.GetUsername());

                    // Get Cart ID
                    string cartQuery = "SELECT CartID FROM Carts WHERE CustomerID = @custId";
                    MySqlCommand cartCmd = new MySqlCommand(cartQuery, conn);
                    cartCmd.Parameters.AddWithValue("@custId", customerId);
                    object result = cartCmd.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("Cart not found.");
                        return;
                    }
                    int cartId = Convert.ToInt32(result);
                    string query = @"
                UPDATE CartItems 
                SET Quantity = Quantity - 1 
                WHERE CartID = @cartId AND ProductID = @productId;

                DELETE FROM CartItems 
                WHERE CartID = @cartId AND ProductID = @productId AND Quantity <= 0;
            ";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@cartId", cartId);
                    cmd.Parameters.AddWithValue("@productId", productId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Item removed.");
                    LoadCartItems(); // Refresh
                }
            }
        }


    }
}
