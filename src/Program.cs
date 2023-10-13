using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using w_escolas.Endpoints.Alunos;
using w_escolas.Endpoints.Cursos;
using w_escolas.Endpoints.Escolas;
using w_escolas.Endpoints.Matriculas;
using w_escolas.Endpoints.Security;
using w_escolas.Endpoints.Temporadas;
using w_escolas.Endpoints.TiposDeCursos;
using w_escolas.Endpoints.Turmas;
using w_escolas.Endpoints.Usuarios;
using w_escolas.Infra.Data.DapperQueries;
using w_escolas.Infra.Data.DapperQueries.Matriculas;
using w_escolas.Infra.SendGrid;
using w_escolas.Shared;

var builder = WebApplication.CreateBuilder(args);

// var logger = new LoggerConfiguration()
//     // .WriteTo.Console(LogEventLevel.Information)
//     .ReadFrom.Configuration(builder.Configuration)
//     .Enrich.FromLogContext()
//     .CreateLogger();
// Log.Logger = logger;
// builder.Logging.ClearProviders();
// builder.Logging.AddSerilog(logger);
// builder.Host.UseSerilog(logger);

builder.Services.AddSqlServer<ApplicationDbContext>(
    builder.Configuration["Database:ConnectionString"]!);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    //options.Password.RequireNonAlphanumeric = false;
    //options.Password.RequireUppercase = false;
    //options.Password.RequireLowercase = false;
    //options.Password.RequireDigit = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
// builder.Services.AddAuthorization();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]!))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("AdminPolicy", p =>
        p.RequireAuthenticatedUser().RequireClaim("Admin"));
});

builder.Services.AddScoped<QueryAllUsersWithClaimNomeDoUsuario>();
builder.Services.AddScoped<MatriculasDoAlunoQuery>();
builder.Services.AddScoped<MatriculasDoCursoQuery>();
builder.Services.AddScoped<MatriculasDaTemporadaQuery>();
builder.Services.AddScoped<UserInfo>();

builder.Services.AddTransient<IEmailSender, SendGridEmailSender>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapMethods(UsuarioPost.Template, UsuarioPost.Methods, UsuarioPost.Handle);
app.MapMethods(UsuarioGetAll.Template, UsuarioGetAll.Methods, UsuarioGetAll.Handle);

app.MapMethods(EmailConfirmationPost.Template, EmailConfirmationPost.Methods, EmailConfirmationPost.Handle);
app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handle);
app.MapMethods(ForgottenPasswordPost.Template, ForgottenPasswordPost.Methods, ForgottenPasswordPost.Handle);
app.MapMethods(PasswordResetPost.Template, PasswordResetPost.Methods, PasswordResetPost.Handle);

app.MapMethods(EscolaPost.Template, EscolaPost.Methods, EscolaPost.Handle);
app.MapMethods(EscolaGetAll.Template, EscolaGetAll.Methods, EscolaGetAll.Handle);
app.MapMethods(EscolaPut.Template, EscolaPut.Methods, EscolaPut.Handle);
app.MapMethods(EscolaDelete.Template, EscolaDelete.Methods, EscolaDelete.Handle);

app.MapMethods(TipoDeCursoPost.Template, TipoDeCursoPost.Methods, TipoDeCursoPost.Handle);
app.MapMethods(TipoDeCursoGet.Template, TipoDeCursoGet.Methods, TipoDeCursoGet.Handle);
app.MapMethods(TipoDeCursoDelete.Template, TipoDeCursoDelete.Methods, TipoDeCursoDelete.Handle);
app.MapMethods(TipoDeCursoPut.Template, TipoDeCursoPut.Methods, TipoDeCursoPut.Handle);

app.MapMethods(CursoPost.Template, CursoPost.Methods, CursoPost.Handle);
app.MapMethods(CursoGet.Template, CursoGet.Methods, CursoGet.Handle);
app.MapMethods(CursoDelete.Template, CursoDelete.Methods, CursoDelete.Handle);
app.MapMethods(CursoPut.Template, CursoPut.Methods, CursoPut.Handle);
app.MapMethods(CursoGetArvore.Template, CursoGetArvore.Methods, CursoGetArvore.Handle);

app.MapMethods(TurmaPost.Template, TurmaPost.Methods, TurmaPost.Handle);
app.MapMethods(TurmaGet.Template, TurmaGet.Methods, TurmaGet.Handle);
app.MapMethods(TurmaDelete.Template, TurmaDelete.Methods, TurmaDelete.Handle);
app.MapMethods(TurmaPut.Template, TurmaPut.Methods, TurmaPut.Handle);

app.MapMethods(AlunoPost.Template, AlunoPost.Methods, AlunoPost.Handle);
app.MapMethods(AlunoGet.Template, AlunoGet.Methods, AlunoGet.Handle);
app.MapMethods(AlunoDelete.Template, AlunoDelete.Methods, AlunoDelete.Handle);
app.MapMethods(AlunoPut.Template, AlunoPut.Methods, AlunoPut.Handle);
app.MapMethods(AlunoImport.Template, AlunoImport.Methods, AlunoImport.Handle);

app.MapMethods(TemporadaPost.Template, TemporadaPost.Methods, TemporadaPost.Handle);
app.MapMethods(TemporadaGet.Template, TemporadaGet.Methods, TemporadaGet.Handle);
app.MapMethods(TemporadaDelete.Template, TemporadaDelete.Methods, TemporadaDelete.Handle);
app.MapMethods(TemporadaPut.Template, TemporadaPut.Methods, TemporadaPut.Handle);
app.MapMethods(TemporadaGetAnos.Template, TemporadaGetAnos.Methods, TemporadaGetAnos.Handle);

app.MapMethods(MatriculaPost.Template, MatriculaPost.Methods, MatriculaPost.Handle);
app.MapMethods(MatriculaGet.Template, MatriculaGet.Methods, MatriculaGet.Handle);
app.MapMethods(MatriculaDelete.Template, MatriculaDelete.Methods, MatriculaDelete.Handle);
app.MapMethods(MatriculaCancelar.Template, MatriculaCancelar.Methods, MatriculaCancelar.Handle);
app.MapMethods(MatriculaAlterarData.Template, MatriculaAlterarData.Methods, MatriculaAlterarData.Handle);
app.MapMethods(MatriculasDoAlunoGet.Template, MatriculasDoAlunoGet.Methods, MatriculasDoAlunoGet.Handle);
app.MapMethods(MatriculasDoCursoGet.Template, MatriculasDoCursoGet.Methods, MatriculasDoCursoGet.Handle);
app.MapMethods(MatriculasDaTemporadaGet.Template, MatriculasDaTemporadaGet.Methods, MatriculasDaTemporadaGet.Handle);

app.Run();



// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
