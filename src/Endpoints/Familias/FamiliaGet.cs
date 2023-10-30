using ken_lo.Domain.Familias;
using ken_lo.Endpoints.Familias.dtos;
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
        UserInfo userInfo)
    {
        if (context.Familias == null)
            return Results.UnprocessableEntity();

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var familias = Get(context, filter!, escolaIdDoUsuarioCorrente);

        var response = familias.Select(
            t => new FamiliaResponse
            {
                Id = t.Id,
                Nome = t.Nome
            }
        );

        return Results.Ok(response);
    }

    private static List<Familia> Get(
        ApplicationDbContext context, FamiliaFilter filter, Guid escolaId)
    {
        if (filter == null)
            return ObterTodos(context, escolaId);

        if (filter.Id != null && filter.Id != "")
            return ObterPorId(context, filter.Id);

        if (filter.Nome != null && filter.Nome != "")
            return ObterPorNome(context, filter.Nome, escolaId);

        return ObterTodos(context, escolaId);
    }

    private static List<Familia> ObterTodos(ApplicationDbContext context, Guid escolaId)
    {
        return context.Familias!
            .Where(t => t.EscolaId == escolaId)
            .OrderBy(t => t.Nome).ToList();
    }
    private static List<Familia> ObterPorId(ApplicationDbContext context, string familiaId)
    {
        return context.Familias!
            .Where(t => t.Id.ToString() == familiaId).ToList();
    }
    private static List<Familia> ObterPorNome(ApplicationDbContext context, string nome, Guid escolaId)
    {
        return context.Familias!
            .Where(t => t.EscolaId == escolaId && t.Nome.Contains(nome) )
            .OrderBy(t => t.Nome).ToList();
    }
}
