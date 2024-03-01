using Moq;
using FluentAssertions;
using ken_lo.Domain;
using ken_lo.Application.UseCases.Aluno;
using ken_lo.Domain.Validation;

namespace ken_lo.Application;

[Collection(nameof(AlunoInsertFixture))]
public class AlunoInsertTest
{
    private readonly AlunoInsertFixture _alunoInsertFixture;
    public AlunoInsertTest(AlunoInsertFixture alunoInsertFixture)
    {
        _alunoInsertFixture = alunoInsertFixture;
    }

    [Theory()]
    [MemberData(nameof(GetIvalidInputs))]
    public async Task ErroAoInstanciarAgregado(
        AlunoInsertInput input,
        string exceptionMessage
    )
    {
        var useCase = new AlunoInsert(
            _alunoInsertFixture.getRepositoryMock().Object,
            _alunoInsertFixture.getUnitOfWorkMock().Object
        );

        Func<Task> task = async () =>
            await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMessage);
    }

    public static IEnumerable<object[]> GetIvalidInputs()
    {
        var fixture = new AlunoInsertFixture();
        var invalidInputList = new List<object[]>();
        var inputSemNome = fixture.GetInput();
        inputSemNome.Nome = "";
        invalidInputList.Add(
            new object[] {
                inputSemNome,
                "Nome não pode ser nulo ou vazio"
            }
        );
        return invalidInputList;
    }

    [Fact()]
    public async Task InserirAluno() {
        // Arrange
        var repositoryMock = _alunoInsertFixture.getRepositoryMock();
        var unitOfWorkMock = _alunoInsertFixture.getUnitOfWorkMock();
        var useCase = new AlunoInsert(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );
        var input = _alunoInsertFixture.GetInput();

        // Act
        var output = await useCase.Handle(input, CancellationToken.None);

        // Assertion
        repositoryMock.Verify(
            repo => repo.Insert(
                It.IsAny<Aluno>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );
        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.EscolaId.Should().Be(input.EscolaId);
        output.Nome.Should().Be(input.Nome);
        output.Codigo.Should().Be(input.Codigo);
        output.DataNascimento.Should().Be(input.DataNascimento);
        output.Nacionalidade.Should().Be(input.Nacionalidade);
        output.UfNascimento.Should().Be(input.UfNascimento);
        output.CidadeNascimento.Should().Be(input.CidadeNascimento);
        output.Sexo.Should().Be(input.Sexo);
        output.Rg.Should().Be(input.Rg);
        output.Cpf.Should().Be(input.Cpf);
        output.Email.Should().Be(input.Email);
        output.TelCelular.Should().Be(input.TelCelular);
        output.Religiao.Should().Be(input.Religiao);
    }
}