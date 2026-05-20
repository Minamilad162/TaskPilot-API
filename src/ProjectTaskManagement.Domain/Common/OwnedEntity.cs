namespace ProjectTaskManagement.Domain.Common;

public abstract class OwnedEntity : Entity
{
    protected OwnedEntity()
    {
    }

    protected OwnedEntity(Guid ownerId)
    {
        if (ownerId == Guid.Empty)
        {
            throw new ArgumentException("Owner id is required.", nameof(ownerId));
        }

        OwnerId = ownerId;
    }

    public Guid OwnerId { get; protected set; }
}
