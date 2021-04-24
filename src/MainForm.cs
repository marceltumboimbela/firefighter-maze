using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//kelas yang merupakan interface utama dari program
namespace FireFighter
{
    public partial class MainForm : Form
    {
        private static Engine X;
        //list yang menyimpan label yang membentuk dinding dan jalan
        public List<Label> Area;
        private FirstForm f3;
        public Label LabelX, LabelY;

        public  MainForm(FirstForm f)
        {
            InitializeComponent();
            LabelX = new Label();
            LabelY = new Label();
            X = new Engine(this);
            Area = new List<System.Windows.Forms.Label>();
            this.Disposed += new EventHandler(Close_Program);
            f3 = f;
            me = true;
            comboBox1.SelectedIndex = 2;
        }

        //prosedur untuk membuat field baru
        public void newfield(int x, int y)
        {
            X.newfield(x, y);
        }

        //prosedur untuk membuat start dan finish kembali ke posisi awal
        private void targetimage()
        {
            label4.Visible = true;
            label5.Visible = true;
            label4.Location = new Point(X.start.X * 30, X.start.Y * 30);
            label5.Location = new Point(X.finish.X * 30, X.finish.Y * 30);
            
        }

        //prosedur untuk membuat inisialisasi label
        public void initlabel()
        {
            int i, j;
            EventHandler eh;
            Label label3;

            for (j = 0; j < X.sizeY; j++)
            {
                for (i = 0; i < X.sizeX; i++)
                {
                    label3 = new System.Windows.Forms.Label();
                    if (X.field[i + j * X.sizeX] == 0) label3.BackColor = Color.Gold;//label3.Image = Image.FromFile("C:\\Users\\tetra\\Documents\\Visual Studio 2008\\Projects\\FireFighter\\WindowsFormsApplication1\\images\\fence_floor copy.png");
                    else if (X.field[i + j * X.sizeX] == 1) label3.BackColor = Color.Brown;//label3.Image = Image.FromFile("C:\\Users\\tetra\\Documents\\Visual Studio 2008\\Projects\\FireFighter\\WindowsFormsApplication1\\images\\fence_wall copy.png");
                        label3.Location = new System.Drawing.Point(30 * i, 30 * j);
                    label3.Size = new System.Drawing.Size(30, 30);
                    panel1.Controls.Add(label3);
                    eh = new System.EventHandler(this.label2_Click);
                    label3.Click += eh;
                    Area.Add(label3);
                }
            }
            targetimage();
            if (X.sizeX < 17)
            {
                LabelX = new System.Windows.Forms.Label();
                LabelX.Location = new System.Drawing.Point(30 * X.sizeX, 0);
                LabelX.Size = new System.Drawing.Size(511 - 30 * X.sizeX, 395);
                LabelX.BackColor = Color.DarkGoldenrod;
                panel1.Controls.Add(LabelX);
            }
            if (X.sizeY < 13)
            {
                LabelY = new System.Windows.Forms.Label();
                LabelY.Location = new System.Drawing.Point(0, 30 * X.sizeY);
                LabelY.Size = new System.Drawing.Size(511, 395 - 30 * X.sizeY);
                LabelY.BackColor = Color.DarkGoldenrod;
                panel1.Controls.Add(LabelY);
            }
        }

        //prosedur untuk menghancurkan label yang telah diinisialisasi
        public void destroy()
        {
            int i;
            Label labels;
            for (i = 0; i < X.sizeX * X.sizeY; i++)
            {
                labels = Area.ElementAt(0);
                Area.RemoveAt(0);
                labels.Dispose();
            }
            LabelX.Dispose();
            LabelY.Dispose();
        }

        public void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(X.mode != 3)new NewForm(this).Show();
        }

        public void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (X.mode != 3)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "All files (*.*)|*.*|dot a files (*.a)|*.a";
                dialog.FilterIndex = 2;
                destroy();
                if (dialog.ShowDialog() == DialogResult.OK) X.fileread(dialog.FileName);
                initlabel();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (X.mode != 3)
            {
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "All files (*.*)|*.*|dot a files (*.a)|*.a";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;


                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        X.filesave(myStream);
                        myStream.Close();
                    }
                }
            }
        }
        
        private void label2_Click(object sender, EventArgs e)
        {
            if (X.mode != 3)
            {
                X.editsf((Label)sender);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
                DialogResult result1 = MessageBox.Show("Keluar?", "FireFighter", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    Close_Program(sender, e);
                }
        }

        private void Close_Program(object sender, EventArgs e)
        {
            f3.StopPlay();
            f3.Dispose();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            if (X.mode == 0)
            {
                X.mode = 1;
                //button1.Text = "edit mode";
                label1.Text = "Build\nMode :\nStart";
            }
            else if(X.mode == 1)
            {
                X.mode = 0;
                //button1.Text = "change start";
                label1.Text = "Build\nMode :\nWall";
            }
            else if(X.mode == 2)
            {
                X.mode = 1;
                //button1.Text = "edit mode";
                //button2.Text = "change finish";
                label1.Text = "Build\nMode :\nStart";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (X.mode == 0)
            {
                X.mode = 2;
                //button2.Text = "edit mode";
                label1.Text = "Build\nMode :\nFinish";
            }
            else if (X.mode == 1)
            {
                X.mode = 2;
                //button1.Text = "change start";
                //button2.Text = "edit mode";
                label1.Text = "Build\nMode :\nFinish";
            }
            else if (X.mode == 2)
            {
                X.mode = 0;
                //button2.Text = "change finish";
                label1.Text = "Build\nMode :\nWall";
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (X.mode != 3)
            {   
                int i;
                for (i = 0; i < X.sizeX * X.sizeY; i++) if (Area.ElementAt(i).BackColor != Color.Brown) Area.ElementAt(i).BackColor = Color.Gold;
                label1.Text = "Solve\nMode :\nDFS";
                X.mode = 3;
                if (X.SolveMaze(X.start))
                {
                    DialogResult result1 = MessageBox.Show("Api Ditemukan!", "FireFighter", MessageBoxButtons.OK);
                }
                else
                {
                    DialogResult result1 = MessageBox.Show("Api tidak ditemukan", "FireFighter", MessageBoxButtons.OK);
                }
                X.clear_field();
                label4.Location = new Point(X.start.X * 30 - panel1.HorizontalScroll.Value, X.start.Y * 30 - panel1.VerticalScroll.Value);
                X.mode = 0;
                label1.Text = "Build\nMode :\nWall";
                label4.Image = global::FireFighter.Properties.Resources.save;
            }
        }

        public bool me;

        private void button4_Click(object sender, EventArgs e)
        {
            Point P = new Point();
            if (X.mode != 3)
            {
                int i;
                for (i = 0; i < X.sizeX * X.sizeY; i++) if (Area.ElementAt(i).BackColor != Color.Brown) Area.ElementAt(i).BackColor = Color.Gold;
                label1.Text = "Solve\nMode :\nBFS";
                X.mode = 3;
                P.X = X.start.X;
                P.Y = X.start.Y;
                if (X.BFS(P))
                {
                    DialogResult result1 = MessageBox.Show("Api Ditemukan!", "FireFighter", MessageBoxButtons.OK);
                }
                else
                {
                    DialogResult result1 = MessageBox.Show("Api tidak ditemukan", "FireFighter", MessageBoxButtons.OK);
                }
                X.clear_field();
                label4.Location = new Point(X.start.X * 30 - panel1.HorizontalScroll.Value, X.start.Y * 30 - panel1.VerticalScroll.Value);
                X.mode = 0;
                label1.Text = "Build\nMode :\nWall";
                label4.Image = global::FireFighter.Properties.Resources.save;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {   
            int i = comboBox1.SelectedIndex;
            if (i == 0)
            {
                X.speed = 200;
            }
            else if (i == 1)
            {
                X.speed = 100;
            }
            else if (i == 2)
            {
                X.speed = 50;
            }
            else if (i == 3)
            {
                X.speed = 25;
            }
            else if (i == 4)
            {
                X.speed = 0;
            }
        }

        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().Show();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cara menjalankan simulasi program FireFighter :\n1) Mulai permainan dengan menjalankan aplikasi FireFighter.exe\n2) Pada menu awal pilih start untuk memulai program simulasi, load untuk menge-load map, dan quit jika anda tidak ingin keluar dari program.\n3) Pada saat pindah dari menu start ke menu simulasi, apabila anda memilih \"Start Game\" maka anda akan diminta untuk memasukkan ukuran panjang dan lebar peta. Jika anda memilih \"Load Game\", maka peta yang anda load akan langsung digambar di menu simulasi.\n4) Pada menu simulasi, anda dapat memulai membuat peta dengan panjang dan lebar yang telah anda masukkan nilainya sebelumnya, anda dapat mengeklik layar peta yang kosong untuk membuat dinding, dan mengeklik dinding untuk menghapusnya. Anda juga dapat memindahkan posisi start dan finish dengan menekan tombol \"place start\" dan \"place finish\". Apabila anda ingin kembali ke build mode, anda hanya perlu menekan tombol \"place start\"(apabila anda dalam mode \"place start\") atau \"place finish\" (apabila anda dalam mode \"place finish\"). Anda juga dapat langsung menge-load map dengan memilih menu>load.\n5) Setelah anda membuat/menge-load map yang anda inginkan, anda dapat langsung memulai simulasi dengan BFS atau DFS dengan menekan tombol \"BFS\" atau \"DFS\". Map tidak dapat diganti-ganti selama simulasi dijalankan.Selama simulasi, jalan berwarna kuning adalah jalan yang belum dilewati, jalan berwarna biru adalah jalan yang sudah dilewati dan benar, sedang jalan berwarna abu-abu adalah jalan yang sudah dilewati tetapi salah.\n6) Jika api sudah ditemukan, anda dapat mengganti map dan memulai lagi simulasinya.");
        }

        
    }
}
