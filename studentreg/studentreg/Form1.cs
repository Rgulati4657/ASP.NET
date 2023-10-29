using System.Data.SqlClient;

namespace studentreg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();

        }

        SqlConnection conn = new SqlConnection("Data Source = (localdb)\\Local; Initial Catalog = mydb; Integrated Security = True");
        SqlCommand cmd;
        SqlDataReader read;
        string id;
        bool Mode = true;
        string sql;
        SqlDataAdapter adapter;
        public void Load()
        {
            try
            {
                sql = "select * from student";
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                read = cmd.ExecuteReader();
                // adapter = new SqlDataAdapter(sql,conn);
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void getID(string id)
        {
            sql = "select * from  student where id = '" + id + "'";
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            read = cmd.ExecuteReader();

            while (read.Read())
            {
                txtName.Text = read[1].ToString();
                txtCourse.Text = read[2].ToString();
                txtFee.Text = read[3].ToString();

            }

            conn.Close();
        }


        // if the mode is true means add record otherwise update record;
        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string fee = txtFee.Text;

            if (Mode == true)
            {
                sql = "insert into student (stname,course,fee) values (@stname,@course,@fee)";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                MessageBox.Show("Record Added \\(>.<)/ ");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();

                txtName.Focus();



            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update student set stname = @stname, course = @course, fee = @fee where id = @id ";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@stname", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Record Updated (～￣▽￣)～ ");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();

                txtName.Focus();
                button1.Text = "SAVE";
                Mode = true;

            }
            conn.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["EDIT"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "UPDATE";
            }
            if (e.ColumnIndex == dataGridView1.Columns["DELETE"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from student where id = @id ";
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted ✍️(◔◡◔)");
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            txtName.Clear();
            txtCourse.Clear();
            txtFee.Clear();

            txtName.Focus();
            button1.Text = "SAVE";
            Mode = true;

        }
    }
}