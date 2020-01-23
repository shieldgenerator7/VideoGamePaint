using System;

public class PlayerInputValue : Expression
{
    public override int parameterCount { get => 1; }
    protected override Type[] _getParameterTypeList()
    {
        return new Type[1]
        {
            typeof(Entity)
        };
    }

    public override bool isVector { get => true; }
    public override Vector toVector()
    {
        Entity entity = Arguments[0].toEntity();
        return entity.inputDir;
    }
}
