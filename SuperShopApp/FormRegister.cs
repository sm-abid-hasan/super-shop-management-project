using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SuperShopApp
{
    public partial class FormRegister : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        //private SqlConnection connection;

        public FormRegister()
        {
            InitializeComponent();
            LoadUserTypeComboBox();
        }
        private void LoadUserTypeComboBox()
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
                    string query = @"SELECT [id] ,[typename] FROM [UserType]";
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO [SuperShopUser] (usertypeid, username, fullname, phone, email, password, address, district, city, postcode) 
                             VALUES (@usertypeid, @username, @fullname, @phone, @email, @password, @address, @district, @city, @postcode)";
                    // Open the database connection
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@usertypeid", comboBox1.SelectedValue);
                        command.Parameters.AddWithValue("@username", textBox1.Text);
                        command.Parameters.AddWithValue("@fullname", textBox2.Text);
                        command.Parameters.AddWithValue("@phone", textBox3.Text);
                        command.Parameters.AddWithValue("@email", textBox4.Text);
                        command.Parameters.AddWithValue("@password", textBox5.Text);
                        command.Parameters.AddWithValue("@address", textBox6.Text);
                        command.Parameters.AddWithValue("@district", textBox7.Text);
                        command.Parameters.AddWithValue("@city", textBox8.Text);
                        command.Parameters.AddWithValue("@postcode", textBox9.Text);

                        // Execute the query
                        command.ExecuteNonQuery();

                        MessageBox.Show("User added successfully!");
                        new FormLogin().Show();
                        this.Hide();
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            new FormLogin().Show();
            this.Hide();
        }

        private void FormRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
