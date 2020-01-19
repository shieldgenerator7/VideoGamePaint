using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VideoGamePaint
{
    public class PixelGridPanel : Panel
    {
        public float pixelSize = 8;//how many screen pixels wide a grid pixel is

        bool mouseDown = false;
        Vector lastMousePosition = new Vector(0, 0);//the position of the mouse at the last mouse event, panel coordinates

        public PixelGrid pixelGrid { get; private set; } = new PixelGrid();

        public Color drawColor;
        Dictionary<Color, Brush> colorBrushes = new Dictionary<Color, Brush>();

        public PixelGridPanel() : this(100,100)
        {
        }

        public PixelGridPanel(int width, int height): base()
        {
            this.DoubleBuffered = true;
            pixelGrid.Size = new Vector(width, height);
        }

        void updatePixelAtPosition(MouseEventArgs e, bool forceRedraw = false)
        {
            int ex = e.X;
            int ey = e.Y;
            if (forceRedraw || ex != lastMousePosition.x || ey != lastMousePosition.y)
            {
                RGB rgb = ColorToRGB(drawColor);
                updatePixelAtPosition(ex, ey, rgb);
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
                        updatePixelAtPosition(ex, y, rgb);
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
                            updatePixelAtPosition(x, y, rgb);
                        }
                    }
                }
                lastMousePosition.x = ex;
                lastMousePosition.y = ey;
                Invalidate();
            }
        }

        /// <summary>
        /// Updates the pixel at the given screen coordinates
        /// </summary>
        /// <param name="x">Screen x</param>
        /// <param name="y">Screen y</param>
        public void updatePixelAtPosition(int ex, int ey, RGB rgb)
        {
            if (ex < pixelGrid.Size.x * pixelSize
                && ey < pixelGrid.Size.y * pixelSize)
            {
                pixelGrid.setPixel(gridPixel(ex), gridPixel(ey), rgb);                
            }
        }

        public Color getColor(int x, int y)
        {
            return RGBToColor(pixelGrid.getPixel(x, y));
        }

        public Rectangle getRect(int x, int y)
        {
            return new Rectangle(
                (int)(x * pixelSize),
                (int)(y * pixelSize),
                (int)pixelSize,
                (int)pixelSize
                );
        }

        /// <summary>
        /// Converts the panel pixel coordinate to grid pixel coordinate
        /// </summary>
        /// <param name="panelPixel"></param>
        /// <returns></returns>
        int gridPixel(int panelPixel)
        {
            return (int)Math.Floor(panelPixel / pixelSize);
        }

        public static RGB ColorToRGB(Color color)
        {
            return new RGB(
                color.R,
                color.G,
                color.B
                );
        }

        public static Color RGBToColor(RGB rgb)
        {
            return Color.FromArgb(
                rgb.red,
                rgb.green,
                rgb.blue
                );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            for (int x = 0; x < pixelGrid.Size.x; x++)
            {
                for (int y = 0; y < pixelGrid.Size.y; y++)
                {
                    g.FillRectangle(
                        getBrush(getColor(x, y)),
                        getRect(x, y)
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            lastMousePosition.x = e.X;
            lastMousePosition.y = e.Y;
            mouseDown = true;
            updatePixelAtPosition(e, true);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseDown)
            {
                updatePixelAtPosition(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseDown = false;
            updatePixelAtPosition(e);
        }
    }
}