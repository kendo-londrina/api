using Moq;
using FluentAssertions;
using ken_lo.Application.UseCases.Aluno;
using ken_lo.Application.Exceptions;

namespace ken_lo.Application;

[Collection(nameof(AlunoGetFixture))]
public class AlunoGetTest
{
    private readonly AlunoGetFixture _alunoGetFixture;
    public AlunoGetTest(AlunoGetFixture alunoGetFixture)
    {
        _alunoGetFixture = alunoGetFixture;
    }

    [Fact()]
    public async Task ObterAluno() {
        // Arrange
        var repositoryMock = _alunoGetFixture.getRepositoryMock();
        var exampleAluno = _alunoGetFixture.GetValid();
        repositoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(exampleAluno);
        var input = new AlunoGetInput(exampleAluno.Id);
        var useCase = new AlunoGet(repositoryMock.Object);

        // Act
        var output = await useCase.Handle(input, CancellationToken.None);

        // Assertion
        repositoryMock.Verify(
            repo => repo.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
        output.Should().NotBeNull();
        output.EscolaId.Should().Be(exampleAluno.EscolaId);
        output.Nome.Should().Be(exampleAluno.Nome);
        output.Codigo.Should().Be(exampleAluno.Codigo);
        output.DataNascimento.Should().Be(exampleAluno.DataNascimento);
        output.Nacionalidade.Should().Be(exampleAluno.Nacionalidade);
        output.UfNascimento.Should().Be(exampleAluno.UfNascimento);
        output.CidadeNascimento.Should().Be(exampleAluno.CidadeNascimento);
        output.Sexo.Should().Be(exampleAluno.Sexo);
        output.Rg.Should().Be(exampleAluno.Rg);
        output.Cpf.Should().Be(exampleAluno.Cpf);
        output.Email.Should().Be(exampleAluno.Email);
        output.TelCelular.Should().Be(exampleAluno.TelCelular);
        output.Religiao.Should().Be(exampleAluno.Religiao);
    }

    [Fact()]
    public async Task NotFoundExceptionQuandoAlunoNaoExiste() {
        // Arrange
        var repositoryMock = _alunoGetFixture.getRepositoryMock();
        var guidAleatorio = Guid.NewGuid();
        repositoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )).ThrowsAsync(
            new NotFoundException($"Aluno '{guidAleatorio}' not found")
        );
        var input = new AlunoGetInput(guidAleatorio);
        var useCase = new AlunoGet(repositoryMock.Object);

        // Act
        var task = async () => await useCase.Handle(input, CancellationToken.None);

        // Assertion
        await task.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(
            repo => repo.Get(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
    }
}
