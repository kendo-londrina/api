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
    }
}