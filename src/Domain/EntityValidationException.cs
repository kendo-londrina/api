namespace ken_lo.Domain;

public class EntityValidationException: Exception
{
    public EntityValidationException(string? message) : base(message)
    {
        
    }
}