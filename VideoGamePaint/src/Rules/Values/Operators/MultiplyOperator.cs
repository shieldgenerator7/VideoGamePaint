using System;

public class MultiplyOperator : Operator
{
    public MultiplyOperator()
    {

    }

    public override int parameterCount { get => 2; }
    protected override int signatureCount { get => 3; }
    protected override Type[] _getParameterTypeList(int signatureIndex)
    {
        switch (signatureIndex)
        {
            case 0:
                return new Type[2]
                {
                    typeof(int),
                    typeof(int)
                };
            case 1:
                return new Type[2]
                {
                    typeof(Vector),
                    typeof(int)
                };
            case 2:
                return new Type[2]
                {
                    typeof(int),
                    typeof(Vector)
                };
        }
        return null;
    }

    public override bool isVector
    {
        get
        {
            if (isMeta)
            {
                return true;
            }
            return arguments[0].isVector
                || arguments[1].isVector;
        }
    }
    public override Vector toVector()
    {
        Vector vector = (arguments[0].isVector)?arguments[0].toVector():arguments[1].toVector();
        int intArg = (arguments[0].isInteger) ? arguments[0].toInteger() : arguments[1].toInteger();
        return vector * intArg;
    }

    public override bool isInteger
    {
        get
        {
            if (arguments == null)
            {
                return true;
            }
            return arguments[0].isInteger
                && arguments[1].isInteger;
        }
    }
    public override int toInteger()
    {
        return arguments[0].toInteger() * arguments[1].toInteger();
    }

    public override string TokenName => "Multiply";
}
