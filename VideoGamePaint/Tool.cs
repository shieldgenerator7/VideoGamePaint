using System;
using VideoGamePaint;

public abstract class Tool
{
    protected PixelGridPanel pixelGridPanel;

    public Tool(PixelGridPanel pixelGridPanel)
	{
        this.pixelGridPanel = pixelGridPanel;
    }

    public abstract void activate(int ex, int ey);

}
