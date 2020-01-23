using System;

public class EntityValue:Expression
{
    Entity entity;

    public EntityValue(Entity entity):base()
    {
        this.entity = entity;
    }

    public override bool isEntity { get => true; }
    public override Entity toEntity()
    {
        return this.entity;
    }
}
