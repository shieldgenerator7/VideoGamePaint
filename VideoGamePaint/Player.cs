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

    public void applyControls(Keys key)
    {
        if (key == Keys.W)
        {
            moveDir.y += -2;
        }
        if (key == Keys.A)
        {
            moveDir.x += -1;
        }
        if (key == Keys.S)
        {
            moveDir.y += 1;
        }
        if (key == Keys.D)
        {
            moveDir.x += 1;
        }
    }

    public void move()
    {
        int gx = pos.x + moveDir.x;
        int gy = pos.y + moveDir.y;
        Vector lastValidPoint = Vector.copy(pos);
        foreach (Vector v in collisionGrid.getPixelsInLine(pos, pos + moveDir))
        {
            if (v.x < 0 || v.x >= collisionGrid.Size.x
                || v.y < 0 || v.y >= collisionGrid.Size.y
                )
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
    }
}
