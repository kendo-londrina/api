using Bogus;

namespace ken_lo.Domain;
public class ResponsavelPeloAlunoFixture
{
    public ResponsavelPeloAluno GetValidObject()
    {
        var faker = new Faker("pt_BR");
        return new ResponsavelPeloAluno(
            Guid.NewGuid(),
            faker.Person.FullName,
            faker.Date.Past(),
            faker.Address.Country(),
            faker.Address.StateAbbr(),
            faker.Address.City(),
            faker.Person.Gender.ToString(),
            faker.Random.AlphaNumeric(15),
            faker.Random.AlphaNumeric(11),
            faker.Internet.Email(),
            faker.Phone.PhoneNumber(),
            faker.Name.FullName()
        );
    }
}

[CollectionDefinition(nameof(ResponsavelPeloAlunoFixture))]
public class ResponsavelPeloAlunoFixtureCollection
    : ICollectionFixture<ResponsavelPeloAlunoFixture>
{ }
