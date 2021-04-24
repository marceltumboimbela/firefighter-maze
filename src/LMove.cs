using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

//kelas yang menyimpan method dan atribut pergerakan label
namespace FireFighter
{
    class LMove
    {
        MainForm form;
        Point P;
        Label label4;
        //menyimpan sekuens gambar untuk gerakan ke kiri
        List<Image> Left = new List<Image>();
        //menyimpan sekuens gambar untuk gerakan ke kanan
        List<Image> Right = new List<Image>();
        //menyimpan sekuens gambar untuk gerakan ke atas
        List<Image> Up = new List<Image>();
        //menyimpan sekuens gambar untuk gerakan ke bawah
        List<Image> Down = new List<Image>();
        int snum;

        public LMove(MainForm a)
        {
            int j;
            form = a;
            
            this.label4 = form.label4;
            for (j = 1; j < 11; j++)
            {
                Right.Add(Image.FromFile("images\\kanan" + j + ".png"));
                Left.Add(Image.FromFile("images\\kiri" + j + ".png"));
                Up.Add(Image.FromFile("images\\atas" + j + ".png"));
                Down.Add(Image.FromFile("images\\bawah" + j + ".png"));
            }
        }

        public void init(Point D)
        {
            P = D;
            snum = 0;
        }

        //fungsi untuk mengambil arah dari point awal ke point akhir
        public int GetDir(Point awal, Point akhir)
        {
            int i = 0;
            if (akhir.X > awal.X) i = 0;
            else if (akhir.X < awal.X) i = 1;
            else if (akhir.Y > awal.Y) i = 2;
            else if (akhir.Y < awal.Y) i = 3;
            return i;
        }

       
        public void nexts()
        {
            snum += 1;
            if (snum > 9) snum = 0;
        }

        //prosedur untuk menjalankan animasi sprite pada DFS dengan speed tertentu
        public void MoveAnimation(int speed) 
        {
            /*while (!form.me) ;
            form.me = false;*/
            int i, a, b;

            List<Image> sprite =  null;
            i = GetDir(new Point((label4.Location.X + form.panel1.HorizontalScroll.Value), (label4.Location.Y + form.panel1.VerticalScroll.Value)), new Point(P.X * 30, P.Y * 30));
            if (i == 0) sprite = Right;
            else if (i == 1) sprite = Left;
            else if (i == 2) sprite = Down;
            else if (i == 3) sprite = Up;

            int x = ((label4.Location.X + form.panel1.HorizontalScroll.Value) / 10);
            int y = ((label4.Location.Y + form.panel1.VerticalScroll.Value)/ 10);
            for (i = 0; i < 10; i++)
            {
                this.label4.Image = sprite.ElementAt(snum);
                nexts();
                a = (P.X * 30) - ((9 - i) * (3 * P.X - x));
                b = (P.Y * 30) - ((9 - i) * (3 * P.Y - y));
                this.label4.Location = new Point(a - form.panel1.HorizontalScroll.Value, b - form.panel1.VerticalScroll.Value);
                form.panel1.AutoScrollPosition = new Point(a - 250, b - 250);
                Thread.Sleep(speed);
                //Thread.Sleep(0);
                Application.DoEvents();
            }
            /*form.me = true;*/
        }

        //prosedur untuk menggerakan animasi sprite pada BFS dengan kecepatan tertentu
        public void JumpAnimation(int speed)
        {
            //while (!form.me) ;
            //form.me = false;
            int i, a, b;
            List<Image> sprite = null;
            int x = ((label4.Location.X + form.panel1.HorizontalScroll.Value));
            int y = ((label4.Location.Y + form.panel1.VerticalScroll.Value));
            for (i = 0; i < 30; i++)
            {
                sprite = Down;
                this.label4.Image = sprite.ElementAt(snum);
                if (i >= 14)
                {
                    Thread.Sleep(speed);
                    Application.DoEvents();
                    a = P.X * 30;
                    b = P.Y * 30;
                }
                else
                {
                    Thread.Sleep(speed/5);
                    Application.DoEvents();
                    a = x;
                    b = y;
                    if (i % 4 == 0) a -= 2;
                    else if (i % 4 == 1) b -= 2;
                    else if (i % 4 == 2) a += 2;
                    else if (i % 4 == 3) b += 2;
                }
                this.label4.Location = new Point(a - form.panel1.HorizontalScroll.Value, b - form.panel1.VerticalScroll.Value);
                form.panel1.AutoScrollPosition = new Point(a - 250, b - 250);
                
            }
            /*
            a = P.X * 30;
            b = P.Y * 30;
            this.label4.Location = new Point(a - form.panel1.HorizontalScroll.Value, b - form.panel1.VerticalScroll.Value);
            System.Threading.Thread.Sleep(10);
            Application.DoEvents();
             */

            //form.me = true;
        }
    }
}
