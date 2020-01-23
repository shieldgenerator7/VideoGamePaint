using System;

public class ConstantValue : Expression
{
    object value = 0;

    public ConstantValue(string valueString)
    {
        this.value = getObjectFromString(valueString);
    }

    public ConstantValue(object value)
    {
        this.value = value;
    }

    private static object getObjectFromString(string valueString)
    {
        valueString = valueString.Trim().ToLower();
        if (valueString == "vectorup")
        {
            return Vector.up;
        }
        else if (valueString == "vectordown")
        {
            return Vector.down;
        }
        else if (valueString == "vectorleft")
        {
            return Vector.left;
        }
        else if (valueString == "vectorright")
        {
            return Vector.right;
        }
        else
        {
            int outInt;
            if (Int32.TryParse(valueString, out outInt))
            {
                return outInt;
            }
        }
        return null;
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

    public override bool isVector { get => value is Vector; }
    public override Vector toVector()
    {
        return (Vector)value;
    }

    public static Expression claimExpressionString(string exprStr)
    {
        exprStr = exprStr.ToLower();
        object strObj = getObjectFromString(exprStr);
        if (strObj != null)
        {
            return new ConstantValue(strObj);
        }
        return null;
    }
}
