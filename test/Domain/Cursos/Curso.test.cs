using Bogus;
using FluentAssertions;

namespace w_escolas.Domain.Cursos;

public class CursoTest
{
    [Fact()]
    public void InstanciarObjeto()
    {
        var faker = new Faker("pt_BR");
        var tipo = new Guid();
        var escolaId = new Guid();
        var codigo = faker.Random.AlphaNumeric(5);
        var nome = faker.Commerce.Department();
        var ordem = faker.Random.Int(1, 100);

        var curso = new Curso(
            tipo, codigo, nome, ordem, escolaId
        );

        curso.Should().NotBeNull();
        curso.EscolaId.Should().Be(escolaId);
        curso.Codigo.Should().Be(codigo);
        curso.Nome.Should().Be(nome);
        curso.Ordem.Should().Be(ordem);
        curso.TipoDeCursoId.Should().Be(tipo);
    }

    [Fact()]
    public void Alterar()
    {
        var faker = new Faker("pt_BR");

        var curso = new Curso(
            new Guid(),
            faker.Random.AlphaNumeric(5),
            faker.Commerce.Department(),
            faker.Random.Int(1, 100),
            new Guid()
        );

        var tipoDeCursoIdAlterado = new Guid();
        var codigoAlterado = faker.Random.AlphaNumeric(5);
        var nomeAlterado = faker.Commerce.Department();
        var ordemAlterado = faker.Random.Int(1, 100);


        curso.Alterar(
            tipoDeCursoIdAlterado,
            codigoAlterado,
            nomeAlterado,
            ordemAlterado
        );

        curso.Should().NotBeNull();
        curso.Codigo.Should().Be(codigoAlterado);
        curso.Nome.Should().Be(nomeAlterado);
        curso.Ordem.Should().Be(ordemAlterado);
        curso.TipoDeCursoId.Should().Be(tipoDeCursoIdAlterado);
    }

}
