using Bogus.Extensions.Brazil;
using ken_lo.Application.UseCases.Aluno;
using ken_lo.Common;
using ken_lo.Domain.Repository;
using Moq;

namespace ken_lo.Application.Aluno;

[CollectionDefinition(nameof(AlunoInsertFixture))]
public class AlunoInsertFixtureCollection : ICollectionFixture<AlunoInsertFixture>
{ }
public class AlunoInsertFixture : BaseFixture
{
    public Mock<IAlunoRepository> getRepositoryMock() {
        return new Mock<IAlunoRepository>();
    }
    // a sintaxe abaixo é uma simplificação da acima!!!
    public Mock<IUnitOfWork> getUnitOfWorkMock() => new();    
    public AlunoInsertInput GetInput()
    {
        return new AlunoInsertInput(
            Guid.NewGuid(),
            Faker.Person.FullName,
            Faker.Random.AlphaNumeric(5),
            Faker.Date.Past(),
            Faker.Address.Country(),
            Faker.Address.StateAbbr(),
            Faker.Address.City(),
            Faker.Person.Gender.ToString(),
            Faker.Random.AlphaNumeric(15),
            Faker.Person.Cpf(),
            Faker.Internet.Email(),
            Faker.Phone.PhoneNumber(),
            Faker.Random.Words(2)
        );
    }

}