using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Player : Entity
{
    public Color color = Color.Purple;

    MoveAction moveAction;
    EntityValue entityValue;
    PlayerInputValue playerInputValue;

    public Player(PixelGrid pg) : base(pg)
    {
        moveAction = new MoveAction();
        entityValue = new EntityValue(this);
        playerInputValue = new PlayerInputValue();
        moveAction.arguments = new Expression[2] { entityValue, playerInputValue };
        playerInputValue.arguments = new Expression[1] { entityValue };
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
        moveAction.runFunction();
    }
}
