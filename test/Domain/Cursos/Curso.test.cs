using Bogus;
using FluentAssertions;
using w_escolas.Domain.Cursos;

namespace kendo_londrina_test.Domain.Cursos;

public class CursoTest
{
    [Fact(DisplayName = nameof(InstanciarObjeto))]
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
}
