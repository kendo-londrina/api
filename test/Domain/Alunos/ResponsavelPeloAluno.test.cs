using Bogus;
using FluentAssertions;

namespace w_escolas.Domain.Alunos;

public class ResponsavelPeloAlunoTest
{
    [Fact()]
    public void InstanciarObjetoSimples()
    {
        var faker = new Faker("pt_BR");

        var escolaId = new Guid();
        var alunoId = new Guid();
        var nome = faker.Person.FullName;

        var aluno = new ResponsavelPeloAluno(
            escolaId, alunoId, nome
        );

        aluno.Should().NotBeNull();
        aluno.EscolaId.Should().Be(escolaId);
        aluno.AlunoId.Should().Be(alunoId);
        aluno.Nome.Should().Be(nome);
    }

    [Fact()]
    public void InstanciarObjetoCompleto()
    {
        var faker = new Faker("pt_BR");

        var escolaId = new Guid();
        var alunoId = new Guid();
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

        var responsavelPeloAluno = new ResponsavelPeloAluno(
            escolaId, alunoId, nome,
            dataNascimento, nacionalidade, ufNascimento, cidadeNascimento,
            sexo, rg, cpf, email, telCelular, religiao
        );

        responsavelPeloAluno.Should().NotBeNull();
        responsavelPeloAluno.EscolaId.Should().Be(escolaId);
        responsavelPeloAluno.Nome.Should().Be(nome);
        responsavelPeloAluno.DataNascimento.Should().Be(dataNascimento);
        responsavelPeloAluno.Nacionalidade.Should().Be(nacionalidade);
        responsavelPeloAluno.UfNascimento.Should().Be(ufNascimento);
        responsavelPeloAluno.CidadeNascimento.Should().Be(cidadeNascimento);
        responsavelPeloAluno.Sexo.Should().Be(sexo);
        responsavelPeloAluno.Rg.Should().Be(rg);
        responsavelPeloAluno.Cpf.Should().Be(cpf);
        responsavelPeloAluno.Email.Should().Be(email);
        responsavelPeloAluno.TelCelular.Should().Be(telCelular);
        responsavelPeloAluno.Religiao.Should().Be(religiao);
    }
}
