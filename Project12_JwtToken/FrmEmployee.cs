using Project12_JwtToken.JWT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project12_JwtToken
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }
        public string tokenGet;
        SqlConnection sqlConnection = new SqlConnection("Server=LAPTOP-U78U9MAM\\SQLEXPRESS;initial catalog=Db12Project20;integrated security=true");
        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            TokenValidator tokenvalidator = new TokenValidator();

            richTextBox1.Text = tokenGet;

            var principal = tokenvalidator.ValidateJwtToken(tokenGet);

            if (principal != null)
            {
                string username = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                MessageBox.Show("Hoş geldiniz: " + username);

                sqlConnection.Open();
                SqlCommand command = new SqlCommand("Select * from TblEmployee", sqlConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                sqlConnection.Close();
            }
            else
            {
                MessageBox.Show("Geçersiz Token!");
            }


           

        }
    }
}
