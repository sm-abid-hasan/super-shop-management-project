using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SuperShopApp
{
    public partial class FormLogin : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        public FormLogin()
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

        private void ButtonRegister_Click(object sender, EventArgs e)
        {            
            new FormRegister().Show();
            this.Hide();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Data.ClearAll();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the database connection
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    // SQL query to select data from table
                    string query = @"SELECT [id] FROM [SuperShopUser] WHERE [username] =  @username AND [password] = @password AND [usertypeid] = @usertypeid";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {                        
                        command.Parameters.AddWithValue("@username", textBox1.Text);
                        command.Parameters.AddWithValue("@password", textBox2.Text);
                        command.Parameters.AddWithValue("@usertypeid", comboBox1.SelectedValue);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count>0)
                            {
                                MessageBox.Show("Login Successfull");
                                if (comboBox1.Text.ToString() == "Admin")
                                {
                                    new FormAdminDasboard().Show();
                                    this.Hide();
                                }

                                else if (comboBox1.Text.ToString() == "Customer")
                                {
                                    new FormCustomerDashboard().Show();
                                    this.Hide();
                                }
                                Data.customerdata = dataTable;
                            }
                            else
                            {
                                MessageBox.Show("Login Failed");
                            }
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
    
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
