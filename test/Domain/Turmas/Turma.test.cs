using Bogus;
using FluentAssertions;

namespace w_escolas.Domain.Turmas;

public class TurmaTest
{
    [Fact()]
    public void InstanciarObjeto()
    {
        var faker = new Faker("pt_BR");
        var escolaId = new Guid();
        var cursoId = new Guid();
        var codigo = faker.Random.AlphaNumeric(5);
        var nome = faker.Commerce.Department();
        var ordem = faker.Random.Int(1, 100);
        var maxAlunos = faker.Random.Int(min: 0, max: 100);
        var dataInicial = faker.Date.Future();
        var dataFinal = dataInicial.AddMonths(1);

        var turma = new Turma(
            escolaId, cursoId, codigo, nome, ordem, maxAlunos, dataInicial, dataFinal
        );

        turma.Should().NotBeNull();
        turma.EscolaId.Should().Be(escolaId);
        turma.CursoId.Should().Be(cursoId);
        turma.Codigo.Should().Be(codigo);
        turma.Nome.Should().Be(nome);
        turma.Ordem.Should().Be(ordem);
        turma.MaxAlunos.Should().Be(maxAlunos);
        turma.DataInicial.Should().Be(dataInicial);
        turma.DataFinal.Should().Be(dataFinal);
    }

    [Fact()]
    public void Alterar()
    {
        var faker = new Faker("pt_BR");

        var turma = new Turma(
            new Guid(),
            new Guid(),
            faker.Random.AlphaNumeric(5),
            faker.Commerce.Department(),
            faker.Random.Int(1, 100)
        );
        turma.MaxAlunos.Should().BeNull();
        turma.DataInicial.Should().BeNull();
        turma.DataFinal.Should().BeNull();

        var cursoIdAlterado = new Guid();
        var codigoAlterado = faker.Random.AlphaNumeric(5);
        var nomeAlterado = faker.Commerce.Department();
        var ordemAlterado = faker.Random.Int(1, 100);
        var maxAlunosAlterado = faker.Random.Int(min: 0, max: 100);
        var dataInicialAlterado = faker.Date.Future();
        var dataFinalAlterado = dataInicialAlterado.AddMonths(1);

        turma.Alterar(
            cursoIdAlterado,
            codigoAlterado,
            nomeAlterado,
            ordemAlterado,
            maxAlunosAlterado,
            dataInicialAlterado,
            dataFinalAlterado
        );

        turma.Should().NotBeNull();
        turma.CursoId.Should().Be(cursoIdAlterado);
        turma.Codigo.Should().Be(codigoAlterado);
        turma.Nome.Should().Be(nomeAlterado);
        turma.Ordem.Should().Be(ordemAlterado);
        turma.MaxAlunos.Should().Be(maxAlunosAlterado);
        turma.DataInicial.Should().Be(dataInicialAlterado);
        turma.DataFinal.Should().Be(dataFinalAlterado);
    }

}