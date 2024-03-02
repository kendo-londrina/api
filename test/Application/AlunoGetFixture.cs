using Bogus.Extensions.Brazil;
using ken_lo.Common;
using domain = ken_lo.Domain;
using ken_lo.Domain.Repository;
using Moq;

namespace ken_lo.Application;

[CollectionDefinition(nameof(AlunoGetFixture))]
public class AlunoGetFixtureCollection : ICollectionFixture<AlunoGetFixture>
{ }
public class AlunoGetFixture : BaseFixture
{
    public Mock<IAlunoRepository> getRepositoryMock() {
        return new Mock<IAlunoRepository>();
    }
    public domain.Aluno GetValid()
    {
        return new domain.Aluno(
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