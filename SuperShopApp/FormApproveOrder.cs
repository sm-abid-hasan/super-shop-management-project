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
    public partial class FormApproveOrder : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        public FormApproveOrder()
        {
            InitializeComponent();
            LoadOrdersToApprove();
        }
        private void LoadOrdersToApprove()
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
                    string query = @"SELECT [id] FROM [Order] WHERE [paymentstatus] = 1 AND [orderstatus] = 1";
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
            LoadOrderCustomer(comboBox1.SelectedValue.ToString());
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
        private void LoadOrderCustomer(string selectedOrderId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                // SQL query to select data from table
                string query = @"SELECT fullname, phone, [address]
                                FROM [Order] O
                                INNER JOIN  SuperShopUser cust ON cust.id = o.customerid
                                WHERE o.id = @orderId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderId", selectedOrderId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            textBox1.Text = dataTable.Rows[0]["fullname"].ToString();
                            textBox2.Text = dataTable.Rows[0]["phone"].ToString();
                            textBox3.Text = dataTable.Rows[0]["address"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Customer Not Found!!");
                        }
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
                                           SET [orderstatus] = 2
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

                        // Execute the query
                        command.ExecuteNonQuery();
                        MessageBox.Show("Order Approved Successfully.");
                    }
                }
                LoadOrdersToApprove();
            }
            catch 
            {
                MessageBox.Show("Order Approved Failed, Please Try Again");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE [dbo].[Order]
                                           SET [orderstatus] = 3
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

                        // Execute the query
                        command.ExecuteNonQuery();
                        MessageBox.Show("Order Declined Successfully.");
                    }
                }
                LoadOrdersToApprove();
            }
            catch
            {
                MessageBox.Show("Order Declined Failed, Please Try Again!!");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            new FormAdminDasboard().Show();
            this.Hide();
        }
        private void FormApproveOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
