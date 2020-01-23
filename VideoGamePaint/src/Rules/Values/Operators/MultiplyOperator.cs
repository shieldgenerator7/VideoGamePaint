using System;

public class MultiplyOperator:Expression
{
    public override int parameterCount { get => 2; }
    protected override Type[] _getParameterTypeList()
    {
        return new Type[2]
        {
            typeof(Vector),
            typeof(int)
        };
    }

    public override bool isVector { get => true; }
    public override Vector toVector()
    {
        Vector vector = arguments[0].toVector();
        int intArg = arguments[1].toInteger();
        return vector * intArg;
    }
}
