

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

    public PixelGrid()
    {
        pixelGrid = new RGB[Size.x, Size.y];
        RGB white = new RGB(255, 255, 255);
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                pixelGrid[x, y] = white;
            }
        }
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
            //Create default color
            RGB white = new RGB(255, 255, 255);
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
                        newGrid[x, y] = white;
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
