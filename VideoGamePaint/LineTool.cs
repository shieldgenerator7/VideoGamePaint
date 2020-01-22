using System;
using VideoGamePaint;

public class LineTool : Tool
{
    public LineTool(PixelGridPanel pgp) : base(pgp)
    {
    }

    public override void activate(int ex, int ey)
    {
        pixelGridPanel.toolGrid.clear(RGB.nullRGB);
        int gx = pixelGridPanel.gridPixelX(ex);
        int gy = pixelGridPanel.gridPixelY(ey);
        if (gx >= 0 && gx < pixelGridPanel.ActiveGrid.Size.x
            && gy >= 0 && gy < pixelGridPanel.ActiveGrid.Size.y)
        {
            foreach (Vector v in PixelGridPanel.getPixelsInBetween(
                pixelGridPanel.gridPixelX(ex),
                pixelGridPanel.gridPixelY(ey),
                pixelGridPanel.gridPixelX(pixelGridPanel.lastMousePosition.x),
                pixelGridPanel.gridPixelY(pixelGridPanel.lastMousePosition.y),
                0.2f
                ))
            {
                pixelGridPanel.toolGrid.setPixel(
                    v.x,
                    v.y,
                    PixelGridPanel.ColorToRGB(pixelGridPanel.drawColor)
                    );
            }
        }
        pixelGridPanel.Invalidate();
    }

    public override void postactivate(int ex, int ey)
    {
        base.postactivate(ex, ey);
        pixelGridPanel.toolGrid.clear(RGB.nullRGB);
        int gx = pixelGridPanel.gridPixelX(ex);
        int gy = pixelGridPanel.gridPixelY(ey);
        if (gx >= 0 && gx < pixelGridPanel.ActiveGrid.Size.x
            && gy >= 0 && gy < pixelGridPanel.ActiveGrid.Size.y)
        {
            foreach (Vector v in PixelGridPanel.getPixelsInBetween(
                pixelGridPanel.gridPixelX(ex),
                pixelGridPanel.gridPixelY(ey),
                pixelGridPanel.gridPixelX(pixelGridPanel.lastMousePosition.x),
                pixelGridPanel.gridPixelY(pixelGridPanel.lastMousePosition.y),
                0.2f
            ))
            {
                pixelGridPanel.updatePixel(
                    v.x,
                    v.y,
                    PixelGridPanel.ColorToRGB(pixelGridPanel.drawColor)
                    );
            }
        }
        pixelGridPanel.Invalidate();
    }
}
