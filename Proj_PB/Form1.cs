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
using System.IO;

namespace Proj_PB
{
    public partial class Form1 : Form
    {
        public bool czyMetalic = false;

        public string wybSilnik = "";
        public string wybranyIndex = "";
        
        public bool klikniete = false;

        public string czyABS = "";
        public string czyKlimatyzacja = "";
        public string czyWspomaganie = "";
        public string tabKom = "";
        public string tab = "Table";
       

        public Form1(string nazwa)
        {
            this.UserName = nazwa;
            InitializeComponent();
            string[] wyposazenie = { "ABS", "klimatyzacja", "wspomaganie kierownicy" };
            checkedListBox1.Items.AddRange(wyposazenie);

            string[] komis = {"komis Kraków", "komis Warszawa"};
            comboBox4.Items.AddRange(komis);
            comboBox4.SelectedIndex = 0;

            // Changes the selection mode from double-click to single click.
            checkedListBox1.CheckOnClick = true;
        }

        private string UserName { get; set; }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
            {
            
            if(checkedListBox1.GetItemChecked(checkedListBox1.SelectedIndex))
            {
                klikniete = false;
            }
            else 
            {
                klikniete = true;
            }


            foreach (int indexChecked in checkedListBox1.CheckedIndices)
            {

                if (klikniete)
                {
                    switch (indexChecked.ToString())
                    {
                        case "0":
                            czyABS = "";
                            break;
                        case "1":
                            czyKlimatyzacja = "";
                            break;
                        case "2":
                            czyWspomaganie = "";
                            break;
                    }
                }
                else
                {
                    switch (indexChecked.ToString())
                    {
                        case "0":
                            czyABS = " AND ABS = 'True'";
                            break;
                        case "1":
                            czyKlimatyzacja = " AND klimatyzacja = 'True'";
                            break;
                        case "2":
                            czyWspomaganie = " AND [wspomaganie kierownicy] = 'True'";
                            break;

                    }
                }                    
            
                           
                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");

                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, marka, model, silnik, kolor, metalic, ABS, klimatyzacja, [wspomaganie kierownicy], zdjecie FROM [dbo].[" + tab + "] where (silnik='" + wybSilnik + "'" + czyABS + czyKlimatyzacja + czyWspomaganie +")", conn);
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'carsDataSet.Table' table. You can move, or remove it, as needed.
            // this.tableTableAdapter.Fill(this.carsDataSet.Table);


            if (UserName == "admin")
            {
                button2.Visible = true;
            }
            else
            {
                button2.Visible = false;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
            DataTable dt = new DataTable();
            string wybMarka = listBox1.GetItemText(listBox1.SelectedValue);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT model FROM [dbo].[" + tab + "] where [marka ]='" + wybMarka+"'", conn);
            adapter.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "model";

            SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
            DataTable dt1 = new DataTable();
            SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT Id, marka, model, silnik, kolor, metalic, ABS, klimatyzacja, [wspomaganie kierownicy], zdjecie FROM [dbo].[" + tab + "] where [marka ]='" + wybMarka + "'", conn1);
            adapter1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[9].Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
            DataTable dt = new DataTable();
            string wybModel = comboBox1.GetItemText(comboBox1.SelectedValue);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT silnik FROM [dbo].[" + tab + "] where model='" + wybModel + "'", conn);
            adapter.Fill(dt);
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "silnik";

            SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
            DataTable dt1 = new DataTable();
            SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT Id, marka, model, silnik, kolor, metalic, ABS, klimatyzacja, [wspomaganie kierownicy], zdjecie FROM [dbo].[" + tab + "] where model='" + wybModel + "'", conn1);
            adapter1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[9].Visible = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
            DataTable dt = new DataTable();
            wybSilnik = comboBox2.GetItemText(comboBox2.SelectedValue);

            SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
            DataTable dt1 = new DataTable();

            if (czyMetalic == false)
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT kolor FROM [dbo].[" + tab + "] where (silnik='" + wybSilnik + "')", conn);
                adapter.Fill(dt);
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "kolor";
                
                SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT DISTINCT Id, marka, model, silnik, kolor, metalic, ABS, klimatyzacja, [wspomaganie kierownicy], zdjecie FROM [dbo].[" + tab + "] where (silnik='" + wybSilnik + "')", conn1);
                adapter1.Fill(dt1);
                dataGridView1.DataSource = dt1;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[9].Visible = false;
            }
            else
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT kolor FROM [dbo].[" + tab + "] where (silnik='" + wybSilnik + "' AND metalic = '" + czyMetalic.ToString() + "')", conn);
                adapter.Fill(dt);
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "kolor";

                SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT DISTINCT Id, marka, model, silnik, kolor, metalic, ABS, klimatyzacja, [wspomaganie kierownicy], zdjecie FROM [dbo].[" + tab + "] where (silnik='" + wybSilnik + "' AND metalic = '" + czyMetalic.ToString() + "')", conn1);
                adapter1.Fill(dt1);
                dataGridView1.DataSource = dt1;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[9].Visible = false;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            czyMetalic = checkBox1.Checked;

            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
            DataTable dt = new DataTable();

            SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
            DataTable dt1 = new DataTable();

            if (czyMetalic == false)
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT kolor FROM [dbo].[" + tab + "] where (silnik='" + wybSilnik + "')", conn);
                adapter.Fill(dt);
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "kolor";

                SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT DISTINCT Id, marka, model, silnik, kolor, metalic, ABS, klimatyzacja, [wspomaganie kierownicy], zdjecie FROM [dbo].[" + tab + "] where (silnik='" + wybSilnik + "')", conn1);
                adapter1.Fill(dt1);
                dataGridView1.DataSource = dt1;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[9].Visible = false;
            }
            else
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT kolor FROM [dbo].[" + tab + "] where (silnik='" + wybSilnik + "' AND metalic = '" + czyMetalic.ToString() + "')", conn);
                adapter.Fill(dt);
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "kolor";

                SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT DISTINCT Id, marka, model, silnik, kolor, metalic, ABS, klimatyzacja, [wspomaganie kierownicy], zdjecie FROM [dbo].[" + tab + "] where (silnik='" + wybSilnik + "' AND metalic = '" + czyMetalic.ToString() + "')", conn1);
                adapter1.Fill(dt1);
                dataGridView1.DataSource = dt1;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[9].Visible = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 a1 = new Form2(UserName);
            a1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Logowanie a1 = new Logowanie();
            a1.ShowDialog();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].FormattedValue);

            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
            conn.Open();
            SqlCommand cm = new SqlCommand("SELECT zdjecie FROM [dbo].[" + tab + "] where Id='" + id + "'", conn);
            string img = cm.ExecuteScalar().ToString();

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string ImagePath = Path.Combine(projectDirectory, "photos", img);

            pictureBox1.Image = Image.FromFile(ImagePath);
            
            conn.Close();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
         tabKom = comboBox4.GetItemText(comboBox4.SelectedIndex);
           
            switch(tabKom) 
            {
                case "0":
                    {
                        tab = "Table";
                        break;
                    }
                case "1":
                    {
                        tab = "Table_1";
                        break;
                    }
            };

            {
                SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT DISTINCT marka FROM [dbo].[" + tab + "] ", conn);
                adapter.Fill(dt);
                listBox1.DataSource = dt;
                listBox1.DisplayMember = "marka";

                //SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\cars.mdf;Integrated Security=True");
                DataTable dt1 = new DataTable();
                SqlDataAdapter adapter1 = new SqlDataAdapter("SELECT Id, marka, model, silnik, kolor, metalic, ABS, klimatyzacja, [wspomaganie kierownicy], zdjecie  FROM [dbo].[" + tab + "] ", conn);
                adapter1.Fill(dt1);
                dataGridView1.DataSource = dt1;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[9].Visible = false;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
