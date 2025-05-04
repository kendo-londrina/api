using Microsoft.EntityFrameworkCore;
using ken_lo.Domain;
using w_escolas.Endpoints.Alunos.dtos;
using w_escolas.Shared;

namespace ken_lo.Endpoints.Alunos;

public static class AlunoGet
{
    public static string Template => "/alunos";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        AlunoFilter? filter,
        ApplicationDbContext context,
        UserInfo userInfo,
        int page = 1, int rowsPerPage = 10, string orderBy = "Nome", string sortOrder = "asc")
    {
        if (context.Alunos == null)
            return Results.UnprocessableEntity();
        
        if (rowsPerPage > 100)
            return Results.Problem(title: "Max rowsPerPage value must be 100", statusCode: 400);

        var escolaIdDoUsuarioCorrente = userInfo.GetEscolaId();

        var queryBase = context.Alunos.AsNoTracking()
            .Where(t => t.EscolaId == escolaIdDoUsuarioCorrente);
        IQueryable<Aluno> queryOrder = orderBy.ToLower() switch
        {
            "codigo" => (sortOrder == "asc")
                ? queryBase.OrderBy(t => t.Codigo)
                : queryBase.OrderByDescending(t => t.Codigo),
            "datanascimento" => (sortOrder == "asc")
                ? queryBase.OrderBy(t => t.DataNascimento)
                : queryBase.OrderByDescending(t => t.DataNascimento),
            _ => (sortOrder == "asc")
                ? queryBase.OrderBy(t => t.Nome)
                : queryBase.OrderByDescending(t => t.Nome),
        };
        var queryFiltered = ApplyFilter(queryOrder, filter);

        var queryPaginated = queryFiltered.Skip((page - 1) * rowsPerPage).Take(rowsPerPage);

        var alunos = queryPaginated.ToList();

        var response = alunos.Select(
            t => new AlunoResponse
            {
                Id = t.Id,
                Codigo = t.Codigo,
                Nome = t.Nome,
                DataNascimento = t.DataNascimento,
                Nacionalidade = t.Nacionalidade,
                UFNascimento = t.UfNascimento,
                CidadeNascimento = t.CidadeNascimento,
                Sexo = t.Sexo,
                RG = t.Rg,
                CPF = t.Cpf,
                Email = t.Email,
                TelCelular = t.TelCelular,
                Religiao = t.Religiao
            }
        );

        if (filter != null && filter.Id != null && filter.Id != "")
            return Results.Ok(response);

        var pageDto = new PageDto<Aluno> { Count = queryFiltered.ToList().Count, Data = alunos };
        return Results.Ok(pageDto);
    }

    private static IQueryable<Aluno> ApplyFilter(IQueryable<Aluno> inputQuery, AlunoFilter? filter)
    {
        if (filter == null)
            return inputQuery;

        if (filter.Id != null && filter.Id != "")
            return inputQuery.Where(t => t.Id.ToString() == filter.Id);
        else if (filter.CodigoOuNome != null)
            return inputQuery.Where(t =>
                t.Codigo!.Contains(filter.CodigoOuNome) ||
                t.Nome!.Contains(filter.CodigoOuNome));
        return inputQuery;
    }
}
