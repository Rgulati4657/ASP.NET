using System;
using System.Data;
using Microsoft.Data.SqlClient;
namespace pracform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // address of sql server and database
            string connectionString = "Data Source=(localdb)\\Local;Initial Catalog=mydb;Integrated Security=True";
            // establish connection
            SqlConnection conn = new SqlConnection(connectionString);
            // open connection
            conn.Open();
            // prepare query
            string username = txtb1.Text;

            string password = txtb2.Text;
            string Query = "INSERT INTO USERR (NAME,ROLL_NO) VALUES ('"+username+"','"+password+"')";
            // execute querry
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.ExecuteNonQuery();
            // close connection
            conn.Close();
            MessageBox.Show("You Got This");
        }
    }
}