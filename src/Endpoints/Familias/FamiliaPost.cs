using ken_lo.Domain.Familias;
using ken_lo.Endpoints.Familias.dtos;
using w_escolas.Endpoints;
using w_escolas.Shared;

namespace ken_lo.Endpoints.Familias;

public class FamiliaPost
{
    public static string Template => "/familias";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;
    public static IResult Action(
        FamiliaRequest familiaRequest,
        ApplicationDbContext context,
        UserInfo userInfo)
    {
        if (context.Familias == null)
            return Results.UnprocessableEntity();

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var familia = DtoToObj(escolaIdDoUsuarioCorrente, familiaRequest);
        var validator = new FamiliaValidator();
        var validation = validator.Validate(familia);
        if(!validation.IsValid)
            return Results.ValidationProblem(validation.Errors.ConvertToProblemDetails());

        if (NaoPodeIncluir(context, familia))
            return Results.ValidationProblem(errorMessages.ConvertToProblemDetails());

        context.Familias.Add(familia);
        context.SaveChanges();
        return Results.Created($"{Template}/{familia.Id}", familia.Id);
    }

    private static Familia DtoToObj(Guid escolaId,
        FamiliaRequest tipoDeCursoRequest)
    {
        return new Familia(
            escolaId,
            tipoDeCursoRequest.Nome!
        );
    }

    private static readonly List<string> errorMessages = new();

    private static void VerificarComMesmoNome(
        ApplicationDbContext context, Familia familia)
    {
        if (context.Familias!.Where(t =>
            t.Nome == familia.Nome &&
            t.EscolaId == familia.EscolaId).Any())
            errorMessages.Add($"Já existe Família {familia.Nome}.");
    }

    private static bool NaoPodeIncluir(ApplicationDbContext context, Familia familia)
    {
        errorMessages.Clear();
        VerificarComMesmoNome(context, familia);
        return errorMessages.Count > 0;
    }
}
