using ken_lo.Application.Common;
using ken_lo.Domain.SeedWork.SearchableRepository;

namespace ken_lo.Application.UseCases.Aluno;

public class AlunoListInput : PaginatedListInput
{
    public AlunoListInput(
        int page,
        int perPage,
        string search,
        string sort,
        SearchOrder direction
    ) : base(page, perPage, search, sort, direction)
    {
    }
}