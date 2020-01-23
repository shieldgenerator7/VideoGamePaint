using System;

public class ConstantValue:Expression
{
    object value = 0;

    public ConstantValue(string valueString)
    {
        valueString = valueString.Trim().ToLower();
        if (valueString == "vectorup")
        {
            this.value = Vector.up;
        }
        else if (valueString == "vectordown")
        {
            this.value = Vector.down;
        }
        else if (valueString == "vectorleft")
        {
            this.value = Vector.left;
        }
        else if (valueString == "vectorright")
        {
            this.value = Vector.right;
        }
        else
        {
            int outInt;
            if (Int32.TryParse(valueString, out outInt))
            {
                this.value = outInt;
            }
        }
    }

    public ConstantValue(object value)
	{
        this.value = value;
	}

    public override bool isInteger { get => value is int; }
    public override int toInteger()
    {
        return (int)value;
    }

    public override bool isFloat { get => value is float; }
    public override float toFloat()
    {
        return (float)value;
    }

    public override bool isBool{ get => value is bool; }
    public override bool toBool()
    {
        return (bool)value;
    }

    public override bool isVector { get => value is Vector; }
    public override Vector toVector()
    {
        return (Vector)value;
    }
}
