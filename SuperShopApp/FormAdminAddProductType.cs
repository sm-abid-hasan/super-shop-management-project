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

namespace SuperShopApp
{
    public partial class FormAdminAddProductType : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        public FormAdminAddProductType()
        {
            InitializeComponent();
            LoadProductType();
        }

        private void LoadProductType()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"SELECT id, typename FROM ProductType";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO [dbo].[ProductType] ([typename]) VALUES (@typename)";
                    // Open the database connection
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection

                        command.Parameters.AddWithValue("@typename", textBox1.Text);

                        // Execute the query
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product Type Added Successfully.");
                    }
                }
                LoadProductType();
            }
            catch
            {
                MessageBox.Show("Failed to add Product Type, Please Try Again!!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            new FormAdminDasboard().Show();
            this.Hide();
        }

        private void FormAdminAddProductType_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

