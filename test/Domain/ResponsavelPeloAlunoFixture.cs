using Bogus;
using ken_lo.Common;

namespace ken_lo.Domain;
public class ResponsavelPeloAlunoFixture : BaseFixture
{
    public ResponsavelPeloAluno GetValidObject()
    {
        return new ResponsavelPeloAluno(
            Guid.NewGuid(),
            Faker.Person.FullName,
            Faker.Date.Past(),
            Faker.Address.Country(),
            Faker.Address.StateAbbr(),
            Faker.Address.City(),
            Faker.Person.Gender.ToString(),
            Faker.Random.AlphaNumeric(15),
            Faker.Random.AlphaNumeric(11),
            Faker.Internet.Email(),
            Faker.Phone.PhoneNumber(),
            Faker.Name.FullName()
        );
    }
}

[CollectionDefinition(nameof(ResponsavelPeloAlunoFixture))]
public class ResponsavelPeloAlunoFixtureCollection
    : ICollectionFixture<ResponsavelPeloAlunoFixture>
{ }
