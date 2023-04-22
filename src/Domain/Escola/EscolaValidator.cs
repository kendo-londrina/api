using FluentValidation;

namespace w_escolas.Domain.Escola;

public class EscolaValidator : AbstractValidator<Escola>
{
    public EscolaValidator()
    {
        RuleFor(t => t.NomeFantasia)
            .NotEmpty()
            .MinimumLength(3);
        RuleFor(t => t.Uf).NotEmpty();
        RuleFor(t => t.Cidade).NotEmpty();

        RuleFor(t => t.Email).EmailAddress();
    }
}
