using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//kelas newform digunakan membuat form yang meminta user untuk memasukkan nilai ukuran maze yang diinginkan
namespace FireFighter
{
    public partial class NewForm : Form
    {
        public MainForm forma;

        public NewForm(MainForm a)
        {
            InitializeComponent();
            forma = a;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i,x,y;
            String S;
            Boolean intc = true;
            S = textBox1.Text;
            if (S.Equals(""))
            {
                intc = false;
                goto intfalse;
            }
            for (i = 0; i < S.Length; i++) if (S[i] < '0' || S[i] > '9') { intc = false; goto intfalse; }
            S = textBox2.Text;
            if (S.Equals(""))
            {
                intc = false;
                goto intfalse;
            }
            for (i = 0; i < S.Length; i++) if (S[i] < '0' || S[i] > '9') { intc = false; goto intfalse; }
            intfalse:
            if (intc)
            {
                x = int.Parse(textBox1.Text);
                y = int.Parse(textBox2.Text);
                if (x < 1 || y < 1)
                {
                    intc = false;
                    goto intfalse;
                }
                forma.newfield(x, y);
                forma.initlabel();
                this.Dispose();
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Masukkan angka minimal 1 pada masing-masing box", "FireFighter", MessageBoxButtons.OK);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
