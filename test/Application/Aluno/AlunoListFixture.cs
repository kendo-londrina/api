using Bogus.Extensions.Brazil;
using ken_lo.Common;
using domain = ken_lo.Domain;
using ken_lo.Domain.Repository;
using Moq;

namespace ken_lo.Application.Aluno;

[CollectionDefinition(nameof(AlunoListFixture))]
public class AlunoListFixtureCollection : ICollectionFixture<AlunoListFixture>
{ }
public class AlunoListFixture : BaseFixture
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

    public List<domain.Aluno> GetList(int count = 30) {
        var listaAlunos = new List<domain.Aluno>();
        for (int i = 0; i < count; i++)
        {
            listaAlunos.Add(GetValid());
        }
        return listaAlunos;
    }
}