using Bogus;
using FluentAssertions;
using w_escolas.Domain.Enderecos;

namespace w_escolas.Domain.Alunos;

public class AlunoTest
{
    [Fact()]
    public void InstanciarObjetoSimples()
    {
        var faker = new Faker("pt_BR");

        var escolaId = new Guid();
        var nome = faker.Person.FullName;
        var codigo = faker.Random.AlphaNumeric(10);

        var aluno = new Aluno(
            escolaId, nome, codigo
        );

        aluno.Should().NotBeNull();
        aluno.EscolaId.Should().Be(escolaId);
        aluno.Nome.Should().Be(nome);
        aluno.Codigo.Should().Be(codigo);
    }

    [Fact()]
    public void InstanciarObjetoCompleto()
    {
        var faker = new Faker("pt_BR");

        var escolaId = new Guid();
        var nome = faker.Person.FullName;
        var codigo = faker.Random.AlphaNumeric(10);
        var dataNascimento = faker.Date.Past();
        var nacionalidade = faker.Address.Country();
        var ufNascimento = faker.Address.StateAbbr();
        var cidadeNascimento = faker.Address.City();
        var sexo = faker.Person.Gender.ToString();
        var rg = faker.Random.AlphaNumeric(15);
        var cpf = faker.Random.AlphaNumeric(11);
        var email = faker.Internet.Email();
        var telCelular = faker.Phone.PhoneNumber();
        var religiao = faker.Name.FullName();

        var aluno = new Aluno(
            escolaId, nome, codigo,
            dataNascimento, nacionalidade, ufNascimento, cidadeNascimento,
            sexo, rg, cpf, email, telCelular, religiao
        );

        aluno.Should().NotBeNull();
        aluno.EscolaId.Should().Be(escolaId);
        aluno.Nome.Should().Be(nome);
        aluno.Codigo.Should().Be(codigo);
        aluno.DataNascimento.Should().Be(dataNascimento);
        aluno.Nacionalidade.Should().Be(nacionalidade);
        aluno.UfNascimento.Should().Be(ufNascimento);
        aluno.CidadeNascimento.Should().Be(cidadeNascimento);
        aluno.Sexo.Should().Be(sexo);
        aluno.Rg.Should().Be(rg);
        aluno.Cpf.Should().Be(cpf);
        aluno.Email.Should().Be(email);
        aluno.TelCelular.Should().Be(telCelular);
        aluno.Religiao.Should().Be(religiao);
    }

    [Fact()]
    public void AlterarDados() {
        var faker = new Faker("pt_BR");

        var aluno = new Aluno(
            new Guid(),
            faker.Person.FullName,
            faker.Random.AlphaNumeric(10),
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

        var nomeAlterado = faker.Person.FullName;
        var codigoAlterado = faker.Random.AlphaNumeric(10);
        var dataNascimentoAlterado = faker.Date.Past();
        var nacionalidadeAlterado = faker.Address.Country();
        var ufNascimentoAlterado = faker.Address.StateAbbr();
        var cidadeNascimentoAlterado = faker.Address.City();
        var sexoAlterado = faker.Person.Gender.ToString();
        var rgAlterado = faker.Random.AlphaNumeric(15);
        var cpfAlterado = faker.Random.AlphaNumeric(11);
        var emailAlterado = faker.Internet.Email();
        var telCelularAlterado = faker.Phone.PhoneNumber();
        var religiaoAlterado = faker.Name.FullName();

        aluno.Alterar(
            nomeAlterado, codigoAlterado,
            dataNascimentoAlterado, nacionalidadeAlterado, ufNascimentoAlterado, cidadeNascimentoAlterado,
            sexoAlterado, rgAlterado, cpfAlterado, emailAlterado, telCelularAlterado, religiaoAlterado
        );

        aluno.Should().NotBeNull();
        aluno.Nome.Should().Be(nomeAlterado);
        aluno.Codigo.Should().Be(codigoAlterado);
        aluno.DataNascimento.Should().Be(dataNascimentoAlterado);
        aluno.Nacionalidade.Should().Be(nacionalidadeAlterado);
        aluno.UfNascimento.Should().Be(ufNascimentoAlterado);
        aluno.CidadeNascimento.Should().Be(cidadeNascimentoAlterado);
        aluno.Sexo.Should().Be(sexoAlterado);
        aluno.Rg.Should().Be(rgAlterado);
        aluno.Cpf.Should().Be(cpfAlterado);
        aluno.Email.Should().Be(emailAlterado);
        aluno.TelCelular.Should().Be(telCelularAlterado);
        aluno.Religiao.Should().Be(religiaoAlterado);
    }

    [Fact()]
    public void AlterarEndereco() {
        var faker = new Faker("pt_BR");

        var aluno = new Aluno(
            new Guid(),
            faker.Person.FullName,
            faker.Random.AlphaNumeric(10)
        );

        var endereco = new Endereco(
            faker.Address.ZipCode(),
            faker.Address.StateAbbr(),
            faker.Address.City(),
            faker.Address.County(),
            faker.Address.County(),
            faker.Address.SecondaryAddress(),
            faker.Address.FullAddress(),
            faker.Random.EnumValues<TipoDeEndereco>().ToString()
        );

        aluno.AlterarEndereco(endereco);

        aluno.Should().NotBeNull();
        aluno.Endereco.Should().Be(endereco);
    }

}
