namespace ken_lo.Domain.SeedWork.SearchableRepository;

public interface ISearchableRepository<T> where T: class
{
    Task<SearchOutput<T>> Search(
        SearchInput input,
        CancellationToken cancellationToken
    );
}