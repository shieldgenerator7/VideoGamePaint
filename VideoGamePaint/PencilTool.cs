using System;
using VideoGamePaint;

public class PencilTool: Tool
{
	public PencilTool(PixelGridPanel pgp):base(pgp)
	{
	}

    public override void activate(int ex, int ey)
    {
        pixelGridPanel.updatePixelAtPosition(ex, ey, true);
    }
}
