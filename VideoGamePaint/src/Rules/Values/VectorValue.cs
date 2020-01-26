using System;

public class VectorValue : Expression
{

    public override int parameterCount => 2;
    protected override Type[] _getParameterTypeList()
    {
        return new Type[2]
        {
            typeof(int),
            typeof(int)
        };
    }

    public override bool isVector => true;
    public override Vector toVector()
    {
        int arg1 = arguments[0].toInteger();
        int arg2 = arguments[1].toInteger();
        return new Vector(arg1, arg2);
    }

    public override string TokenName => "Vector";
}
