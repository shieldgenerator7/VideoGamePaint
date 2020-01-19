using System;
using System.Drawing;
using VideoGamePaint;

public class FillTool : Tool
{
    public FillTool(PixelGridPanel pgp) : base(pgp)
    {
    }

    public override void activate(int ex, int ey)
    {
        PixelGrid pg = pixelGridPanel.pixelGrid;
        RGB baseColor = pg.getPixel(
            pixelGridPanel.gridPixelX(ex),
            pixelGridPanel.gridPixelY(ey)
            );
        for (int x = 0; x < pg.Size.x; x++)
        {
            for (int y = 0; y < pg.Size.y; y++)
            {
                if (pg.getPixel(x, y) == baseColor)
                {
                    pg.setPixel(
                        x,
                        y,
                        PixelGridPanel.ColorToRGB(pixelGridPanel.drawColor)
                        );
                }
            }
        }
        pixelGridPanel.Invalidate();
    }
}
