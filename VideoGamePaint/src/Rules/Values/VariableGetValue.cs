using System;

public class VariableGetValue : AnyTypeValue
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

    protected override object value
    {
        get
        {
            Entity entity = arguments[0].toEntity();
            string str = arguments[1].toString();
            return entity.variables[str];
        }
        set { }
    }

    //public override Expression claimExpressionString(string exprStr)
    //{
    //    if (Player.instance.variables.ContainsKey(exprStr))
    //    {
    //        return Player.instance.variables[exprStr];
    //    }
    //    return null;
    //}

    public override string[] getConstantNames(Type type)
    {
        base.getConstantNames(type);
        if (type == typeof(string))
        {
            string[] varNames = new string[Player.instance.variables.Keys.Count];
            Player.instance.variables.Keys.CopyTo(varNames, 0);
            return varNames;
        }
        return null;
    }

    public override string TokenName => "Get";
}
