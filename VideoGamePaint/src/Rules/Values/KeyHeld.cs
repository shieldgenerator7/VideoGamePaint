using System;
using System.Windows.Forms;

public class KeyHeld:Expression
{
    Keys key;

    public KeyHeld(Keys key)
    {
        this.key = key;
    }

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
        return entity.inputs.Contains(key);
    }
}
