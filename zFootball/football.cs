using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp23
{
    public partial class Form1 : Form
    {
        public int s, l, ct = 0;
        public String j = null;
        public Bitmap a;
        public class actor
        {
            public int x, y;
            public List<Bitmap> imglist = new List<Bitmap>();
            public int currf;
            public Ball myball = null;
            
        }
        public class Ball
        {
            public int x = 0, y = 0;
            public Bitmap img;
            
        }
        actor men = new actor();
        List<Ball> balls = new List<Ball>();
        void createactor()
        {
            actor pnn = new actor();
            pnn.x = (ClientSize.Width / 3) + ((ClientSize.Width / 4) / 2);
            pnn.y = (ClientSize.Height / 5) + 10;
            for (int i = 0; i < 8; i++)
            {
                j = $"{i+1}";
                a = new Bitmap(@"U:\MULTIMEDIA\assignemnt\football\A2\w" +j+".bmp");
                pnn.imglist.Add(a);
            }
            men = pnn;
        }
        void createballs()
        {
            
            s = 0; l = 0;
            for(int i = 0; i < 20; i++)
            {
                Ball pnn = new Ball();
                pnn.x = ClientSize.Width / 2 - 145 + (l*25);
                pnn.y = ClientSize.Height - (ClientSize.Height / 8) + 10 + (s * 30);
                if(pnn.x > (ClientSize.Width / 2 + 125))
                {
                    l = 0;
                    s++;
                }
                pnn.img = new Bitmap(@"U:\MULTIMEDIA\assignemnt\football\A2\ball2.bmp");
                balls.Add(pnn);
                l++;
            }
        }
        public Form1()
        {
            this.Load += Form1_Load;
            this.KeyDown += Form1_KeyDown;
            this.Paint += Form1_Paint;
            this.WindowState = FormWindowState.Maximized;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(CreateGraphics());
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                if(men.x >(ClientSize.Width /2 -150) && men.x < (ClientSize.Width / 2 + 150)
                    && men.y >ClientSize.Height - (ClientSize.Height /8))
                {
                    men.myball = new Ball();
                    men.myball = balls[balls.Count - 1 - ct];
                    men.myball.x = men.x + men.imglist[0].Width - men.imglist[0].Width / 5;
                    men.myball.y = men.y + men.imglist[0].Height;
                    balls.RemoveAt(balls.Count - 1);
                    ct++;
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                if(men.x > (ClientSize.Width / 2 - 150) && men.x < (ClientSize.Width / 2 + 150)
                    && men.y < ClientSize.Height / 8)
                {
                    if(men.myball.x != 0)
                    {
                        balls.Add(men.myball);
                        men.myball = null;
                    }
                }
            }
            if(e.KeyCode == Keys.Up)
            {
                men.y -= 5;
                if(men.myball != null)
                {
                    men.myball.y -= 5;
                }
                men.currf += 1;
                if(men.currf == 8)
                {
                    men.currf = 0;
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                men.y += 5;
                if (men.myball != null)
                {
                    men.myball.y += 5;
                }
                men.currf += 1;
                if (men.currf == 8)
                {
                    men.currf = 0;
                }
            }
            if(e.KeyCode == Keys.Left)
            {
                men.x -= 5;
                if (men.myball != null)
                {
                    men.myball.x -= 5;
                }
                men.currf += 1;
                if (men.currf == 8)
                {
                    men.currf = 0;
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                men.x += 5;
                if (men.myball != null)
                {
                    men.myball.x += 5;
                }
                men.currf += 1;
                if (men.currf == 8)
                {
                    men.currf = 0;
                }
            }
            DrawDubb(CreateGraphics());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            createactor();
            createballs();
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Yellow);
            g.FillRectangle(Brushes.Red, ClientSize.Width / 2 - 150, 0, 300, ClientSize.Height / 8);
            g.FillRectangle(Brushes.Green, ClientSize.Width / 2 - 150, ClientSize.Height - (ClientSize.Height/8), 300, ClientSize.Height / 8);
            men.imglist[men.currf].MakeTransparent(men.imglist[men.currf].GetPixel(0, 0));
            g.DrawImage(men.imglist[men.currf], men.x, men.y);
            if(men.myball != null)
            {
                men.myball.img.MakeTransparent(men.myball.img.GetPixel(0, 0));
                g.DrawImage(men.myball.img, men.myball.x, men.myball.y);
            }
            for(int i = 0; i < balls.Count; i++)
            {
                balls[i].img.MakeTransparent(balls[i].img.GetPixel(0, 0));
                g.DrawImage(balls[i].img, balls[i].x, balls[i].y);
            }
        }
        void DrawDubb(Graphics g2)
        {
            DrawScene(g2);
        }
    }
}