using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperShopApp
{
    public partial class FormAdminDasboard : Form
    {
        public FormAdminDasboard()
        {
            InitializeComponent();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            new FormApproveOrder().Show();
            this.Hide();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            new FormAdminShowReviewRatings().Show();
            this.Hide();
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            new FormAdminAddProductType().Show();
            this.Hide();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            new FormAdminAddProduct().Show();
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            new FormAdminShowSupplierDetails().Show();
            this.Hide(); 
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            new FormAdminShowPaymentDetails().Show();
            this.Hide();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            new FormLogin().Show();
            this.Hide();
        }
        private void FormAdminDasboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
