using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SuperShopApp
{
    public partial class FormOrderItems : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        public FormOrderItems()
        {
            InitializeComponent();
            //load product type
            LoadProductType();
            LoadProduct();
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
        private void LoadProduct()
        {
            if (string.IsNullOrEmpty(comboBox1.SelectedValue.ToString()))
            {
                return;
            }
            if (Convert.ToInt32(comboBox1.SelectedValue.ToString()) <= 0)
            {
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"SELECT [id] ,[productname] FROM [Product] WHERE [producttypeid] = @producttypeid";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@producttypeid", comboBox1.SelectedValue.ToString());
                    // SQL query to select data from table                    
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        comboBox2.DataSource = null;
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        comboBox2.DisplayMember = "productname";
                        comboBox2.ValueMember = "id";
                        comboBox2.DataSource = dataTable;
                    }
                }
            }
        }
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProductPrice();
        }

        private void LoadProductPrice()
        {
            if (comboBox2.SelectedValue == null)
            {
                textBox1.Text = string.Empty;
                return;
            }
            if (string.IsNullOrEmpty(comboBox2.SelectedValue.ToString()))
            {
                return;
            }
            if (Convert.ToInt32(comboBox2.SelectedValue.ToString()) <= 0)
            {
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"SELECT [price] FROM [dbo].[Product]
                                    WHERE [id] = @id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", comboBox2.SelectedValue.ToString());
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count ==1)
                        {
                            textBox1.Text = dataTable.Rows[0]["price"].ToString();
                        }
                    }
                }
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    if (decimal.TryParse(textBox1.Text, out decimal value))
                    {
                        textBox3.Text = (value * Convert.ToDecimal(textBox2.Text)).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox3.Text = "";
            }            
        }
        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            //Validate Form First

            if (dataGridView1.Rows.Count == 0)
            {
                //Create Order and Add Item
                CreateOrder();
                AddItem();
                LoadOrderItems();
            }
            else
            {
                //Add Item On Order
                AddItem();
                LoadOrderItems();
            }
        }

        private void LoadOrderItems()
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
        private void AddItem()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO [dbo].[OrderItem]
                                            ([orderid] ,[productid] ,[quantity] ,[price] ,[totalprice])
                                        VALUES
                                            (@orderid ,@productid ,@quantity ,@price ,@totalprice);";
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
                        command.Parameters.AddWithValue("@productid", comboBox2.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@quantity", textBox2.Text);
                        command.Parameters.AddWithValue("@price", textBox1.Text);
                        command.Parameters.AddWithValue("@totalprice", textBox3.Text);

                        // Execute the query
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void CreateOrder()
        {
            int customerId = 0;
            if (Data.customerdata!=null)
            {
                if (Data.customerdata.Rows.Count>0)
                {
                    customerId = Convert.ToInt32(Data.customerdata.Rows[0]["id"].ToString());
                }
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"INSERT INTO [dbo].[Order]
                                    ([customerid] ,[orderdate] ,[paymentstatus] ,[orderstatus])
                                VALUES
                                    (@customerid, @orderdate,@paymentstatus,@orderstatus);
                                SELECT IDENT_CURRENT('[dbo].[Order]') AS id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customerid", customerId);
                    command.Parameters.AddWithValue("@orderdate", DateTime.Now);
                    command.Parameters.AddWithValue("@paymentstatus", 0);
                    //0 initiated from customer,
                    //1 confirmed from customer
                    //2 approved from admin
                    //3 rejected from admin


                    command.Parameters.AddWithValue("@orderstatus", 0);
                    // SQL query to select data from table                    
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count==1)
                        {
                            Data.OrderId = Convert.ToInt32(dataTable.Rows[0]["id"].ToString());
                        }
                    }
                }
            }
        }
        private void BtnNext_Click(object sender, EventArgs e)
        {
            new FormOrderDeliverySelection().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new FormCustomerDashboard().Show();
            this.Hide();
        }

        private void FormOrderItems_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
