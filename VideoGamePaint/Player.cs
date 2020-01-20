using System;
using System.Drawing;
using System.Windows.Forms;

public class Player
{
    public Vector pos = new Vector(0, 0);
    public Color color = Color.Purple;

	public Player()
	{
	}

    public void applyGravity()
    {
        pos.y += 1;
    }

    public void applyControls(Keys key)
    {
        if (key == Keys.W)
        {
            pos.y -= 1;
        }
        if (key == Keys.A)
        {
            pos.x -= 1;
        }
        if (key == Keys.S)
        {
            pos.y += 1;
        }
        if (key == Keys.D)
        {
            pos.x += 1;
        }
    }
}
