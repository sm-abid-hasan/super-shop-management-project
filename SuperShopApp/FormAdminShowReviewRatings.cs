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
    public partial class FormAdminShowReviewRatings : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        public FormAdminShowReviewRatings()
        {
            InitializeComponent();
            LoadReviewedOrders();
        }

        private void LoadReviewedOrders()
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
                    string query = @"SELECT [id] FROM [Order] WHERE [paymentstatus] = 1 AND [orderstatus] = 2 AND (rating IS NOT NULL OR review IS NOT NULL)";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        comboBox1.DisplayMember = "id";
                        comboBox1.ValueMember = "id";
                        comboBox1.DataSource = dataTable;
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
            LoadOrderReviewAndRating(comboBox1.SelectedValue.ToString());
        }

        private void LoadOrderReviewAndRating(string selectedOrderId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                // SQL query to select data from table
                string query = @"SELECT rating, review
                                FROM [Order]
                                WHERE id = @orderId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderId", selectedOrderId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            textBox1.Text = dataTable.Rows[0]["review"].ToString();
                            textBox2.Text = dataTable.Rows[0]["rating"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Customer Not Found!!");
                        }
                    }
                }
            }
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
            new FormAdminDasboard().Show();
            this.Hide();
        }
    }
}
