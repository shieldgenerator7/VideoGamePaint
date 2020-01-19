using System;
using VideoGamePaint;

public class HandTool : Tool
{
    Vector firstMapPos = new Vector(0, 0);

    public HandTool(PixelGridPanel pgp) : base(pgp)
    {
    }

    public override void preactivate(int ex, int ey)
    {
        base.preactivate(ex, ey);
        firstMapPos.x = pixelGridPanel.mapPos.x;
        firstMapPos.y = pixelGridPanel.mapPos.y;
    }

    public override void activate(int ex, int ey)
    {
        pixelGridPanel.mapPos.x = firstMapPos.x + (ex - pixelGridPanel.firstMousePosition.x);
        pixelGridPanel.mapPos.y = firstMapPos.y + (ey - pixelGridPanel.firstMousePosition.y);
        pixelGridPanel.Invalidate();
    }
}
