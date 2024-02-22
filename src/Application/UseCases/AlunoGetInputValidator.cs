using FluentValidation;

namespace ken_lo.Application.UseCases;
public class AlunoGetInputValidator : AbstractValidator<AlunoGetInput>
{
    public AlunoGetInputValidator()
    {
        // Completely disable FluentValidation’s support for localization,
        // which will force the default English messages to be used.
        // ValidatorOptions.Global.LanguageManager.Enabled = false;
        // Expected validationResult.Errors[0].ErrorMessage to be "'Id' must not be empty."
        // with a length of 23, but "'Id' deve ser informado." has a length of 24
        // erro ao rodar dotnet test no console
        RuleFor(x => x.Id).NotEmpty();
    }
    
}