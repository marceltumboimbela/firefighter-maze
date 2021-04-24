using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

//kelas untuk menyimpan method dan atribut dari engine game
namespace FireFighter
{
    //class yang mengatur jalannya simulasi
    class Engine
    {
        MainForm form;
        public int[] field;//Menyimpan bentuk dari maze
        public int sizeX;//Menyimpan ukuran X field
        public int sizeY;//Menyimpan ukuran Y field
        public Point start;//Menyimpan posisi start
        public Point finish;//Menyimpan posisi finish
        public int mode;//0 = edit,1 = startedit,2 = finishedit,3 = solvemaze
        public int speed;
        LMove T;

        public Engine(MainForm f)
        {
            form = f;
            form.label4.Visible = false;
            form.label5.Visible = false;
            mode = 0;
            speed = 50;
            T = new LMove(form);
        }

        //prosedur untuk membuat map baru
        public void newfield(int x, int y)
        {
            int i;
            form.destroy();
            sizeX = x;
            sizeY = y;
            field = new int[sizeX * sizeY];
            for (i = 0; i < sizeX * sizeY; i++) field[i] = 0;
            start = new Point(0, 0);
            finish = new Point(0, 0);
        }

        //prosedur untuk membaca file eksternal map
        public void fileread(String file)
        {
            int i, j;
            String R;
            StreamReader tr = new StreamReader(file);
            R = tr.ReadLine();
            sizeX = R.Length;
            sizeY = 1;
            while (!tr.EndOfStream)
            {
                tr.ReadLine();
                sizeY++;
            }
            tr.Close();
            field = new int[sizeX * sizeY];
            tr = new StreamReader(file);
            for (j = 0; j < sizeY; j++)
            {
                R = tr.ReadLine();
                for (i = 0; i < sizeX; i++)
                {
                    if (R[i] == 'S')
                    {
                        start.X = i;
                        start.Y = j;
                    }
                    if (R[i] == 'F')
                    {
                        finish.X = i;
                        finish.Y = j;
                    }
                    if (R[i] == '1') field[i + j * sizeX] = 1;
                    else field[i + j * sizeX] = 0;
                }
            }
            tr.Close();
        }

        //prosedur untuk mengesave map yang dibuat menjadi file eksternal
        public void filesave(Stream myStream)
        {
            int i, j;
            StreamWriter w = new StreamWriter(myStream);
            String R = "";
            for (j = 0; j < sizeY; j++)
            {
                for (i = 0; i < sizeX; i++)
                {
                    if (start.X == i && start.Y == j) R += 'S';
                    else if (finish.X == i && finish.Y == j) R += 'F';
                    else R += field[i + j * sizeX];
                }
                if (j != sizeY - 1) R += '\n';
            }
            w.Write(R);
            w.Close();
        }

        //prosedur yang mengatur edit start dan finish
        public void editsf(Label X)
        {
            int x, y;
            int i = form.Area.IndexOf(X);
            x = i % sizeX;
            y = i / sizeX;
            if (mode == 1)
            {
                if (field[x + y * sizeX] != 0)
                {
                    DialogResult result1 = MessageBox.Show("Start tidak dapat diletakkan pada dinding", "FireFighter", MessageBoxButtons.OK);
                }
                else form.label4.Location = new Point(x * 30 - form.panel1.HorizontalScroll.Value, y * 30 - form.panel1.VerticalScroll.Value);
                start.X = x;
                start.Y = y;
            }
            else if (mode == 2)
            {
                if (field[x + y * sizeX] != 0)
                {
                    DialogResult result1 = MessageBox.Show("Finish tidak dapat diletakkan pada dinding", "FireFighter", MessageBoxButtons.OK);
                }
                else form.label5.Location = new Point(x * 30 - form.panel1.HorizontalScroll.Value, y * 30 - form.panel1.VerticalScroll.Value);
                finish.X = x;
                finish.Y = y;
            }
            else if (mode == 0)
            {
                if (field[i] == 0) X.BackColor = Color.Brown;
                else X.BackColor = Color.Gold;
                field[i] = 1 - field[i];
            }
        }

        //algoritma DFS dengan backtracking
        public bool SolveMaze(Point P)
        {
            int i;
            if (P.Equals(finish)) return true;
            else
            {
                field[P.X + P.Y * sizeX] = 2;
                for (i = 0; i < 4; i++)
                {
                    form.Area.ElementAt(P.X + P.Y * sizeX).BackColor = Color.Aquamarine;
                    if (i == 0) P.X += 1;
                    else if (i == 1) P.X -= 1;
                    else if (i == 2) P.Y += 1;
                    else if (i == 3) P.Y -= 1;
                    if (P.X >= 0 && P.X < sizeX && P.Y >= 0 && P.Y < sizeY && field[P.X + P.Y * sizeX] == 0)
                    {
                        move_to(P);
                        if (SolveMaze(P))
                        {
                            return true;
                        }
                        else
                        {
                            form.Area.ElementAt(P.X + P.Y * sizeX).BackColor = Color.Gray;
                            if (i == 0) P.X -= 1;
                            else if (i == 1) P.X += 1;
                            else if (i == 2) P.Y -= 1;
                            else if (i == 3) P.Y += 1;
                            move_to(P);
                        }
                    }
                    else
                    {
                        if (i == 0) P.X -= 1;
                        else if (i == 1) P.X += 1;
                        else if (i == 2) P.Y -= 1;
                        else if (i == 3) P.Y += 1;
                        form.Area.ElementAt(P.X + P.Y * sizeX).BackColor = Color.Gold;
                    }
                }
                return false;
            }
        }

        //algoritma BFS
        public bool BFS(Point v)
        {
            Point w = new Point();
            Point nv;
            int i;
            Queue<Point> q = new Queue<Point>();
            bool found = false;
            field[v.X + v.Y * sizeX] = 2;
            jump_to(v);
            /*System.Threading.Thread.Sleep(300);
            Application.DoEvents();*/
            q.Enqueue(v);
            while (q.Count > 0 && !found)
            {
                nv = q.Dequeue();
                for (i = 0; i < 4; i++)
                {
                    w.X = nv.X;
                    w.Y = nv.Y;
                    if (i == 0) w.X += 1;
                    else if (i == 1) w.X -= 1;
                    else if (i == 2) w.Y += 1;
                    else if (i == 3) w.Y -= 1;

                    if (w.X >= 0 && w.X < sizeX && w.Y >= 0 && w.Y < sizeY && field[w.X + w.Y * sizeX] == 0)
                    {
                        q.Enqueue(w);
                        form.Area.ElementAt((form.label4.Location.X-form.panel1.HorizontalScroll.Value)/30 + (form.label4.Location.Y-form.panel1.VerticalScroll.Value)/30 * sizeX).BackColor = Color.Aquamarine;
                        field[w.X + w.Y * sizeX] = 2;
                        jump_to(w);
                        if (w.Equals(finish))
                        {
                            found = true;
                            break;
                        }                        
                    }
                }
            }
            return found;
        }

        public void move_to(Point P)
        {
            T.init(P);
            T.MoveAnimation(speed);
        }

        public void jump_to(Point P)
        {
            T.init(P);
            T.JumpAnimation(speed);
        }

        //prosedur untuk menghapus map yang ada di window
        public void clear_field()
        {
            int i;
            for (i = 0; i < sizeX * sizeY; i++) if (field[i] == 2) field[i] = 0;
        }

    }
}
