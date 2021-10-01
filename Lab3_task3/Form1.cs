using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_task3
{
    public partial class Form1 : Form
    {
        PointF[] tr_points = new PointF[3];

        
        Point A = new Point(100, 50);
        Point B = new Point (200, 300);
        Point C = new Point(300, 40);

        Color cA;
        Color cB;
        Color cC;

        Random rnd = new Random();

        public class Triangle
        {
            public Point A;
            public Point B;
            public Point C;
        }

        public Form1()
        {
            InitializeComponent();
            tr_points[0] = A;
            tr_points[1] = B;
            tr_points[2] = C;
        }
        

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 1);
            e.Graphics.DrawPolygon(pen, tr_points);
        }

        public Color HEXtoRGB(string hexColor)
        {
            int a = 255;
            if (hexColor.Length == 8)
            {
                string alpha = hexColor.Substring(6, 2);
                a = Convert.ToInt32(alpha, 16);
            }
            int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
            int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
            int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fill();
        }

        public void Fill()
        {
            Triangle abc = new Triangle();
            abc.A = A;
            abc.B = B;
            abc.C = C;
            cA = HEXtoRGB(textBox1.Text);
            cB = HEXtoRGB(textBox2.Text);
            cC = HEXtoRGB(textBox3.Text);
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            int minX = Math.Min(A.X, Math.Min(B.X, C.X));
            int maxX = Math.Max(A.X, Math.Max(B.X, C.X));
            int minY = Math.Min(A.Y, Math.Min(B.Y, C.Y));
            int maxY = Math.Max(A.Y, Math.Max(B.Y, C.Y));
            for (int i = minX; i <= maxX; i++)
                for (int j = minY; j <= maxY; j++)
                {
                    if (inTriangle(new Point(i, j), abc))
                    {
                        double x = i;
                        double y = j;
                        double L1 = ((B.Y - C.Y) * (x - C.X) + (C.X - B.X) * (y - C.Y)) / ((B.Y - C.Y) * (A.X - C.X) + (C.X - B.X) * (A.Y - C.Y));
                        double L2 = ((C.Y - A.Y) * (x - C.X) + (A.X - C.X) * (y - C.Y)) / ((B.Y - C.Y) * (A.X - C.X) + (C.X - B.X) * (A.Y - C.Y));
                        double L3 = 1 - L1 - L2;
                        if (L1 >= 0 && L2 >= 0 && L3 >= 0 && L1 <= 1 && L2 <= 1 && L3 <= 1)
                            bmp.SetPixel(i, j, Color.FromArgb(Convert.ToInt32(L1 * cA.A + L2 * cB.A + L3 * cC.A), Convert.ToInt32(L1 * cA.R + L2 * cB.R + L3 * cC.R), Convert.ToInt32(L1 * cA.G + L2 * cB.G + L3 * cC.G), Convert.ToInt32(L1 * cA.B + L2 * cB.B + L3 * cC.B)));
                    }

                }

            pictureBox1.Image = bmp;
        }

       //public Color AddColor(Color c1, Color c2)
       //{
       //    return Color.FromArgb((c1.A + c2.A) % 256, (c1.R + c2.R) % 256, (c1.G + c2.G) % 256, (c1.B + c2.B) % 256);
       //}
       //
       //public Color SubtractColor(Color c1, Color c2)
       //{
       //    return Color.FromArgb( c1.A >= c2.A ? c1.A - c2.A : 0, c1.R >= c2.R ? c1.R - c2.R : 0, c1.G >= c2.G ? c1.G - c2.G : 0, c1.B >= c2.B ? c1.B - c2.B : 0);
       //}
       //
       //public Color MultiplyColor(Color c, double k)
       //{
       //    return Color.FromArgb((int)(c.A * k), (int)(c.R * k), (int)(c.G * k), (int)(c.B * k));
       //}

        public Boolean inTriangle(Point p0, Triangle t)
        {
            int x0 = p0.X;
            int x1 = t.A.X;
            int x2 = t.B.X;
            int x3 = t.C.X;
            int y0 = p0.Y;
            int y1 = t.A.Y;
            int y2 = t.B.Y;
            int y3 = t.C.Y;

            int a = (x1 - x0) * (y2 - y1) - (x2 - x1) * (y1 - y0);
            int b = (x2 - x0) * (y3 - y2) - (x3 - x2) * (y2 - y0);
            int c = (x3 - x0) * (y1 - y3) - (x1 - x3) * (y3 - y0);
            if ((a > 0 && b > 0 && c > 0) || (a < 0 && b < 0 && c < 0))
                return true;
            return false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.BackColor = HEXtoRGB(textBox1.Text);
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.BackColor = HEXtoRGB(textBox2.Text);
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.BackColor = HEXtoRGB(textBox3.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            A = new Point(rnd.Next(20, pictureBox1.Width - 20), rnd.Next(20, pictureBox1.Height - 20));
            B = new Point(rnd.Next(20, pictureBox1.Width - 20), rnd.Next(20, pictureBox1.Height - 20));
            C = new Point(rnd.Next(20, pictureBox1.Width - 20), rnd.Next(20, pictureBox1.Height - 20));
            tr_points[0] = A;
            tr_points[1] = B;
            tr_points[2] = C;

            pictureBox1.Image = null;
            PaintEventArgs p = new PaintEventArgs(pictureBox1.CreateGraphics(), pictureBox1.Bounds);
            pictureBox1_Paint(sender, p);
        }

        public string HexGenerator()
        {
            string HEX = "0123456789ABCDEF";
            string res = "";
            
            for (int i = 0; i < 8; i++)
            {
                int rnd_val = rnd.Next(16);
                res += HEX[rnd_val];
                rnd_val = rnd.Next(16);
            }
            return res;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string random_hex;
            Color col;

            random_hex = HexGenerator();
            textBox1.Text = random_hex;
            col = HEXtoRGB(random_hex);
            pictureBox2.BackColor = col;
            cA = col;

            random_hex = HexGenerator();
            textBox2.Text = random_hex;
            col = HEXtoRGB(random_hex);
            pictureBox3.BackColor = col;
            cB = col;

            random_hex = HexGenerator();
            textBox3.Text = random_hex;
            col = HEXtoRGB(random_hex);
            pictureBox4.BackColor = col;
            cC = col;

            Fill();
        }
    }
}
