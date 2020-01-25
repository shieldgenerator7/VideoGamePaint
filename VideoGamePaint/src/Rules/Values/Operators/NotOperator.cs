using System;

public class NotOperator : Operator
{
    public override int parameterCount { get => 1; }
    protected override Type[] _getParameterTypeList()
    {
        return new Type[1]
        {
            typeof(bool)
        };
    }

    public override bool isBool { get => true; }
    public override bool toBool()
    {
        bool boolArg = Arguments[0].toBool();
        return !boolArg;
    }
}
