using System;
using VideoGamePaint;

public class LightBulbFillTool : FillTool
{
    bool checkLOS = true;

    public LightBulbFillTool(PixelGridPanel pgp) : base(pgp)
    {
    }

    protected override void process(int gx, int gy, RGB fromRGB, RGB toRGB)
    {
        //2020-01-19
        //Filling the area with temp RGBs makes calculations
        //in certain situations easier.
        //This of course assumes that the tempRGB value
        //is not already in the grid somwhere.
        RGB tempRGB = null;
        checkLOS = true;
        fillArea(gx, gy, fromRGB, tempRGB);
        checkLOS = false;
        fillArea(gx, gy, tempRGB, toRGB);
    }

    protected override bool canFillPixel(int gx, int gy, RGB fromRGB, RGB toRGB)
    {
        bool canFill = base.canFillPixel(gx, gy, fromRGB, toRGB);
        if (!canFill)
        {
            return false;
        }
        //Return true if not required to check line of sight 
        if (!checkLOS)
        {
            return true;
        }
        //Exit if there's no line of sight to beginning
        foreach (Vector v in PixelGridPanel.getPixelsInBetween(
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
