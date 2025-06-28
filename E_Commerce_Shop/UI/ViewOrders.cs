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

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
