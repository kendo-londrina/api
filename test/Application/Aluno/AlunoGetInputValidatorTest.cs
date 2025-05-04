using FluentAssertions;
using ken_lo.Application.UseCases.Aluno;

namespace ken_lo.Application.Aluno;

[Collection(nameof(AlunoGetFixture))]
public class AlunoGetInputValidatorTest
{
    private readonly AlunoGetFixture _fixture;
    public AlunoGetInputValidatorTest(AlunoGetFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void ValidationOk()
    {
        // // Given
        var validInput = new AlunoGetInput(Guid.NewGuid());
        var validator = new AlunoGetInputValidator();
    
        // // When
        var validationResult = validator.Validate(validInput);
    
        // // Then
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void InvalidWhenEmptyId()
    {
        // // Given
        var validInput = new AlunoGetInput(Guid.Empty);
        var validator = new AlunoGetInputValidator();
    
        // // When
        var validationResult = validator.Validate(validInput);
    
        // // Then
        validationResult.Should().NotBeNull();
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors[0].ErrorMessage.Should()
            .Be("'Id' must not be empty.");
    }
   
}