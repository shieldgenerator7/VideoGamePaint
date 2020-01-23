using System;

public class MoveAction : Expression
{
    public override int parameterCount { get => 2; }
    protected override Type[] _getParameterTypeList()
    {
        return new Type[2]
        {
            typeof(Entity),
            typeof(Vector)
        };
    }

    public override bool isFunction { get => true; }
    public override void runFunction(Expression[] parameters)
    {
        Entity entity = parameters[0].toEntity();
        Vector vector = parameters[1].toVector(new Expression[] { parameters[0] });
        entity.move(vector);
    }
}
