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
        move(new Vector(0, 1));
    }

    public void applyControls(Keys key)
    {
        Vector dir = new Vector(0, 0);
        if (key == Keys.W)
        {
            dir.y = -2;
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
        move(dir);
    }

    private void move(Vector dir)
    {
        int gx = pos.x + dir.x;
        int gy = pos.y + dir.y;
        if (gx < 0 || gx >= collisionGrid.Size.x
            || gy < 0 || gy >= collisionGrid.Size.y
            )
        {
            return;
        }
        RGB rgb = collisionGrid.getPixel(gx, gy);
        if (rgb == RGB.white || rgb == null)
        {
            pos += dir;
        }
    }
}
