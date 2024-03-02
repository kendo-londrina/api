using Bogus.Extensions.Brazil;
using ken_lo.Application.UseCases.Aluno;
using ken_lo.Common;
using ken_lo.Domain;
using ken_lo.Domain.Repository;
using Moq;

namespace ken_lo.Application;

[CollectionDefinition(nameof(AlunoUpdateFixture))]
public class AlunoUpdateFixtureCollection : ICollectionFixture<AlunoUpdateFixture>
{ }
public class AlunoUpdateFixture : BaseFixture
{
    public Mock<IAlunoRepository> getRepositoryMock() {
        return new Mock<IAlunoRepository>();
    }
    // a sintaxe abaixo é uma simplificação da acima!!!
    public Mock<IUnitOfWork> getUnitOfWorkMock() => new();
    public Aluno GetExample()
    {
        return new Aluno(
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

    public AlunoUpdateInput GetInput(Guid? id = null)
    {
        return new AlunoUpdateInput(
            id ?? Guid.NewGuid(),
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

    public AlunoUpdateInput GetInputApenasComNome(Guid? id)
    {
        return new AlunoUpdateInput(
            id ?? Guid.NewGuid(),
            Faker.Company.CompanyName()
        );
    }    

}
