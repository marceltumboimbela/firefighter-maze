using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//kelas firstform digunakan untuk membuat mainmenu
//lagu dijalankan pada saat firstform diinisialisasi
namespace FireFighter
{   
    public partial class FirstForm : Form
    {
        private MainForm a;
        private Sound ws;

        public FirstForm()
        {
            InitializeComponent();
            //a = f1;
            //a.Hide();
            ws = new Sound();
            ws.Open("Resources\\X5.mp3");
            ws.Play(true);
        }

        public void StopPlay()
        {
            ws.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            a = new MainForm(this);
            a.Show();
            a.newToolStripMenuItem_Click(sender,e);
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            a = new MainForm(this);
            a.openToolStripMenuItem_Click(sender,e);
            a.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }   
    }
}
