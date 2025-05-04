using Bogus;
using Bogus.Extensions.Brazil;
using ken_lo.Common;

namespace ken_lo.Domain;
public class AlunoFixture : BaseFixture
{
    public Aluno GetValidObject()
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
}

[CollectionDefinition(nameof(AlunoFixture))]
public class AlunoFixtureCollection
    : ICollectionFixture<AlunoFixture>
{ }
