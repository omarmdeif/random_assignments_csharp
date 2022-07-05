using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp31
{
    public partial class Form1 : Form
    {
        public Bitmap off;
        public Pen lime = new Pen(Color.Lime, 3);
        
        public List<PointF> ps = new List<PointF>();
        public int x, y, state = 1;
        public Rectangle s = new Rectangle();
        public Rectangle a = new Rectangle();
        public Rectangle[] rects = new Rectangle[2];
        public int bstate;
        public Timer tt = new Timer();
        public Form1()
        {
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyUp += Form1_KeyUp;
            this.WindowState = FormWindowState.Maximized;
            tt.Tick += Tt_Tick;
            this.KeyDown += Form1_KeyDown;
            tt.Start();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    bstate = 0;
                    break;
                case Keys.Right:
                    bstate = 0;
                    break;
                case Keys.Up:
                    bstate = 0;
                    break;
                case Keys.Down:
                    bstate = 0;
                    break;
                default:
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    bstate = 1;
                    break;
                case Keys.Right:
                    bstate = 2;
                    break;
                case Keys.Up:
                    bstate = 3;
                    break;
                case Keys.Down:
                    bstate = 4;
                    break;
                default:
                    break;
            }
            //DrawDubb(CreateGraphics());
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            switch (bstate)
            {
                case 1:
                    x -= 15;
                    break;
                case 2:
                    x += 15;
                    break;
                case 3:
                    y -= 15;
                    break;
                case 4:
                    y += 15;
                    break;
                default:
                    break;
            }
            switch (state)
            {
                case 1:
                    rects[0].X += 10;
                    rects[1].X -= 10;
                    if(rects[1].X == ClientSize.Width / 3)
                    {
                        state = 2;
                    }
                    break;
                case 2:
                    rects[0].Y += 10;
                    rects[1].Y -= 10;
                    if(rects[1].Y == ClientSize.Height / 2 - 400)
                    {
                        state = 3;
                    }
                    break;
                case 3:
                    rects[0].X -= 10;
                    rects[1].X += 10;
                    if(rects[0].X == ClientSize.Width / 3)
                    {
                        state = 4;
                    }
                    break;
                case 4:
                    rects[0].Y -= 10;
                    rects[1].Y += 10;
                    if(rects[0].Y == ClientSize.Height / 2 - 400)
                    {
                        state = 1;
                    }
                    break;
                default:
                    break;
            }
            for (int i = 0; i < rects.Length; i++)
            {
                if(x >= rects[i].X && x <= rects[i].X + rects[i].Width
                    && y + 35 >= rects[i].Y && y <= rects[i].Y + rects[i].Height)
                {
                    tt.Stop();
                    MessageBox.Show("YOU CRASHED YOU LOSE!!!");
                    
                }
            }
            if(x > ClientSize.Width / 3 * 2)
            {
                tt.Stop();
                MessageBox.Show("YOU HAVE NOT CRASHED AND PASSED SUCCESSFULLY, YOU WIN!!");
                
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
            createlinesnrects();
            DrawDubb(CreateGraphics());
        }
        void createlinesnrects()
        {
            PointF p1 = new PointF(ClientSize.Width / 11, ClientSize.Height / 2 - 150);
            PointF p1s = new PointF(ClientSize.Width / 11, ClientSize.Height / 2 + 150);
            ps.Add(p1);
            ps.Add(p1s);
            PointF p2 = new PointF(ClientSize.Width / 11 * 10, ClientSize.Height / 2 - 150);
            PointF p2s = new PointF(ClientSize.Width / 11 * 10, ClientSize.Height / 2 + 150);
            ps.Add(p2);
            ps.Add(p2s);
            PointF p3 = new PointF(ClientSize.Width / 11, ClientSize.Height / 2 - 150);
            PointF p3s = new PointF(ClientSize.Width / 3, ClientSize.Height / 2 - 150);
            ps.Add(p3);
            ps.Add(p3s);
            PointF p4 = new PointF(ClientSize.Width / 11, ClientSize.Height / 2 + 150);
            PointF p4s = new PointF(ClientSize.Width / 3, ClientSize.Height / 2 + 150);
            ps.Add(p4);
            ps.Add(p4s);
            PointF p5 = new PointF(ClientSize.Width / 11 * 10, ClientSize.Height / 2 - 150);
            PointF p5s = new PointF(ClientSize.Width / 3 * 2, ClientSize.Height / 2 - 150);
            ps.Add(p5);
            ps.Add(p5s);
            PointF p6 = new PointF(ClientSize.Width / 11 * 10, ClientSize.Height / 2 + 150);
            PointF p6s = new PointF(ClientSize.Width / 3*2, ClientSize.Height / 2 + 150);
            ps.Add(p6);
            ps.Add(p6s);
            PointF p7 = new PointF(ClientSize.Width / 3, ClientSize.Height / 2 + 150);
            PointF p7s = new PointF(ClientSize.Width / 3, ClientSize.Height / 2 + 400);
            ps.Add(p7);
            ps.Add(p7s);
            PointF p8 = new PointF(ClientSize.Width / 3 * 2, ClientSize.Height / 2 + 150);
            PointF p8s = new PointF(ClientSize.Width / 3 * 2, ClientSize.Height / 2 + 400 );
            ps.Add(p8);
            ps.Add(p8s);
            PointF p9 = new PointF(ClientSize.Width / 3, ClientSize.Height / 2 - 150);
            PointF p9s = new PointF(ClientSize.Width / 3, ClientSize.Height / 2 - 400);
            ps.Add(p9);
            ps.Add(p9s);
            PointF p11 = new PointF(ClientSize.Width / 3 * 2, ClientSize.Height / 2 - 150);
            PointF p11s = new PointF(ClientSize.Width / 3 * 2, ClientSize.Height / 2 - 400);
            ps.Add(p11);
            ps.Add(p11s);
            PointF p12 = new PointF(ClientSize.Width / 3, ClientSize.Height / 2 - 400);
            PointF p12s = new PointF(ClientSize.Width / 3 * 2, ClientSize.Height / 2 - 400);
            ps.Add(p12);
            ps.Add(p12s);
            PointF p13 = new PointF(ClientSize.Width / 3, ClientSize.Height / 2 + 400);
            PointF p13s = new PointF(ClientSize.Width / 3 * 2, ClientSize.Height / 2 + 400);
            ps.Add(p13);
            ps.Add(p13s);
            x = ((ClientSize.Width / 11) + (ClientSize.Width/3))/ 2;
            y = ClientSize.Height / 2;

            s.X = ClientSize.Width / 3;
            s.Y = ClientSize.Height / 2 - 400;
            s.Width = ((ClientSize.Width / 3*2) - (ClientSize.Width / 3))/2;
            s.Height = 250;
            a.X = ((ClientSize.Width / 3) + (ClientSize.Width / 3 * 2)) / 2;
            a.Y = ClientSize.Height / 2 + 150;
            a.Width = ((ClientSize.Width / 3*2) - (ClientSize.Width / 3))/2;
            a.Height = 250;
            rects[0] = s;
            rects[1] = a;
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
            for (int i = 0; i < ps.Count; i += 2)
            {
                g.DrawLine(lime, ps[i], ps[i + 1]);
            }
            g.FillEllipse(Brushes.Blue, x, y,50, 50);

            g.FillRectangle(Brushes.White, rects[0]);
            g.FillRectangle(Brushes.White, rects[1]);

        }
    }
}
