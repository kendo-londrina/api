using Moq;
using ken_lo.Domain;
using ken_lo.Application.UseCases;
using FluentAssertions;

namespace ken_lo.Application;

[Collection(nameof(AlunoUpdateFixture))]
public class AlunoUpdateTest
{
    private readonly AlunoUpdateFixture _alunoUpdateFixture;
    public AlunoUpdateTest(AlunoUpdateFixture alunoUpdateFixture)
    {
        _alunoUpdateFixture = alunoUpdateFixture;
    }

    [Theory()]
    [MemberData(
        nameof(AlunoUpdateTestGenerator.GetAlunosToUpdate),
        parameters: 10,
        MemberType = typeof(AlunoUpdateTestGenerator)
    )]
    public async Task AlterarAluno(Aluno example, AlunoUpdateInput input) {
        // Arrange
        var repositoryMock = _alunoUpdateFixture.getRepositoryMock();
        var unitOfWorkMock = _alunoUpdateFixture.getUnitOfWorkMock();
        repositoryMock.Setup(x => x.Get(
            example.Id,
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(example);

        var useCase = new AlunoUpdate(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        // Act
        var output = await useCase.Handle(input, CancellationToken.None);

        // Assertion
        repositoryMock.Verify(
            repo => repo.Update(
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
        output.EscolaId.Should().Be(example.EscolaId);
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

    [Fact()]
    public async Task AlterarApenasNome() {
        // Arrange
        var repositoryMock = _alunoUpdateFixture.getRepositoryMock();
        var unitOfWorkMock = _alunoUpdateFixture.getUnitOfWorkMock();
        var fixture = new AlunoUpdateFixture();
        var example = fixture.GetExample();
        var input = fixture.GetInputApenasComNome(example.Id);
        repositoryMock.Setup(x => x.Get(
            example.Id,
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(example);
        var useCase = new AlunoUpdate(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        // Act
        var output = await useCase.Handle(input, CancellationToken.None);

        // Assertion
        repositoryMock.Verify(
            repo => repo.Update(
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
        output.EscolaId.Should().Be(example.EscolaId);
        output.Nome.Should().Be(input.Nome);
        output.Codigo.Should().Be(example.Codigo);
        output.DataNascimento.Should().Be(example.DataNascimento);
        output.Nacionalidade.Should().Be(example.Nacionalidade);
        output.UfNascimento.Should().Be(example.UfNascimento);
        output.CidadeNascimento.Should().Be(example.CidadeNascimento);
        output.Sexo.Should().Be(example.Sexo);
        output.Rg.Should().Be(example.Rg);
        output.Cpf.Should().Be(example.Cpf);
        output.Email.Should().Be(example.Email);
        output.TelCelular.Should().Be(example.TelCelular);
        output.Religiao.Should().Be(example.Religiao);

    }
}