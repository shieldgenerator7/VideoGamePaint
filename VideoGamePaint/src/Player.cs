using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Player : Entity
{
    public Color color = Color.Purple;
    
    Rule movementRule;

    public Player(PixelGrid pg) : base(pg)
    {
        movementRule = new Rule(
            new Expression[4] {
                new MoveAction(),
                new EntityValue(this),
                new PlayerInputValue(),
                new EntityValue(this)
            }
            );
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
        inputDir = Vector.zero;
        if (keys.Contains(Keys.W))
        {
            inputDir.y += -2;
        }
        if (keys.Contains(Keys.A))
        {
            inputDir.x += -1;
        }
        if (keys.Contains(Keys.S))
        {
            inputDir.y += 1;
        }
        if (keys.Contains(Keys.D))
        {
            inputDir.x += 1;
        }
        movementRule.check();
    }
}
