using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ReinoTrebolApi.Extensiones;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddReinoTrebolDbContext(builder.Configuration.GetConnectionString("ReinoTrebolDatabase"));
builder.Services.AddServices();

//var corsConfiguration = builder.Configuration.GetSection("Cors").Get<CorsConfiguration>();

//builder.Services.AddControllers(options =>
//{
//    options.InputFormatters.Insert(0, JsonPatchInputFormatter.GetJsonPatchInputFormatter());
//})
//                .AddFluentValidation(
//                fvc =>
//                {
//                    fvc.RegisterValidatorsFromAssemblyContaining<Program>();
//                })
//                .AddJsonOptions(options =>
//                {
//                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
//                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
//                    options.JsonSerializerOptions.WriteIndented = true;
//                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//                });

//builder.Services.AddFluentValidationAutoValidation();

//builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
