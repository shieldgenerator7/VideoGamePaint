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
        int gx = pixelGridPanel.gridPixelX(ex);
        int gy = pixelGridPanel.gridPixelY(ey);
        RGB baseColor = pg.getPixel(gx, gy);
        RGB toColor = PixelGridPanel.ColorToRGB(pixelGridPanel.drawColor);
        if (baseColor != toColor)
        {
            fillArea(gx, gy, baseColor, toColor);
            pixelGridPanel.Invalidate();
        }
    }

    private void fillArea(int gx, int gy, RGB fromRGB, RGB toRGB)
    {
        //Exit if the pixel doesn't meet the requirements
        if (!canFillPixel(gx, gy, fromRGB))
        {
            return;
        }
        //Set this pixel to the toRGB
        pixelGridPanel.pixelGrid.setPixel(gx, gy, toRGB);
        //Find the next pixels to set
        fillArea(gx - 1, gy, fromRGB, toRGB);
        fillArea(gx + 1, gy, fromRGB, toRGB);
        fillArea(gx, gy - 1, fromRGB, toRGB);
        fillArea(gx, gy + 1, fromRGB, toRGB);
    }

    protected virtual bool canFillPixel(int gx, int gy, RGB fromRGB)
    {
        //No if outside the grid
        if (gx < 0 || gx >= pixelGridPanel.pixelGrid.Size.x
            || gy < 0 || gy >= pixelGridPanel.pixelGrid.Size.y)
        {
            return false;
        }
        //No if no longer in the fromRGB area
        RGB rgb = pixelGridPanel.pixelGrid.getPixel(gx, gy);
        if (rgb != fromRGB)
        {
            return false;
        }
        //Else yes
        return true;
    }
}
