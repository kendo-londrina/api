namespace ken_lo.Domain.SeedWork.SearchableRepository;

public class SearchOutput<T>
    where T: class
{
    public int CurretPage { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<T> Items { get; set; }
    
    public SearchOutput(
        int currentPage,
        int perPage,
        int total,
        IReadOnlyList<T> items
    )
    {
        CurretPage = currentPage;
        PerPage = perPage;
        Total = total;
        Items = items;
    }
}