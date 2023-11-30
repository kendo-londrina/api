using Bogus;
using FluentAssertions;

namespace ken_lo.Domain.Validation;
public class DomainValidationTest
{
    public Faker Faker { get; set; } = new Faker("pt_BR");

    [Theory()]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void NotNullOrEmptyThrow(string? target)
    {
        Action action = () =>
            DomainValidation.NotNullOrEmpty(target, "fieldName");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("fieldName não pode ser nulo ou vazio");
    }

    [Fact()]
    public void NotNullOrEmptyOk()
    {
        var value = Faker.Commerce.ProductName();

        Action action = () =>
            DomainValidation.NotNullOrEmpty(value, "fieldName");

        action.Should().NotThrow();
   }

    
}