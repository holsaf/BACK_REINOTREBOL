namespace ReinoTrebolApi.Extensiones
{
    using AutoMapper;
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
    using ReinoTrebolApi.Controllers.Mappers;
    using ReinoTrebolApi.Models.Data.DbContext;
    using ReinoTrebolApi.Models.Resource;
    using ReinoTrebolApi.Repository.Solicitud;
    using ReinoTrebolApi.Services.Mappers;
    using ReinoTrebolApi.Services.Solicitud;
    using ReinoTrebolApi.Validator;

    public static class ServiceCollectionExtensions
    {
        public static void AddReinoTrebolDbContext (this IServiceCollection collection, string connectionString)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
            collection.AddDbContext<IReinotrebolDbContext, ReinoTrebolDbContext>(options => options
            .UseMySql(connectionString, serverVersion, b => b.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, entity) => $"{schema ?? "dbo"}_{entity}"))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());
        }

        // AutoBuild Db config
        public static void BuidDataBase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<ReinoTrebolDbContext>();
            dataContext.Database.Migrate();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISolicitudRepository, SolicitudRepository>();
            services.AddScoped<ISolicitudService, SolicitudService>();
            services.AddScoped<IValidator<SolicitudPost>, SolicitudPostValidator>();
            services.AddScoped<IValidator<JsonPatchDocument<SolicitudPatch>>, SolicitudPatchValidator>();
            services.AddScoped<IValidator<Solicitud>, SolicitudPutValidator>();

            // Configuracion de Auto Mapper 
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ControllerMappingProfile());
                mc.AddProfile(new ServiceMappingProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
