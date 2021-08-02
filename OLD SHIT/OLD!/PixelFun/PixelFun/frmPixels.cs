using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PixelFun
{
    public partial class frmPixels : Form
    {

        private struct pixl
        {
            public int r;
            public int g;
            public int b;
        }

        private pixl[,] pixls = new pixl[1000,2];
        


        public frmPixels()
        {
            InitializeComponent();
        }

        private void frmPixels_Load(object sender, EventArgs e)
        {

        }

        public void setPixel(int x, int y, int r, int g, int b)
        {

            if (x < 1000)
            {
                pixls[x, y].r = r;
                pixls[x, y].g = g;
                pixls[x, y].b = b;
            }

        
        }

        
        private int intensity(int amount)
        {
            return Convert.ToInt16(amount * 2.5);
        }


        private void frmPixels_Paint(object sender, PaintEventArgs e)
        {

            Graphics go = e.Graphics;
            Pen marker;
            int n;
            int xStart = 15;
            int xEnd = xStart + 1000 - 1;
            int curY;

            SolidBrush sb = new SolidBrush(Color.Silver);
            go.FillRectangle(sb, xStart, 20, 1000, 80);
            go.FillRectangle(sb, xStart, 120, 1000, 80);
            sb.Dispose();


            for (int i = 0; i < 1000; i++)
            {
                curY = 20; 
                marker = new Pen(Color.FromArgb(pixls[i, 0].r, pixls[i, 0].g, pixls[i, 0].b));
                go.DrawLine(marker, new Point(i+xStart, curY), new Point(i+xStart, curY+19));
                marker.Dispose();

                curY += 20;
                if (pixls[i, 0].r > 0)
                {
                    marker = new Pen(Color.FromArgb(255, 0, 0), 1);
                    n = 20 - pixls[i, 0].r / 13 + curY;
                    go.DrawLine(marker, new Point(i+xStart, n), new Point(i+xStart, curY+19));
                    marker.Dispose();
                }

                curY += 20;
                if (pixls[i, 0].g > 0)
                {
                    marker = new Pen(Color.FromArgb(0, 255, 0), 1);
                    n = 20 - pixls[i, 0].g / 13 + curY;
                    go.DrawLine(marker, new Point(i+xStart, n), new Point(i+xStart, curY+19));
                    marker.Dispose();
                }

                curY += 20;
                if (pixls[i, 0].b > 0)
                {
                    marker = new Pen(Color.FromArgb(0, 0, 255), 1);
                    n = 20 - pixls[i, 0].b / 13 + curY;
                    go.DrawLine(marker, new Point(i+xStart, n), new Point(i+xStart, curY+19));
                    marker.Dispose();
                }

                curY += 40;
                marker = new Pen(Color.FromArgb(pixls[i, 1].r, pixls[i, 1].g, pixls[i, 1].b), 1);
                go.DrawLine(marker, new Point(i+xStart, curY), new Point(i+xStart, curY+19));
                marker.Dispose();

                curY += 20;
                if (pixls[i, 1].r > 0)
                {
                    marker = new Pen(Color.FromArgb(255, 0, 0), 1);
                    n = 20 - pixls[i, 1].r / 13 + curY;
                    go.DrawLine(marker, new Point(i+xStart, n), new Point(i+xStart, curY+19));
                    marker.Dispose();
                }

                curY += 20;
                if (pixls[i, 1].g > 0)
                {
                    marker = new Pen(Color.FromArgb(0, 255, 0), 1);
                    n = 20 - pixls[i, 1].g / 13 + curY;
                    go.DrawLine(marker, new Point(i+xStart, n), new Point(i+xStart, curY+19));
                    marker.Dispose();
                }

                curY += 20;
                if (pixls[i, 1].b > 0)
                {
                    marker = new Pen(Color.FromArgb(0, 0, 255), 1);
                    n = 20 - pixls[i, 1].b / 13 + curY;
                    go.DrawLine(marker, new Point(i+xStart, n), new Point(i+xStart, curY+19));
                    marker.Dispose();
                }
            }

            curY = 20; // The Start
            curY += 20;
            marker = new Pen(Color.DarkGray);
            go.DrawLine(marker, new Point(xStart, curY), new Point(xEnd, curY));
            curY += 20;
            go.DrawLine(marker, new Point(xStart, curY), new Point(xEnd, curY));
            curY += 20;
            go.DrawLine(marker, new Point(xStart, curY), new Point(xEnd, curY));
            
            curY += 60;
            go.DrawLine(marker, new Point(xStart, curY), new Point(xEnd, curY));
            curY += 20;
            go.DrawLine(marker, new Point(xStart, curY), new Point(xEnd, curY));
            curY += 20;
            go.DrawLine(marker, new Point(xStart, curY), new Point(xEnd, curY));
            marker.Dispose();

            marker = new Pen(Color.Black);
            go.DrawRectangle(marker, xStart, 20, 1000,80);
            go.DrawRectangle(marker, xStart, 120, 1000, 80);
            marker.Dispose();
        }

    }



}
