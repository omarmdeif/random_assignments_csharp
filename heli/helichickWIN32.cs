using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp32
{
    public partial class Form1 : Form
    {
       public class plat
       {
            public int x, y;
            public Bitmap img;
            public Boolean hasChick = true;
       }
        public class helio
        {
            public int x, y;
            public Bitmap img;
            public List<chick> chicks = new List<chick>();
        }
        public class chick
        {
            public int x, y;
            public List<Bitmap> imgs = new List<Bitmap>();
            public Boolean isCap = false;
        }
        public List<plat> plats = new List<plat>();
        public helio heli = new helio();
        public List<chick> chicks = new List<chick>(); 
        public Timer tt = new Timer();
        public int h1 = 10, c = 0;
        public int[] p = { 10, 10, 10, 10 };
        public Bitmap off;
        public Boolean ri = false, le = false, floating = false;
        public Form1()
        {
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            tt.Tick += Tt_Tick;
            tt.Start();
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            if (heli.y + heli.img.Height < ClientSize.Height / 10 * 8 && !floating)
            {
                heli.y += h1;
                for (int i = 0; i < heli.chicks.Count; i++)
                {
                    heli.chicks[i].y += h1;
                }
            }
            else if (heli.y + heli.img.Height > ClientSize.Height / 10 * 8)
            {
                tt.Stop();
            }
            if (floating && ri)
            {
                heli.y -= h1;
                heli.x += h1;
                for (int i = 0; i < heli.chicks.Count; i++)
                {
                    heli.chicks[i].y -= h1;
                    heli.chicks[i].x += h1;
                }
            }
            else if(floating && le)
            {
                heli.y -= h1;
                heli.x -= h1;
                for (int i = 0; i < heli.chicks.Count; i++)
                {
                    heli.chicks[i].y -= h1;
                    heli.chicks[i].x -= h1;
                }
            }
            else if (floating)
            {
                heli.y -= h1;
                for (int i = 0; i < heli.chicks.Count; i++)
                {
                    heli.chicks[i].y -= h1;
                }
            }
            for (int i = 1; i < plats.Count - 1; i++)
            {
                if(plats[i].x <= plats[0].x + plats[0].img.Width + 5)
                {
                    p[i - 1] *= -1;
                }
                if(plats[i].x >= plats[5].x - plats[5].img.Width)
                {
                    p[i - 1] *= -1;
                }
                if(i % 2 == 0)
                {
                    plats[i].x += p[i - 1];
                    if (plats[i].hasChick)
                    {
                        chicks[c].x += p[i - 1];
                        if (heli.x + heli.img.Width / 2 >= plats[i].x && heli.x + heli.img.Width / 2 <= plats[i].x + plats[i].img.Width
                  && heli.y + heli.img.Height >= plats[i].y - chicks[c].imgs[0].Height - 100)
                        {
                            chicks[c].isCap = true;
                        }
                        else
                        {
                            chicks[c].isCap = false;
                        }
                        c += 1;
                    }
                }
                else
                {
                    plats[i].x -= p[i - 1];
                    if (plats[i].hasChick)
                    {
                        chicks[c].x -= p[i - 1];
                        if (heli.x + heli.img.Width / 2 >= plats[i].x && heli.x + heli.img.Width / 2 <= plats[i].x + plats[i].img.Width
                  && heli.y + heli.img.Height >= plats[i].y - chicks[c].imgs[0].Height - 100)
                        {
                            chicks[c].isCap = true;
                        }
                        else
                        {
                            chicks[c].isCap = false;
                        }
                        c += 1;
                    }
                }
               
            }
            c = 0;
            DrawDubb(CreateGraphics());
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                floating = false;
            }
            if(e.KeyCode == Keys.Right)
            {
                ri = false;
            }
            if(e.KeyCode == Keys.Left)
            {
                le = false;
            }
            DrawDubb(CreateGraphics());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    floating = true;
                    break;
                case Keys.Right:
                    ri = true;
                    break;
                case Keys.Left:
                    le = true;
                    break;
                case Keys.Space:
                    for (int i = 0; i < chicks.Count; i++)
                    {
                        if(heli.x + heli.img.Width / 2 >= plats[i+1].x && heli.x + heli.img.Width / 2 <= plats[i+1].x + plats[i+1].img.Width
                    && heli.y + heli.img.Height >= plats[i+1].y - chicks[i].imgs[0].Height - 100)
                        {
                            chick pnn = new chick();
                            pnn.x = chicks[i].x;
                            pnn.y = heli.y + heli.img.Height + 5;
                            pnn.imgs.Add(chicks[i].imgs[0]);
                            pnn.imgs.Add(chicks[i].imgs[1]);
                            heli.chicks.Add(pnn);
                            chicks.RemoveAt(i);
                            plats[i + 1].hasChick = false;
                        }
                    }
                    break;
                default:
                    break;

            }
            //DrawDubb(CreateGraphics());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            DrawDubb(CreateGraphics());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            createheli();
            createplats();
            createchicks();
            DrawDubb(CreateGraphics());
        }
        void createheli()
        {
            heli.img = new Bitmap(@"U:\MULTIMEDIA\assignemnt\heli\1.bmp");
            heli.x = ClientSize.Width / 2 - heli.img.Width / 2;
            heli.y = ClientSize.Height / 10;
        }
        void createplats()
        {
            int y = ClientSize.Height / 10 * 8;
            Random rr = new Random();
            Bitmap img = new Bitmap(@"U:\MULTIMEDIA\assignemnt\heli\2.bmp");
            for (int i = 0; i < 6; i++)
            {
                plat pnn = new plat();
                if (i == 0)
                {
                    pnn.x = 0;
                    pnn.y = y;
                    pnn.img = img;
                }
                else if(i == 5)
                {
                    pnn.x = ClientSize.Width / 10 * 8;
                    pnn.y = y;
                    pnn.img = img;
                }
                else
                {
                    pnn.x = rr.Next(img.Width, ClientSize.Width / 10 * 7);
                    pnn.y = y;
                    pnn.img = img;
                }

                plats.Add(pnn);
            }

            
        }
        void createchicks()
        {
            for (int i = 1; i < 5; i++)
            {
                chick pnn = new chick();
                Bitmap img = new Bitmap(@"U:\MULTIMEDIA\assignemnt\heli\3.bmp");
                pnn.imgs.Add(img);
                img = new Bitmap(@"U:\MULTIMEDIA\assignemnt\heli\4.bmp");
                pnn.imgs.Add(img);
                pnn.x = (plats[i].x + plats[i].img.Width / 2) - (pnn.imgs[0].Width / 2);
                pnn.y = plats[i].y - pnn.imgs[0].Height - 2;
                chicks.Add(pnn);
            }
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Lime);
            heli.img.MakeTransparent(heli.img.GetPixel(0, 0));
            g.DrawImage(heli.img, heli.x, heli.y);
            for (int i = 0; i < heli.chicks.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    heli.chicks[i].imgs[1].MakeTransparent(heli.chicks[i].imgs[1].GetPixel(0, 0));
                    g.DrawImage(heli.chicks[i].imgs[1], heli.chicks[i].x, heli.chicks[i].y);
                }
            }
            for (int i = 0; i < chicks.Count; i++)
            {
                if (chicks[i].isCap)
                {
                    chicks[i].imgs[1].MakeTransparent(chicks[i].imgs[1].GetPixel(0, 0));
                    g.DrawImage(chicks[i].imgs[1], chicks[i].x, chicks[i].y);
                }
                else
                {
                    chicks[i].imgs[0].MakeTransparent(chicks[i].imgs[0].GetPixel(0, 0));
                    g.DrawImage(chicks[i].imgs[0], chicks[i].x, chicks[i].y);
                }
            }
            for (int i = 0; i < plats.Count; i++)
            {
                g.DrawImage(plats[i].img, plats[i].x, plats[i].y);
            }
        }
    }
}
