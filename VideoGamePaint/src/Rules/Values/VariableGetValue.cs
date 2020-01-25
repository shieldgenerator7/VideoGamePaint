using System;

public class VariableGetValue:AnyTypeValue
{
    public override int parameterCount { get => 2; }
    protected override Type[] _getParameterTypeList()
    {
        return new Type[2]
        {
            typeof(Entity),
            typeof(string)
        };
    }

    protected override object value {
        get
        {
            Entity entity = arguments[0].toEntity();
            string str = arguments[1].toString();
            return entity.variables[str];
        }
        set { }
    }
}
