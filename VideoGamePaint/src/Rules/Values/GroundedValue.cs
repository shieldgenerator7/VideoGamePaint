using System;

public class GroundedValue:Expression
{
    public override int parameterCount { get => 1; }
    protected override Type[] _getParameterTypeList()
    {
        return new Type[1]
        {
            typeof(Entity)
        };
    }

    public override bool isBool { get => true; }
    public override bool toBool()
    {
        Entity entity = Arguments[0].toEntity();
        return !entity.canMove(Vector.down);
    }

    public override string TokenName => "Grounded";
}
