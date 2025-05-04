using ken_lo.Application.Common;
using ken_lo.Application.UseCases.Aluno;
using ken_lo.Domain.SeedWork.SearchableRepository;

public class AlunoListOutput : PaginatedListOutput<AlunoOutput>
{
    public AlunoListOutput(
        int page, 
        int perPage, 
        int total, 
        IReadOnlyList<AlunoOutput> items
    ) : base(page, perPage, total, items)
    {
    }
}