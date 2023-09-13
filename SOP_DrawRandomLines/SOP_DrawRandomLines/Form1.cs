using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SOP_DrawRandomLines
{
    public partial class Form1 : Form
    {
        Graphics g;
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
            g = canvas.CreateGraphics();
        }

        void DrawRandomLines()
        {
            int x1 = rnd.Next(canvas.Width);
            int y1 = rnd.Next(canvas.Height);
            int x2 = rnd.Next(canvas.Width);
            int y2 = rnd.Next(canvas.Height);
            lock (g)
            {
                g.DrawLine(Pens.Black, x1, y1, x2, y2);
            }
            Thread.Sleep(1000);
            lock (g)
            {
                g.DrawLine(Pens.White, x1, y1, x2, y2);
            }
        }

        void DrawHorizontalLines(int x, int y)
        {
            lock (g)
            {
                g.DrawLine(Pens.Black, x, y, canvas.Width, y);
            }
        }

        private void DrawLines_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                //Thread t = new Thread(() => DrawHorizontalLines(0, i));
                Thread t = new Thread(DrawRandomLines);
                t.Start();
            }

        }
    }
}
