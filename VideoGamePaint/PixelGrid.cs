

using System;

public class PixelGrid
{
    const int EXPAND_BUFFER = 50;//how much additional space to create if expanding the grid

    Vector gridOrigin = new Vector(0, 0);//the grid position that is 0,0

    private Vector _size = new Vector(100, 100);
    public Vector Size
    {
        get => _size;
    }
    private Vector memorySize = new Vector(100, 100);//the actual size of the grid, including inaccesible areas

    RGB[,] pixelGrid;
    //List<List<Color>> pixelGrid;

    public RGB defaultFillRGB;

    public PixelGrid()
    {
        pixelGrid = new RGB[Size.x, Size.y];
        defaultFillRGB = new RGB(255, 255, 255);
        clear(defaultFillRGB);
    }

    public RGB getPixel(Vector v)
    {
        return getPixel(v.x, v.y);
    }

    public RGB getPixel(int x, int y)
    {
        try
        {
            return pixelGrid[gridOrigin.x + x, gridOrigin.y + y];
        }
        catch (IndexOutOfRangeException ioore)
        {
            throw new IndexOutOfRangeException(""
                + (gridOrigin.x + x) + " / " + Size.x + ", "
                + (gridOrigin.y + y) + " / " + Size.y + ". "
                + ioore.Message
                );
        }
    }

    /// <summary>
    /// Sets the pixel at the given position in the grid
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void setPixel(int px, int py, RGB rgb)
    {
        try
        {
            pixelGrid[gridOrigin.x + px, gridOrigin.y + py] = rgb;
        }
        catch (IndexOutOfRangeException ioore)
        {
            throw new IndexOutOfRangeException(""
                + (gridOrigin.x + px) + " / " + Size.x + ", "
                + (gridOrigin.y + py) + " / " + Size.y + ". "
                + ioore.Message
                );
        }
    }

    public void clear(RGB rgb)
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                setPixel(x, y, rgb);
            }
        }
    }


    public Vector[] getPixelsInLine(Vector v1, Vector v2)
    {
        return getPixelsInLine(v1.x, v1.y, v2.x, v2.y);
    }
    /// <summary>
    /// Returns the vectors starting from (gx1,gy1) to (gx2,gy2), in that direction
    /// </summary>
    /// <param name="gx1"></param>
    /// <param name="gy1"></param>
    /// <param name="gx2"></param>
    /// <param name="gy2"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public Vector[] getPixelsInLine(int gx1, int gy1, int gx2, int gy2)
    {
        Vector[] vectors;

        int rise = gy2 - gy1;
        int run = gx2 - gx1;

        int xDir = Math.Sign(run);
        int yDir = Math.Sign(rise);

        //If the two coordinates are the same,
        if (run == 0 && rise == 0)
        {
            //Return the first one
            vectors = new Vector[1];
            vectors[0] = new Vector(gx1, gy1);
            return vectors;
        }

        //More horizontal than vertical
        if (Math.Abs(run) >= Math.Abs(rise))
        {
            vectors = new Vector[Math.Abs(run) + 1];
            int offset = gy2 - (gx2 * rise / run);
            int i = 0;
            for (int x = gx1; x != gx2 + xDir; x += xDir)
            {
                int y = (int)Math.Round((float)(x * rise / run) + offset);
                y = clamp(y, gy1, gy2);
                vectors[i] = new Vector(x, y);
                i++;
            }
        }
        //More vertical than horizontal
        else
        {
            vectors = new Vector[Math.Abs(rise) + 1];
            int offset = gx2 - (gy2 * run / rise);
            int i = 0;
            for (int y = gy1; y != gy2 + yDir; y += yDir)
            {
                int x = (int)Math.Round((float)(y * run / rise) + offset);
                x = clamp(x, gx1, gx2);
                vectors[i] = new Vector(x, y);
                i++;
            }
        }
        return vectors;
    }

    /// <summary>
    /// Returns a value between the two bounds. Bounds can be in any order.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="bound1"></param>
    /// <param name="bound2"></param>
    public static int clamp(int value, int bound1, int bound2)
    {
        int min = Math.Min(bound1, bound2);
        int max = Math.Max(bound1, bound2);
        return Math.Max(Math.Min(value, max), min);
    }

    /// <summary>
    /// Expands the grid in the given directions.
    /// </summary>
    /// <param name="dx">less than 0 to add empty space to the left, greater than 0 to add empty space to the right, 0 to not add empty space left or right.</param>
    /// <param name="dy">less than 0 to add empty space above, greater than 0 to add empty space below, 0 to not add empty space above or below.</param>
    public void expandGrid(int dx, int dy)
    {

        //Create new size
        Vector newSize = new Vector(Math.Abs(dx) + Size.x, Math.Abs(dy) + Size.y);
        //Expand into existing grid area, if possible
        if (dx < 0 && gridOrigin.x >= Math.Abs(dx))
        {
            gridOrigin.x += dx;
            dx = 0;
        }
        if (dy < 0 && gridOrigin.y >= Math.Abs(dy))
        {
            gridOrigin.y += dy;
            dy = 0;
        }
        //Create new memory size, if needed
        Vector expandSize = memorySize + new Vector(
            Math.Abs(dx) + ((dx != 0) ? EXPAND_BUFFER : 0),
            Math.Abs(dy) + ((dy != 0) ? EXPAND_BUFFER : 0)
            );
        if (expandSize > memorySize)
        {
            //Make the new grid
            RGB[,] newGrid = new RGB[expandSize.x, expandSize.y];
            //Set copy destination start position
            Vector copyStart = new Vector(
                (dx < 0) ? EXPAND_BUFFER + Math.Abs(dx) : gridOrigin.x,
                (dy < 0) ? EXPAND_BUFFER + Math.Abs(dy) : gridOrigin.y
                );
            //Copy old grid into new grid
            for (int x = 0; x < expandSize.x; x++)
            {
                for (int y = 0; y < expandSize.y; y++)
                {
                    //If the coordinate is in the copy destination area,
                    if (x >= copyStart.x && x < copyStart.x + Size.x
                        && y >= copyStart.y && y < copyStart.y + Size.y)
                    {
                        //Copy the pixel
                        newGrid[x, y] = getPixel(x - copyStart.x, y - copyStart.y);
                    }
                    //Else,
                    else
                    {
                        //Put in the default color
                        newGrid[x, y] = defaultFillRGB;
                    }
                }
            }
            //Set old grid to new grid
            pixelGrid = newGrid;
            gridOrigin.x = (dx < 0) ? EXPAND_BUFFER : gridOrigin.x;
            gridOrigin.y = (dy < 0) ? EXPAND_BUFFER : gridOrigin.y;
            //Update memory size
            memorySize.x = expandSize.x;
            memorySize.y = expandSize.y;
        }
        //Update size width and height
        Size.x = newSize.x;
        Size.y = newSize.y;
    }
}
