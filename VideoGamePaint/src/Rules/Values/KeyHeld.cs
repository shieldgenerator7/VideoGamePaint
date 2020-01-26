using System;
using System.Windows.Forms;

public class KeyHeld:Expression
{
    Keys key;

    public KeyHeld(Keys key):base()
    {
        this.key = key;
    }

    public KeyHeld(string keyString)
        :this((Keys)Enum.Parse(typeof(Keys), keyString))
    {
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

    public override string TokenName => "Key";
    public override int ConstructorParameterCount => 1;
    public KeyHeld()
    {
        isMeta = true;
    }
}
