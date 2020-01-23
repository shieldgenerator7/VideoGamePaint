using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Player : Entity
{
    public static Player instance;
    public Color color = Color.Purple;

    public Player(PixelGrid pg) : base(pg)
    {
        if (instance == null)
        {
            instance = this;
        }
        //Up arrow rule
        rules.Add(RuleBuilder.buildRule(
            "Key W Player, Grounded Player;" +
            "Move Player Multiply 3 VectorUp"
            ));
        //Down arrow rule
        rules.Add(RuleBuilder.buildRule(
            "Key S Player;" +
            "Move Player VectorDown"
            ));
        //Left arrow rule
        rules.Add(RuleBuilder.buildRule(
            "Key A Player;" +
            "Move Player VectorLeft"
            ));
        //Right arrow rule
        rules.Add(RuleBuilder.buildRule(
            "Key D Player;" +
            "Move Player VectorRight"
            ));
        //Gravity Rule
        rules.Add(RuleBuilder.buildRule(
            "Not Grounded Player;" +
            "Move Player VectorDown"
            ));
    }

}
