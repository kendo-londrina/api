using FluentValidation;

namespace ken_lo.Application.UseCases;
public class AlunoGetInputValidator : AbstractValidator<AlunoGetInput>
{
    public AlunoGetInputValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
    
}