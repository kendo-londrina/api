using ken_lo.Application.UseCases.Aluno;
using ken_lo.Common;
using ken_lo.Domain.Repository;
using Moq;

namespace ken_lo.Application.Aluno;

[CollectionDefinition(nameof(AlunoDeleteFixture))]
public class AlunoDeleteFixtureCollection : ICollectionFixture<AlunoDeleteFixture>
{ }
public class AlunoDeleteFixture : BaseFixture
{
    public Mock<IAlunoRepository> getRepositoryMock() {
        return new Mock<IAlunoRepository>();
    }
    // a sintaxe abaixo é uma simplificação da acima!!!
    public Mock<IUnitOfWork> getUnitOfWorkMock() => new();
    public AlunoDeleteInput GetInput()
    {
        return new AlunoDeleteInput(
            Guid.NewGuid()
        );
    }
}
