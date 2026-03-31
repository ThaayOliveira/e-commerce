using Ecommerce.API.Middlewares;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Infrastructure.Repositories;
using Ecommerce.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

//Banco
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Injeção de dependência
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddAutoMapper(typeof(ProdutoProfile));

//controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

//pipeline
app.UseHttpsRedirection();

app.MapControllers();

app.Run();