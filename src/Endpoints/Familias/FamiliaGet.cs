using ken_lo.Domain.Familias;
using ken_lo.Endpoints.Familias.dtos;
using Microsoft.EntityFrameworkCore;
using w_escolas.Shared;

namespace ken_lo.Endpoints.Familias;

public class FamiliaGet
{
    public static string Template => "/familias";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        FamiliaFilter? filter,
        ApplicationDbContext context,
        UserInfo userInfo,
        int page = 1, int rowsPerPage = 10, string orderBy = "Nome", string sortOrder = "asc")
    {
        if (context.Familias == null)
            return Results.UnprocessableEntity();

        if (rowsPerPage > 100)
            return Results.Problem(title: "Max rowsPerPage value must be 100", statusCode: 400);

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var queryBase = context.Familias.AsNoTracking()
            .Where(t => t.EscolaId == escolaIdDoUsuarioCorrente);
        IQueryable<Familia> queryOrder = orderBy.ToLower() switch
        {
            "id" => (sortOrder == "asc")
                ? queryBase.OrderBy(t => t.Id)
                : queryBase.OrderByDescending(t => t.Id),
            _ => (sortOrder == "asc")
                ? queryBase.OrderBy(t => t.Nome)
                : queryBase.OrderByDescending(t => t.Nome),
        };

        var queryFiltered = ApplyFilter(queryOrder, filter);

        var queryPaginated = queryFiltered.Skip((page - 1) * rowsPerPage).Take(rowsPerPage);

        var familias = queryPaginated.ToList();

        var response = familias.Select(
            t => new FamiliaResponse
            {
                Id = t.Id,
                Nome = t.Nome
            }
        );

        if (filter != null && filter.Id != null && filter.Id != "")
            return Results.Ok(response);

        var pageDto = new PageDto<Familia> { Count = queryFiltered.ToList().Count, Data = familias };
        return Results.Ok(pageDto);        
    }

    private static IQueryable<Familia> ApplyFilter(IQueryable<Familia> inputQuery, FamiliaFilter? filter)
    {
        if (filter == null)
            return inputQuery;
        if (filter.Id != null && filter.Id != "")
            return inputQuery.Where(t => t.Id.ToString() == filter.Id);
        if (filter.Nome != null && filter.Nome != "")
            return inputQuery.Where(t => 
                t.Nome.Contains(filter.Nome));
        return inputQuery;
    }
}
