using Dapper;
using Project5_DapperNorthwind.Dtos.CategoryDtos;
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

namespace Project5_DapperNorthwind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Server=LAPTOP-U78U9MAM\\SQLEXPRESS;initial catalog=Db5Project20;integrated security=true");
        private async void btnCategoryList_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Categories";
            var values = await connection.QueryAsync<ResultCategoryDto>(query);
            dataGridView1.DataSource = values.ToList();


        }

        private async void btnCategoryCreate_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Categories(CategoryName,Description) VALUES(@p1,@p2)";
            var parametres= new DynamicParameters();
            parametres.Add("@p1", txtCategoryName.Text);
            parametres.Add("@p2", txtDescription.Text);
            await connection.ExecuteAsync(query, parametres);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM Categories WHERE CategoryID=@categoryId";
            var parametres = new DynamicParameters();
            parametres.Add("@categoryId", txtCategoryId.Text);
            await connection.ExecuteAsync(query, parametres);
            MessageBox.Show("Silme işlemi başarılı");
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "Update Categories SET CategoryName=@CategoryName,Description=@Description WHERE CategoryID=@CategoryId";
            var parametres = new DynamicParameters();
            parametres.Add("@CategoryName", txtCategoryName.Text);
            parametres.Add("@Description", txtDescription.Text);
            parametres.Add("@CategoryId", txtCategoryId.Text);
            await connection.ExecuteAsync(query, parametres);
            MessageBox.Show("Güncelleme işlemi başarılı");
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //string query = "SELECT * FROM Categories";
            //var values = await connection.QueryAsync<ResultCategoryDto>(query);
            //dataGridView1.DataSource = values.ToList();

            string query = "SELECT Count(*) FROM Categories";
            var count =await connection.ExecuteScalarAsync<int>(query);
            lblCategoryCount.Text = "Toplam Kategori Sayısı: " + count;

        }
    }
}
