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
    public partial class FormCustomerDashboard : Form
    {
        public FormCustomerDashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormOrderItems().Show();
            this.Hide();
        }

        private void FormCustomerDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            new FormLogin().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FormCustomerRatingAndReview().Show();
            this.Hide();
        }
    }
}
