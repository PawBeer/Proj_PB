using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proj_PB
{
    public partial class Logowanie : Form
    {
        public Logowanie()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LMSEntities content = new LMSEntities();
            if (textBox1.Text != String.Empty && textBox2.Text != String.Empty)
            {
                var user = content.LMSAdmin.Where(x => x.nazwaUzytkownika.Equals(textBox1.Text)).FirstOrDefault();
                if (user != null)
                {
                    if (user.haslo.Equals(textBox2.Text))
                    {

                        this.Hide();

                        string userName = user.nazwaUzytkownika;

                        if (user.nazwaUzytkownika.Equals("admin"))
                        {
                            Form2 a1 = new Form2(userName);
                            a1.ShowDialog();
                        }
                        else
                        {
                            Form1 w1 = new Form1(userName);
                            w1.ShowDialog();
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Wpisałeś niepoprawne hasło");
                    }
                }
                else
                {
                    MessageBox.Show("Wprowadziłeś niepoprawną nazwę użytkownika");
                }
            }
        }

    }
    
}
