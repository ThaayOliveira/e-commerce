using Ecommerce.API.Middlewares;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Application.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Ecommerce.Application.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//Banco
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Injeção de dependência
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

//auth
builder.Services.AddScoped<AuthService>();

//automapper
builder.Services.AddAutoMapper(typeof(ProdutoProfile));

//validacao
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutoCreateValidator>();

//controllers
builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();  
// builder.Services.AddSwaggerGen();

//jwt
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddAuthorization();

//build
var app = builder.Build();


//middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

//pipeline
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();