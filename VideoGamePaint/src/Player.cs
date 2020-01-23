using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Player : Entity
{
    public Color color = Color.Purple;

    public Player(PixelGrid pg) : base(pg)
    {
        EntityValue entityValue = new EntityValue(this);
        //Up arrow rule
        rules.Add(new Rule(
            new Expression[4]
            {
                new KeyHeld(Keys.W),
                entityValue,
                new GroundedValue(),
                entityValue
            },
            new Expression[3]
            {
                new MoveAction(),
                entityValue,
                new ConstantValue(Vector.up * 2)
            }
            ));
        //Down arrow rule
        rules.Add(new Rule(
            new Expression[2]
            {
                new KeyHeld(Keys.S),
                entityValue
            },
            new Expression[3]
            {
                new MoveAction(),
                entityValue,
                new ConstantValue(Vector.down)
            }
            ));
        //Left arrow rule
        rules.Add(new Rule(
            new Expression[2]
            {
                new KeyHeld(Keys.A),
                entityValue
            },
            new Expression[3]
            {
                new MoveAction(),
                entityValue,
                new ConstantValue(Vector.left)
            }
            ));
        //Right arrow rule
        rules.Add(new Rule(
            new Expression[2]
            {
                new KeyHeld(Keys.D),
                entityValue
            },
            new Expression[3]
            {
                new MoveAction(),
                entityValue,
                new ConstantValue(Vector.right)
            }
            ));
        //Gravity Rule
        rules.Add(new Rule(
            new Expression[3]
            {
                new NotOperator(),
                new GroundedValue(),
                entityValue
            },
            new Expression[3]
            {
                new MoveAction(),
                entityValue,
                new ConstantValue(Vector.down)
            }
            ));
    }

}
