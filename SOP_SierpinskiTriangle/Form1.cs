using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace SOP_SierpinskiTriangle
{
    public partial class Form1 : Form
    {
        Graphics g;
        int depth = 8;
        const int MAX_THREAD_COUNT = 3;
        int threadCount = 0;
        public Form1()
        {
            InitializeComponent();
            g = canvas.CreateGraphics();
        }

        void DrawBaseTriangle()
        {
            Point a = new Point(canvas.Width / 2, 10);
            Point b = new Point(10, canvas.Height - 10);
            Point c = new Point(canvas.Width - 10, canvas.Height - 10);
            g.DrawPolygon(Pens.Black, new Point[] { a, b, c });
            DrawTriangles(a, b, c, depth);
        }

        void DrawTriangles(Point a, Point b, Point c, int depth)
        {
            if(depth < 0)
            {
                return;
            }
            lock (g)
            {
                g.DrawLine(Pens.Black, b, c);
            }
            Point mAB = GetMiddlePoint(a, b);
            Point mAC = GetMiddlePoint(a, c);
            Point mBC = GetMiddlePoint(b, c);
            int currentDepth = depth - 1;
            Thread t1 = new Thread(() => DrawTriangles(a, mAB, mAC, currentDepth));
            if(threadCount < MAX_THREAD_COUNT)
            {
                t1.Start();
                threadCount++;
            }
            else
            {
                DrawTriangles(a, mAB, mAC, currentDepth);
            }
            Thread t2 = new Thread(() => DrawTriangles(b, mAB, mBC, currentDepth));
            if (threadCount < MAX_THREAD_COUNT)
            {
                t2.Start();
                threadCount++;
            }
            else
            {
                DrawTriangles(b, mAB, mBC, currentDepth);
            }
            Thread t3 = new Thread(() => DrawTriangles(c, mAC, mBC, currentDepth));
            if (threadCount < MAX_THREAD_COUNT)
            {
                t3.Start();
                threadCount++;
            }
            else
            {
                DrawTriangles(c, mAC, mBC, currentDepth);
            }
            if(t1.ThreadState != ThreadState.Unstarted)
            {
                t1.Join();
                threadCount--;
            }
            if (t2.ThreadState != ThreadState.Unstarted)
            {
                t2.Join();
                threadCount--;
            }
            if (t3.ThreadState != ThreadState.Unstarted)
            {
                t3.Join();
                threadCount--;
            }
        }

        Point GetMiddlePoint(Point p0, Point p1)
        {
            int x = (p0.X + p1.X) / 2;
            int y = (p0.Y + p1.Y) / 2;
            return new Point(x, y);
        }

        private void drawTriangles_Click(object sender, EventArgs e)
        {
            DrawBaseTriangle();
        }
    }
}
