using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Player
{
    public Vector pos = new Vector(0, 0);
    public Color color = Color.Purple;
    private Vector moveDir = new Vector(0, 0);//how much it moves each frame

    PixelGrid collisionGrid;

    public Player(PixelGrid pg)
    {
        this.collisionGrid = pg;
    }

    public void applyGravity()
    {
        if (pos.y < collisionGrid.Size.y - 1)
        {
            RGB rgb = collisionGrid.getPixel(pos.x, pos.y + 1);
            if (rgb == RGB.white || rgb == null)
            {
                moveDir.y += 1;
            }
        }
    }

    public void applyControls(List<Keys> keys)
    {
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
    }

    public void move()
    {
        int gx = pos.x + moveDir.x;
        int gy = pos.y + moveDir.y;
        Vector endPos = pos + moveDir;
        Vector lastValidPoint = Vector.copy(pos);
        foreach (Vector v in collisionGrid.getPixelsInLine(pos, endPos))
        {
            if (!collisionGrid.validPixel(v))
            {
                break;
            }
            RGB rgb = collisionGrid.getPixel(v.x, v.y);
            if (rgb == RGB.white || rgb == null)
            {
                lastValidPoint.copyFrom(v);
            }
            else
            {
                break;
            }
        }
        pos.copyFrom(lastValidPoint);
        moveDir.copyFrom(Vector.zero);
        //Continue: Slide player in valid direction
        if (lastValidPoint != endPos)
        {
            int rise = endPos.y - lastValidPoint.y;
            int run = endPos.x - lastValidPoint.x;
            Vector[] tryDirs = new Vector[2];
            if (Math.Abs(run) >= Math.Abs(rise))
            {
                tryDirs[0] = new Vector(run, 0);
                tryDirs[1] = new Vector(0, rise);
            }
            else
            {
                tryDirs[0] = new Vector(0, rise);
                tryDirs[1] = new Vector(run, 0);
            }
            for (int i = 0; i < tryDirs.Length; i++)
            {
                Vector nextPos = lastValidPoint + new Vector(
                    Math.Sign(tryDirs[i].x),
                    Math.Sign(tryDirs[i].y)
                    );
                if (collisionGrid.validPixel(nextPos))
                {
                    RGB rgb = collisionGrid.getPixel(nextPos);
                    if (rgb == RGB.white || rgb == null)
                    {
                        moveDir.copyFrom(tryDirs[i]);
                        move();
                        return;
                    }
                }
            }
        }
    }
}
