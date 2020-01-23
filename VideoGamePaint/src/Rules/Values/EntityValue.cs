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
}
