using Bogus;
using FluentAssertions;

namespace w_escolas.Domain.Matriculas;

public class MatriculaTest
{
    private Faker faker = new Faker("pt_BR");

    [Fact()]
    public Matricula InstanciarObjeto()
    {
        var escolaId = new Guid();
        var cursoId = new Guid();
        var alunoId = new Guid();
        var temporadaId = new Guid();
        var dataMatricula = faker.Date.Recent();

        var matricula = new Matricula(
            escolaId, cursoId, alunoId, temporadaId, dataMatricula
        );

        matricula.Should().NotBeNull();
        matricula.EscolaId.Should().Be(escolaId);
        matricula.CursoId.Should().Be(cursoId);
        matricula.AlunoId.Should().Be(alunoId);
        matricula.TemporadaId.Should().Be(temporadaId);
        matricula.DataMatricula.Should().Be(dataMatricula);

        return matricula;
    }

    [Fact]
    public void AlterarDataDaMatricula()
    {
        // Given
        var matricula = InstanciarObjeto();
    
        // When
        var dataAlterada = faker.Date.Recent();
        matricula.AlterarDataMatricula(dataAlterada);

        // Then
        matricula.Should().NotBeNull();
        matricula.DataMatricula.Should().Be(dataAlterada);
    }

    [Fact]
    public void CancelarMatricula()
    {
        // Given
        var matricula = InstanciarObjeto();
        matricula.Cancelada.Should().BeFalse();
    
        // When
        matricula.Cancelar();
    
        // Then
        matricula.Cancelada.Should().BeTrue();
    }
}