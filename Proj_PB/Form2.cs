using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proj_PB
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string nazwa)
        {
            this.UserName = nazwa;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'carsDataSet.Table' table. You can move, or remove it, as needed.
            // this.tableTableAdapter.Fill(this.carsDataSet.Table);

            
                SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
                DataTable dt1 = new DataTable();
                SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT marka, model, silnik, kolor, metalic, ABS, klimatyzacja, [wspomaganie kierownicy] FROM [dbo].[Table] ", conn1);
                adapter1.Fill(dt1);
                dataGridView1.DataSource = dt1;
        }


        private string UserName { get; set; }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 a1 = new Form1(UserName);
            a1.ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Logowanie a1 = new Logowanie();
            a1.ShowDialog();
        }
    }
}
