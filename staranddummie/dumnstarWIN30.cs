using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp30
{
    public partial class Form1 : Form
    {
        public class ids
        {
            public int x, y;
            public Bitmap img;
        }
        public Bitmap off;
        public Boolean laser = false;
        public int ct = 0, next = 0, t = 5;
        public Random r = new Random();
        public Timer tt = new Timer();
        public ids dum = new ids();
        List<ids> stars = new List<ids>();
        public Form1()
        {
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            tt.Tick += Tt_Tick;
            this.KeyUp += Form1_KeyUp;
            tt.Start();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                laser = false;
            }
        }

        private void Tt_Tick(object sender, EventArgs e)
        {

            dum.x+= t;
            if (t < -5)
                t += 5;
            else if (t > 5)
                t -= 5;

            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].y += 10;
                if((dum.x + dum.img.Width/2) > stars[i].x && (dum.x + dum.img.Width / 2) < (stars[i].x + stars[i].img.Width) && laser)
                {
                    stars.RemoveAt(i);
                }
            }

            if (ct == next)
                createstar();
            else if (ct == 50)
                ct = 0;
            else
                ct++;

            DrawDubb(CreateGraphics());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    t += 10;
                    break;
                case Keys.Left:
                    t -= 10;
                    break;
                case Keys.Space:
                    laser = true;
                    break;
                default:
                    laser = false;
                    break;
            }
            DrawDubb(CreateGraphics());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            DrawDubb(CreateGraphics());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            createdummie();
            DrawDubb(CreateGraphics());
        }
        void createdummie()
        {
            dum.img = new Bitmap(@"U:\MULTIMEDIA\assignemnt\staranddummie\dummie.bmp");
            dum.x = ClientSize.Width/2 - (dum.img.Width / 2);
            dum.y = ClientSize.Height - dum.img.Height;
        }
        void createstar()
        {
            ids pnn = new ids();
            pnn.img = new Bitmap(@"U:\MULTIMEDIA\assignemnt\staranddummie\star.bmp");
            pnn.x = r.Next(0, ClientSize.Width - pnn.img.Width);
            pnn.y = 0;
            stars.Add(pnn);
            next = r.Next(15, 50);
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);
            dum.img.MakeTransparent(dum.img.GetPixel(0, 0));
            g.DrawImage(dum.img, dum.x, dum.y);
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].img.MakeTransparent(stars[i].img.GetPixel(0, 0));
                g.DrawImage(stars[i].img, stars[i].x, stars[i].y);
            }
            if (laser)
            {
                Pen lime = new Pen(Color.Lime, 3);
                PointF st = new PointF(dum.x + dum.img.Width / 2, dum.y);
                PointF en = new PointF(dum.x + dum.img.Width / 2, 0);
                g.DrawLine(lime, st, en);
            }
        }
    }
}
