using System;
using System.Windows.Forms;
using VideoGamePaint;

public class HandTool : Tool
{
    Vector firstMapPos = new Vector(0, 0);
    Tool prevTool = null;

    public HandTool(PixelGridPanel pgp) : base(pgp)
    {
        pixelGridPanel.MouseDown += overtakeMouseDown;
    }

    private void overtakeMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Middle)
        {
            prevTool = pixelGridPanel.activeTool;
            pixelGridPanel.activeTool = this;
        }
        else
        {
            prevTool = null;
        }
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

    public override void postactivate(int ex, int ey)
    {
        base.postactivate(ex, ey);
        if (prevTool)
        {
            pixelGridPanel.activeTool = prevTool;
            prevTool = null;
        }
    }
}
