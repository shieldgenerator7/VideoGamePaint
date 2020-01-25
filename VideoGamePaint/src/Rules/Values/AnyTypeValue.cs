using System;

public abstract class AnyTypeValue:Expression
{
    protected virtual object value { get => 0; set { } }

    public override bool isValue { get => true; }
    public override object toValue()
    {
        return value;
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

    public override bool isBool { get => value is bool; }
    public override bool toBool()
    {
        return (bool)value;
    }

    public override bool isString { get => value is string; }
    public override string toString()
    {
        return (string)value;
    }

    public override bool isVector { get => value is Vector; }
    public override Vector toVector()
    {
        return (Vector)value;
    }

    public override bool isEntity { get => value is Entity; }
    public override Entity toEntity()
    {
        return (Entity)value;
    }
}
