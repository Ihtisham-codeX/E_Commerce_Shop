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
    public partial class BestSelling : MerchantDashboard
    {
        protected User user;
        public BestSelling(User user) : base(user)
        {
            InitializeComponent();
            this.user = user;
        }
        public BestSelling() : base()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
            {
                InitializeComponent();
                user = new LoginUser("Designer", "Preview"); // Dummy for designer
                return;
            }

            throw new InvalidOperationException("BestSelling must be created with a User object at runtime.");
        }




        private void BestSelling_Load(object sender, EventArgs e)
        {

        }
    }
}
