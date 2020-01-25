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
        setVariableNames(new List<string>() { "JumpHeight", "Health" });
    }

}
