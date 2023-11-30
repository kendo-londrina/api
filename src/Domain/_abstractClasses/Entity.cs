namespace ken_lo.Domain._abstractClasses;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public string? CreatedBy { get; protected set; }
    public DateTime? CreatedOn { get; protected set; }
    public string? EditedBy { get; protected set; }
    public DateTime? EditedOn { get; protected set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }
}
