namespace ken_lo.Domain.Validation;
public static class DomainValidation
{
    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (String.IsNullOrWhiteSpace(target)) {
            throw new EntityValidationException(
                $"{fieldName} não pode ser nulo ou vazio"
            );
        }
    }
}
