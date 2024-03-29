using ken_lo.Domain.Repository;

namespace ken_lo.Application.UseCases.Aluno;
public class AlunoList
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoList(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }
    public async Task<AlunoListOutput> Handle(
        AlunoListInput input,
        CancellationToken cancellationToken)
    {
        var searchOutput = await _alunoRepository.Search(
            new(
                input.Page,
                input.PerPage,
                input.Search,
                input.Sort,
                input.Direction
            ),
            cancellationToken
        );

        var alunoOutputList = new List<AlunoOutput>();
        foreach (var item in searchOutput.Items)
        {
            alunoOutputList.Add(AlunoOutput.FromAluno(item));
        }

        return new AlunoListOutput(
            searchOutput.CurretPage,
            searchOutput.PerPage,
            searchOutput.Total,
            alunoOutputList.ToList()
        );

            // (IReadOnlyList<AlunoOutput>)searchOutput. .Items. . .ToList()

    }
}
