using Moq;
using domain = ken_lo.Domain;
using ken_lo.Application.UseCases.Aluno;

namespace ken_lo.Application.Aluno;

[Collection(nameof(AlunoDeleteFixture))]
public class AlunoDeleteTest
{
    private readonly AlunoDeleteFixture _alunoDeleteFixture;
    public AlunoDeleteTest(AlunoDeleteFixture alunoInsertFixture)
    {
        _alunoDeleteFixture = alunoInsertFixture;
    }

    [Fact()]
    public async Task ExcluirAluno() {
        // Arrange
        var repositoryMock = _alunoDeleteFixture.getRepositoryMock();
        var unitOfWorkMock = _alunoDeleteFixture.getUnitOfWorkMock();
        var useCase = new AlunoDelete(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );
        var input = _alunoDeleteFixture.GetInput();

        // Act
        await useCase.Handle(input, CancellationToken.None);

        // Assertion
        repositoryMock.Verify(
            repo => repo.Delete(
                It.IsAny<domain.Aluno>(),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );
        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}