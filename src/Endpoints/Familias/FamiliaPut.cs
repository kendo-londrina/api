using ken_lo.Domain.Familias;
using ken_lo.Endpoints.Familias.dtos;
using Microsoft.AspNetCore.Mvc;
using w_escolas.Endpoints;
using w_escolas.Shared;

namespace ken_lo.Endpoints.Familias;

public class FamiliaPut
{
    public static string Template => "/familias/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid id,
        FamiliaRequest familiaRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        if (context.Familias == null)
            return Results.UnprocessableEntity();

        var familia = context.Familias.Where(e => e.Id == id).FirstOrDefault();
        if (familia == null)
            return Results.NotFound();

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();
        if (familia.EscolaId != escolaIdDoUsuarioCorrente)
            return Results.ValidationProblem(
                "Não é proprietário da escola".ConvertToProblemDetails()
            );

        familia.Alterar(familiaRequest.Nome!);
        var validator = new FamiliaValidator();
        var validation = validator.Validate(familia);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeAlterar(context, familia))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Familias.Update(familia);
        context.SaveChanges();
        return Results.Ok();
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoNome(ApplicationDbContext context, Familia familia)
    {
        if (context.Familias!.Where(
            t => t.Nome == familia.Nome
            && t.Id != familia.Id).Any())
            errorMessages.Add($"Já existe Família com nome {familia.Nome}.");
    }

    private static bool NaoPodeAlterar(ApplicationDbContext context, Familia familia)
    {
        errorMessages.Clear();
        VerificarComMesmoNome(context, familia);
        return errorMessages.Count > 0;
    }
}
