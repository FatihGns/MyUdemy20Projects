using Project4_EntityFramewoerCodeFirstMovie.DAL.Context;
using Project4_EntityFramewoerCodeFirstMovie.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project4_EntityFramewoerCodeFirstMovie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MoviesContext context = new MoviesContext();
        private void btnList_Click(object sender, EventArgs e)
        {
            var values = context.Categories.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
             Category category = new Category();
            category.CategoryName = txtName.Text;
            context.Categories.Add(category);
            context.SaveChanges();
            MessageBox.Show("Kategori Eklendi");

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
            var values = context.Categories.Find(int.Parse(txtId.Text));
            values.CategoryName = txtName.Text;
            context.SaveChanges();
            MessageBox.Show("Kategori Güncellendi");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var values = context.Categories.Find(id);
            context.Categories.Remove(values);
            context.SaveChanges();
            MessageBox.Show("Kategori Silindi");

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            var values = context.Categories.Where(x => x.CategoryName.Contains(txtName.Text)).ToList();
            dataGridView1.DataSource = values;


        }
    }
}
