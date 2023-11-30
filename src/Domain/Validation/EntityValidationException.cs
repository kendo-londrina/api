namespace ken_lo.Domain.Validation;

public class EntityValidationException: Exception
{
    public EntityValidationException(string? message) : base(message)
    {
        
    }
}