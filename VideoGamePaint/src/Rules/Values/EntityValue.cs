using System;

public class EntityValue:Expression
{
    Entity entity;

    public EntityValue(Entity entity):base()
    {
        this.entity = entity;
    }

    public EntityValue(string entityString) : this((Entity)null)
    {
        this.entity = parseStringAsEntity(entityString);
    }

    public Entity parseStringAsEntity(string entityString)
    {
        switch (entityString.ToLower())
        {
            case "player":
                return Player.instance;
        }
        throw new ArgumentException(
            "EntityValue cannot parse the string as an entity: "+entityString
            );
    }

    public override bool isEntity { get => true; }
    public override Entity toEntity()
    {
        return this.entity;
    }

    public override Expression claimExpressionString(string exprStr)
    {
        base.claimExpressionString(exprStr);
        exprStr = exprStr.ToLower();
        if (exprStr == "player")
        {
            return new EntityValue(exprStr);
        }
        return null;
    }

    public override string[] getConstantNames(Type type)
    {
        base.getConstantNames(type);
        if (type == typeof(Entity))
        {
            return new string[] { "Player" };
        }
        return null;
    }

    public override string TokenName => "Entity";
    public override int ConstructorParameterCount => 1;
    public override bool canAcceptConstructorArgument(string arg)
    {
        return arg.Trim().ToLower() == "player";
    }
    public EntityValue()
    {
        isMeta = true;
    }
}
