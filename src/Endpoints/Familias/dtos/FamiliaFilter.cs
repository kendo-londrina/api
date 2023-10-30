namespace ken_lo.Endpoints.Familias.dtos;

public class FamiliaFilter
{
    public string? Id { get; set; }
    public string? Nome { get; set; }

    public static ValueTask<FamiliaFilter?> BindAsync(HttpContext context)
    {
        var result = new FamiliaFilter
        {
            Id = context.Request.Query["Id"],
            Nome = context.Request.Query["Nome"]
        };

        Console.WriteLine(result);

        return ValueTask.FromResult<FamiliaFilter?>(result);
    }
}
