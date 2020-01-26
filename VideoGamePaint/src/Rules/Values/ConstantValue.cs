using System;

public class ConstantValue : AnyTypeValue
{
    private object _value;
    protected override object value
    {
        get => _value;
        set => _value = value;
    }

    public ConstantValue(string valueString)
    {
        this.value = getObjectFromString(valueString);
    }

    public ConstantValue(object value)
    {
        this.value = value;
    }

    public override Entity toEntity()
    {
        throw new NotImplementedException(
            "Expression " + this + " does not implement toEntity()." +
            " This method should not be called on this expression type."
            );
    }

    public static object getObjectFromString(string valueString)
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
        //String has not been claimed yet,
        //So claim it as its string value
        if (valueString != null && valueString != "")
        {
            return valueString;
        }
        return null;
    }



    public override Expression claimExpressionString(string exprStr)
    {
        base.claimExpressionString(exprStr);
        exprStr = exprStr.ToLower();
        object strObj = getObjectFromString(exprStr);
        if (strObj != null)
        {
            return new ConstantValue(strObj);
        }
        return null;
    }

    public override string TokenName => "Constant";
    public override int ConstructorParameterCount => 1;
    public ConstantValue()
    {
        isMeta = true;
    }
}
