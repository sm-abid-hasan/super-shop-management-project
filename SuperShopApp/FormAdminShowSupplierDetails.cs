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
    public partial class FormAdminShowSupplierDetails : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        public FormAdminShowSupplierDetails()
        {
            InitializeComponent();
            LoadSupplierDetails();
        }

        private void LoadSupplierDetails()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"SELECT id, suppliername, phone, email FROM Supplier";
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
            new FormAdminDasboard().Show();
            this.Hide();
        }

        private void FormAdminShowSupplierDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
