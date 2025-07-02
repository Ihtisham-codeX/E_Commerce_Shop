using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Commerce_Shop.UI.Admin
{
    public partial class AdminDasshboard : Form
    {
        public AdminDasshboard()
        {
            InitializeComponent();
        }

        private void AdminDasshboard_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewCustomers viewCustomers = new ViewCustomers();
            viewCustomers.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewProducts viewProducts = new ViewProducts();
            viewProducts.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewMerchant viewMerchant = new ViewMerchant();
            viewMerchant.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage homePage = new HomePage();
            homePage.Show();
        }
    }
}
