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
        bool mouseDown = false;
        Vector lastMousePosition = new Vector(0, 0);//the position of the mouse at the last mouse event, panel coordinates

        public frmPaint()
        {
            this.DoubleBuffered = true;
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
            this.DoubleBuffered = true;
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
            updatePixelAtPosition(e);
        }

        private void pnlPaint_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastMousePosition.x = e.X;
            lastMousePosition.y = e.Y;
            updatePixelAtPosition(e);
        }

        void updatePixelAtPosition(MouseEventArgs e)
        {
            updatePixelAtPosition(e.X, e.Y);
        }
        /// <summary>
        /// Updates the pixel at the given screen coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void updatePixelAtPosition(int ex, int ey)
        {
            if (ex < GRID_SIZE * PIXEL_SIZE
                && ey < GRID_SIZE * PIXEL_SIZE)
            {
                updatePixel(ex / PIXEL_SIZE, ey / PIXEL_SIZE);
                if (ex != lastMousePosition.x || ey != lastMousePosition.y)
                {
                    int minx = (int)Math.Min(ex, lastMousePosition.x);
                    int maxx = (int)Math.Max(ex, lastMousePosition.x);
                    int miny = (int)Math.Min(ey, lastMousePosition.y);
                    int maxy = (int)Math.Max(ey, lastMousePosition.y);
                    int rise = ey - lastMousePosition.y;
                    int run = ex - lastMousePosition.x;
                    if (run == 0)
                    {
                        //vertical line
                        for (int y = miny + 1; y < maxy; y++)
                        {
                            updatePixel(ex / PIXEL_SIZE, y / PIXEL_SIZE);
                        }
                    }
                    else
                    {
                        int offset = ey - (ex * rise / run);
                        float threshold = 0.1f;
                        for (int x = minx; x <= maxx; x++)
                        {
                            int lowY = (int)Math.Floor(((x + threshold) * rise / run) + offset);
                            int highY = (int)Math.Floor(((x + 1 - threshold) * rise / run) + offset);
                            for (int y = lowY; y <= highY; y++)
                            {
                                updatePixel(x / PIXEL_SIZE, y / PIXEL_SIZE);
                            }
                        }
                    }
                }
                lastMousePosition.x = ex;
                lastMousePosition.y = ey;
            }
        }
        /// <summary>
        /// Updates the pixel at the given grid x,y index
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void updatePixel(int px, int py)
        {
            pixelGrid[px, py] = dlgColor.Color;
            pnlPaint.Invalidate(new Rectangle(
                px * PIXEL_SIZE,
                py * PIXEL_SIZE,
                PIXEL_SIZE,
                PIXEL_SIZE
                ));
        }

        private void pnlPaint_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                updatePixelAtPosition(e);
            }
        }

        private void pnlPaint_MouseUp(object sender, MouseEventArgs e)
        {
            updatePixelAtPosition(e);
            mouseDown = false;
        }

        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgColor.ShowDialog();
            btnColorPicker.BackColor = dlgColor.Color;
        }
    }
}
