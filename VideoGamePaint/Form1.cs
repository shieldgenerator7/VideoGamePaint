using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoGamePaint
{
    public partial class frmPaint : Form
    {
        const int GRID_SIZE = 100;
        const int PIXEL_SIZE = 8;
        Color[,] pixelGrid;
        Dictionary<Color, Brush> colorBrushes = new Dictionary<Color, Brush>();
        //List<List<Color>> pixelGrid;

        public frmPaint()
        {
            InitializeComponent();
            pixelGrid = new Color[GRID_SIZE, GRID_SIZE];
            for (int x = 0; x < GRID_SIZE; x++)
            {
                for (int y = 0; y < GRID_SIZE; y++)
                {
                    Random r = new Random(x * y);// *(int)System.DateTime.Now.Ticks);
                    pixelGrid[x, y] = Color.FromArgb(
                        r.Next() % 256,
                        r.Next() % 256,
                        r.Next() % 256
                        );
                }
            }
        }

        private void pnlPaint_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Red);
            Brush b = new SolidBrush(Color.Red);
            Graphics g = e.Graphics;
            //g.DrawRectangle(p, 10, 10, 100, 100);
            //g.FillRectangle(b, 10, 10, 100, 100);
            
            for (int x = 0; x < GRID_SIZE; x++)
            {
                for (int y = 0; y < GRID_SIZE; y++)
                {
                    g.FillRectangle(
                        getBrush(pixelGrid[x, y]),
                        x * PIXEL_SIZE,
                        y * PIXEL_SIZE,
                        PIXEL_SIZE,
                        PIXEL_SIZE
                        );
                }
            }
        }

        Brush getBrush(Color color)
        {
            if (color == null)
            {
                color = Color.Red;
            }
            Brush brush;
            if (colorBrushes.ContainsKey(color))
            {
                brush = colorBrushes[color];
            }
            else
            {
                brush = new SolidBrush(color);
                colorBrushes.Add(color, brush);
            }
            return brush;
        }

        private void pnlPaint_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X < GRID_SIZE * PIXEL_SIZE
                && e.Y < GRID_SIZE * PIXEL_SIZE)
            {
                pixelGrid[e.X / PIXEL_SIZE, e.Y / PIXEL_SIZE] = Color.Black;
            }
        }
    }
}
