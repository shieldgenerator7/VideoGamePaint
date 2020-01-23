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
            "Key W Entity Player, Grounded Entity Player;" +
            "Move Entity Player Constant VectorUp"
            ));
        //Down arrow rule
        rules.Add(RuleBuilder.buildRule(
            "Key S Entity Player;" +
            "Move Entity Player Constant VectorDown"
            ));
        //Left arrow rule
        rules.Add(RuleBuilder.buildRule(
            "Key A Entity Player;" +
            "Move Entity Player Constant VectorLeft"
            ));
        //Right arrow rule
        rules.Add(RuleBuilder.buildRule(
            "Key D Entity Player;" +
            "Move Entity Player Constant VectorRight"
            ));
        //Gravity Rule
        rules.Add(RuleBuilder.buildRule(
            "Not Grounded Entity Player;" +
            "Move Entity Player Constant VectorDown"
            ));
    }

}
