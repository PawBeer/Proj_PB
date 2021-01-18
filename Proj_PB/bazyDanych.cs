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
    public partial class bazyDanych : Form
    {
        public bazyDanych(string nazwa, string tab)
        {
            InitializeComponent();
       
            this.UserName = nazwa;
            this.tab = tab;
            InitializeComponent();
            
            string[] komis = { "komis Kraków", "komis Warszawa" };
            comboBox1.Items.AddRange(komis);
            comboBox1.SelectedIndex = 0;

        }
        
        public string tabKom;
        private string UserName { get; set; }
        private string tab { get; set; }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabKom = comboBox1.GetItemText(comboBox1.SelectedValue);

                switch (tabKom)
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

            if (UserName.Equals("admin"))
            {
                Form2 a1 = new Form2(UserName, tab);
                a1.ShowDialog();
            }
            else
            {
                Form1 w1 = new Form1(UserName, tab);
                w1.ShowDialog();
            }
            this.Close();

        }
        }
}
