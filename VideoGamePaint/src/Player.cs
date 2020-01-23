using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Player : Entity
{
    public Color color = Color.Purple;

    public Player(PixelGrid pg) : base(pg)
    {
    }

    public void applyGravity()
    {
        if (canMove(Vector.down))
        {
            move(Vector.down);
        }
    }

    public void applyControls(List<Keys> keys)
    {
        Vector moveDir = Vector.zero;
        if (keys.Contains(Keys.W))
        {
            moveDir.y += -2;
        }
        if (keys.Contains(Keys.A))
        {
            moveDir.x += -1;
        }
        if (keys.Contains(Keys.S))
        {
            moveDir.y += 1;
        }
        if (keys.Contains(Keys.D))
        {
            moveDir.x += 1;
        }
        move(moveDir);
    }
}
