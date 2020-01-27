using System;

public abstract class AnyTypeValue:Expression
{
    protected virtual object value { get => 0; set { } }

    public override bool isValue { get => true; }
    public override object toValue()
    {
        return value;
    }

    public override bool isInteger { get => isMeta || value is int; }
    public override int toInteger()
    {
        return (int)value;
    }

    public override bool isFloat { get => isMeta || value is float; }
    public override float toFloat()
    {
        return (float)value;
    }

    public override bool isBool { get => isMeta || value is bool; }
    public override bool toBool()
    {
        return (bool)value;
    }

    public override bool isString { get => isMeta || value is string; }
    public override string toString()
    {
        return (string)value;
    }

    public override bool isVector { get => isMeta || value is Vector; }
    public override Vector toVector()
    {
        return (Vector)value;
    }

    public override bool isEntity { get => isMeta || value is Entity; }
    public override Entity toEntity()
    {
        return (Entity)value;
    }
}
