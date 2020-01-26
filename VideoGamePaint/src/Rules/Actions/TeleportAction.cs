using System;

public class TeleportAction:Expression
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
    public override void runFunction()
    {
        Entity entity = Arguments[0].toEntity();
        Vector vector = Arguments[1].toVector();
        entity.pos = vector;
    }

    public override string TokenName => "Teleport";
}
