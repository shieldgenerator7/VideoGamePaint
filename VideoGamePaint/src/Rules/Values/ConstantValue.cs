using System;

public class ConstantValue:Expression
{
    object value = 0;

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
