using System;

public class PixelGrid
{
    public int GRID_SIZE { get; private set; } = 100;
    RGB[,] pixelGrid;
    //List<List<Color>> pixelGrid;

    public PixelGrid()
	{
        pixelGrid = new RGB[GRID_SIZE, GRID_SIZE];
        for (int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                Random r = new Random(x * y);
                pixelGrid[x, y] = new RGB(
                    r.Next() % 256,
                    r.Next() % 256,
                    r.Next() % 256
                    );
            }
        }
    }

    public RGB getPixel(int x, int y)
    {
        return pixelGrid[x, y];
    }

    /// <summary>
    /// Updates the pixel at the given position in the grid
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void updatePixel(int px, int py, RGB rgb)
    {
        pixelGrid[px, py] = rgb;
    }
}
