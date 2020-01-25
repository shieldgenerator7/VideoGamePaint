using System;

public class VariableSetAction:Expression
{
    public override int parameterCount { get => 3; }
    protected override Type[] _getParameterTypeList()
    {
        return new Type[3]
        {
            typeof(Entity),
            typeof(string),
            typeof(object)
        };
    }

    public override bool isFunction { get => true; }
    public override void runFunction()
    {
        Entity entity = arguments[0].toEntity();
        string str = arguments[1].toString();
        object obj = arguments[2].toValue();
        entity.variables[str] = obj;
    }
}
