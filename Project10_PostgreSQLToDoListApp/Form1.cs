using Npgsql;
using Project10_PostgreSQLToDoListApp.ConnectionStrings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Project10_PostgreSQLToDoListApp
{
    public partial class Form1 : Form
    {
        // Connection sınıfından connectionString almak için bir nesne oluşturuyoruz
        private readonly string connectionString;
        public Form1()
        {
            InitializeComponent();
            // Connection sınıfından connection string'i alıyoruz
            Connection connection = new Connection();
             connectionString = connection.GetConnectionString();  // Connection dizesini buradan alıyoruz
        }

        void TodoList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM todolists";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();

        }
        void CategoryList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM categories";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryId";
            cmbCategory.DataSource = dataTable;
            connection.Close();
        }

        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            FrmCategory frmCategory = new FrmCategory();
            frmCategory.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TodoList();
            CategoryList();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            TodoList();
        }

        
        //bool status = false;

        //    if (rbdCompleted.Checked)
        //    {
        //        status = true;

        //    }
        //    if (rbdContiune.Checked)
        //    {
        //        status = false;

        //    }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            int categoryId = int.Parse(cmbCategory.SelectedValue.ToString());
            string title = txtTitle.Text;
            string priority = txtpriorty.Text;
            string description = txtDescription.Text;
            string status = "0";

            if (rbdCompleted.Checked)
            {
                status = "1";
            }
            if (rbdContiune.Checked)
            {
                status = "0";
            }

            string query = "INSERT INTO todolists (title,description,status,priority,categoryid)" +
                " VALUES (@title,@description,@status,@priority,@categoryid)";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@priority", priority);
                    command.Parameters.AddWithValue("@categoryid", categoryId);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Yapılacak İşlem Eklendi");
                    TodoList();
                }
                connection.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string query = "DELETE FROM todolists WHERE TodoListId = @toDoListId";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@toDoListId", id);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Todo Başarıyla Silindi");
                    CategoryList();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            int id = int.Parse(txtId.Text);
            int categoryId = int.Parse(cmbCategory.SelectedValue.ToString());
            string title = txtTitle.Text;
            string priority = txtpriorty.Text;
            string description = txtDescription.Text;
            string status = "0";

            if (rbdCompleted.Checked)
            {
                status = "1";
            }
            if (rbdContiune.Checked)
            {
                status = "0";
            }
            string query = "UPDATE todolists SET Title = @title,Description=@description,status=@status, priority=@priority,categoryid=@categoryid WHERE todolistid = @todolistid";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@todolistid", id);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@description",description);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@priority", priority);
                    command.Parameters.AddWithValue("@categoryid", categoryId);
            
                    command.ExecuteNonQuery();
                    MessageBox.Show("Todo Başarıyla Güncellendi");
                    CategoryList();
                }
            }
        }

        private void btnCheckedList_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM todolists where Status='1'";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void btnContiuneList_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM todolists where Status='0'";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void btnListandCategory_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "SELECT todolistid,title,description,status,priority,categoryname From todolists Inner JOIN categories on todolists.categoryid=categories.categoryid";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
    }
}
