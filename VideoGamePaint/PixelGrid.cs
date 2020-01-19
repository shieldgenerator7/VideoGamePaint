

public class PixelGrid
{
    private Vector size = new Vector(100, 100);
    public Vector Size
    {
        get => size;
        set
        {
            //If the new size is bigger,
            if (size.x < value.x || size.y < value.y)
            {
                //Make new grid
                RGB[,] grid = new RGB[value.x, value.y];
                //Copy old grid into new grid
                for (int x = 0; x < size.x; x++)
                {
                    for (int y = 0; y < size.y; y++)
                    {
                        grid[x, y] = pixelGrid[x, y];
                    }
                }
                //Set old grid to new grid
                this.pixelGrid = grid;
            }
            //Set size
            size = value;
        }
    }
    RGB[,] pixelGrid;
    //List<List<Color>> pixelGrid;

    public PixelGrid()
    {
        pixelGrid = new RGB[size.x, size.y];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                pixelGrid[x, y] = new RGB(255, 255, 255);
            }
        }
    }

    public RGB getPixel(int x, int y)
    {
        return pixelGrid[x, y];
    }

    /// <summary>
    /// Sets the pixel at the given position in the grid
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void setPixel(int px, int py, RGB rgb)
    {
        pixelGrid[px, py] = rgb;
    }
}
