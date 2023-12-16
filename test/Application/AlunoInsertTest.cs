using Moq;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using ken_lo.Domain;
using ken_lo.Domain.Repository;
using ken_lo.Application.UseCases;

namespace ken_lo.Application;
public class AlunoInsertTest
{
    [Fact()]
    public async Task InserirAluno() {
        var Faker = new Faker("pt_BR");

        // Arrange
        var repositoryMock = new Mock<IAlunoRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var useCase = new AlunoInsert(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );
        var input = new AlunoInsertInput(
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

        // Act
        var output = await useCase.Handle(input, CancellationToken.None);

        // Assertion
        repositoryMock.Verify(
            repo => repo.Inserir(
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
        output.Id.Should().NotBeEmpty();
        output.EscolaId.Should().Be(input.EscolaId);
        output.Nome.Should().Be(input.Nome);
        output.Codigo.Should().Be(input.Codigo);
        output.DataNascimento.Should().Be(input.DataNascimento);
        output.Nacionalidade.Should().Be(input.Nacionalidade);
        output.UfNascimento.Should().Be(input.UfNascimento);
        output.CidadeNascimento.Should().Be(input.CidadeNascimento);
        output.Sexo.Should().Be(input.Sexo);
        output.Rg.Should().Be(input.Rg);
        output.Cpf.Should().Be(input.Cpf);
        output.Email.Should().Be(input.Email);
        output.TelCelular.Should().Be(input.TelCelular);
        output.Religiao.Should().Be(input.Religiao);
    }
}