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

    public virtual void preactivate(int ex, int ey)
    {

    }

    public virtual void postactivate(int ex, int ey)
    {

    }
}
