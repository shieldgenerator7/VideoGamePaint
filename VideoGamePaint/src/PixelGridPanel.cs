﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VideoGamePaint
{
    public class PixelGridPanel : Panel
    {
        public List<Keys> pressedKeys = new List<Keys>();

        private float pixelSize = 8;//how many screen pixels wide a grid pixel is
        public float PixelSize
        {
            get => pixelSize;
            set
            {
                pixelSize = Math.Min(Math.Max(1, value), 100);
                lighten.Width = pixelSize / 8;
                darken.Width = pixelSize / 8;
            }
        }
        public Vector mapPos = new Vector(0, 0);//the panel position in which to start drawing the grid

        bool mouseDown = false;
        public Vector lastMousePosition = new Vector(0, 0);//the position of the mouse at the last mouse event, panel coordinates
        public Vector firstMousePosition = new Vector(0, 0);//the position of the mouse at the first mouse event of the click, panel coordinates

        public PixelGrid pixelGrid { get; private set; } = new PixelGrid();
        public PixelGrid toolGrid { get; private set; } = new PixelGrid();//grid for drawing tool effect previews
        public PixelGrid entityGrid { get; private set; } = new PixelGrid();//grid for drawing things that move often
        public PixelGrid colliderGrid { get; private set; } = new PixelGrid();//grid for storing collision data
        public PixelGrid ActiveGrid;

        public RGB drawColor;
        public Color DrawColor
        {
            set
            {
                drawColor = ColorToRGB(value);
            }
        }
        public Tool activeTool;
        Dictionary<Color, Brush> colorBrushes = new Dictionary<Color, Brush>();
        Pen lighten = new Pen(Color.FromArgb(100, 255, 255, 255), 1);
        Pen darken = new Pen(Color.FromArgb(100, 0, 0, 0), 1);

        public bool defaultPaintingEnabled = true;
        public bool syncPixelGridToColliderGrid = true;



        public PixelGridPanel() : this(100, 100)
        {
        }

        public PixelGridPanel(int width, int height) : base()
        {
            this.DoubleBuffered = true;
            pixelGrid.Size = new Vector(width, height);
            toolGrid.defaultFillRGB = RGB.nullRGB;
            toolGrid.clear(RGB.nullRGB);
            entityGrid.defaultFillRGB = RGB.nullRGB;
            entityGrid.clear(RGB.nullRGB);
            colliderGrid.defaultFillRGB = RGB.nullRGB;
            colliderGrid.clear(RGB.nullRGB);
            ActiveGrid = pixelGrid;
        }

        void updatePixelAtPosition(MouseEventArgs e, bool forceRedraw = false)
        {
            updatePixelAtPosition(e.X, e.Y, forceRedraw);
        }

        public void updatePixelAtPosition(int ex, int ey, bool forceRedraw = false)
        {
            if (forceRedraw || ex != lastMousePosition.x || ey != lastMousePosition.y)
            {
                RGB rgb = drawColor;
                updatePixelAtPosition(ex, ey, rgb);
                if (ex != lastMousePosition.x || ey != lastMousePosition.y)
                {
                    foreach (Vector v in getPixelsInBetween(ex, ey, lastMousePosition.x, lastMousePosition.y))
                    {
                        updatePixelAtPosition(v.x, v.y, rgb);
                    }

                    lastMousePosition.x = ex;
                    lastMousePosition.y = ey;
                }
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
            int gx = gridPixelX(ex);
            int gy = gridPixelY(ey);
            //If the pixel is outside the pixel grid,
            if (gx < 0
                || gy < 0
                || gx >= ActiveGrid.Size.x
                || gy >= ActiveGrid.Size.y
                )
            {
                //Expand the pixel grid
                int expandX = 0;
                if (gx < 0)
                {
                    expandX = gx;
                    mapPos.x -= (int)(Math.Abs(gx) * PixelSize);
                }
                else if (gx >= ActiveGrid.Size.x)
                {
                    expandX = gx - ActiveGrid.Size.x + 1;
                }
                int expandY = 0;
                if (gy < 0)
                {
                    expandY = gy;
                    mapPos.y -= (int)(Math.Abs(gy) * PixelSize);
                }
                else if (gy >= ActiveGrid.Size.y)
                {
                    expandY = gy - ActiveGrid.Size.y + 1;
                }
                pixelGrid.expandGrid(expandX, expandY);
                toolGrid.expandGrid(expandX, expandY);
                entityGrid.expandGrid(expandX, expandY);
                colliderGrid.expandGrid(expandX, expandY);
            }
            updatePixel(gridPixelX(ex), gridPixelY(ey), rgb);
        }
        public void updatePixel(int gx, int gy, RGB rgb)
        {
            //Set the pixel at the position
            if (ActiveGrid != colliderGrid)
            {
                ActiveGrid.setPixel(gx, gy, rgb);
            }
            if (ActiveGrid == colliderGrid || syncPixelGridToColliderGrid)
            {
                //Draw in the collider grid automatically
                if (rgb.isValid())
                {
                    rgb = (rgb == RGB.white) ? RGB.nullRGB : RGB.black;
                }
                colliderGrid.setPixel(gx, gy, rgb);
            }
        }

        public static List<Vector> getPixelsInBetween(int x1, int y1, int x2, int y2, float threshold = 0.1f)
        {
            List<Vector> vectors = new List<Vector>();

            int minx = (int)Math.Min(x1, x2);
            int maxx = (int)Math.Max(x1, x2);
            int miny = (int)Math.Min(y1, y2);
            int maxy = (int)Math.Max(y1, y2);

            int rise = y2 - y1;
            int run = x2 - x1;

            if (run == 0 && rise == 0)
            {
                vectors.Add(new Vector(x1, y1));
                return vectors;
            }

            //More horizontal than vertical
            if (Math.Abs(run) >= Math.Abs(rise))
            {
                int offset = y2 - (x2 * rise / run);
                for (int x = minx; x <= maxx; x++)
                {
                    int y = (int)Math.Round(((x + threshold) * rise / run) + offset);
                    y = Math.Max(Math.Min(y, maxy), miny);
                    vectors.Add(new Vector(x, y));
                }
            }
            //More vertical than horizontal
            else
            {
                int offset = x2 - (y2 * run / rise);
                for (int y = miny; y <= maxy; y++)
                {
                    int x = (int)Math.Round(((y + threshold) * run / rise) + offset);
                    x = Math.Max(Math.Min(x, maxx), minx);
                    vectors.Add(new Vector(x, y));
                }
            }
            return vectors;
        }

        public bool isColor(int gx, int gy, Color color)
        {
            return RGBToColor(ActiveGrid.getPixel(gx, gy)) == color;
        }

        /// <summary>
        /// Gets the color at the given panel coordinates
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ey"></param>
        /// <returns></returns>
        public Color getColor(int ex, int ey)
        {
            return RGBToColor(ActiveGrid.getPixel(gridPixelX(ex), gridPixelY(ey)));
        }

        /// <summary>
        /// Returns the rectangle that can be used to draw the pixel on the screen
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Rectangle getRect(int x, int y, int width = 1, int height = 1)
        {
            return new Rectangle(
                (int)(x * PixelSize) + mapPos.x,
                (int)(y * PixelSize) + mapPos.y,
                (int)PixelSize * width,
                (int)PixelSize * height
                );
        }

        /// <summary>
        /// Converts the panel pixel coordinate to grid pixel coordinate
        /// </summary>
        /// <param name="panelPixelX"></param>
        /// <returns></returns>
        public int gridPixelX(int panelPixelX)
        {
            return (int)Math.Floor((panelPixelX - mapPos.x) / PixelSize);
        }
        public int gridPixelY(int panelPixelY)
        {
            return (int)Math.Floor((panelPixelY - mapPos.y) / PixelSize);
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
            if (!rgb.isValid())
            {
                return Color.White;
            }
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
            g.Clear(Color.LightGray);
            //
            int sgxMin = gridPixelX(0);
            int sgyMin = gridPixelY(0);
            int sgxMax = gridPixelX(Size.Width) + 1;
            int sgyMax = gridPixelY(Size.Height) + 1;
            //
            int iterXMin = Math.Max(sgxMin, 0);
            int iterYMin = Math.Max(sgyMin, 0);
            int iterXMax = Math.Min(sgxMax, ActiveGrid.Size.x);
            int iterYMax = Math.Min(sgyMax, ActiveGrid.Size.y);
            //Pixel Grid
            for (int x = iterXMin; x < iterXMax; x++)
            {
                for (int y = iterYMin; y < iterYMax; y++)
                {
                    RGB pixel = pixelGrid.getPixel(x, y);
                    if (pixel)
                    {
                        g.FillRectangle(
                        getBrush(RGBToColor(pixel)),
                        getRect(x, y)
                        );
                    }
                }
            }
            //Collider Grid
            if (colliderGrid == ActiveGrid)
            {
                //Put white filter over screen so far
                g.FillRectangle(
                    getBrush(Color.FromArgb(220, 255, 255, 255)),
                    getRect(iterXMin, iterYMin, iterXMax - iterXMin, iterYMax - iterYMin)
                    );
                //Draw collider grid on top
                for (int x = iterXMin; x < iterXMax; x++)
                {
                    for (int y = iterYMin; y < iterYMax; y++)
                    {
                        RGB pixel = colliderGrid.getPixel(x, y);
                        if (pixel && pixel != RGB.white)
                        {
                            Color baseColor = RGBToColor(pixel);
                            Color color = Color.FromArgb(100, baseColor.R, baseColor.G, baseColor.B);
                            g.FillRectangle(
                                getBrush(color),
                                getRect(x, y)
                                );
                        }
                    }
                }
            }
            else
            {
                //Collider Grid Borders
                for (int x = iterXMin; x < iterXMax; x++)
                {
                    for (int y = iterYMin; y < iterYMax; y++)
                    {
                        RGB pixel = colliderGrid.getPixel(x, y);
                        if (pixel)
                        {
                            addBorder(g, x, y);
                        }
                    }
                }
            }
            //Tool Grid
            for (int x = iterXMin; x < iterXMax; x++)
            {
                for (int y = iterYMin; y < iterYMax; y++)
                {
                    RGB pixel = toolGrid.getPixel(x, y);
                    if (pixel)
                    {
                        g.FillRectangle(
                            getBrush(RGBToColor(pixel)),
                            getRect(x, y)
                            );
                    }
                }
            }
            //Entity Grid
            for (int x = iterXMin; x < iterXMax; x++)
            {
                for (int y = iterYMin; y < iterYMax; y++)
                {
                    RGB pixel = entityGrid.getPixel(x, y);
                    if (pixel)
                    {
                        g.FillRectangle(
                            getBrush(RGBToColor(pixel)),
                            getRect(x, y)
                            );
                    }
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

        void addBorder(Graphics g, int gx, int gy)
        {
            RGB pixel = colliderGrid.getPixel(gx, gy);
            //Don't add borders to empty spaces
            if (!pixel || pixel == RGB.white)
            {
                return;
            }
            Rectangle rect = getRect(gx, gy);
            float lineWidthOffset = lighten.Width / 2;
            //Add white line at top of blocks with nothing above them
            if (colliderGrid.validPixel(gx, gy - 1))
            {
                RGB abovePixel = colliderGrid.getPixel(gx, gy - 1);
                if (!abovePixel || abovePixel == RGB.white)
                {
                    g.DrawLine(
                        lighten,
                        rect.Left,
                        rect.Top + lineWidthOffset,
                        rect.Right,
                        rect.Top + lineWidthOffset
                        );
                }
            }
            //Add white line to left of blocks with nothing left of them
            if (colliderGrid.validPixel(gx - 1, gy))
            {
                RGB leftPixel = colliderGrid.getPixel(gx - 1, gy);
                if (!leftPixel || leftPixel == RGB.white)
                {
                    g.DrawLine(
                        lighten,
                        rect.Left + lineWidthOffset,
                        rect.Top,
                        rect.Left + lineWidthOffset,
                        rect.Bottom
                        );
                }
            }
            //Add black line at bottom of blocks with nothing below them
            if (colliderGrid.validPixel(gx, gy + 1))
            {
                RGB belowPixel = colliderGrid.getPixel(gx, gy + 1);
                if (!belowPixel || belowPixel == RGB.white)
                {
                    g.DrawLine(
                        darken,
                        rect.Left,
                        rect.Bottom - lineWidthOffset,
                        rect.Right,
                        rect.Bottom - lineWidthOffset
                        );
                }
            }
            //Add black line at right of blocks with nothing right of them
            if (colliderGrid.validPixel(gx + 1, gy))
            {
                RGB rightPixel = colliderGrid.getPixel(gx + 1, gy);
                if (!rightPixel || rightPixel == RGB.white)
                {
                    g.DrawLine(
                        darken,
                        rect.Right - lineWidthOffset,
                        rect.Top,
                        rect.Right - lineWidthOffset,
                        rect.Bottom
                        );
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mouseDown = true;
            if (defaultPaintingEnabled)
            {
                lastMousePosition.x = e.X;
                lastMousePosition.y = e.Y;
                firstMousePosition.x = e.X;
                firstMousePosition.y = e.Y;
                if (ActiveGrid == colliderGrid)
                {
                    RGB curColor = colliderGrid.getPixel(gridPixelX(e.X), gridPixelY(e.Y));
                    drawColor = (curColor.isValid())
                        ? RGB.nullRGB
                        : RGB.black;
                }
                activeTool.preactivate(e.X, e.Y);
                activeTool.activate(e.X, e.Y);
                Focus();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseDown)
            {
                if (defaultPaintingEnabled)
                {
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
                activeTool.activate(e.X, e.Y);
                activeTool.postactivate(e.X, e.Y);
            }
            onPixelClicked?.Invoke(getColor(e.X, e.Y));
        }
        public delegate void OnPixelClicked(Color pixelColor);
        public OnPixelClicked onPixelClicked;

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int mgx = gridPixelX(e.X);
            int mgy = gridPixelY(e.Y);
            PixelSize += Math.Sign(e.Delta) * 2;
            centerOn(mgx, mgy, e.X, e.Y);
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Add(e.KeyCode);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            pressedKeys.Remove(e.KeyCode);
        }

        /// <summary>
        /// Scrolls the screen so that the given grid coordinate is placed at the given panel coordinate
        /// </summary>
        /// <param name="gx"></param>
        /// <param name="gy"></param>
        /// <param name="ex"></param>
        /// <param name="ey"></param>
        void centerOn(int gx, int gy, int ex, int ey)
        {
            int cgx = gridPixelX(ex);
            int cgy = gridPixelY(ey);
            Rectangle offset = getRect(gx, gy);
            mapPos.x -= offset.X - ex;
            mapPos.y -= offset.Y - ey;
        }

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
        public void checkSyncPixelGridToColliderGrid()
        {
            for (int x = 0; x < pixelGrid.Size.x; x++)
            {
                for (int y = 0; y < pixelGrid.Size.y; y++)
                {
                    bool pixelBlank = pixelGrid.getPixel(x, y) == RGB.white;
                    bool collBlank = !colliderGrid.getPixel(x, y).isValid();
                    if (pixelBlank != collBlank)
                    {
                        //They're not synced, so unsync it
                        syncPixelGridToColliderGrid = false;
                        return;
                    }
                }
            }
            //No asynchronicities found, so sync it
            syncPixelGridToColliderGrid = true;
            return;
        }
    }    
}