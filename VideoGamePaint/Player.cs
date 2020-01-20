using System;
using System.Drawing;
using System.Windows.Forms;

public class Player
{
    public Vector pos = new Vector(0, 0);
    public Color color = Color.Purple;

    PixelGrid collisionGrid;

	public Player(PixelGrid pg)
	{
        this.collisionGrid = pg;
	}

    public void applyGravity()
    {
        pos.y += 1;
    }

    public void applyControls(Keys key)
    {
        Vector dir = new Vector(0, 0);
        if (key == Keys.W)
        {
            dir.y = -1;
        }
        if (key == Keys.A)
        {
            dir.x = -1;
        }
        if (key == Keys.S)
        {
            dir.y = 1;
        }
        if (key == Keys.D)
        {
            dir.x = 1;
        }
        RGB rgb = collisionGrid.getPixel(pos.x + dir.x, pos.y + dir.y);
        if (rgb == RGB.white || rgb == null)
        {
            pos += dir;
        }
    }
}
