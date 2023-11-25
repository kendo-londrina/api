using FluentValidation;

namespace ken_lo.Domain.Familias;

public class FamiliaValidator : AbstractValidator<Familia>
{
    public FamiliaValidator()
    {
        RuleFor(t => t.Nome)
            .NotEmpty()
            .MinimumLength(3);
    }
}
