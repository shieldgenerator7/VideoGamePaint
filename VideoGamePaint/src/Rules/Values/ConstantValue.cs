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
        valueString = valueString.Trim();
        string valueStringLower = valueString.Trim().ToLower();
        //Claim vector
        if (valueStringLower == "vectorup")
        {
            return Vector.up;
        }
        else if (valueStringLower == "vectordown")
        {
            return Vector.down;
        }
        else if (valueStringLower == "vectorleft")
        {
            return Vector.left;
        }
        else if (valueStringLower == "vectorright")
        {
            return Vector.right;
        }
        //Claim bool
        else if (valueStringLower == "true")
        {
            return true;
        }
        else if (valueStringLower == "false")
        {
            return false;
        }
        else
        {
            //Claim int
            int outInt;
            if (Int32.TryParse(valueStringLower, out outInt))
            {
                return outInt;
            }
            //Claim float
            float outFloat;
            if (float.TryParse(valueStringLower, out outFloat))
            {
                return outFloat;
            }
        }
        //String has not been claimed yet,
        //So claim it as its string value
        if (valueStringLower != null && valueStringLower != "")
        {
            return valueString;
        }
        return null;
    }



    public override Expression claimExpressionString(string exprStr)
    {
        base.claimExpressionString(exprStr);
        exprStr = exprStr.Trim();
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
            return new string[] { "VectorUp", "VectorDown", "VectorLeft", "VectorRight" };
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
