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
    public partial class FrmMovies : Form
    {
        public FrmMovies()
        {
            InitializeComponent();
        }
        MoviesContext context = new MoviesContext();

        void CategoryList()
        {
            var values = context.Categories.ToList();
            cbdCategory.DisplayMember = "CategoryName";
            cbdCategory.ValueMember = "CategoryId";
            cbdCategory.DataSource = values;
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            var values = context.Movies.ToList();
            dataGridView1.DataSource = values;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Movie movie = new Movie();
            movie.MovieTitle = txtName.Text;
            movie.Duration = int.Parse(txtDuration.Text);
            movie.Description = txtDescription.Text;
            movie.CreatedDate = DateTime.Now;
            movie.CategoryId = (int)cbdCategory.SelectedValue;
            context.Movies.Add(movie);
            context.SaveChanges();
            MessageBox.Show("Film Ekledi");
        }

        private void FrmMovies_Load(object sender, EventArgs e)
        {
            CategoryList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var values = context.Movies.Find(id);
            context.Movies.Remove(values);
            context.SaveChanges();
            MessageBox.Show("Film Silindi");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var values = context.Movies.Find(id);
            values.MovieTitle = txtName.Text;
            values.Duration = int.Parse(txtDuration.Text);
            values.Description = txtDescription.Text;
            values.CategoryId = (int)cbdCategory.SelectedValue;
            context.SaveChanges();
            MessageBox.Show("Film Güncellendi");

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var values = context.Movies.Where(x => x.MovieTitle.Contains(txtName.Text)).ToList();
            dataGridView1.DataSource = values;
        }

        private void btnMovieWithCategory_Click(object sender, EventArgs e)
        {
            var values = context.Movies
                .Join(context.Categories,
                movie => movie.CategoryId,
                category => category.CategoryId,
                (movie, category) => new
                {
                    MovieId = movie.MovieId,
                    MovieTitle = movie.MovieTitle,
                    Description = movie.Description,
                    Duration = movie.Duration,
                    CategoryName = category.CategoryName
                }).ToList();
            dataGridView1.DataSource = values;

        }
    }
}
