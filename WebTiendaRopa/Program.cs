using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TiendaRopa.Data.Context;
using TiendaRopa.Modelos;
using WebTiendaRopa.DTOs;
using WebTiendaRopa.Repository;
using WebTiendaRopa.Servicios;
using WebTiendaRopa.Validations;

var builder = WebApplication.CreateBuilder(args);
//Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IValidator<RopaIngresoDto>,RopaValidations>();
builder.Services.AddScoped<IValidator<UsuarioRegistroDto>, UsuarioRegistroValidations>();
builder.Services.AddScoped<IValidator<UsuarioIngresoDto>, UsuarioIngresoValidations>();
builder.Services.AddScoped<IValidator<CompraDto>, CompraValidations>();
builder.Services.AddScoped<IValidator<FacturaRespuestaDto>, FacturaValidations>();
builder.Services.AddScoped<IValidator<FiltroRopaDto>, FiltroRopaValidations>();
builder.Services.AddScoped<IValidator<RopaIngresoFiltroDto>, IngresoCompraValidations>();

//add services to services
builder.Services.AddScoped<IRopasServicios<RopaRespuestaListadoDto, RopaRespuestaDetalles, RopaRespuestaCompleta>, RopaServicios>();
builder.Services.AddScoped<IUsuarioServicios<UsuarioRespuestaTotalDto, UsuarioRespuestaDto>,UsuarioServicios>();
builder.Services.AddScoped<IFacturaServicios<FacturaRespuestaDto, FacturaRespuestaCompletaDto>,FacturaServicios>();

//add services to repository

builder.Services.AddScoped<IRopaRepository<Ropa>, RopaRepository>();
builder.Services.AddScoped<IUsuarioRepository<Usuario>,UsuarioRepository>();
builder.Services.AddScoped<IFacturaRepository<Factura>,FacturaRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();