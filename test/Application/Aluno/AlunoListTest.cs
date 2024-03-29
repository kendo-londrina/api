using Moq;
using FluentAssertions;
using ken_lo.Application.UseCases.Aluno;
using ken_lo.Application.Exceptions;
using domain = ken_lo.Domain;
using ken_lo.Domain.SeedWork.SearchableRepository;

namespace ken_lo.Application.Aluno;

[Collection(nameof(AlunoListFixture))]
public class AlunoListTest
{
    private readonly AlunoListFixture _alunoListFixture;
    public AlunoListTest(AlunoListFixture alunoListFixture)
    {
        _alunoListFixture = alunoListFixture;
    }

    [Fact()]
    public async Task ListarAluno() {
        // Arrange
        var domainAlunoList = _alunoListFixture.GetList();
        var repositoryMock = _alunoListFixture.getRepositoryMock();
        var input = new AlunoListInput(
            page: 2,
            perPage: 15,
            search: "termo para filtro",
            sort: "nome do campo para ordenacao",
            direction: SearchOrder.Asc
        );
        var repositorySearchOutput = new SearchOutput<domain.Aluno>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: (IReadOnlyList<domain.Aluno>) domainAlunoList,
            total: 70
        );
        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(searchInput =>
                searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Direction
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(repositorySearchOutput);
        var useCase = new AlunoList(repositoryMock.Object);

        // Act
        var output = await useCase.Handle(input, CancellationToken.None);

        // Assertion
        output.Should().NotBeNull();
        output.Page.Should().Be(repositorySearchOutput.CurretPage);
        output.PerPage.Should().Be(repositorySearchOutput.PerPage);
        output.Total.Should().Be(repositorySearchOutput.Total);
        output.Items.Should().HaveCount(repositorySearchOutput.Items.Count);
        ((List<AlunoOutput>)output.Items).ForEach(outputItem =>
        {
            var alunoRepository = repositorySearchOutput.Items
                .FirstOrDefault(x => x.Id == outputItem.Id);
            outputItem.EscolaId.Should().Be(alunoRepository!.EscolaId);
            outputItem.Nome.Should().Be(alunoRepository.Nome);
            outputItem.Codigo.Should().Be(alunoRepository.Codigo);
            outputItem.DataNascimento.Should().Be(alunoRepository.DataNascimento);
            outputItem.Nacionalidade.Should().Be(alunoRepository.Nacionalidade);
            outputItem.UfNascimento.Should().Be(alunoRepository.UfNascimento);
            outputItem.CidadeNascimento.Should().Be(alunoRepository.CidadeNascimento);
            outputItem.Sexo.Should().Be(alunoRepository.Sexo);
            outputItem.Rg.Should().Be(alunoRepository.Rg);
            outputItem.Cpf.Should().Be(alunoRepository.Cpf);
            outputItem.Email.Should().Be(alunoRepository.Email);
            outputItem.TelCelular.Should().Be(alunoRepository.TelCelular);
            outputItem.Religiao.Should().Be(alunoRepository.Religiao);
        });

        repositoryMock.Verify(
            repo => repo.Search(
                It.Is<SearchInput>(searchInput =>
                    searchInput.Page == input.Page
                    && searchInput.PerPage == input.PerPage
                    && searchInput.Search == input.Search
                    && searchInput.OrderBy == input.Sort
                    && searchInput.Order == input.Direction
                ),
                It.IsAny<CancellationToken>()
            ), Times.Once
        );
    }
}
