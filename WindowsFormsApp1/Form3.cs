using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public string data = "";
        public bool success = false;

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data = textBox1.Text;

            if (data != textBox2.Text)
            {
                label4.Visible = false;
                label3.Visible = true;
            }

            else if (data.Length < 8)
            {
                label3.Visible = false;
                label4.Visible = true;
            }

            else
            {
                success = true;
                Close();
            }
        }

        private void enterIn(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1_Click(null, null);
                e.Handled = true;
            }
        }
    }
}