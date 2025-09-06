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
    public partial class FormAdminAddProduct : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        public FormAdminAddProduct()
        {
            InitializeComponent();
            LoadProductType();
            LoadProductDetails();
        }

        private void LoadProductDetails()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"SELECT pt.typename, productname, price 
                                    FROM Product p
                                    JOIN ProductType pt ON p.producttypeid = pt.id 
                                    WHERE pt.id = @producttypeid";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@producttypeid", comboBox1.SelectedValue.ToString());
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
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
                // SQL query to select data from table
                string query = @"SELECT [id] ,[typename] FROM [ProductType]";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    comboBox1.DisplayMember = "typename";
                    comboBox1.ValueMember = "id";
                    comboBox1.DataSource = dataTable;
                }
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO [dbo].[Product]
                                    ([producttypeid], [productname], [quantity], [price])
                                        VALUES
                                    (@producttypeid ,@productname ,100 ,@price);";
                    // Open the database connection
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@producttypeid", comboBox1.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@productname", textBox1.Text);
                        command.Parameters.AddWithValue("@price", textBox2.Text);

                        // Execute the query
                        command.ExecuteNonQuery();
                        MessageBox.Show("Product name and Price Added Successfully.");
                    }
                }
                LoadProductDetails();
            }
            catch
            {
                MessageBox.Show("Failed to add Product name and price, Please Try Again!!");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            new FormAdminDasboard().Show();
            this.Hide();

        }
        private void FormAdminAddProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProductDetails();
        }
    }
}

