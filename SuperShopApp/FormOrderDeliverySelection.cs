using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SuperShopApp
{
    public partial class FormOrderDeliverySelection : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        public FormOrderDeliverySelection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte deliveryType = 0;
                if (!checkBox1.Checked && !checkBox2.Checked)
                {
                    MessageBox.Show("Please Select Delivery/Collection!!");
                    return;
                }
                else if (checkBox1.Checked)
                {
                    deliveryType = 1;
                }
                else
                {
                    deliveryType = 2;
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE [Order] SET [deliverytype] = @deliverytype;";
                    // Open the database connection
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@deliverytype", deliveryType);

                        // Execute the query
                        command.ExecuteNonQuery();
                        new FormOrderPayment().Show();
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                if (checkBox2.Checked==true)
                {
                    checkBox2.Checked = false;
                }
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                if (checkBox1.Checked == true)
                {
                    checkBox1.Checked = false;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            new FormOrderItems().Show();
            this.Hide();
        }

        private void FormOrderDeliverySelection_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
