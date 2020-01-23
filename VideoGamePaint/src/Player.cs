using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Player : Entity
{
    public Color color = Color.Purple;

    public Player(PixelGrid pg) : base(pg)
    {
        //Up arrow rule
        rules.Add(new Rule(
            new Expression[2]
            {
                new KeyHeld(Keys.W),
                new EntityValue(this)
            },
            new Expression[3]
            {
                new MoveAction(),
                new EntityValue(this),
                new ConstantValue(Vector.up * 2)
            }
            ));
        //Down arrow rule
        rules.Add(new Rule(
            new Expression[2]
            {
                new KeyHeld(Keys.S),
                new EntityValue(this)
            },
            new Expression[3]
            {
                new MoveAction(),
                new EntityValue(this),
                new ConstantValue(Vector.down)
            }
            ));
        //Left arrow rule
        rules.Add(new Rule(
            new Expression[2]
            {
                new KeyHeld(Keys.A),
                new EntityValue(this)
            },
            new Expression[3]
            {
                new MoveAction(),
                new EntityValue(this),
                new ConstantValue(Vector.left)
            }
            ));
        //Right arrow rule
        rules.Add(new Rule(
            new Expression[2]
            {
                new KeyHeld(Keys.D),
                new EntityValue(this)
            },
            new Expression[3]
            {
                new MoveAction(),
                new EntityValue(this),
                new ConstantValue(Vector.right)
            }
            ));
    }
    public void applyGravity()
    {
        if (canMove(Vector.down))
        {
            move(Vector.down);
        }
    }

}
