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

namespace SuperShopApp
{
    public partial class FormCustomerRatingAndReview : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        public FormCustomerRatingAndReview()
        {
            InitializeComponent();
            LoadOrdersToReview();
        }

        private void LoadOrdersToReview()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the database connection

                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    // SQL query to select data from table
                    string query = @"SELECT [id] FROM [Order] WHERE [paymentstatus] = 1 AND customerid = @customerid AND [orderstatus] = 2 AND rating IS NULL AND review IS NULL";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@customerid", Data.customerdata.Rows[0]["id"].ToString());
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            comboBox1.DisplayMember = "id";
                            comboBox1.ValueMember = "id";
                            comboBox1.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOrderItems(comboBox1.SelectedValue.ToString());
        }
        private void LoadOrderItems(string selectedOrderId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"SELECT product.[productname], item.[quantity] ,item.[price] ,item.[totalprice]
                                    FROM [OrderItem] AS item
                                    JOIN [Product] AS product ON item.productid = product.id
                                    WHERE item.[orderid] = @orderid;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderid", selectedOrderId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE [dbo].[Order]
                                           SET [rating] = @rating,
                                               [review] = @review
                                         WHERE [id] = @orderid;";
                    // Open the database connection
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@orderid", comboBox1.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@rating", textBox2.Text);
                        command.Parameters.AddWithValue("@review", textBox1.Text);

                        // Execute the query
                        command.ExecuteNonQuery();
                        MessageBox.Show("Review And Rating Added Successfully.");
                    }
                }
                LoadOrdersToReview();
                textBox1.Text = string.Empty;
                textBox2.Text = string.Empty;
            }
            catch
            {
                MessageBox.Show("Failed to add Review Rating, Please Try Again!!");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            new FormCustomerDashboard().Show();
            this.Hide();
        }

        private void FormCustomerRatingAndReview_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
