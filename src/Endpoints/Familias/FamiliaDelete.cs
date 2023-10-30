using Microsoft.AspNetCore.Mvc;
using w_escolas.Endpoints;
using w_escolas.Infra.Data;
using w_escolas.Shared;

namespace ken_lo.Endpoints.Familias;

public class FamiliaDelete
{
    public static string Template => "/familias/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        [FromRoute] Guid id,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        if (context.Familias == null)
            return Results.UnprocessableEntity();
        if (context.Alunos == null)
            return Results.UnprocessableEntity();
        
        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var familia = context.Familias.Where(e => e.Id == id).FirstOrDefault();
        if (familia == null)
            return Results.NotFound();

        if (familia.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        if (NaoPodeExcluir(context, id))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Familias.Remove(familia);
        context.SaveChanges();

        return Results.Ok();
    }

    private static readonly List<string> errorMessages = new();
    // private static void TemAtletaVinculado(ApplicationDbContext context, Guid tipoDeCursoId)
    // {
    //     if (context.Alunos!.Where(t => t.TipoDeCursoId == tipoDeCursoId).Any())
    //         errorMessages.Add("Existe(m) Curso(s) vinculados");
    // }
    private static bool NaoPodeExcluir(ApplicationDbContext context, Guid familiaId)
    {
        errorMessages.Clear();
        // TemAtletaVinculado(context, tipoDeCursoId);
        return errorMessages.Count > 0;
    }
}
