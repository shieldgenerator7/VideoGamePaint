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
        public Tool activeTool;
        Dictionary<Color, Brush> colorBrushes = new Dictionary<Color, Brush>();

        public bool defaultPaintingEnabled = true;



        public PixelGridPanel() : this(100, 100)
        {
        }

        public PixelGridPanel(int width, int height) : base()
        {
            this.DoubleBuffered = true;
            pixelGrid.Size = new Vector(width, height);
        }

        void updatePixelAtPosition(MouseEventArgs e, bool forceRedraw = false)
        {
            updatePixelAtPosition(e.X, e.Y, forceRedraw);
        }

        public void updatePixelAtPosition(int ex, int ey, bool forceRedraw = false) { 
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

        public bool isColor(int gx, int gy, Color color)
        {
            return RGBToColor(pixelGrid.getPixel(gx, gy)) == color;
        }

        /// <summary>
        /// Gets the color at the given panel coordinates
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ey"></param>
        /// <returns></returns>
        public Color getColor(int ex, int ey)
        {
            return RGBToColor(pixelGrid.getPixel(gridPixel(ex), gridPixel(ey)));
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
        public int gridPixel(int panelPixel)
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
                        getBrush(RGBToColor(pixelGrid.getPixel(x, y))),
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
            mouseDown = true;
            if (defaultPaintingEnabled)
            {
                lastMousePosition.x = e.X;
                lastMousePosition.y = e.Y;
                //updatePixelAtPosition(e, true);
                activeTool.activate(e.X, e.Y);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseDown)
            {
                if (defaultPaintingEnabled)
                {
                    //updatePixelAtPosition(e);
                    activeTool.activate(e.X, e.Y);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseDown = false;
            if (defaultPaintingEnabled)
            {
                //updatePixelAtPosition(e);
                activeTool.activate(e.X, e.Y);
            }
            onPixelClicked?.Invoke(getColor(e.X, e.Y));
        }
        public delegate void OnPixelClicked(Color pixelColor);
        public OnPixelClicked onPixelClicked;

        /// <summary>
        /// Fills the grid with the colors in the list, stopping when it runs out of colors
        /// </summary>
        /// <param name="colors"></param>
        public void setColorsFromList(List<Color> colors)
        {
            int i = 0;
            for (int x = 0; x < pixelGrid.Size.x && i < colors.Count; x++)
            {
                for (int y = 0; y < pixelGrid.Size.y && i < colors.Count; y++)
                {
                    pixelGrid.setPixel(x, y, ColorToRGB(colors[i]));
                    i++;
                }
            }
        }
    }
}