using Npgsql;
using Project10_PostgreSQLToDoListApp.ConnectionStrings;
using System;
using System.Data;
using System.Windows.Forms;

namespace Project10_PostgreSQLToDoListApp
{
    public partial class FrmCategory : Form
    {
        // Connection sınıfından connectionString almak için bir nesne oluşturuyoruz
        private readonly string connectionString;

        public FrmCategory()
        {
            InitializeComponent();
            // Connection sınıfından connection string'i alıyoruz
            Connection connection = new Connection();
            connectionString = connection.GetConnectionString();  // Connection dizesini buradan alıyoruz
        }

        void CategoryList()
        {
            string query = "SELECT * FROM categories ORDER BY CategoryId";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                using (var adapter = new NpgsqlDataAdapter(query, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    dataGridView1.DataSource = dataSet.Tables[0];
                }
            }
        }

        private void FrmCategory_Load(object sender, EventArgs e)
        {
            CategoryList();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            CategoryList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO categories (CategoryName) VALUES (@categoryname)";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryname", txtName.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Kategori Eklendi");
                    CategoryList();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string query = "DELETE FROM categories WHERE CategoryId = @categoryId";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryId", id);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Kategori Başarıyla Silindi");
                    CategoryList();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string categoryName = txtName.Text;
            string query = "UPDATE categories SET CategoryName = @categoryName WHERE CategoryId = @categoryId";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryId", id);
                    command.Parameters.AddWithValue("@categoryName", categoryName);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Kategori Başarıyla Güncellendi");
                    CategoryList();
                }
            }
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string query = "SELECT * FROM categories WHERE CategoryId = @categoryId";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryId", id);
                    using (var adapter = new NpgsqlDataAdapter(command))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        dataGridView1.DataSource = dataSet.Tables[0];
                    }
                }
            }
        }
    }
}
