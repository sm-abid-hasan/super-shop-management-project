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
    public partial class FormOrderPayment : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        public FormOrderPayment()
        {
            InitializeComponent();
            SetDueAmount();
            LoadPaymentType();
        }

        private void SetDueAmount()
        {
            GetOrderTotal();
            GetPaymentAmount();
            SetDueText();
        }

        private void LoadPaymentType()
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
                    string query = @"SELECT [id] ,[methodname] FROM [dbo].[PaymentMethod]";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        comboBox1.DisplayMember = "methodname";
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

        private void SetDueText()
        {
            textBox2.Text = (Data.OrderTotal - Data.PaymentAmount).ToString();
        }

        private void GetPaymentAmount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"SELECT SUM(amount) PaymentAmount
                                    FROM [OrderPayment]
                                    WHERE orderid = @orderid;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderid", Data.OrderId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count == 1)
                        {
                            if (dataTable.Rows[0]["PaymentAmount"].ToString() != "")
                            {
                                Data.PaymentAmount = (float)Convert.ToDouble(dataTable.Rows[0]["PaymentAmount"].ToString());
                            }

                        }
                    }
                }
            }
        }

        private void GetOrderTotal()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"SELECT SUM(totalprice) OrderTotal
                                FROM [OrderItem]
                                WHERE orderid = @orderid;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderid", Data.OrderId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count == 1)
                        {
                            if (dataTable.Rows[0]["OrderTotal"].ToString() != "")
                            {
                                Data.OrderTotal = (float)Convert.ToDouble(dataTable.Rows[0]["OrderTotal"].ToString());
                            }

                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Validate Form First

            //Create Order and Add Item                
            AddPayments();
            LoadPayments();
        }
        private void AddPayments()
        {
            try
            {
                double amount = 0;
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please Enter Amount!!");
                    return;
                }
                else if (!double.TryParse(textBox1.Text, out amount))
                {
                    MessageBox.Show("Please Enter Valid Amount!!");
                    return;
                }
                if ((Data.OrderTotal - Data.PaymentAmount) < amount)
                {
                    MessageBox.Show("Amount Can't Be Greater Than Due Amount!!");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO [OrderPayment]
                                            ([paymentmethodid]
                                            ,[orderid]
                                            ,[amount])
                                        VALUES
                                            (@paymentmethodid
                                            ,@orderid
                                            ,@amount);";
                    // Open the database connection
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@paymentmethodid", comboBox1.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@orderid", Data.OrderId);
                        command.Parameters.AddWithValue("@amount", textBox1.Text);

                        command.ExecuteNonQuery();
                    }
                }
                SetDueAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void LoadPayments()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"SELECT PM.methodname,[amount]
                                  FROM [dbo].[OrderPayment] P
                                  JOIN [dbo].[PaymentMethod] PM ON P.paymentmethodid = PM.id
                                  WHERE orderid = @orderid;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderid", Data.OrderId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if ((Data.OrderTotal - Data.PaymentAmount) == 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = @"UPDATE [dbo].[Order]
                                           SET [paymentstatus] = 1
                                              ,[orderstatus] = 1
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
                            command.Parameters.AddWithValue("@orderid", Data.OrderId);

                            // Execute the query
                            command.ExecuteNonQuery();

                            MessageBox.Show("Order Confirmed Successfully!");
                            Data.ClearAll();
                            new FormCustomerDashboard().Show();
                            this.Hide();
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Pay Due Amount To Pay!!");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            new FormOrderDeliverySelection().Show();
            this.Hide();
        }
        private void FormOrderPayment_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
