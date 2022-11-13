using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ReinoTrebolApi.Extensiones;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var info = new OpenApiInfo
    {
        Title = "Reino del Trebol",
        Version = "v1",
        Description = "Provide access to Reino del Trebol API",
    };
    options.SwaggerDoc("v1", info);
});

//Add DB Connection
builder.Services.AddReinoTrebolDbContext(builder.Configuration.GetConnectionString("ReinoTrebolDatabase"));

// Add config of scoped services and Mappers.
builder.Services.AddServices();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
    options.ReturnHttpNotAcceptable = true;
});

builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
