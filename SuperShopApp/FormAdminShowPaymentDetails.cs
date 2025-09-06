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
    public partial class FormAdminShowPaymentDetails : Form
    {
        private string connectionString = "Data Source=LAPTOP-QJ8RDJ1A\\SQLEXPRESS;Initial Catalog=SuperShopDB;Integrated Security=True";
        public FormAdminShowPaymentDetails()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            ShowOrderAmount();
            ShowPayments();
        }
        private void ShowOrderAmount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"SELECT PM.methodname, SUM(OP.amount) amount
	                                FROM [Order] O
	                                INNER JOIN [OrderPayment] OP ON OP.orderid = O.id
	                                INNER JOIN [PaymentMethod] PM ON PM.id = OP.paymentmethodid
	                                WHERE orderstatus = 2 
	                                AND [orderdate] BETWEEN @startdate AND @enddate
	                                GROUP BY PM.methodname";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@startdate", dateTimePicker1.Value.ToShortDateString());
                    command.Parameters.AddWithValue("@enddate", dateTimePicker2.Value.ToShortDateString());
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            float totalAmount = 0;
                            foreach (DataRow row in dataTable.Rows)
                            {
                                totalAmount += float.Parse(row.ItemArray[1].ToString());
                                if (row.ItemArray[0].ToString() == "Cash")
                                {
                                    textBox1.Text = row.ItemArray[1].ToString();
                                }
                                else if (row.ItemArray[0].ToString() == "Card")
                                {
                                    textBox2.Text = row.ItemArray[1].ToString();
                                }
                                else if (row.ItemArray[0].ToString() == "bKash")
                                {
                                    textBox3.Text = row.ItemArray[1].ToString();
                                }
                            }
                            textBox4.Text = totalAmount.ToString();
                        }
                    }
                }
            }
        }
        private void ShowPayments()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the database connection
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                string query = @"SELECT PM.methodname, OP.amount
	                                FROM [Order] O
	                                INNER JOIN [OrderPayment] OP ON OP.orderid = O.id
	                                INNER JOIN [PaymentMethod] PM ON PM.id = OP.paymentmethodid
	                                WHERE orderstatus = 2 
	                                AND [orderdate] BETWEEN @startdate AND @enddate";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@startdate", dateTimePicker1.Value.ToShortDateString());
                    command.Parameters.AddWithValue("@enddate", dateTimePicker2.Value.ToShortDateString());
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
            new FormAdminDasboard().Show();
            this.Hide();
        }
        private void FormAdminShowPaymentDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
