using ken_lo.Domain.SeedWork.SearchableRepository;

namespace ken_lo.Application.Common;

public abstract class PaginatedListInput
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public string Search { get; set; }
    public string Sort { get; set; }
    public SearchOrder Direction { get; set; }

    public PaginatedListInput(
        int page,
        int perPage,
        string search,
        string sort,
        SearchOrder direction
    )
    {
        Page = page;
        PerPage = perPage;
        Search = search;
        Sort = sort;
        Direction = direction;
    }
}