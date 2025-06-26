using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Commerce_Shop.UI
{
    public partial class MerchantDashboard : Form
    {
        public MerchantDashboard()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void MerchantDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddProducts addProducts = new AddProducts();
            addProducts.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProductTypes productTypes = new ProductTypes();
            productTypes.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewProducts viewProducts = new ViewProducts();
            viewProducts.Show();
        }
    }
}
