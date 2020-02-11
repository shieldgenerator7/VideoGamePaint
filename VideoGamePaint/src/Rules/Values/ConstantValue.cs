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
        //Claim vector
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
        //Claim bool
        else if (valueString == "true")
        {
            return true;
        }
        else if (valueString == "false")
        {
            return false;
        }
        else
        {
            //Claim int
            int outInt;
            if (Int32.TryParse(valueString, out outInt))
            {
                return outInt;
            }
            //Claim float
            float outFloat;
            if (float.TryParse(valueString, out outFloat))
            {
                return outFloat;
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

    public override string[] getConstantNames(Type type)
    {
        base.getConstantNames(type);
        if (type == typeof(Vector))
        {
            return new string[] { "VectorUp","VectorDown","VectorLeft","VectorRight" };
        }
        else if (type == typeof(Boolean))
        {
            return new string[] { "True", "False" };
        }
        else if (type == typeof(Int32)
            || type == typeof(float)
            || type == typeof(string)
            || type == typeof(object))
        {
            return new string[] { "INFINITY" };
        }
        return null;
    }

    public override string TokenName => "Constant";
    public override int ConstructorParameterCount => 1;
    public override bool canAcceptConstructorArgument(string arg)
    {
        return true;
    }
    public ConstantValue()
    {
        isMeta = true;
    }
}
