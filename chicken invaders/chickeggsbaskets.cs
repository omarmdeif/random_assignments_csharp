using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp24
{
    public partial class Form1 : Form
    {
        public class chicken
        {
            public int x, y;
            public Bitmap img;
        }
        public class basket
        {
            public int x, y;
            public Bitmap img;
            public List<egg> myeggs = new List<egg>();
        }
        public class egg
        {
            public int x, y;
            public Bitmap img;
            public Boolean hasB = false;
        }
        public Timer tt = new Timer();
        public int nextx, nexty, t1 = 10, t2 = -10;
        List<egg> eggs = new List<egg>();
        List<basket> baskets = new List<basket>();
        chicken chick = new chicken();
        void CreateChicken()
        {
            chick.img = new Bitmap(@"U:\MULTIMEDIA\assignemnt\chicken invaders\A1\1.bmp");
            chick.x = ClientSize.Width / 2 - (chick.img.Width / 2);
            chick.y = ClientSize.Height / 6;
        }
        void CreateBaskets()
        {
            Random rr = new Random();
            for(int i = 0; i < 4; i++)
            {
                nextx = rr.Next(ClientSize.Width / 4, ((ClientSize.Width / 4) * 3));
                nexty = rr.Next(ClientSize.Height / 2, ((ClientSize.Height / 6) * 5));
                if(i >= 1)
                {
                    while(nexty < (baskets[i - 1].y + baskets[i - 1].img.Height) && nexty > baskets[i-1].y)
                    {
                        nexty = rr.Next(ClientSize.Height / 2, ((ClientSize.Height / 6) * 5));
                    }
                }
                basket pnn = new basket();
                pnn.img = new Bitmap(@"U:\MULTIMEDIA\assignemnt\chicken invaders\A1\2.bmp");
                pnn.x = nextx;
                pnn.y = nexty;
                baskets.Add(pnn);
            }
        }
        public Form1()
        {
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += Form1_KeyDown;
            tt.Tick += Tt_Tick;
            tt.Start();
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i < baskets.Count; i++)
            {
                if(i%2 != 0)
                {
                    baskets[i].x += t1;
                    if(baskets[i].myeggs.Count != 0)
                    {
                        for(int j = 0; j < baskets[i].myeggs.Count; j++)
                        {
                            baskets[i].myeggs[j].x += t1;
                        }
                    }    
                    if (baskets[i].x >= ClientSize.Width - baskets[i].img.Width)
                    {
                        t1 = -10;
                    }
                    if (baskets[i].x <= baskets[i].img.Width)
                    {
                        t1 = 10;
                    }
                }
                else
                {
                    baskets[i].x += t2;
                    if (baskets[i].myeggs.Count != 0)
                    {
                        for (int j = 0; j < baskets[i].myeggs.Count; j++)
                        {
                            baskets[i].myeggs[j].x += t2;
                        }
                    }
                    if (baskets[i].x >= ClientSize.Width - baskets[i].img.Width)
                    {
                        t2 = -10;
                    }
                    if (baskets[i].x <= baskets[i].img.Width)
                    {
                        t2 = 10;
                    }
                }
            }
            for(int i = 0; i < eggs.Count; i++)
            {
                if (!eggs[i].hasB)
                {
                    if(eggs[i].y < ClientSize.Height - eggs[i].img.Height -5)
                    {
                        eggs[i].y += 5;
                    }

                }
                for(int j = 0; j < baskets.Count; j++)
                {
                    if (eggs[i].y > baskets[j].y && eggs[i].y < (baskets[j].y + ((baskets[j].img.Height / 5) *4)) && eggs[i].x >baskets[j].x + 5 && eggs[i].x < (baskets[j].x + baskets[j].img.Width - 5))
                    {
                        baskets[j].myeggs.Add(eggs[i]);
                        eggs.RemoveAt(i);
                    }
                }
            }
            DrawDubb(CreateGraphics());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                egg s = new egg();
                s.y = chick.y + chick.img.Height;
                s.x = chick.x + (chick.img.Width / 2);
                s.img = new Bitmap(@"U:\MULTIMEDIA\assignemnt\chicken invaders\A1\3.bmp");
                eggs.Add(s);
            }
            if(e.KeyCode == Keys.W)
            {
                chick.y -= 5;
            }
            if (e.KeyCode == Keys.D)
            {
                chick.x += 5;
            }
            if (e.KeyCode == Keys.S)
            {
                chick.y += 5;
            }
            if (e.KeyCode == Keys.A)
            {
                chick.x -= 5;
            }
            DrawDubb(CreateGraphics());
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(CreateGraphics());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateChicken();
            CreateBaskets();
            DrawDubb(CreateGraphics());
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);
            chick.img.MakeTransparent(chick.img.GetPixel(0, 0));
            g.DrawImage(chick.img, chick.x, chick.y);
            for(int i = 0; i < baskets.Count; i++)
            {
                baskets[i].img.MakeTransparent(baskets[i].img.GetPixel(0, 0));
                g.DrawImage(baskets[i].img, baskets[i].x, baskets[i].y);
                if(baskets[i].myeggs.Count != 0)
                {
                    for(int j = 0; j < baskets[i].myeggs.Count; j++)
                    {
                        g.DrawImage(baskets[i].myeggs[j].img, baskets[i].myeggs[j].x, baskets[i].myeggs[j].y);
                    }
                }
            }
            for(int i = 0; i< eggs.Count; i++)
            {
                g.DrawImage(eggs[i].img, eggs[i].x, eggs[i].y);
            }

        }
        void DrawDubb(Graphics g2)
        {
            DrawScene(g2);
        }
    }
}