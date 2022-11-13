using System.Text.Json;
using System.Text.Json.Serialization;
using ReinoTrebolApi.Extensiones;
using ReinoTrebolApi.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddNewtonsoftJson();

//Add DB Connection
builder.Services.AddReinoTrebolDbContext(builder.Configuration.GetConnectionString("ReinoTrebolDatabase"));

// Add config of scoped services and Mappers.
builder.Services.AddServices();

builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
    options.ReturnHttpNotAcceptable = true;
    options.InputFormatters.Insert(0, JsonPatchInputFormatter.GetJsonPatchInputFormatter());
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
