using System;
using VideoGamePaint;

public class LightBulbFillTool : FillTool
{
    public LightBulbFillTool(PixelGridPanel pgp) : base(pgp)
    {
    }

    protected override bool canFillPixel(int gx, int gy, RGB fromRGB, RGB toRGB)
    {
        bool canFill = base.canFillPixel(gx, gy, fromRGB, toRGB);
        if (!canFill)
        {
            return false;
        }
        //Exit if there's no line of sight to beginning
        foreach (Vector v in pixelGridPanel.getPixelsInBetween(
            fillSparkPos.x,
            fillSparkPos.y,
            gx,
            gy,
            0.4f
            ))
        {
            RGB rgb = pixelGridPanel.pixelGrid.getPixel(v.x, v.y);
            if (v.x != gx || v.y != gy)
            {
                if (rgb != fromRGB && rgb != toRGB)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
