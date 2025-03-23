using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project10_PostgreSQLToDoListApp
{
    public partial class FrmToDoListApp : Form
    {
        public FrmToDoListApp()
        {
            InitializeComponent();
        }

        NpgsqlConnection connection = new NpgsqlConnection("Server=localHost;port=5432;Database=DbProject10TodoApp;user ID=postgres;Password=1234");
        private void FrmToDoListApp_Load(object sender, EventArgs e)
        {

            string query = "SELECT * FROM mytable1";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query,connection);
            DataSet dataSet=new DataSet();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

        }
    }
}
