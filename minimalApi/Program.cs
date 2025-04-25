using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using minimalApi;
using minimalApi.Models;
using minimalApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
    
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("admin", p => p.RequireRole("admin"));
});

builder.Services.AddTransient<JwtService>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", (JwtService service) =>
{
    var user = new User
    {
        Id = 10,
        Name = "João da Silva",
        Email = "joao@email.com",
        Password = "123456",
        Image = null, // ou um caminho como "joao.jpg"
        Roles = new List<string> { "admin", "user" }
    };

    return service.Create(user);
});

app.Run();
