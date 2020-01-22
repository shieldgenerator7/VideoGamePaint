using System;
using System.Collections.Generic;
using System.Drawing;
using VideoGamePaint;

public class FillTool : Tool
{
    protected Vector fillSparkPos;
    
    public FillTool(PixelGridPanel pgp) : base(pgp)
    {
    }

    public override void activate(int ex, int ey)
    {
        PixelGrid pg = pixelGridPanel.ActiveGrid;
        int gx = pixelGridPanel.gridPixelX(ex);
        int gy = pixelGridPanel.gridPixelY(ey);
        RGB baseColor = pg.getPixel(gx, gy);
        RGB toColor = pixelGridPanel.drawColor;
        if (baseColor != toColor)
        {
            fillSparkPos = new Vector(gx, gy);
            process(gx, gy, baseColor, toColor);
            pixelGridPanel.Invalidate();
        }
    }

    //abstracted out so subtypes can call fillArea() however they want
    protected virtual void process(int gx, int gy, RGB fromRGB, RGB toRGB)
    {
        fillArea(gx, gy, fromRGB, toRGB);
    }

    protected void fillArea(int gx, int gy, RGB fromRGB, RGB toRGB)
    {
        Queue<Vector> fillQueue = new Queue<Vector>();
        fillQueue.Enqueue(new Vector(gx, gy));
        while (fillQueue.Count > 0)
        {
            Vector gv = fillQueue.Dequeue();
            //Exit if the pixel doesn't meet the requirements
            if (!canFillPixel(gv.x, gv.y, fromRGB, toRGB))
            {
                continue;
            }
            //Set this pixel to the toRGB
            pixelGridPanel.updatePixel(gv.x, gv.y, toRGB);
            //Find the next pixels to set
            fillQueue.Enqueue(new Vector(gv.x - 1, gv.y));
            fillQueue.Enqueue(new Vector(gv.x + 1, gv.y));
            fillQueue.Enqueue(new Vector(gv.x, gv.y - 1));
            fillQueue.Enqueue(new Vector(gv.x, gv.y + 1));
        }
    }

    protected virtual bool canFillPixel(int gx, int gy, RGB fromRGB, RGB toRGB)
    {
        //No if outside the grid
        if (!pixelGridPanel.ActiveGrid.validPixel(gx,gy))
        {
            return false;
        }
        //No if no longer in the fromRGB area
        RGB rgb = pixelGridPanel.ActiveGrid.getPixel(gx, gy);
        if (rgb != fromRGB)
        {
            return false;
        }
        //Else yes
        return true;
    }
}
