/*
    Quincy Landvreugd
    StudentNr: S1145622
*/


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CS_Eind.Data;
using CS_Eind.Models.Mappers;
using CS_Eind.Repositories;
using CS_Eind.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddSwaggerGen();





builder.Services.AddDbContext<AirBNBDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AirBNBDbContext") ?? throw new InvalidOperationException("Connection string 'AirBNBDbContext' not found.")));

builder.Services.AddControllers();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.UseApiBehavior = false;
})
    .AddMvc();
builder.Services.AddScoped<IAirBnBRepository, AirBnBRepository>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
       
        );
    app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//app.Run("http://localhost:7279");
