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
    public partial class ConfirmPurchase : Customerdashboard
    {
        protected User user;

        public ConfirmPurchase(User user):base(user) 
        {
            InitializeComponent();
            this.user = user;
        }
        private void ConfirmPurchase_Load(object sender, EventArgs e)
        {
            int customerId = User.GetUserId(user.GetPassword(), user.GetUsername());

            decimal total = GetCartTotal(customerId);
            label3.Text = "Total: $" + total.ToString("0.00");  // Add labelTotal to your Form
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
                {
                    int customerId = User.GetUserId(user.GetPassword(), user.GetUsername());

                    MySqlCommand cmd = new MySqlCommand("ConfirmPurchase", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@in_CustomerID", customerId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Purchase confirmed and cart cleared!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();  // Or redirect to another form
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error confirming purchase:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private decimal GetCartTotal(int customerId)
        {
            decimal total = 0;

            try
            {
                using (MySqlConnection conn = DatabaseHelper.Instance.getConnection())
                {
                    // Step 1: Get CartID for the user
                    int cartId = -1;
                    using (var cmd = new MySqlCommand("SELECT CartID FROM Carts WHERE CustomerID = @CustomerID", conn))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        object cartResult = cmd.ExecuteScalar();
                        if (cartResult != null && cartResult != DBNull.Value)
                            cartId = Convert.ToInt32(cartResult);
                        else
                            return 0; // No cart found
                    }

                    // Step 2: Calculate Total
                    using (var cmd = new MySqlCommand(@"
                SELECT SUM(ci.Quantity * p.Price)
                FROM CartItems ci
                JOIN Products p ON ci.ProductID = p.ProductID
                WHERE ci.CartID = @CartID", conn))
                    {
                        cmd.Parameters.AddWithValue("@CartID", cartId);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            total = Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calculating cart total:\n" + ex.Message);
            }

            return total;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
