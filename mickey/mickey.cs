using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp25
{
    public partial class Form1 : Form
    {
        public class mickey
        {
            public int x, y, currf;
            public List<Bitmap> imgs = new List<Bitmap>();
        }
        public class egg
        {
            public int x, y, currf;
            public List<Bitmap> imgs = new List<Bitmap>();
        }
        public class goldcoin
        {
            public int x, y;
            public Bitmap img;
        }
        void GatherMickies()
        {
            m.currf = 0;
            for (int i = 0; i < 4; i++)
            {
                a = (i+1).ToString();
                b = new Bitmap(@$"U:\MULTIMEDIA\assignemnt\mickey\{a}.bmp");
                m.imgs.Add(b);
            }
            m.x = ClientSize.Width / 2 - (m.imgs[m.currf].Width / 2);
            m.y = ClientSize.Height / 2; // - (m.imgs[m.currf].Height / 2);
            
        }
        void CreateGCs()
        {
            for (int i = 0; i < 4; i++)
            {
                goldcoin pnn = new goldcoin();
                a = (i + 1).ToString();
                b = new Bitmap(@$"U:\MULTIMEDIA\assignemnt\mickey\c{a}.bmp");
                pnn.img = b;
                if (i == 0)
                {
                    pnn.x = rr.Next(0, m.x - pnn.img.Width);
                    pnn.y = ClientSize.Height / 4;
                }
                if (i == 1)
                {
                    pnn.x = rr.Next(m.x + m.imgs[m.currf].Width + pnn.img.Width, ClientSize.Width - pnn.img.Width);
                    pnn.y = ClientSize.Height / 4;
                }
                if (i == 2)
                {
                    pnn.x = rr.Next(0, m.x - pnn.img.Width);
                    pnn.y = ClientSize.Height / 4 * 3;
                }
                if (i == 3)
                {
                    pnn.x = rr.Next(m.x + m.imgs[m.currf].Width + pnn.img.Width, ClientSize.Width - pnn.img.Width);
                    pnn.y = ClientSize.Height / 4 * 3;
                }
                goldcoins.Add(pnn);
            }
        }
        void CreateEggs()
        {
            for (int i = 0; i < 4; i++)
            {
                      egg pnn = new egg();
                      pnn.currf = 0;
                
                for (int j = 0; j < 3; j++)
                {
                    a = (j + 1).ToString();
                    b = new Bitmap(@$"U:\MULTIMEDIA\assignemnt\mickey\e{a}.bmp");
                    pnn.imgs.Add(b);
                }
                if (i == 0)
                {
                    pnn.x = rr.Next(0, m.x - pnn.imgs[pnn.currf].Width);
                    pnn.y = m.y - pnn.imgs[pnn.currf].Height;
                }
                if(i == 1)
                {
                    pnn.x = rr.Next(m.x + m.imgs[m.currf].Width + pnn.imgs[pnn.currf].Width, ClientSize.Width - pnn.imgs[pnn.currf].Width);
                    pnn.y = m.y - pnn.imgs[pnn.currf].Height;
                }
                if(i == 2)
                {
                    pnn.x = rr.Next(0, m.x - pnn.imgs[pnn.currf].Width);
                    pnn.y = m.y + ((m.imgs[m.currf].Height / 3) * 2) - pnn.imgs[pnn.currf].Height;
                }
                if(i == 3)
                {
                    pnn.x = rr.Next(m.x + m.imgs[m.currf].Width + pnn.imgs[pnn.currf].Width, ClientSize.Width - pnn.imgs[pnn.currf].Width);
                    pnn.y = m.y + ((m.imgs[m.currf].Height / 3) * 2) - pnn.imgs[pnn.currf].Height;
                }
                eggs.Add(pnn);
            }
        }
        public Bitmap b;
        public String a;
        public Random rr = new Random();
        public mickey m = new mickey();
        List<egg> eggs = new List<egg>();
        List<goldcoin> goldcoins = new List<goldcoin>();
        public Boolean inBasket = false;
        public Timer tt = new Timer();
        public Form1()
        {
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += Form1_KeyDown;
            this.MouseDown += Form1_MouseDown;
            tt.Tick += Tt_Tick;
            tt.Start();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {

            }
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            if(!inBasket)
            { 
                for (int i = 0; i < eggs.Count; i++)
                {
                    if(eggs[i].x > 0 && eggs[i].x < m.x)
                    {
                        eggs[i].x += 5;
                    }
                    if (eggs[i].x < ClientSize.Width && eggs[i].x > m.x)
                    {
                        eggs[i].x -= 5;
                    }
                    if(eggs[i].x >= m.x  && eggs[i].x <= (m.x + m.imgs[m.currf].Width))
                    {
                        inBasket = true;
                        if(eggs[i].y == m.y + ((m.imgs[m.currf].Height / 3) * 2) - eggs[i].imgs[eggs[i].currf].Height && eggs[i].x < (m.x + (m.imgs[m.currf].Width / 2)) && m.currf == 2)
                        {
                            MessageBox.Show("Congrats the egg is in the basket!!!! You get Gold Coins that create more racings eggs!, Press Space to start the race again afterwards!");
                            CreateGCs();
                            tt.Stop();
                        }
                        else if(eggs[i].y == m.y - eggs[i].imgs[eggs[i].currf].Height && eggs[i].x > m.x + m.imgs[m.currf].Width / 2  && m.currf == 1)
                        {
                            MessageBox.Show("Congrats the egg is in the basket!!!! You get Gold Coins that create more racings eggs!, Press Space to start the race again afterwards!");
                            CreateGCs();
                            tt.Stop();
                        }
                        else if (eggs[i].y == m.y - eggs[i].imgs[eggs[i].currf].Height && eggs[i].x < (m.x + (m.imgs[m.currf].Width / 2)) && m.currf == 3)
                        {
                            MessageBox.Show("Congrats the egg is in the basket!!!! You get Gold Coins that create more racings eggs!, Press Space to start the race again afterwards!");
                            CreateGCs();
                            tt.Stop();
                        }
                        else if (eggs[i].y == m.y + ((m.imgs[m.currf].Height / 3) * 2) && eggs[i].x > m.x + m.imgs[m.currf].Width / 2 && m.currf == 0)
                        {
                            MessageBox.Show("Congrats the egg is in the basket!!!! You get Gold Coins that create more racings eggs!, Press Space to start the race again afterwards!");
                            CreateGCs();
                            tt.Stop();
                        }
                    }
                }
            }
            DrawDubb(CreateGraphics());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Q)
            {
                m.currf = 3;
            }
            if (e.KeyCode == Keys.E)
            {
                m.currf = 1;
            }
            if (e.KeyCode == Keys.A)
            {
                m.currf = 2;
            }
            if (e.KeyCode == Keys.D)
            {
                m.currf = 0;
            }
            
            DrawDubb(CreateGraphics());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(CreateGraphics());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GatherMickies();
            CreateEggs();
            DrawDubb(CreateGraphics());
        }

        void DrawScene(Graphics g)
        {
            g.Clear(Color.Yellow);
            m.imgs[m.currf].MakeTransparent(m.imgs[m.currf].GetPixel(0, 0));
            g.DrawImage(m.imgs[m.currf], m.x, m.y);
            g.FillRectangle(Brushes.Red, 0, m.y, m.x, m.imgs[m.currf].Height / 3);

            g.FillRectangle(Brushes.Red, 0, m.y + ((m.imgs[m.currf].Height /3) *2), m.x, m.imgs[m.currf].Height / 3);

            g.FillRectangle(Brushes.Red, m.x + m.imgs[m.currf].Width, m.y, ClientSize.Width, m.imgs[m.currf].Height / 3);
            
            g.FillRectangle(Brushes.Red, m.x + m.imgs[m.currf].Width, m.y + ((m.imgs[m.currf].Height / 3) * 2), ClientSize.Width,  m.imgs[m.currf].Height / 3);

            for (int i = 0; i < eggs.Count; i++)
            {
                eggs[i].imgs[eggs[i].currf].MakeTransparent(eggs[i].imgs[eggs[i].currf].GetPixel(0, 0));
                g.DrawImage(eggs[i].imgs[eggs[i].currf], eggs[i].x, eggs[i].y);
            }
        }
        void DrawDubb(Graphics g2)
        {
            DrawScene(g2);
        }
    }
}